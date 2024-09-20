using AutoMapper;
using MasterDataService;
using System;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.MasterData.AllItems;
using YiJian.MasterData.Departments;
using YiJian.MasterData.DictionariesMultitypes;
using YiJian.MasterData.DictionariesTypes;
using YiJian.MasterData.Domain;
using YiJian.MasterData.Exams;
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Container;
using YiJian.MasterData.Labs.Position;
using YiJian.MasterData.MasterData.Doctors;
using YiJian.MasterData.MasterData.HospitalClient;
using YiJian.MasterData.MasterData.HospitalClient.Doctors;
using YiJian.MasterData.MasterData.Pharmacies.Dto;
using YiJian.MasterData.MasterData.Regions;
using YiJian.MasterData.MasterData.Separations.Dto;
using YiJian.MasterData.MasterData.Treats;
using YiJian.MasterData.Medicines;
using YiJian.MasterData.Pharmacies.Entities;
using YiJian.MasterData.Regions;
using YiJian.MasterData.Separations.Entities;
using YiJian.MasterData.Sequences;
using YiJian.MasterData.Treats;
using YiJian.MasterData.ViewSettings;
using YiJian.MasterData.VitalSign;

namespace YiJian.MasterData;

/// <summary>
/// MasterDataApplicationAutoMapperProfile
/// </summary>
public class MasterDataApplicationAutoMapperProfile : Profile
{
    /// <summary>
    /// MasterDataApplicationAutoMapperProfile
    /// </summary>
    public MasterDataApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        /* 字典 */
        CreateMap<Dictionaries, DictionariesDto>();
        CreateMap<CreateOrUpdateDictionariesDto, Dictionaries>()
            .ForMember(i => i.Py, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore());


        //手术字典
        CreateMap<Operation, OperationDto>();

        //检验明细项
        CreateMap<LabTarget, LabTargetData>();
        CreateMap<LabTarget, LabTargetsModel>();

