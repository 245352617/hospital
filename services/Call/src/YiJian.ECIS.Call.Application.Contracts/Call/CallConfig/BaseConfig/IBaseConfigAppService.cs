using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.Call.CallConfig.Dtos;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 【叫号设置】应用服务层接口
    /// </summary>
    public interface IBaseConfigAppService : IApplicationService
    {
        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <returns></returns>
        Task<BaseConfigData> GetAsync();

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(BaseConfigUpdate input);
    }
}
