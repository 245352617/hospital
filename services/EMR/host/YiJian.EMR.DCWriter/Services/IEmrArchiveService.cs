using System.Threading.Tasks;
using YiJian.EMR.DCWriter.Models;

namespace YiJian.EMR.DCWriter.Services;

/// <summary>
/// 电子病历，护理文书归档服务
/// </summary>
public interface IEmrArchiveService
{
    void SavePDF();


}

public interface IMasterRemoteervice
{

    /// <summary>
    /// 获取电子病历水印配置信息
    /// </summary>
    /// <returns></returns>
    public Task<EmrWatermarkDto> GetEmrWatermarkingAsync();
}
