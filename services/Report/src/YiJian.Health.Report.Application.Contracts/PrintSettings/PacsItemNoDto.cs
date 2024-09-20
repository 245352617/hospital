using System;

namespace YiJian.Health.Report.PrintSettings
{
    /// <summary>
    /// 检查条码列表
    /// </summary>
    public class PacsItemNoDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 检查项目
        /// </summary>
        public string PacsName { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        public string PathologyName { get; set; }

        /// <summary>
        /// 打印地址
        /// </summary>
        public string PrintUrl { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 是否已打
        /// </summary>
        public bool IsPrint { get; set; }
    }
}
