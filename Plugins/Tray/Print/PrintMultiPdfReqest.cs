using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.Print
{
    public class PrintMultiPdfReqest
    {
        /// <summary>
        /// 打印代码
        /// </summary>
        public string? TemplateCode { get; set; }

        /// <summary>
        /// PDF 打印模式下的PDF url地址
        /// </summary>
        public IEnumerable<string> PdfUrls { get; set; }

        /// <summary>
        /// 打印单数据
        /// </summary>
        public IEnumerable<DataSet> Data { get; set; }

        /// <summary>
        /// 纸张
        /// </summary>
        public string? PaperName { get; set; }
    }
}
