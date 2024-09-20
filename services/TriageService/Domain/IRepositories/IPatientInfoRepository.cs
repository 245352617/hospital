using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using TriageService.Application.Dtos.Triage.Patient;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IPatientInfoRepository : IRepository<PatientInfo, Guid>, IBaseRepository<PatientInfo, Guid>
    {
        /// <summary>
        /// 更新PatientInfo特定方法
        /// </summary>
        /// <param name="patientInfo"></param>
        public Task<bool> UpdateRecordAsync(PatientInfo patientInfo);

        /// <summary>
        /// 保存分诊
        /// </summary>
        /// <param name="patient">患者分诊信息</param>
        /// <param name="dicts">字典</param>
        /// <param name="groupInjuryInfo">群伤事件</param>
        /// <returns></returns>
        Task<ReturnResult<bool>> SaveTriageRecordAsync(IEnumerable<PatientInfo> patient, Dictionary<string, List<TriageConfigDto>> dicts, GroupInjuryInfo groupInjuryInfo = null);

        /// <summary>
        /// 接收病患微服务队列消息更新病患信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdatePatientInfoFromMqAsync(UpdatePatientInfoMqDto dto);

        /// <summary>
        /// 获取当前等候的患者列表
        /// </summary>
        /// <returns></returns>
        Task<List<PatientInfo>> GetCurrentWaitingList();

        /// <summary>
        /// 根据挂号流水号获取患者信息
        /// </summary>
        /// <param name="registerNos"></param>
        /// <returns></returns>
        Task<List<PatientFromHisInfoDto>> GetPatientInfoByHisRegSerialNoAsync(IEnumerable<string> registerNos);
    }
}