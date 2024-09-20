using FreeSql.DataAnnotations;
using System;
using Volo.Abp;
using Volo.Abp.Auditing;

namespace Szyjian.BaseFreeSql
{
    /// <summary>
    /// 审计实体基类
    /// </summary>
    public class AuditEntity : IHasCreationTime, IHasDeletionTime, ISoftDelete, IHasModificationTime
    {
        /// <summary>
        /// 创建人Code
        /// </summary>
        [Column(DbType = "varchar(50)")]
        public string AddUserCode { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string AddUserName { get; set; }

        /// <summary>
        /// 删除人Code
        /// </summary>
        [Column(DbType = "varchar(50)")]
        public string DelUserCode { get; set; }

        /// <summary>
        /// 删除人名称
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string DelUserName { get; set; }

        /// <summary>
        /// 修改人Code
        /// </summary>
        [Column(DbType = "varchar(50)")]
        public string ModUserCode { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string ModUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local)]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}
