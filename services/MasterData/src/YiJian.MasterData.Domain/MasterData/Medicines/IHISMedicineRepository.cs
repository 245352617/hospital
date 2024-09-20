using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品字典 仓储接口
/// </summary>  
public interface IHISMedicineRepository : IRepository<HISMedicine>
{ 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="nameOrPyCode"></param>
    /// <param name="category"></param>
    /// <param name="isEmergency"></param>
    /// <param name="platformType"></param>
    /// <param name="pharmacyCode"></param>
    /// <param name="emergencySign"></param>
    /// <returns></returns>
    public Task<(List<HISMedicine>,int)> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string nameOrPyCode = null,
        string category = null,
        int? isEmergency = null,
        PlatformType platformType = PlatformType.All,
        string pharmacyCode = null,
        int emergencySign = 0
    );

    /// <summary>
    /// 获取勾选的急诊标记的所有药品信息
    /// </summary>
    /// <returns></returns>
    public Task<IList<HisMedicineSample>> GetHisMedicineSampleAsync();
     
    /// <summary>
    /// 跟进药品编码获取药品信息
    /// </summary>
    /// <param name="medicineIds"></param>
    /// <returns></returns>
    public Task<List<HISMedicine>> GetHisMedicinesAsync(decimal[] medicineIds);

    /// <summary>
    /// 根据药药品编码，药品厂商，药品规格获取药品信息
    /// </summary>
    /// <param name="medicineCode"></param>
    /// <param name="factoryCode"></param>
    /// <param name="specification"></param>
    /// <returns></returns>
    public Task<List<HISMedicine>> GetMedicinesAsync(int medicineCode, int factoryCode, string specification);

    /// <summary>
    ///  获取全部数据
    /// </summary>
    /// <returns></returns>
    public Task<List<HISMedicine>> GetListAsync();
}