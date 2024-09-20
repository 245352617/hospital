using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 接诊医生汇总信息
    /// </summary>
    public class UspNursePatientRatio
    {
        /// <summary>
        /// 医生编码
        /// </summary> 
        [Key]
        public string NurseCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 患者总数
        /// </summary>
        public int ReceptionTota { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        [NotMapped]
        public int Row { get; set; }
    }



}
