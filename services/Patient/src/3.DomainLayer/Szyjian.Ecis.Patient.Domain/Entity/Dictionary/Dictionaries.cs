using FreeSql.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 字典
    /// </summary>
    [Table(Name = "Dict_Dictionaries")]
    public class Dictionaries
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Column(Name = "DD_ID", OldName = "Id", IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string DictionariesCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [Column(DbType = "nvarchar(100)")]
        public string DictionariesName { get; set; }

        /// <summary>
        /// 字典类型编码
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public DictionariesType DictionariesTypeCode { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string ExtendField1 { get; set; }
    }
}