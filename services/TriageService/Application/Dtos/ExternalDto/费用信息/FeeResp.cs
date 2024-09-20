namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 费用信息Dto
    /// </summary>
    public class FeeResp
    {
        /// <summary>
        /// 费用类别
        /// </summary>
        public string commentType { get; set; }
            
        /// <summary>
        /// 费用金额
        /// </summary>
        public string comment { get; set; }
    }
}