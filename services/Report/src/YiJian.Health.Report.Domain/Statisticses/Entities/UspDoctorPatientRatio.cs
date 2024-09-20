using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;

namespace YiJian.Health.Report.Statisticses.Entities
{


    /// <summary>
    /// 接诊医生汇总信息
    /// </summary>
    public class UspDoctorPatientRatio
    { 
        /// <summary>
        /// 医生编码
        /// </summary> 
        [Key]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

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
