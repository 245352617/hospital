using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.HisDto;

namespace YiJian.MasterData.MasterData.PUKClient
{
    /// <summary>
    /// 对接深圳北大医院 检验检查去重等接口
    /// </summary>
    public interface IPUKClientAppService : IApplicationService
    {
        /// <summary> 
        /// 检查项目重复校验   /API/HisLis/Check_PacsXmcf
        /// </summary>
        /// <returns></returns> 
        public  Task<CheckPacsXmcfResponseDto> CheckPacsXmcfAsync(CheckPacsXmcfRequestDto request);
        /// <summary>
        /// 检查项目重复校验   /API/HisLis/Check_lisxmcf
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<CheckLisXmcfResponseDto> CheckLisXmcfAsync(CheckLisXmcfRequestDto request);
    }
}
