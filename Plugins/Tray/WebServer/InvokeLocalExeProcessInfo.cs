using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tray
{
    public class InvokeLocalExeProcessInfo
    {
        /// <summary>
        /// 要调用的程序地址或程序名
        /// </summary>
        public string? ProcessName { get; set; }

        /// <summary>
        /// 要调用的程序需要的参数
        /// </summary>
        public string? ProcessArgs { get; set; }
    }
}
