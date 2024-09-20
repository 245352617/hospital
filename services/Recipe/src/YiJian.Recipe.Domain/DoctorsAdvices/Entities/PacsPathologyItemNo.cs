using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 描    述:检查病理小项序号
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/29 9:28:58
    /// </summary>
    [Comment("检查病理小项序号")]
    public class PacsPathologyItemNo : Entity<int>
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        [Comment("检查项Id")]
        public Guid PacsId { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        [Comment("标本名称")]
        [StringLength(500)]
        public string SpecimenName { get; set; }

        /// <summary>
        /// 是否已经打印
        /// </summary>
        [Comment("是否已经打印")]
        public bool IsPrint { get; set; }
    }
}
