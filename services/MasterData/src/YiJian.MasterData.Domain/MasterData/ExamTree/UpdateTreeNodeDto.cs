using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbPrimaryType = System.String;

namespace YiJian.MasterData
{
    /// <summary>
    /// 新增树形结构的Dto
    /// </summary>
    public class UpdateTreeNodeDto
    {
        [StringLength(50)]
        [Comment("Id")]
        public DbPrimaryType Id { get; set; }
        /// <summary>
        /// 当前节点名称
        /// </summary>
        [Required]
        [Comment("当前节点全路径")]
        public string NodeName { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public decimal Sort { get; set; }
    }
}
