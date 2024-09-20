using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{ 
    /// <summary>
    /// 获取电子病历模板请求参数
    /// </summary>
    public class CatalogueRootRequestDto
    {
        /// <summary>
        /// 模板类型，0=通用，1=科室，2=个人
        /// </summary>
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 目录层级level
        /// </summary>
        public int Lv { get; set; }

        /// <summary>
        /// 科室代码[科室模板]
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 医生代码[个人模板]
        /// </summary>
        public string DoctorCode { get; set; }
         
        /// <summary>
        /// 电子文书分类(0=电子病历,1=文书)
        /// </summary> 
        public EClassify Classify { get; set; } = EClassify.EMR;

        /// <summary>
        /// 是否包含已删除的
        /// </summary>
        public bool ContainDeleted { get; set; } = true;
    }
}
