using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Contracts;

namespace YiJian.Recipe.Repositories.DoctorsAdvices;

/// <summary>
/// 分方管理
/// </summary>
public class PrescriptionRepository : EfCoreRepository<RecipeDbContext, Prescription, Guid>, IPrescriptionRepository
{
    /// <summary>
    /// 分方管理
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public PrescriptionRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
      : base(dbContextProvider)
    {

    }

    /// <summary>
    /// 根据Id获取指定的分方信息
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<List<Prescription>> GetPrescriptionsByIdsAsync(List<Guid> ids)
    {
        if (!ids.Any()) return await Task.FromResult(new List<Prescription>());
        var db = await GetDbSetAsync();
        return await db.Where(w => ids.Contains(w.Id)).ToListAsync();
    }

    /// <summary>
    /// 根据visSerialNo获取指定的分方信息
    /// </summary>
    /// <param name="visSerialNo"></param>
    /// <returns></returns>
    public async Task<List<Prescription>> GetPrescriptionsByVisSerialNoAsync(string visSerialNo)
    {
        if (visSerialNo.IsNullOrEmpty()) return await Task.FromResult(new List<Prescription>());
        var db = await GetDbSetAsync();
        return await db.Where(w => w.VisSerialNo == visSerialNo.Trim()).ToListAsync();
    }

    /// <summary>
    /// 根据就诊流水号和处方单号列表查询指定的分方信息
    /// </summary>
    /// <param name="visSerialNo"></param>
    /// <param name="prescriptionNos"></param>
    /// <returns></returns>
    public async Task<List<Prescription>> GetPrescriptionsByBillNosAsync(string visSerialNo, List<string> prescriptionNos)
    {
        if (visSerialNo.IsNullOrEmpty() || !prescriptionNos.Any()) return await Task.FromResult(new List<Prescription>());
        var db = await GetDbSetAsync();
        return await db.Where(w => w.VisSerialNo == visSerialNo.Trim() && prescriptionNos.Contains(w.PrescriptionNo)).ToListAsync();
    }

}
