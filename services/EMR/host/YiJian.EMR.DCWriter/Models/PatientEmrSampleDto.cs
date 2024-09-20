using System;

namespace YiJian.EMR.DCWriter.Models
{
    /// <summary>
    /// 患者电子病历信息
    /// </summary>
    public class PatientEmrSampleDto
    {
        /// <summary>
        /// patientemrid
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 病历名称
        /// </summary>   
        public string Title { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>  
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>   
        public string PatientName { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）
        /// </summary>
        public int Classify { get; set; }
    }
}
