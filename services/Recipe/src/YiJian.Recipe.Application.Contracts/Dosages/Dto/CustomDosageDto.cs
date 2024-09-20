using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.Dosages.Dto;

/// <summary>
/// 急诊自定义的一次剂量规则，里面的数据请根据实际情况配置好
/// </summary>
public class CustomDosageDto : EntityDto<int?>
{
    /// <summary>
    /// 医嘱编码
    /// </summary> 
    [Required, StringLength(20)]
    public string Code { get; set; }

    /// <summary>
    /// 医嘱名称
    /// </summary> 
    [Required, StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// 每次剂量(急诊的)
    /// </summary> 
    [Required]
    public decimal DosageQty { get; set; }

    /// <summary>
    /// 默认规格剂量
    /// </summary> 
    public decimal DefaultDosageQty { get; set; }

    /// <summary>
    /// 剂量单位（急诊的剂量单位，可能单位不固定）
    /// </summary> 
    [Required, StringLength(20)]
    public string DosageUnit { get; set; }

    /// <summary>
    /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    /// </summary> 
    public EMedicineUnPack Unpack { get; set; }

    /// <summary>
    /// 包装价格
    /// </summary> 
    public decimal BigPackPrice { get; set; }

    /// <summary>
    /// 大包装系数(拆零系数)
    /// </summary> 
    public int BigPackFactor { get; set; }

    /// <summary>
    /// 包装单位
    /// </summary> 
    [StringLength(20)]
    public string BigPackUnit { get; set; }

    /// <summary>
    /// 小包装单价
    /// </summary> 
    public decimal SmallPackPrice { get; set; }

    /// <summary>
    /// 小包装单位
    /// </summary>  
    [StringLength(20)]
    public string SmallPackUnit { get; set; }

    /// <summary>
    /// 小包装系数(拆零系数)
    /// </summary> 
    public int SmallPackFactor { get; set; }

    /// <summary>
    /// 包装规格
    /// </summary> 
    [StringLength(200)]
    public string Specification { get; set; }


    /// <summary>
    /// 设置提交给HIS的一次领量
    /// </summary> 
    /// <param name="dosageQty">录入的一次剂量</param>
    /// <param name="unit">录入的一次剂量单位</param>
    /// <returns></returns>
    public decimal GetHisDosageQty(decimal dosageQty, string unit)
    {
        //急诊的一次剂量单位和HIS的一次剂量一致
        if (DosageUnit == unit)
        {
            return dosageQty;
        }

        switch (Unpack)
        {
            //Unpack = 0
            case EMedicineUnPack.RoundByMinUnitAmount:
            //Unpack = 1
            case EMedicineUnPack.RoundByPackUnitAmount:
                {
                    if (unit == DosageUnit)
                    {
                        return Math.Round(dosageQty * DosageQty, 3);
                    }
                    else if (unit == SmallPackUnit)
                    {
                        return Math.Round(dosageQty * SmallPackFactor * DosageQty, 3);
                    }
                    else if (unit == BigPackUnit)
                    {
                        return Math.Round(dosageQty * BigPackFactor * DosageQty, 3);
                    }
                }
                break;
            //Unpack = 2
            case EMedicineUnPack.RoundByMinUnitTime:
            //Unpack = 3
            case EMedicineUnPack.RoundByPackUnitTime:
                {
                    if (unit == DosageUnit)
                    {
                        return Math.Round(dosageQty * DosageQty, 3);
                    }
                    else if (unit == SmallPackUnit)
                    {
                        return Math.Round(dosageQty * (BigPackFactor * DosageQty), 3);
                    }
                    else if (unit == BigPackUnit)
                    {
                        return Math.Round(dosageQty * (SmallPackFactor * DosageQty), 3);
                    }
                }
                break;
            default:
                break;
        }
        return DosageQty;

    }
}
