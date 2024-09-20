namespace YiJian.ECIS.ShareModel.HisDto
{
    /// <summary>
    /// list
    /// </summary>
    public class RequestLisXmlistItem
    {
        /// <summary>
        /// 保存时候唯一识别序号 item级别的id就行
        /// </summary>
        public int jlxh { get; set; }
        /// <summary>
        /// 组套序号 ProjectCode
        /// </summary>
        public int ztxh { get; set; }
    }

    /// <summary>
    /// 检查项目重复校验
    /// </summary>
    public class CheckLisXmcfRequestDto
    {
        /// <summary>
        /// 就诊类型/门诊住院 1：门急诊 2住院
        /// </summary>
        public int mzzy { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        public int ksdm { get; set; }

        /// <summary>
        /// 就诊序号
        /// </summary>
        public int jzxh { get; set; }
        /// <summary>
        /// 组套序号列表
        /// </summary>
        public List<RequestLisXmlistItem> ztxhs { get; set; }
    }
}
