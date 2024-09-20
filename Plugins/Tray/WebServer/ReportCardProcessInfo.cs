namespace YiJian.Tray.WebServer
{
    /// <summary>
    /// 浏览器传递过来对象
    /// 调用报卡服务的一些参数
    /// </summary>
    public class ReportCardProcessInfo
    {
        /// <summary>
        /// 就诊号
        /// </summary>
        public string? VisitNo { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string? PatientID { get; set; }

        /// <summary>
        /// 报卡编号
        /// </summary>
        public string? ReportCardCode { get; set; }

        /// <summary>
        /// 操作工号
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string? DeptCode { get; set; }

        /// <summary>
        /// 动作，是新增Add，还是查看报卡View，还是查询报卡状态ViewStatus
        /// </summary>
        public string? Action { get; set; } = "ViewStatus";

        /// <summary>
        /// 默认 0 <br/>
        /// 0 表示调用程序和参数由使用托盘程序配置，调用者不需要提供ProcessPath和ProcessArgs <br/>
        /// 1 表示调用程序和参数由调用者（现在指前端）提供，需要提供ProcessPath和ProcessArgs
        /// </summary>
        public int UseType { get; set; } = 0;

        /// <summary>
        /// 要调用的程序地址或程序名
        /// </summary>
        public string? ProcessPath { get; set; }

        /// <summary>
        /// 要调用的程序需要的参数
        /// </summary>
        public string? ProcessArgs { get; set; }

    }
}
