using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.WebServer
{
    /// <summary>
    /// 浏览器传递过来对象
    /// </summary>
    public class InputInfo
    {
        public string? tsCode { get; set; }
    }

    /// <summary>
    /// 浏览器传递过来对象
    /// </summary>
    public class InputInfo<T>
    {
        public string? TsCode { get; set; }

        public T? Data { get; set; }
    }
}
