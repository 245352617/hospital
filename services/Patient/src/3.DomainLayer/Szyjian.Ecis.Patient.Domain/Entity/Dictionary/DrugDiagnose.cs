using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 诊断字典
    /// </summary>
    [Table(Name = "Dict_DrugDiagnose")]
    public class DrugDiagnose
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        [Column(DbType = "nvarchar(200)")]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [Column(DbType = "nvarchar(500)")]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 传染病上报标识
        /// </summary>
        public int InfectionFlag { get; set; }

        /// <summary>
        /// 毒麻药诊断标识： 1 毒药 2 麻药 3 精神
        /// </summary>
        public int SpecialFlag { get; set; }

        /// <summary>
        /// 1.西医诊断2.中医诊断
        /// </summary>
        public int DiagType { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [Column(DbType = "nvarchar(50)")]
        public string PyCode { get; set; }

        /// <summary>
        /// 诊断说明
        /// </summary>
        [Column(DbType = "nvarchar(500)")]
        public string DiagnoseNote { get; set; }

        /// <summary>
        /// Icd10
        /// </summary>
        [StringLength(128)]
        public string Icd10 { get; set; }

        /// <summary>
        /// 报卡类型
        /// </summary>
        [StringLength(20)]
        public ECardReportingType CardRepType { get; set; }

        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="id"></param>
        public void setId(int id)
        {
            this.Id = id;
        }
    }
}