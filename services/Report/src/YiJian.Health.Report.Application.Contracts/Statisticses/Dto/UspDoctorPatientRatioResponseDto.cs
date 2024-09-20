using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 接诊医生汇总信息
    /// </summary>
    public class UspDoctorPatientRatioResponseDto
    {
        /// <summary>
        /// 医生编码
        /// </summary>  
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
        public int Row { get; set; }
    }

}
