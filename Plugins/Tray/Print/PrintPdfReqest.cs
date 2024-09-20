using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.Print
{
    public class PrintPdfReqest
    {
        /// <summary>
        /// 打印代码
        /// </summary>
        public string? TemplateCode { get; set; }

        /// <summary>
        /// PDF 打印模式下的PDF url地址
        /// </summary>
        public string? PdfUrl { get; set; }

        /// <summary>
        /// 页码（分页打印）
        /// 页码从1开始
        /// </summary>
        public int[] Pages { get; set; }

        /// <summary>
        /// 纸张
        /// </summary>
        public string? PaperName { get; set; }
    }
}
