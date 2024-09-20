using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Patient.Application.Service.HospitalApplyRecord.PKU.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp;

namespace YiJian.ECIS.DDP.Apis
{
    /// <summary>
    /// PKU: 北京大学深圳医院（Peking University Shenzhen Hospital）接口服务
    /// </summary>
    [RemoteService(false)]
    public class PKUApiService : EcisPatientAppService
    {
        private readonly ILogger<PKUApiService> _log;
        private readonly DdpApiClient _ddpApiClient;

        public PKUApiService(ILogger<PKUApiService> log
            , DdpApiClient ddpApiClient)
        {
            _log = log;
            _ddpApiClient = ddpApiClient;
        }

        /// <summary>
        /// 门口屏叫号
        /// </summary>
        /// <returns></returns>  
        public async Task<DdpBaseResponse<object>> DoorScreenCallAsync(TerminalCallToHisDto model)
        {
            DdpBaseRequest<TerminalCallToHisDto> requestModel = new DdpBaseRequest<TerminalCallToHisDto>
            {
                Path = @"/API/Treatment/TerminalCall",
                Req = model
            };

            return await CallDDPInterfaceAsync(requestModel);
        }

        /// <summary>
        /// 保存绿色通道
        /// </summary>
        /// <returns></returns>  
        public async Task<DdpBaseResponse<object>> PushGreenChannelAsync(GreenChannelToHisDto model)
        {
            DdpBaseRequest<GreenChannelToHisDto> requestModel = new DdpBaseRequest<GreenChannelToHisDto>
            {
                Path = @"/api/ecis/greenchannel",
                Req = model
            };

            return await CallDDPInterfaceAsync(requestModel);
        }

        /// <summary>
        /// 保存就诊记录
        /// </summary>
        /// <returns></returns>  
        public async Task<DdpBaseResponse<object>> SaveVisitRecordAsync(VisitRecordToHisDto model)
        {
            DdpBaseRequest<VisitRecordToHisDto> requestModel = new DdpBaseRequest<VisitRecordToHisDto>
            {
                Path = @"/api/ecis/addVisitRecord",
                Req = model
            };

            return await CallDDPInterfaceAsync(requestModel);
        }


        /// <summary>
        /// 保存入院通知证书数据(急诊)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<HospitalApplyRespDto> SaveInHospital(ApplyForHospitalizationDto model)
        {
            DdpBaseRequest<ApplyForHospitalizationDto> requestModel = new DdpBaseRequest<ApplyForHospitalizationDto>
            {
                Path = @"api/ecis/transferToInpatientDepartment",
                Req = model
            };

            DdpBaseResponse<object> result = await CallDDPInterfaceAsync(requestModel);
            if (result.Code == 200)
            {
                string jsonResult = result.Data.ToJson();
                return JsonConvert.DeserializeObject<HospitalApplyRespDto>(jsonResult);
            }

            return null;
        }

        /// <summary>
        /// 保存诊断
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal async Task<bool> SaveRecipeJzAsync(SaveDiagsDto model)
        {
            DdpBaseRequest<SaveDiagsDto> requestModel = new DdpBaseRequest<SaveDiagsDto>
            {
                Path = @"/api/ecis/saveDiagnose",
                Req = model
            };

            DdpBaseResponse<object> result = await CallDDPInterfaceAsync(requestModel);
            return result.Code == 200;
        }

        internal async Task<string> GetUserBranchAsync(string userName)
        {
            DdpBaseRequest<UserNameDto> requestModel = new DdpBaseRequest<UserNameDto>
            {
                Path = @"/api/ecis/get-user-branch",
                Req = new UserNameDto { UserName = userName }
            };

            DdpBaseResponse<object> result = await CallDDPInterfaceAsync(requestModel);
            if (result.Code == 200 && result.Data != null)
            {

                List<UserBranchDto> userBranchDtos = JsonConvert.DeserializeObject<List<UserBranchDto>>(result.Data.ToString());
                return userBranchDtos.FirstOrDefault()?.Branch;
            }

            return string.Empty;
        }

        internal async Task<List<TreatmentInfo>> GetTreatmentInfosAsync()
        {
            DdpBaseRequest<object> requestModel = new DdpBaseRequest<object>
            {
                Path = @"api/dict/permitgroup",
                Req = new { }
            };

            DdpBaseResponse<object> result = await CallDDPInterfaceAsync(requestModel);
            if (result.Code == 200 && result.Data != null)
            {

                return JsonConvert.DeserializeObject<List<TreatmentInfo>>(result.Data.ToString());
            }

            return new List<TreatmentInfo>();
        }

        /// <summary>
        /// 调用DDP接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        private async Task<DdpBaseResponse<object>> CallDDPInterfaceAsync<T>(DdpBaseRequest<T> requestModel)
        {
            try
            {
                _log.LogInformation("DDP请求入参：{0}", requestModel.ToJson());
                DdpBaseResponse<object> response = await _ddpApiClient.CallAsync(requestModel);
                if (response != null)
                {
                    _log.LogInformation("DDP请求返回结果：{0}", response.ToJson());
                }

                if (response.Code != 200)
                {
                    throw new Exception(response.Msg);
                }

                return response;
            }
            catch (Exception ex)
            {
                _log.LogError($"接口{requestModel.Path}调用DDP异常：{ex.Message}");
                throw;
            }
        }
    }
}
