using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:人体图-编号字典
    /// </summary>
    public class DictCanulaPartDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        /// <example></example>
        public string ModuleName { get; set; }

        /// <summary>
        /// 部位名称
        /// </summary>
        /// <example></example>
        public string PartName { get; set; }

        /// <summary>
        /// 部位编号
        /// </summary>
        /// <example></example>
        public string PartNumber { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 风险级别 默认空，1低危 2中危 3高危
        /// 皮肤分期 默认空  1-1期 2-2期 3-3期 4-4期 5-深部组织损伤 6-不可分期
        /// </summary>
        public string RiskLevel { get; set; }
    }
}
