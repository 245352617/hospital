namespace YiJian.BodyParts
{
    /// <summary>
    /// 接口统一返回类
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public virtual ReturnResultCodeEnum Code { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public virtual string Msg { get; set; }

        /// <summary>
        /// 状态标识
        /// </summary>
        public virtual string Success { get; set; }

        private dynamic _Data;

        /// <summary>
        /// 事件扩展内容
        /// </summary>
        public virtual dynamic Data
        {
            get => _Data;
            set => _Data = value;
        }

        private dynamic _Exten;

        /// <summary>
        /// 扩展字段（常用于存储用户信息）
        /// </summary>
        public virtual dynamic Exten
        {
            get => _Exten;
            set => _Exten = value;
        }

        /// <summary>
        /// 接口返回方法
        /// </summary>
        /// <param name="code">状态码（默认200为正常，其它可自定义）</param>
        /// <param name="msg">事件内容</param>
        /// <param name="success">状态标识（默认"True"）</param>
        /// <param name="data">事件扩展内容(根据需要自定义格式)</param>
        /// <param name="Exten"></param>
        /// <returns></returns>
        public static ReturnResult Write(ReturnResultCodeEnum code = ReturnResultCodeEnum.成功返回200, string msg = "操作完成！", string success = "True", dynamic data = null, dynamic Exten = null)
        {
            var r= new ReturnResult { Code = code, Success = success, Msg = msg, Data = data, Exten = Exten };
           // ConsoleExten.WriteLine(r);
            return r;
        }

        public static ReturnResult Fail(string msg = "操作失败", object data = null,object Exten=null)
        {
            return ReturnResult.Write(code: ReturnResultCodeEnum.程序报错500, msg: msg, success: "False", data: data,Exten:Exten);
        }

        public static ReturnResult DataNotFound(string msg = "数据或模型不存在", object data = null)
        {
            return ReturnResult.Write(code: ReturnResultCodeEnum.程序报错500, msg: msg, success: "False", data: data);
        }

        public static ReturnResult RequestParamsIsNull(string msg = "请求未输入数据参数", object data = null)
        {
            return ReturnResult.Write(code: ReturnResultCodeEnum.程序报错500, msg: msg, success: "False", data: data);
        }

        public static ReturnResult Ok(string msg = "操作成功", object data = null, object exten = null)
        {
            return ReturnResult.Write(code: ReturnResultCodeEnum.成功返回200, msg: msg, success: "True", data: data, Exten: exten);
        }
    }
}