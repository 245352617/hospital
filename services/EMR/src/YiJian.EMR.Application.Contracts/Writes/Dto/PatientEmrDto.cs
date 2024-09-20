using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;

namespace YiJian.EMR.Writes.Dto;

/// <summary>
/// 患者的电子病历
/// </summary>
public class PatientEmrDto : PatientEmrBaseDto
{ 
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime {  get; set; }

    private DateTime? _lastModificationTime { get;set; }
    /// <summary>
    /// 最后更新时间(留痕对比的时候用这个时间)
    /// </summary>
    public DateTime? LastModificationTime
    {
        get
        {
            return _lastModificationTime.HasValue ? _lastModificationTime.Value : CreationTime;
        }
        set
        {
            _lastModificationTime = value;
        }
    }
    /// <summary>
    /// 电子病历的PDF URL地址
    /// </summary>
    public string PdfUrl { get; set; }

} 
