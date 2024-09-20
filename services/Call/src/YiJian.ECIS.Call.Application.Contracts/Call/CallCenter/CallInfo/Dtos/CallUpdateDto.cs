namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 
    /// Directory: input
    /// </summary>
    public class CallUpdateDto
    {
        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DoctorName { get; set; }
    }
}
