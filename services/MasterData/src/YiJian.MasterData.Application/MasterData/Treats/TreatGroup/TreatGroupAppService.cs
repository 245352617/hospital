using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData.Treats;

/// <summary>
/// 诊疗分组API
/// </summary>
[Authorize]
public class TreatGroupAppService : MasterDataAppService, ITreatGroupAppService
{
    private readonly ITreatGroupRepository _treatGroupRepository;
    private readonly ITreatRepository _treatRepository;
    private readonly IDictionariesRepository _dictionariesRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="treatGroupRepository"></param>
    /// <param name="dictionariesRepository"></param>
    /// <param name="treatRepository"></param>
    public TreatGroupAppService(ITreatGroupRepository treatGroupRepository
        , IDictionariesRepository dictionariesRepository
        , ITreatRepository treatRepository)
    {
        _treatGroupRepository = treatGroupRepository;
        _dictionariesRepository = dictionariesRepository;
        _treatRepository = treatRepository;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="dictionariesCode"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    public async Task<bool> SaveAsync(string dictionariesCode, List<TreatGroupCreate> dto)
    {
        var treatGroupList =
            await (await _treatGroupRepository.GetQueryableAsync()).Where(x => x.DictionaryCode == dictionariesCode).ToListAsync();
        var dict = await (await _dictionariesRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
            x.DictionariesCode == dictionariesCode && x.DictionariesTypeCode == "RecipeCategory");
        if (dict == null)
        {
            throw new EcisBusinessException(message: "分类不存在");
        }

        //删除
        if (treatGroupList.Any())
        {
            await _treatGroupRepository.DeleteManyAsync(treatGroupList);
        }

        var addTreatList = new List<TreatGroup>();
        dto.ForEach(f =>
        {
            addTreatList.Add(new TreatGroup(GuidGenerator.Create(), f.CatalogCode, f.CatalogName,
                dictionariesCode, dict.DictionariesName));
        });
        await _treatGroupRepository.InsertManyAsync(addTreatList);
        return true;
    }

    /// <summary>
    /// 获取诊疗分类
    /// </summary>
    /// <param name="dictionariesCode"></param>
    /// <returns></returns>
    public async Task<List<TreatCatalogDto>> GetTreatCatalogAsync([Required] string dictionariesCode)
    {
        var notTreatCatalog = await (await _treatGroupRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrEmpty(dictionariesCode), x => x.DictionaryCode != dictionariesCode).ToListAsync();
        //其他已使用分类不在当前列表显示
        var catalog = await (await _treatRepository.GetQueryableAsync())
            .WhereIf(notTreatCatalog.Any(),
                w => !notTreatCatalog.Select(s => s.CatalogCode).Contains(w.CategoryCode))
            .GroupBy(g => new { g.CategoryCode, g.CategoryName })
            .Select(s => new TreatCatalogDto
            {
                CatalogCode = s.Key.CategoryCode,
                CatalogName = s.Key.CategoryName
            }).ToListAsync();
        return catalog;
    }
    /// <summary>
    /// 获取处置列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<DictDto>> GetListAsync()
    {
        var dictionariesCodeArr = new List<string> { "Medicine" };
        var dict = await (await _dictionariesRepository.GetQueryableAsync()).Where(w =>
                w.DictionariesTypeCode == "RecipeCategory"
                && !dictionariesCodeArr.Contains(w.DictionariesCode)
                && w.Status)
            .OrderBy(o => o.Sort)
            .ToListAsync();

        if (!dict.Any()) return new List<DictDto>();

        var treatCatalog = await (await _treatGroupRepository.GetQueryableAsync()).ToListAsync();
        var dictList = new List<DictDto>();
        foreach (var item in dict)
        {
            dictList.Add(new DictDto()
            {
                DictionariesCode = item.DictionariesCode,
                DictionariesName = item.DictionariesName,
                TreatGroup = ObjectMapper.Map<List<TreatGroup>, List<TreatCatalogDto>>(treatCatalog
                    .Where(x => x.DictionaryCode == item.DictionariesCode)
                    .ToList())
            });
        }

        return dictList;
    }
}