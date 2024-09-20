using SamJan.MicroService.PreHospital.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 病人评分管理
    /// </summary>
    public class ScoreManage : BaseEntity<Guid>
    {
        public ScoreManage SetId(Guid id)
        {
            Id = id;
            return this;
        }
        /// <summary>
        /// 评分名称
        /// </summary>
        [Description("评分名称")]
        [StringLength(50)]
        public string ScoreName { get; set; }
        
        /// <summary>
        /// 评分类型
        /// </summary>
        [Description("评分类型")]
        [StringLength(20)]
        public string ScoreType { get; set; }
        
        /// <summary>
        /// 动态库名称
        /// </summary>
        [Description("动态库名称")]
        [StringLength(50)]
        public string DynamicLibraryName { get; set; }
        
        /// <summary>
        /// 类名
        /// </summary>
        [Description("类名")]
        [StringLength(50)]
        public string ClassName { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public bool IsEnable { get; set; }

    }
}
