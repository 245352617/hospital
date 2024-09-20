using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IGroupInjuryAppService : IApplicationService
    {
        /// <summary>
        /// 院前群伤事件分诊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult<List<PatientOutput>>> SaveGroupInjuryTriageRecordAsync(CreateGroupInjuryTriageDto dto);

        /// <summary>
        /// 取消关联群伤事件
        /// </summary>
        /// <param name="triagePatientInfoId"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteGroupInjuryRecordAsync(Guid triagePatientInfoId);

        /// <summary>
        /// 删除群伤事件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteGroupInjuryAsync(AddGroupInjuryForPatientDto where);

        /// <summary>
        /// 修改关联群伤事件
        /// </summary>
        /// <param name="taskInfoId">任务单Id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> ModifyGroupInjuryRecordAsync(Guid taskInfoId, UpdateGroupInjuryDto dto);

        /// <summary>
        /// 获取群伤关联列表
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<GroupInjuryOutput>>> GetGroupInjuryRecordListAsync(PagedGroupInjuryInput input);

        /// <summary>
        /// 查询单个群伤关联事件
        /// </summary>
        /// <param name="triagePatientInfoId">分诊患者信息Id</param>
        /// <returns></returns>
        Task<JsonResult<GroupInjuryAndPatientInfoDto>> GetGroupInjuryRecordAsync(Guid triagePatientInfoId);

        /// <summary>
        /// 关联群伤事件
        /// </summary>
        /// <param name="dto">群伤事件Id</param>
        /// <returns></returns>
        Task<JsonResult> AddGroupInjuryForPatientAsync(AddGroupInjuryForPatientDto dto);
    }
}