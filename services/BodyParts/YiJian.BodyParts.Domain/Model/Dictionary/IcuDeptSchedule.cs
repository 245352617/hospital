using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:科室班次
    /// </summary>
    public class IcuDeptSchedule : Entity<Guid>
    {
        public IcuDeptSchedule() { }

        public IcuDeptSchedule(Guid id) : base(id) { }


        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 班次代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        /// <example></example>
        [StringLength(20)]
        [Required]
        public string ScheduleName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [StringLength(10)]
        [Required]
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [StringLength(10)]
        [Required]
        public string EndTime { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Period { get; set; }

        /// <summary>
        /// 跨天(包含天数)
        /// </summary>
        [StringLength(4)]
        [Required]
        public string Days { get; set; }

        /// <summary>
        /// 小时数
        /// </summary>
        [StringLength(10)]
        [Required]
        public string Hours { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 前闭后开 = 1,前开后闭 = 2
        /// </summary>
        public ScheduleTimeTypeEnum ScheduleTimeTypeEnum { get; set; }

        /// <summary>
        /// 班次类别，观察项：0，出入量：1 血液净化：2，ECMO：3，PICCO：4
        /// </summary>
        public DeptScheduleTypeEnum Type { get; set; }
    }
}
