using System;

namespace YiJian.Health.Report.PrintCatalogs
{
    /// <summary>
    /// 打印目录Dto
    /// </summary>
    public class PrintCatalogDto
    {
        /// <summary>
        /// id，传参则是修改，没有就是新增
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string CataLogName { get; set; }

        /// <summary>
        /// 类型，0:打印中心，1：其他地方打印
        /// </summary>
        public int Type { get; set; }
    }
}