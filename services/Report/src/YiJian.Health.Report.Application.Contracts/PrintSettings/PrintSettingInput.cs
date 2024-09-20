using System;

namespace YiJian.Health.Report.PrintSettings
{
    /// <summary>
    /// 打印设置查询实体
    /// </summary>
    public class PrintSettingInput
    {
        /// <summary>
        /// 目录id
        /// </summary>
        public Guid Catalog { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}