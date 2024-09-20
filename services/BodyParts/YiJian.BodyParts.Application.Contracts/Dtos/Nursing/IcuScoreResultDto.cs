using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;


namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:
    /// </summary>
    public class IcuScoreResultDto : EntityDto<Guid>
    {


        /// <summary>
        /// 评分名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(200)] public string Name { get; set; }

        /// <summary>
        /// 评分编号
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string Code { get; set; }

        /// <summary>
        /// 父名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(200)] public string Pname { get; set; }

        /// <summary>
        /// 父编码
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string Pcode { get; set; }

        /// <summary>
        /// 评分值
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(200)] public string ScoreResult { get; set; }

        /// <summary>
        /// 条件.等于，大于，大于等于，小于，小于等于，区间（多条件以|分隔）
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string Condition { get; set; }

        /// <summary>
        /// 条件值（多条件以|分隔）
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(100)] public string ConditionValue { get; set; }

        /// <summary>
        /// 评分模块ID
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

    }
}
