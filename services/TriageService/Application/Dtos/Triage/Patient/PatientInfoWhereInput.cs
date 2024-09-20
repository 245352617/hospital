using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分页查询患者分诊信息Dto
    /// </summary>
    public class PatientInfoWhereInput
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        /// <example>1</example>
        [Required]
        public int SkipCount { get; set; }

        /// <summary>
        /// 第页个数
        /// </summary>
        /// <example>15</example>
        [Required]
        public int MaxResultCount { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        /// <example></example>
        public string Sorting { get; set; } = "Id";

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 120来院方式
        /// </summary>
        public string ToHospitalWayFrom120Code { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        public string ToHospitalWayCode { get; set; }

        /// <summary>
        /// 分诊级别代码
        /// </summary>
        public string LevelCode { get; set; }

        /// <summary>
        /// 分诊级别代码用于区域
        /// </summary>
        public string LevelAreaCode { get; set; }

        /// <summary>
        /// 分诊去向代码
        /// </summary>
        public string DirectionCode { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 分诊人code
        /// </summary>
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 群伤事件代码
        /// </summary>
        public string GroupInjuryCode { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string NarrationCode { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        public string NarrationName { get; set; }

        /// <summary>
        /// 评分下的主诉
        /// </summary>
        public string JudgmentMasterId { get; set; }

        /// <summary>
        /// 是否发热 传true和false
        /// </summary>
        public string IsHot { get; set; }

        /// <summary>
        /// 绿色通道代码
        /// </summary>
        public string GreenRoadCode { get; set; }

        /// <summary>
        /// 身份代码
        /// </summary>
        public string IdentityCode { get; set; }

        /// <summary>
        /// 费别代码
        /// </summary>
        public string ChargeTypeCode { get; set; }

        /// <summary>
        /// 重点病种代码
        /// </summary>
        public string DiseaseCode { get; set; }

        /// <summary>
        /// 分诊检索，姓名、病历号、病人姓名py首字母、身份证号等模糊查询
        /// </summary>
        public string PatientSearch { get; set; }

        /// <summary>
        /// 分诊状态，0暂存，1分诊
        /// </summary>
        public int TriageStatus { get; set; } = -1;

        /// <summary>
        /// 车辆编号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 排队号
        /// </summary>
        public string CallingSn { get; set; }
    }
}