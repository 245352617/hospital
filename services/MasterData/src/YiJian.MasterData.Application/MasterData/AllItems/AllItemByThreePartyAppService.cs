using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.Core.UnifyResult;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.AllItems;

/// <summary>
/// 诊疗检查检验药品项目合集 API
/// </summary>
[Authorize]
public class AllItemByThreePartyAppService : MasterDataAppService, IAllItemByThreePartyAppService
{
    private readonly AllItemManager _allItemManager;
    private readonly IAllItemRepository _allItemRepository;
    private readonly IMedicineRepository _medicineRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="allItemRepository"></param>
    /// <param name="allItemManager"></param>
    public AllItemByThreePartyAppService(IAllItemRepository allItemRepository, AllItemManager allItemManager,
        IMedicineRepository medicineRepository)
    {
        _allItemRepository = allItemRepository;
        _allItemManager = allItemManager;
        _medicineRepository = medicineRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> SaveAsync(AllItemCreation input)
    {
        var all = await _allItemRepository.FirstOrDefaultAsync(w =>
            w.Code == input.Code && w.CategoryCode == input.CategoryCode && w.TypeCode == input.TypeCode);
        if (all == null)
        {
            var allItem = await _allItemManager.CreateAsync(categoryCode: input.CategoryCode, // 分类编码
                categoryName: input.CategoryName, // 分类名称
                code: input.Code, // 编码
                name: input.Name, // 名称
                unit: input.Unit, // 单位
                charge: input.Price, // 价格
                indexNo: input.IndexNo, // 排序
                typeCode: input.TypeCode, // 类型编码
                typeName: input.TypeName, // 类型名称
                chargeCode: input.ChargeCode,
                chargeName: input.ChargeName
            );
            return allItem.Id;
        }

        all.Modify(
            name: input.Name, // 名称
            unit: input.Unit, // 单位
            price: input.Price, // 价格
            indexNo: input.IndexNo, // 排序
            chargeCode: input.ChargeCode,
            chargeName: input.ChargeName
        );
        var result = await _allItemRepository.UpdateAsync(all);
        return result.Id;
    }

    #endregion Create

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<AllItemData> GetAsync(int id)
    {
        var allItem = await _allItemRepository.GetAsync(id);

        return ObjectMapper.Map<AllItem, AllItemData>(allItem);
    }

    #endregion Get

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cateCode"></param>
    /// <param name="typeCode"></param>
    /// <returns></returns>
    public async Task DeleteAsync(string code, string cateCode, string typeCode)
    {
        var all = await _allItemRepository.FirstOrDefaultAsync(w =>
            w.Code == code && w.CategoryCode == cateCode && w.TypeCode == typeCode);
        if (all != null)
        {
            await _allItemRepository.DeleteAsync(all);
        }
    }

    #endregion Delete

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<AllItemData>> GetPagedListAsync(GetAllItemPagedInput input)
    {
        var allItems = await _allItemRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting,
            input.CategoryCode);

        var items = ObjectMapper.Map<List<AllItem>, List<AllItemData>>(allItems);

        var totalCount = await _allItemRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<AllItemData>(totalCount, items.AsReadOnly());

        return result;
    }

    /// <summary>
    /// 获取所有医嘱项目分页记录-院前app
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<List<AllItemDataPreHospitalDto>> GetPagedListAppAsync(GetAllItemPagedInput input)
    {
        if (input.TypeCode.IsNullOrEmpty()) input.TypeCode = "PreHospital";
        PlatformType platformType = PlatformType.All;
        switch (input.TypeCode)
        {
            case "PreHospital":
                platformType = PlatformType.PreHospital;
                break;
        }

        List<AllItemDataPreHospitalDto> list = null;

        switch (input.CategoryCode)
        {
            case "Medicine":
                list=  await _medicineRepository.GetPagedDtoAsync(a => new AllItemDataPreHospitalDto
                {
                    Id = a.Id,
                    CategoryCode = "Medicine",
                    CategoryName = "药品",
                    Code = a.MedicineCode,
                    Name = a.MedicineName,
                    Unit = a.Unit,
                    Price = a.Price,
                    TypeCode = a.PlatformType.ToString(),
                    TypeName = "院前急救",
                    FrequencyCode = a.FrequencyCode,
                    FrequencyName = a.FrequencyName,
                    UsageCode = a.UsageCode,
                    UsageName = a.UsageName,
                    Specification = a.Specification
                }, input.Index, input.Size, platformType: platformType,
                  nameOrPyCode:input.Filter,
                  pharmacyCode:input.Filter,
                  isActive:true);
                break;
            default:
                list = ObjectMapper.Map<List<AllItem>, List<AllItemDataPreHospitalDto>>(await _allItemRepository.GetPagedListAsync(
                    input.SkipCount,
                    input.Size,
                    input.Filter,
                    input.Sorting,
                    input.CategoryCode,
                    input.TypeCode));
                break;
        }

        if (list != null)
        {
            foreach (var r in list)
            {
                if (r.FrequencyCode.IsNullOrEmpty())
                {
                    r.FrequencyCode = "ONCE";
                    r.FrequencyName = "ONCE";
                }
            }
        }

        return list;
    }

    #endregion GetPagedList
}