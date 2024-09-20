using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 科室班次配置
    /// </summary>
    public class DeptScheduleConfigDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 班次代码
        /// </summary>
        [Required]
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        /// <example></example>
        [Required]
        public string ScheduleName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public string EndTime { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// 跨天(包含天数)
        /// </summary>
        public bool Days { get; set; }

        /// <summary>
        /// 小时数
        /// </summary>
        public string Hours { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 班次类型 1前闭后开 2前开后闭
        /// </summary>
        public ScheduleTimeTypeEnum ScheduleTimeTypeEnum { get; set; }

        /// <summary>
        /// 班次类别，观察项：0，出入量：1 血液净化：2，ECMO：3，PICCO：4
        /// </summary>
        public DeptScheduleTypeEnum Type { get; set; }
    }
}