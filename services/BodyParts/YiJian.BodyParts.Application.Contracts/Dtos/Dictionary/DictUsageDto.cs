#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  11/12/2020 07:20:16
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
    /// 表:字典-药品用法
    /// </summary>
    public class DictUsageDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用法代码
        /// </summary>
        /// <example></example>
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        /// <example></example>
        public string UsageName { get; set; }

        /// <summary>
        /// 用法全称
        /// </summary>
        /// <example></example>
        public string UsageFullName { get; set; }

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
        /// 所属分组
        /// </summary>
        /// <example></example>
        public string NursingType { get; set; }

        /// <summary>
        /// 护理记录打印名称
        /// </summary>
        /// <example></example>
        public string NuringPrintName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        /// <example></example>
        public string PinYin { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        public int SortNum { get; set; }

        /// <summary>
        /// 有效状态(1-有效，0-无效)
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }

        /// <summary>
        /// 是否单次
        /// </summary>
        /// <example></example>
        public bool Single { get; set; }

        /// <summary>
        /// 是否需要提取
        /// </summary>
        public bool Extract { get; set; }
    }
}
