using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.MasterData.Exams;
using YiJian.MasterData.External.LongGang.ExamAndLab;
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Container;

namespace YiJian.MasterData.External.LongGang;

/// <summary>
/// 检查检验订阅处理
/// </summary>
public class ExamAndLabEventHandler : MasterDataAppService, ILongGangHospitalEventHandler,
    IDistributedEventHandler<ExamAndLabEto>,
    ITransientDependency
{
    //public ILogger<ExamAndLabEventHandler> _logger { get; set; }

    //public ICapPublisher CapPublisher { get; set; }
    //public ILabCatalogRepository LabCatalogRepository { get; set; }
    //public ILabProjectRepository LabProjectRepository { get; set; }
    //public ILabTargetRepository LabTargetRepository { get; set; }
    //public ILabSpecimenRepository LabSpecimenRepository { get; set; }
    //public ILabContainerRepository LabContainerRepository { get; set; }


    //public IExamCatalogRepository ExamCatalogRepository { get; set; }
    //public IExamProjectRepository ExamProjectRepository { get; set; }
    //public IExamTargetRepository ExamTargetRepository { get; set; }

    private readonly ILogger<ExamAndLabEventHandler> _logger; 
    private readonly ICapPublisher _capPublisher;
    private readonly ILabCatalogRepository _labCatalogRepository;
    private readonly ILabProjectRepository _labProjectRepository;
    private readonly ILabTargetRepository _labTargetRepository;
    private readonly ILabSpecimenRepository _labSpecimenRepository;
    private readonly ILabContainerRepository _labContainerRepository; 
    private readonly IExamCatalogRepository _examCatalogRepository;
    private readonly IExamProjectRepository _examProjectRepository;
    private readonly IExamTargetRepository _examTargetRepository;

    /// <summary>
    /// 检查检验订阅处理
    /// </summary>
    public ExamAndLabEventHandler(
        ILogger<ExamAndLabEventHandler> logger,
        ICapPublisher capPublisher,
        ILabCatalogRepository labCatalogRepository,
        ILabProjectRepository labProjectRepository,
        ILabTargetRepository labTargetRepository,
        ILabSpecimenRepository labSpecimenRepository,
        ILabContainerRepository labContainerRepository,
        IExamCatalogRepository examCatalogRepository,
        IExamProjectRepository examProjectRepository,
        IExamTargetRepository examTargetRepository)
    {
        _logger = logger;
        _capPublisher = capPublisher;
        _labCatalogRepository = labCatalogRepository;
        _labProjectRepository = labProjectRepository;
        _labTargetRepository = labTargetRepository;
        _labSpecimenRepository = labSpecimenRepository;
        _labContainerRepository = labContainerRepository;
        _examCatalogRepository = examCatalogRepository;
        _examProjectRepository = examProjectRepository;
        _examTargetRepository = examTargetRepository;
    }

    /// <summary>
    /// 处理方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task HandleEventAsync(ExamAndLabEto item)
    {
        var uow = UnitOfWorkManager.Begin();

        try
        {
            _logger.LogInformation("InspectDicEvents:{0}", JsonConvert.SerializeObject(item));
            if (item == null) return;
            //1:检验
            if (item.InspectType == 1)
            {
                #region 检验

                var selectCatalog = await (await _labCatalogRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.CatalogCode == item.SampleTypeId);
                var selectProject = await (await _labProjectRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.CatalogCode == item.SampleTypeId && x.ProjectCode == item.GroupId);
                var selectTarget = await (await _labTargetRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.ProjectCode == item.GroupId && x.TargetCode == item.GroupsId);
                //1.检验目录
                //判断目录是否存在
                if (selectCatalog == null)
                {
                    //1.1 新增目录
                    var catalog = new LabCatalog();
                    catalog.CatalogCode = item.SampleTypeId;
                    catalog.CatalogName = item.SampleType;
                    catalog.PyCode = item.SampleType.FirstLetterPY();
                    catalog.WbCode = item.SampleType.FirstLetterWB();
                    await _labCatalogRepository.InsertAsync(catalog);
                    _logger.LogInformation($"检验目录新增内容,{JsonConvert.SerializeObject(catalog)}");
                }
                else
                {
                    //1.2 修改目录
                    if (item.SampleType != selectCatalog.CatalogName)
                    {
                        selectCatalog.CatalogName = item.SampleType;
                        await _labCatalogRepository.UpdateAsync(selectCatalog);
                        _logger.LogInformation($"检验目录更新内容,{JsonConvert.SerializeObject(selectCatalog)}");
                    }
                }


                //2.检验项目
                if (selectProject == null)
                {
                    //2.1 新增检验项目
                    var labProject = new LabProject();
                    labProject.CatalogCode = item?.SampleTypeId;
                    labProject.CatalogName = item?.SampleType;
                    labProject.ProjectCode = item.GroupId;
                    labProject.ProjectName = item.GroupName;
                    labProject.SpecimenCode = item?.SpecimenNo;
                    labProject.SpecimenName = item?.SpecimenName;
                    labProject.ExecDeptCode = item?.DepartmentCode;
                    labProject.ExecDeptName = item?.DepartmentName;
                    labProject.DepExecutionType = item?.DepExecutionType;
                    labProject.DepExecutionRules = item?.DepExecutionRules;
                    labProject.ContainerCode = item?.ContainerId;
                    labProject.ContainerName = item?.ContainerType;
                    labProject.Unit = item?.Unit;
                    labProject.Price = Convert.ToDecimal(item.Price);
                    labProject.PyCode = item?.SpellCode;
                    labProject.WbCode = item.GroupName.FirstLetterWB();
                    labProject.AddCard = item?.AddCard;
                    labProject.GuideCode = item?.GuideCode;
                    await _labProjectRepository.InsertAsync(labProject);
                    _logger.LogInformation($"检验项目新增内容,{JsonConvert.SerializeObject(labProject)}");
                }
                else
                {
                    if (item.UseFlag != (selectProject.IsActive == true ? "0" : "1")) //根据删除标志
                    {
                        //2.3 删除检验项目
                        selectProject.IsActive = item.UseFlag == "0" ? true : false; ;
                        await _labProjectRepository.UpdateAsync(selectProject);
                        _logger.LogInformation($"检验项目删除内容,{JsonConvert.SerializeObject(selectProject)}");
                    }
                    else
                    {
                        //2.2 更新检验项目
                        if (selectProject.CatalogName != item?.SampleType ||
                            selectProject.ExecDeptCode != item?.DepartmentCode ||
                            selectProject.ExecDeptName != item?.DepartmentName ||
                            selectProject.DepExecutionType != item?.DepExecutionType ||
                            selectProject.DepExecutionRules != item?.DepExecutionRules ||
                            selectProject.SpecimenName != item?.SpecimenName || 
                            selectProject.ContainerCode != item?.ContainerId ||
                            selectProject.ContainerName != item?.ContainerType ||
                            selectProject.ProjectName != item.GroupName ||
                            selectProject.Unit != item?.Unit ||
                            selectProject.Price!=Convert.ToDecimal(item?.Price) ||
                            selectProject.PyCode != item?.SpellCode ||selectProject.GuideCode != item?.GuideCode||
                            selectProject.AddCard != item?.AddCard)
                        {
                            //2.2 更新检验项目
                            selectProject.CatalogName = item?.SampleType;
                            selectProject.ExecDeptCode = item?.DepartmentCode;
                            selectProject.ExecDeptName = item?.DepartmentName;
                            selectProject.DepExecutionType = item?.DepExecutionType;
                            selectProject.DepExecutionRules = item?.DepExecutionRules;
                            selectProject.SpecimenName = item?.SpecimenName; 
                            selectProject.ContainerCode = item?.ContainerId;
                            selectProject.ContainerName = item?.ContainerType;
                            selectProject.Unit = item?.Unit;
                            selectProject.Price = Convert.ToDecimal(item?.Price);
                            selectProject.ProjectName = item.GroupName;
                            selectProject.PyCode = item?.SpellCode;
                            selectProject.AddCard = item?.AddCard;
                            selectProject.GuideCode = item?.GuideCode;
                            await _labProjectRepository.UpdateAsync(selectProject);
                            _logger.LogInformation($"检验项目更新内容,{JsonConvert.SerializeObject(selectProject)}");
                        }
                    }
                }


                //3.检验明细
                if (selectTarget == null)
                {
                    //3.1新增明细项目
                    var LabTarget = new LabTarget();
                    LabTarget.ProjectCode = item.GroupId;
                    LabTarget.TargetCode = item.GroupsId;
                    LabTarget.TargetName = item.GroupsName;
                    LabTarget.PyCode = item.GroupsName.FirstLetterPY();
                    LabTarget.WbCode = item.GroupsName.FirstLetterWB();
                    LabTarget.Price = decimal.Parse(item.Price.ToString("F2"));
                    LabTarget.TargetUnit = item.Unit;
                    LabTarget.Qty = string.IsNullOrWhiteSpace(item.TotalNumber)
                        ? 0
                        : decimal.Parse(item.TotalNumber);
                    LabTarget.ProjectType = item.ProjectType;
                    LabTarget.ProjectMerge = item.ProjectMerge;
                    await _labTargetRepository.InsertAsync(LabTarget);
                    _logger.LogInformation($"检验明细新增内容,{JsonConvert.SerializeObject(LabTarget)}");
                }
                else
                {
                    //3.2 更新明细项目
                    if (selectTarget.TargetName != item.GroupsName ||
                        selectTarget.Price != decimal.Parse(item.Price.ToString("F2")) ||
                        selectTarget.TargetUnit != item.Unit ||
                        selectTarget.Qty != decimal.Parse(item.TotalNumber) ||
                        selectTarget.ProjectType != item.ProjectType ||
                        selectTarget.ProjectMerge != item.ProjectMerge)
                    {
                        selectTarget.TargetName = item.GroupsName;
                        selectTarget.PyCode = item.GroupsName.FirstLetterPY();
                        selectTarget.WbCode = item.GroupsName.FirstLetterWB();
                        selectTarget.Price = decimal.Parse(item.Price.ToString("F2"));
                        selectTarget.TargetUnit = item.Unit;
                        selectTarget.Qty = string.IsNullOrWhiteSpace(item.TotalNumber)
                            ? 0
                            : decimal.Parse(item.TotalNumber);
                        selectTarget.ProjectType = item.ProjectType;
                        selectTarget.ProjectMerge = item.ProjectMerge;
                        await _labTargetRepository.UpdateAsync(selectTarget);
                        _logger.LogInformation($"检验明细更新内容,{JsonConvert.SerializeObject(selectTarget)}");
                    }
                }

                #endregion
            }
            else
            {
                #region 检查

                var selectCatalog = await (await _examCatalogRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.CatalogCode == item.SampleTypeId && x.FirstNodeCode == item.SuperiorCode);
                var selectProject = await (await _examProjectRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.ProjectCode == item.GroupId && x.CatalogCode == item.SampleTypeId);
                var selectTarget = await (await _examTargetRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.ProjectCode == item.GroupId && x.TargetCode == item.GroupsId);


                //1.检查目录


                //判断目录是否存在
                if (selectCatalog == null)
                {
                    //1.1 新增目录
                    var catalog = new ExamCatalog();
                    catalog.FirstNodeCode = item.SuperiorCode;
                    catalog.FirstNodeName = item.SuperiorName;
                    catalog.CatalogCode = item.SampleTypeId;
                    catalog.CatalogName = item.SampleType;
                    catalog.DisplayName = item.SampleType;
                    catalog.PyCode = (item.SuperiorName + item.SampleType).FirstLetterPY();
                    catalog.WbCode = (item.SuperiorName + item.SampleType).FirstLetterWB();
                    catalog.Sort = 1;
                    await _examCatalogRepository.InsertAsync(catalog);
                    _logger.LogInformation($"{DateTime.Now}检查目录新增内容,{JsonConvert.SerializeObject(catalog)}");
                }
                else
                {
                    //1.2 修改目录
                    if (item.SuperiorName != selectCatalog.FirstNodeName ||
                        item.SampleType != selectCatalog.CatalogName)
                    {
                        selectCatalog.CatalogName = item.SampleType;
                        selectCatalog.FirstNodeName = item.SuperiorName;
                        await _examCatalogRepository.UpdateAsync(selectCatalog);
                        _logger.LogInformation(
                            $"{DateTime.Now}检查目录更新内容,{JsonConvert.SerializeObject(selectCatalog)}");
                    }
                }


                //2.检查项目
                if (selectProject == null)
                {
                    //2.1 新增检查项目
                    var examProject = new ExamProject();
                    examProject.CatalogCode = item?.SampleTypeId;
                    examProject.CatalogName = item?.SampleType;
                    examProject.ProjectCode = item.GroupId;
                    examProject.ProjectName = item.GroupName;
                    examProject.ExecDeptCode = item?.DepartmentCode;
                    examProject.ExecDeptName = item?.DepartmentName;
                    examProject.DepExecutionType = item?.DepExecutionType;
                    examProject.DepExecutionRules = item?.DepExecutionRules;
                    examProject.Unit = item?.Unit;
                    examProject.PyCode = item?.SpellCode;
                    examProject.WbCode = item.GroupName.FirstLetterWB();
                    examProject.AddCard = item?.AddCard;
                    examProject.GuideCode = item?.GuideCode;
                    examProject.Price = Convert.ToDecimal(item?.Price);
                    await _examProjectRepository.InsertAsync(examProject);
                    _logger.LogInformation($"{DateTime.Now}检查项目新增内容,{JsonConvert.SerializeObject(examProject)}");
                }
                else
                {
                    if (item.UseFlag != (selectProject.IsActive == true ? "0" : "1")) //根据删除标志
                    {
                        //2.3 删除检查项目
                        selectProject.IsActive = item.UseFlag == "0" ? true:false ;
                        await _examProjectRepository.UpdateAsync(selectProject);
                        _logger.LogInformation(
                            $"{DateTime.Now}检查项目删除内容,{JsonConvert.SerializeObject(selectProject)}");
                    }
                    else
                    {
                        //比对字段是否相同,不相同则更新
                        if (item.SampleType != selectProject.CatalogName
                            || item.GroupName != selectProject.ProjectName
                            || item.DepartmentCode != selectProject.ExecDeptCode
                            || item.DepartmentName != selectProject.ExecDeptName
                            || item.DepExecutionType != selectProject.DepExecutionType
                            || item.DepExecutionRules != selectProject.DepExecutionRules
                            || item.Unit != selectProject.Unit
                            || item.Price!= Convert.ToDouble(selectProject.Price)
                            || item.AddCard != selectProject.AddCard|| selectProject.GuideCode != item?.GuideCode)
                        {
                            //2.2 更新检查项目
                            selectProject.CatalogName = item?.SampleType;
                            selectProject.ProjectName = item?.GroupName;
                            selectProject.ExecDeptCode = item?.DepartmentCode;
                            selectProject.ExecDeptName = item?.DepartmentName;
                            selectProject.DepExecutionType = item?.DepExecutionType;
                            selectProject.DepExecutionRules = item?.DepExecutionRules;
                            selectProject.Unit = item?.Unit;
                            selectProject.Price = Convert.ToDecimal(item.Price);
                            selectProject.PyCode = item?.SpellCode;
                            selectProject.AddCard = item?.AddCard;
                            selectProject.GuideCode = item?.GuideCode;
                            await _examProjectRepository.UpdateAsync(selectProject);
                            _logger.LogInformation(
                                $"{DateTime.Now}检查项目更新内容,{JsonConvert.SerializeObject(selectProject)}");
                        }
                    }
                }


                // 3.检查明细
                if (selectTarget == null)
                {
                    //3.1新增明细项目
                    var examTarget = new ExamTarget();
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
                    await _examTargetRepository.InsertAsync(examTarget);
                    _logger.LogInformation($"{DateTime.Now}检查明细新增内容,{JsonConvert.SerializeObject(examTarget)}");
                }
                else
                {
                    //3.2 更新明细项目
                    if (selectTarget.TargetName != item.GroupsName
                        || selectTarget.Price != decimal.Parse(item.Price.ToString("F2"))
                        || selectTarget.TargetUnit != item.Unit
                        || selectTarget.Qty != decimal.Parse(item.TotalNumber)
                        || selectTarget.ProjectType != item.ProjectType
                        || selectTarget.ProjectMerge != item.ProjectMerge)
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
                        await _examTargetRepository.UpdateAsync(selectTarget);
                        _logger.LogInformation(
                            $"{DateTime.Now}检查明细更新内容,{JsonConvert.SerializeObject(selectTarget)}");
                    }
                }

                #endregion
            }

            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            _capPublisher.Publish("errorMessage", item);
            await uow.RollbackAsync();
            throw;
        }
    }
}