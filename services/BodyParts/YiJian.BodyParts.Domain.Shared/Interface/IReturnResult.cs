using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts
{
    public interface IReturnResult<T>
    {
        ///<summary>
        /// 状态码
        /// </summary>
        int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        string Msg { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        T Data { get; set; }
    }
 
}
