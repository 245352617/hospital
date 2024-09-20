using System.Collections.Generic;
using System.Threading.Tasks;
using TriageService.Application.Dtos.Triage.Patient;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IRabbitMqAppService : IApplicationService
    {
        /// <summary>
        /// 发布六大中心同步病患队列消息
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task PublishSixCenterSyncPatientAsync(List<SyncPatientEventBusEto> dtos, Dictionary<string, List<TriageConfigDto>> dicts);

        /// <summary>
        /// 发布同步病患到急诊诊疗服务队列消息
        /// </summary>
        /// <param name="mqDtos"></param>
        /// <returns></returns>
        Task PublishEcisPatientSyncPatientAsync(List<PatientInfoMqDto> mqDtos);

        /// <summary>
        /// 发送评分记录到六大中心
        /// </summary>
        /// <param name="scoreInfos"></param>
        /// <returns></returns>
        Task PublishScoreRecordToSixCenterAsync(List<ScoreInfo> scoreInfos);


        /// <summary>
        /// His同步患者状态
        /// </summary>
        /// <param name="mqDtos"></param>
        /// <returns></returns>
        Task PublishEcisPatientStatusAsync(List<PatientInfoFromHisStatusDto> mqDtos);

        /// <summary>
        /// 发布退号消息到Call服务
        /// </summary>
        Task PublishEcisRefundPatientToCallAsync(HashSet<string> mqDtos);
    }
}