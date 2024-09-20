using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    [Obsolete("服务合并到 IEmployeeAppService 中")]
    public interface IDoctorAppService : IApplicationService
    {
        /// <summary>
        /// 获取医生排班、号源信息
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="regDate">挂号日期</param>
        /// <returns></returns>
        Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate);
    }
}
