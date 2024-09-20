using DotNetCore.CAP;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Position;

namespace YiJian.MasterData;

/// <summary>
/// 检验项目 API
/// </summary>
[Authorize]
public class LabProjectAppService : MasterDataAppService, ILabProjectAppService
{
    private readonly ILabProjectRepository _labProjectRepository;
    private readonly IAllItemByThreePartyAppService _allItemAppService;
    private readonly ICapPublisher _capPublisher;
    private readonly ILabSpecimenPositionRepository _labSpecimenPositionRepository;
    private readonly ILabReportInfoRepository _labReportInfoRepository;


    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="labProjectRepository"></param>
    /// <param name="allItemAppService"></param>
    /// <param name="capPublisher"></param>
    /// <param name="labSpecimenPositionRepository"></param>
    /// <param name="labReportInfoRepository"></param>
    public LabProjectAppService(ILabProjectRepository labProjectRepository,
        IAllItemByThreePartyAppService allItemAppService,
        ICapPublisher capPublisher, ILabSpecimenPositionRepository labSpecimenPositionRepository, ILabReportInfoRepository labReportInfoRepository)
    {
        _labProjectRepository = labProjectRepository;
        _allItemAppService = allItemAppService;
        this._capPublisher = capPublisher;
        _labSpecimenPositionRepository = labSpecimenPositionRepository;
        _labReportInfoRepository = labReportInfoRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(LabProjectCreation input)
    {
        if (await _labProjectRepository.AnyAsync(a => a.ProjectCode == input.ProjectCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<LabProjectCreation, LabProject>(input);
        model.PyCode = input.ProjectName.FirstLetterPY();
        var result = await _labProjectRepository.InsertAsync(model);
        if (result.Id > 0 && result.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.SaveAsync(new AllItemCreation
            {
                Code = input.ProjectCode,
                Name = input.ProjectName,
                CategoryCode = "Lab",
                CategoryName = "检验",
                Price = input.Price,
                Unit = input.Unit,
                TypeCode = "PreHospital",
                TypeName = "院前急救"
            });
        }

        var eto = ObjectMapper.Map<LabProject, GrpcLabProjectModel>(result);
        await this._capPublisher.PublishAsync("sync.masterdata.labproject", eto);

        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> UpdateAsync(LabProjectUpdate input)
    {
        var labProject = await _labProjectRepository.GetAsync(input.Id);
        if (labProject == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var oldAid = labProject.PlatformType;
        labProject.Modify(name: input.Name, // 名称
            catalogCode: input.CatalogCode, // 检验分类编码
            catalog: input.CatalogName, // 检验分类
            specimenCode: input.SpecimenCode, // 标本编码
            specimen: input.SpecimenName, // 标本
            deptCode: input.ExecDeptCode, // 科室编码
            dept: input.ExecDeptName, // 科室
            positionCode: input.SpecimenPartCode, // 位置编码
            position: input.SpecimenPartName, // 位置
            indexNo: input.Sort, // 排序号
            unit: input.Unit, // 单位
                              //price: input.Price, // 价格
            otherPrice: input.OtherPrice, // 价格
            isActive: input.IsActive, // 是否启用
            containerCode: input.ContainerCode, containerName: input.ContainerName, platformType: input.PlatformType
        );
        var result = await _labProjectRepository.UpdateAsync(labProject);
        if (result != null)
        {
            if (input.PlatformType == PlatformType.PreHospital)
            {
                await _allItemAppService.SaveAsync(new AllItemCreation
                {
                    Code = labProject.ProjectCode,
                    Name = labProject.ProjectName,
                    CategoryCode = "Lab",
                    CategoryName = "检验",
                    Price = input.Price,
                    Unit = input.Unit,
                    TypeCode = "PreHospital",
                    TypeName = "院前急救"
                });
            }
            else if (oldAid == PlatformType.PreHospital && input.PlatformType != PlatformType.PreHospital)
            {
                await _allItemAppService.DeleteAsync(labProject.ProjectCode, "Lab", "PreHospital");
            }

            var eto = ObjectMapper.Map<LabProject, GrpcLabProjectModel>(result);
            await this._capPublisher.PublishAsync("sync.masterdata.labproject", eto);

            return result.Id;
        }

        return -1;
    }

    /// <summary>
    /// 更新检查数据只能修改PrescribeCode，TreatCode
    /// </summary>
    /// <param name="labProjectUpdateDto"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<bool> UpdateLabProjectAsync(LabProjectUpdateDto labProjectUpdateDto)
    {
        if (labProjectUpdateDto == null || labProjectUpdateDto.Ids == null)
        {
            throw new BusinessException(message: "请求参数为空");
        }


        List<LabProject> labProjects = await _labProjectRepository.GetListAsync(x => labProjectUpdateDto.Ids.Contains(x.Id));
        if (labProjects == null)
        {
            throw new BusinessException(message: "没有找到要更新的数据");
        }

        foreach (LabProject labProject in labProjects)
        {
            labProject.PrescribeCode = labProjectUpdateDto.PrescribeCode;
            labProject.PrescribeName = labProjectUpdateDto.PrescribeName;
            labProject.TreatCode = labProjectUpdateDto.TreatCode;
            labProject.TreatName = labProjectUpdateDto.TreatName;
        }

        await _labProjectRepository.UpdateManyAsync(labProjects);
        return true;
    }
    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //[Authorize(MasterDataPermissions.LabProjects.Default)]
    public async Task<LabProjectData> GetAsync(int id)
    {
        var labProject = await _labProjectRepository.GetAsync(id);

        return ObjectMapper.Map<LabProject, LabProjectData>(labProject);
    }

    #endregion Get

    #region Details

    /// <summary>
    /// 根据编码获取
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    public async Task<LabProjectData> GetDetailsAsync(string code, PlatformType type = PlatformType.EmergencyTreatment)
    {
        var labProject = await _labProjectRepository.FirstOrDefaultAsync(f => f.ProjectCode == code);
        if (labProject == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var result = ObjectMapper.Map<LabProject, LabProjectData>(labProject);
        int projectCode = 0;
        if (int.TryParse(labProject.ProjectCode, out projectCode))
        {
            if (projectCode > 0)
            {
                LabReportInfo labReportInfo = await _labReportInfoRepository.FirstOrDefaultAsync(c => c.Code == projectCode);
                if (string.IsNullOrEmpty(result.SpecimenName))
                {
                    result.SpecimenName = labReportInfo?.SampleCollectType;
                }
                if (string.IsNullOrEmpty(result.GuideName))
                {
                    result.GuideName = labReportInfo?.Remark;
                }
                if (string.IsNullOrEmpty(result.ExecDeptName))
                {
                    result.ExecDeptName = labReportInfo?.ExecDeptName;
                }
                if (string.IsNullOrEmpty(result.ContainerName))
                {
                    result.ContainerName = labReportInfo?.TestTubeName;
                }
                if (string.IsNullOrEmpty(result.GuideCatelogName))
                {
                    result.GuideCatelogName = labReportInfo?.CatelogName;
                }
            }
        }
        if (type == PlatformType.PreHospital)
        {
            var labTargetService = LazyServiceProvider.LazyGetRequiredService<ILabTargetAppService>();
            var targetResult = await labTargetService.GetListAsync(labProject.CatalogAndProjectCode, code);
            result.Items = targetResult.Items.ToList();

            //院前CategoryCode和字典PreHospitalCategory保持一致
            result.CategoryCode = "Laboratory";
            result.CategoryName = "检验";
        }

        return result;
    }

    #endregion

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        var labProject = await _labProjectRepository.GetAsync(id);
        if (labProject == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _labProjectRepository.DeleteAsync(id);
        if (labProject.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.DeleteAsync(labProject.ProjectCode, "Lab", "PreHospital");
        }
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<LabProjectData>> GetListAsync(string cateCode,
        string filter = null, PlatformType platformType = PlatformType.All)
    {
        var result = await _labProjectRepository.GetListAsync(cateCode, filter, platformType);

        var map = ObjectMapper.Map<List<LabProject>, List<LabProjectData>>(result.Where(w => w.IsActive).ToList());
        return new ListResultDto<LabProjectData>(map);
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<LabProjectData>> GetPagedListAsync(GetLabProjectPagedInput input)
    {
        var labProjects = await _labProjectRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting, input.PlatformType);

        var items = ObjectMapper.Map<List<LabProject>, List<LabProjectData>>(labProjects);
        var totalCount = await _labProjectRepository.GetCountAsync(input.Filter, input.PlatformType);
        var result = new PagedResultDto<LabProjectData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}