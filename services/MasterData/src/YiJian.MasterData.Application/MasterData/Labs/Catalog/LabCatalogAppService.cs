using DotNetCore.CAP.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验目录 API
/// </summary>
[Authorize]
public class LabCatalogAppService : MasterDataAppService, ILabCatalogAppService
{
    private readonly ILabCatalogRepository _labCatalogRepository;
    private readonly ILabProjectRepository _labProjectRepository;
    private readonly IExamNoteRepository _examNoteRepository;
    private readonly ILabReportInfoRepository _labReportInfoRepository;

    private readonly IMemoryCache _cache;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="labCatalogRepository"></param>
    /// <param name="labProjectRepository"></param>
    /// <param name="examNoteRepository"></param>
    /// <param name="labReportInfoRepository"></param>
    /// <param name="cache"></param>
    public LabCatalogAppService(ILabCatalogRepository labCatalogRepository
        , ILabProjectRepository labProjectRepository
        , IExamNoteRepository examNoteRepository
        , ILabReportInfoRepository labReportInfoRepository
        , IMemoryCache cache)
    {
        _labCatalogRepository = labCatalogRepository;
        _labProjectRepository = labProjectRepository;
        _examNoteRepository = examNoteRepository;
        _labReportInfoRepository = labReportInfoRepository;
        _cache = cache;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(LabCatalogCreation input)
    {
        if (await (await _labCatalogRepository.GetQueryableAsync()).AnyAsync(a => a.CatalogCode == input.CatalogCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<LabCatalogCreation, LabCatalog>(input);
        var result = await _labCatalogRepository.InsertAsync(model);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(LabCatalogUpdate input)
    {
        var labCatalog = await _labCatalogRepository.GetAsync(input.Id);
        if (labCatalog == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        labCatalog.Modify(name: input.CatalogName, // 分类编码
            deptCode: input.ExecDeptCode, // 科室编码
            dept: input.ExecDeptName, // 科室
            sort: input.Sort, // 排序号
            isActive: input.IsActive // 是否启用
        );
        await _labCatalogRepository.UpdateAsync(labCatalog);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LabCatalogData> GetAsync(int id)
    {
        var labCatalog = await _labCatalogRepository.GetAsync(id);

        return ObjectMapper.Map<LabCatalog, LabCatalogData>(labCatalog);
    }

    #endregion Get

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        if (!await (await _labCatalogRepository.GetQueryableAsync()).AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        await _labCatalogRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<LabCatalogData>> GetListAsync(string filter = null, string sorting = null)
    {
        var data = _cache.Get<ListResultDto<LabCatalogData>>(CacheKey.LABCATALOGKEY);
        if (data != null) return data;

        List<LabCatalog> list = await _labCatalogRepository.GetListAsync(filter, sorting);
        List<LabCatalogData> result = ObjectMapper.Map<List<LabCatalog>, List<LabCatalogData>>(list.Where(w => w.IsActive).ToList());
        List<LabProject> projectList = await _labProjectRepository.GetListAsync(x => x.IsActive);
        List<LabProjectData> projectMap = ObjectMapper.Map<List<LabProject>, List<LabProjectData>>(projectList);
        List<LabReportInfo> labReportInfos = await _labReportInfoRepository.GetListAsync();
        foreach (LabProjectData item in projectMap)
        {
            LabReportInfo labReportInfo = labReportInfos.FirstOrDefault(x => x.Code.ToString() == item.Code);
            if (string.IsNullOrEmpty(item.SpecimenName))
            {
                item.SpecimenName = labReportInfo?.SampleCollectType;
            }
            if (string.IsNullOrEmpty(item.GuideName))
            {
                item.GuideName = labReportInfo?.Remark;
            }
            if (string.IsNullOrEmpty(item.ExecDeptName))
            {
                item.ExecDeptName = labReportInfo?.ExecDeptName;
            }
            if (string.IsNullOrEmpty(item.ContainerName))
            {
                item.ContainerName = labReportInfo?.TestTubeName;
            }
            if (string.IsNullOrEmpty(item.GuideCatelogName))
            {
                item.GuideCatelogName = labReportInfo?.CatelogName;
            }
        }


        foreach (LabCatalogData item in result)
        {
            List<LabProjectData> pro = projectMap.Where(x => x.CatalogCode == item.CatalogCode).ToList();

            item.LabProjects = pro;
        }

        var returnData = new ListResultDto<LabCatalogData>(result);
        _cache.Set(CacheKey.LABCATALOGKEY, returnData, TimeSpan.FromHours(CacheKey.LABCATALOGTIME)); //暂时缓存6小时

        return returnData;
    }

    /// <summary>
    /// 获取列表(龙岗中心医院检验列表)
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<ExamCatalogFirstNodeData>> GetListV2Async(string filter = null, string sorting = null)
    {
        await Task.CompletedTask;
        throw new AggregateException();
    }


    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<LabCatalogData>> GetPagedListAsync(GetLabCatalogPagedInput input)
    {
        var labCatalogs = await _labCatalogRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<LabCatalog>, List<LabCatalogData>>(labCatalogs);

        var totalCount = await _labCatalogRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<LabCatalogData>(totalCount, items.AsReadOnly());

        return result;
    }




    #endregion GetPagedList
}