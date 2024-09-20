using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 获取分诊字典输入项
    /// </summary>
    public class TriageConfigWhereInput
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

        ///// <summary>
        ///// 排序字段
        ///// </summary>
        ///// <example></example>
        //public string Sorting { get; set; } = "Id";

        /// <summary>
        /// 分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准
        /// </summary>
        public string TriageConfigType { get; set; }

    }
}
