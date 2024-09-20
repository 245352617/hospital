namespace YiJian.ECIS.ShareModel.DDPs;

/// <summary>
/// Ddp适配医院配置
/// </summary>
public class DdpHospital
{
    /// <summary>
    /// DDP 开关，true=使用ddp， false=不使用ddp(龙岗中心医院)
    /// </summary>
    public bool DdpSwitch { get; set; } = false;

    /// <summary>
    /// DDP服务器HOST 
    /// <![CDATA[
    /// eg : http://192.168.0.56:8788/
    /// ]]>
    /// </summary>
    public string DdpHost { get; set; }

    /// <summary>
    /// 另外一个平台的Host
    /// </summary>
    public string PlatformHost { get; set; }
    /// <summary>
    /// 使用的医院 PKU是北京大学深圳医院
    /// </summary>
    public string UseHospital { get; set; }
}
