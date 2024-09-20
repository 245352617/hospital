using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.VitalSign;

/// <summary>
/// 评分项 API Interface
/// </summary>   
public interface IVitalSignExpressionAppService : IApplicationService
{
    /// <summary>
    /// 获取生命体征评分表达式集合
    /// </summary>
    /// <returns></returns>
    Task<List<VitalSignExpressionData>> GetVitalSignExpressionListAsync();

    /// <summary>
    /// 获取单个生命体征评分表达式
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<VitalSignExpressionData> GetVitalSignExpressionAsync(Guid id);

    /// <summary>
    /// 保存生命体征表达式
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task SaveVitalSignExpressionAsync(CreateOrUpdateVitalSignExpressionDto dto);


}
