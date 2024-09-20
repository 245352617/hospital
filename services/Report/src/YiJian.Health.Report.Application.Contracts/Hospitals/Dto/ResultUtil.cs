
namespace YiJian.Health.Report.Hospitals.Dto
{
    /// <summary>
    /// 云签Dto
    /// </summary>
    public class StampResponseDto
    {
        /// <summary>
        /// 响应编码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        public EventValue EventValue { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string EventMsg { get; set; }
    }

    /// <summary>
    /// 云签
    /// </summary>
    public class EventValue
    {
        /// <summary>
        /// base64
        /// </summary>
        public string StampBase64 { get; set; }
    }

}