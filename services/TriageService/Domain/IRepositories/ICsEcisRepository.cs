using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊仓储接口
    /// </summary>
    public interface ICsEcisRepository : ITransientDependency
    {
        /// <summary>
        /// CS版急诊分诊保存分诊记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> SaveEcisTriageRecordAsync(CsEcisSaveTriageDto dto);

        /// <summary>
        /// CS版急诊自动审核绿通
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AutoExamineGreenChannelAsync(ExamineGreenChannelDto dto);

        /// <summary>
        /// CS 版急诊入科接诊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> CreatePatientInHouseAsync(CsEcisPatientInDeptDto dto);
    }
}