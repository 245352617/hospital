using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

public class HISMedicineRepository : EfCoreRepository<HISDbContext, HISMedicine>, IHISMedicineRepository
{
    #region constructor

    public HISMedicineRepository(IDbContextProvider<HISDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    #endregion

    #region GetPagedList

    /// <summary>
    /// 获取勾选的急诊标记的所有药品信息
    /// </summary>
    /// <returns></returns>
    public async Task<IList<HisMedicineSample>> GetHisMedicineSampleAsync()
    {
        //勾上急诊标志：  ExecDeptCode = 1(西药房) emergencySign in(0, 1)(全科室药品 + 急诊药品)
        var db = await GetDbSetAsync();
        var query = await db.AsNoTracking()
            .Where(w => w.Qty > 0 && new decimal[] { 0, 1 }.Contains(w.EmergencySign))
            .Select(s => new HisMedicineSample
            {
                InvId = s.InvId,
                EmergencySign = s.EmergencySign,
                IsFirstAid = s.IsFirstAid,
                MedicineCode = s.MedicineCode,
                MedicineName = s.MedicineName,
                PharmacyCode = s.PharmacyCode,
                PharmacyName = s.PharmacyName,
            })
            .ToListAsync();

        return query;

    }

    /// <summary>
    ///  获取分页数据
    /// </summary>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="nameOrPyCode"></param>
    /// <param name="category"></param>
    /// <param name="platformType"></param>
    /// <param name="pharmacyCode"></param>
    /// <param name="emergencySign"></param>
    /// <returns></returns>
    public async Task<(List<HISMedicine>, int)> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string nameOrPyCode = null,
        string category = null,
        int? isEmergency = null,
        PlatformType platformType = PlatformType.All,
        string pharmacyCode = null,
        int emergencySign = 0)
    {
        var db = await GetDbSetAsync();
        var query = db.AsNoTracking()
            .Where(m => m.Qty > 0)
            .WhereIf(!nameOrPyCode.IsNullOrWhiteSpace(), w => w.MedicineName.Contains(nameOrPyCode) || w.PyCode.Contains(nameOrPyCode))
             .WhereIf(!category.IsNullOrWhiteSpace(), w => w.MedicineProperty.Equals(category))
             .WhereIf(!pharmacyCode.IsNullOrWhiteSpace(), w => w.PharmacyCode.Equals(pharmacyCode))
             .WhereIf(platformType != PlatformType.All, w => w.PlatformType == platformType)
             .WhereIf(emergencySign == 1, w => new decimal[] { 0, 1 }.Contains(w.EmergencySign)) //勾上急诊标志：  ExecDeptCode = 1(西药房) emergencySign in(0, 1)(全科室药品 + 急诊药品)
             .WhereIf(emergencySign == 0, w => new decimal[] { 0, 2 }.Contains(w.EmergencySign)); //未勾选就诊标志: ExecDeptCode = 1（西药房）emergencySign in(0, 2)（全科室药品 + 普通药品）

        var list = await query
            .OrderBy(o => o.PyCode.IndexOf(nameOrPyCode))
            .ThenBy(o => o.MedicineName.IndexOf(nameOrPyCode))
            .ThenBy(o => o.PyCode.Length)
            .ThenBy(o => o.MedicineName)
            .PageBy(skipCount, maxResultCount).ToListAsync();

        var count = await query.CountAsync();
        return (list, count);
    }

    /// <summary>
    /// 根据药品编码获取药品信息(化简，不需要分页信息)
    /// </summary>
    /// <param name="medicineIds"></param>
    /// <returns></returns>
    public async Task<List<HISMedicine>> GetHisMedicinesAsync(decimal[] medicineIds)
    {
        var db = await GetDbSetAsync();
        var query = await db.AsNoTracking().Where(w => w.Qty > 0 && medicineIds.Contains(w.InvId)).ToListAsync();
        return query;
    }

    /// <summary>
    /// 根据药药品编码，药品厂商，药品规格获取药品信息
    /// </summary>
    /// <param name="medicineCode"></param>
    /// <param name="factoryCode"></param>
    /// <param name="specification"></param>
    /// <returns></returns>
    public async Task<List<HISMedicine>> GetMedicinesAsync(int medicineCode, int factoryCode, string specification)
    {
        var db = await GetDbSetAsync();
        var query = await db.AsNoTracking()
            .Where(w => w.Qty > 0 && w.MedicineCode == medicineCode && w.FactoryCode == factoryCode && w.Specification == specification)
            .ToListAsync();
        return query;
    }


    #endregion

    /// <summary>
    ///  获取全部数据
    /// </summary>
    /// <returns></returns>
    public async Task<List<HISMedicine>> GetListAsync()
    {
        var db = await GetDbSetAsync();
        var list = await db.AsNoTracking().ToListAsync();

        return list;
    }
}