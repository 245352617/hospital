using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Dosages.Dto;
using YiJian.Recipe;
using YiJian.Recipes.DoctorsAdvices.Contracts;

namespace YiJian.Dosages;

/// <summary>
/// 剂量配置表，处理HIS药品剂量换算配置错误的问题
/// </summary>
[Authorize]
public class DosageAppService : RecipeAppService, IDosageAppService
{
    private readonly IPrescribeCustomRuleRepository _prescribeCustomRuleRepository;
    private readonly ILogger<DosageAppService> _logger;
    private readonly IMemoryCache _cache;

    private const string CUSTOM_DOSAGE_KEY = "DOSAGE_APP_SERVICE::CUSTOM_DOSAGE_KEY";

    /// <summary>
    /// 剂量配置表，处理HIS药品剂量换算配置错误的问题
    /// </summary>
    public DosageAppService(
        IPrescribeCustomRuleRepository prescribeCustomRuleRepository,
        ILogger<DosageAppService> logger,
        IMemoryCache cache)
    {

        _prescribeCustomRuleRepository = prescribeCustomRuleRepository;
        _logger = logger;
        _cache = cache;
    }

    /// <summary>
    /// 获取所有的自定义一次剂量表数据
    /// </summary>
    /// <returns></returns>
    public async Task<List<CustomDosageDto>> GetAllCustomDosagesAsync()
    {
        var data = _cache.Get<List<CustomDosageDto>>(CUSTOM_DOSAGE_KEY);

        if (data == null || !data.Any())
        {
            var list = await _prescribeCustomRuleRepository.GetAllListAsync();
            var map = ObjectMapper.Map<List<PrescribeCustomRule>, List<CustomDosageDto>>(list);
            return _cache.Set<List<CustomDosageDto>>(CUSTOM_DOSAGE_KEY, map);
        }
        return data;
    }

    /// <summary>
    /// 更新或插入新记录
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<bool> UpsertAsync(CustomDosageDto param)
    {
        if (param == null) return false;
        if (param.Id.HasValue)
        {
            //更新
            var entity = await _prescribeCustomRuleRepository.GetEntityByIdAsync(param.Id.Value);
            entity.Update(name: param.Name, dosageQty: param.DosageQty, dosageUnit: param.DosageUnit, defaultDosageQty: param.DefaultDosageQty,
                unpack: param.Unpack, bigPackFactor: param.BigPackFactor, bigPackPrice: param.BigPackPrice, bigPackUnit: param.BigPackUnit,
                smallPackFactor: param.SmallPackFactor, smallPackPrice: param.SmallPackPrice, smallPackUnit: param.SmallPackUnit, specification: param.Specification);
            _ = await _prescribeCustomRuleRepository.UpdateAsync(entity);
            CleanCache();
            return true;
        }
        else
        {
            //新增
            var entity = new PrescribeCustomRule
            {
                Code = param.Code,
                Name = param.Name,
                DosageQty = param.DosageQty,
                DosageUnit = param.DosageUnit,
                DefaultDosageQty = param.DefaultDosageQty,
                Unpack = param.Unpack,
                BigPackFactor = param.BigPackFactor,
                BigPackPrice = param.BigPackPrice,
                BigPackUnit = param.BigPackUnit,
                SmallPackFactor = param.SmallPackFactor,
                SmallPackPrice = param.SmallPackPrice,
                SmallPackUnit = param.SmallPackUnit,
                Specification = param.Specification
            };
            _ = await _prescribeCustomRuleRepository.InsertAsync(entity);
            return true;
        }
    }

    /// <summary>
    /// 如果数据库插入请执行这个操作，清空一次剂量的缓存
    /// </summary>
    /// <returns></returns>
    public bool CleanCache()
    {
        _cache.Remove(CUSTOM_DOSAGE_KEY);
        return true;
    }

}
