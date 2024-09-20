using AutoMapper;
using SamJan.MicroService.PreHospital.TriageService;
using YiJian.ECIS.Call.CallCenter.Dtos;
using YiJian.ECIS.Call.CallConfig;
using YiJian.ECIS.Call.CallConfig.Dtos;
using YiJian.ECIS.Call.Domain;
using YiJian.ECIS.Call.Domain.CallCenter;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.ShareModel.IMServiceEtos.Call;

namespace YiJian.ECIS.Call
{
    public class CallApplicationAutoMapperProfile : Profile
    {
        public CallApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            /* 科室诊室 */
            CreateMap<Department, DepartmentData>();

            // 诊室固定、医生变动
            CreateMap<ConsultingRoomRegular, ConsultingRoomRegularData>()
                .ForMember(dest => dest.IP, opt => opt.Ignore())
                .ForMember(dest => dest.ConsultingRoomName, opt => opt.Ignore())
                .ForMember(dest => dest.ConsultingRoomCode, opt => opt.Ignore())
                .ForMember(dest => dest.DepartmentCode, opt => opt.Ignore())
                .ForMember(dest => dest.DepartmentName, opt => opt.Ignore());
            CreateMap<DoctorRegular, DoctorRegularData>()
                .ForMember(dest => dest.DepartmentName, opt => opt.Ignore());
            // 基础设置
            CreateMap<BaseConfig, BaseConfigData>()
                .ForMember(dest => dest.TomorrowUpdateTime, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUpdateTime, opt => opt.Ignore())
                .ForMember(dest => dest.NextUpdateTime, opt => opt.Ignore());

            // 预检分诊消息体 映射 挂号信息
            CreateMap<CallInfo, CallInfoData>();
            CreateMap<CallInfo, CallInfoData2>();

            // 预检分诊消息体 映射 挂号信息
            CreateMap<CallInfo, CallClientData>();
            CreateMap<CallingRecord, CallingRecordData>()
                .ForMember(dest => dest.CallingSn, opt => opt.MapFrom(src => src.CallInfo.CallingSn))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.CallInfo.PatientID))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.CallInfo.PatientName))
                .ForMember(dest => dest.CallingTime, opt => opt.MapFrom(src => src.CreationTime));// 列表配置
            CreateMap<RowConfig, RowConfigDto>();
            CreateMap<CallInfo, CallingEto>()
                .ForMember(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.TriageDept))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.TriageDeptName));

            //CreateMap<CallInfo, InQueueCallInfoEto>().ForMember(c => c.TriageDeptCode, opt => opt.MapFrom(src => src.TriageDept));
            CreateMap<InQueueCallInfoEto, CallInfo>().ForMember(c => c.TriageDept, opt => opt.MapFrom(src => src.TriageDeptCode));
            CreateMap<CallReferralDto, InQueueCallInfoEto>()
                .ForMember(c => c.DoctorCode, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(c => c.TriageDeptCode, opt => opt.MapFrom(src => src.DeptCode))
                .ForMember(c => c.TriageDeptName, opt => opt.MapFrom(src => src.DeptName))
                .ForMember(c => c.ActTriageLevel, opt => opt.MapFrom(src => src.TriageLevel))
                .ForMember(c => c.ActTriageLevelName, opt => opt.MapFrom(src => src.TriageLevelName))
                .ForMember(c => c.DoctorCode, opt => opt.MapFrom(src => src.DoctorId));

        }
    }
}