namespace YiJian.ECIS.ShareModel.Etos.Pdas
{
    /// <summary>
    /// 描述：pda执行单状态同步eto
    /// 创建人： yangkai
    /// 创建时间：2022/11/28 13:52:16
    /// </summary>
    public class PdaExecuteStatusEto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PatientId { get; set; } = string.Empty;

        /// <summary>
        /// 患者住院流水号
        /// </summary>
        public string PatientNo { get; set; } = string.Empty;

        /// <summary>
        /// 医嘱组号
        /// </summary>
        public string PlacerGroupNumber { get; set; } = string.Empty;

        /// <summary>
        /// 医嘱组内序号
        /// </summary>
        public string PlacerOrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// 预计执行时间 格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime PlanExecTime { get; set; }

        /// <summary>
        /// 医嘱状态 1：待执行 15：待核对 
        /// </summary>
        public string OrderStatus { get; set; } = string.Empty;

        /// <summary>
        /// 核对人
        /// </summary>
        public string CheckBeforeNurseCode { get; set; } = string.Empty;

        /// <summary>
        /// 核对人
        /// </summary>
        public string CheckBeforeNurseName { get; set; } = string.Empty;

        /// <summary>
        /// 核对时间 格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime? CheckBeforeTime { get; set; }
    }
}
