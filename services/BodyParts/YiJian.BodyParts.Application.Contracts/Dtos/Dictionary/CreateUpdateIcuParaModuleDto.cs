using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:模块参数
    /// </summary>
    public class CreateUpdateIcuParaModuleDto : EntityDto<Guid>
    {
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
        /// 模块显示名称
        /// </summary>
        /// <example></example>
        public string DisplayName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块类型:(CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO，RR：呼吸监测)
        /// </summary>
        /// <example></example>
        public string ModuleType { get; set; }

        /// <summary>
        /// 是否血流内导管
        /// </summary>
        public bool IsBloodflow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        public int SortNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        
        /// <summary>
        /// 风险级别 默认空，1低危 2中危 3高危
        /// </summary>
        public string RiskLevel { get; set; }

        /// <summary>
        /// 部位编号
        /// </summary>
        public string PartNumber { get; set; }
    }
}
