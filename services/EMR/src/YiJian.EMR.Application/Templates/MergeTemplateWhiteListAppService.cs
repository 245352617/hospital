using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Templates.Contracts;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Templates
{
    /// <summary>
    /// 电子病历合并模板的白名单
    /// </summary>
    [Authorize]
    //[AllowAnonymous]
    public class MergeTemplateWhiteListAppService : EMRAppService, IMergeTemplateWhiteListAppService
    {
        private readonly IMergeTemplateWhiteListRepository _mergeTemplateWhiteListRepository;

        public MergeTemplateWhiteListAppService(IMergeTemplateWhiteListRepository mergeTemplateWhiteListRepository)
        {
            _mergeTemplateWhiteListRepository = mergeTemplateWhiteListRepository;
        }

        /// <summary>
        /// 获取合并白名单列表记录信息
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<IList<MergeTemplateWhiteListDto>>> GetListAsync()
        {
            var data = await _mergeTemplateWhiteListRepository.GetListAsync();
            var map = ObjectMapper.Map<IList<MergeTemplateWhiteList>,IList<MergeTemplateWhiteListDto>>(data);
            return new ResponseBase<IList<MergeTemplateWhiteListDto>>(EStatusCode.COK,data: map);
        }

        /// <summary>
        /// 添加新的合并病历白名单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> AddAsync(MergeTemplateWhiteListDto model)
        {
            var res = await _mergeTemplateWhiteListRepository.AddAsync(new MergeTemplateWhiteList(model.TemplateId,model.TemplateName));
            return new ResponseBase<int>(EStatusCode.COK,res);
        }

        /// <summary>
        /// 将指定的合并模板从白名单中移除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> RemoveAsync(int id)
        {
            int res = await _mergeTemplateWhiteListRepository.RemoveAsync(id);
            return new ResponseBase<int>(EStatusCode.COK,res);
        }

    }
}
