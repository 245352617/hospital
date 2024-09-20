using BeetleX.Http.Clients;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.DDPs;

namespace YiJian.ECIS.DDP;

/// <summary>
/// DDP API 客户端
/// </summary>
public interface DdpApiClient
{
    /// <summary>
    /// Ddp 接口对接 调用
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Post(Route = "data/standard-call/handle")]
    public Task<DdpBaseResponse<S>> CallAsync<T, S>(DdpBaseRequest<T> request);

}
