using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 判定依据科室分类
    /// </summary>
    public class JudgmentType : BaseEntity<Guid>
    {
        public JudgmentType SetId(Guid id)
        {
            Id = id;
            Py = PyHelper.GetFirstPy(DeptName);
            return this;
        }

        /// <summary>
        /// 科室分类名称
        /// </summary>
        [Description("科室分类名称")]
        public string DeptName { get; set; }

        /// <summary>
        /// 对应科室
        /// </summary>
        [Description("对应科室")]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [Description("拼音码")]
        [Column(TypeName = "varchar(50)")]
        public string Py { get; set; }
        
        /// <summary>
        /// 是否启用; 0：不启用； 1：启用
        /// </summary>
        [Description("是否启用; 0：不启用； 1：启用")]
        public int IsEnabled { get; set; }

        /// <summary>
        /// 判定依据主诉分类
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public ICollection<JudgmentMaster> JudgmentMasters { get; set; }

        /// <summary>
        /// 生成拼音码
        /// </summary>
        /// <returns></returns>
        public JudgmentType GetPy()
        {
            Py = PyHelper.GetFirstPy(DeptName);
            return this;
        }
    }
}