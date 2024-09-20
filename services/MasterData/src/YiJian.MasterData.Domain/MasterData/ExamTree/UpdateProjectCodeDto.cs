using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbPrimaryType = System.String;

namespace YiJian.MasterData
{
    /// <summary>
    /// 新增树形结构的Dto
    /// </summary>
    public class UpdateProjectCodeDto
    {
        [StringLength(50)]
        [Comment("Id")]
        public DbPrimaryType Id { get; set; }
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
    }
}
