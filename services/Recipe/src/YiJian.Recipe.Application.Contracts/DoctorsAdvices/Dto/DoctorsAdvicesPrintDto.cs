using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 描    述:医嘱打印模型
    /// 创 建 人:杨凯
    /// 创建时间:2024/1/5 10:12:35
    /// </summary>
    public class DoctorsAdvicesPrintDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 医嘱
        /// </summary>
        public List<DoctorsAdvicesDto> doctorsAdvicesDtos { get; set; }
    }
}
