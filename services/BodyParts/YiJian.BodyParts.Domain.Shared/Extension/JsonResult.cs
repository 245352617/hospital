using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 页面返回内容
    /// </summary>
    public class JsonResult : JsonResult<dynamic>
    {
        /// <summary>
        /// 接口返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>    
        /// <param name="data"></param>
        /// <returns></returns>
        public static new JsonResult Write(int code, string msg, dynamic data)
        {
            return new JsonResult { Code = code, Msg = msg, Data = data };
        }

        /// <summary>
        /// 返回错误消息
        /// </summary>
        ///  <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static new JsonResult Fail(string msg = "操作失败", object data = null)
        {
            return JsonResult.Write(code: 500, msg: msg, data: data);
        }

        /// <summary>
        /// 返回无数据消息
        /// </summary>
        ///  <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static new JsonResult DataNotFound(string msg = "数据不存在", object data = null)
        {
            return JsonResult.Write(code: 404, msg: msg, data: data);
        }

        /// <summary>
        /// 返回未输入消息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static new JsonResult RequestParamsIsNull(string msg = "请求未输入数据参数", object data = null)
        {
            return JsonResult.Write(code: 500, msg: msg, data: data);
        }


        /// <summary>
        /// 返回操作正常信息
        /// </summary>
        ///  <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static new JsonResult Ok(string msg = "操作成功", object data = null)
        {
            return JsonResult.Write(code: 200, msg: msg, data: data);
        }
    }

    /// <summary>
    /// 页面返回内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResult<T> : IReturnResult<T> where T : class
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public virtual int Code { get; set; } = 200;

        /// <summary>
        /// 消息
        /// </summary>
        public virtual string Msg { get; set; } = "OK";

        /// <summary>
        /// 内容
        /// </summary>
        public virtual T Data { get; set; }

        /// <summary>
        /// 接口返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>    
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult<T> Write(int code, string msg, object data)
        {
            return new JsonResult<T> { Code = code, Msg = msg, Data = (T)data };
        }

        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <param name="code">201为错误提示信息，可展示在界面上</param>
        ///  <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static JsonResult<T> Fail(int code = 500, string msg = "操作失败，请联系管理员！", object data = null)
        {
            return JsonResult<T>.Write(code: code, msg: msg, data: data);
        }

        /// <summary>
        /// 返回无数据消息
        /// </summary>
        ///  <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static JsonResult<T> DataNotFound(string msg = "数据不存在", object data = null)
        {
            return JsonResult<T>.Write(code: 404, msg: msg, data: data);
        }

        /// <summary>
        /// 返回无数据消息
        /// </summary>
        ///  <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static JsonResult<T> RequestParamsIsNull(string msg = "请求未输入数据参数", object data = null)
        {
            return JsonResult<T>.Write(code: 404, msg: msg, data: data);
        }

        /// <summary>
        /// 返回操作正常信息
        /// </summary>
        ///  <param name="msg">信息</param>
        /// <param name="data">内容</param>
        /// <returns></returns>
        public static JsonResult<T> Ok(string msg = "操作成功", T data = default(T))
        {
            return JsonResult<T>.Write(code: 200, msg: msg, data: data);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }
    }
}
