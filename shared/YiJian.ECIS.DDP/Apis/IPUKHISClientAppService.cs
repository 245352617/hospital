using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.HisDto;

namespace YiJian.Apis;
/// <summary>
/// 对接深圳北大医院HIS 检验检查去重等接口
/// </summary>
public interface IPUKHISClientAppService
{
    /// <summary> 
    /// 检查项目重复校验   /API/HisLis/Check_PacsXmcf
    /// </summary>
    /// <returns></returns> 
    public Task<CheckPacsXmcfResponseDto> CheckPacsXmcfAsync(CheckPacsXmcfRequestDto request);
    /// <summary>
    /// 检验项目重复校验   /API/HisLis/Check_lisxmcf
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<CheckLisXmcfResponseDto> CheckLisXmcfAsync(CheckLisXmcfRequestDto request);
}
