using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 群伤事件与患者信息Dto
    /// </summary>
    public class GroupInjuryAndPatientInfoDto
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
        /// 分诊级别
        /// </summary>
        public string TriageLevel { get; set; }
        
        /// <summary>
        /// 分诊去向
        /// </summary>
        public string TriageDirection { get; set; }
        
        /// <summary>
        /// 分诊科室
        /// </summary>
        public string TriageDept { get; set; }
        
        /// <summary>
        /// 最终诊断
        /// </summary>
        public string FinalDiagnosis { get; set; }
        
        /// <summary>
        /// 群伤事件类型
        /// </summary>
        public string GroupInjuryTypeName { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime HappeningTime { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 概要说明
        /// </summary>
        public string Description { get; set; }
    }
}