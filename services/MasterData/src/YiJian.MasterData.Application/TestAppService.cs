using System.Threading.Tasks;

namespace YiJian.MasterData;

/// <summary>
/// 测试服务
/// </summary>
public class TestAppService: MasterDataAppService
{
    /// <summary>
    /// Test方法
    /// </summary>
    /// <returns></returns>
    public async Task GetTestAsync()
    {
        await Task.CompletedTask;
    }
}
