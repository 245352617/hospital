using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.Call.CallConfig.Dtos;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 列表设置
    /// </summary>
    public interface ITableConfigAppService : IApplicationService
    {
        /// <summary>
        /// 获取列表配置
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RowConfigDto>> GetRowConfigsAsync();

        /// <summary>
        /// 修改保存列表配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SaveRowConfigsAsync(RowConfigUpdate input);

        /// <summary>
        /// 重置列表配置
        /// </summary>
        /// <returns></returns>
        Task ResetAsync();
    }
}
