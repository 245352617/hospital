using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品字典 API
/// </summary>
public partial class MedicineAppService
{
    /// <summary>
    /// 同步药品药理信息
    /// </summary>
    /// <returns></returns> 
    public async Task SyncAllToxicAsync()
    {
        var entities = await (await _medicineRepository.GetQueryableAsync()).Where(w => !w.IsSyncToReciped).ToListAsync();
        if (entities.Count == 0) return;
        var eto = ObjectMapper.Map<List<Medicine>, List<ToxicEto>>(entities);
        _capPublisher.Publish<List<ToxicEto>>("sync.masterdata.toxic.all", eto);
        entities.ForEach(x => x.IsSyncToReciped = true);
        await _medicineRepository.UpdateManyAsync(entities, true);
    }


    /// <summary>
    /// 同步药品信息
    /// </summary>
    /// <returns></returns> 
    [CapSubscribe("sync.his.to.medicine")]
    public async Task SyncMedicineAsync(List<HISMedicine> eto)
    {

        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var pushModels = new List<Medicine>();
            var updateEntities = new List<Medicine>();
            var invid = eto.Select(s => s.InvId);

            //取本地数据的所有药品
            var allEntities = await (await _medicineRepository.GetQueryableAsync()).AsTracking().ToListAsync(); 
            //更新的记录
            var oldEntities = allEntities.Where(w => invid.Contains(w.Id)).ToList();
            //已经存在的
            var oldIds = oldEntities.Select(s => s.Id).ToList();
            //新的记录
            var newEntities = eto.Where(w => !oldIds.Contains(Convert.ToInt32(w.InvId))).ToList();

            var insertEntities = ObjectMapper.Map<List<HISMedicine>, List<Medicine>>(newEntities); //映射新增记录

            foreach (var item in oldEntities)
            {
                var model = eto.FirstOrDefault(w => w.InvId == item.Id);
                if (model == null) continue;   
                item.SyncData(model);
                pushModels.Add(item);
            }

            if (insertEntities.Any())
            {
                await _medicineRepository.InsertManyAsync(insertEntities);
                pushModels.AddRange(insertEntities);
            }
            if (updateEntities.Any())
            {
                await _medicineRepository.UpdateManyAsync(updateEntities);
                pushModels.AddRange(updateEntities);
            }

            var pushEto = ObjectMapper.Map<List<Medicine>, List<ToxicEto>>(pushModels);
            _capPublisher.Publish<List<ToxicEto>>("sync.masterdata.toxic.all", pushEto);

            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"同步药理数据异常:{ex.Message}");
            await uow.RollbackAsync();
            throw;
        }

    }

}
