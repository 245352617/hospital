using Volo.Abp.EventBus;

namespace YiJian.Hospitals.Eto
{
    /// <summary>
    /// HIS医嘱状态变更推送或调用接口参数
    /// <code>
    /// {
    ///   "visSerialNo": "6011439",
    ///   "mzBillId": "C10042487,Y10042488",
    ///   "billState": "1",
    ///   "invoiceNo": "ZZ12463275"
    /// }
    /// </code>
    /// </summary>
    [EventName("BillStatus")]
    public class HisChannelBillStatusHanderEto
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// HIS的编号
        /// <![CDATA["CF10042487,YJ10042488"]]>
        /// </summary>
        public string MzBillId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string BillState { get; set; }

        /// <summary>
        /// 票据号码
        /// </summary>
        public string InvoiceNo { get; set; }

    }
}

