using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Entities;

/// <summary>
/// 自定义规则药品一次剂量名单 (自己维护)
/// </summary>
[Comment("自定义规则药品一次剂量名单 (自己维护)")]
public class PrescribeCustomRule : Entity<int>
{
    /// <summary>
    /// 医嘱编码
    /// </summary>
    [Comment("医嘱编码")]
    [Required, StringLength(20)]
    public string Code { get; set; }

    /// <summary>
    /// 医嘱名称
    /// </summary>
    [Comment("医嘱名称")]
    [Required, StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// 每次剂量(急诊的)
    /// </summary>
    [Comment("每次剂量（急诊的）")]
    [Required]
    public decimal DosageQty { get; set; }

    /// <summary>
    /// 默认规格剂量
    /// </summary>
    [Comment("默认规格剂量")]
    public decimal DefaultDosageQty { get; set; }

    /// <summary>
    /// 剂量单位（急诊的剂量单位，可能单位不固定）
    /// </summary>
    [Comment("剂量单位（急诊的）")]
    [Required, StringLength(20)]
    public string DosageUnit { get; set; }

    /// <summary>
    /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    /// </summary>
    [Comment("门诊拆分属性 0=最小单位总量取整 1=包装单位总量取整 2=最小单位每次取整 3=包装单位每次取整 4=最小单位可拆分")]
    public EMedicineUnPack Unpack { get; set; }

    /// <summary>
    /// 包装价格
    /// </summary>
    [Comment("包装价格")]
    public decimal BigPackPrice { get; set; }

    /// <summary>
    /// 大包装系数(拆零系数)
    /// </summary>
    [Comment("大包装系数(拆零系数)")]
    public int BigPackFactor { get; set; }

    /// <summary>
    /// 包装单位
    /// </summary>
    [Comment("包装单位")]
    [StringLength(20)]
    public string BigPackUnit { get; set; }

    /// <summary>
    /// 小包装单价
    /// </summary>
    [Comment("小包装单价")]
    public decimal SmallPackPrice { get; set; }

    /// <summary>
    /// 小包装单位
    /// </summary> 
    [Comment("小包装单位")]
    [StringLength(20)]
    public string SmallPackUnit { get; set; }

    /// <summary>
    /// 小包装系数(拆零系数)
    /// </summary>
    [Comment("小包装系数(拆零系数)")]
    public int SmallPackFactor { get; set; }

    /// <summary>
    /// 包装规格
    /// </summary>
    [Comment("包装规格")]
    [StringLength(200)]
    public string Specification { get; set; }

    /// <summary>
    /// 更新实体
    /// </summary>
    public void Update(
        string name,
        decimal dosageQty, string dosageUnit, decimal defaultDosageQty,
        EMedicineUnPack unpack,
        int bigPackFactor, decimal bigPackPrice, string bigPackUnit,
        int smallPackFactor, decimal smallPackPrice, string smallPackUnit,
        string specification)
    {
        Name = name;
        DosageQty = dosageQty;
        DosageUnit = dosageUnit;
        DefaultDosageQty = defaultDosageQty;
        Unpack = unpack;
        BigPackFactor = bigPackFactor;
        BigPackPrice = bigPackPrice;
        BigPackUnit = bigPackUnit;
        SmallPackFactor = smallPackFactor;
        SmallPackPrice = smallPackPrice;
        SmallPackUnit = smallPackUnit;
        Specification = specification;

    }


}
