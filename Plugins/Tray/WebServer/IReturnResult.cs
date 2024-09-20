using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.WebServer
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
        T? Data { get; set; }

    }
    public interface IBaseReturnResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        int Code { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        string Msg { get; set; }

        /// <summary>
        /// 事件扩展内容
        /// </summary>
        dynamic? Data { get; set; }
    }

    public interface IReturnResult : IBaseReturnResult
    {
        /// <summary>
        /// 状态标识
        /// </summary>
        string Success { get; set; }

        /// <summary>
        /// 扩展字段（常用于存储用户信息）
        /// </summary>
        dynamic Exten { get; set; }
    }
}
