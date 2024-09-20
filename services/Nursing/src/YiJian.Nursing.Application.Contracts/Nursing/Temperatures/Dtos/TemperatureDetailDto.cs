using System;

namespace YiJian.Nursing.Temperatures.Dtos
{
    /// <summary>
    /// 描述：体温单右侧详情dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:03:18
    /// </summary>
    public class TemperatureDetailDto
    {
        /// <summary>
        /// 患者信息主键
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 测量日期
        /// </summary>
        public DateTime MeasureDate { get; set; }

        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime MeasureTime { get; set; }

        /// <summary>
        /// 测量时间点
        /// </summary>
        public int TimePoint { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 项目值
        /// </summary>
        public string PropertyValue { get; set; }
    }
}
