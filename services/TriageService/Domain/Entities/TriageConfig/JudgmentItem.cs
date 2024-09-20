using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 判定依据项目
    /// </summary>
    public class JudgmentItem : BaseEntity<Guid>
    {
        public JudgmentItem SetId(Guid id)
        {
            Id = id;
            return this;
        }
        
        /// <summary>
        /// 判定依据主诉分类主键Id
        /// </summary>
        [Description("判定依据主诉分类主键Id")]
        public Guid JudgmentMasterId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Description("级别")]
        public int EmergencyLevel { get; set; }

        /// <summary>
        /// 分诊级别Code
        /// </summary>
        [Description("分诊级别Code")]
        [StringLength(50,ErrorMessage = "{0}的最大长度为{1}")]
        public string TriageLevelCode { get; set; }

        /// <summary>
        /// 分诊级别名称
        /// </summary>
        [Description("分诊级别名称")]
        [StringLength(50,ErrorMessage = "{0}的最大长度为{1}")]
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 分诊依据
        /// </summary>
        [Description("分诊依据")]
        public string ItemDescription { get; set; }
        
        /// <summary>
        /// 是否属于绿色通道 0：不属于 1：属于
        /// </summary>
        [Description("是否属于绿色通道； 0：不属于 1：属于")]
        public int IsGreenRoad { get; set; }

        /// <summary>
        /// 是否启用; 0：不启用； 1：启用
        /// </summary>
        [Description("是否启用; 0：不启用； 1：启用")]
        public int IsEnabled { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [Description("拼音码")]
        public string Py { get; set; }
        
        /// <summary>
        /// 生成拼音码
        /// </summary>
        /// <returns></returns>
        public JudgmentItem GetPy()
        {
            Py = PyHelper.GetFirstPy(ItemDescription);
            return this;
        }
    }
}