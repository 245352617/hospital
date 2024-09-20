using FreeSql.DataAnnotations;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 床卡配置
    /// </summary>
    [Table(Name = "Config_BedCard")]
    public class BedCard
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 床头卡颜色
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string BedColor { get; set; }

        /// <summary>
        /// 颜色配置
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string ColorRGB { get; set; }

        /// <summary>
        /// 标签配置
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string Label { get; set; }

        /// <summary>
        /// 分诊等级配置
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string TriageLevel { get; set; }

        /// <summary>
        /// 护理等级配置
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string NurseLevel { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
