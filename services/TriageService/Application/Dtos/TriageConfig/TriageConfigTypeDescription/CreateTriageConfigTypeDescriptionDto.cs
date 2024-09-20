using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class CreateTriageConfigTypeDescriptionDto
    {
        ///<summary>
        /// 分诊设置类型代码
        /// </summary>
        public string TriageConfigTypeCode { get; set; }
        /// <summary>
        /// 分诊设置类型名称
        /// </summary>
        public string TriageConfigTypeName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
