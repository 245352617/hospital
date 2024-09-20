using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.MasterData.Pharmacies.Dto;
using YiJian.MasterData.Pharmacies.Contracts;
using YiJian.MasterData.Pharmacies.Entities;

namespace YiJian.MasterData.MasterData.Pharmacies;

/// <summary>
/// 药房配置
/// </summary>
[Authorize]
public class PharmacyAppService : MasterDataAppService, IPharmacyAppService
{
    private readonly IPharmacyRepository _pharmacyReposity;

    public PharmacyAppService(IPharmacyRepository pharmacyReposity)
    {
        _pharmacyReposity = pharmacyReposity;
    }

    /// <summary>
    /// 获取药房配置信息
    /// </summary>
    /// <returns></returns> 
    public async Task<List<ModifyPharmacyDto>> GetAsync()
    {
        var list = await (await _pharmacyReposity.GetQueryableAsync()).ToListAsync();
        var map = ObjectMapper.Map<List<Pharmacy>, List<ModifyPharmacyDto>>(list);
        return map;
    }

    /// <summary>
    /// 删除操作，POST是兼容操作,DELETE是Restfull接口
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    [HttpPost, HttpDelete]
    public async Task<Guid> DeleteAsync(Guid id)
    {
        var entity = await (await _pharmacyReposity.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
        if (entity == null) Oh.Error("没有你的药房配置记录");
        await _pharmacyReposity.DeleteAsync(id);
        return id;
    }

    /// <summary>
    /// 添加，更改药房配置信息
    /// </summary>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<Guid> ModifyAsync(ModifyPharmacyDto model)
    {
        //修改
        if (model.Id.HasValue) return await UpdateAsync(model); 
        //添加 
        return await AddAsync(model); 
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    private async Task<Guid> AddAsync(ModifyPharmacyDto model)
    {
        if (model.IsDefault)
        {
            var list = await (await _pharmacyReposity.GetQueryableAsync()).ToListAsync();
            foreach (var item in list)
            {
                item.ChangeIsDefaultValue(false);
            }
            await _pharmacyReposity.UpdateManyAsync(list);
        }

        var entity = new Pharmacy(GuidGenerator.Create(), model.PharmacyCode, model.PharmacyName,model.IsDefault);
        _ = await _pharmacyReposity.InsertAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    private async Task<Guid> UpdateAsync(ModifyPharmacyDto model)
    {
        List<Pharmacy> data = new List<Pharmacy>();
        if (model.IsDefault)
        {
            var list = await (await _pharmacyReposity.GetQueryableAsync()).ToListAsync();
            foreach (var item in list)
            {
                item.ChangeIsDefaultValue(false);
                data.Add(item);
            } 
        } 

        var entity = await (await _pharmacyReposity.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
        if (entity == null) Oh.Error("找不到需要更新的记录"); 
        entity.Update(model.PharmacyCode, model.PharmacyName, model.IsDefault);
        data.Add(entity); 
        if (data.Count>0) await _pharmacyReposity.UpdateManyAsync(data); 
        return model.Id.Value;
    }

}
