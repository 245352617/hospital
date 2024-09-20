namespace YiJian.EMR.DCWriter.Models;

/// <summary>
/// 远程服务器
/// </summary>
public class RemoteServices
{
    /// <summary>
    /// 电子病历的BaseAddress
    /// </summary>
    public string Emr { get; set; }

    /// <summary>
    /// 字典服务BaseAddress
    /// </summary>
    public string MasterData { get;set; }

}
