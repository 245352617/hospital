using System;

namespace YiJian.ECIS
{
    /// <summary>
    /// 描述：复制护理记录Dto
    /// 创建人： yangkai
    /// 创建时间：2023/4/13 17:14:30
    /// </summary>
    public class CopyCanulaDto
    {
        /// <summary>
        /// 复制的数据Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 复制选中：1，复制上一条：2
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 操作人签名
        /// </summary>
        public string Signature { get; set; }
    }
}
