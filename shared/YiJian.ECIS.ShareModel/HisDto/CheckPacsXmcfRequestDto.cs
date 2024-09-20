namespace YiJian.ECIS.ShareModel.HisDto
{
    /// <summary>
    /// list
    /// </summary>
    public class RequestXmlistItem
    {
        /// <summary>
        /// 记录唯一值  优化传业务主键；如果没有，传本次入参的记录的唯一值
        /// </summary>
        public string jlwyz { get; set; }
        /// <summary>
        /// 组套序号  ProjectCode
        /// </summary>
        public string ztxh { get; set; }
        /// <summary>
        /// 费用序号 TargetCode
        /// </summary>
        public string fyxh { get; set; }
        /// <summary>
        /// 费用名称  TargetName
        /// </summary>
        public string fymc { get; set; }
        /// <summary>
        /// 费用数量 qty
        /// </summary>
        public int fysl { get; set; }
        /// <summary>
        /// 费用单价 price
        /// </summary>
        public decimal fydj { get; set; }
    }

    /// <summary>
    /// 检查项目重复校验
    /// </summary>
    public class CheckPacsXmcfRequestDto
    {
        /// <summary>
        /// 就诊类型/门诊住院 1：门急诊 2住院
        /// </summary>
        public int mzzy { get; set; }
        /// <summary>
        /// 患者ID/住院流水号 门诊传患者档案的病人ID ，  住院传住院流水号
        /// </summary>
        public int brid { get; set; }
        /// <summary>
        /// list
        /// </summary>
        public List<RequestXmlistItem> xmlist { get; set; }
    }
}
