using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:字典-药品频次
    /// </summary>
    public class CreateUpdateDictFrequencyDto : EntityDto<Guid>
    {
        /// <summary>
        /// 频次代码
        /// </summary>
        /// <example></example>
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次名称
        /// </summary>
        /// <example></example>
        public string FrequencyName { get; set; }

        /// <summary>
        /// 频次全称
        /// </summary>
        /// <example></example>
        public string FrequencyFullName { get; set; }

        /// <summary>
        /// 每日次数
        /// </summary>
        /// <example></example>
        public string FrequencyTimes { get; set; }

        /// <summary>
        /// 频次单位（W-每周，D-每天）
        /// </summary>
        /// <example></example>
        public string FrequencyUnit { get; set; }

        /// <summary>
        /// 频次每天执行时间
        /// </summary>
        /// <example></example>
        public string ExecuteDayTime { get; set; }

        /// <summary>
        /// 频次说明
        /// </summary>
        /// <example></example>
        public string FrequencyText { get; set; }

        /// <summary>
        /// his代码
        /// </summary>
        public string HisCode { get; set; }
        
        /// <summary>
        /// 执行间隔，如果FrequencyUnit为D，表示间隔天数，如果FrequencyUnit为W，表示间隔周
        /// </summary>
        public string Interval { get; set; }
    }
}
