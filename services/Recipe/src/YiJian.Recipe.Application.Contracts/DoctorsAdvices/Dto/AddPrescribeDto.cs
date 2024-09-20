namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱药品操作
    /// </summary>
    public class AddPrescribeDto
    {
        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        public ModifyDoctorsAdviceBaseDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 开方信息
        /// </summary>
        public PrescribeDto Items { get; set; } = new PrescribeDto();

        /// <summary>
        /// 疫苗接种记录信息，只有当 toxicLevel=7时，传过来
        /// </summary>
        public ImmunizationRecordDto ImmunizationRecord { get; set; }

    }

}
