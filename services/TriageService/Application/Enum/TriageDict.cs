using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准
    /// </summary>
    public enum TriageDict
    {
        [Description("绿色通道")]
        GreenRoad = 1001,

        [Description("群伤事件")]
        GroupInjuryType = 1002,

        [Description("费别")]
        Faber = 1003,

        [Description("来院方式")]
        ToHospitalWay = 1004,

        [Description("科室配置")]
        TriageDepartment = 1005,

        [Description("院前分诊去向")]
        TriageDirection = 1006,

        [Description("院前分诊级别")]
        TriageLevel = 1007,

        [Description("身份类型")]
        IdentityType = 1008,

        [Description("重点病种")]
        KeyDiseases = 1009,

        [Description("症状或体征")]
        SymptomsOrSigns = 1010,

        [Description("神志，意识")]
        Mind = 1011,

        [Description("就诊类型")]
        TypeOfVisit = 1012,

        [Description("评分类型")]
        ScoreType = 1013,

        [Description("快速通道")]
        FastTrack = 1014,

        [Description("性别")]
        Sex = 1015,

        [Description("分诊报表类型")]
        TriageReportType = 1016,

        [Description("主诉")]
        Narration = 1017,

        [Description("民族")]
        Nation = 1018,

        [Description("生命体征备注")]
        VitalSignRemark = 1019,

        [Description("生命体征心电图")]
        Cardiogram = 1021,

        [Description("最终去向")]
        LastDirection = 1022,

        [Description("证件类型")]
        IdType = 1023,

        [Description("人群")]
        Crowd = 1024,

        [Description("就诊原因")]
        VisitReason = 1025,

        [Description("与联系人关系")]
        SocietyRelation = 1026,

        [Description("参保地")]
        InsuplcAdmdv = 1027,

        /// <summary>
        /// 意识
        /// </summary>
        [Description("意识")]
        Consciousness = 1028,

        /// <summary>
        /// 国籍
        /// </summary>
        [Description("国籍")]
        Country = 1029,

        /// <summary>
        /// 分诊分区 
        /// </summary>
        [Description("分诊分区")]
        TriageArea = 1030,

        /// <summary>
        /// 变更分诊原因
        /// </summary> 
        [Description("变更分诊原因")]
        ChangeTriageReason = 1031,
        /// <summary>
        /// 门诊科室
        /// </summary>
        [Description("门诊科室")]
        OutpatientDepartment = 1032,
        /// <summary>
        /// 转诊限制
        /// </summary>
        [Description("转诊限制")]
        ReferralLimit = 1033,

        /// <summary>
        /// 特约记账类型
        /// </summary>
        [Description("特约记账类型")]
        SpecialAccountType = 1034,

        /// <summary>
        /// 急诊医生
        /// </summary>
        [Description("急诊医生")]
        EmergencyDoctor = 1101
    }
}
