using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者群伤事件Dto
    /// </summary>
    public class GroupInjuryPatientDto
    {
        /// <summary>
        /// 患者分诊基本信息表Id
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }
        
        /// <summary>
        /// 患者群伤事件表Id
        /// </summary>
        public Guid GroupInjuryInfoId { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string PatientId { get; set; }
        
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }
        
        /// <summary>
        /// 患者性别
        /// </summary>
        public string Sex { get; set; }
        
        /// <summary>
        /// 患者年龄
        /// </summary>
        public string Age { get; set; }
        
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime? TriageTime { get; set; }
        
        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? VisitingTime { get; set; }

        /// <summary>
        /// 分诊级别Code
        /// </summary>
        public string TriageLevel { get; set; }
        
        /// <summary>
        /// 分诊级别Name
        /// </summary>
        public string TriageLevelName { get; set; }
        
        /// <summary>
        /// 分诊去向
        /// </summary>
        public string TriageDirection { get; set; }
        
        /// <summary>
        /// 分诊科室
        /// </summary>
        public string TriageDept { get; set; }
        
        /// <summary>
        /// 绿色通道名称
        /// </summary>
        public string GreenRoad { get; set; }
    }
}