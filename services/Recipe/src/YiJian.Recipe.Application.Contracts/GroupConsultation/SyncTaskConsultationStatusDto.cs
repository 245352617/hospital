using System;
using YiJian.Recipes.GroupConsultation;

namespace YiJian.GroupConsultation
{
    /// <summary>
    /// 同步任务单会诊状态Dto
    /// </summary>
    public class SyncTaskConsultationStatusDto
    {
        /// <summary>
        /// EmrPatientInfoId
        /// </summary>
        public Guid PIId { get; set; }

        /// <summary>
        /// 会诊状态
        /// 全部 = -1,
        /// 待开始 = 0,
        /// 已开始 = 1,
        /// 已完结 = 2,
        /// 已取消 = 3
        /// </summary>
        public GroupConsultationStatus Status { get; set; }
    }
}
