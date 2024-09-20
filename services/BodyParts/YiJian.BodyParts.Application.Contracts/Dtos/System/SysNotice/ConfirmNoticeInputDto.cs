using System;

namespace YiJian.BodyParts.Application.Contracts.Dtos.System.SysNotice
{
    public class ConfirmNoticeInputDto
    {
        /// <summary>
        /// 系统公告Id
        /// </summary>
        public Guid NoticeId { get; set; }

        /// <summary>
        /// 确认人工号
        /// </summary>
        public string StaffCode { get; set; }

        /// <summary>
        /// 确认人姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 提醒类型 1系统公告
        /// </summary>
        public int Type { get; set; }
    }
}
