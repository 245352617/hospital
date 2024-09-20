using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IEmployeeAppService : IApplicationService
    {
        /// <summary>
        /// 获取医护人员列表
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<EmployeeDto>>> GetListAsync(string profession,string name);

        /// <summary>
        /// 获取护士排班信息
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync();

        /// <summary>
        /// 获取医生排班、号源信息
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="regDate">挂号日期</param>
        /// <returns></returns>
        Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate);
    }
}
