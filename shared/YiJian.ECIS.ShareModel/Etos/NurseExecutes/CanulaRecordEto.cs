using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.NurseExecutes
{
    /// <summary>
    /// 描述：导管记录写入护理记录单Eto
    /// 创建人： yangkai
    /// 创建时间：2023/3/23 14:34:43
    /// </summary>
    public class CanulaRecordEto
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
        public EEventType? EventType { get; set; }

        /// <summary>
        /// 导管记录
        /// </summary>
        public string CanulaRecord { get; set; } = string.Empty;

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperateCode { get; set; } = string.Empty;

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperateName { get; set; } = string.Empty;

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
