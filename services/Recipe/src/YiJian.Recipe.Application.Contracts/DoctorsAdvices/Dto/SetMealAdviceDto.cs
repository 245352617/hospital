using System;
using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱套餐
    /// </summary>
    public class SetMealAdviceDto
    {
        /// <summary>
        /// 患者信息（用来处理儿童价问题的）
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 医嘱基础信息
        /// </summary>
        public DoctorsAdviceRequestDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        public Guid PackageId { get; set; }

        /// <summary>
        /// 需要筛选的项目Id集合
        /// </summary>
        public List<int> EntryIds { get; set; }
    }
}
