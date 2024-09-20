using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:科室公告表
    /// </summary>
    public class DeptNoticeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <example></example>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 停止时间
        /// </summary>
        /// <example></example>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        /// <example></example>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        /// <example></example>
        public string Content { get; set; }
    }
}
