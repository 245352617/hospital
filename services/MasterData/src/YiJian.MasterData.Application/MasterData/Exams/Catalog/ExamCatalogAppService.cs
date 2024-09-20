using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Domain;
using YiJian.MasterData.Exams;

namespace YiJian.MasterData;


/// <summary>
/// 检查目录 API
/// </summary>
[Authorize]
public class ExamCatalogAppService : MasterDataAppService, IExamCatalogAppService
{
    private readonly IExamCatalogRepository _examCatalogRepository;
    private readonly IExamProjectRepository _examProjectRepository;
    private readonly IMemoryCache _cache;
    private readonly IExamNoteRepository _examNoteRepository;
    private readonly IExecuteDepRuleDicRepository _executeDepRuleDicRepository;

    private const string Categary_ALL_KEY = "exam:category:list:all";
    private const string Project_ALL_KEY = "exam:project:list:all";
    private const string EXECUTE_DEP_RULE_DIC_KEY = "Exam:ExecuteDepRuleDic:All";

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary> 
    public ExamCatalogAppService(IExamCatalogRepository examCatalogRepository,
        IExamProjectRepository examProjectRepository, IMemoryCache cache, IExamNoteRepository examNoteRepository,
        IExecuteDepRuleDicRepository executeDepRuleDicRepository)
    {
        _examCatalogRepository = examCatalogRepository;
        _examProjectRepository = examProjectRepository;
        _cache = cache;
        _examNoteRepository = examNoteRepository;
        _executeDepRuleDicRepository = executeDepRuleDicRepository;
    }

    #endregion constructor

    #region ClearCache

    /// <summary>
    /// 清除缓存
    /// </summary>
    /// <returns></returns>
    public bool GetClearnChache()
    {
        _cache.Remove(Categary_ALL_KEY);
        _cache.Remove(Project_ALL_KEY);
        _cache.Remove(EXECUTE_DEP_RULE_DIC_KEY);
        return true;
    }

