using DotNetCore.CAP;
using Mapster;
using MasterDataService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Patient.Application.Service.HospitalApplyRecord.PKU.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.BackgroundJob;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Users;
using YiJian.ECIS.DDP.Apis;

namespace Szyjian.Ecis.Patient.Application.Hospital
{
    /// <summary>
    /// PKU: 北京大学深圳医院（Peking University Shenzhen Hospital）接口服务
    /// </summary>
    [RemoteService(false)]
    public class PKUspecificApi : IHospitalApi
    {
        private const string HisUser = "his_users";
        private readonly IFreeSql _freeSql;
        private readonly PKUApiService _pkuApiService;
        private readonly ILogger<PKUspecificApi> _logger;
        private readonly ICapPublisher _capPublisher;
        private readonly ICurrentUser _currentUser;
        private readonly IHisClientAppService _hisClientAppService;
        private readonly IDistributedCache<List<His_Users>> _cache;
        private readonly IHisUserService _hisUserService;
        private readonly IDiagnoseRecordRepository _diagnoseRecordRepository;
        private readonly ICallApi _callService;
        /// <summary>
        /// 急诊科代码
        /// </summary>
        private const string DEPT_CODE = "280";

        private const string DEPT_NAME = "急诊科";

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pkuApiService"></param>
        /// <param name="logger"></param>
        /// <param name="freeSql"></param>
        /// <param name="capPublisher"></param>
        /// <param name="currentUser"></param>
        /// <param name="grpcMasterDataClient"></param>
        /// <param name="hisClientAppService"></param>
        /// <param name="cache"></param>
        /// <param name="hisUserService"></param>
        /// <param name="admissionRecordRepository"></param>
        /// <param name="diagnoseRecordRepository"></param>
        public PKUspecificApi(PKUApiService pkuApiService
            , ILogger<PKUspecificApi> logger
            , IFreeSql freeSql
            , ICapPublisher capPublisher
            , ICurrentUser currentUser
            , IHisClientAppService hisClientAppService
            , IDistributedCache<List<His_Users>> cache
            , IHisUserService hisUserService
            , IDiagnoseRecordRepository diagnoseRecordRepository
            , ICallApi callService)
        {
            _pkuApiService = pkuApiService;
            _logger = logger;
            _freeSql = freeSql;
            _capPublisher = capPublisher;
            _currentUser = currentUser;
            _hisClientAppService = hisClientAppService;
            _cache = cache;
            _hisUserService = hisUserService;
            _diagnoseRecordRepository = diagnoseRecordRepository;
            _callService = callService;
        }

        /// <summary>
        /// 保存绿色通道
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <param name="diagnoseNameList"></param>
        /// <param name="doctorInfo"></param>
        /// <returns></returns>
        public async Task EmergencyGreenChannel(AdmissionRecord admissionRecord, bool isGreenChannl)
        {
            string operatorCode = _currentUser.FindClaimValue("name");
            string operatorName = _currentUser.FindClaimValue("fullname");
            //诊断信息
            List<DiagnoseRecord> diagnoseRecords = _freeSql.Select<DiagnoseRecord>().Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && x.PI_ID == admissionRecord.PI_ID && !x.IsDeleted).ToList();
            List<DiagnoseList> diagnoseList = diagnoseRecords.Select(p => new DiagnoseList { DiagnoseName = p.DiagnoseName, DiagnoseCode = p.DiagnoseCode }).ToList();

            //his保存绿色通道
            GreenChannelToHisDto model = new GreenChannelToHisDto()
            {
                VisitNo = admissionRecord.InvoiceNum,
                DoctorName = operatorName,
                DoctorCode = operatorCode,
                Gender = admissionRecord.SexName switch
                {
                    "男" => "1",
                    "女" => "2",
                    _ => "0"
                },
                PatientId = admissionRecord.PatientID,
                PatientName = admissionRecord.PatientName,
                GreenChannelType = "3",
                VisSerialNo = admissionRecord.VisSerialNo,
                DeptCode = DEPT_CODE,
                RegisterNo = admissionRecord.RegisterNo,
                OperationType = isGreenChannl ? "0" : "1",
                DiagnoseList = diagnoseList
            };

