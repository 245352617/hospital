using System;

namespace YiJian.BodyParts.Application.Contracts.Dtos.System.SysNotice
{
    public class SysNoticeInfoDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 系统公告标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 系统公告内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 状态 0未发送 1已发送
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 提醒类型 1系统公告
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否确认
        /// </summary>
        public bool HasConfirm { get; set; } = false;
    }
}
