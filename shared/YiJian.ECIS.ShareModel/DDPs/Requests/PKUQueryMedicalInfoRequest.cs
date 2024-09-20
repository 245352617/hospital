namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 医嘱状态查询
/// </summary>
public class PKUQueryMedicalInfoRequest
{
    /// <summary>
    /// 医嘱状态查询
    /// </summary>
    /// <param name="queryType"></param>
    /// <param name="visSerialNo"></param>
    /// <param name="mzBillId"></param>
    public PKUQueryMedicalInfoRequest(int queryType, string visSerialNo, string mzBillId)
    {
        QueryType = queryType;
        VisSerialNo = visSerialNo;
        MzBillId = mzBillId;
    }

    /// <summary>
    /// 查询类型 1.查询指定信息  0.查询所有信息
    /// </summary>
    public int QueryType { get; set; }

    /// <summary>
    /// 就诊流水号 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
    /// </summary>
    public string VisSerialNo { get; set; }

    /// <summary>
    /// 单据号 单据前面增加，0处方:CF 非处方:YJ 多个以逗号隔开  CF102101,YJ102012 queryType = 0 可为空 4.5.3医嘱信息回传（his提供、需对接集成平台） prescriptionNo, projectItemNo
    /// </summary>
    public string MzBillId { get; set; }

}