            var result = await _pkuApiService.PushGreenChannelAsync(model);
            if (result.Code != 200)
            {
                throw new Exception(message: result.Msg);
            }
        }

        /// <summary>
        /// 保存就诊记录
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <param name="diagnoseRecord"></param>
        /// <param name="name"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> SaveVisitRecordAsync(AdmissionRecord admissionRecord, DiagnoseRecord diagnoseRecord, TransferType destination, string currentUserCode, string currenetUserName)
        {
            try
            {
                string branch = await _pkuApiService.GetUserBranchAsync(currentUserCode);
                if (!"1".Equals(branch))
                {
                    currentUserCode = admissionRecord.FirstDoctorCode;
                    currenetUserName = admissionRecord.FirstDoctorName;
                }

                //保存就诊记录
                var model = new VisitRecordToHisDto()
                {
                    InvoiceNum = admissionRecord.InvoiceNum,
                    PatientId = admissionRecord.PatientID,
                    RegisterNo = admissionRecord.RegisterNo,
                    VisSerialNo = admissionRecord.VisSerialNo,
                    DeptCode = DEPT_CODE,
                    DeptName = DEPT_NAME,
                    DoctorCode = currentUserCode,
                    DoctorName = currenetUserName,
                    StartVisitTime = admissionRecord.VisitDate,
                    EndVisitTime = admissionRecord.FinishVisitTime,
                    VisitStatus = admissionRecord.VisitStatus switch
                    {
                        VisitStatus.待就诊 => "1",
                        VisitStatus.正在就诊 => "1",
                        VisitStatus.已就诊 => "9",
                        VisitStatus.出科 => "9",
                        _ => "9"
                    },
                    VisitType = admissionRecord.AreaCode switch
                    {
                        "OutpatientArea" => "4",
                        "ObservationArea" => "5",
                        "RescueArea" => "5",
                        _ => "4"
                    },
                    FirstVisit = (string.IsNullOrEmpty(admissionRecord.TypeOfVisitName) || admissionRecord.TypeOfVisitName == "初诊") ? "1" : "0",
                    Destination = destination switch
                    {
                        TransferType.InDept => "3",
                        TransferType.RescueArea => "6",
                        TransferType.ObservationArea => "7",
                        TransferType.OutpatientArea => "3",
                        TransferType.ToHospital => "11",
                        TransferType.OutDept => "1",
                        TransferType.Death => "14",
                        _ => "3"
                    },
                    DiagnoseCode = diagnoseRecord?.DiagnoseCode,
                    DiagnoseName = diagnoseRecord?.DiagnoseName,
                };
                var result = await _pkuApiService.SaveVisitRecordAsync(model);
                if (result.Code != 200)
                {
                    _logger.LogError($"调用DDP返回结果失败：{result.Msg}");
                    throw new Exception(message: result.Msg);
                }

                string json = result.Data.ToJson();
                var visitRecordFromHisDto = JsonConvert.DeserializeObject<VisitRecordFromHisDto>(json);
                return visitRecordFromHisDto?.visitNumber;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 顺呼-北大
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<AdmissionRecordDto>> TerminalCallingAsync(TerminalCallDto input, CancellationToken cancellationToken)
        {
            CallInfoData callInfo = new CallInfoData();
            try
            {
                var rESTfulResult = await _callService.CallNextAsync(new CallNextDto { ConsultingRoomCode = input.ConsultingRoomCode, DoctorId = input.DoctorId, DoctorName = input.DoctorName, ConsultingRoomName = input.ConsultingRoomName, IpAddr = input.IpAddr });
                if (rESTfulResult.Success)
                {
                    callInfo = rESTfulResult.Data;
                }
                else
                {
                    return RespUtil.Ok<AdmissionRecordDto>(msg: rESTfulResult.Message);
                }
            }
            catch (Exception ex)
            {
                return RespUtil.Error<AdmissionRecordDto>(msg: ex.Message);
            }

            // 获取当前科室当前要叫号的患者信息
            var callingAdmissionRecord = await _freeSql.Select<AdmissionRecord>()
                .Where(x => x.RegisterNo == callInfo.RegisterNo)
                .FirstAsync<AdmissionRecordDto>();

            // 如果没有候诊中的患者，返回错误提示
            if (callingAdmissionRecord is null || string.IsNullOrEmpty(callingAdmissionRecord.RegisterNo))
                return RespUtil.Error<AdmissionRecordDto>(msg: "急诊系统当前科室暂无候诊中的患者");

            // 当前叫号患者状态更新
            var rows = await _freeSql.Update<AdmissionRecord>()
                .Set(a => a.CallStatus, CallStatus.Calling)
                .Set(a => a.CallConsultingRoomCode, callInfo.ConsultingRoomCode)
                .Set(a => a.CallConsultingRoomName, callInfo.ConsultingRoomName)
                .Set(a => a.CallingDoctorId, input.DoctorId)
                .Set(a => a.CallingDoctorName, input.DoctorName)
                .Where(x => x.PI_ID == callingAdmissionRecord.PI_ID)
                .ExecuteAffrowsAsync();

            _ = _capPublisher.PublishAsync("sync.calldoctor.from.patientservice",
              new { Id = callingAdmissionRecord.PI_ID, DoctorCode = input.DoctorId, DoctorName = input.DoctorName });
            if (rows > 0)
                // 病患列表变化消息
                return RespUtil.Ok<AdmissionRecordDto>(data: callingAdmissionRecord);
            else
                return RespUtil.Error<AdmissionRecordDto>(msg: "插入AdmissionRecord 失败！");
        }

        /// <summary>
        /// 重呼-北大
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<AdmissionRecordDto>> TerminalReCallAsync(TerminalCallDto input,
        CancellationToken cancellationToken)
        {
            CallInfoData callInfo = new CallInfoData();
            try
            {
                var rESTfulResult = await _callService.CallAgainAsync(new CallAgainDto { ConsultingRoomCode = input.ConsultingRoomCode, DoctorId = input.DoctorId, DoctorName = input.DoctorName, ConsultingRoomName = input.ConsultingRoomName, IpAddr = input.IpAddr, RegisterNo = input.RegisterNo });

                if (rESTfulResult.Success)
                {
                    callInfo = rESTfulResult.Data;
                }
                else
                {
                    return RespUtil.Ok<AdmissionRecordDto>(msg: rESTfulResult.Message);
                }

                // 获取当前科室当前要叫号的患者信息
                var callingAdmissionRecord = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.RegisterNo == callInfo.RegisterNo)
                    .FirstAsync<AdmissionRecordDto>();

                // 如果没有候诊中的患者，返回错误提示
                if (callingAdmissionRecord is null || string.IsNullOrEmpty(callingAdmissionRecord.RegisterNo))
                    return RespUtil.Error<AdmissionRecordDto>(msg: "急诊系统当前科室暂无候诊中的患者");
                //指定重呼
                if (!string.IsNullOrWhiteSpace(input.RegisterNo))
                {
                    return RespUtil.Ok<AdmissionRecordDto>(data: callingAdmissionRecord);
                }
                callingAdmissionRecord.CallingDoctorName = input.DoctorName;
                callingAdmissionRecord.CallingDoctorId = input.DoctorId;
                _freeSql.Update<AdmissionRecord>(callingAdmissionRecord);
                _ = _capPublisher.PublishAsync("sync.calldoctor.from.patientservice",
                    new { Id = callingAdmissionRecord.PI_ID, DoctorCode = input.DoctorId, DoctorName = input.DoctorName });
                return RespUtil.Ok<AdmissionRecordDto>(data: callingAdmissionRecord);
            }
            catch (Exception ex)
            {
                return RespUtil.Ok<AdmissionRecordDto>(msg: ex.Message);
            }
        }

        /// <summary>
        /// 过号
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<AdmissionRecordDto>> OutQueueAsync(OutQueueDto input,
        CancellationToken cancellationToken)
        {
            CallInfoData untreatedOverCallInfo = new CallInfoData();
            try
            {
                var rESTfulResult = await _callService.UntreatedOverAsync(new UntreatedOverDto { ConsultingRoomCode = input.ConsultingRoomCode, DoctorId = input.DoctorId, DoctorName = input.DoctorName, IpAddr = input.IpAddr });

                if (rESTfulResult.Success)
                {
                    untreatedOverCallInfo = rESTfulResult.Data;
                }
                else
                {
                    return RespUtil.Ok<AdmissionRecordDto>(msg: rESTfulResult.Message);
                }
            }
            catch (Exception ex)
            {
                return RespUtil.Error<AdmissionRecordDto>(msg: ex.Message);
            }

            // 获取当前科室当前要叫号的患者信息
            var callingAdmissionRecord = await _freeSql.Select<AdmissionRecord>()
                .Where(a => a.RegisterNo == untreatedOverCallInfo.RegisterNo)
                .FirstAsync<AdmissionRecordDto>(cancellationToken: cancellationToken);
            if (callingAdmissionRecord is null)
            {
                return RespUtil.Error<AdmissionRecordDto>(msg: "未找到叫号中的患者");
            }

            // 过号操作，更新当前科室叫号中的患者信息为已过号
            _ = await _freeSql.Update<AdmissionRecord>()
                .Set(a => a.CallStatus, CallStatus.Exceed)
                .Set(a => a.VisitStatus, VisitStatus.过号)
                .Set(a => a.CallConsultingRoomCode, null)
                .Set(a => a.CallConsultingRoomName, null)
                .Set(a => a.ExpireNumberTime, DateTime.Now)
                .Where(a => a.AR_ID == callingAdmissionRecord.AR_ID)
                .ExecuteAffrowsAsync(cancellationToken);
            // 获取患者信息
            callingAdmissionRecord = await _freeSql.Select<AdmissionRecord>()
                .Where(a => a.AR_ID == callingAdmissionRecord.AR_ID)
                .OrderByPropertyName(nameof(AdmissionRecord.VisitDate), false)
                .FirstAsync<AdmissionRecordDto>(cancellationToken: cancellationToken);

            // 插入患者时间轴数据
            var timeAxisRecord = new TimeAxisRecord
            {
                PI_ID = callingAdmissionRecord.PI_ID,
                Time = DateTime.Now,
                TimePointCode = TimePoint.ExpireTime.ToString()
            }.SetTimePointName();
            _freeSql.Insert(timeAxisRecord).ExecuteAffrows();

            _logger.LogError($"发布过号消息：{callingAdmissionRecord.PatientName}");
            _capPublisher.PublishAsync("patient.visitstatus.changed",
                         new { Id = callingAdmissionRecord.PI_ID, VisitStatus = VisitStatus.过号 },
                         cancellationToken: cancellationToken).GetAwaiter();
            return RespUtil.Ok(data: callingAdmissionRecord);
        }

        /// <summary>
        /// 保存诊断数据(急诊)
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <param name="diagnoseRecords"></param>
        /// <returns></returns>
        public async Task<bool> SaveRecipeJzAsync(AdmissionRecord admissionRecord, IEnumerable<DiagnoseRecord> deleteRecords, IEnumerable<DiagnoseRecord> addRecords, string doctorCode)
        {
            SaveDiagsDto saveDiagsDto = new SaveDiagsDto();
            saveDiagsDto.VisSerialNo = admissionRecord.VisSerialNo;
            saveDiagsDto.PatientId = admissionRecord.PatientID;
            saveDiagsDto.VisitNo = admissionRecord.VisitNo;
            saveDiagsDto.RegisterNo = admissionRecord.RegisterNo;
            saveDiagsDto.InvoiceNum = admissionRecord.InvoiceNum;
            saveDiagsDto.DiagnoseDtos = new List<DiagnoseDto>();

            int diseaseNo = 2;
            foreach (var diagnoseRecordDto in addRecords)
            {
                DiagnoseDto diagnoseDto = new DiagnoseDto();
                diagnoseDto.DiagnoseCode = diagnoseRecordDto.DiagnoseCode;
                diagnoseDto.DiagnoseType = "1";//待处理
                diagnoseDto.DoctorCode = doctorCode;
                diagnoseDto.DiagnoseTime = diagnoseRecordDto.CreationTime;
                diagnoseDto.DiagnoseName = diagnoseRecordDto.DiagnoseName;
                diagnoseDto.Remark = diagnoseRecordDto.Remark;
                diagnoseDto.IsDelete = "0";
                diagnoseDto.ICD = diagnoseRecordDto.Icd10.Length > 10 ? diagnoseRecordDto.Icd10.Substring(0, 10) : diagnoseRecordDto.Icd10;
                diagnoseDto.DiseaseNo = diseaseNo.ToString();
                saveDiagsDto.DiagnoseDtos.Add(diagnoseDto);
                diseaseNo++;
            }

            IEnumerable<DiagnoseRecord> deleteRecordList = deleteRecords.Except(addRecords, new DiagnoseRecord());
            foreach (var diagnoseRecordDto in deleteRecordList)
            {
                DiagnoseDto diagnoseDto = new DiagnoseDto();
                diagnoseDto.DiagnoseCode = diagnoseRecordDto.DiagnoseCode;
                diagnoseDto.DiagnoseType = "1";//待处理
                diagnoseDto.DoctorCode = doctorCode;
                diagnoseDto.DiagnoseTime = diagnoseRecordDto.CreationTime;
                diagnoseDto.DiagnoseName = diagnoseRecordDto.DiagnoseName;
                diagnoseDto.Remark = diagnoseRecordDto.Remark;
                diagnoseDto.IsDelete = "1";
                diagnoseDto.ICD = diagnoseRecordDto.Icd10.Length > 10 ? diagnoseRecordDto.Icd10.Substring(0, 10) : diagnoseRecordDto.Icd10;
                diagnoseDto.DiseaseNo = diseaseNo.ToString();
                saveDiagsDto.DiagnoseDtos.Add(diagnoseDto);
                diseaseNo++;
            }

            return await _pkuApiService.SaveRecipeJzAsync(saveDiagsDto);
        }

        /// <summary>
        /// 保存住院通知
        /// </summary>
        /// <param name="applyForHospitalizationDto"></param>
        /// <returns></returns>
        public async Task<HospitalApplyRespDto> SaveInHospital(CreateHospitalApplyRecordDto dto)
        {
            ApplyForHospitalizationDto applyDto = new ApplyForHospitalizationDto();

            AdmissionRecord patientInfo = _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == dto.PI_ID).First();
            List<DiagnoseRecord> diagnoseRecords = _freeSql.Select<DiagnoseRecord>().Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && x.PI_ID == dto.PI_ID && !x.IsDeleted).ToList();
            if (diagnoseRecords == null || !diagnoseRecords.Any())
            {
                throw new Exception("没有诊断不能转住院");
            }
            var specialPatient = 0;
            if (patientInfo.IsOpenGreenChannl)
            {
                specialPatient = 3;
            }
            else
            {
                switch (patientInfo.AreaCode)
                {
                    case "OutpatientArea":
                        specialPatient = 0;
                        break;
                    case "RescueArea":
                        specialPatient = 2;
                        break;
                    case "ObservationArea":
                        specialPatient = 6;
                        break;
                    default:
                        break;
                }
            }
            applyDto.VisSerialNo = patientInfo.VisSerialNo;
            applyDto.RegisterSerialNo = patientInfo.RegisterNo;
            applyDto.VisitNo = patientInfo.VisitNo;
            applyDto.PatientId = patientInfo.PatientID;
            applyDto.PatientName = patientInfo.PatientName;
            applyDto.Gender = GetGender(patientInfo.Sex);
            applyDto.Birthday = patientInfo.Birthday;
            applyDto.CardType = "1";//北大没有证件类型都给身份证
            applyDto.CardNo = patientInfo.IDNo;
            applyDto.Native = dto.Native;
            applyDto.Nation = string.Empty;
            applyDto.Address = patientInfo.HomeAddress;
            applyDto.Phone = patientInfo.ContactsPhone;
            applyDto.Occupation = dto.Occupation;
            applyDto.Company = dto.Company;
            applyDto.CompanyPhone = string.Empty;
            applyDto.CompanyAddress = string.Empty;
            applyDto.MaritalStatus = dto.MarriageType;
            applyDto.BlooodType = string.Empty;
            applyDto.Contacts = patientInfo.ContactsPerson;
            applyDto.ContactsPhone = patientInfo.ContactsPhone;
            applyDto.ContactsAddress = string.Empty;
            applyDto.MedicalCard = patientInfo.MedicalCard;
            applyDto.FeeType = dto.FeeType.ToString();
            applyDto.AllergyHistory = patientInfo.AllergyHistory;

