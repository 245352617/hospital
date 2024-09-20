#region 代码备注
//------------------------------------------------------------------------------
// 创建描述: 代码生成器自动创建于 11/06/2020 07:23:38
//
// 功能描述:
//
// 修改描述:
//------------------------------------------------------------------------------
#endregion 代码备注
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:科室班次
    /// </summary>
    public class IcuDeptScheduleDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 班次代码
        /// </summary>
        /// <example></example>
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        /// <example></example>
        public string ScheduleName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <example></example>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <example></example>
        public string EndTime { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        /// <example></example>
        public string Period { get; set; }

        /// <summary>
        /// 跨天(包含天数)
        /// </summary>
        /// <example></example>
        public string Days { get; set; }

        /// <summary>
        /// 小时数
        /// </summary>
        /// <example></example>
        public string Hours { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
        
        /// <summary>
        /// 班次时间 前闭后开 = 1,前开后闭 = 2
        /// </summary>
        public ScheduleTimeTypeEnum ScheduleTimeTypeEnum { get; set; }
    }
}
