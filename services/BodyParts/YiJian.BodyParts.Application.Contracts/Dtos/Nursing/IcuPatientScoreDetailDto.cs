using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 评分详情表
    /// </summary>

    public class IcuPatientScoreDetailDto : EntityDto<Guid>
    {
        /// <summary>
        /// 评分类型名称
        /// </summary>
        /// <example></example>
        [Required]
        public string ScoreName { get; set; }

        /// <summary>
        /// 类型编码
        /// </summary>
        /// <example></example>
        public string ScoreCode { get; set; }

        /// <summary>
        /// 评分类型明细项名称
        /// </summary>
        /// <example></example>
        [Required]
        public string ScoreDetailName { get; set; }

        /// <summary>
        /// 评分明细项类型编码
        /// </summary>
        /// <example></example>
        public string ScoreDetailCode { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        /// <example></example>
        public string Score { get; set; }

        /// <summary>
        /// 明细项答案
        /// </summary>
        /// <example></example>
        public string Answer { get; set; }

        /// <summary>
        /// 评分项Id
        /// </summary>
        /// <example></example>
        [Required]
        public Guid Pid { get; set; }

    }
}
