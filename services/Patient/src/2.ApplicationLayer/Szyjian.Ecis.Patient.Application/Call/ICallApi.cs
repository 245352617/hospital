using BeetleX.Http.Clients;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application
{
    [JsonFormater]
    public interface ICallApi
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns></returns>
        [Post(Route = "api/call/call-info/paged-list")]
        public Task<RESTfulResult<PagedResultDto<CallInfoData>>> GetPagedListAsync(GetCallInfoInput input);

        /// <summary>
        /// 顺呼
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Post(Route = "api/call/call-info/call-next")]
        public Task<RESTfulResult<CallInfoData>> CallNextAsync(CallNextDto input);

        /// <summary>
        /// 过号
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        [Post(Route = "api/call/call-info/missed-turn")]
        public Task<RESTfulResult<CallInfoData>> MissedTurnAsync(MissedTurnDto dot);


        /// <summary>
        /// 过号（多次叫号没人）
        /// </summary>
        /// <param name="input"></param>
        [Post(Route = "api/call/call-info/untreated-over")]
        public Task<RESTfulResult<CallInfoData>> UntreatedOverAsync(UntreatedOverDto input);

        /// <summary>
        /// 取消叫号
        /// </summary>
        /// <param name="input"></param>
        [Post(Route = "api/call/call-info/call-cancel")]
        public Task<RESTfulResult<CallInfoData>> CancelCallAsync(CallCancelDto input);

        /// <summary>
        /// 重呼
        /// </summary>
        /// <param name="input"></param>
        [Post(Route = "api/call/call-info/call-again")]
        public Task<RESTfulResult<CallInfoData>> CallAgainAsync(CallAgainDto input);

        /// <summary>
        /// 开始接诊（结束叫号）
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        [Post(Route = "api/call/call-info/treat-finish?registerNo={registerNo}")]
        public Task<CallInfoData> TreatFinishAsync(string registerNo);

        /// <summary>
        /// 召回
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        [Post(Route = "api/call/call-info/send-back-waiting?registerNo={registerNo}")]
        public Task<CallInfoData> SendBackWaittingAsync(string registerNo);

        /// <summary>
        /// 过号患者恢复候诊
        /// </summary>
        /// <param name="registerNo">挂号的No</param>
        [Post(Route = "api/call/call-info/return-to-queue?registerNo={registerNo}")]
        public Task<CallInfoData> ReturnToQueueAsync(string registerNo);
    }
}
