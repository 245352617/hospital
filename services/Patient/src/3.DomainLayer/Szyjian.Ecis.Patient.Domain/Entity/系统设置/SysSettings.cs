using FreeSql.DataAnnotations;
using Volo.Abp;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [Table(Name = "Sys_Settings")]
    public class SysSettings : ISoftDelete
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 系统设置编码
        /// </summary>
        [Column(DbType = "varchar(50)", IsNullable = false)]
        public string Code { get; set; }

        /// <summary>
        /// 系统设置编码
        /// </summary>
        [Column(DbType = "nvarchar(100)", IsNullable = false)]
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(DbType = "nvarchar(255)")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}