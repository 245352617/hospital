using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace YiJian.Nursing.Temperatures
{
    /// <summary>
    /// 描述：体温单
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:19:18
    /// </summary>
    [Comment("体温单")]
    public class Temperature : Entity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public Temperature(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 患者信息主键
        /// </summary>
        [Comment("患者信息主键")]
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 测量日期
        /// </summary>
        [Comment("测量日期")]
        public DateTime MeasureDate { get; set; }

        /// <summary>
        /// 体温记录
        /// </summary>
        public virtual List<TemperatureRecord> TemperatureRecords { get; set; }

        /// <summary>
        /// 体温单动态属性
        /// </summary>
        public virtual List<TemperatureDynamic> TemperatureDynamics { get; set; }
    }
}
