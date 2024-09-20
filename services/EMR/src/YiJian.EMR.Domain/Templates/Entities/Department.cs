using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.Templates.Entities
{
    /// <summary>
    /// 科室历史记录
    /// </summary>
    [Comment("科室历史记录")]
    public class Department : Entity<Guid>
    {
        /// <summary>
        /// 科室历史记录
        /// </summary>
        private Department()
        {

        }

        /// <summary>
        /// 科室历史记录
        /// </summary> 
        public Department(Guid id, [NotNull] string deptCode, [NotNull] string deptName)
        {
            Id = id;
            DeptCode = Check.NotNullOrWhiteSpace(deptCode,nameof(deptCode),maxLength:32);
            DeptName = Check.NotNullOrWhiteSpace(deptName, nameof(deptName), maxLength: 32);
        }

        /// <summary>
        /// 病区名称
        /// </summary>
        [Comment("科室名称")]
        [Required(ErrorMessage = "科室名称必填"), StringLength(32, ErrorMessage = "科室名称最大长度32字符")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary>
        [Comment("科室名称")]
        [Required(ErrorMessage = "科室名称必填"), StringLength(100, ErrorMessage = "科室名称最大长度50字符")]
        public string DeptName { get; set; }

    }
}
