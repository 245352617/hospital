using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
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

        public List<ParaItemListDto> paraItemListDtos { get; set; }
    }


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
