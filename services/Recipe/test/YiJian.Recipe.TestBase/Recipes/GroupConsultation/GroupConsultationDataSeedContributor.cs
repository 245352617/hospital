namespace YiJian.Recipes.GroupConsultation
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 会诊管理 DataSeedContributor
    /// </summary>
    public class GroupConsultationDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _groupConsultationRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 
            Guid id = default;
            // 分诊患者id
            Guid pIID = default;
            // 患者id
            string patientId = "";
            // 会诊类型
            string typeCode = "";
            // 
            string typeName = "";
            // 会诊开始时间
            DateTime startTime = DateTime.Now;
            // 会诊状态
            GroupConsultationStatus status = default;
            // 会诊目的编码
            string objectiveCode = "";
            // 会诊目的内容
            string objectiveContent = "";
            // 申请科室编码
            string applyDeptCode = "";
            // 申请科室名称
            string applyDeptName = "";
            // 申请人编码
            string applyCode = "";
            // 申请人名称
            string applyName = "";
            // 联系方式
            string mobile = "";
            // 申请时间
            DateTime? applyTime = null;
            // 地点
            string place = "";
            // 生命体征
            string vitalSigns = "";
            // 检验
            string test = "";
            // 检查
            string inspect = "";
            // 医嘱
            string doctorOrder = "";
            // 诊断
            string diagnose = "";
            // 病历摘要
            string content = "";
            // 总结
            string summary = "";
            // 会诊邀请医生
            List<InviteDoctor.InviteDoctor> inviteDoctors = default;

            var groupConsultation = new GroupConsultation(_guidGenerator.Create(), 
                pIID,   // 分诊患者id
                patientId,      // 患者id
                typeCode,       // 会诊类型
                typeName,
                startTime,      // 会诊开始时间
                status,         // 会诊状态
                objectiveCode,  // 会诊目的编码
                objectiveContent,// 会诊目的内容
                applyDeptCode,  // 申请科室编码
                applyDeptName,  // 申请科室名称
                applyCode,      // 申请人编码
                applyName,      // 申请人名称
                mobile,         // 联系方式
                applyTime,      // 申请时间
                place,          // 地点
                vitalSigns,     // 生命体征
                test,           // 检验
                inspect,        // 检查
                doctorOrder,    // 医嘱
                diagnose,       // 诊断
                content,        // 病历摘要
                summary,        // 总结
                inviteDoctors   // 会诊邀请医生
                );

			await _groupConsultationRepository.InsertAsync(groupConsultation); 
            */
        }

        #region constructor
        public GroupConsultationDataSeedContributor(
            IGroupConsultationRepository groupConsultationRepository, 
            GroupConsultationManager groupConsultationManager,
            IGuidGenerator guidGenerator)
        {
            _groupConsultationRepository = groupConsultationRepository;
            _groupConsultationManager = groupConsultationManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IGroupConsultationRepository _groupConsultationRepository;
        private readonly GroupConsultationManager _groupConsultationManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}
