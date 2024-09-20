using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// 描述：特殊护理记录
    /// 创建人： yangkai
    /// 创建时间：2023/3/31 9:45:53
    /// </summary>
    [Comment("特殊护理记录")]
    public class SpecialNursingRecord : FullAuditedAggregateRoot<Guid>
    {
        public SpecialNursingRecord(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 特殊护理记录
        /// </summary>
        [Comment("特殊护理记录")]
        public string Special { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录Id")]
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 特殊护理记录关联Id 护理记录关联Id 关联导管id 导管护理id 皮肤id等
        /// </summary>
        [Comment("特殊护理记录关联Id")]
        public Guid NursingRelevanceId { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [Comment("事件类型")]
        public EEventType? EventType { get; set; }
    }
}
