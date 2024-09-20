#region 代码备注
//------------------------------------------------------------------------------
// 创建描述: 代码生成器自动创建于 11/06/2020 04:50:47
//
// 功能描述:
//
// 修改描述:
//------------------------------------------------------------------------------
#endregion 代码备注
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;


namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 模块项目
    /// </summary>
    public class ModuleParaItemDto : EntityDto<Guid>
    {


        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        [Required] [StringLength(50)] public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        /// <example></example>
        [Required] [StringLength(80)] public string ModuleName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [Required] [StringLength(20)] public string DeptCode { get; set; }

        /// <summary>
        /// 模块类型：（CANULA：导管，VS：观察项目，IO：出入量）
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string ModuleType { get; set; }

        /// <summary>
        /// 是否出入量
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string IfInOut { get; set; }

        /// <summary>
        /// 模块英文名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(40)] public string Enname { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        [Required] public int SortNum { get; set; }

        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        /// <example></example>
        [Required] public int ValidState { get; set; }

        public List<ParaItemDto> icuParaItemDtos { get; set; }
    }
}
