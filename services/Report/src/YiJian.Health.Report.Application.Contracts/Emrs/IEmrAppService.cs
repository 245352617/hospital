using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Emrs.Dto;

namespace YiJian.Health.Report.Emrs
{
    /// <summary>
    /// 电子病历相关的接口
    /// </summary>
    public interface IEmrAppService : IApplicationService
    {
        /// <summary>
        /// 获取生命体征记录信息集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<EmrNursingRecordDto>>> NursingRecordsAsync(NursingRecordRequestDto model);

    }
}
