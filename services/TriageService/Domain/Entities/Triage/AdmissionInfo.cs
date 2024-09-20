using SamJan.MicroService.PreHospital.Core.BaseEntities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class AdmissionInfo : BaseEntity<Guid>
    {
        public AdmissionInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }
        
        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        [Description("院前分诊患者建档表主键Id")]
        public Guid PatientInfoId { get; set; }
        
        /// <summary>
        /// 现病史
        /// </summary>
        [Description("现病史")]
        [StringLength(500)]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        [Description("既往史")]
        [StringLength(500)]
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        [Description("过敏史")]
        [StringLength(500)]
        public string AllergyHistory { get; set; }

        /// <summary>
        /// 是否咽痛咳嗽
        /// </summary>
        [Description("是否咽痛咳嗽")]
        [StringLength(10)]
        public string IsSoreThroatAndCough { get; set; }
        
        /// <summary>
        /// 是否发热
        /// </summary>
        [Description("是否发热")]
        [StringLength(10)]
        public string IsHot { get; set; }
        
        /// <summary>
        /// 是否去过中高风险区
        /// </summary>
        [Description("是否去过中高风险区")]
        [StringLength(10)]
        public string IsMediumAndHighRisk { get; set; }
        
        /// <summary>
        /// 是否聚集性发病
        /// </summary>
        [Description("是否聚集性发病")]
        [StringLength(10)]
        public string IsAggregation { get; set; }
        
        /// <summary>
        /// 2周内是否接触过中高风险区发热患者
        /// </summary>
        [Description("2周内是否接触过中高风险区发热患者")]
        [StringLength(10)]
        public string IsContactHotPatient { get; set; }
        
        /// <summary>
        /// 2周内是否接触过确诊新冠阳性患者
        /// </summary>
        [Description("2周内是否接触过确诊新冠阳性患者")]
        [StringLength(10)]
        public string IsContactNewCoronavirus { get; set; }
        
        /// <summary>
        /// 最近14天内您是否在集中隔离医学观察场所留观
        /// </summary>
        [Description("最近14天内您是否在集中隔离医学观察场所留观")]
        [StringLength(10)]
        public string IsFocusIsolated { get; set; }
        
        /// <summary>
        /// 2周内是否有境外旅居史
        /// </summary>
        [Description("2周内是否有境外旅居史")]
        [StringLength(10)]
        public string IsBeenAbroad { get; set; }
        
        /// <summary>
        /// 具体国家/地区
        /// </summary>
        [Description("具体国家/地区")]
        [StringLength(200)]
        public string CountrySpecific { get; set; }
        
        /// <summary>
        /// 境外开始日期
        /// </summary>
        [Description("境外开始日期")]
        public string AbroadStartTime { get; set; }
        
        /// <summary>
        /// 境外结束日期
        /// </summary>
        [Description("境外结束日期")]
        public string AbroadEndTime { get; set; }
        
        /// <summary>
        /// 回国日期
        /// </summary>
        [Description("回国日期")]
        public string ReturnTime { get; set; }

    }
}