    #endregion

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamCatalogs.Create)]
    public async Task<int> CreateAsync(ExamCatalogCreation input)
    {
        if (await _examCatalogRepository.AnyAsync(a => a.CatalogCode == input.CatalogCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<ExamCatalogCreation, ExamCatalog>(input);
        var result = await _examCatalogRepository.InsertAsync(model);
        _cache.Remove(Categary_ALL_KEY);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamCatalogs.Update)]
    public async Task UpdateAsync(ExamCatalogUpdate input)
    {
        var examCatalog = await _examCatalogRepository.GetAsync(input.Id);

        if (examCatalog == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        examCatalog.Modify(name: input.CatalogName, // 名称
            displayName: input.DisplayName, // 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
            deptCode: input.DeptCode, // 科室编码
            deptName: input.DeptName, // 科室名称
            sort: input.Sort, // 排序号
            roomCode: input.RoomCode, // 执行机房编码
            room: input.RoomName, // 执行机房
            isActive: input.IsActive // 是否启用
        );
        await _examCatalogRepository.UpdateAsync(examCatalog);
        _cache.Remove(Categary_ALL_KEY);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ExamCatalogData> GetAsync(int id)
    {
        var examCatalog = await _examCatalogRepository.GetAsync(id);
        if (examCatalog == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        return ObjectMapper.Map<ExamCatalog, ExamCatalogData>(examCatalog);
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
        if (!await _examCatalogRepository.AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _examCatalogRepository.DeleteAsync(id);
        _cache.Remove(Categary_ALL_KEY);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamCatalogs.Default)]
    public async Task<ListResultDto<ExamCatalogData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {


        var cacheData = _cache.Get<List<ExamCatalogData>>(Categary_ALL_KEY);
        var categoryList = cacheData;
        if (cacheData == null)
        {
            var list = await _examCatalogRepository.GetListAsync("", sorting);
            categoryList =
                ObjectMapper.Map<List<ExamCatalog>, List<ExamCatalogData>>(list);
            _cache.Set<List<ExamCatalogData>>(Categary_ALL_KEY, categoryList,
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10));
        }

        var cacheProjectData = _cache.Get<List<ExamProjectData>>(Project_ALL_KEY);
        if (cacheProjectData == null)
        {
            var projectList = await (await _examProjectRepository.GetQueryableAsync()).Where(x => x.IsActive).ToListAsync();
            cacheProjectData = ObjectMapper.Map<List<ExamProject>, List<ExamProjectData>>(projectList);
            _cache.Set<List<ExamProjectData>>(Project_ALL_KEY, cacheProjectData,
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10));
        }
        var category = categoryList.WhereIf(!filter.IsNullOrWhiteSpace(),
            e =>
                e.CatalogCode.Contains(filter)).ToList();
        foreach (var item in category)
        {
            item.ExamProject = cacheProjectData.FindAll(x => x.CatalogCode == item.CatalogCode);
            item.ExamProject.ForEach(f =>
            {
                f.FirstCatalogCode = item.FirstNodeCode;
                f.FirstCatalogName = item.FirstNodeName;
            });
        }

        return new ListResultDto<ExamCatalogData>(category);
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamCatalogs.Default)]
    public async Task<ListResultDto<ExamCatalogFirstNodeData>> GetListV2Async(
        string filter = null,
        string sorting = null)
    {
        var cacheData = _cache.Get<List<ExamCatalogData>>(Categary_ALL_KEY);
        if (cacheData == null)
        {
            //查询全部写入缓存
            var list = await _examCatalogRepository.GetListAsync("", sorting);
            cacheData =
                ObjectMapper.Map<List<ExamCatalog>, List<ExamCatalogData>>(list);
            _cache.Set<List<ExamCatalogData>>(Categary_ALL_KEY, cacheData, TimeSpan.FromMinutes(10));
        }

        var cacheProjectData = _cache.Get<List<ExamProjectData>>(Project_ALL_KEY);
        if (cacheProjectData == null)
        {
            var projectList = await (await _examProjectRepository.GetQueryableAsync()).Where(x => x.IsActive).ToListAsync();
            cacheProjectData = ObjectMapper.Map<List<ExamProject>, List<ExamProjectData>>(projectList);
            _cache.Set<List<ExamProjectData>>(Project_ALL_KEY, cacheProjectData, TimeSpan.FromMinutes(10));
        }

        // 一级目录
        var firstNodes = cacheData
            .GroupBy(x => new { x.FirstNodeCode, x.FirstNodeName })
            .Select(x => new ExamCatalogFirstNodeData
            {
                CatalogCode = x.Key.FirstNodeCode,
                CatalogName = x.Key.FirstNodeName
            }).ToList();
        var note = await _examNoteRepository.GetListAsync();
        foreach (var firstNode in firstNodes)
        {
            // 二级目录
            var children = cacheData.Where(w => w.FirstNodeCode == firstNode.CatalogCode && w.IsActive).ToList();
            firstNode.Children = ObjectMapper.Map<List<ExamCatalogData>, List<ExamCatalogDataV2>>(children);
            foreach (var item in firstNode.Children)
            {
                item.Children = cacheProjectData.FindAll(x => x.CatalogCode == item.CatalogCode);
                item.Children.ForEach(f =>
                {
                    f.FirstCatalogCode = firstNode.CatalogCode;
                    f.FirstCatalogName = firstNode.CatalogName;
                    var guide = note.FirstOrDefault(ff => ff.NoteCode == f.GuideCode);
                    f.GuideName = guide?.NoteName;
                });

            }
        }

        return new ListResultDto<ExamCatalogFirstNodeData>(firstNodes);
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.ExamCatalogs.Default)]
    public async Task<ListResultDto<ExamCatalogFirstNodeData>> GetListV3Async(
        string filter = null,
        string sorting = null)
    {
        var cacheData = _cache.Get<List<ExamCatalogData>>(Categary_ALL_KEY);
        if (cacheData == null)
        {
            //查询全部写入缓存
            var list = await _examCatalogRepository.GetListAsync("", sorting);
            cacheData =
                ObjectMapper.Map<List<ExamCatalog>, List<ExamCatalogData>>(list);
            _cache.Set(Categary_ALL_KEY, cacheData, TimeSpan.FromHours(CacheKey.EXAMCATALOGTIME));
        }

        var cacheProjectData = _cache.Get<List<ExamProjectData>>(Project_ALL_KEY);
        if (cacheProjectData == null)
        {
            var projectList = await _examProjectRepository.GetListAsync(x => x.IsActive);
            cacheProjectData = ObjectMapper.Map<List<ExamProject>, List<ExamProjectData>>(projectList);
            _cache.Set(Project_ALL_KEY, cacheProjectData, TimeSpan.FromHours(CacheKey.EXAMCATALOGTIME));
        }

        // 一级目录
        var firstNodes = cacheData
            .GroupBy(x => new { x.FirstNodeCode, x.FirstNodeName })
            .Select(x => new ExamCatalogFirstNodeData
            {
                CatalogCode = x.Key.FirstNodeCode,
                CatalogName = x.Key.FirstNodeName
            }).ToList();
        //var note = await _examNoteRepository.GetListAsync();
        foreach (var firstNode in firstNodes)
        {
            // 二级目录
            var children = cacheData.Where(w => w.FirstNodeCode == firstNode.CatalogCode && w.IsActive).ToList();
            firstNode.Children = ObjectMapper.Map<List<ExamCatalogData>, List<ExamCatalogDataV2>>(children);
            foreach (var item in firstNode.Children)
            {
                item.Children = cacheProjectData.FindAll(x => x.CatalogCode == item.CatalogCode && x.FirstCatalogCode == firstNode.CatalogCode);
                item.Children.ForEach(f =>
                {
                    f.FirstCatalogCode = firstNode.CatalogCode;
                    f.FirstCatalogName = firstNode.CatalogName;
                    //var guide = note.FirstOrDefault(ff => ff.NoteCode == f.GuideCode);
                    //f.GuideName = guide?.NoteName;
                });

            }
        }

        return new ListResultDto<ExamCatalogFirstNodeData>(firstNodes);
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<ExamCatalogData>> GetPagedListAsync(GetExamCatalogPagedInput input)
    {
        var examCatalogs = await _examCatalogRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<ExamCatalog>, List<ExamCatalogData>>(examCatalogs);

        var totalCount = await _examCatalogRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<ExamCatalogData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}