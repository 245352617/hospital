namespace YiJian.Recipe
{
    public class OperationApplyStatusSync
    {
        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public string ApplyNum { get; set; }
        /// <summary>
        /// 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
        /// </summary>
        public OperationApplyStatus Status { get; set; }
        /// <summary>
        /// 麻醉医生
        /// </summary>
        public string Anesthesiologist { get; set; }

        /// <summary>
        /// 麻醉助手
        /// </summary>
        public string AnesthesiologistAssistant { get; set; }

        /// <summary>
        /// 巡回护士
        /// </summary>
        public string TourNurse { get; set; }

        /// <summary>
        /// 器械护士
        /// </summary>
        public string InstrumentNurse { get; set; }

        /// <summary>
        /// 麻醉方式编码
        /// </summary>
        public string AnaestheticCode { get; set; }

        /// <summary>
        /// 麻醉方式名称
        /// </summary>
        public string AnaestheticName { get; set; }

        /// <summary>
        /// 手术台次
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 手术时长
        /// </summary>
        public string OperationDuration { get; set; }

        /// <summary>
        /// 手术位置
        /// </summary>
        public string OperationLocation { get; set; }
    }
}
