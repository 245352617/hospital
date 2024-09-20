namespace YiJian.BodyParts.Application.Contracts.Dtos.Patient
{
    public class Pat360UrlPara
    {

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病人档案号
        /// </summary>
        public string ArchiveId { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string DoctorId { get; set; }
    }
}