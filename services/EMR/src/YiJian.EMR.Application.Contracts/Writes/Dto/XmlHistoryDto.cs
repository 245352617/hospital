using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Writes.Dto;

/// <summary>
/// 电子病历留痕历史记录
/// </summary>
public class XmlHistoryDto : EntityDto<Guid>
{ 
    /// <summary>
    /// 留痕时间
    /// </summary>
    public DateTime CreationTime { get;set; }

    /// <summary>
    /// 患者电子病历的XML文档Id
    /// </summary> 
    [Required(ErrorMessage = "患者电子病历的XML文档Id不能为空")]
    public Guid XmlId { get; set; }


    /// <summary>
    /// xml 电子病例模板类型(0=电子病历库的模板，1=我的电子病历模板的模板，2=已存档的患者电子病历)
    /// </summary>
    [Required(ErrorMessage = "电子病历留痕的Xml文档不能为空")]
    public EXmlCategory XmlCategory { get; set; }
     
    /// <summary>
    /// 医生编号
    /// </summary> 
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生名称
    /// </summary> 
    public string DoctorName { get; set; }

}

