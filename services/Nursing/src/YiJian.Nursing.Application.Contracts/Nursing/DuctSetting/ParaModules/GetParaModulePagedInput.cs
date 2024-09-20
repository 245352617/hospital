using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Nursing.Settings
{
    /// <summary>
    /// 表:模块参数 分页排序输入
    /// </summary>
    [Serializable]
    public class GetParaModulePagedInput : PageBase
    {
        /// <summary>
        /// 过滤条件.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 排序字段.
        /// </summary>
        public string Sorting { get; set; }
    }
}
