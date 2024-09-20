using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分页查询患者告知信息Dto
    /// </summary>
    public class PatientInformQueryDto
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
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 病人检索
        /// </summary>
        public string PatientSearch { get; set; }

        /// <summary>
        /// 急救任务流水
        /// </summary>
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 预警级别
        /// </summary>
        public string WarningLv { get; set; }

        /// <summary>
        /// 病重判断
        /// </summary>
        public string DiseaseIdentification { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string Source { get; set; }
    }
}