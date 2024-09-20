using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 快速开嘱数据集合
    /// </summary>
    public class AllMedicineDto : EntityDto<Guid>
    {
        /// <summary>
        /// 标题名称
        /// </summary> 
        public string Title { get; set; }

        /// <summary>
        /// 是否可以修改标题名称
        /// </summary> 
        public bool CanModify { get; set; }

        /// <summary>
        /// 药品列表
        /// </summary>
        public List<QuickStartQueryDto> Medicines { get; set; }

    }


}
