using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace YiJian.MasterData.VitalSign;

/// <summary>
/// 生命体征表达式 API
/// </summary>
[Authorize]
public class VitalSignExpressionAppService : MasterDataAppService, IVitalSignExpressionAppService, ICapSubscribe
{
    private readonly VitalSignExpressionManager _vitalSignExpressionManager;
    private readonly IVitalSignExpressionRepository _vitalSignExpressionRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="vitalSignExpressionRepository"></param>
    /// <param name="vitalSignExpressionManager"></param>
    public VitalSignExpressionAppService(IVitalSignExpressionRepository vitalSignExpressionRepository,
        VitalSignExpressionManager vitalSignExpressionManager)
    {
        _vitalSignExpressionRepository = vitalSignExpressionRepository;
        _vitalSignExpressionManager = vitalSignExpressionManager;
    }

    #endregion constructor

    /// <summary>
    /// 获取生命体征评分表达式集合
    /// </summary>
    /// <returns></returns>
    public async Task<List<VitalSignExpressionData>> GetVitalSignExpressionListAsync()
    {
        var model = await _vitalSignExpressionRepository.GetListAsync();
        var dto = ObjectMapper.Map<List<VitalSignExpression>, List<VitalSignExpressionData>>(model);
        return dto;
    }

    /// <summary>
    /// 获取单个生命体征评分表达式
    /// </summary>
    /// <returns></returns>
    public async Task<VitalSignExpressionData> GetVitalSignExpressionAsync(Guid id)
    {
        var model = await _vitalSignExpressionRepository.GetAsync(x => x.Id == id);
        var dto = ObjectMapper.Map<VitalSignExpression, VitalSignExpressionData>(model);
        return dto;
    }
    /// <summary>
    /// 同步生命体征表达式
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [CapSubscribe("masterdata.vitalSignExpression.save.from.triage", Group = "ecis.masterdata")]
    [NonAction]
    public async Task SyncVitalSignExpressionAsync(CreateOrUpdateVitalSignExpressionDto dto)
    {
        using var uow = UnitOfWorkManager.Begin();
        await SaveVitalSignExpressionAsync(dto);
        await uow.CompleteAsync();
    }

    /// <summary>
    /// 保存生命体征表达式
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task SaveVitalSignExpressionAsync(CreateOrUpdateVitalSignExpressionDto dto)
    {
        var model = await _vitalSignExpressionRepository.FirstOrDefaultAsync(x => x.ItemName == dto.ItemName);
        if (model != null)
        {
            model.Modify(dto.ItemName, dto.StLevelExpression, dto.NdLevelExpression, dto.RdLevelExpression,
                dto.ThALevelExpression, dto.ThBLevelExpression);
            await _vitalSignExpressionRepository.UpdateAsync(model);
        }
        else
        {
            var result = await _vitalSignExpressionManager.CreateAsync(dto.ItemName, dto.StLevelExpression,
           dto.NdLevelExpression, dto.RdLevelExpression, dto.ThALevelExpression, dto.ThBLevelExpression,
           dto.StLevelExpression, dto.NdLevelExpression, dto.RdLevelExpression, dto.ThALevelExpression,
           dto.ThBLevelExpression);
        }

    }
    /// <summary>
    /// 同步删除生命体征评分表达式
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    [CapSubscribe("masterdata.vitalSignExpression.delete.from.triage", Group = "ecis.masterdata")]
    [NonAction]
    public async Task SyncDeleteVitalSignExpressionAsync(string itemName)
    {
        using var uow = UnitOfWorkManager.Begin();
        var model = await _vitalSignExpressionRepository.FirstOrDefaultAsync(x => x.ItemName == itemName);
        if (model != null)
        {
            await _vitalSignExpressionRepository.DeleteAsync(model);
        }
        await uow.CompleteAsync();

    }
}