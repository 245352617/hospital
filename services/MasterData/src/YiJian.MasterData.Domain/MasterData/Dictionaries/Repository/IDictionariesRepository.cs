using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData;

public interface IDictionariesRepository: IRepository<Dictionaries, Guid>
{

    /// <summary>
    /// 获取电子病历的字段配置信息
    /// </summary>
    /// <returns></returns>
    Task<List<Dictionaries>> GetEmrWatermarkAsync();

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="dictionariesTypeCode"></param>
    /// <returns></returns>
    Task<List<Dictionaries>> GetListAsync(string dictionariesTypeCode);


}