using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.EMR.CloudSign.Dto
{
    /// <summary>
    /// 描述：云签请求结果
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 14:19:04
    /// </summary>
    public class CloudSignResultDto<T> where T : class
    {
        /// <summary>
        /// 状态码不为0表示执行失败
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        public string EventMsg { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        public T EventValue { get; set; }
    }

    /// <summary>
    /// 签名结果
    /// </summary>
    public class SigndataResultDto
    {
        /// <summary>
        /// 签名值
        /// </summary>
        public string SignedData { get; set; }

        /// <summary>
        /// 时间戳签名值
        /// </summary>
        public string Timestamp { get; set; }
    }
}
