namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 入院通知信息
    /// </summary>
    public class AdmissionNoticeDto
    {
        /// <summary>
        /// 是否绿通，true=绿色通道，false 非绿色通道
        /// </summary>
        public bool IsGreenChannel { get; set; } = false;

        /// <summary>
        /// 绿色通道/非绿色通道
        /// </summary>
        public string GreenChannel
        {
            get
            {
                if (IsGreenChannel) return "绿色通道";
                return "非绿色通道";
            }
        }

        /// <summary>
        /// 患者信息相关的数据
        /// </summary>
        public AdmissionRecordDto Paitent { get; set; }
    }

}