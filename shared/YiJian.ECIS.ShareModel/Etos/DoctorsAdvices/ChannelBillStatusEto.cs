namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 订单状态变更ETO模型
/// </summary>
public class ChannelBillStatusEto
{
    /// <summary>
    /// 订单状态变更ETO模型
    /// </summary>
    /// <param name="visSerialNo"></param>
    /// <param name="ids"></param>
    public ChannelBillStatusEto(string visSerialNo, List<Guid> ids)
    {
        VisSerialNo = visSerialNo;
        Ids = ids;
    }

    /// <summary>
    /// 就诊流水号
    /// </summary>
    public string VisSerialNo { get; set; }

    /// <summary>
    /// 查询Prescribe Id
    /// </summary>
    public List<Guid> Ids { get; set; }

}

