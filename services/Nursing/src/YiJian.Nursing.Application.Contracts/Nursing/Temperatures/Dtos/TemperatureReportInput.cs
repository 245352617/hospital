using System;

namespace YiJian.Nursing.Temperatures.Dtos
{
    /// <summary>
    /// 描述：查询体温报表Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:05:32
    /// </summary>
    public class TemperatureReportInput
    {
        /// <summary>
        /// 患者信息主键
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}
