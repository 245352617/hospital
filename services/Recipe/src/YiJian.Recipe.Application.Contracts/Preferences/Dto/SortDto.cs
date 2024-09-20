using System;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 快速开嘱排序信息
    /// </summary>
    public class SortDto
    {
        /// <summary>
        /// 快速开嘱医嘱Id
        /// </summary>
        public Guid QuickStartAdviceDtoId { get; set; }

        /// <summary>
        /// 排序序号（前端排序好）
        /// </summary>
        public int Sort { get; set; }

    }

}
