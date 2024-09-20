namespace YiJian.MasterData.External.PKU
{
    /// <summary>
    /// 检验单信息同步ETO
    /// </summary>
    //[EventName("SyncLabReportInfoEvents")]
    public class LabReportInfoEto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 样本采集类型
        /// </summary>
        public string SampleCollectType { get; set; }

        /// <summary>
        /// 注意信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 指引单大类
        /// </summary>
        public string CatelogName { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 试管名称
        /// </summary>
        public string TestTubeName { get; set; }

        /// <summary>
        /// 门诊合并号
        /// </summary>
        public string MergerNo { get; set; }
    }
}
