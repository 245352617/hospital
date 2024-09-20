using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts;

/// <summary>
/// 处方号管理存储接口
/// </summary>
public interface IPrescriptionRepository : IRepository<Prescription, Guid>
{
    /// <summary>
    /// 根据Id获取指定的分方信息
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public Task<List<Prescription>> GetPrescriptionsByIdsAsync(List<Guid> ids);


    /// <summary>
    /// 根据visSerialNo获取指定的分方信息
    /// </summary>
    /// <param name="visSerialNo"></param>
    /// <returns></returns>
    public Task<List<Prescription>> GetPrescriptionsByVisSerialNoAsync(string visSerialNo);

    /// <summary>
    /// 根据就诊流水号和处方单号列表查询指定的分方信息
    /// </summary>
    /// <param name="visSerialNo"></param>
    /// <param name="prescriptionNos"></param>
    /// <returns></returns>
    public Task<List<Prescription>> GetPrescriptionsByBillNosAsync(string visSerialNo, List<string> prescriptionNos);

}