        CreateMap<LabTargetCreation, LabTarget>().ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore()).ForMember(i => i.ProjectType, n => n.Ignore())
            .ForMember(i => i.ProjectMerge, n => n.Ignore());
        //字典类型编码
        CreateMap<DictionariesTypes.DictionariesType, DictionariesTypeData>();
        //字典多类型编码
        CreateMap<DictionariesMultitype, DictionariesMultitypeDto>();
        CreateMap<DictionariesMultitypeDto, DictionariesMultitype>();

        //检验标本
        CreateMap<LabSpecimen, LabSpecimenData>()
            .ForMember(i => i.SpecimenPartCode, n => n.Ignore())
            .ForMember(i => i.SpecimenPartName, n => n.Ignore());

        CreateMap<LabSpecimenCreation, LabSpecimen>().ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore());


        //检查部位
        CreateMap<ExamPart, ExamPartData>();

        //检查明细项
        CreateMap<ExamTarget, ExamTargetData>();
        CreateMap<ExamTarget, ExamTargetsModel>();

        CreateMap<ExamTargetCreation, ExamTarget>().ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.ProjectType, n => n.Ignore())
            .ForMember(i => i.ProjectMerge, n => n.Ignore());


        //编码
        CreateMap<Sequence, SequenceData>();

        CreateMap<MedicineCreation, Medicine>()
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.IsSyncToReciped, n => n.Ignore());
        //his返回实体映射
        // CreateMap<MedicinesHisOutParams, Medicine>()
        //     .ForMember(i => i.MedicineCode, n => n.MapFrom(m => m.DrugCode))
        //     .ForMember(i => i.MedicineName, n => n.MapFrom(m => m.DrugName))
        //     .ForMember(i => i.MedicineProperty, n => n.MapFrom(m => m.DrugType))
        //     .ForMember(i => i.Specification, n => n.MapFrom(m => m.DrugSpec))
        //     .ForMember(i => i.Unit, n => n.MapFrom(m => m.DrugUnit))
        //     .ForMember(i => i.Price, n => n.MapFrom(m => m.DrugPrice))
        //     .ForMember(i => i.ExpirDate, n => n.MapFrom(m => m.ExpiryDate))
        //     .ForMember(i => i.DosageForm, n => n.MapFrom(m => m.DrugForm))
        //     .ForMember(i => i.BatchNo, n => n.MapFrom(m => m.PassNo))
        //     .ForMember(i => i.AntibioticPermission, n => n.MapFrom(m => m.AntibioticSign))
        //     .ForMember(i => i.FactoryName, n => n.MapFrom(m => m.Firm))
        //     .ForMember(i => i.DefaultDosage, n => n.MapFrom(m => m.DrugUseOneDosage))
        //     .ForMember(i => i.DosageUnit, n => n.MapFrom(m => m.DrugUseOneDosageUnit))
        //     .ForMember(i => i.PyCode, n => n.MapFrom(m => m.PinyinCode))
        //     .ForMember(i => i.PharmacyCode, n => n.MapFrom(m => m.PharmacyType))
        //     ;


        //药品频次字典
        CreateMap<MedicineFrequency, MedicineFrequencyData>()
            .ForMember(i => i.FrequencyTimes, n => n.MapFrom(m => m.Times))
            .ForMember(i => i.FrequencyUnit, n => n.MapFrom(m => m.Unit))
            .ForMember(i => i.FrequencyExecDayTimes, n => n.MapFrom(m => m.ExecDayTimes));

        //药品用法字典
        CreateMap<MedicineUsage, MedicineUsageData>();

        //容器编码
        CreateMap<LabContainer, LabContainerData>();

        //检验标本采集部位
        CreateMap<LabSpecimenPosition, LabSpecimenPositionData>();

        //诊疗项目字典
        CreateMap<Treat, TreatData>()
            .ForMember(i => i.Code, n => n.MapFrom(m => m.TreatCode))
            .ForMember(i => i.Name, n => n.MapFrom(m => m.TreatName))
            .ForMember(i => i.ProjectType, n => n.MapFrom(m => m.CategoryCode))
            .ForMember(i => i.ProjectTypeName, n => n.MapFrom(m => m.CategoryName))
            .ForMember(i => i.RetPrice, n => n.MapFrom(m => m.Price));

        //视图配置
        CreateMap<ViewSetting, ViewSettingData>();

        //诊疗检查检验药品项目合集
        CreateMap<AllItem, AllItemData>();

        CreateMap<AllItem, AllItemDataPreHospitalDto>();

        //药品表
        CreateMap<Medicine, MedicineData>()
            .ForMember(i => i.Code, n => n.MapFrom(m => m.MedicineCode))
            .ForMember(i => i.Name, n => n.MapFrom(m => m.MedicineName))
            .ForMember(i => i.CategoryCode, n => n.Ignore())
            .ForMember(i => i.CategoryName, n => n.Ignore())
            .ForMember(i => i.FrequencyTimes, n => n.Ignore())
            .ForMember(i => i.FrequencyUnit, n => n.Ignore())
            .ForMember(i => i.FrequencyExecDayTimes, n => n.Ignore())
            .ForMember(i => i.DailyFrequency, n => n.Ignore())
            .ForMember(i => i.FrequencyCode,
                n => n.MapFrom(x => x.FrequencyName.IsNullOrEmpty() ? "" : x.FrequencyCode))
            .ForMember(i => i.FrequencyName,
                n => n.MapFrom(x => x.FrequencyCode.IsNullOrEmpty() ? "" : x.FrequencyName));

        CreateMap<MedicineUpdate, Medicine>().ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.MedicineCode, n => n.Ignore())
            .ForMember(i => i.MedicineName, n => n.MapFrom(m => m.Name))
            .ForMember(i => i.IsSyncToReciped, n => n.Ignore());

        //药品用法
        CreateMap<MedicineUsageCreation, MedicineUsage>().ForMember(i => i.PyCode, n => n.Ignore())
            .ForMember(i => i.WbCode, n => n.Ignore())
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.AddCard, n => n.Ignore())
            ;
        //药品频次
        CreateMap<MedicineFrequencyCreation, MedicineFrequency>()
            .ForMember(i => i.Weeks, n => n.MapFrom(m => m.FrequencyWeeks))
            .ForMember(i => i.Unit, n => n.MapFrom(m => m.FrequencyUnit))
            .ForMember(i => i.ExecDayTimes, n => n.MapFrom(m => m.FrequencyExecDayTimes))
            .ForMember(i => i.Times, n => n.MapFrom(m => m.FrequencyTimes))
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore());

        //评分项
        CreateMap<VitalSignExpression, VitalSignExpressionData>();

        //检查申请项目
        CreateMap<ExamProject, ExamProjectData>()
            .ForMember(i => i.Code, n => n.MapFrom(m => m.ProjectCode))
            .ForMember(i => i.Name, n => n.MapFrom(m => m.ProjectName))
            .ForMember(i => i.FirstCatalogCode, n => n.MapFrom(m => m.FirstNodeCode))
            .ForMember(i => i.FirstCatalogName, n => n.Ignore())
            .ForMember(i => i.CategoryCode, n => n.Ignore())
            .ForMember(i => i.CategoryName, n => n.Ignore())
            .ForMember(i => i.GuideName, n => n.Ignore());
        //检查部位
        CreateMap<ExamPartCreation, ExamPart>().ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore());


        //检查目录
        CreateMap<ExamCatalog, ExamCatalogData>()
            .ForMember(i => i.ExamProject, n => n.Ignore());
        CreateMap<ExamCatalog, ExamCatalogDataV2>().ForMember(i => i.Children, n => n.Ignore());
        CreateMap<ExamCatalogData, ExamCatalogDataV2>()
            .ForMember(i => i.Children, n => n.Ignore());

        //检查申请注意事项
        CreateMap<ExamNote, ExamNoteData>();
        CreateMap<ExamNoteCreation, ExamNote>().ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore());


        //检验目录
        CreateMap<LabCatalog, LabCatalogData>().ForMember(i => i.LabProjects, n => n.Ignore());
        CreateMap<LabCatalogCreation, LabCatalog>().ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            ;


        //检验项目
        CreateMap<LabProject, LabProjectData>().ForMember(i => i.CategoryCode, n => n.Ignore())
            .ForMember(i => i.CategoryName, n => n.Ignore())
            .ForMember(i => i.Code, n => n.MapFrom(m => m.ProjectCode))
            .ForMember(i => i.Name, n => n.MapFrom(m => m.ProjectName))
            .ForMember(i => i.SpecimenCode,
                n => n.MapFrom(x => x.SpecimenName.IsNullOrEmpty() ? "" : x.SpecimenCode))
            .ForMember(i => i.SpecimenName,
                n => n.MapFrom(x => x.SpecimenCode.IsNullOrEmpty() ? "" : x.SpecimenName))
            .ForMember(i => i.SpecimenPartCode, n => n.Ignore())
            .ForMember(i => i.SpecimenPartName, n => n.Ignore())
            .ForMember(i => i.GuideName, n => n.Ignore())
            ;

        CreateMap<Separation, SeparationDto>();
        CreateMap<Usage, UsageDto>();

        CreateMap<Pharmacy, ModifyPharmacyDto>();
        CreateMap<TreatGroup, TreatCatalogDto>();
        CreateMap<Region, RegionDto>();
        CreateMap<Medicine, ToxicEto>()
            .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.Id));
        CreateMap<HISMedicine, ToxicEto>()
        .ForMember(dest => dest.IsCompound, opt => opt.MapFrom(src => src.IsCompound == "1"))
        .ForMember(dest => dest.IsRefrigerated, opt => opt.MapFrom(src => src.IsRefrigerated == "1"))
        .ForMember(dest => dest.IsTumour, opt => opt.MapFrom(src => src.IsTumour == "1"))
        .ForMember(dest => dest.IsLimited, opt => opt.MapFrom(src => src.IsLimited == "1"))
        .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.InvId))
        .ForMember(dest => dest.IsHighRisk, opt => opt.MapFrom(src => src.IsHighRisk == "1"));

        CreateHisMaps();
        this.CreateGrpcMaps();

        /* 科室诊室 */
        CreateMap<Department, DepartmentData>();
        CreateMap<Department, DepartmentModel>();
        CreateMap<ConsultingRoom, ConsultingRoomData>();
        CreateMap<ConsultingRoom, ConsultingRoomModel>();
        CreateMap<DoctorEto, Doctor>()
            .ForMember(dest => dest.DrugAuthority, opt => opt.MapFrom(src => src.DrugAuthority == "Y"))
            .ForMember(dest => dest.AnaesthesiaAuthority, opt => opt.MapFrom(src => src.AnaesthesiaAuthority == "Y"))
            .ForMember(dest => dest.AntibioticAuthority, opt => opt.MapFrom(src => src.AntibioticAuthority == "Y"))
            .ForMember(dest => dest.SpiritAuthority, opt => opt.MapFrom(src => src.SpiritAuthority == "Y"))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive == 0));

        CreateMap<Doctor, DoctorDto>();
        CreateMap<DdpHisMedicineResponse, MedicineData>();
        CreateMap<DdpHisMedicineResponse, GrpcMedicineModel>()
            .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.MedicineProperty))
            .ForMember(dest => dest.ExpireDate, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryCode, opt => opt.Ignore())
            .ForMember(dest => dest.ToxicProperty, opt => opt.Ignore())
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remark))
            .ForMember(dest => dest.FrequencyTimes, opt => opt.Ignore())
            .ForMember(dest => dest.FrequencyUnit, opt => opt.Ignore())
            .ForMember(dest => dest.FrequencyExecDayTimes, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeName, opt => opt.Ignore())
            .ForMember(dest => dest.BatchNo, opt => opt.Ignore())
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.DailyFrequency, opt => opt.Ignore());

        CreateMap<DdpHisMedicineResponse, ToxicEto>();
        CreateMap<DdpHisMedicineResponse, HISMedicine>()
        .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.MedicineCode, opt => opt.MapFrom(src => src.Code))
        .ForMember(dest => dest.UsageCode, opt => opt.MapFrom(src => src.UsageCode.DdpParseNullableDecimal()));



    }

    public void CreateHisMaps()
    {
        #region His

        //诊疗
        CreateMap<TreatsEto, Treat>()
            .ForMember(i => i.TreatCode, n => n.MapFrom(m => m.ProjectId))
            .ForMember(i => i.TreatName, n => n.MapFrom(m => m.ProjectName))
            .ForMember(i => i.CategoryName, n => n.MapFrom(m => m.ProjectTypeName))
            .ForMember(i => i.CategoryCode, n => n.MapFrom(m => m.ProjectType))
            .ForMember(i => i.OtherPrice, n => n.MapFrom(m => m.ChargeAmount))
            .ForMember(i => i.PyCode, n => n.MapFrom(m => m.SpellCode))
            .ForMember(i => i.WbCode, n => n.MapFrom(m => m.ProjectName.FirstLetterPY()))
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.Specification, n => n.Ignore())
            .ForMember(i => i.FrequencyCode, n => n.Ignore())
            .ForMember(i => i.ExecDeptCode, n => n.Ignore())
            .ForMember(i => i.ExecDeptName, n => n.Ignore())
            .ForMember(i => i.FeeTypeMainCode, n => n.Ignore())
            .ForMember(i => i.FeeTypeSubCode, n => n.Ignore())
            .ForMember(i => i.PlatformType, n => n.Ignore())
            ;

        //用法
        CreateMap<UsagesEto, MedicineUsage>()
            .ForMember(i => i.UsageCode, n => n.MapFrom(m => m.DrugUsageId))
            .ForMember(i => i.UsageName, n => n.MapFrom(m => m.DrugUsageName))
            .ForMember(i => i.FullName, n => n.MapFrom(m => m.DrugUsageName))
            .ForMember(i => i.PyCode, n => n.MapFrom(m => m.SpellCode))
            .ForMember(i => i.WbCode, n => n.MapFrom(m => m.DrugUsageName.FirstLetterWB()))
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore())
            .ForMember(i => i.Remark, n => n.Ignore())
            .ForMember(i => i.IsSingle, n => n.Ignore())
            .ForMember(i => i.Catalog, n => n.Ignore())
            .ForMember(i => i.Sort, n => n.Ignore())
            .ForMember(i => i.TreatCode, n => n.Ignore())
            .ForMember(i => i.IsActive, n => n.Ignore())
            ;
        //频次
        CreateMap<FrequencyEto, MedicineFrequency>()
            .ForMember(i => i.ThirdPartyId, n => n.MapFrom(m => m.DrugfrequencyId))
            .ForMember(i => i.FrequencyCode, n => n.MapFrom(m => m.DrugfrequencyCode))
            .ForMember(i => i.FrequencyName, n => n.MapFrom(m => m.DrugfrequencyName))
            .ForMember(i => i.FullName, n => n.MapFrom(m => m.DrugfrequencyName))
            .ForMember(i => i.Times, n => n.MapFrom(m => m.DailyFrequency))
            .ForMember(i => i.ExecDayTimes, n => n.MapFrom(m => m.ExecutionTime))
            .ForMember(i => i.Sort, n => n.MapFrom(m => m.ArrangementOrder))
            .ForMember(i => i.IsDeleted, n => n.Ignore())
            .ForMember(i => i.DeleterId, n => n.Ignore())
            .ForMember(i => i.DeletionTime, n => n.Ignore())
            .ForMember(i => i.LastModificationTime, n => n.Ignore())
            .ForMember(i => i.LastModifierId, n => n.Ignore())
            .ForMember(i => i.CreationTime, n => n.Ignore())
            .ForMember(i => i.CreatorId, n => n.Ignore())
            .ForMember(i => i.ConcurrencyStamp, n => n.Ignore())
            .ForMember(i => i.Id, n => n.Ignore())
            .ForMember(i => i.Unit, n => n.Ignore())
            .ForMember(i => i.Weeks, n => n.Ignore())
            .ForMember(i => i.Catalog, n => n.Ignore())
            .ForMember(i => i.Remark, n => n.Ignore())
            .ForMember(i => i.IsActive, n => n.Ignore())
            .ForMember(i => i.ExtraProperties, n => n.Ignore());

        #endregion
    }

    public void CreateGrpcMaps()
    {
        AllowNullDestinationValues = false;
        CreateMap<Medicine, GrpcMedicineModel>()
            .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.MedicineProperty))
            .ForMember(dest => dest.ExpireDate, opt =>
                opt.Ignore())
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.MedicineCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MedicineName))
            .ForMember(dest => dest.CategoryCode, opt => opt.Ignore())
            .ForMember(dest => dest.ToxicProperty, opt => opt.Ignore())
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remark))
            .ForMember(dest => dest.FrequencyTimes, opt => opt.Ignore())
            .ForMember(dest => dest.FrequencyUnit, opt => opt.Ignore())
            .ForMember(dest => dest.FrequencyExecDayTimes, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeName, opt => opt.Ignore())
            .ForMember(dest => dest.BatchNo, opt => opt.Ignore())
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.DailyFrequency, opt => opt.Ignore());

        CreateMap<LabProject, GrpcLabProjectModel>()
            .ForMember(i => i.PositionCode, n => n.MapFrom(m => m.SpecimenPartCode))
            .ForMember(i => i.PositionName, n => n.MapFrom(m => m.SpecimenPartName))
            .ForMember(i => i.ChargeCode, n => n.Ignore())
            .ForMember(i => i.ChargeName, n => n.Ignore())
            .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeName, opt => opt.Ignore());

        CreateMap<ExamProject, GrpcExamProjectModel>()
            .ForMember(i => i.PositionCode, n => n.MapFrom(m => m.PartCode ?? ""))
            .ForMember(i => i.PositionName, n => n.MapFrom(m => m.PartName ?? ""))
            .ForMember(i => i.GuideName, n => n.MapFrom(m => m.Note ?? ""))
            .ForMember(i => i.ReservationPlace, n => n.MapFrom(m => m.ReservationPlace ?? ""))
            .ForMember(i => i.ReservationTime, n => n.MapFrom(m => m.ReservationTime ?? ""))
            .ForMember(i => i.TemplateId, n => n.MapFrom(m => m.TemplateId ?? ""))
            .ForMember(i => i.PrescribeCode, n => n.MapFrom(m => m.PrescribeCode ?? ""))
            .ForMember(i => i.TreatCode, n => n.MapFrom(m => m.TreatCode ?? ""))
            .ForMember(i => i.DepExecutionRules, n => n.MapFrom(m => m.DepExecutionRules ?? ""))
            .ForMember(i => i.RoomCode, n => n.MapFrom(m => m.RoomCode ?? ""))
            .ForMember(i => i.RoomName, n => n.MapFrom(m => m.RoomName ?? ""))
            .ForMember(i => i.FirstCatalogCode, n => n.Ignore())
            .ForMember(i => i.FirstCatalogName, n => n.Ignore())
            .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeName, opt => opt.Ignore());
        CreateMap<TreatProjectModelDto, GrpcTreatProjectModel>()
            .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeName, opt => opt.Ignore());
        CreateMap<Treat, GrpcTreatProjectModel>()
            .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
            .ForMember(dest => dest.ChargeName, opt => opt.Ignore())
            .ForMember(dest => dest.DictionaryCode, opt => opt.Ignore())
            .ForMember(dest => dest.DictionaryName, opt => opt.Ignore());

        CreateMap<MedicineFrequency, GrpcFrequencyModel>()
            .ForMember(dest => dest.DailyFrequency, opt => opt.MapFrom(src => src.ThirdPartyId))
            .ForMember(dest => dest.FrequencyUnit, opt => opt.MapFrom(src => src.Unit));
        CreateMap<LabSpecimen, GrpcLabSpecimenModel>();
        CreateMap<LabSpecimenPosition, GrpcLabSpecimenPositionModel>();
        CreateMap<ExamPart, GrpcExamPartModel>()
            .ForMember(dest => dest.WbCode, opt => opt.Ignore());
        // 嘱托
        CreateMap<Entrust, EntrustDto>();
        // 药品用法
        CreateMap<MedicineUsage, MedicineUsageModel>();
        CreateMap<HISMedicine, Medicine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => (int)x.InvId))
            .ForMember(dest => dest.IsLimited, opt => opt.MapFrom(m => m.IsLimited.IsNullOrEmpty() ? (bool?)null : (m.IsLimited == "True" ? true : false)))
            .ForMember(dest => dest.IsCompound, opt => opt.MapFrom(x => x.IsCompound.IsNullOrEmpty() ? (bool?)null : (x.IsCompound == "True" ? true : false)))
            .ForMember(dest => dest.MedicineProperty, opt => opt.MapFrom(x => x.MedicineProperty.ToString()))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(x => x.Unit ?? ""))
            .ForMember(dest => dest.InsurancePayRate, opt => opt.Ignore())
            .ForMember(idest => idest.Volume, opt => opt.Ignore())
            .ForMember(dest => dest.IsCompound, opt => opt.Ignore())
            .ForMember(dest => dest.IsHighRisk, opt => opt.Ignore())
            .ForMember(dest => dest.IsRefrigerated, opt => opt.Ignore())
            .ForMember(dest => dest.IsTumour, opt => opt.Ignore());


        CreateMap<HISMedicine, MedicineData>()
            .ForMember(i => i.Id, n => n.MapFrom(m => m.InvId))
            .ForMember(i => i.Code, n => n.MapFrom(m => m.MedicineCode))
            .ForMember(i => i.Name, n => n.MapFrom(m => m.MedicineName))
            .ForMember(i => i.IsLimited, n => n.MapFrom(m => m.IsLimited == "1" ? true : false))
            .ForMember(i => i.InsurancePayRate, n => n.Ignore())
            .ForMember(i => i.Volume, n => n.Ignore())
            .ForMember(i => i.IsCompound, n => n.Ignore())
            .ForMember(i => i.IsHighRisk, n => n.Ignore())
            .ForMember(i => i.IsRefrigerated, n => n.Ignore())
            .ForMember(i => i.IsTumour, n => n.Ignore())
            .ForMember(i => i.PyCodeLen, n => n.Ignore());

        CreateMap<HISMedicine, GrpcMedicineModel>()
            .ForMember(i => i.Id, n => n.MapFrom(m => m.InvId))
            .ForMember(i => i.MedicineId, n => n.MapFrom(m => m.InvId))
            .ForMember(i => i.Code, n => n.MapFrom(m => m.MedicineCode))
            .ForMember(i => i.Name, n => n.MapFrom(m => m.MedicineName))
            .ForMember(i => i.IsLimited, n => n.MapFrom(m => m.IsLimited == "1" ? true : false))
            .ForMember(i => i.InsurancePayRate, n => n.Ignore())
            .ForMember(i => i.Volume, n => n.Ignore())
            .ForMember(i => i.IsCompound, n => n.Ignore())
            .ForMember(i => i.IsHighRisk, n => n.Ignore())
            .ForMember(i => i.IsRefrigerated, n => n.Ignore())
            .ForMember(i => i.IsTumour, n => n.Ignore());

        CreateMap<DictionariesMultitype, GetDictionariesMultiTypeResponse>();
        CreateMap<Dictionaries, GetDictionariesResponse>();
        CreateMap<ExecuteDepRuleDic, ExecuteDepRuleDicDto>();

    }
}