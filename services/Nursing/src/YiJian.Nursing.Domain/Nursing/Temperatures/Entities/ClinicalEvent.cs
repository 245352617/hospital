using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing.Temperatures
{
    /// <summary>
    /// 描述：临床事件表
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:10:01
    /// </summary>
    [Comment("临床事件表")]
    public class ClinicalEvent : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public ClinicalEvent(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 患者信息主键
        /// </summary>
        [Comment("患者信息主键")]
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 事件分类编码
        /// </summary>
        [StringLength(64)]
        [Comment("事件分类编码")]
        public string EventCategoryCode { get; set; }

        /// <summary>
        /// 事件分类
        /// </summary>
        [StringLength(64)]
        [Comment("事件分类")]
        public string EventCategory { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        [Comment("发生时间")]
        public DateTime HappenTime { get; set; }

        /// <summary>
        /// 上下标编码
        /// </summary>
        [StringLength(20)]
        [Comment("上下标编码")]
        public string UpDownFlagCode { get; set; }

        /// <summary>
        /// 上下标
        /// </summary>
        [StringLength(20)]
        [Comment("上下标")]
        public string UpDownFlag { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        [Comment("事件描述")]
        public string EventDescription { get; set; }

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
