using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.Writes.Entities;

/// <summary>
/// Minio对象存储采集表(采集Minio的表)
/// </summary>
[Comment("Minio对象存储采集表")]
public class MinioEmrInfo : FullAuditedAggregateRoot<Guid>
{
    private MinioEmrInfo()
    {
        
    }

    /// <summary>
    /// Minio对象存储采集表
    /// </summary> 
    public MinioEmrInfo(
        Guid id,
        Guid patientEmrId,
        string emrTitle,
        string minioUrl,
        Guid pI_ID,
        string patientNo,
        string patientName,
        string doctorCode,
        string doctorName)
    {
        Id = id;
        PatientEmrId = patientEmrId;
        EmrTitle = emrTitle;
        MinioUrl = minioUrl;
        PI_ID = pI_ID;
        PatientNo = patientNo;
        PatientName = patientName;
        DoctorCode = doctorCode;
        DoctorName = doctorName;
    }


    /// <summary>
    /// 病历Id
    /// </summary>
    public Guid PatientEmrId { get; set; }

    /// <summary>
    /// 病历名称
    /// </summary>
    [Comment("病历名称")]
    [StringLength(500)]
    public string EmrTitle { get; set; }

    /// <summary>
    /// Minio对象存储的临时URL路径
    /// </summary>
    [StringLength(1000)]
    public string MinioUrl { get; set; }

    /// <summary>
    /// 患者唯一Id
    /// </summary>
    public Guid PI_ID { get; set; }
     
    /// <summary>
    /// 患者编号
    /// </summary>
    [Comment("患者编号")]
    [StringLength(50)]
    public string PatientNo { get; set; }

    /// <summary>
    /// 患者名称
    /// </summary>
    [Comment("患者名称")]
    [StringLength(50)]
    public string PatientName { get; set; }

    /// <summary>
    /// 医生编号
    /// </summary>
    [Comment("医生编号")]
    [StringLength(50)]
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生名称
    /// </summary>
    [Comment("医生名称")]
    [StringLength(50)] 
    public string DoctorName { get; set; }
     
}


/// <summary>
/// Minio对象存储采集表(采集Minio的表)
/// </summary> 
public class AddMinioEmrInfo
{
    /// <summary>
    /// 病历Id
    /// </summary>
    public Guid PatientEmrId { get; set; }

    /// <summary>
    /// 病历名称
    /// </summary>
    [Comment("病历名称")]
    [StringLength(500)]
    public string EmrTitle { get; set; }
     
    /// <summary>
    /// 患者唯一Id
    /// </summary>
    public Guid PI_ID { get; set; }
      
    /// <summary>
    /// 患者编号
    /// </summary>
    [Comment("患者编号")]
    [StringLength(50)]
    public string PatientNo { get; set; }

    /// <summary>
    /// 患者名称
    /// </summary>
    [Comment("患者名称")]
    [StringLength(50)]
    public string PatientName { get; set; }

    /// <summary>
    /// 医生编号
    /// </summary>
    [Comment("医生编号")]
    [StringLength(50)]
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生名称
    /// </summary>
    [Comment("医生名称")]
    [StringLength(50)]
    public string DoctorName { get; set; }

}
