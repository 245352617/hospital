namespace YiJian.ECIS.ShareModel.Etos.EMRs
{

    /// <summary>
    /// 合并留观病程（生命体征）的病历内容
    /// </summary>
    public class MergeCourseOfObservationVitalSignsEto
    {
        /// <summary>
        /// 电子病历分类名称，以后用来做minio的目录, filename=emrtitle + "_" + piid + ".pdf"
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 患者信息
        /// </summary>
        public VitalSignsPatientInfoEto PatientInfo { get; set; }

        /// <summary>
        /// 希望合并的电子病历xml文件集合
        /// </summary>
        public IList<string> EmrXmls { get; set; }

    }
}