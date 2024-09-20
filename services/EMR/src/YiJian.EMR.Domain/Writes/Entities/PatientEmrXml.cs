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
    /// 患者的电子病历xml文档
    /// </summary>
    [Comment("患者的电子病历xml文档")]
    public class PatientEmrXml : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 患者的电子病历xml文档
        /// </summary>
        private PatientEmrXml()
        { 
        }

        /// <summary>
        /// 患者的电子病历xml文档
        /// </summary> 
        public PatientEmrXml(
            Guid id,
            [NotNull] string emrXml,
            Guid patientEmrId
        )
        {
            Id = id;
            EmrXml = Check.NotNullOrWhiteSpace(emrXml, nameof(emrXml));
            PatientEmrId = patientEmrId; 
        }

        /// <summary>
        /// 电子病历Xml文档
        /// </summary>
        [Comment("电子病历Xml文档")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "电子病历Xml文档不能为空")]
        public string EmrXml { get; set; }

        /// <summary>
        /// 患者电子病历Id
        /// </summary>
        [Comment("患者电子病历Id")]
        [Required(ErrorMessage = "患者电子病历Id不能为空")]
        public Guid PatientEmrId {  get; set;}
         
        /// <summary>
        /// 患者电子病历
        /// </summary>
        public virtual PatientEmr PatientEmr { get;set;}
          
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="emrXml"></param>
        public void Update([NotNull] string emrXml)
        {
            EmrXml = Check.NotNullOrWhiteSpace(emrXml, nameof(emrXml));
        }

    }
}
