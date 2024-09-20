using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using YiJian.ECIS.ShareModel.HisDto;
using YiJian.Apis;

namespace YiJian.MasterData.MasterData.PUKClient
{
    /// <summary>
    /// 对接深圳北大医院 检验检查去重等接口
    /// </summary>
    public class PUKClientAppService : MasterDataAppService, IPUKClientAppService
    {
        //private string _url { get; set; }
    
        private IPUKHISClientAppService _hisService;
        /// <summary>
        /// 对接深圳北大医院 检验检查去重等接口
        /// </summary>
        /// <param name="configuration"></param>
        public PUKClientAppService(IConfiguration configuration)
        {
            _hisService = new PUKHISClientAppService(configuration);
            //_url = configuration["HisUrl:LongGangHospital"];
        }

        /// <summary>
        /// 检查项目重复校验
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CheckPacsXmcfResponseDto> CheckPacsXmcfAsync([FromBody] CheckPacsXmcfRequestDto request)
        {
            return await _hisService.CheckPacsXmcfAsync(request);
        }

        /// <summary>
        /// 检验项目重复校验
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CheckLisXmcfResponseDto> CheckLisXmcfAsync([FromBody] CheckLisXmcfRequestDto request)
        {
            return await _hisService.CheckLisXmcfAsync(request);
        }

    }
}
