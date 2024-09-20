using System;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IGroupInjuryRepository : IBaseRepository<GroupInjuryInfo, Guid>, IRepository<GroupInjuryInfo, Guid>
    {
        /// <summary>
        /// 查询群伤事件列表及关联患者
        /// </summary>
        /// <param name="input">输入项</param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<GroupInjuryOutput>>> GetGroupInjuryPatientListAsync(
            PagedGroupInjuryInput input);
            /// <summary>
            /// 根据患者分诊Id查询该患者群伤事件
            /// </summary>
            /// <param name="triagePatientInfoId"></param>
            /// <returns></returns>
            Task<GroupInjuryAndPatientInfoDto> GetGroupInjuryPatientAsync(Guid triagePatientInfoId);
        }
    }