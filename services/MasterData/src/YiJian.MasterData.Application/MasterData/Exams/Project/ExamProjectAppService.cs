using DotNetCore.CAP;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.AllItems;
using YiJian.MasterData.Exams;

namespace YiJian.MasterData;

/// <summary>
/// 检查申请项目 API
/// </summary>
[Authorize]
public class ExamProjectAppService : MasterDataAppService, IExamProjectAppService
{
    private readonly IExamProjectRepository _examProjectRepository;
    private readonly IAllItemByThreePartyAppService _allItemAppService;
    private readonly ICapPublisher _capPublisher;
    private readonly IMemoryCache _cache;
    private readonly List<ExamExecDeptConfig> _examExecDeptConfigs;
    private const string Project_ALL_KEY = "exam:project:list:all";

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="examProjectRepository"></param>
    /// <param name="allItemAppService"></param>
    /// <param name="capPublisher"></param>
    /// <param name="cache"></param>
    /// <param name="optionsMonitor"></param>
    public ExamProjectAppService(IExamProjectRepository examProjectRepository,
        IAllItemByThreePartyAppService allItemAppService,
        ICapPublisher capPublisher,
        IMemoryCache cache,
        IOptions<List<ExamExecDeptConfig>> optionsMonitor)
    {
        _examProjectRepository = examProjectRepository;
        _allItemAppService = allItemAppService;
        this._capPublisher = capPublisher;
        _cache = cache;
        _examExecDeptConfigs = optionsMonitor.Value;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(ExamProjectCreation input)
    {
        var model = ObjectMapper.Map<ExamProjectCreation, ExamProject>(input);
        model.PyCode = model.ProjectName.FirstLetterPY();
        var project = await _examProjectRepository.InsertAsync(model);
        if (project.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.SaveAsync(new AllItemCreation
            {
                Code = input.ProjectCode,
                Name = input.ProjectName,
                CategoryCode = "Examine",
                CategoryName = "检查",
                Price = input.Price,
                Unit = input.Unit,
                TypeCode = "PreHospital",
                TypeName = "院前急救"
            });
        }

        var eto = ObjectMapper.Map<ExamProject, GrpcExamProjectModel>(project);
        await this._capPublisher.PublishAsync("sync.masterdata.examproject", eto);
        _cache.Remove(Project_ALL_KEY);
        return project.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamProjects.Update)]
    public async Task<int> UpdateAsync(ExamProjectUpdate input)
    {
        var examProject = await _examProjectRepository.GetAsync(input.Id);

        if (examProject == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var oldAid = examProject.PlatformType;
        examProject.Modify(name: input.Name, // 名称
            catalogCode: input.CatalogCode, // 分类编码
            catalog: input.CatalogName, // 分类名称
            indexNo: input.Sort, // 排序号
            examPartCode: input.PartCode, // 检查部位
            examPartName: input.PartName, // 检查部位
            unit: input.Unit, // 单位
            price: input.Price, // 价格
            deptCode: input.ExecDeptCode, // 科室编码
            deptName: input.ExecDeptName, // 科室名称
            roomCode: input.RoomCode, // 执行机房编码
            room: input.RoomName, // 执行机房描述
            isActive: input.IsActive, // 是否启用
            platformType: input.PlatformType
        );
        var project = await _examProjectRepository.UpdateAsync(examProject);
        if (_cache != null) _cache.Remove(Project_ALL_KEY);
        if (input.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.SaveAsync(new AllItemCreation
            {
                Code = project.ProjectCode,
                Name = project.ProjectName,
                CategoryCode = "Examine",
                CategoryName = "检查",
                Price = input.Price,
                Unit = input.Unit,
                TypeCode = "PreHospital",
                TypeName = "院前急救"
            });
        }
        else if (oldAid == PlatformType.PreHospital && input.PlatformType != PlatformType.PreHospital)
        {
            await _allItemAppService.DeleteAsync(project.ProjectCode, "Examine", "PreHospital");
        }

        var eto = ObjectMapper.Map<ExamProject, GrpcExamProjectModel>(project);
        await this._capPublisher.PublishAsync("sync.masterdata.examproject", eto);

        return project.Id;
    }

    /// <summary>
    /// 更新检查数据只能修改ReservationPlace，ReservationTime，Note，TemplateId
    /// </summary>
    /// <param name="examProjectUpdateDto"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<bool> UpdateExamProjectAsync(ExamProjectUpdateDto examProjectUpdateDto)
    {
        if (examProjectUpdateDto == null || examProjectUpdateDto.Ids == null)
        {
            throw new BusinessException(message: "请求参数为空");
        }


        List<ExamProject> examProjects = await _examProjectRepository.GetListAsync(x => examProjectUpdateDto.Ids.Contains(x.Id));
        if (examProjects == null)
        {
            throw new BusinessException(message: "没有找到要更新的数据");
        }

        foreach (ExamProject examProject in examProjects)
        {
            examProject.ReservationPlace = examProjectUpdateDto.ReservationPlace;
            examProject.ReservationTime = examProjectUpdateDto.ReservationTime;
            examProject.Note = examProjectUpdateDto.Note;
            examProject.TemplateId = examProjectUpdateDto.TemplateId;
            examProject.PrescribeCode = examProjectUpdateDto.PrescribeCode;
            examProject.PrescribeName = examProjectUpdateDto.PrescribeName;
            examProject.TreatCode = examProjectUpdateDto.TreatCode;
            examProject.TreatName = examProjectUpdateDto.TreatName;
        }

        await _examProjectRepository.UpdateManyAsync(examProjects);
        return true;
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamProjects.Default)]
    public async Task<ExamProjectData> GetAsync(int id)
    {
        var examProject = await _examProjectRepository.GetAsync(id);

        return ObjectMapper.Map<ExamProject, ExamProjectData>(examProject);
    }

