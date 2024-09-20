using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 描    述:检查病理小项
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/24 14:23:27
    /// </summary>
    [Comment("检查病理小项")]
    public class PacsPathologyItem : Entity<Guid>
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        [Comment("检查项Id")]
        public Guid PacsId { get; set; }

        /// <summary>
        /// 标本名称多个用","隔开
        /// </summary>
        [Comment("标本名称")]
        [StringLength(500)]
        public string Specimen { get; set; }

        /// <summary>
        /// 取材部位
        /// </summary>
        [Comment("取材部位")]
        [StringLength(100)]
        public string DrawMaterialsPart { get; set; }

        /// <summary>
        /// 标本数量
        /// </summary>
        [Comment("标本数量")]
        public int SpecimenQty { get; set; }

        /// <summary>
        /// 离体时间
        /// </summary>
        [Comment("离体时间")]
        public DateTime LeaveTime { get; set; }

        /// <summary>
        /// 固定时间
        /// </summary>
        [Comment("固定时间")]
        public DateTime RegularTime { get; set; }

        /// <summary>
        /// 特异性感染
        /// </summary>
        [Comment("特异性感染")]
        [StringLength(100)]
        public string SpecificityInfect { get; set; }

        /// <summary>
        /// 申请目的
        /// </summary>
        [Comment("申请目的")]
        [StringLength(100)]
        public string ApplyForObjective { get; set; }

        /// <summary>
        /// 临床症状及体征
        /// </summary>
        [Comment("临床症状及体征")]
        [StringLength(500)]
        public string Symptom { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public PacsPathologyItem(Guid id)
        {
            Id = id;
        }
    }
}
