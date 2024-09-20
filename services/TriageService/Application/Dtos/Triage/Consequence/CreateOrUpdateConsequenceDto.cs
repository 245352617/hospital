using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 创建或更新分诊去向Dto
    /// </summary>
    public class CreateOrUpdateConsequenceDto
    {
        /// <summary>
        /// ConsequenceInfo表Id（注：修改时传入）
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 分诊科室Code
        /// </summary>
        public string TriageDept { get; set; }

        /// <summary>
        /// 分诊科室Name
        /// </summary>
        public string TriageDeptName { get; set; }

        ///// <summary>
        ///// 分诊科室变更Code
        ///// </summary>
        //public string ChangeDept { get; set; }

        ///// <summary>
        ///// 分诊科室变更Code
        ///// </summary>
        //public string ChangeDeptName { get; set; }

        /// <summary>
        /// 分诊去向Code
        /// </summary>
        public string TriageTarget { get; set; }

        /// <summary>
        /// 分诊去向Name
        /// </summary>
        public string TriageTargetName { get; set; }

        /// <summary>
        /// 其他分诊去向：二次分诊
        /// </summary>
        public string OtherTriageTarget { get; set; }

        /// <summary>
        /// 实际分诊级别Code
        /// </summary>
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 实际分诊级别Name
        /// </summary>
        public string ActTriageLevelName { get; set; }

        /// <summary>
        /// 自动分诊级别Code
        /// </summary>
        public string AutoTriageLevel { get; set; }

        /// <summary>
        /// 自动分诊级别Name
        /// </summary>
        public string AutoTriageLevelName { get; set; }

        ///// <summary>
        ///// 分诊级别变更Code
        ///// </summary>
        //public string ChangeLevel { get; set; }

        ///// <summary>
        ///// 分诊级别变更Name
        ///// </summary>
        //public string ChangeLevelName { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public string WorkType { get; set; }

        /// <summary>
        /// 分诊分区代码
        /// </summary>
        public string TriageAreaCode { get; set; }

        /// <summary>
        /// 变更分诊原因 
        /// </summary>
        /// <value></value>
        public string ChangeTriageReasonCode { get; set; }

        /// <summary>
        /// 变更分诊原因
        /// </summary>
        public string ChangeTriageReasonName { get; set; }
        /// <summary>
        /// 变更分诊
        /// </summary>
        /// <value></value>
        public bool ChangeTriage { get; set; }
    }
}