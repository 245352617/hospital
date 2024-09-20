using System.Collections.Generic;
using System.Threading.Tasks;
using TriageService.HisApiBridge.Model;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 对接叫号系统相关方法
    /// </summary>
    public interface ICallApi
    {
        /// <summary>
        /// 根据新dto与数据库对比，确定调用的Call接口
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="currentDBPatient"></param>
        /// <returns></returns>
        Task<CommonHttpResult<PatientInfo>> GetFromCallServeic(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentDBPatient);

        /// <summary>
        /// 调用Call接口，获得患者列表
        /// </summary>
        /// <param name="triageDept"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<CallPatientInfo>> GetOrderListFromCallAsync(string triageDept, int pageSize);
    }
}