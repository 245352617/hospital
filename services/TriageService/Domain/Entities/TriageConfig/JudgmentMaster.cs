using System;
using System.Collections.Generic;
using System.ComponentModel;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 判定依据主诉分类
    /// </summary>
    public class JudgmentMaster : BaseEntity<Guid>
    {
        public JudgmentMaster SetId(Guid id)
        {
            Id = id;
            return this;
        }
        
        /// <summary>
        /// 判定依据科室分类主键Id
        /// </summary>
        [Description("判定依据科室分类主键Id")]
        public Guid JudgmentTypeId { get; set; }
        
        /// <summary>
        /// 拼音码
        /// </summary>
        [Description("拼音码")]
        public string Py { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        [Description("主诉名称")]
        public string ItemDescription { get; set; }
        
        /// <summary>
        /// 是否启用; 0：不启用； 1：启用
        /// </summary>
        [Description("是否启用; 0：不启用； 1：启用")]
        public int IsEnabled { get; set; }

        /// <summary>
        /// 判定依据项目
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public ICollection<JudgmentItem> JudgmentItems { get; set; }
        
        /// <summary>
        /// 生成拼音码
        /// </summary>
        /// <returns></returns>
        public JudgmentMaster GetPy()
        {
            Py = PyHelper.GetFirstPy(ItemDescription);
            return this;
        }
    }
}