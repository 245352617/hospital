using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class IcuScoreStandardTipsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 提醒内容
        /// </summary>
        [StringLength(200)]
        public string Tips { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        [StringLength(20)]
        public string Min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        [StringLength(20)]
        public string Max { get; set; }
        /// <summary>
        /// 评分ID
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 评分频次
        /// </summary>
        public string FrequencyName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int SortNum { get; set; }
    }
}
