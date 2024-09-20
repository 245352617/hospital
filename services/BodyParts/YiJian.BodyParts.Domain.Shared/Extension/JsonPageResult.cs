using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 分页返回内容
    /// </summary>
    public class JsonPageResult : JsonResult<object>
    {

        /// <summary>
        /// 当前页
        /// </summary>
        public virtual int? PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public virtual int? PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public virtual int? TotalPage { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public virtual int TotalNumber { get; set; }


        /// <summary>
        /// 接口返回
        /// </summary>
        /// <param name="code">返回编码</param>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回内容</param> 
        /// <param name="totalnumber">总条数</param>    
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public static JsonPageResult Write(int code, string msg, object data, int totalnumber = 0, int? pageindex = null, int? pagesize = null)
        {
            return new JsonPageResult
            {
                Code = code,
                Msg = msg,
                PageIndex = pageindex,
                PageSize = pagesize,
                TotalNumber = totalnumber,
                TotalPage = pagesize.HasValue ? (totalnumber / pagesize.Value) + ((totalnumber % pagesize.Value) > 0 ? 1 : 0) : pagesize,
                Data = data
            };
        }

        public static new JsonPageResult Fail(string msg = "操作失败", object data = null)
        {
            return JsonPageResult.Write(code: 500, msg: msg, data: data);
        }

        public static new JsonPageResult DataNotFound(string msg = "数据不存在", object data = null)
        {
            return JsonPageResult.Write(code: 404, msg: msg, data: data);
        }

        public static new JsonPageResult RequestParamsIsNull(string msg = "请求未输入数据参数", object data = null)
        {
            return JsonPageResult.Write(code: 500, msg: msg, data: data);
        }

        /// <summary>
        /// 返回操作正常信息
        /// </summary>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回内容</param> 
        /// <param name="totalnumber">总条数</param>    
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public static JsonPageResult Ok(string msg = "操作成功", object data = null, int totalnumber = 0, int? pageindex = null, int? pagesize = null)
        {
            return JsonPageResult.Write(code: 200, msg: msg, data: data, totalnumber: totalnumber, pageindex: pageindex, pagesize: pagesize);
        }

    }


    /// <summary>
    /// 分页返回内容
    /// </summary>
    public class JsonPageResult<T> : JsonResult<T> where T : class
    {

        /// <summary>
        /// 当前页
        /// </summary>
        public virtual int? PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public virtual int? PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public virtual int? TotalPage { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public virtual int TotalNumber { get; set; }


        /// <summary>
        /// 接口返回
        /// </summary>
        /// <param name="code">返回编码</param>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回内容</param> 
        /// <param name="totalnumber">总条数</param>    
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public static JsonPageResult<T> Write(int code, string msg, T data, int totalnumber = 0, int? pageindex = null, int? pagesize = null)
        {
            return new JsonPageResult<T>
            {
                Code = code,
                Msg = msg,
                PageIndex = pageindex,
                PageSize = pagesize,
                TotalNumber = totalnumber,
                TotalPage = pagesize.HasValue ? (totalnumber / pagesize.Value) + ((totalnumber % pagesize.Value) > 0 ? 1 : 0) : pagesize,
                Data = data
            };
        }

        public static  JsonPageResult<T> Fail(string msg = "操作失败", T data = default(T))
        {
            return JsonPageResult<T>.Write(code: 500, msg: msg, data: data);
        }

        public static  JsonPageResult<T> DataNotFound(string msg = "数据不存在", T data = default(T))
        {
            return JsonPageResult<T>.Write(code: 404, msg: msg, data: data);
        }

        public static  JsonPageResult<T> RequestParamsIsNull(string msg = "请求未输入数据参数", T data = default(T))
        {
            return JsonPageResult<T>.Write(code: 500, msg: msg, data: data);
        }

        /// <summary>
        /// 返回操作正常信息
        /// </summary>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回内容</param> 
        /// <param name="totalnumber">总条数</param>    
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public static JsonPageResult<T> Ok(string msg = "操作成功", T data = default(T), int totalnumber = 0, int? pageindex = null, int? pagesize = null)
        {
            return JsonPageResult<T>.Write(code: 200, msg: msg, data: data, totalnumber: totalnumber, pageindex: pageindex, pagesize: pagesize);
        }

    }
}
