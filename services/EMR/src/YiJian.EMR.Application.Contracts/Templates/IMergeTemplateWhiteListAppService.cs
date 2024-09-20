using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Templates.Dto;

namespace YiJian.EMR.Templates
{
    /// <summary>
    /// 电子病历合并模板的白名单
    /// </summary>
    public interface IMergeTemplateWhiteListAppService : IApplicationService
    {

        /// <summary>
        /// 获取合并白名单列表记录信息
        /// </summary>
        /// <returns></returns>
        public Task<ResponseBase<IList<MergeTemplateWhiteListDto>>> GetListAsync();

        /// <summary>
        /// 添加新的合并病历白名单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> AddAsync(MergeTemplateWhiteListDto model);

        /// <summary>
        /// 将指定的合并模板从白名单中移除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> RemoveAsync(int id);

    }
}
