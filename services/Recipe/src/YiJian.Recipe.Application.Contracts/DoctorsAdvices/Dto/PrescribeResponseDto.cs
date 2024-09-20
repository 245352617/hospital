using System;

namespace YiJian.DoctorsAdvices.Dto
{
    public class PrescribeResponseDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 关闭用法附加项
        /// </summary>
        public bool CloseUsage { get; set; }

        /// <summary>
        /// 关闭皮试附加项
        /// </summary>
        public bool CloseSkin { get; set; }
    }
}