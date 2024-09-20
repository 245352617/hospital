namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱详细模型(只修改单条记录)
    /// </summary>
    public class AdviceDetailDto
    {
        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 医嘱主表内容
        /// </summary>
        public ModifyDoctorsAdviceBaseDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        public PacsDto Pacs { get; set; }

        /// <summary>
        /// 检验
        /// </summary>
        public LisDto Lis { get; set; }

        /// <summary>
        /// 药方
        /// </summary>
        public PrescribeDto Prescribe { get; set; }

        /// <summary>
        /// 诊疗
        /// </summary>
        public TreatDto Treat { get; set; }

        /// <summary>
        /// 如果是急诊平台，诊疗项的频次设为null
        /// </summary>
        public void SetTreatFrequencyNull()
        {
            if (DoctorsAdvice.PlatformType == ECIS.ShareModel.Enums.EPlatformType.EmergencyTreatment)
            {
                if (Treat != null)
                {
                    Treat.FrequencyCode = "";
                    Treat.FrequencyName = "";
                }
            }
        }

    }

}
