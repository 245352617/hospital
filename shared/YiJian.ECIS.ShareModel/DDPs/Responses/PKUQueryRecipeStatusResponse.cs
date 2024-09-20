namespace YiJian.ECIS.ShareModel.DDPs.Responses
{
    /// <summary>
    /// 描    述:查询his医嘱状态返回
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/11 16:27:38
    /// </summary>
    public class PKUQueryRecipeStatusResponse
    {
        /// <summary>
        /// 票据号码
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 状态说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 缴费日期
        /// </summary>
        public string ChargeDate { get; set; }
    }
}
