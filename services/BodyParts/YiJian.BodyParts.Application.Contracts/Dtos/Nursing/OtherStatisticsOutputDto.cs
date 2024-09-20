using System;
using System.Collections.Generic;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class OtherStatisticsOutputDto
    {
        /// <summary>
        /// 床号
        /// </summary>
        public string BedNum { get; set; }
        
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatId { get; set; }
        
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatName { get; set; }
        
        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StaticsticDate { get; set; }
        
        /// <summary>
        /// 统计项目及结果
        /// </summary>
        public List<OtherStatisticsItemDto> Items { get; set; }
    }
}