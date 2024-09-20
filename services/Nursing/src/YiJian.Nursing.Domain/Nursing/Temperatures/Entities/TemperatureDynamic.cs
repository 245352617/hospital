using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing.Temperatures
{
    /// <summary>
    /// 描述：体温表动态属性
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:59:37
    /// </summary>
    [Comment("体温表动态属性")]
    public class TemperatureDynamic : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public TemperatureDynamic(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 体温表主键
        /// </summary>
        [Comment("体温表主键")]
        public Guid TemperatureId { get; set; }

        /// <summary>
        /// 体温记录表主键
        /// </summary>
        [Comment("体温记录表主键")]
        public Guid TemperatureRecordId { get; set; }

        /// <summary>
        /// 属性字段
        /// </summary>
        [StringLength(64)]
        [Comment("属性字段")]
        public string PropertyCode { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [StringLength(64)]
        [Comment("属性名称")]
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [StringLength(500)]
        [Comment("属性值")]
        public string PropertyValue { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(20)]
        [Comment("单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 额外标记 In=入量，Out=出量
        /// </summary>
        [StringLength(100)]
        [Comment("额外标记")]
        public string ExtralFlag { get; set; }

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
