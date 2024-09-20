using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class MapsterMappingConfig
    {
        /// <summary>
        /// 初始化映射规则
        /// </summary>
        public static void InitMapster()
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

            #region Dto 映射为实体

            #region 分诊业务映射

            TypeAdapterConfig<Hl7PatientInfoDto, PatientOutput>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<Hl7PatientInfoDto, PatientInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IdentityNo, sou => sou.IdentityNo)
                // 医保控费相关 S
                .Ignore(dest => dest.PatnId)
                .Ignore(dest => dest.CurrMDTRTId)
                .Ignore(dest => dest.PoolArea)
                .Ignore(dest => dest.InsureType)
                .Ignore(dest => dest.OutSetlFlag)
                .Ignore(dest => dest.IsTop)
                .Ignore(dest => dest.IsUntreatedOver)
                .Ignore(dest => dest.CallNo)
                // 医保控费相关 E
                ;

            TypeAdapterConfig<CreateOrUpdatePatientDto, PatientInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.Id, sou => sou.TriagePatientInfoId)
                .Map(dest => dest.ToHospitalWayCode, sou => sou.ToHospitalWay)
                .Map(dest => dest.GreenRoadCode, sou => sou.GreenRoad)
                .Map(dest => dest.GroupInjuryInfoId,
                    sou => string.IsNullOrEmpty(sou.GroupInjuryName) ? Guid.Empty : Guid.Parse(sou.GroupInjuryName))
                .Map(dest => dest.IdentityNo, sou => sou.IdentityNo)
                // 医保控费相关 S
                .Ignore(dest => dest.PatnId)
                .Ignore(dest => dest.CurrMDTRTId)
                .Ignore(dest => dest.PoolArea)
                .Ignore(dest => dest.InsureType)
                .Ignore(dest => dest.OutSetlFlag)
                .Ignore(dest => dest.IsTop)
                .Ignore(dest => dest.IsUntreatedOver)
                .Ignore(dest => dest.CallNo)
                // 医保控费相关 E
                ;

            TypeAdapterConfig<PatientInfoDto, PatientInfoExportExcelDto>
                .NewConfig().AfterMapping(
                (src, dest) =>
                {
                    dest.Temp = string.IsNullOrEmpty(dest.Temp) ? "" : dest.Temp + "℃";
                    dest.BreathRate = string.IsNullOrEmpty(dest.BreathRate) ? "" : dest.BreathRate + "次/分";
                    dest.HeartRate = string.IsNullOrEmpty(dest.HeartRate) ? "" : dest.HeartRate + "次/分";
                    dest.Sbp = string.IsNullOrEmpty(dest.Sbp) ? "" : dest.Sbp + "mmHg";
                    dest.Sdp = string.IsNullOrEmpty(dest.Sdp) ? "" : dest.Sdp + "mmHg";
                    dest.SpO2 = string.IsNullOrEmpty(dest.SpO2) ? "" : dest.SpO2 + "%";
                    dest.BloodGlucose = string.IsNullOrEmpty(dest.BloodGlucose) ? "" : dest.BloodGlucose + "mmol/L";
                })
                .Map(dest => dest.Id, sou => sou.TriagePatientInfoId)
                .Map(dest => dest.ActTriageLevel, sou => sou.ConsequenceInfo.ActTriageLevelName)
                .Map(dest => dest.Birthday,
                    sou => sou.Birthday == null ? "" : sou.Birthday.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(dest => dest.TriageTime,
                    sou => sou.TriageTime == null ? "" : sou.TriageTime.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(dest => dest.GroupInjury, sou => sou.GroupInjuryName)
                .Map(dest => dest.OtherTarget, sou => sou.ConsequenceInfo.OtherTriageTarget)
                .Map(dest => dest.TriageTarget, sou => sou.ConsequenceInfo.TriageTargetName)
                .Map(dest => dest.TriageDeptName, sou => sou.ConsequenceInfo.TriageDeptName)
                .Map(dest => dest.TriageDeptCode, sou => sou.ConsequenceInfo.TriageDept)
                .Map(dest => dest.SpO2, sou => sou.VitalSignInfo.SpO2)
                .Map(dest => dest.BreathRate, sou => sou.VitalSignInfo.BreathRate)
                .Map(dest => dest.HeartRate, sou => sou.VitalSignInfo.HeartRate)
                .Map(dest => dest.Temp, sou => sou.VitalSignInfo.Temp)
                .Map(dest => dest.Sbp, sou => sou.VitalSignInfo.Sbp)
                .Map(dest => dest.Sdp, sou => sou.VitalSignInfo.Sdp)
                .Map(dest => dest.BloodGlucose, sou => sou.VitalSignInfo.BloodGlucose)
                .Map(dest => dest.Remark, sou => sou.VitalSignInfo.RemarkName)
                .Map(dest => dest.Sex, sou => sou.Sex)
                .Map(dest => dest.SexName, sou => sou.SexName)
                .Map(dest => dest.Narration,
                    sou => new List<string> { sou.NarrationName, sou.NarrationComments }
                        .Where(x => !string.IsNullOrWhiteSpace(x)).JoinAsString("; "))
                .Map(dest => dest.GreenRoad, sou => sou.GreenRoadName)
                .Map(dest => dest.ChargeType, sou => sou.ChargeTypeName)
                .Map(dest => dest.DiseaseCode, sou => sou.DiseaseName)
                .Map(dest => dest.TypeOfVisit, sou => sou.TypeOfVisitName)
                .Map(dest => dest.Identity, sou => sou.IdentityName)
                .Map(dest => dest.Nation, sou => sou.NationName)
                .Map(dest => dest.Consciousness, sou => sou.ConsciousnessName)
                .Map(dest => dest.PastMedicalHistory, sou => sou.AdmissionInfo.PastMedicalHistory)
                .Map(dest => dest.AllergyHistory, sou => sou.AdmissionInfo.AllergyHistory)
                .Map(dest => dest.IsFirstAid, sou => sou.IsFirstAid ? "是" : "否")
                .Map(dest => dest.RegisterTime,
                    sou => sou.RegisterTime == null ? "" : sou.RegisterTime.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(dest => dest.BeginTime,
                    sou => sou.BeginTime == null ? "" : sou.BeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(dest => dest.EndTime,
                    sou => sou.EndTime == null ? "" : sou.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(dest => dest.IsUntreatedOver, sou => sou.IsUntreatedOver)
                .Map(dest => dest.Sbp_Sdp,
                    sou => (sou.VitalSignInfo != null && !string.IsNullOrWhiteSpace(sou.VitalSignInfo.Sbp)) ?
                    sou.VitalSignInfo.Sbp + "/" + sou.VitalSignInfo.Sdp : "")
                ;

            TypeAdapterConfig<CreateOrUpdateConsequenceDto, ConsequenceInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriageDeptCode, sou => sou.TriageDept)
                .Map(dest => dest.TriageTargetCode, sou => sou.TriageTarget)
                ;

            TypeAdapterConfig<CreateOrUpdateScoreDto, ScoreInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<CreateGroupInjuryTriageDto, GroupInjuryInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.GroupInjuryCode, sou => sou.GroupInjuryTypeCode);

            TypeAdapterConfig<CreateGroupInjuryDto, GroupInjuryInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.GroupInjuryCode, sou => sou.GroupInjuryTypeCode);

            TypeAdapterConfig<CreateOrUpdateVitalSignDto, VitalSignInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;

            TypeAdapterConfig<PatientOutput, PatientInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IdentityNo, sou => sou.IdentityNo)
                // 医保控费相关 S
                .Ignore(dest => dest.PatnId)
                .Ignore(dest => dest.CurrMDTRTId)
                .Ignore(dest => dest.PoolArea)
                .Ignore(dest => dest.InsureType)
                .Ignore(dest => dest.OutSetlFlag)
                .Ignore(dest => dest.IsTop)
                .Ignore(dest => dest.IsUntreatedOver)
                .Ignore(dest => dest.CallNo)
                // 医保控费相关 E
                ;

            TypeAdapterConfig<CreateOrUpdateJudgmentItemDto, JudgmentItem>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled));

            TypeAdapterConfig<CreateOrUpdateJudgmentTypeDto, JudgmentType>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled));

            TypeAdapterConfig<CreateOrUpdateJudgmentMasterDto, JudgmentMaster>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled));

            TypeAdapterConfig<CreateOrUpdateFastTrackRegisterInfoDto, FastTrackRegisterInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;

            TypeAdapterConfig<PatientRespDto, RegisterInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.RegisterDeptCode, sou => sou.deptId)
                .Map(dest => dest.RegisterNo, sou => sou.appointmentId)
                .Map(dest => dest.RegisterTime, sou => sou.registerDate)
                .Map(dest => dest.VisitNo, sou => sou.visitNo)
                .Map(dest => dest.AddUser, sou => sou.@operator)
                ;

            TypeAdapterConfig<PatientRespDto, Hl7PatientInfoDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PatientName, sou => sou.patientName)
                .Map(dest => dest.Birthday, sou => sou.birthday)
                .Map(dest => dest.Sex, sou => sou.sex)
                .Map(dest => dest.PatientId, sou => sou.patientId)
                .Map(dest => dest.Nation, sou => sou.nationality)
                .Map(dest => dest.IdentityNo, sou => sou.identifyNO)
                .Map(dest => dest.Address, sou => sou.homeAddress)
                .Map(dest => dest.ContactsPhone, sou => sou.phoneNumberHome)
                ;
            TypeAdapterConfig<PatientRespDto, PatientOutput>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PatientName, sou => sou.patientName)
                .Map(dest => dest.Birthday, sou => sou.birthday)
                .Map(dest => dest.Age, sou => sou.age)
                .Map(dest => dest.Sex, sou => sou.sex)
                .Map(dest => dest.PatientId, sou => sou.patientId)
                .Map(dest => dest.Nation, sou => sou.nationality)
                .Map(dest => dest.IdentityNo, sou => sou.identifyNO)
                .Map(dest => dest.Address, sou => sou.homeAddress)
                .Map(dest => dest.IdTypeCode, sou => sou.cardType)
                .Map(dest => dest.GuardianIdTypeCode, sou => sou.guardIdType)
                .Map(dest => dest.ContactsPerson, sou => sou.contactName)
                .Map(dest => dest.GuardianIdCardNo, sou => sou.cardNo)
                .Map(dest => dest.ContactsPhone, sou => sou.phoneNumberHome)
                .Map(dest => dest.SeqNumber, sou => sou.appointmentId)
                ;

            TypeAdapterConfig<PatientRespDto, PatientInfoFromHis>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PatientName, sou => sou.patientName)
                .Map(dest => dest.Birthday, sou => sou.birthday)
                .Map(dest => dest.Age, sou => sou.age)
                .Map(dest => dest.Sex, sou => sou.sex)
                .Map(dest => dest.PatientId, sou => sou.patientId)
                .Map(dest => dest.Nation, sou => sou.ethnicGroup)
                .Map(dest => dest.IdentityNo, sou => sou.identifyNO)
                .Map(dest => dest.Address, sou => sou.homeAddress)
                .Map(dest => dest.IdTypeCode, sou => sou.cardType)
                .Map(dest => dest.GuardianIdTypeCode, sou => sou.guardIdType)
                .Map(dest => dest.ContactsPerson, sou => sou.contactName)
                .Map(dest => dest.GuardianIdCardNo, sou => sou.cardNo)
                .Map(dest => dest.ContactsPhone, sou => sou.phoneNumberHome)
                .Map(dest => dest.SeqNumber, sou => sou.appointmentId)
                .Map(dest => dest.Country, sou => sou.nationality)
                ;

            TypeAdapterConfig<PatientRespDto, RegisterPatientInfoDto>
            .NewConfig()
            .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
             .Map(dest => dest.PatientId, sou => sou.patientId)
             .Map(dest => dest.GuardianIdTypeCode, sou => sou.guardIdType)
             .Map(dest => dest.CardNo, sou => sou.idCard)
             .Map(dest => dest.VisitNo, sou => sou.visitNo ?? sou.visitNum)
             .Map(dest => dest.RegisterNo, sou => sou.visitNo ?? sou.registerId)
             .Map(dest => dest.CrowdCode, sou => sou.patientType == "1" ? "普通" : sou.patientType == "2" ? "儿童" : "")
             .Map(dest => dest.CrowdName, sou => sou.patientType == "1" ? "Crowd_Normal" : sou.patientType == "2" ? "Crowd_Child" : "")
             .Map(dest => dest.IsNoThree, sou => sou.patientType == "3")
             .Map(dest => dest.IdTypeCode, sou => sou.cardType)
             .Map(dest => dest.IdentityNo, sou => sou.identifyNO)
             .Map(dest => dest.PatientName, sou => sou.patientName)
             .Map(dest => dest.Birthday, sou => sou.birthday)
             .Map(dest => dest.Age, sou => sou.age)
             .Map(dest => dest.Sex, sou => sou.sex)
             .Map(dest => dest.Address, sou => sou.homeAddress)
             .Map(dest => dest.ContactsPhone, sou => sou.contactPhone ?? sou.phoneNumberHome ?? sou.phoneNumberBus)
             .Map(dest => dest.ChargeType, sou => sou.patientType)
             .Map(dest => dest.Nation, sou => sou.ethnicGroup)
             .Map(dest => dest.CountryName, sou => sou.nationality)
             .Map(dest => dest.SeqNumber, sou => sou.appointmentId)
             .Map(dest => dest.Weight, sou => sou.weight)
             .Map(dest => dest.ContactsPerson, sou => sou.contactName)
             .Map(dest => dest.ContactsPhone, sou => sou.contactPhone)
             .Map(dest => dest.RegisterTime, sou => sou.registerDate)
             .Map(dest => dest.Operator, sou => sou.@operator)
             .Map(dest => dest.RegType, sou => sou.regType)
             .Map(dest => dest.PatientKind, sou => sou.patientKind)
             .Map(dest => dest.TimeInterval, sou => sou.timeInterval)
             .Map(dest => dest.Diagnosis, sou => sou.diagnosis)
             .Map(dest => dest.IsRefund, sou => sou.isRefund)
             .Map(dest => dest.TriageDeptCode, sou => sou.deptId)
             .Map(dest => dest.TriageDeptName, sou => sou.deptName)
             .Map(dest => dest.RegisterInfo, sou => new List<RegisterInfo>())
             .Ignore(dest => dest.DoctorName)
             ;

            TypeAdapterConfig<Hl7PatientInfoDto, PatientInfoFromHis>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<PatientInfoDto, PatientInfoFromHis>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TaskInfoId, sou => sou.TaskInfoId)
                .Map(dest => dest.CarNum, sou => sou.CarNum)
                .Map(dest => dest.VisitNo, sou => sou.VisitNo)
                .Map(dest => dest.PatientId, sou => sou.PatientId)
                .Map(dest => dest.PatientName, sou => sou.PatientName)
                .Map(dest => dest.Py, sou => sou.Py)
                .Map(dest => dest.Sex, sou => sou.Sex)
                .Map(dest => dest.Weight, sou => sou.Weight)
                .Map(dest => dest.Birthday, sou => sou.Birthday)
                .Map(dest => dest.Address, sou => sou.Address)
                .Map(dest => dest.ContactsPerson, sou => sou.ContactsPerson)
                .Map(dest => dest.ContactsPhone, sou => sou.ContactsPhone)
                .Map(dest => dest.ToHospitalWay, sou => sou.ToHospitalWay)
                .Map(dest => dest.Identity, sou => sou.Identity)
                .Map(dest => dest.ChargeType, sou => sou.ChargeType)
                .Map(dest => dest.IdentityNo, sou => sou.IdentityNo)
                .Map(dest => dest.Nation, sou => sou.Nation)
                .Map(dest => dest.Country, sou => sou.Country)
                .Map(dest => dest.CardNo, sou => sou.CardNo)
                .Map(dest => dest.RFID, sou => sou.RFID)
                .Map(dest => dest.MedicalNo, sou => sou.MedicalNo)
                .Map(dest => dest.Age, sou => sou.Age)
                .Map(dest => dest.StartTriageTime, sou => sou.StartTriageTime)
                .Map(dest => dest.IsNoThree, sou => sou.IsNoThree)
                .Map(dest => dest.IdTypeCode, sou => sou.IdTypeCode)
                .Map(dest => dest.IdTypeName, sou => sou.IdTypeName)
                .Map(dest => dest.GuardianIdCardNo, sou => sou.GuardianIdCardNo)
                .Map(dest => dest.GuardianPhone, sou => sou.GuardianPhone)
                .Map(dest => dest.GuardianIdTypeCode, sou => sou.GuardianIdTypeCode)
                .Map(dest => dest.GuardianIdTypeName, sou => sou.GuardianIdTypeName)
                .Map(dest => dest.SocietyRelationCode, sou => sou.SocietyRelationCode)
                .Map(dest => dest.SeqNumber, sou => sou.SeqNumber)
                .Map(dest => dest.InsuplcAdmdvCode, sou => sou.InsuplcAdmdvCode)
                ;

            TypeAdapterConfig<ConsequenceInfoMqDto, ConsequenceInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;

            TypeAdapterConfig<ScoreInfoMqDto, ScoreInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<AdmissionInfoMqDto, AdmissionInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<VitalSignInfoMqDto, VitalSignInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");


            TypeAdapterConfig<PatientInfoDto, PatientInformExportExcelDto>
               .NewConfig()
               .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
               .Map(dest => dest.Id, sou => sou.TriagePatientInfoId)
               .Map(dest => dest.TaskInfoNum, sou => sou.TaskInfoNum)
               .Map(dest => dest.CarNum, sou => sou.InformInfo.CarNum)
               .Map(dest => dest.PatientId, sou => sou.PatientId)
               .Map(dest => dest.PatientName, sou => sou.InformInfo.PatientName)
               .Map(dest => dest.Gender, sou => sou.InformInfo.Gender)
               .Map(dest => dest.Age, sou => sou.InformInfo.Age)
               .Map(dest => dest.IdTypeName, sou => sou.IdTypeName)
               .Map(dest => dest.IdentityNo, sou => sou.IdentityNo)
               .Map(dest => dest.WarningLv, sou => sou.InformInfo.WarningLv)
               .Map(dest => dest.DiseaseIdentification, sou => sou.InformInfo.DiseaseIdentification)
               .Map(dest => dest.InformTime,
                   sou => sou.InformInfo.InformTime.HasValue ? "" : sou.InformInfo.InformTime.Value.ToString("yyyy-MM-dd HH:mm:ss"))
               .Map(dest => dest.Source, sou => sou.InformInfo.Source);


            //告知
            TypeAdapterConfig<InformPatDto, InformPatInfo>
               .NewConfig()
               .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
               .Map(dest => dest.Gender, sou => sou.Gender == "Sex_Man" ? "男" : (sou.Gender == "Sex_Woman" ? "女" : "未知"));

            #endregion

            #region 分诊配置映射

            TypeAdapterConfig<JudgmentTypeDto, JudgmentType>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled))
                .Map(dest => dest.DeptName, sou => sou.ItemName)
                .Map(dest => dest.JudgmentMasters, sou => sou.Children)
                ;

            TypeAdapterConfig<JudgmentMasterDto, JudgmentMaster>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled))
                .Map(dest => dest.ItemDescription, sou => sou.ItemName)
                .Map(dest => dest.JudgmentItems, sou => sou.Children)
                ;

            TypeAdapterConfig<JudgmentItemDto, JudgmentItem>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled))
                .Map(dest => dest.ItemDescription, sou => sou.ItemName)
                ;

            TypeAdapterConfig<VitalSignExpressionDto, VitalSignExpression>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<CreateOrUpdateVitalSignExpressionDto, VitalSignExpression>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<CreateTriageConfigDto, TriageConfig>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<ReportSettingDto, ReportSetting>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;

            #endregion

            #region CS版急诊分诊

            TypeAdapterConfig<PatientInfo, PatientVisitDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PVID, sou => sou.Id)
                .Map(dest => dest.MedicalCardNo, sou => sou.CardNo)
                .Map(dest => dest.IndentityNo, sou => sou.IdentityNo)
                .Map(dest => dest.GreenRoad, sou => sou.GreenRoadName)
                .Map(dest => dest.BirthDate, sou => sou.Birthday)
                .Map(dest => dest.RegisterFrom, sou => sou.ToHospitalWayName)
                .Map(dest => dest.ChargeType, sou => sou.ChargeTypeName)
                .Map(dest => dest.Identity, sou => sou.IdentityName)
                .Map(dest => dest.ContactPerson, sou => sou.ContactsPerson)
                .Map(dest => dest.ContactPhone, sou => sou.ContactsPhone)
                .Map(dest => dest.Nation, sou => sou.NationName)
                .Map(dest => dest.VisitDate, sou => DateTime.Now)
                .Map(dest => dest.ImportantDisease, sou => sou.DiseaseName)
                .Map(dest => dest.BulkinjuryID, sou => sou.GroupInjuryInfoId)
                .Map(dest => dest.HappenDate, sou => DateTime.Now)
                .Map(dest => dest.Mind, sou => sou.VitalSignInfo != null ? sou.VitalSignInfo.ConsciousnessName : "")
                ;

            TypeAdapterConfig<ConsequenceInfo, TriageRecordDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.ActTriageLevel, sou => sou.ActTriageLevelName)
                .Map(dest => dest.AutoTriageLevel, sou => sou.AutoTriageLevelName)
                .Map(dest => dest.ChangeLevel, sou => sou.ChangeLevelName)
                ;

            TypeAdapterConfig<ScoreInfo, ScoreRecordDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.Operator, sou => sou.AddUser)
                .Map(dest => dest.ScoreContent, sou => XmlHelper.JsonToXml(sou.ScoreContent))
                ;

            TypeAdapterConfig<VitalSignInfo, TriageVitalSignRecordDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.RecordDT, sou => sou.CreationTime)
                .Map(dest => dest.SBP, sou => sou.Sbp)
                .Map(dest => dest.BreathRate, sou => sou.BreathRate)
                .Map(dest => dest.SDP, sou => sou.Sdp)
                .Map(dest => dest.SPO2, sou => sou.SpO2)
                .Map(dest => dest.Temp, sou => sou.Temp)
                .Map(dest => dest.HeartRate, sou => sou.HeartRate)
                .Map(dest => dest.Operator, sou => sou.AddUser)
                .Map(dest => dest.VitalSignMemo, sou => sou.RemarkName)
                ;

            TypeAdapterConfig<GroupInjuryInfo, CsEcisGroupInjuryDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.Memo, sou => sou.Remark)
                .Map(dest => dest.HappenDate, sou => sou.HappeningTime)
                .Map(dest => dest.InjuryType, sou => sou.GroupInjuryName)
                .Map(dest => dest.RecordTitle, sou => sou.Description)
                .Map(dest => dest.BulkinjuryId, sou => sou.Id)
                ;

            TypeAdapterConfig<PatientVisitDto, CsEcisPatientInDeptDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.DeptCode, sou => sou.TriageRecord.TriageDepartmentCode)
                .Map(dest => dest.DeptName, sou => sou.TriageRecord.TriageDepartment)
                .Map(dest => dest.Additional2, sou => sou.TriageRecord.ActTriageLevel)
                ;

            #endregion

            #endregion

            #region 实体映射为Dto

            #region 分诊业务映射

            TypeAdapterConfig<GroupInjuryInfo, GroupInjuryOutput>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.GroupInjuryTypeName, sou => sou.GroupInjuryName)
                .Map(dest => dest.GroupInjuryInfoId, sou => sou.Id)
                ;

            TypeAdapterConfig<PatientInfo, GroupInjuryPatientDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriagePatientInfoId, sou => sou.Id)
                .Map(dest => dest.GroupInjuryInfoId, sou => sou.GroupInjuryInfoId)
                .Map(dest => dest.TriageLevel, sou => sou.ConsequenceInfo.ActTriageLevel)
                .Map(dest => dest.TriageLevelName, sou => sou.ConsequenceInfo.ActTriageLevelName)
                .Map(dest => dest.TriageDirection, sou => sou.ConsequenceInfo.TriageTargetName)
                .Map(dest => dest.TriageDept, sou => sou.ConsequenceInfo.TriageDeptName)
                .Map(dest => dest.GreenRoad, sou => sou.GreenRoadName)
                .Map(dest => dest.Sex, sou => sou.SexName)
                ;

            TypeAdapterConfig<PatientInfo, PatientInfoDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.GreenRoad, sou => sou.GreenRoadCode)
                .Map(dest => dest.ToHospitalWay, sou => sou.ToHospitalWayCode)
                .Map(dest => dest.TriagePatientInfoId, sou => sou.Id)
                .Map(dest => dest.GroupInjuryName, sou => sou.GroupInjuryInfoId.ToString())
                .Map(dest => dest.IdentityNo, sou => sou.IdentityNo)
                // 医保控费相关 S
                .Map(dest => dest.PatnId, src => src.PatnId)
                .Map(dest => dest.CurrMDTRTId, src => src.CurrMDTRTId)
                .Map(dest => dest.PoolArea, src => src.PoolArea)
                .Map(dest => dest.InsureType, src => src.InsureType)
                .Map(dest => dest.OutSetlFlag, src => src.OutSetlFlag)
                .Map(dest => dest.IsTop, src => src.IsTop)
                .Map(dest => dest.IsUntreatedOver, src => src.IsUntreatedOver)
                // 医保控费相关 E
                ;

            TypeAdapterConfig<PatientInfoDto, PatientInfo>
                 .NewConfig()
                 .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                 .Map(dest => dest.GreenRoadCode, sou => sou.GreenRoad)
                 .Map(dest => dest.ToHospitalWayCode, sou => sou.ToHospitalWay)
                 // 医保控费相关 S
                 .Map(dest => dest.PatnId, src => src.PatnId)
                 .Map(dest => dest.CurrMDTRTId, src => src.CurrMDTRTId)
                 .Map(dest => dest.PoolArea, src => src.PoolArea)
                 .Map(dest => dest.InsureType, src => src.InsureType)
                 .Map(dest => dest.OutSetlFlag, src => src.OutSetlFlag)
                 .Map(dest => dest.IsTop, src => src.IsTop)
                 .Map(dest => dest.IsUntreatedOver, src => src.IsUntreatedOver)
                 // 医保控费相关 E
                 ;

            TypeAdapterConfig<ScoreInfo, ScoreInfoDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriagePatientInfoId, sou => sou.PatientInfoId);

            TypeAdapterConfig<VitalSignInfo, VitalSignInfoDto>
                .NewConfig()
                 .Map(dest => dest.BloodGlucose, src => src.BloodGlucose.ToString())
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriagePatientInfoId, sou => sou.PatientInfoId);

            TypeAdapterConfig<ConsequenceInfo, ConsequenceInfoDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriageDept, sou => sou.TriageDeptCode)
                .Map(dest => dest.TriageTarget, sou => sou.TriageTargetCode)
                .Map(dest => dest.TriagePatientInfoId, sou => sou.PatientInfoId);

            TypeAdapterConfig<ConsequenceInfoDto, ConsequenceInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriageDeptCode, sou => sou.TriageDept)
                .Map(dest => dest.TriageTargetCode, sou => sou.TriageTarget)
                .Map(dest => dest.PatientInfoId, sou => sou.TriagePatientInfoId);

            TypeAdapterConfig<PatientInfo, SyncPatientEventBusEto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.Pid, sou => sou.Id)
                .Map(dest => dest.Phone, sou => sou.ContactsPhone)
                .Map(dest => dest.IdCard, sou => sou.IdentityNo)
                .Map(dest => dest.ArrivalTime, sou => sou.StartTriageTime)
                .Map(dest => dest.Temperature,
                    sou => sou.VitalSignInfo != null ? sou.VitalSignInfo.Temp : "")
                .Map(dest => dest.Breathing,
                    sou => sou.VitalSignInfo != null ? sou.VitalSignInfo.BreathRate : "")
                .Map(dest => dest.HeartRate,
                    sou => sou.VitalSignInfo != null ? sou.VitalSignInfo.HeartRate : "")
                .Map(dest => dest.IdCard, sou => sou.IdentityNo)
                .Map(dest => dest.OnsetTime, sou => sou.OnsetTime)
                .Map(dest => dest.SaO2,
                    sou => sou.VitalSignInfo != null ? sou.VitalSignInfo.SpO2 : "")
                .Map(dest => dest.RFID, sou => sou.RFID)
                .Map(dest => dest.LSBloodPressureMax,
                    sou => sou.VitalSignInfo != null ? sou.VitalSignInfo.Sbp : "")
                .Map(dest => dest.LSBloodPressureMin, sou =>
                    sou.VitalSignInfo != null ? sou.VitalSignInfo.Sdp : "")
                .Map(dest => dest.ActTriageLevel,
                    sou => sou.ConsequenceInfo != null ? sou.ConsequenceInfo.ActTriageLevel : "")
                .Map(dest => dest.WebSite, sou => sou.GreenRoadCode)
                .Map(dest => dest.From, sou => Guid.Empty != sou.TaskInfoId ? "急诊" : "院前")
                .Map(dest => dest.TaskId, sou => sou.TaskInfoId)
                .AfterMapping(x => x.SetGroupInjury())
                ;

            TypeAdapterConfig<VitalSignInfo, VitalSignInfoToSixCenterDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.LSBloodPressureMin, sou => sou.Sbp)
                .Map(dest => dest.RSBloodPressureMin, sou => sou.Sdp)
                .Map(dest => dest.Temperature, sou => sou.Temp)
                .Map(dest => dest.Breathing, sou => sou.BreathRate);

            TypeAdapterConfig<ConsequenceInfo, ConsequenceInfoMqDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;

            TypeAdapterConfig<ScoreInfo, ScoreInfoMqDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<AdmissionInfo, AdmissionInfoMqDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<VitalSignInfo, VitalSignInfoMqDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<PatientInfo, CreateOrUpdateEmrPatientDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.DocumentNum, sou => sou.IdentityNo)
                .Map(dest => dest.Contacts, sou => sou.ContactsPerson)
                .Map(dest => dest.Telephone, sou => sou.ContactsPhone)
                .Map(dest => dest.Occupation, sou => sou.IdentityName)
                .Map(dest => dest.Faber, sou => sou.ChargeType)
                .Map(dest => dest.Sex, sou => sou.SexName)
                .Map(dest => dest.Consciousness, sou => sou.VitalSignInfo.ConsciousnessName)
                .Map(dest => dest.Nation, sou => sou.NationName)
                ;

            #endregion

            #region 分诊配置映射

            TypeAdapterConfig<JudgmentType, JudgmentTypeDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToBoolean(sou.IsEnabled))
                .Map(dest => dest.Children, sou => sou.JudgmentMasters)
                .Map(dest => dest.ItemName, sou => sou.DeptName)
                ;

            TypeAdapterConfig<JudgmentMaster, JudgmentMasterDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsEnabled, sou => Convert.ToBoolean(sou.IsEnabled))
                .Map(dest => dest.Children, sou => sou.JudgmentItems)
                .Map(dest => dest.ItemName, sou => sou.ItemDescription)
                ;

            TypeAdapterConfig<JudgmentItem, JudgmentItemDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.IsGreenRoad, sou => Convert.ToBoolean(sou.IsGreenRoad))
                .Map(dest => dest.IsEnabled, sou => Convert.ToBoolean(sou.IsEnabled))
                .Map(dest => dest.ItemName, sou => sou.ItemDescription)
                ;

            TypeAdapterConfig<CreateOrUpdateJudgmentTypeDto, JudgmentType>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.DeptName, sou => sou.ItemName)
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled))
                ;

            TypeAdapterConfig<CreateOrUpdateJudgmentMasterDto, JudgmentMaster>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.ItemDescription, sou => sou.ItemName)
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled))
                ;

            TypeAdapterConfig<CreateOrUpdateJudgmentItemDto, JudgmentItem>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.ItemDescription, sou => sou.ItemName)
                .Map(dest => dest.IsGreenRoad, sou => Convert.ToInt32(sou.IsGreenRoad))
                .Map(dest => dest.IsEnabled, sou => Convert.ToInt32(sou.IsEnabled))
                ;

            TypeAdapterConfig<VitalSignExpression, VitalSignExpressionDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<TriageConfig, TriageConfigDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<FastTrackRegisterInfo, CreateOrUpdateFastTrackRegisterInfoDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;

            TypeAdapterConfig<ReportSetting, ReportSettingDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;
            TypeAdapterConfig<TriageConfig, DictionariesDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(i => i.DictionariesCode, n => n.TriageConfigCode)
                .Map(i => i.DictionariesName, n => n.TriageConfigName)
                .Map(i => i.DictionariesTypeCode, n => ((TriageDict)n.TriageConfigType).ToString())
                .Map(i => i.DictionariesTypeName, n => ((TriageDict)n.TriageConfigType).GetDescriptionByEnum())
                .Map(i => i.Status, n => n.IsDisable);

            TypeAdapterConfig<ScoreDict, ScoreDictDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;

            #endregion

            #region 挂号业务映射

            TypeAdapterConfig<PatientInfo, PatientReqDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.patientId, sou => sou.PatientId)
                .Map(dest => dest.idNo, sou => sou.IdentityNo)
                .Map(dest => dest.name, sou => sou.PatientName)
                .Map(dest => dest.sex, sou => sou.SexName)
                .Map(dest => dest.birthday, sou => sou.Birthday)
                ;

            TypeAdapterConfig<RegisterInfoBeforeTriageInput, CreateOrUpdatePatientDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PatientId, src => src.PatientId)
                .Map(dest => dest.PatientName, src => src.PatientName)
                .Map(dest => dest.Sex, src => src.Sex)
                .Map(dest => dest.Birthday, src => src.Birthday)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.ContactsPerson, src => src.ContactsPerson)
                .Map(dest => dest.ContactsPhone, src => src.ContactsPhone)
                .Map(dest => dest.ToHospitalWay, src => src.ToHospitalWay)
                .Map(dest => dest.ChargeType, src => src.ChargeType)
                .Map(dest => dest.IdentityNo, src => src.IdentityNo)
                .Map(dest => dest.Nation, src => src.Nation)
                .Map(dest => dest.CardNo, src => src.CardNo)
                .Ignore(dest => dest.Remark)
                ;

            TypeAdapterConfig<RegisterInfoBeforeTriageInput, PatientInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PatientId, src => src.PatientId)
                .Map(dest => dest.PatientName, src => src.PatientName)
                .Map(dest => dest.Sex, src => src.Sex)
                .Map(dest => dest.Birthday,
                    src => (!string.IsNullOrEmpty(src.Birthday) ? new DateTime?(DateTime.Parse(src.Birthday)) : null))
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.ContactsPerson, src => src.ContactsPerson)
                .Map(dest => dest.ContactsPhone, src => src.ContactsPhone)
                .Map(dest => dest.ChargeType, src => src.ChargeType)
                .Map(dest => dest.IdentityNo, src => src.IdentityNo)
                .Map(dest => dest.Nation, src => src.Nation)
                .Map(dest => dest.CardNo, src => src.CardNo)
                // 医保控费相关 S
                .Ignore(dest => dest.PatnId)
                .Ignore(dest => dest.CurrMDTRTId)
                .Ignore(dest => dest.PoolArea)
                .Ignore(dest => dest.InsureType)
                .Ignore(dest => dest.OutSetlFlag)
                .Ignore(dest => dest.IsTop)
                .Ignore(dest => dest.IsUntreatedOver)
                .Ignore(dest => dest.CallNo)
                // 医保控费相关 E
                ;

            TypeAdapterConfig<RegisterInfoBeforeTriageInput, RegisterInfo>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Ignore(dest => dest.PatientInfoId)
                .Ignore(dest => dest.Id)
                .Map(dest => dest.RegisterNo, src => src.RegisterNo)
                .Map(dest => dest.VisitNo, src => src.VisitNo)
                .Map(dest => dest.RegisterTime, src => src.RegisterDate)
                .Map(dest => dest.RegisterDoctorCode, src => src.DoctorCode)
                .Map(dest => dest.RegisterDoctorName, src => src.DoctorCode)
                .Map(dest => dest.AddUser, src => src.Operator)
                ;

            TypeAdapterConfig<PatientInfo, RegisterPatientInfoDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriagePatientInfoId, src => src.Id)
                .Map(dest => dest.TriageDeptCode, src => src.ConsequenceInfo.TriageDeptCode)
                .Map(dest => dest.TriageDeptName, src => src.ConsequenceInfo.TriageDeptName)
                .Map(dest => dest.TriageTargetCode, src => src.ConsequenceInfo.TriageTargetCode)
                .Map(dest => dest.TriageTargetName, src => src.ConsequenceInfo.TriageTargetName)
                .Map(dest => dest.ActTriageLevel, src => src.ConsequenceInfo.ActTriageLevel)
                .Map(dest => dest.ActTriageLevelName, src => src.ConsequenceInfo.ActTriageLevelName)
                .Map(dest => dest.IsTop, src => src.IsTop)
                .Map(dest => dest.IsUntreatedOver, src => src.IsUntreatedOver)
                .Map(dest => dest.Sbp, src => src.VitalSignInfo.Sbp)
                .Map(dest => dest.Sdp, src => src.VitalSignInfo.Sdp)
                .Map(dest => dest.SpO2, src => src.VitalSignInfo.SpO2)
                .Map(dest => dest.BreathRate, src => src.VitalSignInfo.BreathRate)
                .Map(dest => dest.Temp, src => src.VitalSignInfo.Temp)
                .Map(dest => dest.HeartRate, src => src.VitalSignInfo.HeartRate)
                .Map(dest => dest.IdentityNo, sou => sou.IdentityNo)
                .Map(dest => dest.NarrationName,
                    sou => new List<string> { sou.NarrationName, sou.NarrationComments }
                        .Where(x => !string.IsNullOrWhiteSpace(x)).JoinAsString("; "))
                .Map(dest => dest.DoctorName, src => src.DoctorName) // 就诊医生
                .Ignore(dest => dest.HasFinishedCovid19Exam)
                .Ignore(dest => dest.TriageNurse)
                .Ignore(dest => dest.TriageNurseName)
                ;

            TypeAdapterConfig<PatientInfo, PatientVisitDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PVID, sou => sou.Id)
                .Map(dest => dest.PatientID, sou => sou.PatientId)
                .Map(dest => dest.BirthDate, sou => sou.Birthday)
                .Map(dest => dest.ImportantDisease, sou => sou.DiseaseName)
                .Map(dest => dest.Identity, sou => sou.IdentityName)
                .Map(dest => dest.IndentityNo, sou => sou.IdentityNo)
                .Map(dest => dest.HappenDate, sou => sou.OnsetTime)
                .Map(dest => dest.GreenRoad, sou => sou.GreenRoadName)
                .Map(dest => dest.VisitDate, sou => DateTime.Now)
                .Map(dest => dest.BulkinjuryID, sou => sou.GroupInjuryInfoId)
                .Map(dest => dest.Sex, sou => sou.SexName)
                ;


            TypeAdapterConfig<PatientCovid19ExamInput, Covid19Exam>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Ignore(x => x.Id)
                ;
            TypeAdapterConfig<Covid19Exam, PatientCovid19ExamDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                ;
            TypeAdapterConfig<PatientRespDto, RegisterInfoBeforeTriageInput>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PatientId, src => src.patientId)
                .Map(dest => dest.PatientName, src => src.patientName)
                .Map(dest => dest.Sex, src => src.sex)
                .Map(dest => dest.Birthday, src => src.birthday)
                .Map(dest => dest.Address, src => src.homeAddress)
                .Map(dest => dest.ContactsPerson, src => src.contactName)
                .Map(dest => dest.ContactsPhone, src => src.contactPhone)
                .Ignore(dest => dest.ToHospitalWay)
                .Map(dest => dest.ChargeType, src => src.patientClass)
                .Map(dest => dest.IdentityNo, src => src.identifyNO)
                .Map(dest => dest.Nation, src => src.nationality)
                .Map(dest => dest.CardNo, src => src.cardNo)
                .Map(dest => dest.RegisterNo, src => src.registerId)
                .Map(dest => dest.RegisterDate, src => src.registerDate)
                .Map(dest => dest.DoctorCode, src => src.doctorCode)
                .Map(dest => dest.Operator, src => src.@operator)
                .Map(dest => dest.VisitNo, src => src.visitNo);
            TypeAdapterConfig<RegisterMode, RegisterModeData>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.TriageConfigCode, src => src.Code)
                .Map(dest => dest.TriageConfigName, src => src.Name)
                .Map(dest => dest.IsDisable, src => src.IsActive ? 1 : 0);
            TypeAdapterConfig<RegisterModeData, RegisterMode>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.Code, src => src.TriageConfigCode)
                .Map(dest => dest.Name, src => src.TriageConfigName)
                .Map(dest => dest.IsActive, src => src.IsDisable > 0);
            #endregion

            #endregion

            #region SMS

            TypeAdapterConfig<TagSettings, TagSettingsDto>
                .NewConfig()
                .AddDestinationTransform((string x) => x.IsNullOrWhiteSpace() ? "" : x)
                ;

            TypeAdapterConfig<TagSettingsDto, TagSettings>
                .NewConfig()
                .AddDestinationTransform((string x) => x.IsNullOrWhiteSpace() ? "" : x)
                ;

            TypeAdapterConfig<DutyTelephone, DutyTelephoneDto>
                .NewConfig()
                .AddDestinationTransform((string x) => x.IsNullOrWhiteSpace() ? "" : x)
                .Map(dest => dest.Time, sou => sou.LastModificationTime ?? sou.CreationTime)
                ;

            TypeAdapterConfig<DutyTelephoneDto, DutyTelephone>
                .NewConfig()
                .AddDestinationTransform((string x) => x.IsNullOrWhiteSpace() ? "" : x)
                ;

            TypeAdapterConfig<TextMessageTemplateDto, TextMessageTemplate>
                .NewConfig()
                .AddDestinationTransform((string x) => x.IsNullOrWhiteSpace() ? "" : x)
                ;

            TypeAdapterConfig<TextMessageTemplate, TextMessageTemplateDto>
                .NewConfig()
                .AddDestinationTransform((string x) => x.IsNullOrWhiteSpace() ? "" : x)
                .Map(dest => dest.Time, sou => sou.LastModificationTime ?? sou.CreationTime)
                ;

            TypeAdapterConfig<TextMessageRecord, TextMessageRecordDto>
                .NewConfig()
                .AddDestinationTransform((string x) => x.IsNullOrWhiteSpace() ? "" : x)
                ;

            #endregion
        }
    }
}