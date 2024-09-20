using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品字典 仓储接口
/// </summary>  
public interface IMedicineRepository : IRepository<Medicine, int>
{
    Task<long> GetCountAsync(string nameOrPyCode = null,
        string category = null,
        bool? isEmergency = null, PlatformType platformType = PlatformType.All, string pharmacyCode=null,int? toxicLevel=null,bool? isActive=null);

    Task<string[]> GetCategoriesAsync();

    Task<List<Medicine>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string nameOrPyCode = null,
        string category = null,
        bool? isEmergency = null,PlatformType platformType=PlatformType.All, string pharmacyCode=null,int? toxicLevel=null,bool? isActive=null
    );

    /// <summary>
    /// 获取Dto分页数据
    /// </summary>
    /// <param name="selectorDto">Dto 表达式</param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="nameOrPyCode"></param>
    /// <param name="category"></param>
    /// <param name="isEmergency"></param>
    /// <param name="platformType"></param>
    /// <param name="pharmacyCode"></param>
    /// <param name="toxicLevel"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    Task<List<T>> GetPagedDtoAsync<T>(
        Expression<Func<Medicine, T>> selectorDto,
        int pageIndex = 1,
        int pageSize = 20,
        string nameOrPyCode = null,
        string category = null,
        bool? isEmergency = null, PlatformType platformType = PlatformType.All, string pharmacyCode = null,
        int? toxicLevel = null, bool? isActive = null);
    
}