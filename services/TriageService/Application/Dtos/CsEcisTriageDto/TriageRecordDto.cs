using System;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊分诊记录Dto
    /// </summary>
    public class TriageRecordDto
    {
        
        public Guid Id { get; set; }

        /// <summary>
        /// MH_PatientVisit
        /// </summary>
        public Guid PVID { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>

        public DateTime? TriageDT { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>

        public string TriageBy { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>

        public string TriageDepartmentCode { get; set; }

        /// <summary>
        /// 分诊科室
        /// </summary>

        public string TriageDepartment { get; set; }

        /// <summary>
        /// 分诊去向编码
        /// </summary>

        public string TriageTargetCode { get; set; }

        /// <summary>
        /// 分诊去向
        /// </summary>

        public string TriageTarget { get; set; }

        /// <summary>
        /// 其他分诊去向：二次分诊
        /// </summary>

        public string OtherTriageTarget { get; set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>

        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 自动分诊级别
        /// </summary>

        public string AutoTriageLevel { get; set; }

        /// <summary>
        /// 分诊理由
        /// </summary>

        public string TriageMemo { get; set; }


        public int HasVitalSign { get; set; }

        /// <summary>
        /// 是否包含分诊评分记录
        /// </summary>

        public int HasScoreRecord { get; set; }

        /// <summary>
        /// 是否包含判定依据记录
        /// </summary>

        public int HasAccordingRecord { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>

        public DateTime StartRecordDT { get; set; }


        public bool RegisterFirst { get; set; }

        /// <summary>
        /// 分诊级别变更
        /// </summary>

        public string ChangeLevel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string Comment { get; set; }
    }
}