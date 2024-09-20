using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Stores;

/// <summary>
/// 库存信息
/// </summary>
[Table("DrugStock")]
public class DrugStockQueryResponse
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 药房编码
    /// <![CDATA[
    /// 2.1药房编码码字典（字典、写死）
    /// ]]>
    /// </summary>
    public int Storage { get; set; }

    /// <summary>
    /// 药品编号, 医院药品唯一编码
    /// </summary>
    public string? DrugCode { get; set; }

    /// <summary>
    /// 药品名称
    /// </summary>
    public string? DrugName { get; set; }

    /// <summary>
    /// 药品规格
    /// </summary>
    public string? DrugSpec { get; set; }

    /// <summary>
    /// 包装单位,大/小包装单位 小包装单位=片
    /// </summary>
    public string? MinPackageUnit { get; set; }

    /// <summary>
    /// 厂商id
    /// </summary>
    public string? FirmID { get; set; }

    /// <summary>
    /// 进货价格 元
    /// </summary>
    public decimal PurchasePrice { get; set; }

    /// <summary>
    /// 零售价格 元
    /// </summary>
    public decimal RetailPrice { get; set; }

    /// <summary>
    /// 药房规格, 门急诊药房规格
    /// </summary>
    public string? PharSpec { get; set; }

    /// <summary>
    /// 药品单位, 门急诊药房单位
    /// </summary>
    public string? PharmUnit { get; set; }

    /// <summary>
    /// 包装数量, 大/小包装数量 小包装数量 =（例如一盒有36片）=36
    /// </summary>
    public decimal PackageAmount { get; set; }

    /// <summary>
    /// 包装类型, 1 表示小包装，0表示大包装
    /// </summary>
    public int MinPackageIndicator { get; set; }

    /// <summary>
    /// 最小单位数量 
    /// </summary>
    public int Dosage { get; set; }

    /// <summary>
    /// 最小单位
    /// </summary>
    public string? DosageUnit { get; set; }

    /// <summary>
    /// 药品剂量
    /// </summary>
    public decimal DrugDose { get; set; }

    /// <summary>
    /// 剂量单位
    /// </summary>
    public string? DrugUnit { get; set; }

    /// <summary>
    /// 备注信息
    /// </summary>
    public string? ReturnDesc { get; set; }

    /// <summary>
    /// 库存数量
    /// </summary>
    public decimal Quantity { get; set; }

}

