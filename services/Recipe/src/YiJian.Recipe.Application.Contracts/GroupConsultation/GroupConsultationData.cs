using System.Collections.Generic;

namespace YiJian.Recipes.GroupConsultation
{
    using System;
    using YiJian.Recipe;

    /// <summary>
    /// 会诊管理 读取输出
    /// </summary>
    [Serializable]
    public class GroupConsultationData
    {
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
        public string TypeCode { get; set; }

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
        public string ObjectiveCode { get; set; }

        /// <summary>
        /// 会诊目的内容
        /// </summary>
        public string ObjectiveContent { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 申请人编码
        /// </summary>
        public string ApplyCode { get; set; }

        /// <summary>
        /// 申请人名称
        /// </summary>
        public string ApplyName { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        public string VitalSigns { get; set; }

        /// <summary>
        /// 检验
        /// </summary>
        public string Test { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        public string Inspect { get; set; }

        /// <summary>
        /// 医嘱
        /// </summary>
        public string DoctorOrder { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnose { get; set; }

        /// <summary>
        /// 病历摘要
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 总结
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 是否发起申请医生
        /// </summary>
        public bool IsApplyDoctor { get; set; } = false;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 会诊邀请医生
        /// </summary>
        public List<InviteDoctor.InviteDoctorData> InviteDoctors { get; set; }

        /// <summary>
        /// 会诊纪要医生
        /// </summary>
        public List<DoctorSummaryData> DoctorSummarys { get; set; }
    }
}