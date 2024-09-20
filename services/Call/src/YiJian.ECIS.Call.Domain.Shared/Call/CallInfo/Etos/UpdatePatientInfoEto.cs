namespace YiJian.ECIS.Call.Etos
{
    /// <summary>
    /// 更新患者信息推送队列Dto
    /// </summary>
    public class UpdatePatientInfoEto
    {
        /// <summary>
        /// 患者分诊基本信息Id
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }
    
    }
}
