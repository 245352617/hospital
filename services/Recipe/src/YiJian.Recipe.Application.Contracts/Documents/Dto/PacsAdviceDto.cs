using System;
using System.Collections.Generic;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 打印的检查项的基本信息内容
    /// </summary>
    public class PacsAdviceDto : BaseAdviceDto
    {
        /// <summary>
        /// 检查目录编码
        /// </summary> 
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检查目录名称
        /// </summary> 
        public string CatalogName { get; set; }
        /// <summary>
        /// 一级检查目录编码
        /// </summary> 
        public string FirstCatalogCode { get; set; }

        /// <summary>
        /// 一级检查目录名称
        /// </summary> 
        public string FirstCatalogName { get; set; }
        /// <summary>
        /// 临床症状
        /// </summary> 
        public string ClinicalSymptom { get; set; }

        /// <summary>
        /// 病史简要
        /// </summary> 
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 检查部位编码
        /// </summary> 
        public string PartCode { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary> 
        public string PartName { get; set; }

        /// <summary>
        /// 目录描述名称 例如心电图申请单、超声申请单
        /// </summary> 
        public string CatalogDisplayName { get; set; }

        /// <summary>
        /// 出报告时间
        /// </summary> 
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 确认报告医生编码
        /// </summary> 
        public string ReportDoctorCode { get; set; }

        /// <summary>
        /// 确认报告医生
        /// </summary> 
        public string ReportDoctorName { get; set; }

        /// <summary>
        /// 报告标识
        /// </summary> 
        public bool HasReport { get; set; }

        /// <summary>
        /// 是否紧急
        /// </summary> 
        public bool IsEmergency { get; set; }

        /// <summary>
        /// 是否在床旁
        /// </summary> 
        public bool IsBedSide { get; set; }

        /// <summary>
        /// 附加卡片类型  
        /// 12.TCT细胞学检查申请单 
        /// 11.病理检验申请单 
        /// 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用 
        /// </summary> 
        public string AddCard { get; set; }

        /// <summary>
        /// 检查项Id
        /// </summary>
        public Guid PacsId { get; set; }

        /// <summary>
        /// 标本名称多个用","隔开
        /// </summary>
        public string Specimen { get; set; }

        /// <summary>
        /// 取材部位
        /// </summary>
        public string DrawMaterialsPart { get; set; }

        /// <summary>
        /// 标本数量
        /// </summary>
        public int SpecimenQty { get; set; }

        /// <summary>
        /// 离体时间
        /// </summary>
        public DateTime LeaveTime { get; set; }

        /// <summary>
        /// 固定时间
        /// </summary>
        public DateTime RegularTime { get; set; }

        /// <summary>
        /// 特异性感染
        /// </summary>
        public string SpecificityInfect { get; set; }

        /// <summary>
        /// 申请目的
        /// </summary>
        public string ApplyForObjective { get; set; }

        /// <summary>
        /// 临床症状及体征
        /// </summary>
        public string Symptom { get; set; }

        /// <summary>
        /// 病理标本序号
        /// </summary>
        public List<PacsPathologyItemNoDto> pacsPathologyItemNoDtos { get; set; }
    }

    /// <summary>
    /// 病理标本序号Dto
    /// </summary>
    public class PacsPathologyItemNoDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        public string SpecimenName { get; set; }
    }
}
