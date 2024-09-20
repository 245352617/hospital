using System;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 医嘱状态变更实体
    /// </summary>
    public class ModifyAdviceStatusDto
    {
        /// <summary>
        /// 医嘱Id（UUID）
        /// </summary>
        public Guid DoctorAdviceId { get; set; }

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费
        /// </summary>
        public ERecipeStatus Status { get; set; }

    }
}
