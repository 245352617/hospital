using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using NullableDbPrimaryType = System.String;

namespace YiJian.MasterData
{
    /// <summary>
    /// 新增树形结构的Dto
    /// </summary>
    public class AddTreeDto
    {
        /// <summary>
        /// 父级id 如果是根节点请传入 0 
        /// </summary>
        [StringLength(50)]
        [Comment("父级id")]
        public NullableDbPrimaryType ParentId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        [Required]
        [Comment("当前节点全路径")]
        public string NodeName { get; set; }

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
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编码 一般有项目编码的节点 视为最后一层。不再挂其他节点
        /// </summary>
        [StringLength(20)]
        [Comment("编码")]
        public string ProjectCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { set; get; }
    }
}
