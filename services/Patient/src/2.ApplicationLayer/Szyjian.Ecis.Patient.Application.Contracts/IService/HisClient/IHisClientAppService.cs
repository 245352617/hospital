using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// his对接
    /// </summary>
    public interface IHisClientAppService : IApplicationService
    {

        /// <summary>
        /// 调用接口获取患者诊断
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<List<GetDiagnoseRecordBySocketDto>> GetPatientDiagnoseByIdAsync(string patientId);

        /// <summary>
        /// 调用接口获取患者就诊流水号
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        Task<string> GetPatientRegisterInfoByIdAsync(string patientId, string registerNo);
    }
}