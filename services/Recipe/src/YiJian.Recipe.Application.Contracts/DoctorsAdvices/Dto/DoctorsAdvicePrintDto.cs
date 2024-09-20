using System;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 描    述:
    /// 创 建 人:杨凯
    /// 创建时间:2024/1/5 16:41:10
    /// </summary>
    public class DoctorsAdvicePrintDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint { get; set; }
    }
}
