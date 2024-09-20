using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 叫号历史查询
    /// Directory: output
    /// </summary>
    public class PagedCallInfoHistoryDto
    {
        /// <summary>
        /// 总计
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 已就诊合计
        /// </summary>
        public long TreatedCount { get; set; }
         

        /// <summary>
        /// 过号合计
        /// </summary>
        public long UntreatedOverCount { get; set; }

        /// <summary>
        /// 未挂号合计
        /// </summary>
        public long NotRegisterCount { get; set; }

        /// <summary>
        /// 出科合计
        /// </summary>
        public long OutDepartmentCount { get; set; }

        /// <summary>
        /// 退号合计
        /// </summary>
        public long RefundNoCount { get; set; }

        /// <summary>
        /// 查询结果列表
        /// </summary>
        public List<CallInfoData> Items { get; set; }
    }
}
