using System;
using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 判定依据主诉分类Dto
    /// </summary>
    public class JudgmentMasterDto
    {
        /// <summary>
        /// Id 
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 判定依据科室分类主键Id
        /// </summary>
        public Guid JudgmentTypeId { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string Py { get; set; }

        /// <summary>
        /// 是否启用：0：不启用；1：启用
        /// </summary>
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 判定依据项目
        /// </summary>
        public List<JudgmentItemDto> Children { get; set; }
    }
}