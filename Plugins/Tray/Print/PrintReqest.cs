using System.Data;

namespace YiJian.CardReader.Print
{
    public class PrintReqest
    {
        /// <summary>
        /// 操作（Print、Preview）
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 打印代码
        /// </summary>
        public string? TemplateCode { get; set; }

        /// <summary>
        /// 数据来源类型
        /// </summary>
        public ESourceType? SourceType { get; set; }

        /// <summary>
        /// 是否横向
        /// </summary>
        public bool IsTransverse { get; set; }
    }

    public class PrintDevReqest : PrintReqest
    {
        /// <summary>
        /// 其他参数
        /// </summary>
        public string? OtherParam { get; set; }

        /// <summary>
        /// 打印数据
        /// </summary>
        public DataSet Data { get; set; }
    }

    /// <summary>
    /// 数据来源类型(doctorsAdvice=1,emr=2,other=3)
    /// </summary>
    public enum ESourceType
    {
        Default = 0,
        DoctorsAdvice = 1,
        Emr = 2,
        PastingBottles = 3,
        WristStrap = 4,
        A5ToA4 = 5
    }

}
