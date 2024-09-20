using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.ECIS.ShareModel.Models;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ExtraFullAuditedAggregateRoot<T> : FullAuditedAggregateRoot<T>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    protected ExtraFullAuditedAggregateRoot()
    {

    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id"></param>
    protected ExtraFullAuditedAggregateRoot(T id) : base(id)
    {

    }
    /// <summary>
    /// 是否院前使用  1：是 0：否
    /// </summary>
    public bool? IsProHospitalUse { get; set; }

    /// <summary>
    /// 医院编码
    /// </summary>
    [StringLength(20)]
    public string HospitalCode { get; set; }

    /// <summary>
    /// 医院名称
    /// </summary>
    [StringLength(100)]
    public string HospitalName { get; set; }

}
