using System;
using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 创建或更新判定依据主诉分类Dto
    /// </summary>
    public class CreateOrUpdateJudgmentMasterDto
    {
        /// <summary>
        /// 判定依据类型Id
        /// </summary>
        public Guid JudgmentTypeId { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        public string ItemName { get; set; }
        
        /// <summary>
        /// 是否启用：false：不启用；true：启用
        /// </summary>
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}