using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using MasterDataService;
using RecipeService;
using System;
using System.Linq;
using YiJian.Basic.RecipeProjects.Dto;
using YiJian.DoctorsAdvices.Dto;
using YiJian.DoctorsAdvices.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Documents.Dto;
using YiJian.Dosages.Dto;
using YiJian.ECIS.ShareModel.DDPs.Dto;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.Hospitals.Dto;
using YiJian.OwnMedicines.Entities;
using YiJian.OwnMediciness.Dto;
using YiJian.Preferences.Dto;
using YiJian.Recipe.Packages;
using YiJian.Recipe.Packages.Dtos;
using YiJian.Recipes.Basic;
using YiJian.Recipes.DoctorsAdvices.Entities;
using YiJian.Recipes.GroupConsultation;
using YiJian.Recipes.InviteDoctor;
using YiJian.Recipes.Preferences.Entities;

namespace YiJian.Recipe
{
    /// <summary>
    /// RecipeApplicationAutoMapperProfile
    /// </summary>
    public class RecipeApplicationAutoMapperProfile : Profile
    {
        /// <summary>
        /// RecipeApplicationAutoMapperProfile
        /// </summary>
        public RecipeApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            //手术申请
            CreateMap<OperationApply, OperationApplyDataList>();
            CreateMap<OperationApply, OperationApplyData>();

            CreateMapFromRecipePackage();
            CreateMap<PackageGroup, PackageGroupData>()
            .ForMember(dest => dest.PackageDatas, opt => opt.Ignore());

            CreateMap<PackageGroup, PackageTreeData>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.IsLeaf, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Disabled, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.UserOrDepartmentId, opt => opt.MapFrom(src => src.UserOrDepartmentId))
                .ForMember(dest => dest.InputCode, opt => opt.Ignore())
                .ForMember(dest => dest.FirstParentId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.Ignore());

            CreateMapFromMedicine();
            CreateMapFromLabProject();
            CreateMapFromExamProject();
            CreateMapFromTreatProject();

            CreateMap<GrpcMedicineModel, RecipeMedicineProp>()
                .ConstructUsing(src => new RecipeMedicineProp(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => src.ExpireDate.ToDateTime()))
                .ForMember(dest => dest.RecipeProject, opt => opt.Ignore());

