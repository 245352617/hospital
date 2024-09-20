#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  11/10/2020 08:08:45
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
    /// 表:字典-通用业务
    /// </summary>
    public class DictDto : EntityDto<Guid>
    {
        /// <summary>
        /// 参数代码
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        /// <example></example>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        /// <example></example>
        public string DictValue { get; set; }

        /// <summary>
        /// 字典值说明
        /// </summary>
        /// <example></example>
        public string DictDesc { get; set; }

        /// <summary>
        /// 上级代码
        /// </summary>
        /// <example></example>
        public string ParentId { get; set; }

        /// <summary>
        /// 字典标准（国标、自定义）
        /// </summary>
        /// <example></example>
        public string DictStandard { get; set; }

        /// <summary>
        /// HIS对照代码
        /// </summary>
        /// <example></example>
        public string HisCode { get; set; }

        /// <summary>
        /// HIS对照
        /// </summary>
        /// <example></example>
        public string HisName { get; set; }

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
        /// 排序
        /// </summary>
        /// <example></example>
        public int SortNum { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        /// <example></example>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <example></example>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否有效（1-是，0-否）
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }
    }
}
