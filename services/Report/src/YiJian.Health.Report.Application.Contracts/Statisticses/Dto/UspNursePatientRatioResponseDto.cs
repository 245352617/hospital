namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 执行护士汇总信息
    /// </summary>
    public class UspNursePatientRatioResponseDto
    {
        /// <summary>
        /// 医生编码
        /// </summary>  
        public string NurseCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 患者总数
        /// </summary>
        public int ReceptionTota { get; set; }

        /// <summary>
        /// 行数
        /// </summary> 
        public int Row { get; set; }
    }

}
