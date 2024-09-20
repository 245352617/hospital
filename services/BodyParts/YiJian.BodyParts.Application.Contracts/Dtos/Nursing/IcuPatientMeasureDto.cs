using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:病人评分措施表
    /// </summary>
    public class IcuPatientMeasureDto : EntityDto<Guid>
    {
        /// <summary>
        /// 评分类型名称
        /// </summary>
        /// <example></example>
        [Required]
        public string ScoreName { get; set; }

        /// <summary>
        /// 评分类型编码
        /// </summary>
        /// <example></example>
        public string ScoreCode { get; set; }

        /// <summary>
        /// 措施名称
        /// </summary>
        /// <example></example>
        public string MeasureName { get; set; }

        /// <summary>
        /// 措施编码
        /// </summary>
        /// <example></example>
        public string MeasureCode { get; set; }

        /// <summary>
        /// 措施明细名称
        /// </summary>
        /// <example></example>
        public string MeasureDetailName { get; set; }

        /// <summary>
        /// 措施明细编码
        /// </summary>
        /// <example></example> 
        public string MeasureDetailCode { get; set; }

        /// <summary>
        /// 评分记录id
        /// </summary>
        /// <example></example>
        public Guid? Pid { get; set; }

        public List<IcuScoreMeasureDetailDto> ScoreMeasureDetailDtos { get; set; }

    }
}
