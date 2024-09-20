using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 医生选择科室诊室
    /// </summary>
    public interface IDoctorDeptAppService
    {
        /// <summary>
        /// 保存医生选择的科室诊室
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<string>> CreateAsync(DoctorDeptDto dto);

        /// <summary>
        /// 查询当前医生的科室诊室
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<DoctorDeptDto>> GetAsync();
    }
}