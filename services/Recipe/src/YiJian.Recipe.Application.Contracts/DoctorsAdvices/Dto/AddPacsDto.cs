using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 检查项信息参数
    /// </summary>
    public class AddPacsDto
    {
        /// <summary>
        /// 医嘱信息
        /// </summary>
        public ModifyDoctorsAdviceBaseDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 检查项集合
        /// </summary>
        public List<PacsDto> Items { get; set; } = new List<PacsDto>();

        /// <summary>
        /// 用户信息
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

    }

    /// <summary>
    /// 检查项信息参数（院前）
    /// </summary>
    public class AddPrePacsDto
    {
        /// <summary>
        /// 医嘱信息
        /// </summary>
        public ModifyDoctorsAdviceBaseDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 检查项集合
        /// </summary>
        public PacsDto Items { get; set; }

    }

}
