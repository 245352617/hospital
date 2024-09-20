using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace YiJian.Recipes.GroupConsultation
{
    /// <summary>
    /// 会诊管理 领域服务
    /// </summary>
    public class GroupConsultationManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IGroupConsultationRepository _groupConsultationRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupConsultationRepository"></param>
        /// <param name="guidGenerator"></param>
        public GroupConsultationManager(IGroupConsultationRepository groupConsultationRepository,
            IGuidGenerator guidGenerator)
        {
            _groupConsultationRepository = groupConsultationRepository;
            _guidGenerator = guidGenerator;
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="pIID">分诊患者id</param>
        /// <param name="patientId">患者id</param>
        /// <param name="typeCode">会诊类型</param>
        /// <param name="typeName"></param>
        /// <param name="startTime">会诊开始时间</param>
        /// <param name="status">会诊状态</param>
        /// <param name="objectiveCode">会诊目的编码</param>
        /// <param name="objectiveContent">会诊目的内容</param>
        /// <param name="applyDeptCode">申请科室编码</param>
        /// <param name="applyDeptName">申请科室名称</param>
        /// <param name="applyCode">申请人编码</param>
        /// <param name="applyName">申请人名称</param>
        /// <param name="mobile">联系方式</param>
        /// <param name="applyTime">申请时间</param>
        /// <param name="place">地点</param>
        /// <param name="vitalSigns">生命体征</param>
        /// <param name="test">检验</param>
        /// <param name="inspect">检查</param>
        /// <param name="doctorOrder">医嘱</param>
        /// <param name="diagnose">诊断</param>
        /// <param name="content">病历摘要</param>
        /// <param name="summary">总结</param>
        /// <param name="inviteDoctors">会诊邀请医生</param>
        public async Task<GroupConsultation> CreateAsync(Guid pIID, // 分诊患者id
            string patientId, // 患者id
            [NotNull] string typeCode, // 会诊类型
            [NotNull] string typeName,
            DateTime startTime, // 会诊开始时间
            GroupConsultationStatus status, // 会诊状态
            string objectiveCode, // 会诊目的编码
            [NotNull] string objectiveContent, // 会诊目的内容
            string applyDeptCode, // 申请科室编码
            string applyDeptName, // 申请科室名称
            string applyCode, // 申请人编码
            string applyName, // 申请人名称
            string mobile, // 联系方式
            DateTime? applyTime, // 申请时间
            string place, // 地点
            string vitalSigns, // 生命体征
            string test, // 检验
            string inspect, // 检查
            string doctorOrder, // 医嘱
            string diagnose, // 诊断
            string content, // 病历摘要
            string summary, // 总结
            List<InviteDoctor.InviteDoctor> inviteDoctors // 会诊邀请医生
        )
        {
            var groupConsultation = new GroupConsultation(_guidGenerator.Create(),
                pIID, // 分诊患者id
                patientId, // 患者id
                typeCode, // 会诊类型
                typeName,
                startTime, // 会诊开始时间
                status, // 会诊状态
                objectiveCode, // 会诊目的编码
                objectiveContent, // 会诊目的内容
                applyDeptCode, // 申请科室编码
                applyDeptName, // 申请科室名称
                applyCode, // 申请人编码
                applyName, // 申请人名称
                mobile, // 联系方式
                applyTime, // 申请时间
                place, // 地点
                vitalSigns, // 生命体征
                test, // 检验
                inspect, // 检查
                doctorOrder, // 医嘱
                diagnose, // 诊断
                content, // 病历摘要
                summary, // 总结
                inviteDoctors // 会诊邀请医生
            );

            return await _groupConsultationRepository.InsertAsync(groupConsultation);
        }

        #endregion Create
    }
}