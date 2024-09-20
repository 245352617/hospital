using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 统计Dto
    /// </summary>
    public class CanulaTypeDto
    {
        /// <summary>
        /// 类型（呼吸导管：VapAttack，中央导管：CrbsiAttack，尿管：CautiAttack）
        /// </summary>
        public string CanulaType { get; set; }

        /// <summary>
        /// 比率
        /// </summary>
        public List<InfectionDto> infectionDtos { get; set; } = new List<InfectionDto>();
    }

    public class InfectionDto
    {
        /// <summary>
        /// 每月起始时间
        /// </summary>
        public DateTime startMonth { get; set; }

        /// <summary>
        /// 比率
        /// </summary>
        public Decimal Rate { get; set; }

        /// <summary>
        /// 分子
        /// </summary>
        public Decimal InfectionNumber { get; set; }

        /// <summary>
        /// 分母
        /// </summary>
        public Decimal Days { get; set; }
    }
}
