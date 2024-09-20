using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using SamJan.MicroService.TriageService.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 通用 HIS Api
    /// </summary>
    public class MockHisApi : IHisApi
    {
        private readonly IRepository<TriageConfig> _triageConfigRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MockHisApi> _logger;
        private readonly SocketHelper _socketHelper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public MockHisApi(IRepository<TriageConfig> triageConfigRepository, IConfiguration configuration, ILogger<MockHisApi> logger, SocketHelper socketHelper, IHttpContextAccessor accessor, IHttpClientFactory httpClientFactory)
        {
            _triageConfigRepository = triageConfigRepository;
            _configuration = configuration;
            _logger = logger;
            _socketHelper = socketHelper;
            _accessor = accessor;
            _httpClientFactory = httpClientFactory;
        }

        public Task<PatientInfo> RegisterPatientAsync(PatientInfo patient, string doctorId, string doctorName, bool isUpdated, bool hasChangedDoctor, bool isFirstTimePush)
        {
            return Task.FromResult(patient);
        }

        public Task<PatientInfo> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo patient, bool isUpdated, bool hasChangedDoctor)
        {
            return Task.FromResult(patient);
        }

        public Task<JsonResult> SyncRegisterPatientFromHis()
        {
            return Task.FromResult(JsonResult.Ok());
        }

        /// <summary>
        /// 获取医生列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public async Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate)
        {
            var uri = _configuration["getDoctorListUrl"];
            try
            {
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                httpClient.DefaultRequestHeaders.Add("Authorization", _accessor.HttpContext.Request.Headers["Authorization"].ToString());
                var response = await httpClient.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("平台接口无法连接");
                }

                var responseText = await response.Content.ReadAsStringAsync();

                JsonResult<PagedResultDto<PlatformUser>> hisResponse = JsonSerializer.Deserialize<JsonResult<PagedResultDto<PlatformUser>>>(responseText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (hisResponse.Code != 0)
                {
                    return JsonResult<List<DoctorSchedule>>.Fail(msg: hisResponse.Msg);
                }
                if (hisResponse.Data.TotalCount <= 0)
                {
                    return JsonResult<List<DoctorSchedule>>.Ok();
                }
                var result = hisResponse.Data.Items.BuildAdapter()
                    .ForkConfig(forked =>
                    {
                        forked.ForType<PlatformUser, DoctorSchedule>()
                             .Map(dest => dest.DoctorName, src => src.Name)
                             .Map(dest => dest.DoctorCode, src => src.UserName)
                             .Map(dest => dest.DoctorTitle, src => src.Position);
                    })
                    .AdaptToType<List<DoctorSchedule>>();
                foreach (var item in result)
                {
                    item.DoctorNamePy = PyHelper.GetFirstPy(item.DoctorName);
                }

                return JsonResult<List<DoctorSchedule>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<List<DoctorSchedule>>.Fail(msg: ex.Message);

            }
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="idType">
        /// 证件类型 
        /// 1: 就诊卡
        /// 2: 居民身份证
        /// </param>
        /// <param name="identityNo">身份证号码</param>
        /// <param name="visitNo">就诊号</param>
        /// <param name="patientName">姓名</param>
        /// <param name="phone">电话号码</param>
        /// <param name="registerNo">挂号流水号</param>
        /// <returns></returns>
        public async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordAsync(string idType, string identityNo, string visitNo, string patientName, string phone = "", string registerNo = "")
        {
            var res = await _socketHelper.GetPatientRecordAsync(new Hl7PatientInput
            {
                IdNo = identityNo,
                Name = patientName,
                Sex = "男"
            });
            _logger.LogInformation("SocketHelper 查询结果：{Json}", JsonHelper.SerializeObject(res));
            if (!string.IsNullOrEmpty(res.ErrorMsg))
            {
                _logger.LogError("根据输入项获取患者病历号失败！原因：{Msg}", res.ErrorMsg);
                return JsonResult<List<PatientInfoFromHis>>.Fail(res.ErrorMsg);
            }

            PatientInfoFromHis dto = await GetHl7PatientOutput(new CreateOrGetPatientIdInput
            {
                ContactsPhone = phone,
                IdTypeCode = idType,
                VisitNo = visitNo,
                RegisterNo = registerNo,
            }, res);

            _logger.LogInformation("根据输入项获取患者病历号成功");
            return JsonResult<List<PatientInfoFromHis>>.Ok(data: new List<PatientInfoFromHis> { dto });
        }

        /// <summary>
        /// 患者建档接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordAsync(CreateOrGetPatientIdInput input)
        {
            if (string.IsNullOrEmpty(input.PatientName))
            {
                return JsonResult<PatientInfoFromHis>.Fail("姓名不能为空");
            }
            var response = await _socketHelper.CreatePatientRecordAsync(new Hl7PatientInput
            {
                IdNo = input.IdentityNo,
                Name = input.PatientName,
                Sex = "男",
            });


            if (!string.IsNullOrEmpty(response.ErrorMsg))
            {
                _logger.LogInformation("根据输入项创建患者病历号失败！原因：{Msg}", response.ErrorMsg);
                return JsonResult<PatientInfoFromHis>.Fail(response.ErrorMsg);
            }
            if (input.Birthday != null)
            {
                response.Birthday = Convert.ToDateTime(input.Birthday);
                response.GetAge();
            }

            var res = response.BuildAdapter().AdaptToType<PatientInfoFromHis>();

            return JsonResult<PatientInfoFromHis>.Ok(data: res);
        }

        /// <summary>
        /// 三无患者建档
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreateNoThreePatientRecordAsync(CreateOrGetPatientIdInput input)
        {
            // 性别
            var sexConfig = await _triageConfigRepository.AsNoTracking().OrderBy(x => x.Sort)
                .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.Sex && x.TriageConfigCode == input.Sex);
            input.PatientName ??= $"{(input.IsInfant ? "婴幼儿" : "无名氏")}_{sexConfig?.TriageConfigName}{DateTime.Now:yyyyMMddHHmmss}";
            return await this.CreatePatientRecordAsync(input);
        }

        public Task<JsonResult> ValidateBeforeCreatePatient(CreateOrGetPatientIdInput input, out bool isInfant)
        {
            isInfant = false;
            return Task.FromResult(JsonResult.Ok());

            // if (!input.IsNoThree && string.IsNullOrEmpty(input.ContactsPhone) && string.IsNullOrEmpty(input.Address))
            // {
            //     return Task.FromResult(JsonResult.Fail("电话号码和住址必须填写其中一项"));
            // }

            // return Task.FromResult(JsonResult.Ok());
        }

        public Task<JsonResult> CancelRegisterInfoAsync(string regSerialNo)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        public Task<JsonResult<VitalSignsInfoByJinWan>> GetHisVitalSignsAsync(string serialNumber)
        {
            return Task.FromResult(JsonResult<VitalSignsInfoByJinWan>.Ok());
        }

        public Task<JsonResult> RevisePerson(PatientModifyDto input)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        public async Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync()
        {
            var uri = _configuration["getNurseListUrl"];
            try
            {
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                httpClient.DefaultRequestHeaders.Add("Authorization", _accessor.HttpContext.Request.Headers["Authorization"].ToString());
                var response = await httpClient.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("HIS 接口无法连接");
                }

                var responseText = await response.Content.ReadAsStringAsync();

                JsonResult<PagedResultDto<PlatformUser>> hisResponse = JsonSerializer.Deserialize<JsonResult<PagedResultDto<PlatformUser>>>(responseText, new JsonSerializerOptions
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

                return JsonResult<List<EmployeeDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<List<EmployeeDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 暂停/恢复叫号（挂起状态，医生站不能呼叫、接诊患者）
        /// </summary>
        /// <param name="patientId">患者Id</param>
        /// <param name="isSuspend">是否暂停（0：暂停，1：开启）</param>
        /// <returns></returns>
        public Task<JsonResult> SuspendCalling(string patientId, bool isSuspend)
        {
            return Task.FromResult(JsonResult.Ok());
        }

        private async Task<PatientInfoFromHis> GetHl7PatientOutput(CreateOrGetPatientIdInput input, Hl7PatientInfoDto res)
        {
            if (input.IdentityNo.Length == 18)
            {
                res.Birthday = DateTime.ParseExact(input.IdentityNo.Substring(6, 8), "yyyyMMdd",
                    CultureInfo.CurrentCulture);
                var sexFlag = Convert.ToInt32(input.IdentityNo.Substring(16, 1));
                res.Sex = Convert.ToBoolean(sexFlag & 1) ? Gender.Male.GetDescriptionByEnum() : Gender.Female.GetDescriptionByEnum();
                res.GetAge();
            }

            //若接口身份证号返回为空则使用前端传入
            if (res.IdentityNo.IsNullOrWhiteSpace())
            {
                res.IdentityNo = input.IdentityNo;
            }

            var dto = res.BuildAdapter().AdaptToType<PatientInfoFromHis>();
            // 默认来院方式
            var defaultToHospitalWay = await this._triageConfigRepository.AsQueryable().AsNoTracking()
                .Where(x => x.TriageConfigType == (int)TriageDict.ToHospitalWay && x.IsDisable == 1)
                .OrderBy(x => x.Sort)
                .FirstOrDefaultAsync();
            dto.TaskInfoId = input.TaskInfoId;
            dto.CarNum = input.CarNum;
            dto.Sex ??= input.Sex;
            dto.ContactsPerson = string.IsNullOrWhiteSpace(dto.ContactsPerson)
                ? input.ContactsPerson
                : dto.ContactsPerson;
            dto.ContactsPhone = string.IsNullOrWhiteSpace(dto.ContactsPhone)
                ? input.ContactsPhone
                : dto.ContactsPhone;
            dto.StartTriageTime = DateTime.Now;
            dto.ToHospitalWay ??= defaultToHospitalWay?.TriageConfigCode;

            return dto;
        }

        /// <summary>
        /// 获取base64签名
        /// </summary>
        /// <param name="empCode">工号</param>
        /// <returns></returns>
        public Task<JsonResult<string>> GetStampBase64Async(string empCode)
        {
            return Task.FromResult(JsonResult<string>.Ok(msg: "获取签章图片成功", data: "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"));
        }

        /// <summary>
        /// 门诊患者信息查询
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<PatientInfoFromHis> GetPatienInfoBytIdAsync(string patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientRespDto>> GetRegisterInfoAsync(HisRegisterInfoQueryDto hisRegisterInfoQueryDto)
        {
            var patientRespDtos = new List<PatientRespDto>();
            await Task.Run(() =>
            {
                patientRespDtos.Add(new PatientRespDto()
                {
                    patientId = "0003452138",
                    idCard = null,
                    patientNo = null,
                    cardType = "01",
                    identifyNO = "432301197201267032",
                    patientName = "彭建兵",
                    birthday = "1972/1/26 0:00:00",
                    sex = "M",
                    homeAddress = "贵州省兴义市湖南街32号",
                    officeAddress = null,
                    nationaddress = null,
                    phoneNumberHome = null,
                    phoneNumberBus = "15870369571",
                    maritalStatus = null,
                    ssnNum = null,
                    ethnicGroup = null,
                    nationality = null,
                    patientClass = null,
                    patientType = "1",
                    visitNum = "",
                    visitNo = "0003452138",
                    alternateVisitId = null,
                    appointmentId = null,
                    job = null,
                    weight = null,
                    contactName = null,
                    contactPhone = null,
                    guardIdType = null,
                    cardNo = null,
                    seeDate = null,
                    registerId = "2630674",
                    registerSequence = null,
                    registerDate = "2022/7/26 0:00:46",
                    shift = null,
                    deptId = "0120",
                    dcotorCode = null,
                    isCancel = "0",
                    @operator = null,
                    regType = "1",
                    patientKind = "1",
                    chargeType = "1",
                    age = "50岁",
                    deptName = "急诊科门诊",
                    timeInterval = "1",
                    diagnosis = "2",
                    isRefund = "0"
                });

                patientRespDtos.Add(new PatientRespDto()
                {
                    patientId = "0003452138",
                    idCard = null,
                    patientNo = null,
                    cardType = "01",
                    identifyNO = "432301197201267032",
                    patientName = "彭建兵",
                    birthday = "1972/1/26 0:00:00",
                    sex = "M",
                    homeAddress = "贵州省兴义市湖南街32号",
                    officeAddress = null,
                    nationaddress = null,
                    phoneNumberHome = null,
                    phoneNumberBus = "15870369571",
                    maritalStatus = null,
                    ssnNum = null,
                    ethnicGroup = null,
                    nationality = null,
                    patientClass = null,
                    patientType = "1",
                    visitNum = "",
                    visitNo = "0003452138",
                    alternateVisitId = null,
                    appointmentId = null,
                    job = null,
                    weight = null,
                    contactName = null,
                    contactPhone = null,
                    guardIdType = null,
                    cardNo = null,
                    seeDate = null,
                    registerId = "2630674",
                    registerSequence = null,
                    registerDate = "2022/7/26 0:00:46",
                    shift = null,
                    deptId = "0120",
                    dcotorCode = null,
                    isCancel = "0",
                    @operator = null,
                    regType = "1",
                    patientKind = "1",
                    chargeType = "1",
                    age = "50岁",
                    deptName = "急诊科门诊1",
                    timeInterval = "1",
                    diagnosis = "2",
                    isRefund = "0"
                });
            });
            return patientRespDtos;
        }

        public Task<JsonResult<List<RegisterInfoHisDto>>> GetRegisterInfoListAsync(RegisterInfoInput input)
        {
            throw new NotImplementedException();
        }

        public Task<HisResponseDto> payCurRegisterAsync(string visitNum)
        {
            throw new NotImplementedException();
        }

        public Task<InsuranceDto> GetInsuranceInfoByElectronCert(string electronCertNo, string extraCode)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(
        GetRegisterPagedListInput input)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult> ReturnToNoTriage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
