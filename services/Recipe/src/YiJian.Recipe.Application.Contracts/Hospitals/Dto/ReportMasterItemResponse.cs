namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 检验项目
    /// </summary>
    public class ReportMasterItemResponse
    {
        /// <summary>
        /// 检验内部ID
        /// </summary>
        public string MasterItemId { get; set; }

        /// <summary>
        /// 检验项目序号
        /// </summary>
        public string MasterItemNo { get; set; }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string MasterItemCode { get; set; }

        /// <summary>
        /// 检验项目名称[展示]
        /// </summary>
        public string MasterItemName { get; set; }

    }

}