using System.Collections.Generic;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 快速开嘱的信息集合
    /// </summary>
    public class QuickStartDto
    {
        /// <summary>
        /// 标题名称
        /// </summary> 
        public string Title { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 快速开嘱药品
        /// </summary> 
        public List<QuickStartSampleDto> Medicines { get; set; } = new List<QuickStartSampleDto>();


    }

}
