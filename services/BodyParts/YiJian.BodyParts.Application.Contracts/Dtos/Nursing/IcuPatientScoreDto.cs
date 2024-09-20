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
    /// 表:患者评分表
    /// </summary>
    public class IcuPatientScoreDto : EntityDto<Guid>
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
        /// 住院号
        /// </summary>
        /// <example></example>
        [Required] 
        public string PI_ID { get; set; }

        /// <summary>
        /// 病人档案号
        /// </summary>
        /// <example></example>
        public string Archivsid { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        /// <example></example>
        public string Gname { get; set; }

        /// <summary>
        /// 组编号
        /// </summary>
        /// <example></example>
        public string Gcode { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        /// <example></example>
        public decimal? TotalScore { get; set; }

        /// <summary>
        /// 结论  多个结果以|分开
        /// </summary>
        /// <example></example>
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
        public string Creator { get; set; }

        /// <summary>
        /// 文书管理-危重患者转运评估单，转运时间（只诊断转运评估单，该字段不可为空）
        /// </summary>
        public DateTime? TransportTime { get; set; }


        /// <summary>
        /// 文书管理-危重患者转运评估单，备注
        /// </summary>
        [CanBeNull]
        [StringLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 文书管理-危重患者转运评估单签名
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string SignatureId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <example></example>
        public DateTime? CreatDate { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        /// <example></example>
        public string UpdateId { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        /// <example></example>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string SignImage { get; set; }


        public List<IcuPatientScoreDetailDto> icuPatientScoreDetailDtos { get; set; }
        public List<IcuPatientMeasureDto> icuPatientScoreMeasureDtos { get; set; }

        public IcuPatientScoreOtherDto icuPateintScoreOtherDto { get; set; }
    }
}
