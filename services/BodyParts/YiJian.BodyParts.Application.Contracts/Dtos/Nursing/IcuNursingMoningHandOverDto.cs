using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;


namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 晨会交班表
    /// </summary>
    public class IcuNursingMoningHandOverDto : EntityDto<Guid>
    {

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string PI_ID { get; set; }

        /// <summary>
        /// 主要护理问题
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(2000)] public string NursingProblem { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(2000)] public string PastHistory { get; set; }

        /// <summary>
        /// 症状/体征/化验/检查
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(2000)] public string Assessment { get; set; }

        /// <summary>
        /// 建议措施
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(2000)] public string Reconmmendation { get; set; }

        /// <summary>
        /// 交班时间
        /// </summary>
        /// <example></example>
        public DateTime? HandOverTime { get; set; }
    }
}
