namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// DDP返回基类
    /// </summary>
    public class DdpBaseResponse<T>
    {
        /// <summary>
        /// 成功:200;异常:500
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 接口状态信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }

    }
}
