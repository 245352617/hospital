using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using SamJan.MicroService.TriageService.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.TriageService.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 通用 HIS Api
    /// </summary>
    public class LonggangHisApi : IHisApi
    {
        /// <summary>
        /// 自费
        /// </summary>
        private const string DEFAULTCHARGETYPE = "Faber_001";

        private readonly CommonHisApi _commonHisApi;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly IRegisterInfoRepository _registerInfoRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CommonHisApi> _log;
        private readonly ICurrentUser _currentUser;
        private readonly IDatabase _redis;
        private readonly IRepository<TriageConfig> _triageConfigRepository;

        private readonly ITriageConfigAppService _triageConfigAppService;


        private static readonly AsyncRetryPolicy<HttpResponseMessage> TransientErrorRetryPolicy =
            Policy.HandleResult<HttpResponseMessage>(
                    message => ((int)message.StatusCode == 429 || (int)message.StatusCode >= 500))
                .WaitAndRetryAsync(2, retryAttempt => { return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)); });

        private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 503)
                //.AdvancedCircuitBreakerAsync(0.5,
                //    TimeSpan.FromMilliseconds(1),
                //    100,
                //    TimeSpan.FromMinutes(1))
                .CircuitBreakerAsync(2, TimeSpan.FromMilliseconds(1));

        public LonggangHisApi(CommonHisApi commonHisApi, IPatientInfoRepository patientInfoRepository,
            IRegisterInfoRepository registerInfoRepository,
            IConfiguration configuration, IHttpClientFactory httpClientFactory, RedisHelper redisHelper,
            IHttpClientHelper httpClientHelper, ILogger<CommonHisApi> log, ICurrentUser currentUser,
            IRepository<TriageConfig> triageConfigRepository,
        ITriageConfigAppService triageConfigAppService)
        {
            this._commonHisApi = commonHisApi;
            this._patientInfoRepository = patientInfoRepository;
            this._registerInfoRepository = registerInfoRepository;
            this._configuration = configuration;
            this._httpClientFactory = httpClientFactory;
            // this._httpClientHelper = httpClientHelper;
            this._log = log;
            this._currentUser = currentUser;
            _triageConfigAppService = triageConfigAppService;
            this._redis = redisHelper.GetDatabase();
            _triageConfigRepository = triageConfigRepository;
        }

        /// <summary>
        /// 挂号/预约/分诊
        /// 龙岗中心规则：分诊后自动挂号；若已挂号的患者，分诊不推送挂号信息
        /// 分诊后自动挂号不考虑收费问题，龙岗中心是在医生接诊后自动添加一条挂号费（诊金），结束看诊（诊毕）后缴费
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="doctorCode">医生代码</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="isUpdated">是否修改分诊信息（false：新增；true：修改）</param>
        /// <param name="hasChangedDoctor">科室、医生信息修改</param>
        /// <param name="isFirstTimePush">是否第一次分诊</param>
        /// <returns></returns>
        public async Task<PatientInfo> RegisterPatientAsync(PatientInfo patient, string doctorCode, string doctorName,
            bool isUpdated, bool hasChangedDoctor, bool isFirstTimePush)
        {
            try
            {
                // 暂存的患者不进行推送
                if (patient.TriageStatus != 1) return await Task.FromResult(patient);
                _log.LogInformation("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号开始】");
                var patientInfoAfterRegister = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.VitalSignInfo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == patient.Id);

                if (patientInfoAfterRegister is null)
                {
                    throw new Exception($"调用预约确认接口失败，患者信息不存在，患者id: {patient.Id}");
                }

                RegisterInfo registerInfo = null;
                // 如果未挂号，则自动挂号（用RegisterNo挂号流水号进行判断）
                if (patientInfoAfterRegister.RegisterInfo?.Count() <= 0 ||
                    string.IsNullOrWhiteSpace(patientInfoAfterRegister.RegisterInfo?.FirstOrDefault()?.RegisterNo))
                {
                    var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
                    // 新增挂号信息
                    var deptConfig = dicts[TriageDict.TriageDepartment.ToString()].FirstOrDefault(x =>
                        x.TriageConfigCode == patientInfoAfterRegister.ConsequenceInfo.TriageDeptCode);
                    var dto = patientInfoAfterRegister.BuildAdapter().AdaptToType<PatientReqDto>();

                    dto.birthday = (patientInfoAfterRegister.Birthday ?? DateTime.Now).ToString("yyyy-MM-dd");
                    dto.chargeType = "2"; // 1医保 2自费
                    // 免费号
                    dto.regType = patient.RegType;
                    // 患者开启绿色通道
                    if (!patient.GreenRoadCode.IsNullOrWhiteSpace())
                    {
                        dto.regWay = "8";
                    }


                    //特约记账类型
                    try
                    {
                        // TODO: 使用 HisCode 来关联 HIS 提交编码
                        dto.bookKeepingUnit = !string.IsNullOrEmpty(patient.SpecialAccountTypeCode) && patient.SpecialAccountTypeCode.Split('_').Length == 2
                                ? patient.SpecialAccountTypeCode.Split('_')[1] : "";
                    }
                    catch { }
                    dto.safetyType = !string.IsNullOrEmpty(patient.SpecialAccountTypeCode) ? "8" : "";

                    //// 参保地
                    //var insuplecAdmdv = await this._triageConfigRepository.AsNoTracking()
                    //    .FirstOrDefaultAsync(x => x.TriageConfigType == (int)TriageDict.InsuplcAdmdv && x.TriageConfigCode == patient.InsuplcAdmdvCode);
                    // 获取医保信息
                    var (hasInsurType, insuranceDto, insuranceType) = await this.GetInsuInfo(patient);

                    // ExtraCode 用于存在医保代码
                    if (hasInsurType && insuranceDto != null)
                    {
                        dto.chargeType = "1"; // 1医保 2自费
                        dto.safetyType = insuranceType.clctstd_crtf_rule_codg; // 医保类别(医保)
                        dto.safetyNo = insuranceDto.psn_no; // 医保号/人员编号(医保)

                        // cardType 在接口平台会被映射成idCardType出去
                        // 经过医保办与社保局沟通，不允许我们用身份证类型进行结算了。预检分诊医保结算一定要带社保卡或者通过医保电子凭证进行结算了   by: ywlin 2023-03-22
                        dto.cardType = !string.IsNullOrEmpty(patient.ElectronCertNo) ? "01" : (!string.IsNullOrWhiteSpace(patient.MedicalNo) ? "03" : "02");  // 01:医保电子凭证  02:居民身份证  03:社会保障卡  
                        dto.cardNo = !string.IsNullOrEmpty(patient.ElectronCertNo) ? patient.ElectronCertNo : patient.IdentityNo; // 实体卡片号
                        dto.sscno = !string.IsNullOrEmpty(patient.ElectronCertNo) ? patient.IdentityNo : patient.MedicalNo;

                        // dto.cardType = "02"; // 01:医保电子凭证  02:居民身份证  03:社会保障卡
                        // dto.cardNo = patient.IdentityNo; // 实体卡片号

                        //dto.insuplcAdmdvs = insuplecAdmdv?.ExtraCode ?? insuranceDto.insuplc_admdvs;   // 参保地医保区划
                        dto.insuplcAdmdvs = insuranceDto.insuplc_admdvs; // 参保地医保区划
                        dto.insuType = insuranceType.insutype; // 险种类型

                        patient.SafetyNo = dto.safetyNo;
                    }

                    dto.deptId = deptConfig.HisConfigCode ?? deptConfig.TriageConfigCode;

                    dto.deptName = deptConfig.TriageConfigName;
                    dto.beginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dto.endTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    dto.seeDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                    dto.doctorId = doctorCode;
                    dto.operatorCode = _currentUser.UserName;
                    dto.operatorName = _currentUser.GetFullName();
                    var currentHour = DateTime.Now.Hour;
                    if (currentHour >= 8 && currentHour <= 12)
                    {
                        dto.noonId = "1";
                    }
                    else if (currentHour > 12 && currentHour <= 18)
                    {
                        dto.noonId = "2";
                    }
                    else
                    {
                        dto.noonId = "3";
                    }

                    dto.insurance = "0";

                    var sexConfig = dicts[TriageDict.Sex.ToString()].FirstOrDefault(x => x.TriageConfigName == dto.sex);
                    dto.sex = sexConfig?.HisConfigCode ?? sexConfig?.TriageConfigCode;
                    if (patientInfoAfterRegister.ConsequenceInfo.TriageTargetCode == "TriageDirection_003" ||
                        patientInfoAfterRegister.ConsequenceInfo.TriageTargetCode == "TriageDirection_002")
                    {
                        dto.patientType = "2";
                    }
                    var msg = JsonSerializer.Serialize(dto);
                    var httpContent = new StringContent(msg);
                    httpContent.Headers.ContentType.MediaType = "application/json";
                    var uri = _configuration.GetSection("HisApiSettings:registerPatient").Value;
                    _log.LogInformation("调用接口平台挂号，url: {Uri}, request: {Serialize}", uri,
                        JsonSerializer.Serialize(dto));
                    // var response = await _httpClientHelper.PostAsync(uri, httpContent);
                    var httpClient = _httpClientFactory.CreateClient("HisApi");
                    var response = await httpClient.PostAsync(uri, httpContent);
                    response.EnsureSuccessStatusCode();
                    var responseText = await response.Content.ReadAsStringAsync();
                    _log.LogInformation("调用接口平台挂号，reponse: {Response}", responseText);
                    if (string.IsNullOrWhiteSpace(responseText))
                    {
                        _log.LogError(
                            "【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：调用挂号接口失败，响应为空】");
                        throw new Exception("调用挂号接口失败，响应为空！");
                    }

                    var json = JObject.Parse(responseText);
                    if (json["code"]?.ToString() == "0")
                    {
                        if (json["data"] != null && !string.IsNullOrWhiteSpace(json["data"].ToString()))
                        {
                            var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString());
                            registerInfo = resp.BuildAdapter().AdaptToType<RegisterInfo>();
                            registerInfo.PatientInfoId = patientInfoAfterRegister.Id;
                            // 挂号流水号
                            registerInfo.RegisterNo = resp.appointmentId;
                            // 已挂号
                            patient.VisitStatus = VisitStatus.WattingTreat;
                            // 排队号
                            patient.CallingSn = resp.registerSequence;
                            patient.LogTime = DateTime.Parse(resp.registerDate);

                            patient.VisitStatus = VisitStatus.WattingTreat;
                            // 挂号流水号
                            // 限制患者基本信息为只读，挂号后不允许修改患者基本信息，这会导致ECIS的患者信息跟HIS的患者信息产生差异
                            patient.IsBasicInfoReadOnly = true;
                        }
                        else
                        {
                            _log.LogError("【PatientInfoService】【GetPatientRecordByHl7MsgAsync】【挂号失败】" +
                                          "【Msg：调用挂号接口失败。返回Data为null或空】");
                            throw new Exception("调用挂号接口失败！接口未返回data");
                        }
                    }
                    else
                    {
                        _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】" +
                                      "【挂号失败】【Msg：调用挂号接口失败。返回原因：{Msg}】", json["msg"]);
                        throw new Exception($"调用挂号接口失败！{json["msg"]}");
                    }

                    if (string.IsNullOrWhiteSpace(registerInfo.RegisterNo))
                    {
                        _log.LogError("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：调用挂号接口失败】");
                        throw new Exception("调用挂号接口失败！HIS未返回挂号流水号！");
                    }

                    var dbContext = _registerInfoRepository.GetDbContext();
                    dbContext.Entry(registerInfo).State = EntityState.Added;
                    if (await dbContext.SaveChangesAsync() <= 0)
                    {
                        _log.LogError(
                            "【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号失败】【Msg：DB保存挂号数据报错】");
                        throw new Exception("挂号失败");
                    }

                    dbContext.Entry(registerInfo).State = EntityState.Detached;
                }

                else if (hasChangedDoctor)
                {
                    // 修改挂号信息
                    await ModifyRegInformations(doctorCode, doctorName, patientInfoAfterRegister);
                }
                else
                {
                    // isUpdated = true, hasChangedDoctor = false 修改分诊但未修改预约信息，不对接HIS接口
                    // 暂无其他操作
                }


                if (patient.VitalSignInfo != null && (!string.IsNullOrWhiteSpace(patient.VitalSignInfo.Temp) ||
                                                      !string.IsNullOrWhiteSpace(patient.Weight) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.Sbp) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.Sdp) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.HeartRate) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.BreathRate) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.SpO2) ||
                                                      patient.VitalSignInfo.BloodGlucose.HasValue ||
                                                      !string.IsNullOrWhiteSpace(
                                                          patient.VitalSignInfo.ConsciousnessName) ||
                                                      !string.IsNullOrWhiteSpace(patient.VitalSignInfo.CardiogramName)))
                {
                    // 登记生命体征
                    _ = await this.PatientVitalSign(patient,
                            registerInfo ?? patientInfoAfterRegister.RegisterInfo.OrderBy(x => x.RegisterTime)
                                .FirstOrDefault())
                        .ConfigureAwait(false);
                }


                _log.LogInformation("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号结束】");

                if (registerInfo != null && !registerInfo.RegisterNo.IsNullOrWhiteSpace())
                {
                    patientInfoAfterRegister.RegisterInfo ??= new List<RegisterInfo>();
                    patientInfoAfterRegister.RegisterInfo.Add(registerInfo);
                }

                return patientInfoAfterRegister;
            }
            catch (Exception e)
            {
                _log.LogWarning("【PatientRegisterService】【CreateRegisterNoForPatientAsync】【挂号错误】【Msg：{Msg}】", e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取医保信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<(bool, InsuranceDto, InsuranceType)> GetInsuInfo(PatientInfo patient)
        {
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
            // 获取费别信息
            var faber = dicts[TriageDict.Faber.ToString()].FirstOrDefault(x =>
                x.TriageConfigType == (int)TriageDict.Faber && x.TriageConfigCode == patient.ChargeType);
            // 医保相关信息
            _log.LogInformation("开始挂号, 费别: {Faber}, 医保代码：{ExtraCode}", patient.ChargeType,
                faber?.TriageConfigName, faber?.ExtraCode);


            if (string.IsNullOrWhiteSpace(faber?.ExtraCode))
            {// 如果是非医保费别（ExtraCode 为 null），则不查询医保信息
                return (false, null, null);
            }
            if (faber.ExtraCode == "SpecialAccount")
            {// 特约记账是非医保类型，编码做特殊判断
                return (false, null, null);
            }

            InsuranceDto insuranceDto = null; // 医保信息
            // // 如果开启绿色通道则是先诊疗后付费的流程，在预检分诊如果开通绿通，则不记录费别及医保信息
            //if (!patient.GreenRoadCode.IsNullOrWhiteSpace()) return (false, null, null);
            if (string.IsNullOrEmpty(faber?.ExtraCode)) return (false, null, null);
            if (!_redis.KeyExists($"{_configuration["ServiceName"]}:Insurance:{patient.IdentityNo}:{patient.InsuplcAdmdvCode}"))
            {
                _log.LogInformation("未从缓存获取到患者医保信息：{PatientInfo}", JsonSerializer.Serialize(patient));

                TriageConfig insuplcAdmdv = null;
                if (!string.IsNullOrEmpty(patient.InsuplcAdmdvCode))
                {
                    insuplcAdmdv = await _triageConfigRepository.AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                        x.TriageConfigType == (int)TriageDict.InsuplcAdmdv &&
                            x.TriageConfigCode == patient.InsuplcAdmdvCode);
                }

                try
                {
                    insuranceDto =
                        await GetInsuranceInfos(patient.IdentityNo, insuplcAdmdv?.ExtraCode ?? "440300"); // 医保参保地，默认深圳市

                    if (insuranceDto == null)
                    {
                        throw new Exception($"该患者不可使用医保费别【{faber.TriageConfigName}】");
                    }
                }
                catch (InsuranceException exception)
                {
                    _log.LogException(exception);
                    throw;
                }
            }


            // 缓存数据
            var cacheString = (await _redis.StringGetAsync(
                $"{_configuration["ServiceName"]}:Insurance:{patient.IdentityNo}:{patient.InsuplcAdmdvCode}")).ToString();
            // 从缓存中读取医保信息
            if (!string.IsNullOrEmpty(cacheString))
            {
                insuranceDto = JsonSerializer.Deserialize<InsuranceDto>(cacheString);
                _log.LogInformation("从缓存读取医保信息：{Insurance}", insuranceDto);

                if (!insuranceDto.InsuranceTypes.Any(x => x.clctstd_crtf_rule_codg == faber.ExtraCode))
                {
                    throw new Exception($"该患者不可使用医保费别【{faber.TriageConfigName}】");
                }
            }

            InsuranceType insuranceType =
                insuranceDto?.InsuranceTypes?.FirstOrDefault(x =>
                    x.clctstd_crtf_rule_codg == faber.ExtraCode);

            return (insuranceDto != null && insuranceType != null, insuranceDto, insuranceType);
        }


        /// <summary>
        /// 获取医保信息
        /// </summary>
        /// <param name="electronCertNo"></param>
        /// <param name="extraCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<InsuranceDto> GetInsuranceInfoByElectronCert(string electronCertNo, string extraCode)
        {
            try
            {
                var basicInfo = await Insurance.GetBasicInfo("01", electronCertNo, extraCode);
                _log.LogInformation($"电子医保凭证获取信息,GetInsuranceInfos,参数： {JsonSerializer.Serialize(basicInfo)}");

                if (!string.IsNullOrWhiteSpace(basicInfo.err_msg))
                {
                    throw new InsuranceException(basicInfo.err_msg);
                }

                return new InsuranceDto
                {
                    mdtrt_cert_no = electronCertNo,
                    psn_cert_type = basicInfo.output.baseinfo.psn_cert_type,
                    psn_no = basicInfo.output.baseinfo.psn_no,
                    certno = basicInfo.output.baseinfo.certno,
                    psn_name = basicInfo.output.baseinfo.psn_name,
                    brdy = basicInfo.output.baseinfo.brdy,
                    gend = basicInfo.output.baseinfo.gend,
                    naty = basicInfo.output.baseinfo.naty,
                    age = basicInfo.output.baseinfo.age
                };
            }
            catch (Exception e)
            {
                _log.LogWarning("【GetInsuranceInfoByElectronCert】", e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 查询医保信息（省医保）
        /// </summary>
        /// <param name="identityNo">身份证号码</param>
        /// <param name="insuplcAdmdvs">参保地医保区划</param>
        /// <returns></returns>
        private async Task<InsuranceDto> GetInsuranceInfos(string identityNo, string insuplcAdmdvs, string cert_type = "02")
        {
            var basicInfo = await Insurance.GetBasicInfo(cert_type, identityNo, insuplcAdmdvs);

            _log.LogInformation($"电子医保凭证获取信息,GetInsuranceInfos,参数： {JsonSerializer.Serialize(basicInfo)}");

            if (!string.IsNullOrWhiteSpace(basicInfo.err_msg))
            {
                throw new InsuranceException(basicInfo.err_msg);
            }

            if (basicInfo.output.insuinfo.Any())
            {
                insuplcAdmdvs = basicInfo.output.insuinfo[0].insuplc_admdvs;
            }

            List<InsuranceType> insuranceTypes = new List<InsuranceType>();

            string currentInsuplcAdmdvs = "440300"; // 深圳市
                                                    // 医保区划号省级前缀
            string provinceInsuplcAdmdvs = currentInsuplcAdmdvs[..2];
            // 医保区划号市级前缀
            string cityInsuplcAdmdvs = currentInsuplcAdmdvs[..4];
            // 判断是否跨省异地医保
            if (!insuplcAdmdvs.StartsWith(provinceInsuplcAdmdvs))
            {
                foreach (var insurance in basicInfo.output.insuinfo)
                {
                    // 龙岗中心HIS要求，使用9008作为跨省异地医保标识
                    insuranceTypes.Add(new InsuranceType
                    { clctstd_crtf_rule_codg = "9008", insutype = insurance.insutype });
                }
            }
            // 判断是否省内异地医保
            else if (!insuplcAdmdvs.StartsWith(cityInsuplcAdmdvs))
            {
                foreach (var insurance in basicInfo.output.insuinfo)
                {
                    // 龙岗中心HIS要求，使用9000作为省内异地医保标识
                    insuranceTypes.Add(new InsuranceType
                    { clctstd_crtf_rule_codg = "9000", insutype = insurance.insutype });
                }
            }
            // 查询医保缴费信息
            else
            {
                var payQuery =
                    await Insurance.GetPayQueryInfo(basicInfo.output.baseinfo.psn_no, insuplcAdmdvs);
                _log.LogInformation("医保信息：{BasicInfo}", JsonSerializer.Serialize(payQuery.output));
                if (!string.IsNullOrWhiteSpace(payQuery.err_msg))
                {
                    throw new InsuranceException(payQuery.err_msg);
                }

                if (payQuery.output != null)
                {
                    foreach (var insurance in payQuery.output)
                    {
                        if (!insuranceTypes.Exists(x =>
                                x.clctstd_crtf_rule_codg == insurance.clctstd_crtf_rule_codg &&
                                x.insutype == insurance.insutype))
                        {
                            insuranceTypes.Add(new InsuranceType
                            {
                                clctstd_crtf_rule_codg = insurance.clctstd_crtf_rule_codg,
                                insutype = insurance.insutype
                            });
                        }
                    }
                }
            }

            return new InsuranceDto
            {
                mdtrt_cert_no = identityNo,
                psn_cert_type = basicInfo.output.baseinfo.psn_cert_type,
                psn_no = basicInfo.output.baseinfo.psn_no,
                certno = basicInfo.output.baseinfo.certno,
                psn_name = basicInfo.output.baseinfo.psn_name,
                brdy = basicInfo.output.baseinfo.brdy,
                gend = basicInfo.output.baseinfo.gend,
                naty = basicInfo.output.baseinfo.naty,
                age = basicInfo.output.baseinfo.age,
                insuplc_admdvs = insuplcAdmdvs,
                InsuranceTypes = insuranceTypes,
            };
        }



        private async Task<JsonResult> PatientVitalSign(PatientInfo patient, RegisterInfo registerInfo)
        {
            // 未开启登记生命体体征时不做处理
            if (!_configuration.GetValue<bool>("HisApiSettings:EnablePatientVitalSign"))
                return JsonResult.Ok(msg: "不推送生命体征信息");
            if (registerInfo == null) return JsonResult.Ok(msg: "患者未挂号，不推送生命体征信息");

            var dicts =
                await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == patient.ConsequenceInfo.TriageDeptCode);
            if (deptConfig == null) return JsonResult.Ok(msg: "科室不存在");

            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var uri = _configuration["HisApiSettings:PatientVitalSign"];
            var signDetailReqs = new List<SignDetailReq>
            {
                // 体温
                new SignDetailReq
                {
                    itemCode = "1",
                    itemName = "体温",
                    itemValue = patient.VitalSignInfo.Temp,
                    unit = "℃",
                    signId = "1",
                },
                // 体重
                new SignDetailReq
                {
                    itemCode = "2",
                    itemName = "体重",
                    itemValue = patient.Weight,
                    unit = "kg",
                    signId = "2",
                },
                // 收缩压
                new SignDetailReq
                {
                    itemCode = "3",
                    itemName = "收缩压",
                    itemValue = patient.VitalSignInfo.Sbp,
                    unit = "mmHg",
                    signId = "3",
                },
                // 舒张压
                new SignDetailReq
                {
                    itemCode = "4",
                    itemName = "舒张压",
                    itemValue = patient.VitalSignInfo.Sdp,
                    unit = "mmHg",
                    signId = "4",
                },
                // 心率
                new SignDetailReq
                {
                    itemCode = "5",
                    itemName = "心率",
                    itemValue = patient.VitalSignInfo.HeartRate,
                    unit = "次/分",
                    signId = "5",
                },
                // 脉搏
                new SignDetailReq
                {
                    itemCode = "6",
                    itemName = "脉搏",
                    itemValue = patient.VitalSignInfo.HeartRate,
                    unit = "次/分",
                    signId = "6",
                },
                // 呼吸
                new SignDetailReq
                {
                    itemCode = "7",
                    itemName = "呼吸",
                    itemValue = patient.VitalSignInfo.BreathRate,
                    unit = "次/分",
                    signId = "7",
                },
                // 血氧
                new SignDetailReq
                {
                    itemCode = "8",
                    itemName = "血氧",
                    itemValue = patient.VitalSignInfo.SpO2,
                    unit = "%",
                    signId = "8",
                },
                // 血糖
                new SignDetailReq
                {
                    itemCode = "9",
                    itemName = "血糖",
                    itemValue = patient.VitalSignInfo.BloodGlucose?.ToString(),
                    unit = "mmol/L",
                    signId = "9",
                },
                // 意识
                new SignDetailReq
                {
                    itemCode = "10",
                    itemName = "意识",
                    itemValue = patient.ConsciousnessName,
                    unit = "",
                    signId = "10",
                },
                // 病情等级
                new SignDetailReq
                {
                    itemCode = "11",
                    itemName = "病情等级",
                    itemValue = "",
                    unit = "",
                    signId = "11",
                },
                // 分区
                new SignDetailReq
                {
                    itemCode = "12",
                    itemName = "分区",
                    itemValue = "",
                    unit = "",
                    signId = "12",
                },
                // 血氧饱和度
                new SignDetailReq
                {
                    itemCode = "13",
                    itemName = "血氧饱和度",
                    itemValue = patient.VitalSignInfo.SpO2,
                    unit = "%",
                    signId = "13",
                },
                // 是否已做心电图
                new SignDetailReq
                {
                    itemCode = "14",
                    itemName = "是否已做心电图",
                    itemValue = patient.VitalSignInfo.CardiogramName,
                    unit = "",
                    signId = "14",
                },
            };
            PatientVitalSign request = new PatientVitalSign
            {
                deptCode = deptConfig?.HisConfigCode,
                deptName = deptConfig?.TriageConfigCode,
                patientGender = patient.Sex switch
                {
                    "Sex_Man" => 1,
                    "Sex_Woman" => 2,
                    _ => 3
                },
                patientName = patient.PatientName,
                patientType = "1",
                recorderCode = _currentUser.UserName,
                recorderName = _currentUser.GetFullName(),
                regDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                regSerialNo = registerInfo.RegisterNo,
                visitNo = patient.VisitNo,
                visitSerialNo = "",
                signDetailReqs = signDetailReqs
            };

            var responseText = await PostAsync(httpClient, uri, request, "调用HIS接口登记生命体征信息");
            CommonHttpResult result;
            try
            {
                result = JsonSerializer.Deserialize<CommonHttpResult>(responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                if (result.Code == 0)
                {
                    return JsonResult.Ok();
                }
                else
                {
                    return JsonResult.Fail(msg: result.Msg);
                }
            }
            catch (Exception)
            {
                _log.LogInformation("调用HIS接口登记生命体征信息，Url: {Url}, Request：{Request}, Response: {Response}",
                    httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);
                //throw new Exception("无法正常处理 HIS 接口返回数据");
                return JsonResult.Fail("无法正常处理 HIS 接口返回数据");
            }
        }

        /// <summary>
        /// 确认分诊前的一些业务逻辑处理（可包括业务检验）
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="patient"></param>
        /// <param name="isUpdated"></param>
        /// <param name="hasChangedDoctor"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PatientInfo> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo patient,
            bool isUpdated, bool hasChangedDoctor)
        {

            //if (!string.IsNullOrEmpty(patient.ElectronCertNo))
            //{
            //    // 医保区划
            //    var insuplcAdmdv = await this._triageConfigRepository.AsNoTracking()
            //        .Where(x => x.TriageConfigType == (int)TriageDict.InsuplcAdmdv)
            //        // 传递了医保区划编码，则查询医保区划编码
            //        .WhereIf(!string.IsNullOrWhiteSpace(dto.InsuplcAdmdvCode), x => x.TriageConfigCode == dto.InsuplcAdmdvCode)
            //        // 如果没有传递医保区划编码，则查询默认的“深圳市”
            //        .WhereIf(string.IsNullOrWhiteSpace(dto.InsuplcAdmdvCode), x => x.ExtraCode == "440300")
            //        .FirstOrDefaultAsync();

            //    //获取医保电子凭证信息
            //    var basicInfo = await GetInsuranceInfoByElectronCert(patient.ElectronCertNo, insuplcAdmdv.ExtraCode);
            //    _log.LogInformation($"BeforeSaveTriageRecordAsync 电子医保凭证获取信息,凭证号：{patient.ElectronCertNo}人员信息: {JsonSerializer.Serialize(basicInfo)}");
            //}


            if (Encoding.ASCII.GetByteCount(dto.PatientName) > 20)
            {
                throw new Exception($"姓名长度超出限制，请修改姓名");
            }
            //必填字段前端未限制 为空 后端校验不让提交
            if (string.IsNullOrEmpty(dto.ChargeType))
            {
                throw new Exception($"患者费别不能为空");
            }
            var patientExists = await _patientInfoRepository.Include(c => c.ConsequenceInfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == patient.Id);
            // 已确认分诊，不允许修改分诊科室
            if (patientExists is { TriageStatus: 1 } && patientExists.ConsequenceInfo?.TriageDeptCode != patient.ConsequenceInfo.TriageDeptCode)
            {
                throw new Exception($"不允许修改分诊科室，若要修改分诊科室请作废后重新分诊");
            }

            // 兼容旧数据：如果建档的时候没有社保卡号，但再次就诊的时候有社保卡号，则使用社保卡号赋值
            if (string.IsNullOrWhiteSpace(patient.MedicalNo) && !string.IsNullOrWhiteSpace(dto.MedicalNo))
            {
                patient.MedicalNo = dto.MedicalNo;
            }

            return patient;
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
        public Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate)
        {
            return Task.FromResult(JsonResult<List<DoctorSchedule>>.Ok(data: new List<DoctorSchedule>()));
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
        /// <param name="regSerialNo">挂号流水号</param>
        /// <returns></returns>
        public async Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordAsync(string idType, string identityNo,
            string visitNo, string patientName, string phone = "", string regSerialNo = "")
        {
            List<PatientInfoFromHis> res = new List<PatientInfoFromHis>();
            var uri = _configuration["HisApiSettings:getPatientInfo"] +
                      $"?idNo={identityNo}&idType={idType}&visitNo={visitNo}&name={patientName}&phone={phone}&regSerialNo={regSerialNo}";


            try
            {
                var httpClient = _httpClientFactory.CreateClient("HisApi");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();
                _log.LogInformation("调用平台接口查询患者信息，url: {Uri}，response: {ResponseText}", uri, responseText);
                if (string.IsNullOrWhiteSpace(responseText))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "查询患者信息接口响应为空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("调用查询患者信息接口失败！请检查后重试");
                }

                var json = JObject.Parse(responseText);
                if (json["code"]?.ToString() != "0")
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", json["msg"]);
                    return JsonResult<List<PatientInfoFromHis>>.Fail("调用查询患者信息接口失败，" + json["msg"]);
                }

                if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
                {
                    _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "调用查询患者信息接口失败。返回Data为null或空");
                    return JsonResult<List<PatientInfoFromHis>>.Fail("查询患者信息失败！请检查后重试");
                }

                // // 返回单条数据
                // var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString(),
                //     new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                // 返回多条数据
                var resp = JsonSerializer.Deserialize<List<PatientRespDto>>(json["data"].ToString(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                res = resp.BuildAdapter().AdaptToType<List<PatientInfoFromHis>>();

                foreach (var patientOutput in res)
                {
                    var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
                    if (!string.IsNullOrWhiteSpace(patientOutput.IdTypeCode))
                    {
                        var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                            .FirstOrDefault(x => x.HisConfigCode == patientOutput.IdTypeCode);
                        if (idTypeConfig != null)
                        {
                            patientOutput.IdTypeCode = idTypeConfig.TriageConfigCode;
                            patientOutput.IdTypeName = idTypeConfig.TriageConfigName;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(patientOutput.GuardianIdTypeCode))
                    {
                        var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                            .FirstOrDefault(x => x.HisConfigCode == patientOutput.GuardianIdTypeCode);
                        if (idTypeConfig != null)
                        {
                            patientOutput.GuardianIdTypeCode = idTypeConfig.TriageConfigCode;
                            patientOutput.GuardianIdTypeName = idTypeConfig.TriageConfigName;
                        }
                    }

                    // // 信息科约定 patientId 返回的值就是 visitNo
                    // patientOutput.VisitNo = patientOutput.PatientId;
                    // 默认医保参保地【深圳】
                    var insuplcAdmdv = dicts[TriageDict.InsuplcAdmdv.ToString()]
                        .FirstOrDefault(x => x.ExtraCode == "440300");
                    patientOutput.InsuplcAdmdvCode = insuplcAdmdv?.TriageConfigCode;

                    // res = new List<PatientInfoFromHis>
                    // {
                    //     patientOutput
                    // };
                    patientOutput.Sex = patientOutput.Sex switch
                    {
                        "M" => "Sex_Man",
                        "F" => "Sex_Woman",
                        _ => "Sex_Unknown"
                    };
                }

                return JsonResult<List<PatientInfoFromHis>>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<List<PatientInfoFromHis>>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 查询门诊患者信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<PatientInfoFromHis> GetPatienInfoBytIdAsync(string patientId)
        {
            var uri = _configuration["HisApiSettings:getPatientInfo"] +
                      $"?patientId={patientId}";
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("调用平台接口查询患者信息，url: {Uri}，reponse: {ResponseText}", uri, responseText);
            if (string.IsNullOrWhiteSpace(responseText))
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "查询患者信息接口响应为空");
                throw new HisResponseException("调用查询患者信息接口失败！请检查后重试");
            }

            var json = JObject.Parse(responseText);
            if (json["code"]?.ToString() != "0")
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", json["msg"]);
                throw new HisResponseException("调用查询患者信息接口失败，" + json["msg"]);
            }

            if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
            {
                _log.LogError("根据输入项获取患者病历号失败！原因：{Msg}", "调用查询患者信息接口失败。返回Data为null或空");
                throw new HisResponseException("查询患者信息失败！请检查后重试");
            }

            // 返回多条数据
            var resp = JsonSerializer.Deserialize<List<PatientRespDto>>(json["data"].ToString(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var res = resp.BuildAdapter().AdaptToType<List<PatientInfoFromHis>>();

            foreach (var patientOutput in res)
            {
                var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
                if (!string.IsNullOrWhiteSpace(patientOutput.IdTypeCode))
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                        .FirstOrDefault(x => x.HisConfigCode == patientOutput.IdTypeCode);
                    if (idTypeConfig != null)
                    {
                        patientOutput.IdTypeCode = idTypeConfig.TriageConfigCode;
                        patientOutput.IdTypeName = idTypeConfig.TriageConfigName;
                    }
                }

                if (!string.IsNullOrWhiteSpace(patientOutput.GuardianIdTypeCode))
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                        .FirstOrDefault(x => x.HisConfigCode == patientOutput.GuardianIdTypeCode);
                    if (idTypeConfig != null)
                    {
                        patientOutput.GuardianIdTypeCode = idTypeConfig.TriageConfigCode;
                        patientOutput.GuardianIdTypeName = idTypeConfig.TriageConfigName;
                    }
                }

                // 默认医保参保地【深圳】
                var insuplcAdmdv = dicts[TriageDict.InsuplcAdmdv.ToString()]
                    .FirstOrDefault(x => x.ExtraCode == "440300");
                patientOutput.InsuplcAdmdvCode = insuplcAdmdv?.TriageConfigCode;
                patientOutput.Sex = patientOutput.Sex switch
                {
                    "M" => "Sex_Man",
                    "F" => "Sex_Woman",
                    _ => "Sex_Unknown"
                };
            }

            return res.FirstOrDefault();
        }

        /// <summary>
        /// 患者建档接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordAsync(CreateOrGetPatientIdInput input)
        {

            if (Encoding.ASCII.GetByteCount(input.PatientName) > 20)
            {
                throw new Exception($"姓名长度超出限制，请修改姓名");
            }
            // 证件类型
            TriageConfigDto idType = await GetIdTypeByTriageConfigCode(input.IdTypeCode ?? "IdType_01");
            // 联系人证件类型
            TriageConfigDto guardianIdType = await GetGuardianIdTypeByTriageConfigCode(input.GuardianIdTypeCode);
            // 与联系人关系
            var societyRelation = await GetSocietyRelation(input.SocietyRelationCode);
            var uri = _configuration["HisApiSettings:buildPatientArchives"];
            var req = new PatientReqDto
            {
                name = input.PatientName,
                idNo = input.IdentityNo ?? "",
                idType = input.CardType.ToString(),
                birthday = input.Birthday?.ToString("yyyy/MM/dd"),
                phone = input.ContactsPhone ?? "",
                homeAddress = input.Address ?? "",
                nationality = input.Nation,
                contactName = input.ContactsPerson ?? "",
                sex = input.Sex switch
                {
                    "Sex_Man" => "M",
                    "Sex_Woman" => "F",
                    _ => "U"
                },
                patIdType = idType?.HisConfigCode,
                cardNo = input.CardNo ?? "",
                crowdCode = input.CrowdCode,
                associationName = input.ContactsPerson ?? "",
                associationIdType = guardianIdType?.HisConfigCode ?? "",
                associationIdNo = input.GuardianIdCardNo ?? "",
                associationPhone = input.GuardianPhone ?? "",
                associationAddress = input.ContactsAddress ?? "",
                societyRelation = societyRelation?.HisConfigCode ?? "",
            };

            try
            {
                var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.IdType.ToString());
                var res = await this.CreatePatientRecord(uri, req);
                if (string.IsNullOrWhiteSpace(res.PatientName))
                {
                    res.PatientName = input.PatientName;
                }

                if (string.IsNullOrWhiteSpace(res.Sex))
                {
                    res.Sex = input.Sex;
                }
                else
                {
                    res.Sex = res.Sex switch
                    {
                        "M" => "Sex_Man",
                        "F" => "Sex_Woman",
                        _ => "Sex_Unknown"
                    };
                }

                res.Birthday ??= input.Birthday;
                res.IdTypeCode = input.IdTypeCode;
                if (string.IsNullOrWhiteSpace(res.IdentityNo))
                {
                    res.IdentityNo = input.IdentityNo;
                }

                // // 信息科约定 patientId 返回的值就是 visitNo
                // res.VisitNo = res.PatientId;
                //if (input.CardType == 2)
                // 建档默认身份证
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()].FirstOrDefault(x => x.TriageConfigCode == res.IdTypeCode);
                    if (idTypeConfig != null)
                    {
                        res.IdTypeCode = idTypeConfig.TriageConfigCode;
                        res.IdTypeName = idTypeConfig.TriageConfigName;
                    }
                }
                // 查询本地对应的监护人身份证号码
                if (!string.IsNullOrWhiteSpace(input.GuardianIdTypeCode))
                {
                    var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                        .FirstOrDefault(x => x.HisConfigCode == res.GuardianIdTypeCode);
                    if (idTypeConfig != null)
                    {
                        res.GuardianIdTypeCode = idTypeConfig.TriageConfigCode;
                        res.GuardianIdTypeName = idTypeConfig.TriageConfigName;
                    }
                }

                return JsonResult<PatientInfoFromHis>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<PatientInfoFromHis>.Fail(ex.Message, new PatientInfoFromHis());
            }
        }

        /// <summary>
        /// 三无患者建档
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<PatientInfoFromHis>> CreateNoThreePatientRecordAsync(
            CreateOrGetPatientIdInput input)
        {
            var uri = _configuration["HisApiSettings:buildNoThreePatientArchives"];
            // 三无患者建档，是否不需要身份证号码
            var buildNoThreePatientWithoutIdCardNo =
                bool.TryParse(_configuration["HisApiSettings:buildNoThreePatientWithoutIdCardNo"],
                    out bool buildNoThreePatientWithoutIdCardNoValue)
                && buildNoThreePatientWithoutIdCardNoValue;
            var nowDate = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000)
                           / 10000000)
                .ToString();
            // 证件类型
            TriageConfigDto idType = await GetIdTypeByTriageConfigCode("IdType_01");
            // 联系人证件类型
            TriageConfigDto guardianIdType = await GetGuardianIdTypeByTriageConfigCode(input.GuardianIdTypeCode);
            // 与联系人关系
            var societyRelation = await GetSocietyRelation(input.SocietyRelationCode);
            input.IdentityNo = buildNoThreePatientWithoutIdCardNo ? null : "Y" + nowDate;

            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.Sex.ToString());
            // 龙岗中心限制姓名是varchar(20)，所以有此限制
            DateTime startDate = new DateTime(1970, 1, 1);
            input.PatientName = (input.PatientName.IsNullOrWhiteSpace() || input.PatientName.Contains("无名氏"))
                ? "无名氏_" + (long)(DateTime.Now - startDate).TotalSeconds +
                  dicts.GetNameByDictCode(TriageDict.Sex, input.Sex)
                : input.PatientName;
            var req = new PatientReqDto
            {
                name = input.PatientName ?? "",
                idNo = input.IdentityNo ?? "",
                idType = input.CardType.ToString(),
                birthday = input.Birthday?.ToString("yyyy/MM/dd"),
                phone = input.ContactsPhone ?? "/",
                homeAddress = input.Address ?? "/",
                nationality = input.Nation,
                contactName = input.ContactsPerson,
                sex = input.Sex switch
                {
                    "Sex_Man" => "M",
                    "Sex_Woman" => "F",
                    _ => "U"
                },
                patIdType = idType?.HisConfigCode,
                cardNo = input.CardNo ?? "",
                crowdCode = "Crowd_NoThree",
                associationName = input.ContactsPerson ?? "",
                associationIdType = guardianIdType?.HisConfigCode ?? "",
                associationIdNo = input.GuardianIdCardNo ?? "",
                associationPhone = input.GuardianPhone ?? "",
                associationAddress = input.ContactsAddress ?? "",
                societyRelation = societyRelation?.HisConfigCode ?? "",
            };

            try
            {
                var res = await this.CreatePatientRecord(uri, req);
                if (string.IsNullOrWhiteSpace(res.PatientName))
                {
                    res.PatientName = input.PatientName;
                }

                if (string.IsNullOrWhiteSpace(res.IdentityNo))
                {
                    res.IdentityNo = input.IdentityNo;
                }

                res.IdTypeCode = input.IdTypeCode;
                res.Sex = input.Sex;
                res.Birthday ??= input.Birthday;
                // // 信息科约定 patientId 返回的值就是 visitNo
                // res.VisitNo = res.PatientId;
                return JsonResult<PatientInfoFromHis>.Ok(data: res);
            }
            catch (Exception ex)
            {
                return JsonResult<PatientInfoFromHis>.Fail(ex.Message, data: new PatientInfoFromHis { });
            }
        }

        private async Task<TriageConfigDto> GetIdTypeByTriageConfigCode(string idTypeCode)
        {
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.IdType.ToString());
            var idType = dicts[TriageDict.IdType.ToString()].FirstOrDefault(x => x.TriageConfigCode == idTypeCode);
            if (idType == null)
            {
                throw new Exception("证件类型不存在");
            }

            if (string.IsNullOrEmpty(idType.HisConfigCode))
            {
                throw new Exception("证件类型未设置对应的HIS编码");
            }

            return idType;
        }

        private async Task<TriageConfigDto> GetGuardianIdTypeByTriageConfigCode(string guardianIdTypeCode)
        {
            if (string.IsNullOrEmpty(guardianIdTypeCode)) return null;
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.IdType.ToString());
            var guardianIdType = dicts[TriageDict.IdType.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == guardianIdTypeCode);
            if (guardianIdType == null)
            {
                throw new Exception("联系人证件类型不存在");
            }

            if (string.IsNullOrEmpty(guardianIdType?.HisConfigCode))
            {
                throw new Exception("联系人证件类型未设置对应的HIS编码");
            }

            return guardianIdType;
        }

        private async Task<TriageConfigDto> GetSocietyRelation(string societyRelationCode)
        {
            if (string.IsNullOrEmpty(societyRelationCode))
            {
                return null;
            }

            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
            // 联系人证件类型
            var societyRelation = dicts[TriageDict.SocietyRelation.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == societyRelationCode);
            if (societyRelation == null)
            {
                throw new Exception("与联系人关系不存在");
            }

            if (string.IsNullOrEmpty(societyRelation?.HisConfigCode))
            {
                throw new Exception("与联系人关系未设置对应的HIS编码");
            }

            return societyRelation;
        }

        /// <summary>
        /// 普通建档，三无建档通用接口
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<PatientInfoFromHis> CreatePatientRecord(string uri, PatientReqDto req)
        {
            _log.LogInformation("根据输入项创建患者病历号，建档入参：{Json}", JsonHelper.SerializeObject(req));
            // var content = new StringContent(JsonSerializer.Serialize(req));
            var content = new StringContent(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            //#if !DEBUG
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var response = await httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("根据输入项创建患者病历号，建档返回：{Json}", responseText);
            //#else
            //            var responseText = @"{""code"":0,""msg"":null,""data"":{""patientId"":""364039"",""patientName"":""孙春伟"",""identifyNo"":""371521198410264915"",""cardType"":""1"",""cardNum"":""0005070435"",""phoneNumberHome"":""13800138222"",""sex"":""M"",""accountBalance"":0.0,""setPassword"":1,""createDate"":""2022-01-14 10:01:29"",""useTime"":0}}";
            //#endif
            if (string.IsNullOrWhiteSpace(responseText))
            {
                throw new Exception("调用患者建档接口响应为null或空");
            }

            var json = JObject.Parse(responseText);
            if (json["code"]?.ToString() != "0")
            {
                throw new Exception("调用患者建档接口失败，" + json["msg"]);
            }

            if (json["data"] == null || string.IsNullOrWhiteSpace(json["data"].ToString()))
            {
                throw new Exception("返回Data为null或空");
            }

            var resp = JsonSerializer.Deserialize<PatientRespDto>(json["data"].ToString(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var res = resp.BuildAdapter().AdaptToType<PatientInfoFromHis>();
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.InsuplcAdmdv.ToString());
            // 默认医保参保地【深圳】
            var insuplcAdmdv = dicts[TriageDict.InsuplcAdmdv.ToString()].FirstOrDefault(x => x.ExtraCode == "440300");
            res.InsuplcAdmdvCode = insuplcAdmdv?.TriageConfigCode;

            return res;
        }

        /// <summary>
        /// 建档前验证
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isInfant"></param>
        /// <returns></returns>
        public Task<JsonResult> ValidateBeforeCreatePatient(CreateOrGetPatientIdInput input, out bool isInfant)
        {
            isInfant = input.IsInfant;
            List<string> validateErrors = new List<string>();
            if (input.IsNoThree)
            {
                if (!input.Birthday.HasValue)
                {
                    // 龙岗中心三无建档要求生日
                    input.Birthday = DateTime.Today;
                }
            }
            else if (input.CrowdCode == "Crowd_Child" && string.IsNullOrEmpty(input.IdentityNo))
            {
                // 儿童且没有身份证号码的，需要联系人
                isInfant = true;
                input.Birthday ??= DateTime.Today;

                if ((string.IsNullOrEmpty(input.GuardianPhone) || string.IsNullOrEmpty(input.ContactsPerson) ||
                     string.IsNullOrEmpty(input.GuardianIdCardNo)))
                {
                    validateErrors.Add("该患者为儿童，需输入联系人姓名、电话、证件号码");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(input.IdTypeCode))
                {
                    validateErrors.Add("证件类型必填");
                }

                if (string.IsNullOrEmpty(input.IdentityNo))
                {
                    validateErrors.Add("身份证号码必填");
                }

                if (string.IsNullOrEmpty(input.ContactsPhone))
                {
                    validateErrors.Add("电话号码必填");
                }

                if (string.IsNullOrEmpty(input.Address))
                {
                    validateErrors.Add("住址必填");
                }
            }

            // TODO: 已挂号的患者不允许修改分诊科室
            if (validateErrors.Any())
            {
                return Task.FromResult(JsonResult.Fail(string.Join(", ", validateErrors)));
            }

            return Task.FromResult(JsonResult.Ok());
        }

        /// <summary>
        /// 取消挂号信息
        /// </summary>
        /// <param name="regSerialNo">挂号流水号</param>
        /// <returns></returns>
        public async Task<JsonResult> CancelRegisterInfoAsync(string regSerialNo)
        {
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            _log.LogInformation("调用HIS取消挂号接口, url: {Url}, regSerialNo：{RegSerialNo}",
                httpClient.BaseAddress + _configuration["HisApiSettings:cancelRegisterInfo"],
                JsonHelper.SerializeObject(regSerialNo));
            var httpResponseMessage = await httpClient.GetAsync(_configuration["HisApiSettings:cancelRegisterInfo"] +
                                                                $"?regSerialNo={regSerialNo}");
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"调用HIS取消挂号接口失败，HttpCode: {(int)httpResponseMessage.StatusCode}");
            }

            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseString);
            if (json["code"]?.ToString() != "0")
            {
                string msg = json["msg"]?.ToString();
                throw new Exception($"调用HIS取消挂号接口失败, 错误信息: {msg}");
            }

            return JsonResult.Ok();
        }

        public Task<JsonResult<VitalSignsInfoByJinWan>> GetHisVitalSignsAsync(string serialNumber)
        {
            throw new NotImplementedException();
        }

        private async Task ModifyRegInformations(string doctorCode, string doctorName, PatientInfo patientReadonly)
        {
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var uri = _configuration["HisApiSettings:ModifyRegInfo"];
            var dicts =
                await _triageConfigAppService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment.ToString());
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()].FirstOrDefault(x =>
                x.TriageConfigCode == patientReadonly.ConsequenceInfo.TriageDeptCode);
            ModifyRegInfoReq request = new ModifyRegInfoReq
            {
                visitNo = patientReadonly.VisitNo,
                regSerialNo = patientReadonly.RegisterInfo.OrderBy(x => x.RegisterTime).FirstOrDefault()?.RegisterNo,
                //regSerialNo = "",
                deptCode = deptConfig.HisConfigCode,
                deptName = deptConfig.TriageConfigName,
                doctorCode = doctorCode,
                doctorName = doctorName,
                roomCode = "",
                roomName = "",
            };
            var responseText = await PostAsync(httpClient, uri, request, "调用HIS接口修改挂号信息");
            CommonHttpResult result;
            try
            {
                result = JsonSerializer.Deserialize<CommonHttpResult>(responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }
            catch (Exception)
            {
                _log.LogInformation("调用HIS接口修改挂号信息，Url: {Url}, Request：{Request}, Response: {Response}",
                    httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);
                throw new Exception("无法正常处理 HIS 接口返回数据");
            }

            if (result.Code != 0)
            {
                throw new Exception($"HIS接口返回错误：{result.Msg}");
            }
        }

        private async Task<string> PostAsync<T>(HttpClient httpClient, string uri, T request, string apiComment)
        {
            _log.LogInformation("，Url: {Url}, Request：{Json}", httpClient.BaseAddress + uri,
                JsonHelper.SerializeObject(request));
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await TransientErrorRetryPolicy.ExecuteAsync(() => httpClient.PostAsync(uri, content));
            }
            catch (Exception exception)
            {
                _log.LogInformation("{ApiComment}, Message: {Message}", apiComment, exception.Message);
                throw new Exception("HIS 接口无法正常连接");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("HIS 接口无法连接");
            }

            var responseText = await response.Content.ReadAsStringAsync();
            _log.LogInformation("{ApiComment}，Url: {Url}, Request：{Request}, Response: {Response}", apiComment,
                httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);

            return responseText;
        }

        /// <summary>
        /// 修改患者档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult> RevisePerson(PatientModifyDto input)
        {
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var uri = _configuration["HisApiSettings:RevisePerson"];
            var dicts = await _triageConfigAppService.GetTriageConfigByRedisAsync();
            // 证件类型
            var idTypeConfig = dicts[TriageDict.IdType.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == input.IdTypeCode);
            RevisePersonRequest request = new RevisePersonRequest
            {
                patientId = input.PatientId,
                visitNo = input.VisitNo,
                name = input.PatientName,
                gender = input.Sex switch
                {
                    "Sex_Man" => "1",
                    "Sex_Woman" => "2",
                    _ => "0"
                },
                genderName = input.Sex switch
                {
                    "Sex_Man" => "男性",
                    "Sex_Woman" => "女性",
                    _ => "未知"
                },
                addrDetail = input.Address,
                birthDate = DateTime.TryParse(input.BirthDay, out DateTime birthDate)
                    ? birthDate.ToString("yyyy-MM-dd")
                    : null,
                contactType = "01",
                content = input.ContactsPhone,
                idType = idTypeConfig?.HisConfigCode,
                idTypeName = idTypeConfig?.TriageConfigName,
                idCode = input.IdentityNo,
            };
            var responseText = await PostAsync(httpClient, uri, request, "调用HIS接口修改患者信息");
            CommonHttpResult result;
            try
            {
                result = JsonSerializer.Deserialize<CommonHttpResult>(responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                if (result.Code == 0)
                {
                    return JsonResult.Ok();
                }
                else
                {
                    return JsonResult.Fail(msg: result.Msg);
                }
            }
            catch (Exception)
            {
                _log.LogInformation("调用HIS接口修改患者信息，Url: {Url}, Request：{Request}, Response: {Response}",
                    httpClient.BaseAddress + uri, JsonHelper.SerializeObject(request), responseText);
                throw new Exception("无法正常处理 HIS 接口返回数据");
            }
        }

        /// <summary>
        /// 获取护士列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync()
        {
            return await _commonHisApi.GetNurseScheduleAsync();
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

        /// <summary>
        /// 获取流调表
        /// </summary>
        /// <param name="showType">HIS证件类型（01 居民身份证  03 护照  04 军官证  06 港澳居民来往内地通行证  07 台湾居民来往内地通行证）</param>
        /// <param name="idCardNo">证件号码</param>
        /// <returns></returns>
        public async Task<QuestionnaireData> GetQuestionnaireAsync(string showType, string idCardNo)
        {
            if (string.IsNullOrWhiteSpace(showType)) throw new ArgumentException("云签证件类型不能为空");
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            string cid = _configuration["LDC:Questionnarie:ChannelId"];
            var uri = _configuration["LDC:Questionnarie:Url"] + $"?cardType={showType}&cardNo={idCardNo}";
            var (timestamp, signature) = this.GetHmacsha256();

            _log.LogInformation("查询流调表信息，Url: {Url}", uri);

            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            //request.Headers.Add("Content-Type", "application/json;charset=UTF-8");
            // 特定的 header
            request.Headers.Add("god-portal-channelid", cid);
            request.Headers.Add("god-portal-timestamp", timestamp);
            request.Headers.Add("god-portal-signature", signature);
            HttpResponseMessage response;
            try
            {
                // response = await TransientErrorRetryPolicy.ExecuteAsync(() => httpClient.SendAsync(request));
                response = await httpClient.SendAsync(request);
            }
            catch (Exception exception)
            {
                _log.LogInformation("HIS 接口无法正常连接, Message: {Message}", exception.Message);
                throw new Exception($"HIS 接口无法正常连接, Message: {exception.Message}");
            }

            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var result =
                Newtonsoft.Json.JsonConvert.DeserializeObject<QuestionnaireResponse<QuestionnaireData>>(responseText);
            if (result != null && result.ResultCode != 1)
            {
                throw new Exception(result.ErrorMsg);
            }

            return result?.Data;
        }

        /// <summary>
        /// 通过二维码查询流调表
        /// </summary>
        /// <param name="barcode">二维码字符串</param>
        /// <returns></returns>
        public async Task<QuestionnaireData> GetQuestionnaireAsync(string barcode)
        {
            //string aec_key = "tH9PX0WioTHEAFpS";
            //string aec_iv = "BqgnJom2jCH6IvGU";
            string aecKey = _configuration["LDC:Questionnarie:AecKey"];
            string aecIv = _configuration["LDC:Questionnarie:AecIv"];
            string jsonResult = this.AesDecrypt(barcode, aecKey, aecIv);
            QuestionnaireBarcode barcodeInfo =
                Newtonsoft.Json.JsonConvert.DeserializeObject<QuestionnaireBarcode>(jsonResult);

            return await this.GetQuestionnaireAsync(barcodeInfo.ShowType, barcodeInfo.Card);
        }

        /// <summary>
        /// 获取base64签名
        /// </summary>
        /// <param name="empCode">工号</param>
        /// <returns></returns>
        public async Task<JsonResult<string>> GetStampBase64Async(string empCode)
        {
            empCode = empCode.Length < 4 ? empCode.PadLeft(4, '0') : empCode;
            var httpClient = _httpClientFactory.CreateClient();
            var uri = _configuration["LDC:CA:Getstamp"];
            var requestBody = new
            {
                relBizNo = empCode
            };
            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content =
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _log.LogInformation("获取云签base64签名，Url: {Url}, Body: {Json}", uri, requestBody);

            HttpResponseMessage response;
            try
            {
                response = await TransientErrorRetryPolicy.ExecuteAsync(() => httpClient.SendAsync(request));
            }
            catch (Exception exception)
            {
                _log.LogInformation("获取云签base64签名失败, Message: {Message}", exception.Message);
                throw new Exception($"获取云签base64签名失败, Message: {exception.Message}");
            }

            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            //string responseText = @"{
            //        ""eventMsg"": ""获取签章图片成功."",
            //        ""eventValue"": {
            //            ""stampBase64"": ""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""
            //        },
            //        ""statusCode"": 0
            //    }";
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<StampResponse>(responseText);
            if (result?.StatusCode != 0)
            {
                throw new Exception(result?.EventMsg);
            }

            return JsonResult<string>.Ok(msg: result.EventMsg, data: result.EventValue?.StampBase64);
        }

        /// <summary>
        /// 查询挂号列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<JsonResult<List<RegisterInfoHisDto>>> GetRegisterInfoListAsync(RegisterInfoInput input)
        {
            var httpClient = _httpClientFactory.CreateClient("HisApi");
            var uri = _configuration["HisApiSettings:GetRegisterInfo"] + $"?endDate={input.EndDate}&startDate={input.StartDate}&visitNum={input.VisitNum}&patientId={input.PatientId}";
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Content =
                new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
            _log.LogInformation("查询挂号信息，Url: {Url}", uri);
            HttpResponseMessage response;
            try
            {
                response = await TransientErrorRetryPolicy.ExecuteAsync(() => httpClient.SendAsync(request));
            }
            catch (Exception exception)
            {
                _log.LogInformation("查询挂号信息失败, Message: {Message}", exception.Message);
                throw new Exception($"查询挂号信息失败, Message: {exception.Message}");
            }
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var result =
                Newtonsoft.Json.JsonConvert.DeserializeObject<CommonHttpResult<List<RegisterInfoHisDto>>>(responseText);
            if (result?.Code != 0)
            {
                throw new Exception(result?.Msg);
            }
            return JsonResult<List<RegisterInfoHisDto>>.Ok(msg: result.Msg, data: result.Data);
        }
        /// <summary>
        /// 签名参数
        /// </summary>
        /// <returns></returns>
        private (string, string) GetHmacsha256()
        {
            //string cid = "LGZXYY";
            //string token = "ll8sr1ph9tj5oe4y";
            string cid = _configuration["LDC:Questionnarie:ChannelId"];
            string token = _configuration["LDC:Questionnarie:Token"];
            var encoding = new System.Text.UTF8Encoding();
            var key = encoding.GetBytes(token);
            //获取当前时间截
            string timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();
            //消息拼接
            string message = cid + timestamp;
            //获取签名值
            byte[] messageBytes = encoding.GetBytes(message);
            using HMACSHA256 hmacSHA256 = new HMACSHA256(key);
            byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
            // 签名
            var signature = BitConverter.ToString(hashMessage).Replace("-", "").ToLower();

            return (timestamp, signature);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private string AesDecrypt(string str, string key, string IV)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.Zeros;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(str);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = Encoding.UTF8.GetBytes(IV);
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static int GetStrLength(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            var ascii = new ASCIIEncoding();
            int len = 0;
            var s = ascii.GetBytes(str);
            foreach (var t in s)
            {
                if (t == 63)
                    len += 2;
                else
                    len += 1;
            }
            return len;
        }
        public Task<List<PatientRespDto>> GetRegisterInfoAsync(HisRegisterInfoQueryDto hisRegisterInfoQueryDto)
        {
            throw new NotImplementedException();
        }

        public Task<HisResponseDto> payCurRegisterAsync(string visitNum)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(
            GetRegisterPagedListInput input)
        {
            return await _commonHisApi.GetRegisterPagedListAsync(input);
        }

        public Task<JsonResult> ReturnToNoTriage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}