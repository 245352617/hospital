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
    public class TriageConfigTypeDescription : BaseEntity<Guid>
    {
        /// <summary>
        /// 分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TriageConfigTypeDescription SetId(Guid id)
        {
            Id = id;
            return this;
        }
        ///<summary>
        /// 分诊设置类型代码
        /// </summary>
        [StringLength(20)]
        [Description("分诊设置类型代码")]
        public string TriageConfigTypeCode { get; set; }
        
        /// <summary>
        /// 分诊设置类型名称
        /// </summary>
        [StringLength(20)]
        [Description("分诊设置类型名称")]
        public string TriageConfigTypeName { get; set; }
    }
}
