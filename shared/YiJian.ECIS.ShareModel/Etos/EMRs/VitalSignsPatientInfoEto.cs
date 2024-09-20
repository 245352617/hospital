namespace YiJian.ECIS.ShareModel.Etos.EMRs
{
    /// <summary>
    /// 生命体征患者信息
    /// </summary>
    public class VitalSignsPatientInfoEto
    {
        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactsPhone { get; set; }

    }
}