            CreateMap<GrpcTreatProjectModel, RecipeProject>()
                .ConstructUsing(src => new RecipeProject(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.TreatCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TreatName))
                .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Sort, opt => opt.Ignore())
                .ForMember(dest => dest.ScientificName, opt => opt.Ignore())
                .ForMember(dest => dest.Alias, opt => opt.Ignore())
                .ForMember(dest => dest.AliasPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.AliasWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                .ForMember(dest => dest.CanUseInFirstAid, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.MedicineProp, opt => opt.Ignore())
                .ForMember(dest => dest.ExamProp, opt => opt.Ignore())
                .ForMember(dest => dest.LabProp, opt => opt.Ignore())
                .ForMember(dest => dest.TreatProp, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => src.DictionaryCode))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.DictionaryName));


            CreateMap<GrpcTreatProjectModel, RecipeTreatProp>()
                .ConstructUsing(src => new RecipeTreatProp(Guid.Empty, src.CategoryCode, src.CategoryName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectType, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectName, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeProject, opt => opt.Ignore());

            CreateMap<RecipeProject, RecipeProjectData>()
                .IncludeMembers(src => src.MedicineProp, src => src.ExamProp, src => src.LabProp)
                .ForMember(dest => dest.CatalogCode,
                    opt => opt.MapFrom(src =>
                        src.ExamProp != null ? src.ExamProp.CatalogCode :
                        src.LabProp != null ? src.LabProp.CatalogCode : null))
                .ForMember(dest => dest.CatalogName,
                    opt => opt.MapFrom(src =>
                        src.ExamProp != null ? src.ExamProp.CatalogName :
                        src.LabProp != null ? src.LabProp.CatalogName : null))
                // .ForMember(dest => dest.PositionCode,
                //     opt => opt.MapFrom(src =>
                //         src.ExamProp != null ? src.ExamProp.PositionCode :
                //         src.LabProp != null ? src.LabProp.PositionCode : null))
                // .ForMember(dest => dest.PositionName,
                //     opt => opt.MapFrom(src =>
                //         src.ExamProp != null ? src.ExamProp.PositionName :
                //         src.LabProp != null ? src.LabProp.PositionName : null))
                ;
            CreateMap<RecipeMedicineProp, RecipeProjectData>()
                .IncludeMembers(src => src.RecipeProject)
                .ForMember(dest => dest.BasicUnit, opt => opt.MapFrom(src => src.Unit))
                .ForMember(dest => dest.BasicUnitPrice, opt => opt.MapFrom(src => src.Price));
            CreateMap<RecipeExamProp, RecipeProjectData>()
                .IncludeMembers(src => src.RecipeProject);
            CreateMap<RecipeLabProp, RecipeProjectData>()
                .IncludeMembers(src => src.RecipeProject);
            CreateMap<PackageRecipeSaveAsInput, PackageExamProjectInput>();
            // .ForMember(dest => dest.ApplyTime, opt => opt.Ignore());
            CreateMap<PackageRecipeSaveAsInput, PackageLabProjectInput>()
                .ForMember(dest => dest.SpecimenName, opt => opt.Ignore());
            // .ForMember(dest => dest.ApplyTime, opt => opt.Ignore());
            CreateMap<PackageRecipeSaveAsInput, PackageRecipeInput>()
                .ForMember(dest => dest.AdditionalItemsId, opt => opt.Ignore())
                .ForMember(dest => dest.AdditionalItemsType, opt => opt.Ignore());

            #region 医嘱ETO转换

            CreateMap<DoctorsAdvice, DoctorsAdviceEto>();
            CreateMap<Prescribe, PrescribeEto>();
            CreateMap<Lis, LisEto>();
            CreateMap<Pacs, PacsEto>();
            CreateMap<Treat, TreatEto>();

            #endregion

            CreateMap<DoctorsAdvice, ModifyDoctorsAdviceBaseDto>();
            CreateMap<DoctorsAdvice, DoctorsAdvicePartialDto>()
                .ForMember(dest => dest.UsageCode, opt => opt.Ignore())
                .ForMember(dest => dest.UsageName, opt => opt.Ignore());

            CreateMap<Pacs, PacsDto>()
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.PacsPathologyItemDto, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeCode, opt => opt.Ignore())
                .ForMember(dest => dest.TreatCode, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.PositionCode, opt => opt.Ignore())
                .ForMember(dest => dest.PositionName, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceCode, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceType, opt => opt.Ignore())
                .ForMember(dest => dest.PayTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PayType, opt => opt.Ignore())
                .ForMember(dest => dest.PrescriptionNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeGroupNo, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyTime, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                .ForMember(dest => dest.IsBackTracking, opt => opt.Ignore())
                .ForMember(dest => dest.IsRecipePrinted, opt => opt.Ignore())
                .ForMember(dest => dest.HisOrderNo, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptCode, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptName, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.PacsItems))
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeName, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveQty, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveUnit, opt => opt.Ignore())
                .ForMember(dest => dest.PyCode, opt => opt.Ignore())
                .ForMember(dest => dest.WbCode, opt => opt.Ignore());
            CreateMap<Pacs, PacsListDto>()
                .ForMember(dest => dest.ApplyTime, opt => opt.Ignore());
            CreateMap<Lis, LisDto>()
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeCode, opt => opt.Ignore())
                .ForMember(dest => dest.TreatCode, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.PositionCode, opt => opt.Ignore())
                .ForMember(dest => dest.PositionName, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceCode, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceType, opt => opt.Ignore())
                .ForMember(dest => dest.PayTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PayType, opt => opt.Ignore())
                .ForMember(dest => dest.PrescriptionNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeGroupNo, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyTime, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                .ForMember(dest => dest.IsBackTracking, opt => opt.Ignore())
                .ForMember(dest => dest.IsRecipePrinted, opt => opt.Ignore())
                .ForMember(dest => dest.HisOrderNo, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptCode, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptName, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LisItems))
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeName, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveQty, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveUnit, opt => opt.Ignore())
                .ForMember(dest => dest.PyCode, opt => opt.Ignore())
                .ForMember(dest => dest.WbCode, opt => opt.Ignore());
            CreateMap<Lis, LisListDto>()
                .ForMember(dest => dest.ApplyTime, opt => opt.Ignore());
            CreateMap<Prescribe, PrescribeDto>()
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.PositionCode, opt => opt.Ignore())
                .ForMember(dest => dest.PositionName, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceCode, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceType, opt => opt.Ignore())
                .ForMember(dest => dest.PayTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PayType, opt => opt.Ignore())
                .ForMember(dest => dest.PrescriptionNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeGroupNo, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyTime, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                .ForMember(dest => dest.IsBackTracking, opt => opt.Ignore())
                .ForMember(dest => dest.IsRecipePrinted, opt => opt.Ignore())
                .ForMember(dest => dest.HisOrderNo, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptCode, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptName, opt => opt.Ignore())
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeName, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveQty, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveUnit, opt => opt.Ignore())
                .ForMember(dest => dest.ScientificName, opt => opt.Ignore())
                .ForMember(dest => dest.Alias, opt => opt.Ignore())
                .ForMember(dest => dest.AliasPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.AliasWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.PyCode, opt => opt.Ignore())
                .ForMember(dest => dest.WbCode, opt => opt.Ignore());
            CreateMap<Treat, TreatDto>()
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.PositionCode, opt => opt.Ignore())
                .ForMember(dest => dest.PositionName, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceCode, opt => opt.Ignore())
                .ForMember(dest => dest.InsuranceType, opt => opt.Ignore())
                .ForMember(dest => dest.PayTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PayType, opt => opt.Ignore())
                .ForMember(dest => dest.PrescriptionNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeGroupNo, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyTime, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                .ForMember(dest => dest.IsBackTracking, opt => opt.Ignore())
                .ForMember(dest => dest.IsRecipePrinted, opt => opt.Ignore())
                .ForMember(dest => dest.HisOrderNo, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptCode, opt => opt.Ignore())
                .ForMember(dest => dest.ExecDeptName, opt => opt.Ignore())
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeName, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveQty, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveUnit, opt => opt.Ignore())
                .ForMember(dest => dest.PyCode, opt => opt.Ignore())
                .ForMember(dest => dest.WbCode, opt => opt.Ignore())
                .ForMember(dest => dest.intTreatId, opt => opt.MapFrom(map => map.TreatId));
            CreateMap<LisItem, LisItemDto>();
            CreateMap<PacsItem, PacsItemDto>();

            CreateMap<PrescribeDto, Prescribe>()
                .ForMember(dest => dest.DoctorsAdviceId, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorsAdvice, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.OldDoctorsAdviceId, opt => opt.Ignore());

            CreateMap<LisDto, Lis>()
                .ForMember(dest => dest.DoctorsAdviceId, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorsAdvice, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.LisItems, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.ProjectCode, opt => opt.MapFrom(src => src.Code));

            CreateMap<PacsDto, Pacs>()
                .ForMember(dest => dest.DoctorsAdviceId, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorsAdvice, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
                .ForMember(dest => dest.PacsStatus, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PacsItems, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.ProjectCode, opt => opt.MapFrom(src => src.Code));

            CreateMap<TreatDto, Treat>()
                .ForMember(dest => dest.TreatId, opt => opt.MapFrom(map => map.intTreatId))
                .ForMember(dest => dest.DoctorsAdviceId, opt => opt.Ignore())
                .ForMember(dest => dest.DoctorsAdvice, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore());

            CreateMap<LisItemDto, LisItem>()
                .ForMember(dest => dest.LisId, opt => opt.Ignore())
                .ForMember(dest => dest.Lis, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PacsItemDto, PacsItem>()
                .ForMember(dest => dest.PacsId, opt => opt.Ignore())
                .ForMember(dest => dest.Pacs, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<LisDto, DoctorsAdvicePartial>();
            CreateMap<PacsDto, DoctorsAdvicePartial>();
            CreateMap<PrescribeDto, DoctorsAdvicePartial>();
            CreateMap<TreatDto, DoctorsAdvicePartial>();

            //会诊邀请医生
            CreateMap<InviteDoctor, InviteDoctorData>();
            CreateMap<InviteDoctorUpdate, InviteDoctor>();
            //会诊管理
            CreateMap<Recipes.GroupConsultation.GroupConsultation, GroupConsultationData>()
                .ForMember(i => i.IsApplyDoctor, n => n.Ignore());
            CreateMap<SettingPara, SettingParaData>();

            //会诊纪要医生
            CreateMap<DoctorSummary, DoctorSummaryData>();
            CreateMap<DoctorSummaryUpdate, DoctorSummary>();

            //GRPC 实体到 快速开嘱药品映射
            CreateMap<GrpcMedicineModel, QuickStartMedicine>().ConstructUsing(src => new QuickStartMedicine(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MedicineCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Qty, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => src.ExpireDate.ToDateTime()))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(dest => dest.RecieveQty, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveUnit, opt => opt.Ignore())
                .ForMember(dest => dest.LongDays, opt => opt.Ignore())
                .ForMember(dest => dest.ActualDays, opt => opt.Ignore())
                .ForMember(dest => dest.DefaultDosageQty, opt => opt.Ignore())
                .ForMember(dest => dest.QtyPerTimes, opt => opt.Ignore())
                .ForMember(dest => dest.DefaultDosageUnit, opt => opt.Ignore())
                .ForMember(dest => dest.SkinTestResult, opt => opt.Ignore())
                .ForMember(dest => dest.MaterialPrice, opt => opt.Ignore())
                .ForMember(dest => dest.IsBindingTreat, opt => opt.Ignore())
                .ForMember(dest => dest.IsAmendedMark, opt => opt.Ignore())
                .ForMember(dest => dest.IsAdaptationDisease, opt => opt.Ignore())
                .ForMember(dest => dest.Speed, opt => opt.Ignore())
                .ForMember(dest => dest.IsBackTracking, opt => opt.MapFrom(src => false)) //是否补录（设置一个默认false）
                .ForMember(dest => dest.PrescriptionNo, opt => opt.MapFrom(src => "")) //目前字典那边没有处方号
                .ForMember(dest => dest.ApplyTime, opt => opt.Ignore()) //默认留空即可
                .ForMember(dest => dest.ApplyDoctorCode, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyDoctorName, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyDeptCode, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyDeptName, opt => opt.Ignore())
                .ForMember(dest => dest.TraineeCode, opt => opt.Ignore())
                .ForMember(dest => dest.TraineeName, opt => opt.Ignore())
                .ForMember(dest => dest.ExecutorCode, opt => opt.Ignore())
                .ForMember(dest => dest.ExecutorName, opt => opt.Ignore())
                .ForMember(dest => dest.StopDoctorCode, opt => opt.Ignore())
                .ForMember(dest => dest.StopDoctorName, opt => opt.Ignore())
                .ForMember(dest => dest.StopDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ERecipeStatus.Saved))
                .ForMember(dest => dest.PayTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PayType, opt => opt.Ignore())
                .ForMember(dest => dest.QuickStartAdviceId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
                .ForMember(dest => dest.SkinTestSignChoseResult, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.RestrictedDrugs, opt => opt.Ignore())
                .ForMember(dest => dest.IsCriticalPrescription, opt => opt.Ignore())
                .ForMember(dest => dest.HisUnit, opt => opt.MapFrom(src => src.Unit))
                .ForMember(dest => dest.HisDosageUnit, opt => opt.MapFrom(src => src.DosageUnit))
                .ForMember(dest => dest.HisDosageQty, opt => opt.MapFrom(src => src.DosageQty))
                .ForMember(dest => dest.CommitHisDosageQty, opt => opt.Ignore()); //临时注释，可能还是需要同步字典那边的数据的

            CreateMap<QuickStartMedicine, QuickStartMedicineDto>();
            CreateMap<QuickStartMedicine, QuickStartSampleDto>();
            CreateMap<ALLHistoryDoctorsAdvices, PrescribeInfoDto>();

            //CreateMap<QueryPacsReportResponse, EmrPacsReportResponse>();
            CreateMap<NovelCoronavirusRna, NovelCoronavirusRnaDto>()
                .ForMember(dest => dest.ApplyTime,
                    opt => opt.MapFrom(mf => mf.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss")));
            CreateMap<DrugStockQueryResponse, DrugStockQuery>()
                .ForMember(dest => dest.DoctorsAdviceId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            //CreateMap<Prescribe, PrescribeInfoDto>()
            //    .ForMember(dest => dest.Code, opt => opt.Ignore())
            //    .ForMember(dest => dest.Name, opt => opt.Ignore())
            //    .ForMember(dest => dest.RecieveQty, opt => opt.Ignore());

            CreateMap<OwnMedicine, OwnMedicineDto>();
            CreateMap<OwnMedicineDto, OwnMedicine>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PlatformType, opt => opt.Ignore())
                .ForMember(dest => dest.PIID, opt => opt.Ignore())
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.PatientName, opt => opt.Ignore())
                .ForMember(dest => dest.IsPush, opt => opt.Ignore());
            CreateMap<OwnMedicine, OwnMedicineEto>();

            CreateMap<UserSettingDto, UserSetting>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore());

            CreateMap<UserSetting, UserSettingDto>();

            CreateMap<DdpDrugStockQueryResponse, DrugStockQueryResponse>();
            CreateMap<PacsItem, PacsItem4HisDto>();
            CreateMap<LisItem, LisItem4HisDto>();

            CreateMap<AdmissionRecordDto, PatientInfoEto>();
            CreateMap<PrescribeCustomRule, CustomDosageDto>();

            this.CreateGrpcMaps();
        }

        private void CreateMapFromRecipePackage()
        {
            CreateMap<Package, PackageData>()
                .ForMember(dest => dest.CreationTime,
                    opt => opt.MapFrom(src => src.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime,
                    opt => opt.MapFrom(src =>
                        (src.LastModificationTime ?? src.CreationTime).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.Recipes,
                    opt => opt.MapFrom(src => src.Projects.Where(x =>
                        x.Project.CategoryCode != "Lab" && x.Project.CategoryCode != "Exam")))

                 .ForMember(dest => dest.Medicines,
                    opt => opt.MapFrom(src => src.Projects.Where(x =>
                        x.Project.CategoryCode != "Lab" && x.Project.CategoryCode != "Exam")))
                  .ForMember(dest => dest.Treats,
                    opt => opt.MapFrom(src => src.Projects.Where(x =>
                        x.Project.CategoryCode != "Lab" && x.Project.CategoryCode != "Exam")))

                .ForMember(dest => dest.LabProjects,
                    opt => opt.MapFrom(src => src.Projects.Where(x => x.Project.CategoryCode == "Lab")))
                .ForMember(dest => dest.ExamProjects,
                    opt => opt.MapFrom(src => src.Projects.Where(x => x.Project.CategoryCode == "Exam")));

            // 药品、诊疗映射
            CreateMap<PackageProject, PackageRecipeData>()
                .IncludeMembers(src => src.Project)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Project.SourceId))
                .ForMember(dest => dest.RecieveQty, opt => opt.MapFrom(src => src.RecieveQty))
                .ForMember(dest => dest.IsCriticalPrescription, opt => opt.MapFrom(src => src.IsCriticalPrescription))
                // TODO: 计算总金额
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeCode, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeName, opt => opt.Ignore());
            CreateMap<RecipeProject, PackageRecipeData>()
                .IncludeMembers(src => src.MedicineProp)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.DosageForm, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EntryId, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeGroupNo, opt => opt.Ignore())
                .ForMember(dest => dest.FrequencyTimes, opt => opt.Ignore())
                .ForMember(dest => dest.FrequencyUnit, opt => opt.Ignore())
                .ForMember(dest => dest.FrequencyExecDayTimes, opt => opt.Ignore())
                .ForMember(dest => dest.Qty, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveQty, opt => opt.Ignore())
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForMember(dest => dest.LongDays, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveUnit, opt => opt.Ignore())
                .ForMember(dest => dest.PackageId, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.IsCriticalPrescription, opt => opt.Ignore());
            CreateMap<RecipeMedicineProp, PackageRecipeData>()
                .IncludeMembers(src => src.RecipeProject)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecipeProject.SourceId))
                .ForMember(dest => dest.DefaultDosageQty, opt => opt.MapFrom(src => src.DosageQty))
                .ForMember(dest => dest.DosageForm, opt => opt.Ignore())
                .ForMember(dest => dest.DefaultDosageUnit, opt => opt.MapFrom(src => src.DosageUnit))
                .ForMember(dest => dest.EntryId, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeNo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeGroupNo, opt => opt.Ignore())
                .ForMember(dest => dest.FrequencyTimes, opt => opt.Ignore())
                .ForMember(dest => dest.FrequencyUnit, opt => opt.Ignore())
                .ForMember(dest => dest.FrequencyExecDayTimes, opt => opt.Ignore())
                .ForMember(dest => dest.Qty, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveQty, opt => opt.Ignore())
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForMember(dest => dest.LongDays, opt => opt.Ignore())
                .ForMember(dest => dest.RecieveUnit, opt => opt.Ignore())
                .ForMember(dest => dest.PackageId, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                .ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.IsCriticalPrescription, opt => opt.Ignore());

            // 检验项目映射
            CreateMap<PackageProject, PackageLabProjectData>()
                .IncludeMembers(src => src.Project)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Project.SourceId))
                .ForMember(dest => dest.PartCode, opt => opt.MapFrom(src => src.PartCode))
                .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.PartName))
                .ForMember(dest => dest.PositionCode, opt => opt.MapFrom(src => src.PositionCode))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.PositionName))
                .ForMember(dest => dest.ExecDeptCode, opt => opt.MapFrom(src => src.ExecDeptCode))
                .ForMember(dest => dest.ExecDeptName, opt => opt.MapFrom(src => src.ExecDeptName))
                .ForMember(dest => dest.ContainerCode, opt => opt.MapFrom(src => src.ContainerCode))
                .ForMember(dest => dest.ContainerName, opt => opt.MapFrom(src => src.ContainerName))
                .ForMember(dest => dest.SpecimenCode, opt => opt.MapFrom(src => src.SpecimenCode))
                .ForMember(dest => dest.SpecimenName, opt => opt.MapFrom(src => src.SpecimenName))
                .ForMember(dest => dest.ChargeCode, opt => opt.MapFrom(src => src.ChargeCode))
                .ForMember(dest => dest.ChargeName, opt => opt.MapFrom(src => src.ChargeName));
            CreateMap<RecipeProject, PackageLabProjectData>()
                .IncludeMembers(src => src.LabProp)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RecieveUnit, opt => opt.MapFrom(src => src.Unit))
                .ForMember(dest => dest.EntryId, opt => opt.Ignore())
                .ForMember(dest => dest.PackageId, opt => opt.Ignore());
            CreateMap<RecipeLabProp, PackageLabProjectData>()
                .IncludeMembers(src => src.RecipeProject)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecipeProject.SourceId))
                .ForMember(dest => dest.RecieveUnit, opt => opt.MapFrom(src => src.RecipeProject.Unit))
                .ForMember(dest => dest.EntryId, opt => opt.Ignore())
                .ForMember(dest => dest.PackageId, opt => opt.Ignore());

            // 检查项目映射
            CreateMap<PackageProject, PackageExamProjectData>()
                .IncludeMembers(src => src.Project)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Project.SourceId))
                .ForMember(dest => dest.PartCode, opt => opt.MapFrom(src => src.PartCode))
                .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.PartName))
                .ForMember(dest => dest.PositionCode, opt => opt.MapFrom(src => src.PositionCode))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.PositionName))
                .ForMember(dest => dest.ExecDeptCode, opt => opt.MapFrom(src => src.ExecDeptCode))
                .ForMember(dest => dest.ExecDeptName, opt => opt.MapFrom(src => src.ExecDeptName))
                .ForMember(dest => dest.ChargeCode, opt => opt.MapFrom(src => src.ChargeCode))
                .ForMember(dest => dest.ChargeName, opt => opt.MapFrom(src => src.ChargeName));
            CreateMap<RecipeProject, PackageExamProjectData>()
                .IncludeMembers(src => src.ExamProp)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EntryId, opt => opt.Ignore())
                .ForMember(dest => dest.PackageId, opt => opt.Ignore());
            CreateMap<RecipeExamProp, PackageExamProjectData>()
                .IncludeMembers(src => src.RecipeProject)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecipeProject.SourceId))
                .ForMember(dest => dest.RecieveUnit, opt => opt.MapFrom(src => src.RecipeProject.Unit))
                .ForMember(dest => dest.EntryId, opt => opt.Ignore())
                .ForMember(dest => dest.PackageId, opt => opt.Ignore());

            CreateMap<Package, PackageTreeData>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.IsLeaf, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Disabled, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.UserOrDepartmentId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstParentId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.Ignore());
            CreateMap<MedicineAdviceDto, GroupMedicineDto>()
                .ForMember(dest => dest.EntryId, opt => opt.Ignore())
                .ForMember(dest => dest.MedicineCount, opt => opt.Ignore())
                .ForMember(dest => dest.GroupAmount, opt => opt.Ignore());
        }

        private void CreateMapFromMedicine()
        {
            AllowNullDestinationValues = false;
            CreateMap<GrpcMedicineModel, RecipeMedicineProp>()
                .ConstructUsing(src => new RecipeMedicineProp(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => src.ExpireDate.ToDateTime()))
                .ForMember(dest => dest.RecipeProject, opt => opt.Ignore());
            CreateMap<GrpcMedicineModel, RecipeProject>()
                .ConstructUsing(src => new RecipeProject(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => "Medicine"))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => "药物"))
                .ForMember(dest => dest.CategoryPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.Sort, opt => opt.Ignore())
                .ForMember(dest => dest.OtherPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Additional, opt => opt.Ignore())
                .ForMember(dest => dest.CanUseInFirstAid, opt => opt.MapFrom(src => src.IsFirstAid))
                //.ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                //.ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.MedicineProp, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ExamProp, opt => opt.Ignore())
                .ForMember(dest => dest.LabProp, opt => opt.Ignore())
                .ForMember(dest => dest.TreatProp, opt => opt.Ignore());

            AllowNullDestinationValues = true;
        }

        private void CreateMapFromLabProject()
        {
            AllowNullDestinationValues = false;
            CreateMap<GrpcLabProjectModel, RecipeLabProp>()
                .ConstructUsing(src => new RecipeLabProp(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PartCode, opt => opt.Ignore()) // TODO: 检验部位
                .ForMember(dest => dest.PartName, opt => opt.Ignore()) // TODO: 检验部位
                .ForMember(dest => dest.RecipeProject, opt => opt.Ignore())
                .ForMember(dest => dest.GuideCatelogName, opt => opt.Ignore())
                ;
            CreateMap<GrpcLabProjectModel, RecipeProject>()
                .ConstructUsing(src => new RecipeProject(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.ProjectCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.Specification, opt => opt.Ignore())
                .ForMember(dest => dest.ScientificName, opt => opt.Ignore())
                .ForMember(dest => dest.Alias, opt => opt.Ignore())
                .ForMember(dest => dest.AliasPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.AliasWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => "Lab"))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => "检验"))
                .ForMember(dest => dest.CategoryPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.CanUseInFirstAid, opt => opt.Ignore())
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                .ForMember(dest => dest.Additional, opt => opt.Ignore())
                //.ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                //.ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.MedicineProp, opt => opt.Ignore())
                .ForMember(dest => dest.ExamProp, opt => opt.Ignore())
                .ForMember(dest => dest.LabProp, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TreatProp, opt => opt.Ignore());
            AllowNullDestinationValues = true;
        }

        private void CreateMapFromExamProject()
        {
            AllowNullDestinationValues = false;
            CreateMap<GrpcExamProjectModel, RecipeExamProp>()
                .ConstructUsing(opt => new RecipeExamProp(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeProject, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ExamTitle, opt => opt.Ignore())
            .ForMember(dest => dest.ReservationPlace, opt => opt.Ignore())
            .ForMember(dest => dest.TemplateId, opt => opt.Ignore());


            CreateMap<GrpcExamProjectModel, RecipeProject>()
                .ConstructUsing(opt => new RecipeProject(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.ProjectCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.Specification, opt => opt.Ignore())
                .ForMember(dest => dest.ScientificName, opt => opt.Ignore())
                .ForMember(dest => dest.Alias, opt => opt.Ignore())
                .ForMember(dest => dest.AliasPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.AliasWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OtherPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Additional, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => "Exam"))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => "检查"))
                .ForMember(dest => dest.CategoryPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.CanUseInFirstAid, opt => opt.Ignore())
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                //.ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                //.ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.MedicineProp, opt => opt.Ignore())
                .ForMember(dest => dest.ExamProp, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.LabProp, opt => opt.Ignore())
                .ForMember(dest => dest.TreatProp, opt => opt.Ignore());
            AllowNullDestinationValues = true;
        }

        private void CreateMapFromTreatProject()
        {
            AllowNullDestinationValues = false;
            CreateMap<GrpcTreatProjectModel, RecipeProject>()
                .ConstructUsing(opt => new RecipeProject(Guid.Empty))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.TreatCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TreatName))
                .ForMember(dest => dest.ScientificName, opt => opt.Ignore())
                .ForMember(dest => dest.Alias, opt => opt.Ignore())
                .ForMember(dest => dest.AliasPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.AliasWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryPyCode, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryWbCode, opt => opt.Ignore())
                .ForMember(dest => dest.CanUseInFirstAid, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Sort, opt => opt.Ignore())
                .ForMember(dest => dest.Remarks, opt => opt.Ignore())
                //.ForMember(dest => dest.PrescribeTypeCode, opt => opt.Ignore())
                //.ForMember(dest => dest.PrescribeTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.MedicineProp, opt => opt.Ignore())
                .ForMember(dest => dest.ExamProp, opt => opt.Ignore())
                .ForMember(dest => dest.LabProp, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => src.DictionaryCode))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.DictionaryName));
            AllowNullDestinationValues = true;
        }

        /// <summary>
        /// CreateGrpcMaps
        /// </summary>
        public void CreateGrpcMaps()
        {
            AllowNullDestinationValues = false;

            CreateMap<OperationApply, GrpcOperApplyModel>()
                .ForMember(dest => dest.ApplyTime,
                    opt =>
                    {
                        opt.MapFrom(src =>
                            Timestamp.FromDateTime((src.ApplyTime ?? DateTime.MinValue).ToUniversalTime()));
                    })
                .ForMember(dest => dest.OperationCode, opt => opt.MapFrom(src => src.ProposedOperationCode))
                .ForMember(dest => dest.OperationName, opt => opt.MapFrom(src => src.ProposedOperationName));
        }
    }
}