using System;
using System.Collections.Generic;

namespace YiJian.Nursing.Temperatures.Dtos
{
    /// <summary>
    /// 描述：体温单Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:56:20
    /// </summary>
    public class TemperatureDto
    {
        /// <summary>
        /// 体温表主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者信息主键
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 测量日期
        /// </summary>
        public DateTime MeasureDate { get; set; }

        /// <summary>
        /// 体温记录
        /// </summary>
        public List<TemperatureRecord> TemperatureRecords { get; set; }

        /// <summary>
        /// 体温单动态属性
        /// </summary>
        public List<TemperatureDynamic> TemperatureDynamics { get; set; }
    }
}
