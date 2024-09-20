using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData;


/// <summary>
/// 药品频次字典 修改输入
/// </summary>
[Serializable]
public class MedicineFrequencyUpdate
{ 
    public int Id { get; set; }

    /// <summary>
    /// 频次名称
    /// </summary>
    [Required(ErrorMessage = "频次名称不能为空！")]
    [DynamicStringLength(typeof(MedicineFrequencyConsts), nameof(MedicineFrequencyConsts.MaxFrequencyNameLength), ErrorMessage = "频次名称最大长度不能超过{1}!")]
    public string  FrequencyName { get; set; }

    /// <summary>
    /// 频次全称
    /// </summary>
    [DynamicStringLength(typeof(MedicineFrequencyConsts), nameof(MedicineFrequencyConsts.MaxFullNameLength), ErrorMessage = "频次全称最大长度不能超过{1}!")]
    public string  FullName { get; set; }

    /// <summary>
    /// 频次系数
    /// </summary>
    [Required(ErrorMessage = "频次系数不能为空！")]
    public int FrequencyTimes { get; set; }

    /// <summary>
    /// 频次单位
    /// </summary>
    [DynamicStringLength(typeof(MedicineFrequencyConsts), nameof(MedicineFrequencyConsts.MaxUnitLength), ErrorMessage = "频次单位最大长度不能超过{1}!")]
    public string FrequencyUnit { get; set; }

    /// <summary>
    /// 执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开
    /// </summary>
    [DynamicStringLength(typeof(MedicineFrequencyConsts), nameof(MedicineFrequencyConsts.MaxExecDayTimesLength), ErrorMessage = "执行时间最大长度不能超过{1}!")]
    public string FrequencyExecDayTimes { get; set; }

    /// <summary>
    /// 频次周明细
    /// </summary>
    [DynamicStringLength(typeof(MedicineFrequencyConsts), nameof(MedicineFrequencyConsts.MaxWeeksLength), ErrorMessage = "频次周明细最大长度不能超过{1}!")]
    public string FrequencyWeeks { get; set; }

    /// <summary>
    /// 频次分类 0：临时 1：长期 2：通用
    /// </summary>
    [Required(ErrorMessage = "频次分类不能为空！")]
    public MedicineFrequencyCatalog  Catalog { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int  Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DynamicStringLength(typeof(MedicineFrequencyConsts), nameof(MedicineFrequencyConsts.MaxRemarkLength), ErrorMessage = "备注最大长度不能超过{1}!")]
    public string  Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// 第三方id
    /// </summary>
    public string ThirdPartyId { get; set; }
}