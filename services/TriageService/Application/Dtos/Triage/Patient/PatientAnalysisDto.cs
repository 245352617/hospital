using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊红黄区统计
    /// </summary>
    public class LevelPersonAnalysis
    {
        /// <summary>
        /// 所有分诊人数
        /// </summary>
        public int TotalTriagePerson { get; set; }

        /// <summary>
        /// 红区分诊人数
        /// </summary>
        public int RedTriagePerson { get; set; }

        /// <summary>
        /// 黄区分诊人数
        /// </summary>
        public int YellowTriagePerson { get; set; }

        /// <summary>
        /// 绿区分诊人数
        /// </summary>
        public int GreenTriagePerson { get; set; }

        /// <summary>
        /// 未分级人数
        /// </summary>
        public int NoLevelPerson { get; set; }
    }


    /// <summary>
    /// 统计项
    /// </summary>
    public class PatientAnalysisDto
    {
        /// <summary>
        /// 分诊红黄区统计
        /// </summary>
        public LevelPersonAnalysis LevelPersonAnalysis { get; set; }

        /// <summary>
        /// 分诊级别统计
        /// </summary>
        public PatientAnalysisItemResult TraigeLevelAnalysis { get; set; }

        /// <summary>
        /// 分诊性别统计
        /// </summary>
        public PatientAnalysisItemResult TraigeSexAnalysis { get; set; }

        /// <summary>
        /// 分诊变更原因统计
        /// </summary>
        public PatientAnalysisItemResult TriageTransferAnalysis { get; set; }

        /// <summary>
        /// 分诊部门统计
        /// </summary>
        public PatientAnalysisItemResult TraigeDepartmentAnalysis { get; set; }

        /// <summary>
        /// 分诊年龄统计
        /// </summary>
        public PatientAnalysisItemResult TraigeAgeAnalysis { get; set; }

        /// <summary>
        /// 评分项统计
        /// </summary>
        public PatientAnalysisItemResult TraigeScoreAnalysis { get; set; }

        /// <summary>
        /// 级别修改统计
        /// </summary>
        public PatientAnalysisItemResult LevelModifyAnalysis { get; set; }

        /// <summary>
        /// 部门修改统计
        /// </summary>
        public PatientAnalysisItemResult DeptModifyAnalysis { get; set; }

        /// <summary>
        /// 三无人员统计
        /// </summary>
        public PatientAnalysisItemResult NoThreenPersonAnalysis { get; set; }

        /// <summary>
        /// 分诊去向统计
        /// </summary>
        public List<TriageTarget> TriageTargetAnalysis { get; set; }

        /// <summary>
        /// 群上事件统计
        /// </summary>
        public PatientAnalysisItemResult GroupInjuryTypeAnalysis { get; set; }

        /// <summary>
        /// 绿色通道统计
        /// </summary>
        public PatientAnalysisItemResult GreenRoadAnalysis { get; set; }
    }

    public class TriageTarget
    {
        /// <summary>
        /// 项名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<PatientAnalysisItem> ItemList { get; set; }
    }


    /// <summary>
    /// 统计项
    /// </summary>
    public class PatientAnalysisItem
    {
        /// <summary>
        /// 统计项
        /// </summary>
        public string AnalysisItem { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int PersonNum { get; set; }
    }

    public class PatientAnalysisItemResult
    {
        /// <summary>
        /// 统计项
        /// </summary>
        public string[] AnalysisItem { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int[] ItemData { get; set; }
    }
}