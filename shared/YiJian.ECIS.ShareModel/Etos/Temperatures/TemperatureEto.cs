namespace YiJian.ECIS.ShareModel.Etos.Temperatures
{
    /// <summary>
    /// 描述：描述：体温单同步Eto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:35:50
    /// </summary>
    public class TemperatureEto
    {
        /// <summary>
        /// 患者Id
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime MeasureTime { get; set; }

        /// <summary>
        /// 体温（单位℃）
        /// </summary>
        public decimal? Temperature { get; set; }

        /// <summary>
        /// 脉搏P(次/min)
        /// </summary>
        public int? Pulse { get; set; }

        /// <summary>
        /// 呼吸(次/min)
        /// </summary>
        public int? Breathing { get; set; }

        /// <summary>
        /// 心率(次/min)
        /// </summary>
        public int? HeartRate { get; set; }

        /// <summary>
        /// 血压BP收缩压(mmHg)
        /// </summary>
        public int? SystolicPressure { get; set; }

        /// <summary>
        /// 血压BP舒张压(mmHg)
        /// </summary>
        public int? DiastolicPressure { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        public string Consciousness { get; set; } = string.Empty;

        /// <summary>
        /// 护士账号
        /// </summary>
        public string NurseCode { get; set; } = string.Empty;

        /// <summary>
        /// 护士名字
        /// </summary>
        public string NurseName { get; set; } = string.Empty;

        /// <summary>
        /// 动态字段
        /// </summary>
        public List<IntakeEto> TemperatureDynamics { get; set; } = default!;
    }

    /// <summary>
    /// 出入量Eto
    /// </summary>
    public class IntakeEto
    {
        /// <summary>
        /// 属性字段
        /// </summary>
        public string PropertyCode { get; set; } = string.Empty;

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; } = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 额外标记 In=入量，Out=出量
        /// </summary>
        public string ExtralFlag { get; set; } = string.Empty;
    }
}
