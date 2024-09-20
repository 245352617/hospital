using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 精简的药品信息
/// </summary>
public class HisMedicineSample: IEntity
{  
    /// <summary>
    /// 药品编码
    /// </summary>
    // [Required]  
    public decimal MedicineCode { get; set; }
     
    /// <summary>
    /// 药品名称
    /// </summary>
    // [Required]
    // [StringLength(200)]
    [Comment("药品名称")]
    public string MedicineName{get;set; }
      
    ///// <summary>
    ///// 急救药
    ///// </summary>
    //[Comment("急救药")]
    public int IsFirstAid { get; set; }
      
    ///// <summary>
    ///// 药房代码
    ///// </summary>
    //[StringLength(20)]
    [Comment("药房代码")]
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 药房
    /// </summary>
    // [StringLength(20)]
    [Comment("药房")]
    public string PharmacyName { get; set; }
       
    ///// <summary>
    ///// 库存记录唯一ID 
    ///// </summary>
    //[StringLength(20)]
    //[Comment("库存记录唯一ID ")]
    [Key]
    public decimal InvId { get; set; }

    ///// <summary>
    ///// （急诊处方标志）1.急诊 0.普通 
    ///// </summary>
    [Comment("（急诊处方标志）1.急诊 0.普通")]
    public decimal EmergencySign { get; set; }

    public object[] GetKeys()
    {
        return new object[] { InvId };
    }
}