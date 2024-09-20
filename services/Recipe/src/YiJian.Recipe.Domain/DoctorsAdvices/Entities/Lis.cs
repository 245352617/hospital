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
    /// 检验项
    /// </summary>
    [Comment("检验项")]
    public class Lis : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 检验项
        /// </summary>
        private Lis()
        {

        }

        /// <summary>
        /// 检验项
        /// </summary>
        /// <param name="id"></param>
        public Lis(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 检验项
        /// </summary>
        public Lis(Guid id,
            string catalogCode,
            string catalogName,
            string clinicalSymptom,
            string specimenCode,
            string specimenName,
            string specimenPartCode,
            string specimenPartName,
            string containerCode,
            string containerName,
            string containerColor,
            string specimenDescription,
            DateTime? specimenCollectDatetime,
            DateTime? specimenReceivedDatetime,
            DateTime? reportTime,
            string reportDoctorCode,
            string reportDoctorName,
            bool hasReportName,
            bool isEmergency,
            bool isBedSide,
            Guid doctorsAdviceId, string addCard, string guideCode, string guideName, string guideCatelogName)
        {
            Id = id;
            CatalogCode = catalogCode;
            CatalogName = catalogName;
            ClinicalSymptom = clinicalSymptom;
            SpecimenCode = specimenCode;
            SpecimenName = specimenName;
            SpecimenPartCode = specimenPartCode;
            SpecimenPartName = specimenPartName;
            ContainerCode = containerCode;
            ContainerName = containerName;
            ContainerColor = containerColor;
            SpecimenDescription = specimenDescription;
            SpecimenCollectDatetime = specimenCollectDatetime;
            SpecimenReceivedDatetime = specimenReceivedDatetime;
            ReportTime = reportTime;
            ReportDoctorCode = reportDoctorCode;
            ReportDoctorName = reportDoctorName;
            HasReportName = hasReportName;
            IsEmergency = isEmergency;
            IsBedSide = isBedSide;
            DoctorsAdviceId = doctorsAdviceId;
            AddCard = addCard;
            GuideCode = guideCode;
            GuideName = guideName;
            GuideCatelogName = guideCatelogName;
        }

        /// <summary>
        /// 检验类别编码
        /// </summary>
        [Comment("检验类别编码")]
        [Required, StringLength(20)]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检验类别
        /// </summary>
        [Comment("检验类别")]
        [Required, StringLength(100)]
        public string CatalogName { get; set; }

        /// <summary>
        /// 临床症状
        /// </summary>
        [Comment("临床症状")]
        [StringLength(2000)]
        public string ClinicalSymptom { get; set; }

        private string _specimenCode { get; set; }

        /// <summary>
        /// 标本编码
        /// </summary>
        [Comment("标本编码")]
        [Required, StringLength(50)]
        public string SpecimenCode
        {
            get { return _specimenCode; }
            set
            {
                if (value == null) value = "";//兼容操作
                _specimenCode = value;
            }
        }

        /// <summary>
        /// 标本名称
        /// </summary>
        [Comment("标本名称")]
        [Required, StringLength(100)]
        public string SpecimenName { get; set; }

        private string _specimenPartCode;

        /// <summary>
        /// 标本采集部位编码
        /// </summary>
        [Comment("标本采集部位编码")]
        [StringLength(20)]
        public string SpecimenPartCode
        {
            get
            {
                return _specimenPartCode;
            }
            set
            {
                if (value == null) value = ""; //兼容操作
                _specimenPartCode = value;
            }
        }

        /// <summary>
        /// 标本采集部位
        /// </summary>
        [Comment("标本采集部位")]
        [StringLength(20)]
        public string SpecimenPartName { get; set; }

        private string _containerCode;

        /// <summary>
        /// 标本容器代码
        /// </summary>
        [Comment("标本容器代码")]
        [StringLength(20)]
        public string ContainerCode
        {
            get { return _containerCode; }
            set
            {
                if (value == null) value = "";//兼容操作
                _containerCode = value;
            }
        }

        /// <summary>
        /// 标本容器
        /// </summary>
        [Comment("标本容器")]
        [StringLength(20)]
        public string ContainerName { get; set; }

        /// <summary>
        /// 标本容器颜色:0=红帽,1=蓝帽,2=紫帽
        /// </summary>
        [Comment("标本容器颜色:0=红帽,1=蓝帽,2=紫帽")]
        [StringLength(20)]
        public string ContainerColor { get; set; }

        /// <summary>
        /// 标本说明
        /// </summary>
        [Comment("标本说明")]
        [StringLength(200)]
        public string SpecimenDescription { get; set; }

        /// <summary>
        /// 标本采集时间
        /// </summary>
        [Comment("标本采集时间")]
        public DateTime? SpecimenCollectDatetime { get; set; }

        /// <summary>
        /// 标本接收时间
        /// </summary>
        [Comment("标本接收时间")]
        public DateTime? SpecimenReceivedDatetime { get; set; }

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
        public bool HasReportName { get; set; }

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
        /// 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
        /// 14.新型冠状病毒RNA检测申请单
        /// 13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
        /// </summary>
        [StringLength(50)]
        [Comment("附加卡片类型: 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期);14.新型冠状病毒RNA检测申请单;13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单")]
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
        /// 指引单大类
        /// </summary>
        [Comment("指引单大类")]
        public string GuideCatelogName { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        [NotMapped]
        public DoctorsAdvice DoctorsAdvice { get; set; }

        /// <summary>
        /// 检验小项集合
        /// </summary>
        public virtual List<LisItem> LisItems { get; set; } = new List<LisItem>();

        /// <summary>
        /// 项目Id
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
            LisItems = null;
            DoctorsAdvice = null;
        }

        /// <summary>
        /// 检验项
        /// </summary>
        public void Update(Lis obj)
        {
            CatalogCode = obj.CatalogCode;
            CatalogName = obj.CatalogName;
            ClinicalSymptom = obj.ClinicalSymptom;
            SpecimenCode = obj.SpecimenCode;
            SpecimenName = obj.SpecimenName;
            SpecimenPartCode = obj.SpecimenPartCode;
            SpecimenPartName = obj.SpecimenPartName;
            ContainerCode = obj.ContainerCode;
            ContainerName = obj.ContainerName;
            ContainerColor = obj.ContainerColor;
            SpecimenDescription = obj.SpecimenDescription;
            SpecimenCollectDatetime = obj.SpecimenCollectDatetime;
            SpecimenReceivedDatetime = obj.SpecimenReceivedDatetime;
            ReportTime = obj.ReportTime;
            ReportDoctorCode = obj.ReportDoctorCode;
            ReportDoctorName = obj.ReportDoctorName;
            HasReportName = obj.HasReportName;
            IsEmergency = obj.IsEmergency;
            IsBedSide = obj.IsBedSide;
        }

        /// <summary>
        /// 检验项
        /// </summary>
        public void Update(
            string catalogCode,
            string catalogName,
            string clinicalSymptom,
            string specimenCode,
            string specimenName,
            string specimenPartCode,
            string specimenPartName,
            string containerCode,
            string containerName,
            string containerColor,
            string specimenDescription,
            DateTime? specimenCollectDatetime,
            DateTime? specimenReceivedDatetime,
            DateTime? reportTime,
            string reportDoctorCode,
            string reportDoctorName,
            bool hasReportName,
            bool isEmergency,
            bool isBedSide)
        {
            CatalogCode = catalogCode;
            CatalogName = catalogName;
            ClinicalSymptom = clinicalSymptom;
            SpecimenCode = specimenCode;
            SpecimenName = specimenName;
            SpecimenPartCode = specimenPartCode;
            SpecimenPartName = specimenPartName;
            ContainerCode = containerCode;
            ContainerName = containerName;
            ContainerColor = containerColor;
            SpecimenDescription = specimenDescription;
            SpecimenCollectDatetime = specimenCollectDatetime;
            SpecimenReceivedDatetime = specimenReceivedDatetime;
            ReportTime = reportTime;
            ReportDoctorCode = reportDoctorCode;
            ReportDoctorName = reportDoctorName;
            HasReportName = hasReportName;
            IsEmergency = isEmergency;
            IsBedSide = isBedSide;
        }


    }
}
