using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.TriageService.Application.Dtos;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IPekingUniversityHisApi
    {
        /// <summary>
        /// 调用接口平台转诊接口
        /// </summary>
        /// <param name="patientInfo">病人信息</param>
        /// <param name="triageConfig"></param>
        /// <returns></returns>
        Task<HisResponseDto> ReferralAsync(PatientInfo patientInfo, TriageConfig triageConfig);
    }
}
