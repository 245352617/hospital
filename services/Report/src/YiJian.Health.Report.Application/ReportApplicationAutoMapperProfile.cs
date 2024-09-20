using AutoMapper;
using HealthReportService;
using Newtonsoft.Json;
using YiJian.Health.Report.Domain.PhraseCatalogues.Entities;
using YiJian.Health.Report.Emrs.Dto;
using YiJian.Health.Report.Hospitals.Dto;
using YiJian.Health.Report.Hospitals.Entities;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Dto;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingSettings.Dto;
using YiJian.Health.Report.NursingSettings.Entities;
using YiJian.Health.Report.PrintCatalogs;
using YiJian.Health.Report.PrintSettings;
using YiJian.Health.Report.ReportDatas;
using YiJian.Health.Report.ReportDatas.Dto;
using YiJian.Health.Report.Statisticses.Dto;
using YiJian.Health.Report.Statisticses.Entities;
using YiJian.Report.PhraseCatalogues.Dto;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportApplicationAutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportApplicationAutoMapperProfile()
        {
            CreateMap<NursingDocument, NursingDocumentDto>()
                .ForMember(dest => dest.Sheet, act => act.Ignore())
                .ForMember(dest => dest.CriticalIllnessList, act => act.Ignore())
                .ForMember(dest => dest.DynamicField, act => act.Ignore())
                .ForMember(dest => dest.InIntakesTotal, act => act.Ignore())
                .ForMember(dest => dest.OutIntakesTotal, act => act.Ignore());
            CreateMap<NursingRecord, NursingRecordDto>()
                .ForMember(dest => dest.InIntakes, act => act.Ignore())
                .ForMember(dest => dest.IntakeDtos, act => act.MapFrom(x => x.Intakes))
                .ForMember(dest => dest.OutIntakes, act => act.Ignore())
                .ForMember(dest => dest.PupilLeft, act => act.Ignore())
                .ForMember(desct => desct.PupilRight, act => act.Ignore())
                .ForMember(desct => desct.DynamicDataList, act => act.Ignore())
                .ForMember(desct => desct.IsStatistics, act => act.Ignore())
                .ForMember(desct => desct.InIntakesTotal, act => act.Ignore())
                .ForMember(desct => desct.OutIntakesTotal, act => act.Ignore())
                .ForMember(desct => desct.IntakeStatisticsId, act => act.Ignore())
                .ForMember(desct => desct.Begintime, act => act.Ignore())
                .ForMember(desct => desct.Endtime, act => act.Ignore());
            CreateMap<DynamicField, DynamicFieldDto>()
                .ForMember(desct => desct.FieldName, act => act.Ignore());
            CreateMap<DynamicFieldDto, DynamicField>()
                .ForMember(dest => dest.NursingDocument, act => act.Ignore())
                .ForMember(dest => dest.IsDeleted, act => act.Ignore())
                .ForMember(dest => dest.DeleterId, act => act.Ignore())
                .ForMember(dest => dest.DeletionTime, act => act.Ignore())
                .ForMember(dest => dest.LastModificationTime, act => act.Ignore())
                .ForMember(dest => dest.LastModifierId, act => act.Ignore())
                .ForMember(dest => dest.CreationTime, act => act.Ignore())
                .ForMember(dest => dest.CreatorId, act => act.Ignore());
            CreateMap<DynamicData, DynamicDataDto>();
            CreateMap<CriticalIllness, CriticalIllnessDto>();
            CreateMap<Mmol, MmolDto>();
            CreateMap<Mmol, MmolBaseDto>();
            CreateMap<Pupil, PupilDto>();
            CreateMap<SpecialNursingRecord, SpecialNursingRecordDto>();
            CreateMap<Intake, IntakeDto>().ForMember(i => i.RecordTime, n => n.Ignore()).ForMember(i => i.ContentUnit, n => n.Ignore());
            CreateMap<NursingSetting, NursingSettingDto>()
                .ForMember(dest => dest.Header, act => act.MapFrom(a => a.Category));
            CreateMap<NursingSettingHeader, NursingSettingHeaderDto>();
            CreateMap<NursingSettingHeader, NursingSettingHeaderBaseDto>();
            CreateMap<NursingSettingHeader, NursingInputOptionsDto>();
            CreateMap<NursingSettingItem, NursingSettingItemDto>();
            CreateMap<Characteristic, CharacteristicDto>();
            CreateMap<HospitalInfo, HospitalInfoDto>();
            CreateMap<PrintCatalog, PrintCatalogDto>();
            CreateMap<PrintCatalog, PrintCatalogListDto>().ForMember(i => i.PrintSettingList, n => n.Ignore());
            CreateMap<PrintSetting, PrintSettingListDto>()
                .ForMember(dest => dest.Comm, act => act.MapFrom(map => map.ReportTypeCode))
                .ForMember(dest => dest.UsageCode, act => act.Ignore())
                .ForMember(dest => dest.SeparationId, act => act.Ignore())
                .ForMember(dest => dest.PageSizeHeight, act => act.Ignore())
                .ForMember(dest => dest.PageSizeWidth, act => act.Ignore())
                .ForMember(dest => dest.Child, act => act.Ignore())
                .ForMember(dest => dest.PageCount, act => act.Ignore())
                ;
            CreateMap<PrintSetting, PrintSettingDto>()
                .ForMember(dest => dest.Comm, act => act.MapFrom(map => map.ReportTypeCode))
                .ForMember(dest => dest.PageSizeHeight, act => act.Ignore())
                .ForMember(dest => dest.PageSizeWidth, act => act.Ignore());
            CreateMap<PageSize, PageSizeDto>();

            CreateMap<NursingRecord, EmrNursingRecordDto>()
                .ForMember(dest => dest.InIntakes, act => act.Ignore())
                .ForMember(dest => dest.OutIntakes, act => act.Ignore());
            CreateMap<ReportData, ReportDataDto>();
            this.CreateGrpcMaps();
            CreateMap<WardRoundDto, WardRound>()
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.IsDeleted, act => act.Ignore())
                .ForMember(dest => dest.DeleterId, act => act.Ignore())
                .ForMember(dest => dest.DeletionTime, act => act.Ignore())
                .ForMember(dest => dest.LastModificationTime, act => act.Ignore())
                .ForMember(dest => dest.LastModifierId, act => act.Ignore())
                .ForMember(dest => dest.CreationTime, act => act.Ignore())
                .ForMember(dest => dest.CreatorId, act => act.Ignore())
                .ForMember(dest => dest.Signature, act => act.MapFrom(src => JsonConvert.SerializeObject(src.Signature)));
            CreateMap<WardRound, WardRoundDto>()
                .ForMember(dest => dest.Signature, act => act.MapFrom(src => JsonConvert.DeserializeObject(src.Signature)));

            CreateMap<PhraseCatalogue, PhraseCatalogueInfoDto>()
                        .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<PhraseCatalogue, PhraseCatalogueSampleDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<PhraseCatalogueInfoDto, PhraseCatalogue>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id ?? 0))
                .ForMember(dest => dest.Phrases, opt => opt.Ignore())
                .ForMember(m => m.IsDeleted, opt => opt.Ignore())
                .ForMember(m => m.DeleterId, opt => opt.Ignore())
                .ForMember(m => m.DeletionTime, opt => opt.Ignore())
                .ForMember(m => m.LastModificationTime, opt => opt.Ignore())
                .ForMember(m => m.LastModifierId, opt => opt.Ignore())
                .ForMember(m => m.CreatorId, opt => opt.Ignore())
                .ForMember(m => m.CreationTime, opt => opt.Ignore())
                .ForMember(m => m.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(m => m.ExtraProperties, opt => opt.Ignore());

            CreateMap<Phrase, PhraseDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<PhraseDto, Phrase>()
                .ForMember(dest => dest.Catalogue, opt => opt.Ignore());

            CreateMap<IntakeSetting, IntakeSettingDto>();
            //.ForMember(dest => dest.IntakeType, act => act.MapFrom(a => a.IntakeType));

            CreateMap<IntakeSettingDto, IntakeSetting>().ForMember(m => m.IsDeleted, opt => opt.Ignore())
                .ForMember(m => m.DeleterId, opt => opt.Ignore())
                .ForMember(m => m.DeletionTime, opt => opt.Ignore())
                .ForMember(m => m.LastModificationTime, opt => opt.Ignore())
                .ForMember(m => m.LastModifierId, opt => opt.Ignore())
                .ForMember(m => m.CreatorId, opt => opt.Ignore())
                .ForMember(m => m.CreationTime, opt => opt.Ignore())
                .ForMember(m => m.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(m => m.ExtraProperties, opt => opt.Ignore());
            //.ForMember(dest => dest.Header, act => act.MapFrom(a => a.Category));
            #region Rp

            CreateMap<StatisticsMonthDoctorAndPatient, StatisticsMonthDoctorAndPatientDto>()
                .ForMember(dest => dest.Ratio, act => act.Ignore())
                .ForMember(dest => dest.FormatRatio, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsQuarterDoctorAndPatient, StatisticsQuarterDoctorAndPatientDto>()
                .ForMember(dest => dest.Ratio, act => act.Ignore())
                .ForMember(dest => dest.FormatRatio, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsYearDoctorAndPatient, StatisticsYearDoctorAndPatientDto>()
                .ForMember(dest => dest.Ratio, act => act.Ignore())
                .ForMember(dest => dest.FormatRatio, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());

            CreateMap<StatisticsMonthNurseAndPatient, StatisticsMonthNurseAndPatientDto>()
                .ForMember(dest => dest.Ratio, act => act.Ignore())
                .ForMember(dest => dest.FormatRatio, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsQuarterNurseAndPatient, StatisticsQuarterNurseAndPatientDto>()
                .ForMember(dest => dest.Ratio, act => act.Ignore())
                .ForMember(dest => dest.FormatRatio, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsYearNurseAndPatient, StatisticsYearNurseAndPatientDto>()
                .ForMember(dest => dest.Ratio, act => act.Ignore())
                .ForMember(dest => dest.FormatRatio, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());

            CreateMap<StatisticsMonthLevelAndPatient, StatisticsMonthLevelAndPatientDto>()
                .ForMember(dest => dest.Total, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsQuarterLevelAndPatient, StatisticsQuarterLevelAndPatientDto>()
                .ForMember(dest => dest.Total, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsYearLevelAndPatient, StatisticsYearLevelAndPatientDto>()
                .ForMember(dest => dest.Total, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());

            CreateMap<StatisticsMonthEmergencyroomAndPatient, StatisticsMonthEmergencyroomAndPatientDto>()
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsQuarterEmergencyroomAndPatient, StatisticsQuarterEmergencyroomAndPatientDto>()
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsYearEmergencyroomAndPatient, StatisticsYearEmergencyroomAndPatientDto>()
                .ForMember(dest => dest.FormatDate, act => act.Ignore());

            CreateMap<StatisticsMonthEmergencyroomAndDeathPatient, StatisticsMonthEmergencyroomAndDeathPatientDto>()
                .ForMember(dest => dest.DeathRate, act => act.Ignore())
                .ForMember(dest => dest.FormatDeathRate, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsQuarterEmergencyroomAndDeathPatient, StatisticsQuarterEmergencyroomAndDeathPatientDto>()
                .ForMember(dest => dest.DeathRate, act => act.Ignore())
                .ForMember(dest => dest.FormatDeathRate, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());
            CreateMap<StatisticsYearEmergencyroomAndDeathPatient, StatisticsYearEmergencyroomAndDeathPatientDto>()
                .ForMember(dest => dest.DeathRate, act => act.Ignore())
                .ForMember(dest => dest.FormatDeathRate, act => act.Ignore())
                .ForMember(dest => dest.FormatDate, act => act.Ignore());

            CreateMap<UspDoctorPatientRatio, UspDoctorPatientRatioResponseDto>();
            CreateMap<ViewAdmissionRecord, ViewAdmissionRecordResponseDto>()
                .ForMember(dest => dest.TransferType, act => act.Ignore())
                .ForMember(dest => dest.TransferTime, act => act.Ignore())
                .ForMember(dest => dest.ResidenceTime, act => act.Ignore())
                .ForMember(dest => dest.FromAreaCode, act => act.Ignore())
                .ForMember(dest => dest.ToAreaCode, act => act.Ignore())
                .ForMember(dest => dest.ToArea, act => act.Ignore())
                .ForMember(dest => dest.TransferReason, act => act.Ignore());
            CreateMap<UspNursePatientRatio, UspNursePatientRatioResponseDto>();

            CreateMap<StatisticsMonthLevelAndPatientDto, StatisticsLevelAndPatientResponseDto>();
            CreateMap<StatisticsQuarterLevelAndPatientDto, StatisticsLevelAndPatientResponseDto>();
            CreateMap<StatisticsYearLevelAndPatientDto, StatisticsLevelAndPatientResponseDto>();

            CreateMap<StatisticsMonthEmergencyroomAndPatientDto, StatisticsEmergencyroomAndPatientResponseDto>();
            CreateMap<StatisticsQuarterEmergencyroomAndPatientDto, StatisticsEmergencyroomAndPatientResponseDto>();
            CreateMap<StatisticsYearEmergencyroomAndPatientDto, StatisticsEmergencyroomAndPatientResponseDto>();

            CreateMap<StatisticsMonthEmergencyroomAndDeathPatientDto, StatisticsEmergencyroomAndDeathPatientResponseDto>();
            CreateMap<StatisticsQuarterEmergencyroomAndDeathPatientDto, StatisticsEmergencyroomAndDeathPatientResponseDto>();
            CreateMap<StatisticsYearEmergencyroomAndDeathPatientDto, StatisticsEmergencyroomAndDeathPatientResponseDto>();


            #endregion

        }

        public void CreateGrpcMaps()
        {
            AllowNullDestinationValues = false;

            CreateMap<NursingRecord, GrpcVitalSignsModel>()
                .ForMember(dest => dest.RecordTime, opt => { opt.MapFrom(src => src.RecordTime.ToString()); })
                .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.T.ToString()))
                .ForMember(dest => dest.Heart, opt => opt.MapFrom(src => src.HR.ToString()))
                .ForMember(dest => dest.Breath, opt => opt.MapFrom(src => src.R.ToString()))
                .ForMember(dest => dest.BP, opt => opt.MapFrom(src => src.BP.ToString()))
                .ForMember(dest => dest.BP2, opt => opt.MapFrom(src => src.BP2.ToString()))
                .ForMember(dest => dest.SPO2, opt => opt.MapFrom(src => src.SPO2.ToString()));
        }
    }
}