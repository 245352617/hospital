namespace YiJian.ECIS.ShareModel.DDPs.Requests
{
    /// <summary>
    /// 北京大学 门口屏显示叫号接口
    /// </summary>
    public class PKUDReferralOperation
    {
        /// <summary>
        /// 科室名
        /// </summary>
        public string clinicName { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public string doctorID { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string patientID { get; set; }
        /// <summary>
        /// IpAddr
        /// </summary>
        public string ipAddress { get; set; }
    }
}
