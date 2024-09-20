using AutoMapper;
using Volo.Abp.AutoMapper;

namespace YiJian.Handover
{
    using YiJian.Handover;
    public class HandoverApplicationAutoMapperProfile : Profile
    {
        public HandoverApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<ShiftHandoverSetting, ShiftHandoverSettingData>();
            CreateMap<ShiftHandoverSetting, ShiftHandoverSettingDataList>();
            CreateMap<DoctorPatients, DoctorPatientsData>();
            CreateMap<DoctorPatientStatistic, DoctorPatientStatisticData>();
            CreateMap<DoctorHandover, DoctorHandoverData>();
            CreateMap<DoctorPatientsData, HandoverHistoryPatientsDataList>().Ignore(i => i.HandoverDoctorName);
            

            //交班日期
            CreateMap<NurseHandover, NurseHandoverData>();
        }
    }
}
