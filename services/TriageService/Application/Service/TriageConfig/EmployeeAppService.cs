using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.TriageService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SamJan.MicroService.PreHospital.Core.Help;
using Microsoft.AspNetCore.Mvc;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class EmployeeAppService : ApplicationService, IEmployeeAppService
    {
        private readonly IHisApi _hisApi;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ILogger<EmployeeAppService> _log;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _accessor;
        private readonly IRepository<TriageConfig> _triageConfigRepository;

        public EmployeeAppService(IHisApi hisApi, IConfiguration configuration, IHttpClientHelper httpClientHelper,
            ILogger<EmployeeAppService> log, IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor,
            IRepository<TriageConfig> triageConfigRepository)
        {
            this._hisApi = hisApi;
            this._configuration = configuration;
            this._httpClientHelper = httpClientHelper;
            this._log = log;
            _httpClientFactory = httpClientFactory;
            _accessor = accessor;
            _triageConfigRepository = triageConfigRepository;
        }

        /// <summary>
        /// 获取护士排班信息
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync()
        {
            return await this._hisApi.GetNurseScheduleAsync();
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

        /// <summary>
        /// 获取医护人员列表
        /// </summary>
        /// <param name="profession">人员类型，枚举：Doctor、Nurse</param>
        /// <param name="name">人员姓名</param>
        /// <returns></returns>
        public async Task<JsonResult<List<EmployeeDto>>> GetListAsync(string profession,string name)
        {
            if (profession == "Doctor")
            {
                try
                {
                    // 获取费别
                    List<TriageConfig> list = await _triageConfigRepository.AsNoTracking()
                        .Where(x => x.TriageConfigType == (int)TriageDict.EmergencyDoctor)
                        .WhereIf(!string.IsNullOrWhiteSpace(name),x=>x.TriageConfigName.Contains(name))
                        .Where(x => x.IsDisable == 1)
                        .Where(x => x.IsDeleted == false)
                        .OrderBy(o => o.Sort).IgnoreQueryFilters().ToListAsync();

                    List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
                    foreach (var item in list)
                    {
                        employeeDtos.Add(new EmployeeDto
                        {
                            DeptCode = null,
                            DeptName = null,
                            Code = item.HisConfigCode,
                            Name = item.TriageConfigName,
                            NamePy = item.Py
                        });
                    }

                    return JsonResult<List<EmployeeDto>>.Ok(data: employeeDtos);

                }
                catch (Exception ex)
                {
                    return JsonResult<List<EmployeeDto>>.Fail(msg: ex.Message);
                }
            }
            else
            {

                if (profession != "Doctor" && profession != "Nurse")
                    return JsonResult<List<EmployeeDto>>.Fail("人员类型只允许使用 Doctor 或 Nurse");
                var uri = (profession == "Nurse" ? _configuration["getNurseListUrl"] : _configuration["getDoctorListUrl"]);
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("HisApi");
                    httpClient.DefaultRequestHeaders.Add("Authorization", _accessor.HttpContext.Request.Headers["Authorization"].ToString());
                    this._log.LogInformation("人员信息查询，uri: {Uri}", uri);
                    var response = await httpClient.GetAsync(uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("平台接口无法连接");
                    }

                    var responseText = await response.Content.ReadAsStringAsync();

                    JsonResult<PagedResultDto<PlatformUser>> hisResponse =
                        JsonSerializer.Deserialize<JsonResult<PagedResultDto<PlatformUser>>>(responseText,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                    if (hisResponse.Code != 0)
                    {
                        return JsonResult<List<EmployeeDto>>.Fail(msg: hisResponse.Msg);
                    }

                    if (hisResponse.Data.TotalCount <= 0)
                    {
                        return JsonResult<List<EmployeeDto>>.Ok();
                    }

                    var result = hisResponse.Data.Items.BuildAdapter()
                        .ForkConfig(forked =>
                        {
                            forked.ForType<PlatformUser, EmployeeDto>()
                                .Map(dest => dest.Code, src => src.UserName);
                        })
                        .AdaptToType<List<EmployeeDto>>();
                    foreach (var item in result)
                    {
                        item.NamePy = PyHelper.GetFirstPy(item.Name);
                    }
                    // 北大特殊处理，北大 周启棣（1276） 医生在重症科，需要在急诊科可以分诊
                    // TODO: 临时方案；后面在自己系统维护医生，不使用平台的医生列表
                    var hisCode = _configuration.GetValue<string>("HospitalCode");
                    if (hisCode == "PekingUniversity")
                    {
                        result.Add(new EmployeeDto
                        {
                            DeptCode = null,
                            DeptName = null,
                            Code = "1276",
                            Name = "周启棣",
                            NamePy = "ZQD"
                        });
                    }

                    return JsonResult<List<EmployeeDto>>.Ok(data: result);
                }
                catch (Exception ex)
                {
                    return JsonResult<List<EmployeeDto>>.Fail(msg: ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取base64签名
        /// </summary>
        /// <param name="empCode">工号</param>
        /// <returns></returns>
        public async Task<JsonResult<string>> GetStampBase64(string empCode)
        {
            try
            {
                return await _hisApi.GetStampBase64Async(empCode);
            }
            catch (HttpRequestException ex)
            {
                return JsonResult<string>.Fail(msg: $"调用接口失败，{ex.Message}");
            }
            catch (Exception ex)
            {
                return JsonResult<string>.Fail(msg: ex.Message);
            }
        }
    }
}