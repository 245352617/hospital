using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:模块参数
    /// </summary>
    public class IcuParaModule : Entity<Guid>
    {
        public IcuParaModule() { }

        public IcuParaModule(Guid id) : base(id) { }


        /// <summary>
        /// 模块代码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [StringLength(80)]
        [Required]
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块显示名称
        /// </summary>
        [StringLength(80)]
        public string DisplayName { get; set; }


        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string ModuleType { get; set; }

        /// <summary>
        /// 是否血流内导管
        /// </summary>
        public bool IsBloodflow { get; set; }

        /// <summary>
        /// 模块拼音
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        public string Enname { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [StringLength(10)]
        [Required]
        public int SortNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        [Required]
        public int ValidState { get; set; }
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
