using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian
{
    public class InvokeLocalExeProcessResult
    {
        public static InvokeLocalExeProcessResult Success(string processName)
        {
            return new InvokeLocalExeProcessResult
            {
                Status = "success",
                Message = "打开成功",
                ProcessName = processName,
            };
        }


        public static InvokeLocalExeProcessResult Error(string processName, string errorMessage)
        {
            return new InvokeLocalExeProcessResult
            {
                Status = "error",
                Message = errorMessage,
                ProcessName = processName,
            };
        }

        /// <summary>
        /// 状态：success 完成；error 错误
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 程序名称
        /// </summary>
        public string ProcessName { get; set; }
    }
}
