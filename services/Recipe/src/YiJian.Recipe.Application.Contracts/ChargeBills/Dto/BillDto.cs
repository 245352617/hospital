namespace YiJian.ChargeBills.Dto
{
    /// <summary>
    /// 急诊收款单
    /// </summary>
    public class BillDto
    {
        /// <summary>
        /// 记录总数（共：XX条）
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 未提交的记录数(未提交：XX条)
        /// </summary>
        public int NoSubmitRowCount { get; set; }

        /// <summary>
        /// 已提交记录数(已提交：XX条)
        /// </summary>
        public int SubmitRowCount { get; set; }

        /// <summary>
        /// 已执行记录数(已执行:XX条)
        /// </summary>
        public int ExecRowCount { get; set; }

        /// <summary>
        /// 停嘱记录数(停嘱：XX条)
        /// </summary>
        public int StopRowCount { get; set; }

        /// <summary>
        /// 已作废记录数(作废：XX条)
        /// </summary>
        public int ObsRowCount { get; set; }

        /// <summary>
        /// 未提交医嘱金额（未提交：医嘱金额￥XXX.XX）
        /// </summary>
        public decimal NoSubmitAmount { get; set; }

        /// <summary>
        /// 未提交耗材金额（耗材：￥XXX.XX）
        /// </summary>
        public decimal NoSubmitConsumablesAmount { get; set; }

        /// <summary>
        /// 已提交金额（已提交：医嘱金额：XXX.XX）
        /// </summary>
        public decimal SubmitAmount { get; set; }

        /// <summary>
        /// 已提交耗材金额（耗材：￥XXX.XX）
        /// </summary>
        public decimal SubmitConsumablesAmount { get; set; }

    }
}
