using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 基础设置 仓储接口
    /// </summary>
    public interface IBaseConfigRepository : IRepository<BaseConfig, int>
    {
        /// <summary>
        /// 获取最新的配置信息
        /// </summary>
        /// <returns></returns>
        Task<BaseConfig> GetLastConfigAsync();
    }
}
