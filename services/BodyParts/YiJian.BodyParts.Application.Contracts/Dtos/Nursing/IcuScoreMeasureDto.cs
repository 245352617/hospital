using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:评分措施表
    /// </summary>
    public class IcuScoreMeasureDto : EntityDto<Guid>
    {
        /// <summary>
        /// 措施项编号
        /// </summary>
        /// <example></example>
        public Guid Pid { get; set; }

        /// <summary>
        /// 护理措施名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(255)] public string Name { get; set; }

        /// <summary>
        /// 措施项名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(255)] public string PName { get; set; }

        /// <summary>
        /// 护理措施编号
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(4)] public string Code { get; set; }

        /// <summary>
        /// 数据类型(0为单选，1为多选）
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(1)] public string DataValue { get; set; }

        public List<IcuScoreMeasureDetailDto> ScoreMeasureDetailDtos { get; set; }

    }
}
