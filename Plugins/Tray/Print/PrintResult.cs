using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.Print
{
    public class PrintResult
    {
        public static PrintResult Finished(string templateCode)
        {
            return new PrintResult
            {
                Status = "finish",
                Message = "打印完成",
                TemplateCode = templateCode,
            };
        }

        public static PrintResult Begin(string templateCode)
        {
            return new PrintResult
            {
                Status = "begin",
                Message = "开始打印",
                TemplateCode = templateCode,
            };
        }


        public static PrintResult Error(string templateCode, string errorMessage)
        {
            return new PrintResult
            {
                Status = "error",
                Message = errorMessage,
                TemplateCode = templateCode,
            };
        }

        public static PrintResult Printing(string templateCode, int currentPageNo)
        {
            return new PrintResult
            {
                Status = "printing",
                Message = "打印中",
                TemplateCode = templateCode,
                CurrentPageNo = currentPageNo,
            };
        }

        /// <summary>
        /// 状态：begin 开始；finish 完成；error 错误；printing 打印中（返回带页码）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 当前打印模板代码
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPageNo { get; set; }
    }
}
