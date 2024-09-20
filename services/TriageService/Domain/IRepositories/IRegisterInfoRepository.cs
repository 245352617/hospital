using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IRegisterInfoRepository : IRepository<RegisterInfo, Guid>, IBaseRepository<RegisterInfo, Guid>
    {
        /// <summary>
        /// 分页查询分诊患者基本信息以及挂号信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PatientRegisterInfoDto>> GetPatientRegisterInfoListAsync(PagedPatientRegisterInput input);
        /// <summary>
        /// 获取北大 HIS 急诊科挂号患者列表
        /// </summary>
        /// <returns></returns>
        Task<HisRegisterPatient> GetHisPatientInfoAsync(string registerNo);

    }
}