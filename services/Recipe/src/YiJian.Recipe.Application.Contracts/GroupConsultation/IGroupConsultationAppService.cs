using System.Collections.Generic;
using YiJian.Recipes.InviteDoctor;

namespace YiJian.Recipes.GroupConsultation
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;
    using YiJian.Recipe;

    /// <summary>
    /// 会诊管理 API Interface
    /// </summary>   
    public interface IGroupConsultationAppService : IApplicationService
    {
        Task<Guid> CreateAsync(GroupConsultationUpdate input);
        Task DoctorCheckInAsync(DateTime? checkInTime);
        Task<Guid> InvitationAsync(InviteDoctorUpdate input);
        Task<List<InviteDoctorData>> GetJoinAsync();
        Task OpinionAsync(string opinion);
        Task OpinionAsync(Guid inviteId, string opinion);
        Task FinishAsync(Guid groupConsultationId);
        Task DoctorLeaveAsync();
        Task<GroupConsultationData> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<ListResultDto<GroupConsultationData>> GetListAsync(string pIId, string typeCode, GroupConsultationStatus status);
        Task<PagedResultDto<GroupConsultationData>> GetPageListAsync(GetGroupConsultationPagedInput input);
        Task<List<DoctorSummaryData>> SyncOpinionAsync(Guid id);
        Task<ListResultDto<GroupConsultationData>> GetListByEcisAsync(string pIId);
        Task<bool> SummaryAsync(Guid groupConsultationId, string summary);

        Task<GroupConsultationData> GetNewestAsync(string applyCode, string code,
            GroupConsultationStatus status);
    }
}