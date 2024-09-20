using Mapster;
using System.Collections.Generic;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;

namespace Szyjian.Ecis.Patient.Application
{
    public static class MapsterMappingConfig
    {
        public static void InitMapsterConfig()
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

            TypeAdapterConfig<TriagePatientInfosMqDto, AdmissionRecord>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PI_ID, sou => sou.PatientInfo.TriagePatientInfoId)
                .Map(dest => dest.TriageDirectionCode, sou => sou.ConsequenceInfo.TriageTarget)
                .Map(dest => dest.TriageDirectionName, sou => sou.ConsequenceInfo.TriageTargetName)
                .Map(dest => dest.TriageLevel, sou => sou.ConsequenceInfo.ActTriageLevel)
                .Map(dest => dest.TriageLevelName, sou => sou.ConsequenceInfo.ActTriageLevelName)
                .Map(dest => dest.TriageDeptName, sou => sou.ConsequenceInfo.TriageDeptName)
                .Map(dest => dest.TriageDeptCode, sou => sou.ConsequenceInfo.TriageDept)
                .Map(dest => dest.TriageErrorFlag, sou => !string.IsNullOrWhiteSpace(sou.ConsequenceInfo.ChangeLevel))
                .Map(dest => dest.ChangeLevel, sou => sou.ConsequenceInfo.ChangeLevel)
                .Map(dest => dest.CoughFlag,
                    sou => sou.AdmissionInfo != null &&
                           sou.AdmissionInfo.IsSoreThroatAndCough == "true")
                .Map(dest => dest.ChestFlag,
                    sou => sou.AdmissionInfo != null &&
                           sou.AdmissionInfo.IsSoreThroatAndCough == "true")
                .Map(dest => dest.FluFlag,
                    sou => sou.AdmissionInfo != null &&
                           sou.AdmissionInfo.IsAggregation == "true")
                .Map(dest => dest.IDNo, sou => sou.PatientInfo.IdentityNo)
                .Map(dest => dest.PastMedicalHistory, sou => sou.AdmissionInfo.PastMedicalHistory)
                .Map(dest => dest.AllergyHistory, sou => sou.AdmissionInfo.AllergyHistory)
                .Map(dest => dest.KeyDiseasesCode, sou => sou.PatientInfo.DiseaseCode)
                .Map(dest => dest.RegisterNo, sou => sou.RegisterInfo.RegisterNo)
                .Map(dest => dest.RegisterTime, sou => sou.RegisterInfo.RegisterTime)
                .Map(dest => dest.RegisterDoctorCode, sou => sou.RegisterInfo.RegisterDoctorCode)
                .Map(dest => dest.RegisterDoctorName, sou => sou.RegisterInfo.RegisterDoctorName)
                .Map(dest => dest.TriageDoctorCode, sou => sou.ConsequenceInfo.DoctorCode)
                .Map(dest => dest.TriageDoctorName, sou => sou.ConsequenceInfo.DoctorName)
                .Map(dest => dest.KeyDiseasesName, sou => sou.PatientInfo.DiseaseName)
                // 医保控费相关 S
                .Map(dest => dest.PatnId, src => src.PatientInfo.PatnId)
                .Map(dest => dest.CurrMDTRTId, src => src.PatientInfo.CurrMDTRTId)
                .Map(dest => dest.PoolArea, src => src.PatientInfo.PatnId)
                .Map(dest => dest.InsureType, src => src.PatientInfo.InsureType)
                .Map(dest => dest.OutSetlFlag, src => src.PatientInfo.OutSetlFlag)
                // 医保控费相关 E
                .Map(dest => dest.PatientNamePy, src => src.PatientInfo.Py)
                ;
            TypeAdapterConfig<PatientInfoMqDto, AdmissionRecord>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.PI_ID, sou => sou.TriagePatientInfoId)
                .Map(dest => dest.TriageDirectionCode, sou => sou.ConsequenceInfo.TriageTarget)
                .Map(dest => dest.TriageDirectionName, sou => sou.ConsequenceInfo.TriageTargetName)
                .Map(dest => dest.TriageLevel, sou => sou.ConsequenceInfo.ActTriageLevel)
                .Map(dest => dest.TriageLevelName, sou => sou.ConsequenceInfo.ActTriageLevelName)
                .Map(dest => dest.TriageDeptName, sou => sou.ConsequenceInfo.TriageDeptName)
                .Map(dest => dest.TriageDeptCode, sou => sou.ConsequenceInfo.TriageDept)
                .Map(dest => dest.TriageErrorFlag, sou => !string.IsNullOrWhiteSpace(sou.ConsequenceInfo.ChangeLevel))
                .Map(dest => dest.ChangeLevel, sou => sou.ConsequenceInfo.ChangeLevel)
                .Map(dest => dest.CoughFlag,
                    sou => sou.AdmissionInfo != null &&
                           sou.AdmissionInfo.IsSoreThroatAndCough == "true")
                .Map(dest => dest.ChestFlag,
                    sou => sou.AdmissionInfo != null &&
                           sou.AdmissionInfo.IsSoreThroatAndCough == "true")
                .Map(dest => dest.FluFlag,
                    sou => sou.AdmissionInfo != null &&
                           sou.AdmissionInfo.IsAggregation == "true")
                .Map(dest => dest.IDNo, sou => sou.IdentityNo)
                .Map(dest => dest.PastMedicalHistory, sou => sou.AdmissionInfo.PastMedicalHistory)
                .Map(dest => dest.AllergyHistory, sou => sou.AdmissionInfo.AllergyHistory)
                .Map(dest => dest.KeyDiseasesCode, sou => sou.DiseaseCode)
                .Map(dest => dest.RegisterNo, sou => sou.RegisterNo)
                .Map(dest => dest.KeyDiseasesName, sou => sou.DiseaseName)
                .Map(dest => dest.ChargeType, sou => sou.ChargeType)
                .Map(dest => dest.ChargeTypeName, sou => sou.ChargeTypeName)
                .Map(dest => dest.RegType, sou => sou.RegType)
                .Map(dest => dest.ToHospitalWayCode, sou => sou.ToHospitalWay)
                .Map(dest => dest.InDeptWay, sou => sou.ToHospitalWayName) //ToHospitalWayName和InDeptWay业务上意义相同
                .Map(dest => dest.GreenRoadCode, sou => sou.GreenRoad)
                .Map(dest => dest.HomeAddress, sou => sou.Address)
                .Map(dest => dest.ContactsPerson, sou => sou.ContactsPerson)
                .Map(dest => dest.ContactsPhone, sou => sou.ContactsPhone)
                .Map(dest => dest.GuardianIdCardNo, sou => sou.GuardianIdCardNo)
                .Map(dest => dest.GuardianIdTypeCode, sou => sou.GuardianIdTypeCode)
                .Map(dest => dest.GuardianIdTypeName, sou => sou.GuardianIdTypeName)
                .Map(dest => dest.GuardianIdTypeName, sou => sou.GuardianIdTypeName)
                // 医保控费相关 S
                .Map(dest => dest.PatnId, src => src.PatnId)
                .Map(dest => dest.CurrMDTRTId, src => src.CurrMDTRTId)
                .Map(dest => dest.PoolArea, src => src.PoolArea)
                .Map(dest => dest.InsureType, src => src.InsureType)
                .Map(dest => dest.OutSetlFlag, src => src.OutSetlFlag)
                // 医保控费相关 E
                .Map(dest => dest.PatientNamePy, src => src.Py)
                .Ignore(dest => dest.VisitStatus)
                // .Map(dest => dest.RegisterTime, sou => sou.RegisterInfo.RegisterTime)
                // .Map(dest => dest.RegisterDoctorCode, sou => sou.RegisterInfo.RegisterDoctorCode)
                // .Map(dest => dest.RegisterDoctorName, sou => sou.RegisterInfo.RegisterDoctorName)
                ;

            TypeAdapterConfig<UpdatePatientInfoDto, UpdatePatientInfoMqDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "")
                .Map(dest => dest.Id, sou => sou.PI_ID)
                .Map(dest => dest.IdentityNo, sou => sou.IDNo)
                .Map(dest => dest.Narration, sou => sou.NarrationCode)
                .Map(dest => dest.HomeAddress, sou => sou.HomeAddress)
                ;

            TypeAdapterConfig<CreateDiagnoseRecordDto, DiagnoseRecord>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            TypeAdapterConfig<HospitalApplyRecord, HospitalApplyRecordDto>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");


            TypeAdapterConfig<CreateCollectionDiagnoseDto, DiagnoseRecord>
                .NewConfig()
                .AddDestinationTransform((string x) => !string.IsNullOrWhiteSpace(x) ? x : "");

            #region 报卡
            TypeAdapterConfig<ReportCardCreateDto, ReportCard>.NewConfig();
            TypeAdapterConfig<ReportCardRelatedDiagnose, ReportCardRelatedDiagnoseDto>.NewConfig();
            TypeAdapterConfig<ReportCard, ReportCardDto>
                .NewConfig()
                .Map(dto => dto.RelatedDiagnoseList, entity => entity.RelatedDiagnoseList.Adapt<List<ReportCardRelatedDiagnoseDto>>());

            TypeAdapterConfig<ReportCardPluginSettingDto, ReportCardPluginSetting>
                .NewConfig();

            #endregion
        }
    }
}