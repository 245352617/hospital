using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{

    /// <summary>
    /// 检验项信息参数
    /// </summary>
    public class AddLisDto
    {
        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        public ModifyDoctorsAdviceBaseDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 检验项集合
        /// </summary>
        public List<LisDto> Items { get; set; } = new List<LisDto>();

    }

    /// <summary>
    /// 检验项信息参数（院前）
    /// </summary>
    public class AddPreLisDto
    {
        /// <summary>
        /// 医嘱信息
        /// </summary>
        public ModifyDoctorsAdviceBaseDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 检验项集合
        /// </summary>
        public LisDto Items { get; set; }

    }

}
