using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TriageService.Application.Dtos.Triage.Patient;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊患者信息仓储
    /// </summary>
    public class PatientInfoRepository : BaseRepository<PreHospitalTriageDbContext, PatientInfo, Guid>,
        IPatientInfoRepository
    {
        private readonly ILogger<PatientInfoRepository> _log;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;
        private readonly IDbContextProvider<PreHospitalTriageDbContext> _dbContextProvider;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true // Optional: Make the JSON output indented for readability
        };

        public PatientInfoRepository(IDbContextProvider<PreHospitalTriageDbContext> dbContextProvider,
            ILogger<PatientInfoRepository> log, ICurrentUser currentUser, IConfiguration configuration)
            : base(dbContextProvider)
        {
            _log = log;
            _currentUser = currentUser;
            _configuration = configuration;
            _dbContextProvider = dbContextProvider;
        }


        /// <summary>
        /// 更新PatientInfo但是不更新CallingSn
        /// 目前北大医院使用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns>是否进行过数据库更新</returns>
        public async Task<bool> UpdateRecordAsync(PatientInfo patientInfo)
        {
            //var DbContext = _dbContextProvider.GetDbContext();
            _log.LogDebug($"【PatientInfoRepository.UpdateRecord】Start");
            var isSyncVisitStatus = _configuration["PekingUniversity:isSyncVisitStatus"] ?? "true";
            // 是否进行过数据库更新
            bool IsDoUpdate = false;

            // 获得数据库最新患者数据
            PatientInfo dbPatientInfo = DbContext.PatientInfo.AsNoTracking()
                                                             .Include(x => x.ConsequenceInfo)
                                                             .Include(x => x.RegisterInfo)
                                                             .Where(p => p.Id == patientInfo.Id)
                                                             .FirstOrDefault();

            {  // PatientInfo
                EntityEntry<PatientInfo> entityNew = DbContext.Entry(patientInfo);
                EntityEntry<PatientInfo> entityDB = DbContext.Entry(dbPatientInfo);

                StringBuilder sqlBuilder = new StringBuilder();
                List<SqlParameter> parameters = new List<SqlParameter>();
                // 对比数据库患者数据和同步HIS后的患者数据，找出更改的字段，添加进更新字段
                foreach (var item in entityNew.Properties)
                {
                    string fieldName = item.Metadata.PropertyInfo.Name;
                    var dbValue = entityDB.Property(fieldName).CurrentValue;
                    if (item.CurrentValue?.ToString() != dbValue?.ToString())
                    {
                        // 排除更新 CallingSn，解决 CallingSn 被老数据覆盖问题
                        if (fieldName == nameof(patientInfo.CallingSn) && item.CurrentValue?.ToString() != "暂无")
                            continue;

                        if (fieldName == nameof(patientInfo.VisitStatus) && !isSyncVisitStatus.ParseToBool())
                            continue;

                        else if (item.Metadata.PropertyInfo.PropertyType.BaseType == typeof(Enum))
                        {
                            sqlBuilder.Append($" {fieldName} = @{fieldName}, ");
                            parameters.Add(new SqlParameter($"@{fieldName}", (int)item.CurrentValue));
                        }
                        else
                        {
                            sqlBuilder.Append($" {fieldName} = @{fieldName}, ");
                            parameters.Add(new SqlParameter($"@{fieldName}", item.CurrentValue ?? DBNull.Value));
                        }
                    }
                }
                if (parameters.Count > 0)
                {
                    sqlBuilder.Remove(sqlBuilder.Length - 2, 2);
                    string sqlRaw = $"Update Triage_PatientInfo set {sqlBuilder} where {nameof(patientInfo.Id)}='{patientInfo.Id}'";
                    _log.LogDebug($"【PatientInfoRepository.UpdateRecord】执行sql更新患者-{patientInfo.PatientName}-的PatietInfo：{sqlRaw}，parameters为：{JsonSerializer.Serialize(parameters.Select(x => new { Name = x.ParameterName, Value = x.Value }), options)}");
                    int effCount = await DbContext.Database.ExecuteSqlRawAsync(sqlRaw, parameters);
                    IsDoUpdate = true;
                }
            }
            {  // ConsequenceInfo
                EntityEntry<ConsequenceInfo> entityNew = DbContext.Entry(patientInfo.ConsequenceInfo);
                EntityEntry<ConsequenceInfo> entityDB = DbContext.Entry(dbPatientInfo.ConsequenceInfo);

                StringBuilder sqlBuilder = new StringBuilder();
                List<SqlParameter> parameters = new List<SqlParameter>();
                foreach (var item in entityNew.Properties)
                {
                    string fieldName = item.Metadata.PropertyInfo.Name;
                    var dbValue = entityDB.Property(fieldName).CurrentValue;
                    if (item.CurrentValue?.ToString() != dbValue?.ToString())
                    {
                        if (item.Metadata.PropertyInfo.PropertyType.BaseType == typeof(Enum))
                        {
                            sqlBuilder.Append($" {fieldName} = @{fieldName}, ");
                            parameters.Add(new SqlParameter($"@{fieldName}", (int)item.CurrentValue));
                        }
                        else
                        {
                            sqlBuilder.Append($" {fieldName} = @{fieldName}, ");
                            parameters.Add(new SqlParameter($"@{fieldName}", item.CurrentValue ?? DBNull.Value));
                        }
                    }
                }
                if (parameters.Count > 0)
                {
                    sqlBuilder.Remove(sqlBuilder.Length - 2, 2);
                    string sqlRaw = $"Update Triage_ConsequenceInfo set {sqlBuilder} where {nameof(patientInfo.ConsequenceInfo.Id)}='{patientInfo.ConsequenceInfo.Id}'";
                    _log.LogDebug($"【PatientInfoRepository.UpdateRecord】将执行sql更新患者-{patientInfo.PatientName}-的ConsequenceInfo：{sqlRaw}，parameters为：{JsonSerializer.Serialize(parameters.Select(x => new { Name = x.ParameterName, Value = x.Value }), options)}");
                    int effCount = await DbContext.Database.ExecuteSqlRawAsync(sqlRaw, parameters);
                    IsDoUpdate = true;
                }
            }
            {  // RegisterInfo
                var registerInfo = patientInfo.RegisterInfo.FirstOrDefault();
                var registerInfoDB = dbPatientInfo.RegisterInfo.FirstOrDefault();
                if (registerInfo == null || registerInfoDB == null)
                {
                    _log.LogWarning($"【PatientInfoRepository.UpdateRecord】将执行sql更新患者-{patientInfo.PatientName}-的RegisterInfo，registerInfo is {(registerInfo == null ? "null" : "not null")}，registerInfoDB is {(registerInfoDB == null ? "null" : "not null")}");
                }
                else
                {
                    var entityNew = DbContext.Entry(registerInfo);
                    var entityDB = DbContext.Entry(registerInfoDB);

                    StringBuilder sqlBuilder = new StringBuilder();
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    foreach (var item in entityNew.Properties)
                    {
                        string fieldName = item.Metadata.PropertyInfo.Name;
                        var dbValue = entityDB.Property(fieldName).CurrentValue;
                        if (item.CurrentValue?.ToString() != dbValue?.ToString())
                        {
                            if (item.Metadata.PropertyInfo.PropertyType.BaseType == typeof(Enum))
                            {
                                sqlBuilder.Append($" {fieldName} = @{fieldName}, ");
                                parameters.Add(new SqlParameter($"@{fieldName}", (int)item.CurrentValue));
                            }
                            else
                            {
                                sqlBuilder.Append($" {fieldName} = @{fieldName}, ");
                                parameters.Add(new SqlParameter($"@{fieldName}", item.CurrentValue ?? DBNull.Value));
                            }
                        }
                    }
                    if (parameters.Count > 0)
                    {
                        sqlBuilder.Remove(sqlBuilder.Length - 2, 2);
                        string sqlRaw = $"Update Triage_RegisterInfo set {sqlBuilder} where {nameof(registerInfo.Id)}='{registerInfo.Id}'";
                        _log.LogDebug($"【PatientInfoRepository.UpdateRecord】将执行sql更新患者-{patientInfo.PatientName}-的RegisterInfo：{sqlRaw}，parameters为：{JsonSerializer.Serialize(parameters.Select(x => new { Name = x.ParameterName, Value = x.Value }), options)}");
                        int effCount = await DbContext.Database.ExecuteSqlRawAsync(sqlRaw, parameters);
                        IsDoUpdate = true;
                    }
                }
            }
            _log.LogDebug($"【PatientInfoRepository.UpdateRecord】End");

            return IsDoUpdate;
        }

        /// <summary>
        /// 保存分诊
        /// </summary>
        /// <param name="patients">患者分诊信息</param>
        /// <param name="dicts">字典</param>
        /// <param name="groupInjuryInfo">群伤事件</param>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> SaveTriageRecordAsync(IEnumerable<PatientInfo> patients, Dictionary<string, List<TriageConfigDto>> dicts, GroupInjuryInfo groupInjuryInfo = null)
        {
            try
            {
                if (patients.Count() <= 0)
                {
                    _log.LogError("分诊保存失败，没有需要保存的分诊记录");
                    return ReturnResult<bool>.Fail(msg: "没有需要保存的分诊记录", data: false);
                }

                var isTriagedPatients = new List<PatientInfo>();

                if (groupInjuryInfo != null)
                {
                    _log.LogInformation("分诊保存，群伤数据不为空保存群伤事件");
                    groupInjuryInfo.AddUser = _currentUser.UserName;
                    DbContext.Entry(groupInjuryInfo).State = EntityState.Added;
                }

                foreach (var newPatient in patients)
                {
                    #region 根据Code赋值Name

                    if (newPatient.ConsequenceInfo != null)
                    {
                        #region 赋值科室变更Name

                        newPatient.ConsequenceInfo.ChangeDeptName = "";
                        if (!string.IsNullOrWhiteSpace(newPatient.ConsequenceInfo.ChangeDept))
                        {
                            foreach (var dept in newPatient.ConsequenceInfo.ChangeDept.Split("=>"))
                            {
                                newPatient.ConsequenceInfo.ChangeDeptName +=
                                    dicts.GetNameByDictCode(TriageDict.TriageDepartment, dept) + "=>";
                            }

                            newPatient.ConsequenceInfo.ChangeDeptName =
                                newPatient.ConsequenceInfo.ChangeDeptName[..^"=>".Length];
                        }

                        #endregion

                        #region 赋值分诊级别变更Name

                        newPatient.ConsequenceInfo.ChangeLevelName = "";
                        if (!string.IsNullOrWhiteSpace(newPatient.ConsequenceInfo.ChangeLevel))
                        {
                            foreach (var level in newPatient.ConsequenceInfo.ChangeLevel.Split("=>"))
                            {
                                newPatient.ConsequenceInfo.ChangeLevelName +=
                                    dicts.GetNameByDictCode(TriageDict.TriageLevel, level) + "=>";
                            }

                            newPatient.ConsequenceInfo.ChangeLevelName =
                                newPatient.ConsequenceInfo.ChangeLevelName[..^"=>".Length];
                        }

                        #endregion

                        #region 分诊去向信息

                        newPatient.ConsequenceInfo.TriageDeptName = string.IsNullOrWhiteSpace(dicts.GetNameByDictCode(
                            TriageDict.TriageDepartment,
                            newPatient.ConsequenceInfo.TriageDeptCode))
                            ? newPatient.ConsequenceInfo.TriageDeptName
                            : dicts.GetNameByDictCode(TriageDict.TriageDepartment,
                                newPatient.ConsequenceInfo.TriageDeptCode);

                        newPatient.ConsequenceInfo.TriageTargetName = string.IsNullOrWhiteSpace(
                            dicts.GetNameByDictCode(TriageDict.TriageDirection,
                                newPatient.ConsequenceInfo.TriageTargetCode))
                            ? newPatient.ConsequenceInfo.TriageTargetName
                            : dicts.GetNameByDictCode(TriageDict.TriageDirection,
                                newPatient.ConsequenceInfo.TriageTargetCode);

                        newPatient.ConsequenceInfo.ActTriageLevelName = string.IsNullOrWhiteSpace(
                            dicts.GetNameByDictCode(
                                TriageDict.TriageLevel,
                                newPatient.ConsequenceInfo.ActTriageLevel))
                            ? newPatient.ConsequenceInfo.ActTriageLevelName
                            : dicts.GetNameByDictCode(TriageDict.TriageLevel,
                                newPatient.ConsequenceInfo.ActTriageLevel);

                        newPatient.ConsequenceInfo.AutoTriageLevelName = string.IsNullOrWhiteSpace(
                            dicts.GetNameByDictCode(TriageDict.TriageLevel,
                                newPatient.ConsequenceInfo.AutoTriageLevel))
                            ? newPatient.ConsequenceInfo.AutoTriageLevelName
                            : dicts.GetNameByDictCode(TriageDict.TriageLevel,
                                newPatient.ConsequenceInfo.AutoTriageLevel);

                        newPatient.ConsequenceInfo.ChangeTriageReasonName = dicts.GetNameByDictCode(
                            TriageDict.ChangeTriageReason,
                            newPatient.ConsequenceInfo.ChangeTriageReasonCode);
                        newPatient.ConsequenceInfo.TriageAreaName = dicts.GetNameByDictCode(TriageDict.TriageArea,
                            newPatient.ConsequenceInfo.TriageAreaCode);

                        #endregion
                    }

                    if (newPatient.VitalSignInfo != null)
                    {
                        newPatient.VitalSignInfo.RemarkName = dicts.GetNameByDictCode(TriageDict.VitalSignRemark,
                            newPatient.VitalSignInfo.Remark);

                        newPatient.VitalSignInfo.CardiogramName = dicts.GetNameByDictCode(TriageDict.Cardiogram,
                            newPatient.VitalSignInfo.CardiogramCode);

                        newPatient.VitalSignInfo.ConsciousnessName = dicts.GetNameByDictCode(TriageDict.Consciousness,
                            newPatient.VitalSignInfo.ConsciousnessCode);
                    }

                    #region 病患基础信息

                    newPatient.ToHospitalWayName =
                        dicts.GetNameByDictCode(TriageDict.ToHospitalWay, newPatient.ToHospitalWayCode);

                    newPatient.SexName = dicts.GetNameByDictCode(TriageDict.Sex, newPatient.Sex);


                    newPatient.IdentityName = dicts.GetNameByDictCode(TriageDict.IdentityType, newPatient.Identity);

                    newPatient.ChargeTypeName = dicts.GetNameByDictCode(TriageDict.Faber, newPatient.ChargeType);

                    newPatient.SpecialAccountTypeName = dicts.GetNameByDictCode(TriageDict.SpecialAccountType, newPatient.SpecialAccountTypeCode);

                    newPatient.NationName = dicts.GetNameByDictCode(TriageDict.Nation, newPatient.Nation);

                    newPatient.CountryName = dicts.GetNameByDictCode(TriageDict.Country, newPatient.CountryCode);

                    newPatient.GreenRoadName = dicts.GetNameByDictCode(TriageDict.GreenRoad, newPatient.GreenRoadCode);

                    newPatient.DiseaseName = dicts.GetNameByDictCode(TriageDict.KeyDiseases, newPatient.DiseaseCode);

                    newPatient.TypeOfVisitName = dicts.GetNameByDictCode(TriageDict.TypeOfVisit, newPatient.TypeOfVisitCode);

                    newPatient.ConsciousnessName = dicts.GetNameByDictCode(TriageDict.Mind, newPatient.Consciousness);

                    newPatient.IdTypeName = dicts.GetNameByDictCode(TriageDict.IdType, newPatient.IdTypeCode);
                    newPatient.CrowdName = dicts.GetNameByDictCode(TriageDict.Crowd, newPatient.CrowdCode);
                    // 就诊原因
                    if (!newPatient.VisitReasonCode.IsNullOrWhiteSpace())
                    {
                        var visitReasonCodes =
                            newPatient.VisitReasonCode.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        newPatient.VisitReasonName = "";
                        foreach (var visitReasonCode in visitReasonCodes)
                        {
                            newPatient.VisitReasonName += dicts.GetNameByDictCode(TriageDict.VisitReason, visitReasonCode);
                        }
                    }

                    newPatient.SocietyRelationName =
                        dicts.GetNameByDictCode(TriageDict.SocietyRelation, newPatient.SocietyRelationCode);
                    newPatient.GuardianIdTypeName = dicts.GetNameByDictCode(TriageDict.IdType, newPatient.GuardianIdTypeCode);

                    if (!string.IsNullOrWhiteSpace(newPatient.Narration))
                    {
                        newPatient.NarrationName = "";
                        foreach (var narration in newPatient.Narration.Split(","))
                        {
                            newPatient.NarrationName += dicts.GetNameByDictCode(TriageDict.Narration, narration) + ",";
                        }

                        newPatient.NarrationName = newPatient.NarrationName.TrimEnd(',');
                    }

                    #endregion

                    #endregion

                    var isTriaged = newPatient.Id != Guid.Empty && isTriagedPatients.Exists(x => x.Id == newPatient.Id);

                    //新增分诊记录
                    if (newPatient.Id == Guid.Empty)
                    {
                        newPatient.AddUser = _currentUser.UserName;

                        // 急诊预检分诊 Id 为空
                        // 院前预检分诊 Id 不为空，与调度微服务急救病历患者信息Id保持一致
                        if (newPatient.Id == Guid.Empty)
                        {
                            newPatient.SetId(GuidGenerator.Create());
                        }

                        newPatient.GetNamePy();

                        #region 分诊处置

                        if (newPatient.ConsequenceInfo != null)
                        {
                            newPatient.ConsequenceInfo.ChangeDept = "";
                            newPatient.ConsequenceInfo.ChangeDeptName = "";
                            newPatient.ConsequenceInfo.ChangeLevel = "";
                            newPatient.ConsequenceInfo.ChangeLevelName = "";
                            newPatient.ConsequenceInfo.SetId(GuidGenerator.Create());
                            DbContext.Entry(newPatient.ConsequenceInfo).State = EntityState.Added;
                        }

                        #endregion

                        #region 入院情况

                        if (newPatient.AdmissionInfo != null)
                        {
                            newPatient.AdmissionInfo.SetId(GuidGenerator.Create());
                            DbContext.Entry(newPatient.AdmissionInfo).State = EntityState.Added;
                        }

                        #endregion

                        #region 生命体征

                        if (newPatient.VitalSignInfo != null)
                        {
                            //前端不管是否输入生命体征数据都会传入一个对象，所以要判断生命体征数据是否所有字段都为空，为空则不需插入数据
                            if (!string.IsNullOrEmpty(newPatient.VitalSignInfo.Sbp + newPatient.VitalSignInfo.Sdp +
                                                      newPatient.VitalSignInfo.Temp +
                                                      newPatient.VitalSignInfo.BreathRate +
                                                      newPatient.VitalSignInfo.HeartRate +
                                                      newPatient.VitalSignInfo.SpO2 +
                                                      newPatient.VitalSignInfo.Remark +
                                                      newPatient.VitalSignInfo.CardiogramCode +
                                                      newPatient.VitalSignInfo.ConsciousnessCode +
                                                      newPatient.VitalSignInfo.BloodGlucose))
                            {
                                newPatient.VitalSignInfo.AddUser = _currentUser.UserName;
                                newPatient.VitalSignInfo.SetId(GuidGenerator.Create());
                                DbContext.Entry(newPatient.VitalSignInfo).State = EntityState.Added;
                            }
                        }

                        #endregion

                        #region 评分

                        if (newPatient.ScoreInfo != null && newPatient.ScoreInfo.Count > 0)
                        {
                            foreach (var item in newPatient.ScoreInfo)
                            {
                                //前端不管是否输入评分数据都会传入一个对象，所以要判断评分类型为空，为空则不需插入数据
                                if (string.IsNullOrEmpty(item.ScoreType)) continue;
                                item.AddUser = _currentUser.UserName;
                                item.SetId(GuidGenerator.Create());
                                DbContext.Entry(item).State = EntityState.Added;
                            }
                        }

                        #endregion

                        if (newPatient.TriageStatus == 1)
                        {
                            newPatient.TriageTime = DateTime.Now;
                        }

                        DbContext.Entry(newPatient).State = EntityState.Added;
                    }
                    //修改分诊记录
                    else
                    {
                        var currentPatientInfo = await DbContext.PatientInfo.Include(i => i.VitalSignInfo)
                            .Include(c => c.AdmissionInfo)
                            .Include(i => i.ScoreInfo)
                            .Include(i => i.RegisterInfo)
                            .Include(i => i.ConsequenceInfo)
                            .AsNoTracking()
                            .OrderByDescending(p => p.CreationTime)
                            .FirstOrDefaultAsync(w => w.Id == newPatient.Id);

                        if (currentPatientInfo == null) continue;

                        #region 分诊结果

                        if (newPatient.ConsequenceInfo != null)
                        {
                            if (!string.IsNullOrEmpty(newPatient.ConsequenceInfo.DoctorCode) && string.IsNullOrEmpty(newPatient.ConsequenceInfo.DoctorName))
                            {
                                _log.LogWarning($"保存患者-{newPatient.PatientName}-诊断时，检测到有问题ConsequenceInfo数据，其DoctorCode具有值{newPatient.ConsequenceInfo.DoctorCode}，但是DoctorName数据为空，该患者信息为：{JsonSerializer.Serialize(newPatient, options)}");
                            }

                            newPatient.ConsequenceInfo.PatientInfoId = newPatient.Id;
                            if (newPatient.ConsequenceInfo?.Id != Guid.Empty)
                            {
                                if (currentPatientInfo.ConsequenceInfo != null && !currentPatientInfo.ConsequenceInfo.ActTriageLevel.IsNullOrWhiteSpace()
                                                                        && currentPatientInfo.ConsequenceInfo
                                                                            ?.ActTriageLevel != newPatient.ConsequenceInfo.ActTriageLevel)
                                {
                                    newPatient.ConsequenceInfo.ChangeLevel = currentPatientInfo.ConsequenceInfo?.ActTriageLevel +
                                                                          "=>" + newPatient.ConsequenceInfo.ActTriageLevel;
                                    newPatient.ConsequenceInfo.ChangeLevelName =
                                        currentPatientInfo.ConsequenceInfo?.ActTriageLevelName + "=>" +
                                        newPatient.ConsequenceInfo.ActTriageLevelName;
                                }

                                if (currentPatientInfo.ConsequenceInfo != null)
                                {
                                    var isChangeDept = false;
                                    var isChangeDoctor = false;

                                    if (currentPatientInfo.VisitStatus == VisitStatus.Treated)
                                    {
                                        //是否改变科室
                                        isChangeDept = !currentPatientInfo.ConsequenceInfo.TriageDeptCode.IsNullOrWhiteSpace()
                                                     && currentPatientInfo.ConsequenceInfo?.TriageDeptCode != newPatient.ConsequenceInfo.TriageDeptCode;
                                        //是否改变就诊医生
                                        isChangeDoctor = currentPatientInfo.ConsequenceInfo?.DoctorCode != newPatient.ConsequenceInfo.DoctorCode;
                                    }

                                    if (isChangeDept)
                                    {
                                        newPatient.ConsequenceInfo.ChangeDeptName =
                                        currentPatientInfo.ConsequenceInfo?.TriageDeptName + "=>" +
                                        newPatient.ConsequenceInfo.TriageDeptName;
                                    }

                                    if (!newPatient.ConsequenceInfo.ChangeTriage)
                                    {
                                        newPatient.ConsequenceInfo.ChangeTriage = isChangeDept || isChangeDoctor ? true : false;
                                    }
                                }
                                //if (patientInfo.ConsequenceInfo != null && !patientInfo.ConsequenceInfo.TriageDeptCode
                                //                                            .IsNullOrWhiteSpace()
                                //                                        && patientInfo.ConsequenceInfo
                                //                                            ?.TriageDeptCode != patient.ConsequenceInfo
                                //                                            .TriageDeptCode)
                                //{
                                //    patient.ConsequenceInfo.ChangeDept = patientInfo.ConsequenceInfo?.TriageDeptCode +
                                //                                         "=>" + patient.ConsequenceInfo.TriageDeptCode;
                                //    patient.ConsequenceInfo.ChangeDeptName =
                                //        patientInfo.ConsequenceInfo?.TriageDeptName + "=>" +
                                //        patient.ConsequenceInfo.TriageDeptName;
                                //    patient.ConsequenceInfo.ChangeTriage = true;
                                //}
                                //else  if (patientInfo.ConsequenceInfo != null && !patientInfo.ConsequenceInfo.DoctorCode
                                //                                            .IsNullOrWhiteSpace()
                                //                                        && patientInfo.ConsequenceInfo
                                //                                            ?.DoctorCode != patient.ConsequenceInfo
                                //                                            .DoctorCode)
                                //{ 
                                //    patient.ConsequenceInfo.ChangeTriage = true;
                                //}

                                newPatient.ConsequenceInfo.ModUser = _currentUser.UserName;
                                DbContext.Entry(newPatient.ConsequenceInfo).State = EntityState.Modified;
                            }
                            else
                            {
                                newPatient.ConsequenceInfo.AddUser = _currentUser.UserName;
                                DbContext.Entry(newPatient.ConsequenceInfo.SetId(GuidGenerator.Create())).State = EntityState.Added;
                            }

                        }
                        else
                        {
                            if (currentPatientInfo.ConsequenceInfo != null)
                            {
                                currentPatientInfo.ConsequenceInfo = new ConsequenceInfo
                                {
                                    PatientInfoId = currentPatientInfo.Id,
                                }.SetId(currentPatientInfo.ConsequenceInfo.Id);

                                currentPatientInfo.ConsequenceInfo.DeleteUser = _currentUser.UserName;
                                DbContext.Entry(currentPatientInfo.ConsequenceInfo).State = EntityState.Deleted;
                            }
                        }

                        #endregion

                        #region 入院情况

                        if (newPatient.AdmissionInfo != null)
                        {
                            newPatient.AdmissionInfo.PatientInfoId = newPatient.Id;
                            if (newPatient.AdmissionInfo.Id != Guid.Empty)
                            {
                                newPatient.AdmissionInfo.ModUser = _currentUser.UserName;
                                DbContext.Entry(newPatient.AdmissionInfo).State = EntityState.Modified;
                            }
                            else
                            {
                                newPatient.AdmissionInfo.AddUser = _currentUser.UserName;
                                DbContext.Entry(newPatient.AdmissionInfo.SetId(GuidGenerator.Create())).State =
                                    EntityState.Added;
                            }
                        }
                        else
                        {
                            if (currentPatientInfo.AdmissionInfo != null)
                            {
                                currentPatientInfo.AdmissionInfo = new AdmissionInfo
                                {
                                    PatientInfoId = currentPatientInfo.Id
                                }.SetId(currentPatientInfo.AdmissionInfo.Id);

                                newPatient.AdmissionInfo.DeleteUser = _currentUser.UserName;
                                DbContext.Entry(currentPatientInfo.AdmissionInfo).State = EntityState.Deleted;
                            }
                        }

                        #endregion

                        #region 生命体征

                        if (currentPatientInfo.VitalSignInfo != null)
                        {
                            //传入的生命体征参数都为空时删除该记录
                            if (newPatient.VitalSignInfo.CheckVitalSignIsNullOrEmpty())
                            {
                                newPatient.VitalSignInfo.DeleteUser = _currentUser.UserName;
                                DbContext.Entry(newPatient.VitalSignInfo).State = EntityState.Deleted;
                            }
                            else
                            {
                                newPatient.VitalSignInfo.SetId(currentPatientInfo.VitalSignInfo.Id);
                                newPatient.VitalSignInfo.PatientInfoId = newPatient.Id;
                                DbContext.Entry(newPatient.VitalSignInfo).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            if (!newPatient.VitalSignInfo.CheckVitalSignIsNullOrEmpty())
                            {
                                newPatient.VitalSignInfo.AddUser = _currentUser.UserName;
                                newPatient.VitalSignInfo.SetId(GuidGenerator.Create());
                                newPatient.VitalSignInfo.PatientInfoId = newPatient.Id;
                                DbContext.Entry(newPatient.VitalSignInfo).State = EntityState.Added;
                            }
                        }

                        #endregion

                        #region 评分

                        if (currentPatientInfo.ScoreInfo != null && currentPatientInfo.ScoreInfo.Count > 0)
                        {
                            // 先将患者要修改的评分Id查询出来
                            // 1.若患者没有要修改的评分DB中又存在评分默认删除所有评分
                            // 2.若患者有要修改的评分DB中也存在评分，则将DB中存在的评分可修改评分中却不存在的项删除
                            var idsList = newPatient.ScoreInfo.Where(x => x.Id != Guid.Empty)
                                .Select(s => s.Id)?.ToList();
                            var list = currentPatientInfo.ScoreInfo;
                            if (idsList.Count > 0)
                            {
                                list = currentPatientInfo.ScoreInfo.Where(x => !idsList.Contains(x.Id)).ToList();
                            }

                            if (list != null && list.Count > 0)
                            {
                                foreach (var item in list)
                                {
                                    item.DeleteUser = _currentUser.UserName;
                                    DbContext.Entry(item).State = EntityState.Deleted;
                                }
                            }
                        }

                        if (newPatient.ScoreInfo != null && newPatient.ScoreInfo.Count > 0)
                        {
                            foreach (var item in newPatient.ScoreInfo)
                            {
                                if (item.Id != Guid.Empty)
                                {
                                    item.ModUser = _currentUser.UserName;
                                    DbContext.Entry(item).State = EntityState.Modified;
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(item.ScoreType)) continue;
                                    item.AddUser = _currentUser.UserName;
                                    item.SetId(GuidGenerator.Create());
                                    DbContext.Entry(item).State = EntityState.Added;
                                }
                            }
                        }

                        #endregion

                        #region 从暂存变为确认分诊状态

                        if (currentPatientInfo.TriageStatus == 0 && newPatient.TriageStatus == 1)
                        {
                            newPatient.TriageTime = DateTime.Now;
                        }
                        else
                        {
                            newPatient.TriageTime = currentPatientInfo.TriageTime;
                        }
                        //北大特殊处理
                        var hisCode = _configuration.GetValue<string>("HospitalCode");
                        if (hisCode != "PekingUniversity")
                        {
                            newPatient.VisitStatus = newPatient.TriageStatus == 0
                            ? VisitStatus.NotTriageYet
                            : VisitStatus.WattingTreat;
                        }
                        //else
                        //{
                        //    patient.VisitStatus = patientInfo.VisitStatus;
                        //}


                        #endregion

                        #region 二次分诊相关特殊处理
                        //二次分诊患者需保留原本的开始就诊时间和结束就诊时间，用于与同步HIS数据时不被HIS的已就诊状态覆盖
                        if (currentPatientInfo.VisitStatus == VisitStatus.Treated)
                        {
                            Logger.LogDebug($"保存分诊，二次分诊处理，患者：{currentPatientInfo.PatientName}，ID：{currentPatientInfo.Id}");
                            newPatient.BeginTime = currentPatientInfo.BeginTime;
                            newPatient.EndTime = currentPatientInfo.EndTime;
                        }
                        #endregion

                        // 叫号排队号不变
                        newPatient.CallingSn = !string.IsNullOrEmpty(newPatient.CallingSn)
                            ? newPatient.CallingSn
                            : currentPatientInfo.CallingSn;
                        // 新冠问卷是否外部系统获取字段保持不变
                        newPatient.IsCovidExamFromOuterSystem = currentPatientInfo.IsCovidExamFromOuterSystem;
                        if (currentPatientInfo.IsBasicInfoReadOnly)
                        {
                            newPatient.IsBasicInfoReadOnly = currentPatientInfo.IsBasicInfoReadOnly;
                            // 患者基本信息不可修改
                            newPatient.PatientId = currentPatientInfo.PatientId;
                            newPatient.PatientName = currentPatientInfo.PatientName;
                            //patient.Identity = patientInfo.Identity;
                            //patient.IdentityName = patientInfo.IdentityName;
                            newPatient.IdentityNo = currentPatientInfo.IdentityNo;
                            newPatient.Sex = currentPatientInfo.Sex;
                            newPatient.SexName = currentPatientInfo.SexName;
                            newPatient.Birthday = currentPatientInfo.Birthday;
                            newPatient.Age = currentPatientInfo.Age;
                            newPatient.Nation = currentPatientInfo.Nation;
                            newPatient.NationName = currentPatientInfo.NationName;
                        }

                        newPatient.ModUser = _currentUser.UserName;
                        DbContext.Entry(newPatient).State = EntityState.Modified;
                    }
                    _log.LogInformation("分诊保存:" + JsonSerializer.Serialize(newPatient, options));
                }
                if (await DbContext.SaveChangesAsync() > 0)
                {
                    return ReturnResult<bool>.Ok(msg: "分诊保存成功", data: true);
                }

                _log.LogError("分诊保存失败，原因：{Msg}", "DbContext保存数据失败");

                return ReturnResult<bool>.Fail("数据库保存失败，请检查后重试！", data: false);
            }
            catch (Exception e)
            {
                _log.LogError("分诊保存错误！原因：{Msg}", e);
                return ReturnResult<bool>.Fail(msg: e.Message, data: false);
            }
        }


        /// <summary>
        /// 接收病患微服务队列消息更新病患信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePatientInfoFromMqAsync(UpdatePatientInfoMqDto dto)
        {
            try
            {
                var patient = await DbContext.PatientInfo
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.ScoreInfo)
                    .Include(c => c.RegisterInfo)
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (patient != null)
                {
                    patient.PatientName = dto.PatientName;
                    patient.Address = dto.HomeAddress;
                    patient.Sex = !dto.Sex.IsNullOrWhiteSpace() ? dto.Sex : patient.Sex;
                    patient.IdentityNo = dto.IdentityNo;
                    patient.Birthday = dto.Birthday;
                    patient.Weight = dto.Weight;
                    patient.Age = dto.Age;
                    patient.ContactsPerson = dto.ContactsPerson;
                    patient.ContactsPhone = dto.ContactsPhone;
                    patient.Narration = dto.Narration;
                    patient.TypeOfVisitCode = dto.TypeOfVisitCode;
                    patient.GreenRoadCode = dto.GreenRoadCode;
                    patient.GreenRoadName = dto.GreenRoadName;
                    patient.AdmissionInfo ??= new AdmissionInfo
                    {
                        PatientInfoId = patient.Id
                    };

                    patient.AdmissionInfo.IsSoreThroatAndCough = (dto.CoughFlag || dto.ChestFlag).ToString();
                    patient.AdmissionInfo.PastMedicalHistory = dto.PastMedicalHistory;
                    DbContext.PatientInfo.Update(patient);
                    _log.LogInformation("接收病患微服务队列消息更新病患信息成功");
                    return true;
                }

                _log.LogError("接收病患微服务队列消息更新病患信息失败！原因：不存在此患者");
                return false;
            }
            catch (Exception e)
            {
                _log.LogError("接收病患微服务队列消息更新病患信息错误！原因：{Msg}", e);
                return false;
            }
        }


        /// <summary>
        /// 获取当前等候的患者列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<PatientInfo>> GetCurrentWaitingList()
        {
            int.TryParse(_configuration["Settings:RegisterShowTime"], out int time);
            time = time > 0 ? -time : -24;
            // 等候人数计算
            var waitingList = await DbContext.PatientInfo.AsNoTracking()
                     .Include(x => x.ConsequenceInfo)
                .Where(x => x.RegisterInfo.Any(y =>
                    y.RegisterTime >= DateTime.Now.AddHours(time) && !y.IsCancelled)) // 只查询挂号时间在24小时以内的
                .Where(x => x.ConsequenceInfo != null)
                .Where(x => x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Treating)
                .ToListAsync();

            return waitingList;
        }

        /// <summary>
        /// 根据挂号流水号获取患者信息
        /// </summary>
        /// <param name="registerNos"></param>
        /// <returns></returns>
        public async Task<List<PatientFromHisInfoDto>> GetPatientInfoByHisRegSerialNoAsync(IEnumerable<string> registerNos)
        {
            try
            {
                var res = await (from a in DbContext.PatientInfo
                                 join b in DbContext.RegisterInfo on a.Id equals b.PatientInfoId
                                 where registerNos.Contains(b.RegisterNo)
                                 select new PatientFromHisInfoDto
                                 {
                                     patientInfo = a,
                                     registerNo = b.RegisterNo
                                 }
                                 ).ToListAsync();
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}