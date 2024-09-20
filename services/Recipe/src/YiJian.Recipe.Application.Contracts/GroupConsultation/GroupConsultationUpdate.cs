using JetBrains.Annotations;
using System.Collections.Generic;

namespace YiJian.Recipes.GroupConsultation
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Volo.Abp.Validation;
    using YiJian.Recipe;

    /// <summary>
    /// 会诊管理 修改输入
    /// </summary>
    [Serializable]
    public class GroupConsultationUpdate
    {
        /// <summary>
        /// 修改传参，新增不传参
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 分诊患者id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 会诊类型
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxTypeCodeLength),
            ErrorMessage = "会诊类型最大长度不能超过{1}!")]
        [Required(ErrorMessage = "会诊类型不能为空！")]
        public string TypeCode { get; set; }

        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxTypeNameLength),
            ErrorMessage = "最大长度不能超过{1}!")]
        [Required(ErrorMessage = "不能为空！")]
        public string TypeName { get; set; }

        /// <summary>
        /// 会诊开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 会诊状态
        /// </summary>
        public GroupConsultationStatus Status { get; set; }

        /// <summary>
        /// 会诊目的编码
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxObjectiveCodeLength),
            ErrorMessage = "会诊目的编码最大长度不能超过{1}!")]
        public string ObjectiveCode { get; set; }

        /// <summary>
        /// 会诊目的内容
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxObjectiveContentLength),
            ErrorMessage = "会诊目的内容最大长度不能超过{1}!")]
        public string ObjectiveContent { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxApplyDeptCodeLength),
            ErrorMessage = "申请科室编码最大长度不能超过{1}!")]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxApplyDeptNameLength),
            ErrorMessage = "申请科室名称最大长度不能超过{1}!")]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 申请人编码
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxApplyCodeLength),
            ErrorMessage = "申请人编码最大长度不能超过{1}!")]
        public string ApplyCode { get; set; }

        /// <summary>
        /// 申请人名称
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxApplyNameLength),
            ErrorMessage = "申请人名称最大长度不能超过{1}!")]
        public string ApplyName { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxMobileLength),
            ErrorMessage = "联系方式最大长度不能超过{1}!")]
        public string Mobile { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        [DynamicStringLength(typeof(GroupConsultationConsts), nameof(GroupConsultationConsts.MaxPlaceLength),
            ErrorMessage = "地点最大长度不能超过{1}!")]
        public string Place { get; set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        [StringLength(4000)]
        public string VitalSigns { get; set; }

        /// <summary>
        /// 检验
        /// </summary>
        [StringLength(4000)]
        [Comment("检验")]
        public string Test { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        [StringLength(4000)]
        public string Inspect { get; set; }

        /// <summary>
        /// 医嘱
        /// </summary>
        [StringLength(4000)]
        public string DoctorOrder { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [StringLength(4000)]
        public string Diagnose { get; set; }

        /// <summary>
        /// 病历摘要
        /// </summary>
        [StringLength(4000)]
        public string Content { get; set; }

        /// <summary>
        /// 总结
        /// </summary>
        [StringLength(4000)]
        public string Summary { get; set; }

        /// <summary>
        /// 会诊邀请医生
        /// </summary>
        [NotNull]
        public List<InviteDoctor.InviteDoctorUpdate> InviteDoctors { get; set; }

        /// <summary>
        /// 会诊纪要医生
        /// </summary>
        public List<DoctorSummaryUpdate> DoctorSummarys { get; set; }

        /// <summary>
        /// 拼接邀请科室信息
        /// </summary>
        /// <returns></returns>
        public string JoinDepts()
        {
            var depts = InviteDoctors.Select(s => s.DeptName).ToList();
            return string.Join(';', depts);
        }

    }
}