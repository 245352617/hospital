using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 绑定 RFID API
    /// </summary>
    public interface IRfidApi
    {
        /// <summary>
        /// RFID 绑定
        /// </summary>
        /// <returns></returns>
        Task BindAsync(PatientInfo patientInfo,RegisterInfo registerInfo);
    }
}