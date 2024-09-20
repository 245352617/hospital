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
    public class IcuScoreStandardDetailDto : EntityDto<Guid>
    {
        /// <summary>
        /// 明细项名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(200)] public string Name { get; set; }

        /// <summary>
        /// 明细项编号
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
        /// 组名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(200)] public string Gname { get; set; }

        /// <summary>
        /// 组编号
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string Gcode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <example></example>
        [CanBeNull][StringLength(200)] public string Describe { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        /// <example></example>
        public decimal? Score { get; set; }

        /// <summary>
        /// 条件.等于，大于，大于等于，小于，小于等于，区间（多条件以|分隔
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string Condition { get; set; }

        /// <summary>
        /// 条件值（多条件以|分隔）
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(100)] public string ConditionValue { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string DataValue { get; set; }

        /// <summary>
        /// 评分项id
        /// </summary>
        /// <example></example>
        public Guid Pid { get; set; }

        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid? DependId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        public List<IcuScoreResultDto> ScoreResultDtoDtos { get; set; }

    }
}
