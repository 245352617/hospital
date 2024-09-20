namespace YiJian.MasterData.MasterData.Users;

/// <summary>
/// 远程服务配置BaseAddress
/// </summary>
public class RemoteServices
{
    /// <summary>
    /// 认证服务BaseAddress
    /// </summary>
    public RemoteBaseAddress Identity { get; set; }

}

public class RemoteBaseAddress
{
    public string BaseUrl { get; set; }
}
