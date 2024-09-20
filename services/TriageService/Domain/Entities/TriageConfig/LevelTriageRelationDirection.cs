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
    public class LevelTriageRelationDirection : BaseEntity<Guid>
    {
        public LevelTriageRelationDirection SetId(Guid id)
        {
            Id = id;
            return this;
        }
        /// <summary>
        /// 分诊去向级别代码
        /// </summary>
        [StringLength(50)]
        [Description("分诊去向级别代码")]
        public string LevelTriageDirectionCode { get; set; }

        /// <summary>
        /// 分诊级别代码  字典表获取
        /// </summary>
        [StringLength(50)]
        [Description("分诊级别代码")]
        public string TriageLevelCode { get; set; }

        /// <summary>
        /// 分诊去向code
        /// </summary>
        [StringLength(50)]
        [Description("分诊去向code")]
        public string TriageDirectionCode { get; set; }

        /// <summary>
        /// 其他去向code
        /// </summary>
        [StringLength(50)]
        [Description("其他去向code")]
        public string OtherDirectionCode { get; set; }

    }
}