            applyDto.Condition = GetCondition(dto.AdmissionCode);

            applyDto.GreenChannelIdentification = patientInfo.GreenRoadName;

            List<Diagnose> diagnoses = new List<Diagnose>();
            foreach (DiagnoseRecord item in diagnoseRecords)
            {
                Diagnose diagnose = new Diagnose();
                diagnose.DiagnoseCode = item.DiagnoseCode;
                diagnose.DiagnoseName = item.DiagnoseName.Length > 20 ? item.DiagnoseName.Substring(0, 20) : item.DiagnoseName;
                diagnoses.Add(diagnose);
            }
            applyDto.Diagnoses = diagnoses;

            applyDto.DoctorCode = dto.DoctorCode;
            applyDto.DoctorName = dto.DoctorName;
            applyDto.DeptCode = DEPT_CODE;
            applyDto.DeptName = DEPT_NAME;
            applyDto.DateOfAdmission = dto.DateOfAdmission;
            applyDto.AdmissionModeCode = GetAdmissionModeCode(dto.AdmissionModeCode);
            applyDto.AdmissionModeName = GetAdmissionModeName(dto.AdmissionModeCode);

            applyDto.InpatientDepartmentCode = dto.InpatientDepartmentCode;
            applyDto.InpatientDepartmentName = dto.InpatientDepartmentName;
            applyDto.AdmissionTypeCode = dto.AdmissionTypeCode;
            applyDto.AdmissionTypeName = dto.AdmissionTypeName;
            applyDto.AdvancePaymentAmount = dto.Advance;
            applyDto.AdvancePayWay = dto.PayWay;
            applyDto.TenDaysAdmission = dto.TenDaysAdmission;
            applyDto.ExtendedFields.Add("InvoiceNumber", patientInfo.InvoiceNum);
            applyDto.ExtendedFields.Add("DiseaseCoding", diagnoseRecords.First().DiagnoseCode);
            applyDto.ExtendedFields.Add("TreatmentNo", dto.TreatmentNo);
            applyDto.SpecialPatient = specialPatient.ToString();
            applyDto.EmergencySign = dto.SignatureType.HasValue ? dto.SignatureType.Value : 2;
            return await _pkuApiService.SaveInHospital(applyDto);
        }

        /// <summary>
        /// 入院情况
        /// </summary>
        /// <param name="admissionCode"></param>
        /// <returns></returns>
        private int GetCondition(string admissionCode)
        {
            if (string.IsNullOrEmpty(admissionCode)) return 3;

            switch (admissionCode)
            {
                case "1": return 3;
                case "2": return 2;
                case "3": return 1;
                default: return 3;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        private int GetGender(string sex)
        {
            if (sex == "Sex_Woman")
            {
                return 2;
            }
            return 1;
        }

        /// <summary>
        /// 来院方式
        /// </summary>
        /// <param name="admissionModeCode"></param>
        /// <returns></returns>
        private string GetAdmissionModeName(string admissionModeCode)
        {
            if (string.IsNullOrEmpty(admissionModeCode))
            {
                return "直送";
            }

            switch (admissionModeCode.ToLower())
            {
                case "tohospitalway_002": return "步行";
                case "tohospitalway_005": return "轮椅";
                case "tohospitalway_001": return "直送";
                case "tohospitalway_003": return "直送";
                case "tohospitalway_004": return "直送";
                case "tohospitalway_006": return "直送";
                default: return "直送";
            }
        }

        /// <summary>
        /// 来院方式
        /// </summary>
        /// <param name="admissionModeCode"></param>
        /// <returns></returns>
        private int GetAdmissionModeCode(string admissionModeCode)
        {
            if (string.IsNullOrEmpty(admissionModeCode))
            {
                return 1;
            }

            switch (admissionModeCode.ToLower())
            {
                case "tohospitalway_002": return 4;
                case "tohospitalway_005": return 3;
                case "tohospitalway_001": return 1;
                case "tohospitalway_003": return 1;
                case "tohospitalway_004": return 1;
                case "tohospitalway_006": return 1;
                default: return 1;
            }
        }

        #region private

        /// <summary>
        /// 分诊级别名称转换
        /// </summary>
        /// <param name="triageLevel"></param>
        /// <returns></returns>
        private string GetTriageLevelName(string triageLevel)
        {
            string levelStr = triageLevel.Substring(triageLevel.Length - 1);
            int level;
            if (!int.TryParse(levelStr, out level))
            {
                return string.Empty;
            }

            switch (level)
            {
                case 1: return "一级";
                case 2: return "二级";
                case 3: return "三级";
                case 4: return "四级";
                case 5: return "五级";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="consultingRoom"></param>
        /// <returns></returns>
        private ResponseResult<AdmissionRecordDto> ValidateBeforeTerminalCall(DepartmentModel dept,
            ConsultingRoomModel consultingRoom)
        {
            // 科室不存在
            if (dept is null)
            {
                return RespUtil.Error<AdmissionRecordDto>(msg: "科室不存在，请检查科室代码");
            }

            // 诊室不存在
            if (consultingRoom is null)
            {
                return RespUtil.Error<AdmissionRecordDto>(msg: "诊室不存在，请检查诊室代码");
            }

            return RespUtil.Ok<AdmissionRecordDto>();
        }
        #endregion

        /// <summary>
        /// 获取历史诊断
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<ResponseResult<IEnumerable<DiagnoseWithDeptDto>>> GetHistoryDiagnoseListAsync(
            string patientId, Guid pI_ID)
        {
            var list = new List<DiagnoseWithDeptDto>();
            try
            {
                var ourSysDataList = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.PatientID == patientId && x.IsDeleted == false && x.PI_ID != pI_ID)
                    .ToListAsync(a => new HistoryDiagnoseDto
                    {
                        PI_ID = a.PI_ID,
                        DiagnoseTypeName = a.DiagnoseType,
                        OpenTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        Icd10 = a.Icd10,
                        VisitSerialNo = a.VisitNo
                    });

                if (ourSysDataList != null && ourSysDataList.Count > 0)
                {
                    var patientAdmissionRecordList = await _freeSql.Select<AdmissionRecord>()
                        .Where(x => x.PatientID == patientId)
                        .ToListAsync(a => new
                        {
                            a.PI_ID,
                            a.TriageLevelName,
                            a.VisitDate,
                            a.VisSerialNo
                        });

                    foreach (var item in ourSysDataList.GroupBy(p => p.VisitSerialNo))
                    {
                        var patient = patientAdmissionRecordList.FirstOrDefault(p => p.VisSerialNo == item.Key);
                        list.Add(new DiagnoseWithDeptDto()
                        {
                            VisitDate = patient?.VisitDate == null
                            ? ""
                            : patient.VisitDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                            DeptName = "急诊科",
                            TriageLevelName = patient?.TriageLevelName,
                            DiagnoseCount = "共" + item.Count() + "条诊断",
                            Diagnoses = item.ToList()
                        });
                    }

                    _logger.LogInformation("DiagnoseRecordAppService Get History Diagnose List Success");
                    return RespUtil.Ok<IEnumerable<DiagnoseWithDeptDto>>(data: list);
                }

                _logger.LogInformation("DiagnoseRecordAppService Get History Diagnose List Success");
                return RespUtil.Ok<IEnumerable<DiagnoseWithDeptDto>>(data: list);
            }
            catch (Exception e)
            {
                _logger.LogError("DiagnoseRecordAppService Get History Diagnose List Error.Msg:{Msg}", e);
                return RespUtil.Error<IEnumerable<DiagnoseWithDeptDto>>(data: list);
            }
        }

        /// <summary>
        /// 同步his诊断
        /// </summary>
        /// <returns></returns>
        public async Task SyncHisHistoryDiagnoseListAsync(AdmissionRecord admissionRecord)
        {
            try
            {
                int arId = admissionRecord.AR_ID;
                Guid piId = admissionRecord.PI_ID;
                string visSerialNo = await _hisClientAppService.GetPatientRegisterInfoByIdAsync(admissionRecord.PatientID, admissionRecord.RegisterNo);
                if (string.IsNullOrEmpty(visSerialNo)) return;

                await _freeSql.Update<AdmissionRecord>()
                     .WhereIf(arId > 0, x => x.AR_ID == arId)
                     .WhereIf(piId != Guid.Empty, x => x.PI_ID == piId)
                     .Set(x => x.VisSerialNo == visSerialNo).ExecuteAffrowsAsync();

                List<His_Users> hisUsers = await _cache.GetAsync(HisUser);
                if (hisUsers == null || !hisUsers.Any())
                {
                    hisUsers = await _hisUserService.GetHisUsersAsync();
                    await _cache.SetAsync("his_users", hisUsers, RedisPolicyHelper.GetRedisProcily(true, 30));
                }

                //根据在科患者获取接口中患者诊断
                List<GetDiagnoseRecordBySocketDto> hisDiagnoseList = await _hisClientAppService.GetPatientDiagnoseByIdAsync(admissionRecord.PatientID);

                hisDiagnoseList = hisDiagnoseList.Where(x => x.VisitId == visSerialNo).ToList();
                hisDiagnoseList.ForEach(x =>
                {
                    x.PI_ID = admissionRecord.PI_ID;
                    x.VisitDate = admissionRecord.VisitDate;
                });

                foreach (GetDiagnoseRecordBySocketDto item in hisDiagnoseList)
                {
                    His_Users doctorData = hisUsers.FirstOrDefault(u => u.UserName == item.DiagnoseDoctor);
                    item.DiagnoseDoctorName = doctorData?.Name;
                }

                //调用patient删除新增诊断接口
                List<GetDiagnoseRecordBySocket> diagnoseRecordList = hisDiagnoseList.BuildAdapter().AdaptToType<List<GetDiagnoseRecordBySocket>>();
                await _diagnoseRecordRepository.HandleDiagnoseAsync(diagnoseRecordList);
            }
            catch (Exception e)
            {
                _logger.LogError("入科同步his历史诊断异常:{Msg}", e);
            }
        }

        public Task<ResponseResult<string>> ModifyRecordStatusAsync(AdmissionRecord admissionRecord)
        {
            return Task.FromResult(new ResponseResult<string>());
        }

        public async Task<List<TreatmentInfo>> GetTreatmentInfosAsync()
        {
            return await _pkuApiService.GetTreatmentInfosAsync();
        }
    }
}
