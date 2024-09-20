using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.Medicines;


/// <summary>
/// 药品用法字典 新增输入
/// </summary>
public class MedicineUsageCreation
{
    /// <summary>
    /// 编码
    /// </summary>
    [DynamicStringLength(typeof(MedicineUsageConsts), nameof(MedicineUsageConsts.MaxUsageCodeLength),
        ErrorMessage = "编码最大长度不能超过{1}!")]
    public string UsageCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空！")]
    [DynamicStringLength(typeof(MedicineUsageConsts), nameof(MedicineUsageConsts.MaxUsageNameLength),
        ErrorMessage = "名称最大长度不能超过{1}!")]
    public string UsageName { get; set; }

    /// <summary>
    /// 全称
    /// </summary>
    [DynamicStringLength(typeof(MedicineUsageConsts), nameof(MedicineUsageConsts.MaxFullNameLength),
        ErrorMessage = "全称最大长度不能超过{1}!")]
    public string FullName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 是否单次
    /// </summary>
    public bool IsSingle { get; set; }

    /// <summary>
    /// 分类  1：输液  2：注射  3：治疗  4：服药  10其他
    /// </summary>
    public MedicineUsageCatalog Catalog { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 诊疗项目 描述：一个或多个项目，多个以,隔开
    /// </summary>
    [DynamicStringLength(typeof(MedicineUsageConsts), nameof(MedicineUsageConsts.MaxTreatCodeLength),
        ErrorMessage = "诊疗项目最大长度不能超过{1}!")]
    public string TreatCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 是否回车组合
    /// </summary>
    public bool IsEnterCombination { get; set; }
}