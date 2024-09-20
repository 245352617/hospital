using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 接口调用返回参数
    /// </summary>
    public class MZResult
    {
        /// <summary>
        /// 结果 （false表示失败，true表示成功）
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 保存成功或失败原因
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public static MZResult Success()
        {
            return new MZResult { Result = true, Message = "保存成功" };
        }

        public static MZResult Fail(string message = "保存失败")
        {
            return new MZResult { Result = false, Message = message };
        }
    }
}
