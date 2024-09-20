using YiJian.BodyParts.Application.Contracts.Dtos.Nursing;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:护理评分表
    /// </summary>
    public class IcuScoreStandardDto : EntityDto<Guid>
    {
        /// <summary>
        /// 评分编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 评分名称
        /// </summary>
        /// <example></example>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [CanBeNull]
        [StringLength(300)]
        public string Describe { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        /// <example></example>
        public string Remark { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 能否打印
        /// </summary>
        public bool PrintEnabled { get; set; }


        public List<IcuScoreStandardDetailDto> ScoreStandardDetailDtos { get; set; }

        public List<IcuScoreMeasureDto> ScoreMeasureDtos { get; set; }

        /// <summary>
        /// 评分结论数据
        /// </summary>
        public List<IcuScoreStandardTipsDto> ScoreStandardTipsDtos { get; set; }

    }
}
