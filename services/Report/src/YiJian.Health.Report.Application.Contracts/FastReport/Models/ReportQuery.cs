using System;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 报表查询参数
    /// </summary>
    public class ReportQuery
    {
        /// <summary>
        /// 响应内容 png, pdf, html
        /// </summary>
        public string Format { get; set; } = "html";

        /// <summary>
        /// 响应数据，在线：inline，attachment
        /// </summary>
        public bool Inline { get; set; } = true;

        /// <summary>
        /// 参数URL，PrintSetting的id
        /// </summary>
        public string ParamUrl { get; set; }

        /// <summary>
        /// 模板的id
        /// </summary>
        public Guid PrintTempId { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
    }
}