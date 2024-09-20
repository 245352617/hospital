using System;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 流转记录Dto
    /// </summary>
    public class TransferRecordDto
    {
        /// <summary>
        /// 流转时间
        /// </summary>
        public DateTime TransferTime { get; set; }

        /// <summary>
        /// 转向区域编码
        /// </summary>
        public string ToAreaCode { get; set; }

        /// <summary>
        /// 转向区域
        /// </summary>
        public string ToArea { get; set; }

        /// <summary>
        /// 来自区域编码
        /// </summary>
        public string FromAreaCode { get; set; }

        /// <summary>
        /// 来自区域编码
        /// </summary>
        public string FromArea { get; set; }

        /// <summary>
        /// 转向科室
        /// </summary>
        public string ToDept { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 流转原因
        /// </summary>
        public string TransferReason { get; set; }
    }
}