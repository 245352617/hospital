using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 检查项
    /// </summary>
    [Comment("检查项")]
    public class Pacs : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 检查项
        /// </summary>
        private Pacs()
        {

        }

        /// <summary>
        /// 检查项
        /// </summary>
        /// <param name="id"></param>
        public Pacs(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 检查项
        /// </summary>
        public Pacs(Guid id,
            string catalogCode,
            string catalogName,
            string firstCatalogCode,
            string firstCatalogName,
            string clinicalSymptom,
            string medicalHistory,
            string partCode,
            string partName,
            string catalogDisplayName,
            DateTime? reportTime,
            string reportDoctorCode,
            string reportDoctorName,
            bool hasReport,
            bool isEmergency,
            bool isBedSide,
            Guid doctorsAdviceId, string addCard, string guideCode, string guideName, string examTitle, string reservationPlace, string templateId)
        {
            Id = id;
            CatalogCode = catalogCode;
            CatalogName = catalogName;
            FirstCatalogCode = firstCatalogCode;
            FirstCatalogName = firstCatalogName;
            ClinicalSymptom = clinicalSymptom;
            MedicalHistory = medicalHistory;
            PartCode = partCode;
            PartName = partName;
            CatalogDisplayName = catalogDisplayName;
            ReportTime = reportTime;
            ReportDoctorCode = reportDoctorCode;
            ReportDoctorName = reportDoctorName;
            HasReport = hasReport;
            IsEmergency = isEmergency;
            IsBedSide = isBedSide;
            DoctorsAdviceId = doctorsAdviceId;
            AddCard = addCard;
            GuideCode = guideCode;
            GuideName = guideName;
            ExamTitle = examTitle;
            ReservationPlace = reservationPlace;
            TemplateId = templateId;
        }

        /// <summary>
        /// 检查目录编码
        /// </summary>
        [Comment("检查类别编码")]
        [Required, StringLength(20)]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检查目录名称
        /// </summary>
        [Comment("检查类别")]
        [Required, StringLength(100)]
        public string CatalogName { get; set; }
        /// <summary>
        /// 一级检查目录编码
        /// </summary>
        [Comment("一级检查类别编码")]
        [StringLength(20)]
        public string FirstCatalogCode { get; set; }

        /// <summary>
        /// 一级检查目录名称
        /// </summary>
        [Comment("一级检查目录名称")]
        [StringLength(100)]
        public string FirstCatalogName { get; set; }
        /// <summary>
        /// 临床症状
        /// </summary>
        [Comment("临床症状")]
        [StringLength(2000)]
        public string ClinicalSymptom { get; set; }

        /// <summary>
        /// 病史简要
        /// </summary>
        [Comment("病史简要")]
        [StringLength(2000)]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 检查部位编码
        /// </summary>
        [Comment("检查部位编码")]
        [StringLength(20)]
        public string PartCode { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        [Comment("检查部位")]
        [StringLength(50)]
        public string PartName { get; set; }

        /// <summary>
        /// 目录描述名称 例如心电图申请单、超声申请单
        /// </summary>
        [Comment("分类类型名称 例如心电图申请单、超声申请单")]
        [StringLength(100)]
        public string CatalogDisplayName { get; set; }

        /// <summary>
        /// 出报告时间
        /// </summary>
        [Comment("出报告时间")]
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 确认报告医生编码
        /// </summary>
        [Comment("确认报告医生编码")]
        [StringLength(20)]
        public string ReportDoctorCode { get; set; }

        /// <summary>
        /// 确认报告医生
        /// </summary>
        [Comment("确认报告医生")]
        [StringLength(50)]
        public string ReportDoctorName { get; set; }

        /// <summary>
        /// 报告标识
        /// </summary>
        [Comment("报告标识")]
        public bool HasReport { get; set; }

        /// <summary>
        /// 是否紧急
        /// </summary>
        [Comment("是否紧急")]
        public bool IsEmergency { get; set; }

        /// <summary>
        /// 是否在床旁
        /// </summary>
        [Comment("是否在床旁")]
        public bool IsBedSide { get; set; }

        /// <summary>
        /// 附加卡片类型  
        /// 12.TCT细胞学检查申请单 
        /// 11.病理检验申请单 
        /// 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用 
        /// </summary>
        [StringLength(50)]
        [Comment("附加卡片类型: 12.TCT细胞学检查申请单 ,11.病理检验申请单,16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用")]
        public string AddCard { get; set; }
        /// <summary>
        /// 指引ID 关联 ExamNote表code
        /// </summary>
        [StringLength(50)]
        [Comment("指引Id 关联 ExamNote表code")]
        public string GuideCode { get; set; }

        /// <summary>
        /// 指引名称 关联 ExamNote表code
        /// </summary>
        [StringLength(2000)]
        [Comment("指引名称 关联 ExamNote表name")]
        public string GuideName { get; set; }

        /// <summary>
        /// 检查单单名标题
        /// </summary>
        [Comment("检查单单名标题")]
        public string ExamTitle { get; set; }

        /// <summary>
        /// 预约地点
        /// </summary>
        [Comment("预约地点")]
        public string ReservationPlace { get; set; }

        /// <summary>
        /// 打印模板Id
        /// </summary>
        [Comment("打印模板Id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// 检查状态
        /// </summary>
        [Comment("检查状态")]
        public string PacsStatus { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        [NotMapped]
        public virtual DoctorsAdvice DoctorsAdvice { get; set; }

        /// <summary>
        /// 检查小项集合
        /// </summary>
        public virtual List<PacsItem> PacsItems { get; set; } = new List<PacsItem>();

        /// <summary>
        /// 项目code
        /// </summary>
        [NotMapped]
        public string ProjectCode { get; set; }

        /// <summary>
        /// 克隆的时候需要重新设置关联关系
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorsAdviceId"></param>
        public void ResetId([NotNull] Guid id, [NotNull] Guid doctorsAdviceId)
        {
            Id = id;
            DoctorsAdviceId = doctorsAdviceId;
            DoctorsAdvice = null;
            PacsItems = null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update(Pacs obj)
        {
            CatalogCode = obj.CatalogCode;
            CatalogName = obj.CatalogName;
            FirstCatalogCode = obj.FirstCatalogCode;
            FirstCatalogName = obj.FirstCatalogName;
            ClinicalSymptom = obj.ClinicalSymptom;
            MedicalHistory = obj.MedicalHistory;
            PartCode = obj.PartCode;
            PartName = obj.PartName;
            CatalogDisplayName = obj.CatalogDisplayName;
            ReportTime = obj.ReportTime;
            ReportDoctorCode = obj.ReportDoctorCode;
            ReportDoctorName = obj.ReportDoctorName;
            HasReport = obj.HasReport;
            IsEmergency = obj.IsEmergency;
            IsBedSide = obj.IsBedSide;
            GuideCode = obj.GuideCode;
            GuideName = obj.GuideName;
            ExamTitle = obj.ExamTitle;
            ReservationPlace = obj.ReservationPlace;
            TemplateId = obj.TemplateId;
        }
    }
}
