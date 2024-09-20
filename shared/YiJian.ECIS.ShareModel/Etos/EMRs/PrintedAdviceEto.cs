namespace YiJian.ECIS.ShareModel.Etos.EMRs
{
    /// <summary>
    /// 标记上所有的电子病历为打印过的病历
    /// </summary>
    public class PrintedAdviceEto
    {
        /// <summary>
        /// piid
        /// </summary>
        public Guid Piid { get; set; }

        /// <summary>
        /// 医生code
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }
    }
}