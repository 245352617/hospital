using System;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 纸张大小Dto
    /// </summary>
    public class PageSizeDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public decimal Width { get; set; }
    }
}