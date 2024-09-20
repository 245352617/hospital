using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.Templates.Entities
{
    /// <summary>
    /// 病区
    /// </summary>
    [Comment("病区")]
    public class InpatientWard : Entity<Guid>
    {
        /// <summary>
        /// 病区名称
        /// </summary>
        [Comment("病区名称")]
        [Required(ErrorMessage = "病区名称必填"), StringLength(100, ErrorMessage = "病区名称最大长度50字符")]
        public string WardName { get; set; }

    }
}
