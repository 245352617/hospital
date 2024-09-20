using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.TriageService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    [Obsolete("服务合并到 EmployeeAppService 中")]
    public class DoctorAppService : ApplicationService, IDoctorAppService
    {
        private readonly IHisApi _hisApi;

        public DoctorAppService(IHisApi hisApi)
        {
            this._hisApi = hisApi;
        }

        /// <summary>
        /// 获取医生排班、号源信息
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="regDate">挂号日期</param>
        /// <returns></returns>
        public async Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate)
        {
            return await this._hisApi.GetDoctorScheduleAsync(deptCode, regDate);
        }
    }
}
