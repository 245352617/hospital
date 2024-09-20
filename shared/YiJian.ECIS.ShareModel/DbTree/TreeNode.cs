using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DbPrimaryType = System.String;
using NullableDbPrimaryType = System.String;

namespace YiJian.ECIS.ShareModel
{
    /// <summary>
    /// Tree 基类
    /// </summary>
    public class TreeNode : ITreeNode
    {

        [StringLength(50)]
        [Comment("Id")]
        public DbPrimaryType Id { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [StringLength(50)]
        [Comment("父级id")]
        public NullableDbPrimaryType ParentId { get; set; }

        /// <summary>
        /// 当前节点全路径
        /// </summary>
        [Comment("当前节点全路径")]
        public string FullPath { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        [Required]
        [Comment("当前节点全路径")]
        public string NodeName { get; set; }

        /// <summary>
        /// 子节点list
        /// </summary>
        [NotMapped]
        public List<TreeNode> ChildNodes { get; set; }

        /// <summary>
        /// Parent
        /// </summary>
        [NotMapped]
        //[JsonIgnore]
        public TreeNode Parent { get; set; }

        /// <summary>
        /// 逻辑软删除true 删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public decimal Sort { get; set; }

        /// <summary>
        /// 项目名称 
        /// </summary>
        [StringLength(200)]
        [Comment("名称")]
        public string? ProjectName { get; set; }

        /// <summary>
        /// 项目编码 一般有项目编码的节点 视为最后一层。不再挂其他节点
        /// </summary>
        [StringLength(20)]
        [Comment("编码")]
        public string? ProjectCode { get; set; }

        /// <summary>
        /// 拼音编码 
        /// </summary>
        [StringLength(200)]
        [Comment("拼音编码")]
        public string? PyCode { get; set; }
    }
}
