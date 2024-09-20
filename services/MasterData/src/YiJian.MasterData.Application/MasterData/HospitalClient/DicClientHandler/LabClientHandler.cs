using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.External.LongGang.Lab;
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Container;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：检验字典同步
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/16 18:04:23
    /// </summary>
    public class LabClientHandler : MasterDataAppService, IDistributedEventHandler<LabEto>,
    ITransientDependency
    {
        private readonly ILabCatalogRepository _labCatalogRepository;
        private readonly ILabProjectRepository _labProjectRepository;
        private readonly ILabTargetRepository _labTargetRepository;
        private readonly ILabContainerRepository _labContainerRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="labCatalogRepository"></param>
        /// <param name="labProjectRepository"></param>
        /// <param name="labTargetRepository"></param>
        /// <param name="labContainerRepository"></param>
        public LabClientHandler(ILabCatalogRepository labCatalogRepository
            , ILabProjectRepository labProjectRepository
            , ILabTargetRepository labTargetRepository
            , ILabContainerRepository labContainerRepository)
        {
            _labCatalogRepository = labCatalogRepository;
            _labProjectRepository = labProjectRepository;
            _labTargetRepository = labTargetRepository;
            _labContainerRepository = labContainerRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(LabEto eventData)
        {
            if (eventData == null) return;

            List<LabEto> list = new List<LabEto>() { eventData };
            List<LabProject> labProjectList = new List<LabProject>();
            List<LabTarget> labTargetList = new List<LabTarget>();
            List<LabCatalog> catalogList = new List<LabCatalog>();
            var labProjectSort = 0;
            var labTargetSort = 0;
            var labCatalog = list.GroupBy(g => new { g.SampleTypeId }).ToList();
            int sortCata = 0;

            foreach (var catalogItem in labCatalog)
            {
                var sampleTyme = catalogItem.FirstOrDefault().SampleType;
                catalogList.Add(new LabCatalog()
                {
                    CatalogCode = catalogItem.Key.SampleTypeId,
                    CatalogName = sampleTyme,
                    PyCode = sampleTyme.FirstLetterPY(),
                    WbCode = sampleTyme.FirstLetterWB(),
                    Sort = sortCata++
                });
                var labProject = catalogItem.GroupBy(g => new { g.GroupId }).ToList();
                labProject.ForEach(x =>
                {
                    var catalogAndProjectCode = catalogItem.Key.SampleTypeId + x.Key.GroupId;
                    labProjectSort++;
                    LabEto dto = x.FirstOrDefault();
                    labProjectList.Add(new LabProject()
                    {
                        CatalogAndProjectCode = catalogAndProjectCode,
                        CatalogCode = catalogItem.Key.SampleTypeId,
                        CatalogName = sampleTyme,
                        ProjectCode = x.Key.GroupId,
                        ProjectName = dto?.GroupName,
                        SpecimenCode = dto?.SpecimenNo,
                        SpecimenName = dto?.SpecimenName,
                        ExecDeptCode = dto?.DepartmentCode,
                        ExecDeptName = dto?.DepartmentName,
                        ContainerCode = dto?.ContainerId,
                        ContainerName = dto?.ContainerType,
                        Unit = dto?.Unit,
                        Price = x.Sum(s => Convert.ToDecimal(s.Price.ToString("F2")) * (string.IsNullOrEmpty(s.TotalNumber) ? 0 : decimal.Parse(s.TotalNumber))),
                        Sort = labProjectSort,
                        PyCode = dto?.SpellCode,
                        WbCode = dto?.GroupName.FirstLetterWB(),
                        AddCard = dto?.AddCard,
                        IsActive = dto?.UseFlag == "0"
                    });
                    x.ForEach(f =>
                    {
                        labTargetSort++;
                        labTargetList.Add(new LabTarget()
                        {
                            CatalogAndProjectCode = catalogAndProjectCode,
                            ProjectCode = x.Key.GroupId,
                            TargetCode = f.GroupsId,
                            TargetName = f.GroupsName,
                            Sort = labTargetSort,
                            PyCode = f.GroupsName.FirstLetterPY(),
                            WbCode = f.GroupsName.FirstLetterWB(),
                            Price = decimal.Parse(f.Price.ToString("F2")),
                            TargetUnit = f.Unit,
                            Qty = string.IsNullOrEmpty(f.TotalNumber) ? 0 : decimal.Parse(f.TotalNumber),
                            ProjectType = f.ProjectType,
                            ProjectMerge = f.ProjectMerge,
                            IsActive = f.UseFlag == "0"
                        });
                    });
                });
            }


            #region 检验目录
            //获取已存在的检查部位信息
            List<LabCatalog> catalog = await _labCatalogRepository.ToListAsync();
            //查询新增的部位信息
            //新增检查部位
            List<LabCatalog> addCatalog = catalogList.Where(x => catalog.All(a => a.CatalogCode != x.CatalogCode))
                .ToList();
            //删除检查部位
            List<LabCatalog> deleteCatalog = catalog.Where(x => catalogList.All(a => a.CatalogCode != x.CatalogCode))
                .ToList();
            //修改检查部位
            List<LabCatalog> updateCatalog = new List<LabCatalog>();
            catalogList.RemoveAll(addCatalog);
            catalogList.RemoveAll(deleteCatalog);
            catalogList.ForEach(x =>
            {
                LabCatalog data = catalog.FirstOrDefault(g => x.CatalogCode == g.CatalogCode && x.CatalogName != g.CatalogName);
                if (data != null)
                {
                    data.Modify(x.CatalogName, "", "", data.Sort, data.IsActive);
                    updateCatalog.Add(data);
                }
            });
            if (addCatalog.Any())
            {
                await _labCatalogRepository.InsertManyAsync(addCatalog);
            }
            if (updateCatalog.Any())
            {
                await _labCatalogRepository.UpdateManyAsync(updateCatalog);
            }

            if (deleteCatalog.Any())
            {
                await _labCatalogRepository.DeleteManyAsync(deleteCatalog);
            }

            #endregion
            #region 检验项目
            List<LabProject> projectList = await _labProjectRepository.ToListAsync();
            List<LabProject> updateProject = new List<LabProject>();
            List<LabProject> addProject = labProjectList.Where(x => projectList.All(a => a.CatalogAndProjectCode != x.CatalogAndProjectCode))
                .ToList();
            var deleteProject = projectList.Where(x => labProjectList.All(a => a.CatalogAndProjectCode != x.CatalogAndProjectCode))
                .ToList();
            //去掉已删除的项
            labProjectList.RemoveAll(deleteProject);
            //去掉新增的项
            labProjectList.RemoveAll(addProject);

            labProjectList.ForEach(x =>
            {
                var data = projectList.FirstOrDefault(g =>
                    g.CatalogAndProjectCode == x.CatalogAndProjectCode
                    && (g.ProjectName != x.ProjectName
                        || x.Price != g.Price
                        || x.Unit != g.Unit
                        || x.ContainerCode != g.ContainerCode
                        || x.ContainerName != g.ContainerName
                        || x.PyCode != g.PyCode
                        || x.SpecimenCode != g.SpecimenCode
                        || x.SpecimenName != g.SpecimenName
                        || x.ExecDeptCode != g.ExecDeptCode
                        || x.ExecDeptName != g.ExecDeptName
                        || x.AddCard != g.AddCard
                        || x.CatalogCode != g.CatalogCode
                        || x.CatalogName != g.CatalogName
                        || x.IsActive != g.IsActive));
                if (data != null)
                {
                    data.Modify(x.ProjectName, x.CatalogCode, x.CatalogName,
                        x.SpecimenCode, x.SpecimenName, x.ExecDeptCode,
                        x.ExecDeptName, "", "", data.Sort, x.Unit,
                        //x.Price,
                        data.OtherPrice, x.IsActive, x.ContainerCode, x.ContainerName);
                    updateProject.Add(data);
                }
            });


            //检验项目
            if (addProject.Any())
            {
                await _labProjectRepository.InsertManyAsync(addProject);
            }

            if (updateProject.Any())
            {
                await _labProjectRepository.UpdateManyAsync(updateProject);
            }

            if (deleteProject.Any())
            {
                await _labProjectRepository.DeleteManyAsync(deleteProject);
            }

            #endregion
            #region 检验明细

            //查询检验已存在数据
            List<LabTarget> labTarget = await _labTargetRepository.ToListAsync();
            List<LabTarget> addTarget = labTargetList.Where(x => !labTarget.Any(a => a.TargetCode == x.TargetCode && a.CatalogAndProjectCode == x.CatalogAndProjectCode))
                .ToList();
            List<LabTarget> deleteTarget = labTarget.Where(x => !labTargetList.Any(a => a.TargetCode == x.TargetCode && a.CatalogAndProjectCode == x.CatalogAndProjectCode))
                .ToList();

            List<LabTarget> updateTarget = new List<LabTarget>();
            IEnumerable<LabTarget> maybeUpdateLabTargetList = labTargetList.Except(deleteTarget).Except(addTarget);
            // //去掉已删除的项
            // labTargetList.RemoveAll(deleteTarget);
            // //去掉新增的项
            // labTargetList.RemoveAll(addTarget);
            maybeUpdateLabTargetList.ForEach(x =>
            {
                LabTarget data = labTarget.FirstOrDefault(g =>
                    g.TargetCode == x.TargetCode && x.CatalogAndProjectCode == g.CatalogAndProjectCode
                    && (g.TargetName != x.TargetName
                        || x.Price != g.Price
                        || x.TargetUnit != g.TargetUnit
                        || x.ProjectType != g.ProjectType
                        || x.ProjectMerge != g.ProjectMerge
                        || x.Qty != g.Qty
                        || x.IsActive != g.IsActive));
                if (data != null)
                {
                    data.Modify(x.TargetName, data.Sort, x.TargetUnit, x.Qty, x.Price, InsuranceCatalog.Self, x.IsActive);
                    updateTarget.Add(data);
                }
            });

            //检验明细
            if (addTarget.Any())
            {
                await _labTargetRepository.InsertManyAsync(addTarget);
            }

            if (updateTarget.Any())
            {
                await _labTargetRepository.UpdateManyAsync(updateTarget);
            }

            if (deleteTarget.Any())
            {
                await _labTargetRepository.DeleteManyAsync(deleteTarget);
            }

            #endregion
            #region 容器

            //查询检验容器已存在数据
            List<LabContainer> container = await _labContainerRepository.ToListAsync();
            List<LabContainer> containerList = new List<LabContainer>();
            //添加新增的检验容器
            labProjectList.GroupBy(g => new { g.ContainerCode, g.ContainerName })
                .ForEach(f =>
                {
                    if (string.IsNullOrEmpty(f.Key.ContainerName))
                    {
                        return;
                    }
                    containerList.Add(new LabContainer(
                        containerCode: f.Key.ContainerCode, containerName: f.Key.ContainerName, // 容器名称
                        containerColor: "", // 容器颜色
                        isActive: true
                    ));
                });
            //新增的容器
            List<LabContainer> addContainer = containerList.Where(x => container.All(a => a.ContainerCode != x.ContainerCode))
                .ToList();
            //删除的容器
            List<LabContainer> deleteContainer = container
                .Where(x => containerList.All(a => a.ContainerCode != x.ContainerCode))
                .ToList();
            //修改容器
            List<LabContainer> updateContainer = new List<LabContainer>();
            containerList.RemoveAll(addContainer);
            containerList.RemoveAll(deleteContainer);
            containerList.ForEach(x =>
            {
                LabContainer data = container.FirstOrDefault(g => x.ContainerCode == g.ContainerCode && x.ContainerName != g.ContainerName);
                if (data != null)
                {
                    data.Modify(x.ContainerName, "", true);
                    updateContainer.Add(data);
                }
            });
            if (addContainer.Any())
            {
                await _labContainerRepository.InsertManyAsync(addContainer);
            }

            if (updateContainer.Any())
            {
                await _labContainerRepository.UpdateManyAsync(updateContainer);
            }

            if (deleteContainer.Any())
            {
                await _labContainerRepository.DeleteManyAsync(deleteContainer);
            }

            #endregion

        }
    }
}
