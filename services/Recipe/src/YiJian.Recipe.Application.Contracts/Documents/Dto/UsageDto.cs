using System;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 用药途经
    /// </summary>
    public class UsageDto
    {
        /// <summary>
        /// id 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用法编码
        /// </summary>  
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>  
        public string UsageName { get; set; }

    }

}
