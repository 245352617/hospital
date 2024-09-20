using System;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Writes.Dto;

/// <summary>
/// 病例痕迹
/// </summary>
public class XmlHistoryTraceDto : EntityDto<Guid>
{

    /// <summary>
    /// 患者病历，这个是需要修复的病历XMLID
    /// </summary>  
    public Guid XmlId { get; set; }

    /// <summary>
    /// 病历标题
    /// </summary>
    public string EmrTitle { get; set; }

    /// <summary>
    /// 医生编号
    /// </summary> 
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生名称
    /// </summary> 
    public string DoctorName { get; set; }

    /// <summary>
    /// 留痕时间
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 痕迹病历ID（如果需要替换的时候这个要传递回来）
    /// </summary>
    public Guid TraceId { get; set; }

}

