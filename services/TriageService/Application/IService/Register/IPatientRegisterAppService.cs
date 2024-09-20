using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IPatientRegisterAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询分诊患者基本信息以及挂号信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<PatientRegisterInfoDto>>> GetPatientRegisterInfoListAsync(
            PagedPatientRegisterInput input);

        /// <summary>
        /// 挂号
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> CreateRegisterNoForPatientAsync(Guid triagePatientInfoId);

        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <param name="patientInfoId">分诊患者Id</param>
        /// <returns></returns>
        Task<JsonResult> CancelRegisterNoAsync(Guid patientInfoId);

        #region 挂号 --> 分诊

        /// <summary>
        /// 挂号（分诊前）（适用于 挂号 --> 分诊 的模式）
        /// </summary>
        /// <param name="input">挂号参数（来自HIS）</param>
        /// <returns></returns>
        Task<JsonResult> RegisterBeforeTriageAsync(RegisterInfoBeforeTriageInput input);

        /// <summary>
        /// 获取挂号患者列表
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterListAsync(GetRegisterListInput input);

        /// <summary>
        /// 挂号列表打印
        /// </summary>
        /// <param name="patientIds"></param>
        /// <returns></returns>
        Task<JsonResult<List<RegisterPatientInfoDto>>> GetRegisterByPrintAsync(string patientIds);

        /// <summary>
        /// 获取科室挂号统计列表
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<IEnumerable<DeptStatisticsDto>>> GetDeptStatistics();

        /// <summary>
        /// 添加新冠问卷信息
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <param name="input">新冠问卷信息</param>
        /// <returns></returns>
        Task<JsonResult<Covid19Exam>> AddCovid19ExamAsync(string patientId, PatientCovid19ExamInput input);

        /// <summary>
        /// 查询新冠问卷信息
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns></returns>
        Task<JsonResult<PatientCovid19ExamDto>> GetCovid19ExamAsync(string patientId);

        Task<JsonResult> SyncRegisterPatientFromHis();

        #endregion
    }
}