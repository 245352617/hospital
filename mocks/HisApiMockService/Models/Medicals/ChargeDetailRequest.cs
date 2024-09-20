using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 处方明细节点
/// </summary>
[Table("ChargeDetail")]
public class ChargeDetailRequest
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 药品编号
    /// </summary>
    public string? DrugCode { get; set; }

    /// <summary>
    /// 处方明细序号
    /// </summary>
    public string? ChargeDetailNo { get; set; }

    /// <summary>
    /// 药品产地id
    /// <![CDATA[
    /// 4.3.1 药品库存信息查询（his提供）  firmID
    /// ]]>
    /// </summary>
    public string? FirmID { get; set; }

    /// <summary>
    /// 药品数量
    /// </summary>
    public decimal DrugQuantity { get; set; }

    /// <summary>
    /// 药品单价
    /// </summary>
    public decimal DrugPrice { get; set; }

    /// <summary>
    /// 药品总金额
    /// </summary>
    public decimal DrugTotamount { get; set; }

    /// <summary>
    /// 给药途径
    /// <![CDATA[
    /// 4.4.12 药品信息查询（HIS提供） drugChannel
    /// ]]>
    /// </summary>
    public string? DrugChannel { get; set; }

    /// <summary>
    /// 药品用法
    /// <![CDATA[
    /// 4.4.10药品用法字典（his提供） drugUsageDic
    /// ]]>
    /// </summary>
    public string? DrugUsageDic { get; set; }

    /// <summary>
    /// 药品组号
    /// <![CDATA[
    /// 从1开始排列、同组组号相同
    /// ]]>
    /// </summary>
    public string? DrugGroupNo { get; set; }

    /// <summary>
    /// 药房规格
    /// <![CDATA[
    /// 4.3.1 药品库存信息查询（his提供）  pharSpec
    /// ]]>
    /// </summary>
    public string? PharSpec { get; set; }

    /// <summary>
    /// 药品单位
    /// <![CDATA[
    /// 4.3.1 药品库存信息查询（his提供） PharmUnit
    /// ]]>
    /// </summary>
    public string? PharmUnit { get; set; }

    /// <summary>
    /// 药房包装
    /// <![CDATA[
    /// 4.3.1 药品库存信息查询（his提供） packageAmount
    /// ]]>
    /// </summary>
    public string? PackageAmount { get; set; }

    /// <summary>
    /// 0.不需要皮试  1.需要皮试
    /// </summary>
    public int SkinTest { get; set; }

    /// <summary>
    /// 每日次数
    /// <![CDATA[
    /// 4.4.11药品频次（his提供） dailyFrequency
    /// ]]>
    /// </summary>
    public string? DailyFrequency { get; set; }

    /// <summary>
    /// 一次剂
    /// <![CDATA[
    /// 中草药默认：1  
    /// ]]>
    /// </summary>
    public string? PrimaryDose { get; set; }

    /// <summary>
    /// 限制标志
    /// </summary>
    public ERestrictedDrugs RestrictedDrugs { get; set; }

    /// <summary>
    /// 备注信息
    /// </summary>
    public string? Remarks { get; set; }
     
}
