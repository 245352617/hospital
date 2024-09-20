namespace YiJian.DoctorsAdvices.Dto;

/// <summary>
/// HIS医嘱状态变更推送或调用接口参数
/// <code>
/// {
///   "visSerialNo": "6011439",
///   "mzBillId": "C10042487,Y10042488",
///   "billState": "1"
/// }
/// </code>
/// </summary>
public class HisChannelBillStatusDto
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
    public int BillState { get; set; }

}

