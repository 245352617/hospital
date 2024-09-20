using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts
{
    public interface IReturnPageResult<T>
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
        /// 当前页
        /// </summary>
        int? PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        int? PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        int? TotalPage { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        int TotalNumber { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        T Data { get; set; }

    }

}
