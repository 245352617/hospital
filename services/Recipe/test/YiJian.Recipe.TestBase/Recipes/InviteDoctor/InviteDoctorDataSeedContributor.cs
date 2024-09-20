namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 会诊邀请医生 DataSeedContributor
    /// </summary>
    public class InviteDoctorDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _inviteDoctorRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 
            Guid id = default;
            // 会诊id
            Guid groupConsultationId = default;
            // 医生编码
            string code = "";
            // 医生名称
            string name = "";
            // 科室编码
            string deptCode = "";
            // 科室名称
            string deptName = "";
            // 状态，0：已邀请，1：已报到
            CheckInStatus checkInStatus = default;
            // 报道时间
            DateTime? checkInTime = null;
            // 意见
            string opinion = "";

            var inviteDoctor = new InviteDoctor(_guidGenerator.Create(), 
                groupConsultationId,// 会诊id
                code,           // 医生编码
                name,           // 医生名称
                deptCode,       // 科室编码
                deptName,       // 科室名称
                checkInStatus,  // 状态，0：已邀请，1：已报到
                checkInTime,    // 报道时间
                opinion         // 意见
                );

			await _inviteDoctorRepository.InsertAsync(inviteDoctor); 
            */
        }

        #region constructor
        public InviteDoctorDataSeedContributor(
            IInviteDoctorRepository inviteDoctorRepository, 
            IGuidGenerator guidGenerator)
        {
            _inviteDoctorRepository = inviteDoctorRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IInviteDoctorRepository _inviteDoctorRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}
