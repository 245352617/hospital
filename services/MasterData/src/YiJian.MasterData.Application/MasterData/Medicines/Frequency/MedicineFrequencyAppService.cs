using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData;

/// <summary>
/// 药品频次字典 API
/// </summary>
[Authorize]
public class MedicineFrequencyAppService : MasterDataAppService, IMedicineFrequencyAppService
{
    private readonly IMedicineFrequencyRepository _medicineFrequencyRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="medicineFrequencyRepository"></param>
    public MedicineFrequencyAppService(IMedicineFrequencyRepository medicineFrequencyRepository)
    {
        _medicineFrequencyRepository = medicineFrequencyRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(MedicineFrequencyCreation input)
    {
        //编码为空则生成随机数
        if (string.IsNullOrEmpty(input.FrequencyCode))
        {
            Random random = new Random(20);
            input.FrequencyCode = random.Next(100000000, 999999999).ToString();
        }

        if (await _medicineFrequencyRepository.AnyAsync(a => a.FrequencyCode == input.FrequencyCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<MedicineFrequencyCreation, MedicineFrequency>(input);
        var result = await _medicineFrequencyRepository.InsertAsync(model);

        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> UpdateAsync(MedicineFrequencyUpdate input)
    {
        var medicineFrequency = await _medicineFrequencyRepository.GetAsync(input.Id);
        if (medicineFrequency == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        medicineFrequency.Modify(name: input.FrequencyName, // 频次名称
            times: input.FrequencyTimes, // 频次系数
            unit: input.FrequencyUnit, // 频次单位
            execTimes: input.FrequencyExecDayTimes, // 执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开
            weeks: input.FrequencyWeeks, // 频次周明细
            catalog: input.Catalog, // 频次分类 0：临时 1：长期 2：通用
            indexNo: input.Sort, // 排序号
            remark: input.Remark, // 备注
            isActive: input.IsActive, // 是否启用
            fullName: input.FullName, thirdPartyId: input.ThirdPartyId
        );
        var result = await _medicineFrequencyRepository.UpdateAsync(medicineFrequency);
        return result.Id;
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<MedicineFrequencyData> GetAsync(int id)
    {
        var medicineFrequency = await _medicineFrequencyRepository.GetAsync(id);

        return ObjectMapper.Map<MedicineFrequency, MedicineFrequencyData>(medicineFrequency);
    }

    #endregion Get

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        if (!await _medicineFrequencyRepository.AnyAsync(x => x.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _medicineFrequencyRepository.DeleteAsync(id);
    }

    #endregion Delete

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<MedicineFrequencyData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        List<MedicineFrequency> medicineFrequencys = await _medicineFrequencyRepository.GetListAsync(filter, sorting);

        return new ListResultDto<MedicineFrequencyData>(
            ObjectMapper.Map<List<MedicineFrequency>, List<MedicineFrequencyData>>(medicineFrequencys));
    }

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<MedicineFrequencyData>> GetPagedListAsync(GetMedicineFrequencyPagedInput input)
    {
        List<MedicineFrequency> medicineFrequencies = await _medicineFrequencyRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        List<MedicineFrequencyData> medicineFrequencyDatas = ObjectMapper.Map<List<MedicineFrequency>, List<MedicineFrequencyData>>(medicineFrequencies);

        long totalCount = await _medicineFrequencyRepository.GetCountAsync(input.Filter);

        PagedResultDto<MedicineFrequencyData> result = new PagedResultDto<MedicineFrequencyData>(totalCount, medicineFrequencyDatas.AsReadOnly());

        return result;
    }
}