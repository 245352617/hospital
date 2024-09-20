using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core.Help;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊分诊仓储
    /// </summary>
    public class CsEcisRepository : ICsEcisRepository
    {
        private readonly ILogger<CsEcisRepository> _log;
        private readonly IConfiguration _configuration;

        public CsEcisRepository(ILogger<CsEcisRepository> log, IConfiguration configuration)
        {
            _log = log;
            _configuration = configuration;
        }

        /// <summary>
        /// CS版急诊分诊保存分诊记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> SaveEcisTriageRecordAsync(CsEcisSaveTriageDto dto)
        {
            _log.LogInformation("CS版急诊分诊保存分诊开始");
            await using var conn = new SqlConnection(_configuration["ConnectionStrings:ECISTriage"]);
            SqlTransaction tran = null;
            try
            {
                await conn.OpenAsync();
                tran = conn.BeginTransaction();
                var cmd = conn.CreateCommand();
                cmd.Transaction = tran;

                #region 保存CS版急诊分诊群伤事件

                if (dto.GroupInjury != null)
                {
                    var bulkInjuryId = dto.GroupInjury.BulkinjuryId;
                    cmd.CommandText = "proc_SaveGroupInjury";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.AddParameter("@BulkinjuryID", SqlDbType.UniqueIdentifier, bulkInjuryId);
                    cmd.AddParameter("@RecordTitle", SqlDbType.NVarChar, dto.GroupInjury.RecordTitle);
                    cmd.AddParameter("@InjuryType", SqlDbType.NVarChar, dto.GroupInjury.InjuryType);
                    cmd.AddParameter("@Memo", SqlDbType.NVarChar, dto.GroupInjury.Memo);
                    cmd.AddParameter("@HappenDate", SqlDbType.DateTime, dto.GroupInjury.HappenDate);

                    if (await cmd.ExecuteNonQueryAsync() <= 0)
                    {
                        _log.LogError("CS版急诊分诊保存群伤事件失败");
                        await tran.RollbackAsync();
                        return false;
                    }

                    _log.LogInformation("CS版急诊分诊保存群伤事件成功");
                }

                #endregion

                foreach (var pv in dto.PvList)
                {
                    #region 保存CS版急诊分诊患者基本信息

                    cmd.CommandText = "proc_SaveTriagePatientVisit_YQJJ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.AddParameter("@PVID", SqlDbType.UniqueIdentifier, pv.PVID);
                    cmd.AddParameter("@VisitID", SqlDbType.NVarChar, pv.VisitID);
                    cmd.AddParameter("@PatientID", SqlDbType.NVarChar, pv.PatientID);
                    cmd.AddParameter("@PatientName", SqlDbType.NVarChar, pv.PatientName);
                    cmd.AddParameter("@CardNo", SqlDbType.NVarChar, pv.ClinicCardNo);
                    cmd.AddParameter("@Sex", SqlDbType.NVarChar, pv.Sex);
                    cmd.AddParameter("@Age", SqlDbType.NVarChar, pv.Age);
                    cmd.AddParameter("@BirthDate", SqlDbType.DateTime2, pv.BirthDate);
                    cmd.AddParameter("@Address", SqlDbType.NVarChar, pv.Address);
                    cmd.AddParameter("@ContectPerson", SqlDbType.NVarChar, pv.ContactPerson);
                    cmd.AddParameter("@ContectPhone", SqlDbType.NVarChar, pv.ContactPhone);
                    cmd.AddParameter("@RegisterDT", SqlDbType.DateTime2, pv.RegisterDT);
                    cmd.AddParameter("@Status", SqlDbType.Int, pv.Status);
                    cmd.AddParameter("@RegisterFrom", SqlDbType.NVarChar, pv.RegisterFrom);
                    cmd.AddParameter("@VisitDate", SqlDbType.DateTime2, pv.VisitDate);
                    //名族，国籍，工作单位，头像，联系人，联系人电话，来院方式，绿色通道，其他标识
                    cmd.AddParameter("@Identity", SqlDbType.NVarChar, pv.Identity);
                    cmd.AddParameter("@ChargeType", SqlDbType.NVarChar, pv.ChargeType);
                    cmd.AddParameter("@UpdateSign", SqlDbType.Int, pv.UpdateSign);
                    cmd.AddParameter("@RegisterNo", SqlDbType.NVarChar, pv.RegisterNo);
                    cmd.AddParameter("@IndentityNo", SqlDbType.NVarChar, pv.IndentityNo);
                    cmd.AddParameter("@Nation", SqlDbType.NVarChar, pv.Nation);
                    cmd.AddParameter("@Country", SqlDbType.NVarChar, pv.Country);
                    cmd.AddParameter("@Photo", SqlDbType.Image, pv.Photo);
                    cmd.AddParameter("@Organization", SqlDbType.NVarChar, pv.Organization);
                    cmd.AddParameter("@GreenRoad", SqlDbType.NVarChar, pv.GreenRoad);
                    cmd.AddParameter("@SpecialSign", SqlDbType.NVarChar, pv.SpecialSign);
                    cmd.AddParameter("@BulkinjuryID", SqlDbType.UniqueIdentifier, pv.BulkinjuryID);
                    cmd.AddParameter("@BedNo", SqlDbType.NVarChar, pv.BedNo);
                    cmd.AddParameter("@HappenDate", SqlDbType.DateTime2, pv.HappenDate);
                    cmd.AddParameter("@RFID", SqlDbType.VarChar, pv.RFID);
                    cmd.AddParameter("@Weight", SqlDbType.NVarChar, pv.Weight);
                    cmd.AddParameter("@IsDelete", SqlDbType.Int, pv.IsDelete);
                    cmd.AddParameter("@Mind", SqlDbType.NVarChar, pv.Mind);

                    if (await cmd.ExecuteNonQueryAsync() <= 0)
                    {
                        _log.LogError("CS版急诊分诊保存患者基本信息失败");
                        await tran.RollbackAsync();
                        return false;
                    }

                    _log.LogInformation("CS版急诊分诊保存患者基本信息成功");

                    #endregion

                    #region CS版急诊分诊保存分诊记录

                    cmd.CommandText = "proc_SaveTriageRecord";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.AddParameter("@TID", SqlDbType.UniqueIdentifier, pv.TriageRecord.Id);
                    cmd.AddParameter("@PVID", SqlDbType.UniqueIdentifier, pv.PVID);
                    cmd.AddParameter("@TriageDT", SqlDbType.DateTime2,
                        pv.TriageRecord.TriageDT?.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.AddParameter("@TriageBy", SqlDbType.NVarChar, pv.TriageRecord.TriageBy);
                    cmd.AddParameter("@TriageDepartment", SqlDbType.NVarChar, pv.TriageRecord.TriageDepartment);
                    cmd.AddParameter("@TriageDepartmentCode", SqlDbType.NVarChar, pv.TriageRecord.TriageDepartmentCode);
                    cmd.AddParameter("@TriageTarget", SqlDbType.NVarChar, pv.TriageRecord.TriageTarget);
                    cmd.AddParameter("@TriageTargetCode", SqlDbType.NVarChar, pv.TriageRecord.TriageTargetCode);
                    cmd.AddParameter("@OtherTriageTarget", SqlDbType.NVarChar, pv.TriageRecord.OtherTriageTarget);
                    cmd.AddParameter("@ActTriageLevel", SqlDbType.NVarChar, pv.TriageRecord.ActTriageLevel);
                    cmd.AddParameter("@AutoTriageLevel", SqlDbType.NVarChar, pv.TriageRecord.AutoTriageLevel);
                    cmd.AddParameter("@TriageMemo", SqlDbType.NVarChar, pv.TriageRecord.TriageMemo);
                    cmd.AddParameter("@ImportantDisease", SqlDbType.NVarChar, pv.ImportantDisease);
                    cmd.AddParameter("@Comment", SqlDbType.NVarChar, pv.TriageRecord.Comment);
                    cmd.AddParameter("@HasVitalSign", SqlDbType.Int, pv.TriageRecord.HasVitalSign);
                    cmd.AddParameter("@HasScoreRecord", SqlDbType.Int, pv.TriageRecord.HasScoreRecord);
                    cmd.AddParameter("@HasAccordingRecrd", SqlDbType.Int, pv.TriageRecord.HasAccordingRecord);
                    cmd.AddParameter("@StartRecordDT", SqlDbType.DateTime2,
                        pv.TriageRecord.StartRecordDT.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.AddParameter("@RegisterFirst", SqlDbType.Bit, pv.TriageRecord.RegisterFirst);
                    cmd.AddParameter("@ChangeLevel", SqlDbType.NVarChar, pv.TriageRecord.ChangeLevel);
                    cmd.AddParameter("@Examination", SqlDbType.NVarChar, pv.Examination);
                    cmd.AddParameter("@ChangeTriageDept", SqlDbType.VarChar, null);
                    cmd.AddParameter("@ChangeDept", SqlDbType.VarChar, null);
                    cmd.AddParameter("@ChangeDeptBy", SqlDbType.VarChar, null);

                    if (await cmd.ExecuteNonQueryAsync() <= 0)
                    {
                        _log.LogError("CS版急诊分诊保存分诊记录失败");

                        await tran.RollbackAsync();
                        return false;
                    }

                    _log.LogInformation("CS版急诊分诊保存分诊记录成功");

                    #endregion

                    #region CS版急诊分诊评分赋值

                    if (pv.ScoreRecords != null && pv.ScoreRecords.Count > 0)
                    {
                        var scores = pv.ScoreRecords.FindAll(x => !string.IsNullOrWhiteSpace(x.ScoreType))
                            .BuildAdapter().AdaptToType<List<ScoreRecordDto>>();

                        foreach (var item in scores)
                        {
                            cmd.CommandText = "proc_SaveScoreRecord";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.AddParameter("@SID", SqlDbType.UniqueIdentifier, item.Id);
                            cmd.AddParameter("@PVID", SqlDbType.UniqueIdentifier, pv.PVID);
                            cmd.AddParameter("@TID", SqlDbType.UniqueIdentifier, pv.TriageRecord.Id);
                            cmd.AddParameter("@RecordDT", SqlDbType.DateTime2, item.RecordDT);
                            cmd.AddParameter("@ScoreType", SqlDbType.NVarChar, item.ScoreType);
                            cmd.AddParameter("@ScoreValue", SqlDbType.NVarChar, item.ScoreValue);
                            cmd.AddParameter("@ScoreDescription", SqlDbType.NVarChar, item.ScoreDescription);
                            cmd.AddParameter("@ScoreContent", SqlDbType.NVarChar, item.ScoreContent);
                            cmd.AddParameter("@Operator", SqlDbType.NVarChar, item.Operator);
                            cmd.AddParameter("@RecordType", SqlDbType.Int, item.RecordType);

                            if (await cmd.ExecuteNonQueryAsync() <= 0)
                            {
                                _log.LogError("CS版急诊分诊保存评分失败");
                                await tran.RollbackAsync();
                                return false;
                            }

                            _log.LogInformation("CS版急诊分诊保存评分成功");
                        }
                    }

                    #endregion

                    #region CS版急诊分诊生命体征赋值

                    if (pv.VitalSignRecord != null)
                    {
                        cmd.CommandText = "proc_SaveVitalSign";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.AddParameter("@VID", SqlDbType.UniqueIdentifier, pv.VitalSignRecord.Id);
                        cmd.AddParameter("@PVID", SqlDbType.UniqueIdentifier, pv.PVID);
                        cmd.AddParameter("@TID", SqlDbType.UniqueIdentifier, pv.TriageRecord.Id);
                        cmd.AddParameter("@RecordDT", SqlDbType.DateTime2, pv.VitalSignRecord.RecordDT);
                        cmd.AddParameter("@SBP", SqlDbType.NVarChar, pv.VitalSignRecord.SBP);
                        cmd.AddParameter("@SDP", SqlDbType.NVarChar, pv.VitalSignRecord.SDP);
                        cmd.AddParameter("@SPO2", SqlDbType.NVarChar, pv.VitalSignRecord.SPO2);
                        cmd.AddParameter("@BreathRate", SqlDbType.NVarChar, pv.VitalSignRecord.BreathRate);
                        cmd.AddParameter("@Temp", SqlDbType.NVarChar, pv.VitalSignRecord.Temp);
                        cmd.AddParameter("@HeartRate", SqlDbType.NVarChar, pv.VitalSignRecord.HeartRate);
                        cmd.AddParameter("@Operator", SqlDbType.NVarChar, pv.VitalSignRecord.Operator);
                        cmd.AddParameter("@Memo", SqlDbType.NVarChar, pv.VitalSignRecord.VitalSignMemo);

                        if (await cmd.ExecuteNonQueryAsync() <= 0)
                        {
                            _log.LogError("CS版急诊分诊保存生命体征失败");
                            await tran.RollbackAsync();
                            return false;
                        }

                        _log.LogInformation("CS版急诊分诊保存生命体征成功");
                    }

                    #endregion
                }

                await tran.CommitAsync();
                _log.LogInformation("CS版急诊分诊成功");
                return true;
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    await tran.RollbackAsync();
                }

                _log.LogInformation("CS版急诊分诊成功错误！原因：{Msg}", e);
                return false;
            }
            finally
            {
                if (tran != null)
                {
                    await tran.DisposeAsync();
                }
            }
        }

        /// <summary>
        /// CS版急诊自动审核绿通
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> AutoExamineGreenChannelAsync(ExamineGreenChannelDto dto)
        {
            _log.LogInformation("CS版急诊分诊保存分诊开始");
            await using var conn = new SqlConnection(_configuration["ConnectionStrings:ECISPlatform"]);
            SqlTransaction tran = null;
            try
            {
                await conn.OpenAsync();
                tran = conn.BeginTransaction();
                var cmd = conn.CreateCommand();
                cmd.Transaction = tran;

                cmd.CommandText = "Proc_SaveGreenRoad";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.AddParameter("PVID", SqlDbType.UniqueIdentifier, dto.PVID);
                cmd.AddParameter("PatientID", SqlDbType.NVarChar, dto.PatientId);
                cmd.AddParameter("VisitNo", SqlDbType.NVarChar, dto.VisitNo);
                cmd.AddParameter("ExamineDoctor", SqlDbType.NVarChar, dto.ExamineDoctor);
                cmd.AddParameter("ExamineDT", SqlDbType.DateTime, dto.ExamineDT);
                cmd.AddParameter("Result", SqlDbType.Int, 0);
                cmd.Parameters["Result"].Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                var resultId = Convert.ToInt32(cmd.Parameters["Result"].Value);
                if (resultId <= 0)
                {
                    tran.Rollback();
                    return false;
                }

                await tran.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    await tran.RollbackAsync();
                }

                _log.LogInformation("CS版急诊自动审核绿通错误，原因：{Msg}", e);
                return false;
            }
            finally
            {
                if (tran != null)
                {
                    await tran.DisposeAsync();
                }
            }
        }

        /// <summary>
        /// CS 版急诊入科接诊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> CreatePatientInHouseAsync(CsEcisPatientInDeptDto dto)
        {
            _log.LogInformation("CS版急诊分诊保存分诊开始");
            await using var conn = new SqlConnection(_configuration["ConnectionStrings:ECISPlatform"]);
            SqlTransaction tran = null;
            try
            {
                await conn.OpenAsync();
                tran = conn.BeginTransaction();
                var cmd = conn.CreateCommand();
                cmd.Transaction = tran;

                cmd.CommandText = "Proc_PatientList_PatInDept";
                cmd.CommandType = CommandType.StoredProcedure;

                #region AddParameter

                cmd.AddParameter("@PVID", SqlDbType.UniqueIdentifier, dto.PVID);
                cmd.AddParameter("@PatientID", SqlDbType.NVarChar, dto.PatientID);
                cmd.AddParameter("@VisitNo", SqlDbType.NVarChar, dto.VisitID);
                cmd.AddParameter("@VisitDate", SqlDbType.DateTime, dto.VisitDate);
                cmd.AddParameter("@Status", SqlDbType.Int, dto.Status);
                cmd.AddParameter("@WardArea", SqlDbType.NVarChar, dto.WardArea);
                cmd.AddParameter("@DeptCode", SqlDbType.NVarChar, dto.DeptCode);
                cmd.AddParameter("@DeptName", SqlDbType.NVarChar, dto.DeptName);
                cmd.AddParameter("@BedNo", SqlDbType.NVarChar, dto.BedNo);
                cmd.AddParameter("@ClinicType", SqlDbType.NVarChar, dto.ClinicType);
                cmd.AddParameter("@FstTreatCode", SqlDbType.NVarChar, dto.FstTreatCode);
                cmd.AddParameter("@FstTreatName", SqlDbType.NVarChar, dto.FstTreatName);
                cmd.AddParameter("@NurseCode", SqlDbType.NVarChar, dto.NurseCode);
                cmd.AddParameter("@NurseName", SqlDbType.NVarChar, dto.NurseName);
                cmd.AddParameter("@IsPlanBackWard", SqlDbType.Int, dto.IsPlanBackWard);
                cmd.AddParameter("@InDeptTime", SqlDbType.DateTime, dto.InDeptTime);
                cmd.AddParameter("@InDeptWay", SqlDbType.NVarChar, dto.InDeptWay);
                cmd.AddParameter("@OperatorCode", SqlDbType.NVarChar, dto.OperatorCode);
                cmd.AddParameter("@OperatorName", SqlDbType.NVarChar, dto.OperatorName);
                cmd.AddParameter("@Additional1", SqlDbType.NVarChar, dto.Additional1);
                cmd.AddParameter("@Additional2", SqlDbType.NVarChar, dto.Additional2);
                cmd.AddParameter("@Additional3", SqlDbType.NVarChar, dto.Additional3);

                #region ICU Info

                cmd.AddParameter("@Irritability", SqlDbType.NVarChar, dto.Irritability);
                cmd.AddParameter("@Height", SqlDbType.Float, dto.Height);
                cmd.AddParameter("@Weight", SqlDbType.Float, dto.Weight);
                cmd.AddParameter("@PastHistory", SqlDbType.NVarChar, dto.PastHistory);
                cmd.AddParameter("@InfectiousHistory", SqlDbType.NVarChar, dto.InfectiousHistory);
                cmd.AddParameter("@Announcements", SqlDbType.NVarChar, dto.Announcements);
                cmd.AddParameter("@SpecialMark", SqlDbType.NVarChar, dto.SpecialMark);

                #endregion

                SqlParameter parReturn = new SqlParameter("@return", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                
                cmd.Parameters.Add(parReturn);

                #endregion AddParameter

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return (int) parReturn.Value == 1;
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    await tran.RollbackAsync();
                }

                _log.LogInformation("CS版急诊自动审核绿通错误，原因：{Msg}", e);
                return false;
            }
            finally
            {
                if (tran != null)
                {
                    await tran.DisposeAsync();
                }
            }
        }
    }
}