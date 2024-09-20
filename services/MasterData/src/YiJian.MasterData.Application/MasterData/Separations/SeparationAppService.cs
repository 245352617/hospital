using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.MasterData.Separations.Dto;
using YiJian.MasterData.Medicines;
using YiJian.MasterData.Separations.Contracts;
using YiJian.MasterData.Separations.Entities;

namespace YiJian.MasterData.MasterData.Separations;

/// <summary>
/// 分方配置管理
/// </summary>
[Authorize]
public class SeparationAppService : MasterDataAppService, ISeparationAppService
{
    private readonly IMedicineUsageRepository _medicineUsageRepository;
    private readonly ISeparationRepository _separationRepository;
    private readonly IUsageRepository _usageRepository;
    private readonly IMemoryCache _cache;

    private const string USAGES_ALL_KEY = "separation:usages:all";
    private const string LIST_ALL_KEY = "separation:list:all";

    public SeparationAppService(
        IMedicineUsageRepository medicineUsageRepository,
        ISeparationRepository separationRepository,
        IUsageRepository usageRepository,
        IMemoryCache cache)
    {
        _medicineUsageRepository = medicineUsageRepository;
        _separationRepository = separationRepository;
        _usageRepository = usageRepository;
        _cache = cache;
    }

    /// <summary>
    /// 获取途径的所有分类
    /// </summary>
    /// <returns></returns> 
    public async Task<List<UsageSelectDto>> GetUsagesAsync()
    { 
        var cacheData = _cache.Get<List<UsageSelectDto>>(USAGES_ALL_KEY);
        if (cacheData != null) return cacheData;
        var query = from u in (await _medicineUsageRepository.GetQueryableAsync())
                    group u by u.UsageCode into g
                    select new UsageSelectDto {
                        UsageCode = g.Key, 
                        UsageName = g.Max(s => s.UsageName) 
                    };
        var data = await query.ToListAsync();
        var retData = _cache.Set<List<UsageSelectDto>>(USAGES_ALL_KEY, data,absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10)); 
        return retData;
    }

    /// <summary>
    /// 获取分方配置列表
    /// </summary>
    /// <returns></returns> 
    public async Task<List<SeparationDto>> GetListAsync()
    { 
        var cacheData = _cache.Get<List<SeparationDto>>(LIST_ALL_KEY);
        if (cacheData != null) return cacheData;
        var data = (await _separationRepository.GetListAsync()).OrderBy(o => o.Sort).ToList();
        var map = ObjectMapper.Map<List<Separation>, List<SeparationDto>>(data);
        var retData = _cache.Set<List<SeparationDto>>(LIST_ALL_KEY, map, absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10));
        return map;
    }
     
    /// <summary>
    /// 更新分方操作
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<Guid> ModifyAsync(ModifySeparationDto model)
    { 
        var entity = (await _separationRepository.GetQueryableAsync()).FirstOrDefault(w => w.Id == model.Id);
        if (entity == null) Oh.Error("找不到需要更新的分方配置对象");
        var usages = await (await _usageRepository.GetQueryableAsync()).Where(w => w.SeparationId == entity.Id).ToListAsync();
        if (usages.Any()) await _usageRepository.DeleteManyAsync(usages);
        var newUsages = model.Usages.Select(s => new { s.UsageCode, s.UsageName }).Distinct().ToList();
        var items = new List<Usage>();
        newUsages.ForEach(x => items.Add(new Usage(GuidGenerator.Create(), x.UsageCode, x.UsageName, entity.Id)));
        if (items.Any()) await _usageRepository.InsertManyAsync(items);
        entity.Update(model.Code,model.Title, model.PrintSettingId,model.PrintSettingName);//目前符合业务，分类是初始化录入的，以后如果有变动需要添加一个添加的功能
        _cache.Remove(LIST_ALL_KEY);
        return entity.Id;
    }

}
