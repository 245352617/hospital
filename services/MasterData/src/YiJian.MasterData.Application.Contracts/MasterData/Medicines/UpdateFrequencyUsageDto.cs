using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace YiJian.MasterData.MasterData
{

    public class UpdateFrequencyDto
    {
        /// <summary>
        /// 药品主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 默认频次编码
        /// </summary>
        [StringLength(20)]
        [Comment("默认频次编码")]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 默认频次名称
        /// </summary>
        [StringLength(50)]
        [Comment("默认频次名称")]
        public string FrequencyName { get; set; }
    }
    
    public class UpdateFrequencyUsageDto:UpdateFrequencyDto
    {
        /// <summary>
        /// 默认用法编码
        /// </summary>
        [StringLength(20)]
        [Comment("默认用法编码")]
        public string UsageCode { get; set; }

        /// <summary>
        /// 默认用法名称
        /// </summary>
        [StringLength(50)]
        [Comment("默认用法名称")]
        public string UsageName { get; set; }
    }
}