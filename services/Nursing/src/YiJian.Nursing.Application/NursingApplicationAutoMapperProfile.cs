using AutoMapper;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.Nursing.Recipes.Entities;
using YiJian.Nursing.Settings;

namespace YiJian.Nursing
{
    /// <summary>
    /// 映射类
    /// </summary>
    public class NursingApplicationAutoMapperProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public NursingApplicationAutoMapperProfile()
        {
            ExecutingBiz();

            NursingBiz();
        }

        /// <summary>
        /// 医嘱执行
        /// </summary>
        private void ExecutingBiz()
        {
            //开嘱==>医嘱主表
            CreateMap<DoctorsAdviceEto, Recipe>()

            #region 枚举转换

                .ForMember(d => d.PlatformType, o => o.MapFrom(s => (EPlatformType)s.PlatformType))
                .ForMember(d => d.ItemType, o => o.MapFrom(s => (ERecipeItemType)s.ItemType))
                .ForMember(d => d.InsuranceType, o => o.MapFrom(s => (InsuranceCatalog)(s.InsuranceType ?? 0)))
                .ForMember(d => d.PayType, o => o.MapFrom(s => (ERecipePayType)(s.PayType ?? 0)))
                .ForMember(d => d.Status, o => o.MapFrom(s => (EDoctorsAdviceStatus)s.Status))
                .ForMember(d => d.IsBackTracking, o => o.MapFrom(s => s.IsBackTracking ?? false))

            #endregion 枚举转换

            #region 打印信息

                .ForMember(d => d.PrintedTimes, o => o.Ignore()) //打印信息

            #endregion 打印信息

            #region 停嘱信息

                .ForMember(d => d.StopDoctorCode, o => o.Ignore()) //停嘱信息
                .ForMember(d => d.StopDoctorName, o => o.Ignore())
                .ForMember(d => d.StopTime, o => o.Ignore())
                .ForMember(d => d.StopOptTime, o => o.Ignore())

            #endregion 停嘱信息

            #region 审计信息

                .ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore())
                .ForMember(i => i.ExtraProperties, n => n.Ignore())
                .ForMember(i => i.ConcurrencyStamp, n => n.Ignore());

            #endregion 审计信息

            //开嘱==>药物处方表
            CreateMap<PrescribeEto, Prescribe>()
                .ForMember(d => d.RecipeId, o => o.MapFrom(s => s.DoctorsAdviceId))
                .ForMember(d => d.DosageQty, o => o.MapFrom(s => s.DosageQty ?? 0))
                .ForMember(d => d.LongDays, o => o.MapFrom(s => s.LongDays ?? 0))

            #region 皮试结果

                .ForMember(d => d.SkinTestResult, o => o.Ignore()) //皮试结果

            #endregion 皮试结果

            #region 执行统计

                //.ForMember(d => d.StartTime, o => o.Ignore()) //执行统计
                //.ForMember(d => d.EndTime, o => o.Ignore())
                .ForMember(d => d.ActualDays, o => o.Ignore())

            #endregion 执行统计

            #region 审计信息

                .ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore())
                .ForMember(i => i.ExtraProperties, n => n.Ignore())
                .ForMember(i => i.ConcurrencyStamp, n => n.Ignore());

            #endregion 审计信息

            //开嘱==>检查表
            CreateMap<PacsEto, Pacs>()
                .ForMember(d => d.RecipeId, o => o.MapFrom(s => s.DoctorsAdviceId))

            #region 报告信息

                .ForMember(d => d.HasReport, o => o.Ignore()) //报告信息
                .ForMember(d => d.ReportTime, o => o.Ignore())
                .ForMember(d => d.ReportDoctorCode, o => o.Ignore())
                .ForMember(d => d.ReportDoctorName, o => o.Ignore())

            #endregion 报告信息

            #region 审计信息

                .ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore())
                .ForMember(i => i.ExtraProperties, n => n.Ignore())
                .ForMember(i => i.ConcurrencyStamp, n => n.Ignore());

            #endregion 审计信息


            //开嘱==>检验表
            CreateMap<LisEto, Lis>()
                .ForMember(d => d.RecipeId, o => o.MapFrom(s => s.DoctorsAdviceId))

            #region 报告信息

                .ForMember(d => d.HasReport, o => o.Ignore()) //报告信息
                .ForMember(d => d.ReportTime, o => o.Ignore())
                .ForMember(d => d.ReportDoctorCode, o => o.Ignore())
                .ForMember(d => d.ReportDoctorName, o => o.Ignore())

            #endregion 报告信息

            #region 标本信息

                .ForMember(d => d.SpecimenCollectDatetime, o => o.Ignore()) //标本信息
                .ForMember(d => d.SpecimenReceivedDatetime, o => o.Ignore())
                .ForMember(d => d.ContainerCode, o => o.Ignore())
                .ForMember(d => d.ContainerName, o => o.Ignore())
                .ForMember(d => d.ContainerColor, o => o.Ignore())
                .ForMember(d => d.SpecimenDescription, o => o.Ignore())

            #endregion 标本信息

            #region 审计信息

                .ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore())
                .ForMember(i => i.ExtraProperties, n => n.Ignore())
                .ForMember(i => i.ConcurrencyStamp, n => n.Ignore());

            #endregion 审计信息

            //开嘱==>诊疗表
            CreateMap<TreatEto, Treat>()
                .ForMember(d => d.RecipeId, o => o.MapFrom(s => s.DoctorsAdviceId))

            #region 审计信息

                .ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore())
                .ForMember(i => i.ExtraProperties, n => n.Ignore())
                .ForMember(i => i.ConcurrencyStamp, n => n.Ignore());

            #endregion 审计信息
        }

        /// <summary>
        /// 护理/导管
        /// </summary>
        private void NursingBiz()
        {
            //表:模块参数
            CreateMap<ParaModule, ParaModuleData>();

            //表:护理项目表
            CreateMap<ParaItem, ParaItemData>();
            CreateMap<ParaItemData, ParaItem>()
                .ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore());

            //表:导管字典-通用业务
            CreateMap<Dict, DictData>();
            CreateMap<DictData, Dict>();
            CreateMap<DictUpdate, Dict>();


            //表:人体图-编号字典
            CreateMap<CanulaPart, CanulaPartData>();
            CreateMap<CanulaPartData, CanulaPart>()
                .ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore());
            CreateMap<ParaItemUpdate, ParaItem>().ForMember(i => i.IsDeleted, n => n.Ignore())
                .ForMember(i => i.DeleterId, n => n.Ignore())
                .ForMember(i => i.DeletionTime, n => n.Ignore())
                .ForMember(i => i.LastModificationTime, n => n.Ignore())
                .ForMember(i => i.LastModifierId, n => n.Ignore())
                .ForMember(i => i.CreationTime, n => n.Ignore())
                .ForMember(i => i.CreatorId, n => n.Ignore());
        }
    }
}