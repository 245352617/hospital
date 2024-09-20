using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class CreateUpdateIcuPatientScoreDto : EntityDto<Guid>
    {
        /// <summary>
        /// 评分类型名称
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(100)]
        public string ScoreName { get; set; }

        /// <summary>
        /// 类型编码
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(30)]
        public string ScoreCode { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(20)]
        public string PI_ID { get; set; }

        /// <summary>
        /// 病人档案号
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(20)]
        public string Archivsid { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(200)]
        public string Gname { get; set; }

        /// <summary>
        /// 组编号
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(30)]
        public string Gcode { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        /// <example></example>
        [StringLength(10)]
        public decimal? TotalScore { get; set; }

        /// <summary>
        /// 结论  多个结果以|分开
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(200)]
        public string Result { get; set; }

        /// <summary>
        /// 评分时间
        /// </summary>
        /// <example></example>

        public DateTime? ScoreTime { get; set; }

        /// <summary>
        /// 评分人
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(20)]
        public string Creator { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        /// <example></example>

        public DateTime? CreatDate { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(20)]
        public string UpdateId { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        /// <example></example>

        public DateTime? UpdateDate { get; set; }
    }
}
