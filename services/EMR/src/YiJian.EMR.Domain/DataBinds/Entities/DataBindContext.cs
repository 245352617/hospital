using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.EMR.Enums;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.DataBinds.Entities
{
    /// <summary>
    /// 电子病历、文书绑定的数据上下文
    /// </summary>
    [Comment("电子病历、文书绑定的数据上下文")]
    public class DataBindContext : FullAuditedAggregateRoot<Guid>
    {
        private DataBindContext()
        {

        }

        public DataBindContext(
            Guid id,

            string visitNo,
            string orgCode, 
            string registerSerialNo,

            Guid pi_id, 
            string patientId, 
            string patientName, 
            string writerId,
            string writerName,
            EClassify classify,
            Guid patientEmrId)
        { 
            Id = id;

            VisitNo = visitNo;
            OrgCode = orgCode; 
            RegisterSerialNo = registerSerialNo; 

            PI_ID = pi_id;
            PatientId = patientId;
            PatientName = patientName;
            WriterId = writerId;
            WriterName = writerName;
            Classify = classify;
            PatientEmrId = patientEmrId;
        }


        /// <summary>
        /// 就诊号
        /// </summary>
        [Comment("就诊号")]
        [Required, StringLength(32)]
        public string VisitNo { get; set; }
         
        /// <summary>
        /// 流水号
        /// </summary>
        [Comment("流水号")]
        [Required, StringLength(32)]
        public string RegisterSerialNo { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        [Comment("机构ID")]
        [Required, StringLength(100)]
        public string OrgCode { get; set; }
         
        /// <summary>
        /// 患者唯一Id
        /// </summary>
        [Comment("患者唯一Id")]
        [Required]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者Id")]
        [Required,StringLength(32)]
        public string PatientId { get; set; }
          
        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [Required, StringLength(50)]
        public string PatientName { get; set; }
          
        /// <summary>
        /// 录入人Id
        /// </summary>
        [Comment("录入人Id")]
        [StringLength(32)]
        public string WriterId { get; set; }

        /// <summary>
        /// 录入人名称
        /// </summary>
        [Comment("录入人名称")]
        [StringLength(30)]
        public string WriterName { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）默认电子病历
        /// </summary>
        [Comment("电子文书分类（0=电子病历，1=文书）")]
        [Required]
        public EClassify Classify { get; set; } = EClassify.EMR;

        /// <summary>
        /// 患者电子病历Id
        /// </summary>
        [Comment("患者电子病历Id")]
        [Required(ErrorMessage = "患者电子病历Id不能为空")]
        public Guid PatientEmrId { get; set; }

        /// <summary>
        /// 患者电子病历
        /// </summary>
        public virtual PatientEmr PatientEmr { get; set; }

        /// <summary>
        /// 当前表达维护的数据集合
        /// </summary>
        public virtual List<DataBindMap> DataBindMaps { get; set; } = new List<DataBindMap>();

        public void Update(
            string visitNo,
            string orgCode, 
            string registerSerialNo,
            string patientId,
            string patientName,
            string writerId,
            string writerName,
            EClassify classify)
        {
            VisitNo = visitNo;
            OrgCode = orgCode; 
            RegisterSerialNo = registerSerialNo;

            PatientId = patientId;
            PatientName = patientName;
            WriterId = writerId;
            WriterName = writerName;
            Classify = classify;
        }

    }
}
