namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 南方医绿通挂号支付
    /// </summary>
    public class HisRegFeeEntity
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string TransID { get; set; }

        /// <summary>
        /// 消息时间
        /// </summary>
        public string TransTime { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string TransType { get; set; }

        /// <summary>
        /// 消息来源 ECIS
        /// </summary>
        public string TransSource { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public TransBodyEntity TransBody { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperCode { get; set; }


        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperDate { get; set; }
    }
}