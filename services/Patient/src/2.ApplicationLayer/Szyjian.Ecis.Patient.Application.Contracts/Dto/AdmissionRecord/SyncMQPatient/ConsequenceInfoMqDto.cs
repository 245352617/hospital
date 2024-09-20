namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 推送患者分诊去向信息队列Dto
    /// </summary>
    public class ConsequenceInfoMqDto
    {
        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDept { get; set; }

        /// <summary>
        /// His 的科室编码
        /// </summary>
        public string HisDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 科室变更Code
        /// </summary>
        public string ChangeDept { get; set; }

        /// <summary>
        /// 科室变更名称
        /// </summary>
        public string ChangeDeptName { get; set; }

        /// <summary>
        /// 分诊去向编码
        /// </summary>
        public string TriageTarget { get; set; }

        /// <summary>
        /// 分诊去向名称
        /// </summary>
        public string TriageTargetName { get; set; }


        /// <summary>
        /// 其他分诊去向：二次分诊
        /// </summary>
        public string OtherTriageTarget { get; set; }


        /// <summary>
        /// 实际分诊级别编码
        /// </summary>
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 实际分诊级别名称
        /// </summary>
        public string ActTriageLevelName { get; set; }


        /// <summary>
        /// 自动分诊级别编码
        /// </summary>
        public string AutoTriageLevel { get; set; }

        /// <summary>
        /// 自动分诊级别名称
        /// </summary>
        public string AutoTriageLevelName { get; set; }


        /// <summary>
        /// 分诊级别变更
        /// </summary>
        public string ChangeLevel { get; set; }

        /// <summary>
        /// 分诊级别变更名称
        /// </summary>
        public string ChangeLevelName { get; set; }

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
        /// 分诊分区
        /// </summary>
        public string TriageAreaName { get; set; }

        /// <summary>
        /// 变更分诊原因 
        /// </summary>
        /// <value></value>
        public string ChangeTriageReasonCode { get; set; }

        /// <summary>
        /// 变更分诊原因 
        /// </summary>
        /// <value></value>
        public string ChangeTriageReasonName { get; set; }
    }
}