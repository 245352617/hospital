using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.Domain
{
    /// <summary>
    /// 打印模板
    /// </summary>
    public class PrintTemplate
    {
        /// <summary>
        /// 模板Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 打印模板编码
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 打印模板名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 模版类型
        /// </summary>
        public string? TempType { get; set; }
    }
}
