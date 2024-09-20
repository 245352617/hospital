using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.EMRs
{

    /// <summary>
    /// 会诊记录绑定
    /// </summary>
    public class ConsultationRecordBindEto
    {
        /// <summary>
        /// 就诊号
        /// </summary>  
        public string VisitNo { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>  
        public string RegisterSerialNo { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>  
        public string OrgCode { get; set; }

        /// <summary>
        /// 患者唯一Id
        /// </summary> 
        [Required]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>  
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>  
        public string PatientName { get; set; }

        /// <summary>
        /// 录入人Id
        /// </summary>  
        public string WriterId { get; set; }

        /// <summary>
        /// 录入人名称
        /// </summary>  
        public string WriterName { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）默认电子病历
        /// </summary>  
        public int Classify { get; set; } = 1;

        /// <summary>
        /// 患者电子病历Id
        /// </summary>  
        public Guid? PatientEmrId { get; set; }

        /// <summary>
        /// 绑定的数据类型, 第一层key是datasource ,第二层key是path
        /// <code>
        /// //实例如下： 第一层如 patient, doctor;第二层 如 patientId, patientName,age,doctorId,doctorName ...
        /// {
        ///     "patient":{
        ///         "patientId":"1000001",
        ///         "patientName":"张三",
        ///         "age":"20"
        ///     },
        ///     "doctor":{
        ///         "doctorId":"0000001",
        ///         "doctorName":"张大大"
        ///     },
        ///     ...
        /// }
        /// </code>
        /// </summary>
        [Required]
        public Dictionary<string, Dictionary<string, object>> Data { get; set; } = new Dictionary<string, Dictionary<string, object>>();

    }
}