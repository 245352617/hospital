using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;



public class MedicineRepository : MasterDataRepositoryBase<Medicine, int>, IMedicineRepository
{
    #region constructor

    public MedicineRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    #endregion

    #region GetCount

    /// <summary>
    /// 统计总记录数
    /// </summary>
    /// <param name="nameOrPyCode"></param>
    /// <param name="category"></param>
    /// <param name="isEmergency"></param>
    /// <param name="platformType"></param>
    /// <param name="pharmacyCode"></param>
    /// <param name="toxicLevel"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<long> GetCountAsync(string nameOrPyCode = null,
        string category = null,
        bool? isEmergency = null, PlatformType platformType = PlatformType.All, string pharmacyCode = null,int? toxicLevel=null,bool? isActive=null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !nameOrPyCode.IsNullOrWhiteSpace(),
                m =>
                    m.MedicineName.Contains(nameOrPyCode) || m.PyCode.Contains(nameOrPyCode)
            )
            .WhereIf(
                !category.IsNullOrWhiteSpace(),
                m =>
                    m.MedicineProperty.Equals(category)
            )
            .WhereIf(
                !pharmacyCode.IsNullOrWhiteSpace(),
                m =>
                    m.PharmacyCode.Equals(pharmacyCode)
            )
            .WhereIf(
                isEmergency.HasValue,
                m =>
                    m.IsFirstAid == isEmergency.Value
            )
            .WhereIf(
                toxicLevel.HasValue,
                m =>
                    m.ToxicLevel == toxicLevel
            )           
            .WhereIf(
                isActive.HasValue,
                m =>
                    m.IsActive == isActive.Value
            )
            .WhereIf(
                platformType != PlatformType.All,
                e =>
                    e.PlatformType == platformType)
            .LongCountAsync();
    }

    #endregion

    #region Categories

    /// <summary>
    /// 获取目录列表
    /// </summary>
    /// <returns></returns>
    public async Task<string[]> GetCategoriesAsync()
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .Select(m => m.MedicineProperty)
            .Distinct()
            .ToArrayAsync();
    }

    #endregion

    #region GetPagedList

    /// <summary>
    /// 获取分页数据
    /// </summary>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="nameOrPyCode"></param>
    /// <param name="category"></param>
    /// <param name="isEmergency"></param>
    /// <param name="platformType"></param>
    /// <param name="pharmacyCode"></param>
    /// <param name="toxicLevel"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<List<Medicine>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string nameOrPyCode = null,
        string category = null,
        bool? isEmergency = null, PlatformType platformType = PlatformType.All, string pharmacyCode = null,int? toxicLevel=null,bool? isActive=null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            //.IncludeDetails(includeDetails)
            .WhereIf(
                !nameOrPyCode.IsNullOrWhiteSpace(),
                m =>
                    m.MedicineName.Contains(nameOrPyCode) || m.PyCode.Contains(nameOrPyCode)
            )
            .WhereIf(
                !category.IsNullOrWhiteSpace(),
                m =>
                    m.MedicineProperty.Equals(category)
            ).WhereIf(
                !pharmacyCode.IsNullOrWhiteSpace(),
                m =>
                    m.PharmacyCode.Equals(pharmacyCode)
            )
            .WhereIf(
                isEmergency.HasValue,
                m =>
                    m.IsFirstAid == isEmergency.Value
            )
            .WhereIf(
                toxicLevel.HasValue,
                m =>
                    m.ToxicLevel == toxicLevel
            )
            .WhereIf(
                isActive.HasValue,
                m =>
                    m.IsActive == isActive.Value
            )
            .WhereIf(
                platformType != PlatformType.All,
                e =>
                    e.PlatformType == platformType)
            .OrderByDescending(o => o.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

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
    public async Task<List<T>> GetPagedDtoAsync<T>(
        Expression<Func<Medicine, T>> selectorDto,
        int pageIndex = 1,
        int pageSize = 20,
        string nameOrPyCode = null,
        string category = null,
        bool? isEmergency = null, PlatformType platformType = PlatformType.All, string pharmacyCode = null,int? toxicLevel=null,bool? isActive=null)
    {
        if (pageIndex <= 0) pageIndex = 1;
        if (pageSize <= 0) pageSize = 20;

        return await (await GetDbSetAsync())
            .AsNoTracking()
            //.IncludeDetails(includeDetails)
            .WhereIf(
                !nameOrPyCode.IsNullOrWhiteSpace(),
                m =>
                    m.MedicineName.Contains(nameOrPyCode) || m.PyCode.Contains(nameOrPyCode)
            )
            .WhereIf(
                !category.IsNullOrWhiteSpace(),
                m =>
                    m.MedicineProperty.Equals(category)
            ).WhereIf(
                !pharmacyCode.IsNullOrWhiteSpace(),
                m =>
                    m.PharmacyCode.Equals(pharmacyCode)
            )
            .WhereIf(
                isEmergency.HasValue,
                m =>
                    m.IsFirstAid == isEmergency.Value
            )
            .WhereIf(
                toxicLevel.HasValue,
                m =>
                    m.ToxicLevel == toxicLevel
            )
            .WhereIf(
                isActive.HasValue,
                m =>
                    m.IsActive == isActive.Value
            )
            .WhereIf(
                platformType != PlatformType.All,
                e =>
                    e.PlatformType == platformType)
            .OrderByDescending(o => o.CreationTime)
            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .Select(selectorDto).ToListAsync();
    }
    #endregion
}