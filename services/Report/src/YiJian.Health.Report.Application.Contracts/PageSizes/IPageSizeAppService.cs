using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Responses;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 纸张大小设置Api
    /// </summary>
    public interface IPageSizeAppService : IApplicationService
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResponseBase<bool>> SaveAsync(PageSizeDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseBase<bool>> DeleteAsync(Guid id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<ResponseBase<List<PageSizeDto>>> GetListAsync();
    }
}