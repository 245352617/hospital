using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.Recipe;

namespace YiJian.Recipes.GroupConsultation
{
    /// <summary>
    /// 会诊管理
    /// </summary>
    [Comment("会诊管理")]
    public class GroupConsultation : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 分诊患者id
        /// </summary>
        [Comment("分诊患者id")]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [Comment("患者id")]
        public string PatientId { get; set; }

        /// <summary>
        /// 会诊类型编码
        /// </summary>
        [Comment("会诊类型编码")]
        [StringLength(50)]
        [Required]
        public string TypeCode { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Comment("类型名称")]
        [StringLength(100)]
        [Required]
        public string TypeName { get; set; }

        /// <summary>
        /// 会诊开始时间
        /// </summary>
        [Comment("会诊开始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 会诊状态
        /// </summary>
        [Comment("会诊状态")]
        public GroupConsultationStatus Status { get; set; }

        /// <summary>
        /// 会诊目的编码
        /// </summary> 
        [Comment("会诊目的编码")]
        [StringLength(50)]
        public string ObjectiveCode { get; set; }

        /// <summary>
        /// 会诊目的内容
        /// </summary>
        [Comment("会诊目的内容")]
        [StringLength(200)]
        public string ObjectiveContent { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        [Comment("申请科室编码")]
        [StringLength(50)]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        [Comment("申请科室名称")]
        [StringLength(100)]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 申请人编码
        /// </summary>
        [Comment("申请人编码")]
        [StringLength(50)]
        public string ApplyCode { get; set; }

        /// <summary>
        /// 申请人名称
        /// </summary>
        [Comment("申请人名称")]
        [StringLength(100)]
        public string ApplyName { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [Comment("联系方式")]
        [StringLength(20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [Comment("申请时间")]
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        [Comment("地点")]
        [StringLength(50)]
        public string Place { get; set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        [Comment("生命体征")]
        [StringLength(4000)]
        public string VitalSigns { get; set; }

        /// <summary>
        /// 检验
        /// </summary>
        [Comment("检验")]
        [StringLength(4000)]
        public string Test { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        [Comment("检查")]
        [StringLength(4000)]
        public string Inspect { get; set; }

        /// <summary>
        /// 医嘱
        /// </summary>
        [Comment("医嘱")]
        [StringLength(4000)]
        public string DoctorOrder { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [Comment("诊断")]
        [StringLength(4000)]
        public string Diagnose { get; set; }

        /// <summary>
        /// 病历摘要
        /// </summary>
        [Comment("病历摘要")]
        [StringLength(4000)]
        public string Content { get; set; }

        /// <summary>
        /// 总结
        /// </summary>
        [Comment("总结")]
        [StringLength(4000)]
        public string Summary { get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        [Comment("结束时间")]
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 会诊邀请医生
        /// </summary>
        [NotNull]
        public List<InviteDoctor.InviteDoctor> InviteDoctors { get; set; }
        /// <summary>
        /// 会诊纪要医生
        /// </summary>
        [NotNull]
        public List<DoctorSummary> DoctorSummarys { get; set; }
        #region constructor

        /// <summary>
        /// 会诊管理构造器
        /// </summary>
        /// <param name="id"></param>
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
        public GroupConsultation(Guid id,
            Guid pIID, // 分诊患者id
            string patientId, // 患者id
            [NotNull] string typeCode, // 会诊类型
            [NotNull] string typeName,
            DateTime startTime, // 会诊开始时间
            GroupConsultationStatus status, // 会诊状态
            string objectiveCode, // 会诊目的编码
            string objectiveContent, // 会诊目的内容
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
        ) : base(id)
        {
            //分诊患者id
            PI_ID = pIID;

            Modify(patientId, // 患者id
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
                inviteDoctors, null // 会诊邀请医生
            );
        }

        #endregion

        #region Modify

        /// <summary>
        /// 修改
        /// </summary>
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
        /// <param name="doctorSummarys">会诊纪要医生</param>
        public void Modify(string patientId, // 患者id
            [NotNull] string typeCode, // 会诊类型
            [NotNull] string typeName,
            DateTime startTime, // 会诊开始时间
            GroupConsultationStatus status, // 会诊状态
            string objectiveCode, // 会诊目的编码
            string objectiveContent, // 会诊目的内容
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
            List<InviteDoctor.InviteDoctor> inviteDoctors, // 会诊邀请医生
            List<DoctorSummary> doctorSummarys// 会诊纪要医生
        )
        {
            //患者id
            PatientId = Check.Length(patientId, "患者id", GroupConsultationConsts.MaxPatientIdLength);

            //会诊类型
            TypeCode = Check.NotNull(typeCode, "会诊类型", GroupConsultationConsts.MaxTypeCodeLength);

            //
            TypeName = Check.NotNull(typeName, "", GroupConsultationConsts.MaxTypeNameLength);

            //会诊开始时间
            StartTime = startTime;

            //会诊状态
            Status = status;

            //会诊目的编码
            ObjectiveCode = Check.Length(objectiveCode, "会诊目的编码", GroupConsultationConsts.MaxObjectiveCodeLength);

            //会诊目的内容
            ObjectiveContent =
                Check.Length(objectiveContent, "会诊目的内容", GroupConsultationConsts.MaxObjectiveContentLength);

            //申请科室编码
            ApplyDeptCode = Check.Length(applyDeptCode, "申请科室编码", GroupConsultationConsts.MaxApplyDeptCodeLength);

            //申请科室名称
            ApplyDeptName = Check.Length(applyDeptName, "申请科室名称", GroupConsultationConsts.MaxApplyDeptNameLength);

            //申请人编码
            ApplyCode = Check.Length(applyCode, "申请人编码", GroupConsultationConsts.MaxApplyCodeLength);

            //申请人名称
            ApplyName = Check.Length(applyName, "申请人名称", GroupConsultationConsts.MaxApplyNameLength);

            //联系方式
            Mobile = Check.Length(mobile, "联系方式", GroupConsultationConsts.MaxMobileLength);

            //申请时间
            ApplyTime = applyTime;

            //地点
            Place = Check.Length(place, "地点", GroupConsultationConsts.MaxPlaceLength);

            //生命体征
            VitalSigns = Check.Length(vitalSigns, "生命体征", maxLength: 4000);

            //检验
            Test = Check.Length(test, "检验", maxLength: 4000);

            //检查
            Inspect = Check.Length(inspect, "检查", maxLength: 4000);

            //医嘱
            DoctorOrder = Check.Length(doctorOrder, "医嘱", maxLength: 4000);

            //诊断
            Diagnose = Check.Length(diagnose, "诊断", maxLength: 4000);

            //病历摘要
            Content = Check.Length(content, "病历摘要", maxLength: 4000);

            //总结
            Summary = Check.Length(summary, "总结", maxLength: 4000);

            //会诊邀请医生
            InviteDoctors = inviteDoctors;

            DoctorSummarys = doctorSummarys;
        }

        #endregion

        #region 结束会诊

        public void Finish(
            GroupConsultationStatus status // 会诊状态
        )
        {
            Status = status;
            FinishTime = DateTime.Now;
        }

        #endregion

        #region constructor

        private GroupConsultation()
        {
            // for EFCore
        }

        #endregion
    }
}