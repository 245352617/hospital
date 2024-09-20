namespace YiJian.ECIS.ShareModel.DDPs.Responses;

/// <summary>
/// 医嘱信息回传
/// </summary>
public class DdpSendMedicalInfoResponse
{
    public string MedicalNo { get; set; }

    public List<MedDetail> medDetail { get; set; }

}

public class MedDetail
{
    public string ChannelNumber { get; set; }

    public string ChannelNo { get; set; }

    public string HisNumber { get; set; }
}
