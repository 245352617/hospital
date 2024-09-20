using System.Collections.Generic;

namespace YiJian.Nursing
{
    /// <summary>
    /// 系统模块参数
    /// </summary>
    public class ModuleListDto
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

        /// <summary>
        /// 项目列表
        /// </summary>
        public List<ParaItemListDto> ParaItemListDtos { get; set; }
    }

    /// <summary>
    /// 项目明细
    /// </summary>
    public class ParaItemListDto
    {
        /// <summary>
        /// 项目代码
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        /// <example></example>
        public string UnitName { get; set; }
    }
}
