using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{
    public class IcuQCStatisticsDto
    {

        /// <summary>
        /// 参数编码，主键唯一
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 质控项目Code
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 质控项目名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 统计项分子
        /// </summary>
        public double Molecule { get; set; }
        /// <summary>
        /// 统计项分母
        /// </summary>
        public double Denominator { get; set; }

        /// 统计年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 统计月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 统计日
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StatisticalTime { get; set; }
    }
}
