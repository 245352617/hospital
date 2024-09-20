using System;

namespace YiJian.Nursing.Temperatures.Dtos
{
    /// <summary>
    /// 描述：体温记录Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:02:12
    /// </summary>
    public class TemperatureRecordDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 测量时间点
        /// </summary>
        public int TimePoint { get; set; }

        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime MeasureTime { get; set; }

        /// <summary>
        /// 体温（单位℃）
        /// </summary>
        public decimal? Temperature { get; set; }

        ///// <summary>
        ///// 体温是否正常
        ///// </summary>
        //public bool IsNormal { get; set; } = true;

        /// <summary>
        /// 降温方式
        /// </summary>
        public string CoolingWay { get; set; }

        /// <summary>
        /// 复测体温（单位℃）
        /// </summary>
        public decimal? RetestTemperature { get; set; }

        /// <summary>
        /// 测量位置
        /// </summary>
        public string MeasurePosition { get; set; }

        /// <summary>
        /// 护士账号
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名字
        /// </summary>
        public string NurseName { get; set; }
    }
}
