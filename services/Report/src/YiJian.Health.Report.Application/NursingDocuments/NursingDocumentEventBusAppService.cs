using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YiJian.ECIS.Core.Utils;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.NurseExecutes;
using YiJian.ECIS.ShareModel.Etos.Patients;
using YiJian.ECIS.ShareModel.Etos.Temperatures;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Enums;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingDocuments.Eto;
using YiJian.Health.Report.Patients.Dto;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// CAP总线部分
    /// </summary>
    public partial class NursingDocumentAppService
    {
        /// <summary>
        /// 订阅病危病重信息
        /// </summary>
        /// <returns></returns> 
        [AllowAnonymous]
        [CapSubscribe("ecis.report.nursing.critical.illness")]
        public async Task<ResponseBase<bool>> SubscribeCriticalIllnessAsync(CriticalIllnessEto eto)
        {
            try
            {
                //开启事务
                using var uow = _uowManager.Begin();

                var doc = await (await _nursingDocumentRepository.GetQueryableAsync())
                    .OrderByDescending(o => o.CreationTime).Where(w => w.PI_ID == eto.PI_ID)
                    .FirstOrDefaultAsync();

                if (doc == null)
                {
                    return new ResponseBase<bool>(EStatusCode.CNULL, false, "护理单还未创建");
                }

                var entity = new CriticalIllness(GuidGenerator.Create(), eto.PI_ID, eto.Status, eto.Begintime, eto.Endtime, eto.PatientId, eto.PatientName, doc.Id);
                _ = await _criticalIllnessRepository.InsertAsync(entity);

                await uow.CompleteAsync();

                return new ResponseBase<bool>(EStatusCode.COK, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"订阅病危病重的记录异常：{ex.Message}");
                return new ResponseBase<bool>(EStatusCode.CFail, false);
            }
        }

        /// <summary>
        /// 同步患者的生命体征信息
        /// </summary>
        /// <param name="eto"></param> 
        [CapSubscribe("sync.report.vitalsign.info")]
        public async Task AddFirstRecordAsync(VitalSignInfoMqEto eto)
        {
            try
            {
                using IUnitOfWork uow = _uowManager.Begin();
                AdmissionRecordDto patient = await _patientAppService.GetPatientInfoAsync(eto.PI_ID);
                if (!patient.InDeptTime.HasValue) return;

                NursingDocument nursingDocument = await _nursingDocumentRepository.FirstOrDefaultAsync(x => x.PI_ID == eto.PI_ID);
                if (nursingDocument == null)
                {
                    string idCard = patient.IDNo;
                    string gender = patient.Sex == "Sex_Man" ? "男" : (patient.Sex == "Sex_Woman" ? "女" : "未知");
                    DateTime? birthday = patient.Birthday;
                    if (!idCard.IsNullOrEmpty())
                    {
                        var idcard = IDCard.IDCard.Verify(idCard);
                        if (idcard.IsVerifyPass)
                        {
                            gender = idcard.Gender;
                            birthday = idcard.DayOfBirth;
                        }
                    }

                    nursingDocument = new NursingDocument(GuidGenerator.Create(), eto.PI_ID, "护理记录单", patient.CardNo,
                        patient.PatientID, patient.PatientName,
                        patient.IDNo, gender, birthday, patient.Bed, patient.Age, patient.InDeptTime.Value, patient.DiagnoseName,
                        patient.TriageDeptCode, patient.TriageDeptName,
                        patient.InDeptWay, eto.OperationCode);
                    await _nursingDocumentRepository.InsertAsync(nursingDocument);

                    ECriticalIllness status = (ECriticalIllness)patient.EmergencyLevel;
                    CriticalIllness criticalIllness = new CriticalIllness(GuidGenerator.Create(), eto.PI_ID, status: status, patient.InDeptTime.Value, null, patient.PatientID, patient.PatientName, nursingDocument.Id);
                    await _criticalIllnessRepository.InsertAsync(criticalIllness);

                    DynamicField dynamicField = new DynamicField(GuidGenerator.Create(), 0, "护理记录单1", nursingDocument.Id, null, null, null, null,
                        null, null, null, null, null);
                    await _dynamicFieldRepository.InsertAsync(dynamicField);
                }

                string nurse = eto.OperationName;
                string nurseCode = eto.OperationCode;
                NursingRecord nursingRecord = new NursingRecord(GuidGenerator.Create(), DateTime.Now, eto.Temp.ToString(), eto.Pulse.ToString(), eto.HeartRate.ToString(), eto.BreathRate.ToString(), eto.Sbp.ToString(), eto.Sdp.ToString(), eto.SpO2.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, nurseCode, nurse, 0, nursingDocument.Id, eto.Signature, string.Empty, string.Empty, string.Empty);

                Mmol mmol = new Mmol(GuidGenerator.Create(), EMealTimeType.Before, eto.BloodGlucose.ToString(), nursingRecord.Id);
                await _nursingRecordRepository.InsertAsync(nursingRecord);
                await _mmolRepository.InsertAsync(mmol);

                Characteristic characteristic = new Characteristic(GuidGenerator.Create(), string.Empty, nursingRecord.Id, null);
                await _characteristicRepository.InsertAsync(characteristic);

                await CapPublishToTemperatureAsync(nursingRecord, pi_Id: eto.PI_ID);

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 同步执行记录到护理记录单
        /// </summary>
        /// <param name="recipeExecEtos"></param>
        /// <returns></returns> 
        [CapSubscribe("recipeexec.to.nursingrecord")]
        public async Task AddRecipeExecRecordAsync(List<RecipeExecEto> recipeExecEtos)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                RecipeExecEto commonInfo = recipeExecEtos.FirstOrDefault();
                Guid piid = commonInfo.PiId;
                List<NursingDocument> nursingDocuments = await (await _nursingDocumentRepository.GetQueryableAsync()).Where(w => w.PI_ID == piid).ToListAsync();
                if (!nursingDocuments.Any()) return;
                NursingDocument nursingDocument = nursingDocuments.OrderByDescending(x => x.CreationTime).First();

                List<RecipeExecEto> newRecipeExecEtos = new List<RecipeExecEto>();
                Guid nursingRecordId = Guid.Empty;
                foreach (RecipeExecEto recipeExecEto in recipeExecEtos)
                {
                    Intake intake = (await _intakeRepository.GetQueryableAsync()).FirstOrDefault(x => x.RecipeExecId == recipeExecEto.RecipeExecId);
                    if (intake != null)
                    {
                        nursingRecordId = intake.NursingRecordId;
                        intake.Quantity = recipeExecEto.ExecDosage;
                        await _intakeRepository.UpdateAsync(intake);
                    }
                    else
                    {
                        newRecipeExecEtos.Add(recipeExecEto);
                    }
                }

                //没有新增记录直接返回，返回之前统计出入量
                if (!newRecipeExecEtos.Any())
                {
                    NursingRecord oldNursingRecord = await (await _nursingRecordRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == nursingRecordId);
                    if (oldNursingRecord == null) return;
                    await GetUpdateIntakeStatisticsAsync(oldNursingRecord.NursingDocumentId, oldNursingRecord.SheetIndex);
                    await uow.CompleteAsync();
                    return;
                }

                //同步到最新的记录单上
                DynamicField lastRecord = await (await _dynamicFieldRepository.GetQueryableAsync()).Where(x => x.NursingDocumentId == nursingDocument.Id).OrderByDescending(x => x.SheetIndex).FirstOrDefaultAsync();
                int sheetIndex = lastRecord?.SheetIndex ?? 0;

                List<NursingRecord> newNursingRecords = new List<NursingRecord>();
                List<Intake> newIntakes = new List<Intake>();
                //循环插入记录
                foreach (RecipeExecEto item in newRecipeExecEtos)
                {
                    int second = item.OperateTime.Second;
                    DateTime startTime = item.OperateTime.AddSeconds(-second);
                    DateTime endTime = startTime.AddMinutes(1);

                    NursingRecord nursingRecord = await (await _nursingRecordRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.NursingDocumentId == nursingDocument.Id && x.RecordTime >= startTime && x.RecordTime < endTime && x.SheetIndex == sheetIndex);

                    //当前时间点没有护理记录，创建一条护理记录
                    if (nursingRecord == null)
                    {
                        nursingRecord = newNursingRecords.FirstOrDefault(x => x.NursingDocumentId == nursingDocument.Id && x.RecordTime >= startTime && x.RecordTime < endTime && x.SheetIndex == sheetIndex);
                        if (nursingRecord == null)
                        {
                            string nurse = item.OperateName;
                            string nurseCode = item.OperateCode;
                            nursingRecord = new NursingRecord(GuidGenerator.Create(), item.OperateTime, string.Empty, string.Empty,
                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                                string.Empty,
                                string.Empty, string.Empty,
                                string.Empty, nurseCode, nurse, sheetIndex, nursingDocument.Id, item.Signature, string.Empty, string.Empty, string.Empty);

                            if (_configuration["HospitalCode"] == "LDC" && string.IsNullOrEmpty(item.Signature))
                            {
                                var signature = await _hospitalClientAppService.QueryStampBaseAsync(nurseCode);
                                nursingRecord.Sign(signature);
                            }

                            newNursingRecords.Add(nursingRecord);
                        }
                    }

                    Intake intake = new Intake(Guid.NewGuid(), EIntakeType.In, item.UsageCode, item.UsageName, item.RecipeName, item.ExecDosage, item.ExecDosage, item.Unit, item.Unit, string.Empty, string.Empty, 1, nursingRecord.Id, item.RecipeExecId, Guid.Empty, string.Empty, string.Empty);
                    newIntakes.Add(intake);
                }
                if (newNursingRecords.Any()) await _nursingRecordRepository.InsertManyAsync(newNursingRecords);
                if (newIntakes.Any()) await _intakeRepository.InsertManyAsync(newIntakes);

                await uow.SaveChangesAsync();
                await GetUpdateIntakeStatisticsAsync(nursingDocument.Id, sheetIndex);

                await uow.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 取消执行记录
        /// </summary>
        /// <param name="recipeExecEto"></param>
        /// <returns></returns> 
        [CapSubscribe("cancel.recipeexec.to.nursingrecord")]
        public async Task CancelRecipeExecRecordAsync(RecipeExecEto recipeExecEto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                Intake intake = (await _intakeRepository.GetQueryableAsync()).FirstOrDefault(x => x.RecipeExecId == recipeExecEto.RecipeExecId);
                NursingRecord nursingRecord = await (await _nursingRecordRepository.GetQueryableAsync()).FirstOrDefaultAsync(p => p.Id == intake.NursingRecordId);

                if (intake != null)
                {
                    await _intakeRepository.DeleteAsync(intake);
                }
                await uow.SaveChangesAsync();
                //NursingRecord nursingRecord = (await _nursingRecordRepository.GetQueryableAsync())
                //                                .Include(x => x.Intakes)
                //                                .Include(x => x.Pupil)
                //                                .Include(x => x.Characteristic)
                //                                .FirstOrDefault(x => x.Id == intake.NursingRecordId);

                //bool result = CheckedNursingRecord(nursingRecord);
                //if (result)
                //{
                //    await _nursingRecordRepository.DeleteAsync(nursingRecord);
                //}

                //重新计算出入量统计
                await GetUpdateIntakeStatisticsAsync(nursingRecord.NursingDocumentId, nursingRecord.SheetIndex);
                await uow.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 检查NursingRecord是否有数据
        /// </summary>
        /// <param name="nursingRecord"></param>
        /// <returns></returns>
        private bool CheckedNursingRecord(NursingRecord nursingRecord)
        {
            if (nursingRecord == null) return false;
            if (nursingRecord.Characteristic.Any()) return false;
            if (nursingRecord.Intakes.Any()) return false;
            if (nursingRecord.Pupil.Any()) return false;
            if (nursingRecord.SpecialNursings.Any()) return false;

            List<string> checkList = new List<string>() { "t", "p", "hr", @"r", "bp", "bp2", "spo2", "consciousness", "field1", "field2", "field3", "field4", "field5", "field6", "field7", "field8", "field9", "remark" };

            PropertyInfo[] properties = typeof(NursingRecord).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string name = property.Name.ToLower();
                if (checkList.Contains(name))
                {
                    var obj = property.GetValue(nursingRecord);
                    if (obj == null) continue;

                    string value = obj.ToString();
                    if (!string.IsNullOrEmpty(value)) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 同步护理记录到护理记录单
        /// </summary>
        /// <param name="canulaRecordEto"></param>
        /// <returns></returns>
        [CapSubscribe("nursingrecord.to.nursingreport")]
        public async Task AddNursingRecordAsync(CanulaRecordEto canulaRecordEto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                Guid piid = canulaRecordEto.PiId;
                var nursingDocuments = await (await _nursingDocumentRepository.GetQueryableAsync()).Where(w => w.PI_ID == piid).ToListAsync();
                if (!nursingDocuments.Any())
                {
                    _logger.LogError($"没有找到护理记录单{canulaRecordEto.ToJsonString()}");
                    return;
                }

                SpecialNursingRecord specialRecord = await (await _specialNursingRecordRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.NursingRelevanceId == canulaRecordEto.NursingCanulaId && x.EventType == canulaRecordEto.EventType);
                if (specialRecord != null)
                {
                    NursingRecord nursingRecordData = await (await _nursingRecordRepository.GetQueryableAsync()).Where(x => x.Id == specialRecord.NursingRecordId).FirstOrDefaultAsync();
                    if (nursingRecordData == null)
                    {
                        _logger.LogError($"没有找到护理记录{canulaRecordEto.ToJsonString()}");
                        return;
                    }

                    string time1 = canulaRecordEto.OperateTime.ToString("f");
                    string time2 = nursingRecordData.RecordTime.ToString("f");
                    if (time1 == time2 && !string.IsNullOrEmpty(canulaRecordEto.CanulaRecord))
                    {
                        specialRecord.Special = canulaRecordEto.CanulaRecord;
                        await _specialNursingRecordRepository.UpdateAsync(specialRecord);
                        await uow.SaveChangesAsync();
                        await uow.CompleteAsync();
                        return;
                    }
                    else
                    {
                        await _specialNursingRecordRepository.DeleteAsync(specialRecord);
                    }
                }

                //空记录不添加
                if (string.IsNullOrEmpty(canulaRecordEto.CanulaRecord))
                {
                    await uow.SaveChangesAsync();
                    await uow.CompleteAsync();
                    return;
                }

                NursingDocument nursingDocument = nursingDocuments.OrderByDescending(x => x.CreationTime).First();
                int second = canulaRecordEto.OperateTime.Second;
                DateTime startTime = canulaRecordEto.OperateTime.AddSeconds(-second);
                DateTime endTime = startTime.AddMinutes(1);

                NursingRecord nursingRecord = await (await _nursingRecordRepository.GetQueryableAsync()).Where(x => x.NursingDocumentId == nursingDocument.Id && x.RecordTime >= startTime && x.RecordTime < endTime).FirstOrDefaultAsync();
                if (nursingRecord != null)
                {
                    SpecialNursingRecord newSpecialRecord = new SpecialNursingRecord(Guid.NewGuid())
                    {
                        Special = canulaRecordEto.CanulaRecord,
                        NursingRecordId = nursingRecord.Id,
                        NursingRelevanceId = canulaRecordEto.NursingCanulaId,
                        EventType = canulaRecordEto.EventType
                    };
                    await _specialNursingRecordRepository.InsertAsync(newSpecialRecord);
                    await uow.SaveChangesAsync();
                    await uow.CompleteAsync();
                    return;
                }
                DynamicField lastRecord = await (await _dynamicFieldRepository.GetQueryableAsync()).Where(x => x.NursingDocumentId == nursingDocument.Id).OrderByDescending(x => x.SheetIndex).FirstOrDefaultAsync();
                int sheetIndex = lastRecord?.SheetIndex ?? 0;

                string nurse = canulaRecordEto.OperateName;
                string nurseCode = canulaRecordEto.OperateCode;
                nursingRecord = new NursingRecord(GuidGenerator.Create(), canulaRecordEto.OperateTime, string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    string.Empty,
                    string.Empty, string.Empty,
                    string.Empty, nurseCode, nurse, sheetIndex, nursingDocument.Id, canulaRecordEto.Signature, string.Empty, string.Empty, string.Empty);

                if (_configuration["HospitalCode"] == "LDC" && string.IsNullOrEmpty(canulaRecordEto.Signature))
                {
                    var signature = await _hospitalClientAppService.QueryStampBaseAsync(nurseCode);
                    nursingRecord.Sign(signature);
                }

                SpecialNursingRecord newSpecial = new SpecialNursingRecord(Guid.NewGuid())
                {
                    Special = canulaRecordEto.CanulaRecord,
                    NursingRecordId = nursingRecord.Id,
                    NursingRelevanceId = canulaRecordEto.NursingCanulaId,
                    EventType = canulaRecordEto.EventType
                };
                await _nursingRecordRepository.InsertAsync(nursingRecord);
                await _specialNursingRecordRepository.InsertAsync(newSpecial);
                await uow.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 同步删除护理记录
        /// </summary>
        /// <param name="canulaRecordEtos"></param>
        /// <returns></returns>
        [CapSubscribe("deleterecord.to.nursingreport")]
        [RemoteService(false)]
        public async Task DeleteNursingRecordAsync(List<CanulaRecordEto> canulaRecordEtos)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                List<Guid> ids = canulaRecordEtos.Select(x => x.NursingCanulaId).ToList();
                EEventType? eventType = canulaRecordEtos.First().EventType;

                List<SpecialNursingRecord> specialRecords = await (await _specialNursingRecordRepository.GetQueryableAsync()).Where(x => ids.Contains(x.NursingRelevanceId)).ToListAsync();
                specialRecords = specialRecords.Where(x => x.EventType == eventType || !x.EventType.HasValue).ToList();

                if (specialRecords.Any())
                {
                    await _specialNursingRecordRepository.DeleteManyAsync(specialRecords);
                }

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 同步数据到体温单
        /// </summary>
        /// <param name="nursingRecord"></param>
        /// <param name="intakes"></param>
        /// <param name="pi_Id">患者ID</param>
        /// <returns></returns>
        private async Task CapPublishToTemperatureAsync(NursingRecord nursingRecord, IEnumerable<Intake> intakes = null, Guid? pi_Id = null)
        {
            if (pi_Id == null || pi_Id == Guid.Empty)
            {
                var nursingDocument = (await _nursingDocumentRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == nursingRecord.NursingDocumentId);
                if (nursingDocument is null) return;
                pi_Id = nursingDocument.PI_ID;
            }

            TemperatureEto temperatureEto = new TemperatureEto();

            temperatureEto.PI_Id = pi_Id.Value;
            temperatureEto.NursingRecordId = nursingRecord.Id;
            temperatureEto.MeasureTime = nursingRecord.RecordTime;
            if (decimal.TryParse(nursingRecord.T, out decimal temperature))
            {
                temperatureEto.Temperature = temperature;
            }

            if (int.TryParse(nursingRecord.P, out int p))
            {
                temperatureEto.Pulse = p;
            }

            if (int.TryParse(nursingRecord.HR, out int hr))
            {
                temperatureEto.HeartRate = hr;
            }

            if (int.TryParse(nursingRecord.R, out int r))
            {
                temperatureEto.Breathing = r;
            }

            if (int.TryParse(nursingRecord.BP, out int bp))
            {
                temperatureEto.SystolicPressure = bp;
            }

            if (int.TryParse(nursingRecord.BP2, out int bp2))
            {
                temperatureEto.DiastolicPressure = bp2;
            }
            temperatureEto.Consciousness = nursingRecord.Consciousness;
            temperatureEto.NurseCode = nursingRecord.NurseCode;
            temperatureEto.NurseName = nursingRecord.Nurse;


            if (intakes != null && intakes.Any())
            {
                List<IntakeEto> intakesEto = new List<IntakeEto>();
                foreach (Intake intake in intakes)
                {
                    IntakeEto intakeEto = new()
                    {
                        PropertyCode = intake.Code,
                        PropertyName = intake.Content,
                        PropertyValue = intake.Quantity,
                        Unit = intake.Unit,
                        ExtralFlag = intake.IntakeType.ToString()
                    };
                    intakesEto.Add(intakeEto);
                }
                temperatureEto.TemperatureDynamics = intakesEto;
            }

            await _capPublisher.PublishAsync("addtemperature.reportservice.to.nursingservice", temperatureEto);
        }
    }
}
