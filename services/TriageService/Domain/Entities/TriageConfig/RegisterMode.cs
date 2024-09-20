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
    /// 挂号模式
    /// Author: ywlin
    /// Date: 2021-12-07
    /// </summary>
    public class RegisterMode : BaseEntity<Guid>
    {
        public RegisterMode SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 挂号模式代码
        /// </summary>
        [StringLength(50)]
        [Description("挂号模式代码")]
        public string Code { get; set; }

        /// <summary>
        /// 挂号模式名称
        /// </summary>
        [StringLength(50)]
        [Description("挂号模式名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public bool IsActive { get; set; }
    }
}
