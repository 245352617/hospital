using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 患者的电子病历xml文档
    /// </summary>
    public class PatientEmrXmlDto : EntityDto<Guid>
    {
        /// <summary>
        /// 电子病历Xml文档
        /// </summary> 
        public string EmrXml { get; set; }

        /// <summary>
        /// 患者电子病历Id
        /// </summary> 
        public Guid PatientEmrId { get; set; }
         
        /// <summary>
        /// 原电子病历模板Id
        /// </summary>
        public Guid OriginalId { get; set; }

        /// <summary>
        /// 电子病历/文书 绑定的数据
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Data { get;set;} = new Dictionary<string, Dictionary<string, string>>();
    }

    /// <summary>
    /// 患者的电子病历xml文档
    /// </summary>
    public class PatientEmrXmTraceDto
    {
        /// <summary>
        /// 电子病历Xml文档
        /// </summary> 
        public string EmrXml { get; set; }

        /// <summary>
        /// 患者电子病历Id
        /// </summary> 
        public Guid PatientEmrId { get; set; }
        
        /// <summary>
        /// 电子病历/文书 绑定的数据
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Data { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }

}
