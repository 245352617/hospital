using System;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 描述：特殊护理记录Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/31 15:22:42
    /// </summary>
    public class SpecialNursingRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 特殊护理记录
        /// </summary>
        public string Special { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 特殊护理记录关联Id 护理记录关联Id 关联导管id 导管护理id 皮肤id等
        /// </summary>
        public Guid NursingRelevanceId { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public EEventType? EventType { get; set; }

        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
