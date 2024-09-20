using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{
    /// <summary>
    /// 质控月统计中间表
    /// </summary>
    public class IcuQCMonthStatisticsDto
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
        public string Molecule { get; set; }
        /// <summary>
        /// 统计项分母
        /// </summary>
        public string Denominator { get; set; }

        /// 统计年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 统计月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 统计年月
        /// </summary>
        public string StatisticalTime { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [Required]
        public string DeptName { get; set; }


    }
}
