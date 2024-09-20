using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IPatientDataAnalysisAppService : IApplicationService
    {
        Task<JsonResult<PatientAnalysisDto>> GetPatientDataAnalysisAsync(PatientInfoWhereInput input);
    }
}