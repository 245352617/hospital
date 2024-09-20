using FreeSql.DataAnnotations;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 床位配置
    /// </summary>
    [Table(Name = "Config_Bed")]
    public class Bed
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Name = "CB_ID", IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string BedAreaCode { get; set; }

        /// <summary>
        /// 床位名称
        /// </summary>
        [Column(DbType = "nvarchar(20)")]
        public string BedName { get; set; }

        /// <summary>
        /// 床位排序
        /// </summary>
        public int BedOrder { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}