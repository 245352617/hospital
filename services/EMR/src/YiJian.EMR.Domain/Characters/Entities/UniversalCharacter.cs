using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.Characters.Entities
{
    /// <summary>
    /// 通用字符
    /// </summary>
    [Comment("通用字符")]
    public class UniversalCharacter : Entity<int>
    {
        public UniversalCharacter(string category, int sort)
        {
            Category = category;
            Sort = sort;
        }

        /// <summary>
        /// 分类-相当目录
        /// </summary>
        [Comment("分类")]
        [StringLength(50)]
        public string Category { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public virtual List<UniversalCharacterNode> Nodes { get; set; } = new List<UniversalCharacterNode>();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="category"></param>
        /// <param name="sort"></param>
        public void Update(string category, int sort)
        {
            Category = category;
            Sort = sort;
        }

    }


}
