namespace YiJian.MasterData;

/// <summary>
/// 药品库存出参
/// </summary>
public class StockOutDto
{
    /// <summary>
    /// 药房编码
    /// </summary>
    public string Storage { get; set; }

    /// <summary>
    /// 药品编号
    /// </summary>
    public string DrugCode { get; set; }

    /// <summary>
    /// 药品名称
    /// </summary>
    public string DrugName { get; set; }

    /// <summary>
    /// 药品规格
    /// </summary>
    public string DrugSpec { get; set; }

    /// <summary>
    /// 包装单位	大/小包装单位 小包装单位=片
    /// </summary>
    public string MinPackageUnit { get; set; }
    /// <summary>
    /// 药品单位
    /// </summary>
    public string PharmUnit { get; set; }
    /// <summary>
    /// 大/小包装数量   小包装数量=（例如一盒有36片）=36
    /// </summary>
    public int PackageAmount { get; set; }
    
    /// <summary>
    /// 包装类型	 1 表示小包装，0表示大包装
    /// </summary>
    public int MinPackageIndicator { get; set; }
    
    /// <summary>
    /// 最小单位数量
    /// </summary>
    public int Dosage { get; set; }
    /// <summary>
    /// 最小单位
    /// </summary>
    public string DosageUnit { get; set; }
    /// <summary>
    /// 药品剂量
    /// </summary>
    public string DrugDose { get; set; }
    /// <summary>
    /// 药品单位
    /// </summary>
    public string DrugUnit { get; set; }
    /// <summary>
    /// 备注信息
    /// </summary>
    public string ReturnDesc { get; set; }
    
    /// <summary>
    /// 批号
    /// </summary>
    public string BatchNo { get; set; }

    /// <summary>
    /// 有效期
    /// </summary>
    public string ExpiryDate { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int Quantity { get; set; }
    

    /// <summary>
    /// 零售价
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// 进货价
    /// </summary>
    public double PurchasePrice { get; set; }
    /// <summary>
    /// 零售价格
    /// </summary>
    public double RetailPrice { get; set; }

    /// <summary>
    /// 厂商id
    /// </summary>
    public string FirmID { get; set; }
    
    /// <summary>
    /// 生产厂家
    /// </summary>
    public string Firm { get; set; }
    
}