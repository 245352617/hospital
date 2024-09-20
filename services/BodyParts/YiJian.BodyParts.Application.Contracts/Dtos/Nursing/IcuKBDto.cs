using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:知识库
    /// </summary>
    public class IcuKBDto : EntityDto<Guid>
    {


        /// <summary>
        /// 知识库代码
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string KBCode { get; set; }

        /// <summary>
        /// 分类代码
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(10)] public string TypeCode { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string TypeName { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(600)] public string KBConditon { get; set; }

        /// <summary>
        /// 提醒内容
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(2000)] public string KBTips { get; set; }

        /// <summary>
        /// 知识库
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(2000)] public string KBText { get; set; }
    }
}
