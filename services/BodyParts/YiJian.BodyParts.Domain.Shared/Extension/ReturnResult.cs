using System;
using System.Threading.Tasks;

namespace YiJian.BodyParts
{
    public class ReturnResult<T> //: IReturnResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public ReturnResultCodeEnum Code { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 状态标识
        /// </summary>
        public string Success { get; set; }

        T _Data { get; set; }

        /// <summary>
        /// 事件扩展内容
        /// </summary>
        public T Data
        {
            get => this._Data; //.RemoveNullField();
            set => _Data = value;
        }

        private dynamic _Exten;

        /// <summary>
        /// 扩展字段（常用于存储用户信息）
        /// </summary>
        public virtual dynamic Exten
        {
            get => _Exten; //ObjectExten.RemoveNullField(_Exten);
            set => _Exten = value;
        }

        public virtual Task<ReturnResult<T>> ToTask()
        {
            return Task.FromResult(this);
        }

        public virtual ReturnResult<T> SetCode(ReturnResultCodeEnum code = ReturnResultCodeEnum.成功返回200)
        {
            this.Code = code;
            return this;
        }

        public virtual ReturnResult<T> SetMsg(string msg)
        {
            this.Msg = msg;
            return this;
        }

        /// <summary>
        /// 新增成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual ReturnResult<T> AddSuccess(string msg = "新增成功")
        {
            this.Msg = msg;
            return this;
        }

        /// <summary>
        /// 更新成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual ReturnResult<T> UpdateSuccess(string msg = "更新成功")
        {
            this.Msg = msg;
            return this;
        }

        public virtual ReturnResult<T> SetData(T data)
        {
            this.Data = data;

            return this;
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual ReturnResult<T> SetOk(ReturnResultCodeEnum code = ReturnResultCodeEnum.成功返回200)
        {
            this.Code = code;

            return this;
        }

        /// <summary>
        /// 找不到资源
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual ReturnResult<T> SetNotFound(ReturnResultCodeEnum code = ReturnResultCodeEnum.找不到页面404)
        {
            this.Code = code;
            return this;
        }

        /// <summary>
        /// 数据不存在
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual ReturnResult<T> SetDataNotFound(string msg = "数据不存在",
            ReturnResultCodeEnum code = ReturnResultCodeEnum.程序报错500)
        {
            this.Msg = msg;
            this.Code = code;
            return this;
        }

        /// <summary>
        /// 操作出错
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual ReturnResult<T> SetError(string msg = "操作出错",
            ReturnResultCodeEnum code = ReturnResultCodeEnum.程序报错500)
        {
            this.Code = code;
            this.Success = "False";
            if (msg != "操作出错")
                this.Msg = msg;
            return this;
        }

        public virtual ReturnResult<T> SetExten(dynamic exten = null)
        {
            this.Exten = exten;
            return this;
        }


        public static ReturnResult<T> Write(
            ReturnResultCodeEnum code = ReturnResultCodeEnum.成功返回200,
            string msg = "操作完成！", 
            string success = "True",
            T data = default(T), 
            dynamic Exten = null
            )
        {
            ReturnResult<T> r = new ReturnResult<T> {Code = code, Success = success, Msg = msg, Data = data, Exten = Exten};

            //ConsoleExten.WriteLine(r);

            return r;
        }

        public static ReturnResult<T> Fail(string msg = "操作失败", T data = default(T), dynamic exten = null)
        {
            return ReturnResult<T>.Write(code: ReturnResultCodeEnum.程序报错500, 
                msg: msg, success: "False", 
                data: data,
                Exten: exten);
        }

        public static ReturnResult<T> Warning(string msg = "功能预警", T data = default(T), dynamic exten = null)
        {
            return ReturnResult<T>.Write(code: ReturnResultCodeEnum.警告返回201, msg: "警告！" + msg, success: "true",
                data: data, Exten: exten);
        }

        public static ReturnResult<T> DataNotFound(string msg = "数据或模型不存在", T data = default(T))
        {
            return ReturnResult<T>.Write(code: ReturnResultCodeEnum.程序报错500, msg: msg, success: "False", data: data);
        }

        public static ReturnResult<T> RequestParamsIsNull(string msg = "请求未输入数据参数", T data = default(T))
        {
            return ReturnResult<T>.Write(code: ReturnResultCodeEnum.程序报错500, msg: msg, success: "False", data: data);
        }

        public static ReturnResult<T> Ok(string msg = "操作成功", T data = default(T), dynamic exten = null)
        {
            return ReturnResult<T>.Write(code: ReturnResultCodeEnum.成功返回200,
                msg: msg, success: "True", 
                data: data,
                Exten: exten);
        }
    }
}