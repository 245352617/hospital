using BeetleX.Http.Clients;
using YiJian.ECIS.ShareModel.DDPs;

namespace YiJian.ECIS.DDP;

/// <summary>
/// Ddp 客户端调用 基类Api
/// </summary>
public class DdpBasicApi
{
    private readonly DdpHospital _ddpHospital;

    /// <summary>
    /// Ddp 客户端调用 基类Api
    /// </summary>
    public DdpBasicApi(DdpHospital ddpHospital)
    {
        _ddpHospital = ddpHospital;
    }

    /// <summary>
    /// 构建代理服务
    /// </summary>
    /// <returns></returns>
    public DdpApiClient Builder()
    {
        HttpCluster httpCluster = new HttpCluster();
        httpCluster.DefaultNode.Add(_ddpHospital.DdpHost);
        return httpCluster.Create<DdpApiClient>();
    }


    public PlatformClient PlatformBuilder()
    {
        HttpCluster httpCluster = new HttpCluster();
        httpCluster.DefaultNode.Add(_ddpHospital.PlatformHost);
        return httpCluster.Create<PlatformClient>();
    }
}
