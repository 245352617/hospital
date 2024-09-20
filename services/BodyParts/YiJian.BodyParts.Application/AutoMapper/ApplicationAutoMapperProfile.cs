using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.Model;
using AutoMapper;
using System;
using YiJian.BodyParts.Application.Contracts.Dtos.CanulaSkin;
using YiJian.BodyParts.Application.Contracts.Dtos.Nursing;
using YiJian.BodyParts.Application.Contracts.Dtos.System.SysNotice;
using YiJian.BodyParts.Application.Contracts.Dtos.Dictionary;

namespace YiJian.BodyParts
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here. 您可以在这里配置AutoMapper映射配置。
             * Alternatively, you can split your mapping configurations 或者，可以拆分映射配置
             * into multiple profile classes for a better organization. 进入多个配置文件类以便更好地组织。 */

            //模板字典
            CreateMap<IcuParaModule, IcuParaModuleDto>().ReverseMap();
            CreateMap<CreateUpdateIcuParaModuleDto, IcuParaModule>(MemberList.None);

            //参数字典
            CreateMap<IcuParaItem, IcuParaItemDto>().ReverseMap();
            CreateMap<CreateUpdateIcuParaItemDto, IcuParaItem>(MemberList.None);

            //参数下拉框字典
            CreateMap<Dict, DictDto>().ReverseMap();
            CreateMap<CreateUpdateDictDto, Dict>(MemberList.None);

            //患者基本设置
            CreateMap<IcuDeptSchedule, IcuDeptScheduleDto>();
        
            //基础信息
            CreateMap<DictSource, DictSourceDto>().ReverseMap();
            CreateMap<DictSource, DictCaseDto>().ReverseMap();

            //皮肤管理
            CreateMap<IcuNursingSkin, IcuNursingSkinDto>(MemberList.None);
            CreateMap<CreateUpdateIcuNursingSkinDto, IcuNursingSkin>(MemberList.None);
            CreateMap<IcuSkin, IcuSkinDto>();
            CreateMap<CreateUpdateIcuSkinDto, IcuSkin>(MemberList.None).ReverseMap();
            CreateMap<DictCanulaPartDto, DictCanulaPart>(MemberList.None).ReverseMap();

            //护理事件
            CreateMap<IcuNursingEvent, IcuNursingEventDto>()
                .ForMember(dest => dest.RecordTime, options =>
                {
                    options.MapFrom(src => src.NurseTime);
                });
            CreateMap<CreateUpdateIcuNursingEventDto, IcuNursingEvent>(MemberList.None)
                .ForMember(dest => dest.NurseTime, options => { options.MapFrom(src => Convert.ToDateTime(src.RecordTime.ToString("yyyy-MM-dd HH:mm"))); })
                .ForMember(dest => dest.NurseDate, options => { options.MapFrom(src => src.RecordTime.Date); });

            // 科室班次配置
            CreateMap<DeptScheduleConfigDto, IcuDeptSchedule>(MemberList.None);
            CreateMap<IcuDeptSchedule, DeptScheduleConfigDto>(MemberList.None);
         
            CreateMap<OrderDto, HisOrderDto>(MemberList.None);

            //皮肤
            CreateMap<IcuParaModule, IcuSkinParaModuleDto>(MemberList.None);
            CreateMap<Dict, Dtos.Dicts>(MemberList.None);

            CreateMap<FileRecord, RulesRegulationsFileListDto>(MemberList.None);
            CreateMap<RulesRegulationsFileListDto, FileRecord>(MemberList.None);

            //护理模板字典
            CreateMap<IcuPhrase, IcuPhraseDto>();
            CreateMap<IcuPhrase, IcuPhraseReplenishDto>().ReverseMap();
            CreateMap<CreateUpdateIcuPhraseDto, IcuPhrase>(MemberList.None);
        }
    }
}