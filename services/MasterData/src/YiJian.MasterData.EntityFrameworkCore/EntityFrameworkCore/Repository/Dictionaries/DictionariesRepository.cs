using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.EntityFrameworkCore;

namespace YiJian.MasterData;

public class DictionariesRepository : MasterDataRepositoryBase<Dictionaries, Guid>, IDictionariesRepository
{
    public DictionariesRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }

    /// <summary>
    /// 获取电子病历的字段配置信息
    /// </summary>
    /// <returns></returns> 
    public async Task<List<Dictionaries>> GetEmrWatermarkAsync()
    {
        var db = await GetDbContextAsync();
        var watermarking = await db.Dictionaries.Where(w=>w.DictionariesTypeCode == "EmrWatermark").ToListAsync();
        return watermarking;
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="dictionariesTypeCode"></param>
    /// <returns></returns>
    public async Task<List<Dictionaries>> GetListAsync(string dictionariesTypeCode)
    {
        if (string.IsNullOrWhiteSpace(dictionariesTypeCode))
            return new List<Dictionaries>();

        return AbpEnumerableExtensions.WhereIf((await GetDbSetAsync())
            .AsNoTracking(), !dictionariesTypeCode.IsNullOrWhiteSpace(),
            d =>
                dictionariesTypeCode.Contains(d.DictionariesTypeCode)
        ).Where(w => w.Status).OrderBy(s => s.Sort).ToList();
    }


}