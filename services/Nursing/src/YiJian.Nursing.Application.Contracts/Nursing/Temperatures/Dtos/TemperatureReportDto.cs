using System;
using System.Collections.Generic;

namespace YiJian.Nursing.Temperatures.Dtos
{
    /// <summary>
    /// 描述：体温报表Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:04:15
    /// </summary>
    public class TemperatureReportDto
    {
        /// <summary>
        /// 体温单头部
        /// </summary>
        public List<TemperatureOtherRecordDto> TemperaturesHead { get; set; }

        /// <summary>
        /// 体温数据
        /// </summary>
        public List<TemperatureDataDto> TemperatureData { get; set; }

        /// <summary>
        /// 动态数据
        /// </summary>
        public List<TemperatureOtherRecordDto> TemperatureDynamic { get; set; }
    }

    /// <summary>
    /// 体温数据
    /// </summary>
    public class TemperatureDataDto
    {
        /// <summary>
        /// 测量日期
        /// </summary>
        public DateTime MeasureDate { get; set; }

        /// <summary>
        /// 测量时间点
        /// </summary>
        public int TimePoint { get; set; }

        /// <summary>
        /// 体温（单位℃）
        /// </summary>
        public decimal? Temperature { get; set; }

        /// <summary>
        /// 脉搏P(次/min)
        /// </summary>
        public int? Pulse { get; set; }

        /// <summary>
        /// 心率(次/min)
        /// </summary>
        public int? HeartRate { get; set; }

        /// <summary>
        /// 呼吸(次/min)
        /// </summary>
        public int? Breathing { get; set; }

        /// <summary>
        /// 疼痛程度
        /// </summary>
        public string PainDegree { get; set; }

        /// <summary>
        /// 血压
        /// </summary>
        public string PressureRecord { get; set; }

        /// <summary>
        /// 降温方式
        /// </summary>
        public string CoolingWay { get; set; }

        /// <summary>
        /// 测量位置
        /// </summary>
        public string MeasurePosition { get; set; }

        /// <summary>
        /// 复测体温（单位℃）
        /// </summary>
        public decimal? RetestTemperature { get; set; }

        /// <summary>
        /// 上标事件描述
        /// </summary>
        public string UpEventDescription { get; set; }

        /// <summary>
        /// 下标事件描述
        /// </summary>
        public string DownEventDescription { get; set; }
    }

    /// <summary>
    /// 体温动态数据
    /// </summary>
    public class TemperatureOtherRecordDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 额外标记 In=入量，Out=出量
        /// </summary>
        public string ExtralFlag { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 第一天数据
        /// </summary>
        public string Field1 { get; set; }

        /// <summary>
        /// 第二天数据
        /// </summary>
        public string Field2 { get; set; }

        /// <summary>
        /// 第三天数据
        /// </summary>
        public string Field3 { get; set; }

        /// <summary>
        /// 第四天数据
        /// </summary>
        public string Field4 { get; set; }

        /// <summary>
        /// 第五天数据
        /// </summary>
        public string Field5 { get; set; }

        /// <summary>
        /// 第六天数据
        /// </summary>
        public string Field6 { get; set; }

        /// <summary>
        /// 第七天数据
        /// </summary>
        public string Field7 { get; set; }
    }
}
