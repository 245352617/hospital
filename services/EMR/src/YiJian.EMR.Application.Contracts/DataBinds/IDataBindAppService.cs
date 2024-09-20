using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.EMR.Libs.Dto;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Enums;
using YiJian.EMR.DataBinds.Dto;

namespace YiJian.EMR.DataBinds
{
    /// <summary>
    /// 数据绑定服务接口
    /// </summary>
    public interface IDataBindAppService : IApplicationService
    {
        /// <summary>
        /// 获取绑定
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public Task<ResponseBase<Dictionary<string, Dictionary<string, string>>>> GetBindAsync(Guid patientEmrId);

        /// <summary>
        /// 修改或新增绑定的电子病历，文书数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> ModifyBindAsync(ModifyDataBindDto model);
    }
}
