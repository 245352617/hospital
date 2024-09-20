using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing.Temperatures
{
    /// <summary>
    /// 描述：体温表记录
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:57:59
    /// </summary>
    [Comment("体温表记录")]
    public class TemperatureRecord : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public TemperatureRecord(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 体温表主键
        /// </summary>
        [Comment("体温表主键")]
        public Guid TemperatureId { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录表主键")]
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 测量时间点
        /// </summary>
        [Comment("测量时间点")]
        public int TimePoint { get; set; }

        /// <summary>
        /// 测量时间
        /// </summary>
        [Comment("测量时间")]
        public DateTime MeasureTime { get; set; }

        /// <summary>
        /// 体温（单位℃）
        /// </summary>
        [Comment("体温（单位℃）")]
        [Description("体温")]
        public decimal? Temperature { get; set; }

        /// <summary>
        /// 复测体温（单位℃）
        /// </summary>
        [Comment("复测体温（单位℃）")]
        public decimal? RetestTemperature { get; set; }

        /// <summary>
        /// 降温方式
        /// </summary>
        [StringLength(32)]
        [Comment("降温方式")]
        public string CoolingWay { get; set; }

        /// <summary>
        /// 测量位置
        /// </summary>
        [StringLength(32)]
        [Comment("测量位置")]
        public string MeasurePosition { get; set; }

        /// <summary>
        /// 脉搏P(次/min)
        /// </summary>
        [Comment("脉搏P(次/min)")]
        [Description("脉搏")]
        public int? Pulse { get; set; }

        /// <summary>
        /// 呼吸(次/min)
        /// </summary>
        [Comment("呼吸(次/min)")]
        [Description("呼吸")]
        public int? Breathing { get; set; }

        /// <summary>
        /// 心率(次/min)
        /// </summary>
        [Comment("心率(次/min)")]
        [Description("心率")]
        public int? HeartRate { get; set; }

        /// <summary>
        /// 血压BP收缩压(mmHg)
        /// </summary>
        [Comment("血压BP收缩压(mmHg)")]
        [Description("收缩压")]
        public int? DiastolicPressure { get; set; }

        /// <summary>
        /// 血压BP舒张压(mmHg)
        /// </summary>
        [Comment("血压BP舒张压(mmHg)")]
        [Description("舒张压")]
        public int? SystolicPressure { get; set; }

        /// <summary>
        /// 疼痛程度
        /// </summary>
        [Comment("疼痛程度")]
        [Description("疼痛程度")]
        public string PainDegree { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        [Comment("意识")]
        [Description("意识")]
        public string Consciousness { get; set; }

        /// <summary>
        /// 护士账号
        /// </summary>
        [Comment("护士账号")]
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名字
        /// </summary>
        [Comment("护士名字")]
        public string NurseName { get; set; }
    }
}
