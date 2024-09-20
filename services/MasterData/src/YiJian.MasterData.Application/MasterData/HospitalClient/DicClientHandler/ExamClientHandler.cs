using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.Exams;
using YiJian.MasterData.External.LongGang.ExamAndLab;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：同步检查字典
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/17 9:15:51
    /// </summary>
    public class ExamClientHandler : MasterDataAppService, IDistributedEventHandler<ExamAndLabEto>,
    ITransientDependency
    {
        private readonly IExamCatalogRepository _examCatalogRepository;
        private readonly IExamProjectRepository _examProjectRepository;
        private readonly IExamTargetRepository _examTargetRepository;
        private readonly ILogger<ExamClientHandler> _logger;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="examCatalogRepository"></param>
        /// <param name="examProjectRepository"></param>
        /// <param name="examTargetRepository"></param>
        /// <param name="logger"></param>
        public ExamClientHandler(IExamCatalogRepository examCatalogRepository
            , IExamProjectRepository examProjectRepository
            , IExamTargetRepository examTargetRepository
            , ILogger<ExamClientHandler> logger)
        {
            _examCatalogRepository = examCatalogRepository;
            _examProjectRepository = examProjectRepository;
            _examTargetRepository = examTargetRepository;
            _logger = logger;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(ExamAndLabEto eventData)
        {
            if (eventData == null) return;

            List<ExamCatalog> dict_catalog = await _examCatalogRepository.GetListAsync();
            List<ExamProject> dict_project = await _examProjectRepository.GetListAsync();
            List<ExamTarget> dict_target = await _examTargetRepository.GetListAsync();

            List<ExamAndLabEto> inspectGroupLists = new List<ExamAndLabEto>() { eventData };
            if (inspectGroupLists == null || inspectGroupLists.Count <= 0)
            {
                _logger.LogError("序列化检查组套数据失败");
            }

            //1.检查目录
            var catalogGroup = inspectGroupLists
                .GroupBy(x => new { x.SuperiorCode, x.SuperiorName, x.SampleTypeId, x.SampleType }).ToList();

            foreach (var item in catalogGroup)
            {
                //判断目录是否存在
                ExamCatalog selectCatalog = dict_catalog.Find(x =>
                    x.CatalogCode == item.Key.SampleTypeId && x.FirstNodeCode == item.Key.SuperiorCode);
                if (selectCatalog == null)
                {
                    //1.1 新增目录
                    var catalog = new ExamCatalog();
                    catalog.FirstNodeCode = item.Key.SuperiorCode;
                    catalog.FirstNodeName = item.Key.SuperiorName;
                    catalog.CatalogCode = item.Key.SampleTypeId;
                    catalog.CatalogName = item.Key.SampleType;
                    catalog.DisplayName = item.Key.SampleType;
                    catalog.PyCode = (item.Key.SuperiorName + item.Key.SampleType).FirstLetterPY();
                    catalog.WbCode = (item.Key.SuperiorName + item.Key.SampleType).FirstLetterWB();
                    catalog.Sort = 1;
                    await _examCatalogRepository.InsertAsync(catalog);
                    _logger.LogInformation($"{DateTime.Now}检查目录新增内容,{JsonConvert.SerializeObject(catalog)}");
                }
                else
                {
                    //1.2 修改目录
                    if (item.Key.SuperiorName != selectCatalog.FirstNodeName ||
                        item.Key.SampleType != selectCatalog.CatalogName)
                    {
                        selectCatalog.CatalogName = item.Key.SampleType;
                        selectCatalog.FirstNodeName = item.Key.SuperiorName;
                        await _examCatalogRepository.UpdateAsync(selectCatalog);
                        _logger.LogInformation($"{DateTime.Now}检查目录更新内容,{JsonConvert.SerializeObject(selectCatalog)}");
                    }
                }
            }


            //2.检查项目
            var groupLists = inspectGroupLists.GroupBy(x => new { x.GroupId, x.GroupName }).ToList();
            foreach (var group in groupLists)
            {
                ExamAndLabEto common = group.FirstOrDefault();
                var sumPrice = group.Sum(s =>
                    decimal.Parse(s.Price.ToString("F2")) * decimal.Parse(s.TotalNumber)); //计算总价
                ExamProject selectProject = dict_project.Find(x =>
                    x.CatalogCode == common.SampleTypeId && x.ProjectCode == common.GroupId);
                if (selectProject == null)
                {
                    //2.1 新增检查项目
                    ExamProject examProject = new ExamProject();
                    examProject.CatalogCode = common?.SampleTypeId;
                    examProject.CatalogName = common?.SampleType;
                    examProject.ProjectCode = group.Key.GroupId;
                    examProject.ProjectName = group.Key.GroupName;
                    examProject.ExecDeptCode = common?.DepartmentCode;
                    examProject.ExecDeptName = common?.DepartmentName;
                    examProject.Unit = common?.Unit;
                    examProject.Price = sumPrice;
                    examProject.GuideCode = common?.GuideCode;
                    examProject.PyCode = common?.SpellCode;
                    examProject.WbCode = group.Key.GroupName.FirstLetterWB();
                    examProject.AddCard = common?.AddCard;
                    examProject.IsActive = common?.UseFlag == "0";
                    await _examProjectRepository.InsertAsync(examProject);
                    _logger.LogInformation($"{DateTime.Now}检查项目新增内容,{JsonConvert.SerializeObject(examProject)}");
                }
                else
                {
                    if (common.UseFlag != (selectProject.IsActive == true ? "0" : "1")) //根据删除标志
                    {
                        //2.3 删除检查项目
                        selectProject.IsActive = common.UseFlag == "0" ? true : false;
                        await _examProjectRepository.UpdateAsync(selectProject);
                        _logger.LogInformation(
                            $"{DateTime.Now}检查项目删除内容,{JsonConvert.SerializeObject(selectProject)}");
                    }
                    else
                    {
                        //比对字段是否相同,不相同则更新
                        if (group.Key.GroupName != selectProject.ProjectName
                            || common.SampleType != selectProject.CatalogName
                            || common.DepartmentCode != selectProject.ExecDeptCode
                            || common.DepartmentName != selectProject.ExecDeptName
                            || common.Unit != selectProject.Unit
                            || sumPrice != selectProject.Price
                            || selectProject.GuideCode != common?.GuideCode
                            || common.AddCard != selectProject.AddCard
                            || common.UseFlag != (selectProject.IsActive ? "0" : "1"))
                        {
                            //2.2 更新检查项目
                            selectProject.ProjectName = group.Key.GroupName;
                            selectProject.CatalogName = common?.SampleType;
                            selectProject.ExecDeptCode = common?.DepartmentCode;
                            selectProject.ExecDeptName = common?.DepartmentName;
                            selectProject.Unit = common?.Unit;
                            selectProject.Price = sumPrice;
                            selectProject.PyCode = common?.SpellCode;
                            selectProject.AddCard = common?.AddCard;
                            selectProject.IsActive = common.UseFlag == "0";
                            selectProject.GuideCode = common?.GuideCode;
                            await _examProjectRepository.UpdateAsync(selectProject);
                            _logger.LogInformation(
                                $"{DateTime.Now}检查项目更新内容,{JsonConvert.SerializeObject(selectProject)}");
                        }
                    }
                }
            }


            // 3.检查明细

            foreach (ExamAndLabEto item in inspectGroupLists)
            {
                ExamTarget selectTarget = dict_target.Find(x =>
                    x.ProjectCode == item.GroupId && x.TargetCode == item.GroupsId);
                if (selectTarget == null)
                {
                    //3.1新增明细项目
                    ExamTarget examTarget = new ExamTarget();
                    examTarget.ProjectCode = item.GroupId;
                    examTarget.TargetCode = item.GroupsId;
                    examTarget.TargetName = item.GroupsName;
                    examTarget.Specification = "";
                    examTarget.PyCode = item.GroupsName.FirstLetterPY();
                    examTarget.WbCode = item.GroupsName.FirstLetterWB();
                    examTarget.Price = decimal.Parse(item.Price.ToString("F2"));
                    examTarget.TargetUnit = item.Unit;
                    examTarget.Qty = string.IsNullOrWhiteSpace(item.TotalNumber)
                        ? 0
                        : decimal.Parse(item.TotalNumber);
                    examTarget.ProjectType = item.ProjectType;
                    examTarget.ProjectMerge = item.ProjectMerge;
                    examTarget.IsActive = item.UseFlag == "0";
                    await _examTargetRepository.InsertAsync(examTarget);
                    _logger.LogInformation($"{DateTime.Now}检查明细新增内容,{JsonConvert.SerializeObject(examTarget)}");
                }
                else
                {
                    if (item.UseFlag == "1")
                    {
                        await _examTargetRepository.DeleteAsync(selectTarget);
                    }
                    else
                    {
                        //3.2 更新明细项目
                        if (selectTarget.TargetName != item.GroupsName
                            || selectTarget.Price != decimal.Parse(item.Price.ToString("F2"))
                            || selectTarget.TargetUnit != item.Unit
                            || selectTarget.Qty != decimal.Parse(item.TotalNumber)
                            || selectTarget.ProjectType != item.ProjectType
                            || selectTarget.ProjectMerge != item.ProjectMerge
                            || selectTarget.IsActive != (item.UseFlag == "0"))
                        {
                            selectTarget.TargetName = item.GroupsName;
                            selectTarget.Specification = "";
                            selectTarget.PyCode = item.GroupsName.FirstLetterPY();
                            selectTarget.WbCode = item.GroupsName.FirstLetterWB();
                            selectTarget.Price = decimal.Parse(item.Price.ToString("F2"));
                            selectTarget.TargetUnit = item.Unit;
                            selectTarget.Qty = string.IsNullOrWhiteSpace(item.TotalNumber)
                                ? 0
                                : decimal.Parse(item.TotalNumber);
                            selectTarget.ProjectType = item.ProjectType;
                            selectTarget.ProjectMerge = item.ProjectMerge;
                            selectTarget.IsActive = item.UseFlag == "0";
                            await _examTargetRepository.UpdateAsync(selectTarget);
                            _logger.LogInformation(
                                $"{DateTime.Now}检查明细更新内容,{JsonConvert.SerializeObject(selectTarget)}");
                        }
                    }

                }
            }

        }
    }
}