    #endregion Get

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamProjects.Delete)]
    public async Task DeleteAsync(int id)
    {
        var examProject = await _examProjectRepository.GetAsync(id);
        if (examProject == null)
        {
            throw new BusinessException(message: "数据不存在");
        }

        await _examProjectRepository.DeleteAsync(id);
        if (_cache != null) _cache.Remove(Project_ALL_KEY);
        if (examProject.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.DeleteAsync(examProject.ProjectCode, "Examine", "PreHospital");
        }
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<ExamProjectData>> GetListAsync(
        string cateCode, string filter = null, PlatformType platformType = PlatformType.All)
    {
        var cacheData = _cache.Get<List<ExamProjectData>>(Project_ALL_KEY);
        if (cacheData != null)
        {
            cacheData = cacheData.WhereIf(
                    platformType != PlatformType.All,
                    e =>
                        e.PlatformType == platformType)
                .WhereIf(
                    !cateCode.IsNullOrWhiteSpace(),
                    e =>
                        e.CatalogCode == cateCode)
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    e =>
                        e.ProjectName.Contains(filter) ||
                        e.PyCode.Contains(filter))
                .ToList();
            return new ListResultDto<ExamProjectData>(cacheData.Where(w => w.IsActive).ToList());
        }

        var list = await _examProjectRepository.GetListAsync(cateCode, filter, platformType);
        var mapList = ObjectMapper.Map<List<ExamProject>, List<ExamProjectData>>(list);
        return new ListResultDto<ExamProjectData>(mapList);
    }

    #endregion GetList

    #region Details

    /// <summary>
    /// 根据编码获取
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<ExamProjectData> GetDetailsAsync(string code, PlatformType type = PlatformType.EmergencyTreatment)
    {
        var examProject = await _examProjectRepository.FirstOrDefaultAsync(f => f.ProjectCode == code);
        if (examProject == null)
        {
            throw new BusinessException(message: "数据不存在");
        }

        var result = ObjectMapper.Map<ExamProject, ExamProjectData>(examProject);
        UpdateExecDept(new List<ExamProjectData>() { result });

        if (type == PlatformType.PreHospital) //院前特殊处理
        {
            var examTargetService = LazyServiceProvider.LazyGetRequiredService<IExamTargetAppService>();
            var targetResult = await examTargetService.GetListAsync(code);
            result.Items = targetResult.Items.ToList();

            //院前CategoryCode和字典PreHospitalCategory保持一致
            result.CategoryCode = "Examine";
            result.CategoryName = "检查";
        }

        return result;
    }

    private void UpdateExecDept(IEnumerable<ExamProjectData> examProjectDatas)
    {
        foreach (var examProjectData in examProjectDatas)
        {
            ExamExecDeptConfig examExecDeptConfig = _examExecDeptConfigs.FirstOrDefault(x => x.ProjectCodes.Contains(examProjectData.Code));
            if (examExecDeptConfig == null) continue;

            examProjectData.ExecDeptCode = examExecDeptConfig.ExecDeptCode;
            examProjectData.ExecDeptName = examExecDeptConfig.ExecDeptName;
        }
    }

    #endregion

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<ExamProjectData>> GetPagedListAsync(GetExamProjectInput input)
    {
        var examProjects = await _examProjectRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter, input.PlatformType);

        var items = ObjectMapper.Map<List<ExamProject>, List<ExamProjectData>>(examProjects);
        UpdateExecDept(items);
        var totalCount = await _examProjectRepository.GetCountAsync(input.Filter, input.PlatformType);

        var result = new PagedResultDto<ExamProjectData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList

}