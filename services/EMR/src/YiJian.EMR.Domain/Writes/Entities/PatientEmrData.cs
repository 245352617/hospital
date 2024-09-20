using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.Writes.Entities
{
    /// <summary>
    /// 绑定的电子病历提取的数据
    /// </summary>
    [Comment("绑定的电子病历提取的数据")]
    public class PatientEmrData : Entity<Guid>
    {
        private PatientEmrData()
        {

        }
         
        public PatientEmrData(Guid id, [NotNull]string name, [NotNull] string value, Guid patientEmrXmlId)
        {
            Id = id;
            Name = Check.NotNullOrWhiteSpace(name,nameof(name),maxLength:50);
        }

        /// <summary>
        /// 电子病历表单ID
        /// </summary>
        public string XInputField { get; set; }

        /// <summary>
        /// 绑定的字段
        /// </summary>
        [Comment("绑定的字段")]
        [Required,StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 绑定的字段描述
        /// </summary>
        [Comment("绑定的字段描述")]
        [Required, StringLength(50)]
        public string Text { get; set; }

        /// <summary>
        /// 医生书写的内容(写/选择/勾选...的内容[json](文本，数值，单选项，数字，日历...))
        /// </summary>
        [Comment("书写的内容")]
        [Required, StringLength(500)]
        public string InnerValue { get; set; }

        /// <summary>
        /// 电子病历XML的Id
        /// </summary>
        [Comment("电子病历XML的Id")]
        public Guid PatientEmrXmlId { get; set; }

    }
}
