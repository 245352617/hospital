namespace YiJian.EMR.Writes.Dto
{ 
    /// <summary>
    /// 同一个就诊流水号下的电子病历记录
    /// </summary>
    public class EmrVisitSerialDto
    {
        /// <summary>
        /// 同一个就诊流水号下的电子病历记录
        /// </summary>
        /// <param name="emrTitle"></param>
        /// <param name="pdf"></param>
        /// <param name="patientName"></param>
        /// <param name="doctorName"></param>
        public EmrVisitSerialDto(string emrTitle, string pdf, string patientName, string doctorName)
        {
            EmrTitle = emrTitle;
            Pdf = pdf;
            PatientName = patientName;
            DoctorName = doctorName;
        }

        /// <summary>
        /// 病历名称
        /// </summary> 
        public string EmrTitle { get; set; }

        /// <summary>
        /// 合并的PDF路径
        /// </summary>
        public string Pdf { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        public string PatientName { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

    } 

}
