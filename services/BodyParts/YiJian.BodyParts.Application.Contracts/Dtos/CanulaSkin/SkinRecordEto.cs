using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class SkinRecordEto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public Guid PiId { get; set; }

        /// <summary>
        /// 导管关联键
        /// </summary>
        public Guid NursingCanulaId { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public int? EventType { get; set; }

        /// <summary>
        /// 皮肤记录
        /// </summary>
        public string CanulaRecord { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperateCode { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperateName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;
    }
}
