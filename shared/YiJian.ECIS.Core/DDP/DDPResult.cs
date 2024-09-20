namespace YiJian.ECIS.Core
{
    /// <summary>
    /// DDP返回包装层
    /// </summary>
    public class DDPResult<T>
    {

        public DDPResult()
        {
            code = 200;
            msg = "OK";
            data = data;
        }
        public DDPResult(T Tdata)
        {
            code = 200;
            msg = "OK";
            data = Tdata;
        }
        /// <summary>
        /// 状态码 200:成功
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
}
