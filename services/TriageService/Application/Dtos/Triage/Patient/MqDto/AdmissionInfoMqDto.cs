namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 推送患者入院情况Dto
    /// </summary>
    public class AdmissionInfoMqDto
    {
        /// <summary>
        /// 现病史
        /// </summary>
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string PastMedicalHistory { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string AllergyHistory { get; set; }
        /// <summary>
        /// 是否咽痛咳嗽
        /// </summary>
        public string IsSoreThroatAndCough { get; set; }
        /// <summary>
        /// 是否发热
        /// </summary>
        public string IsHot { get; set; }
        /// <summary>
        /// 是否去过中高风险区
        /// </summary>
        public string IsMediumAndHighRisk { get; set; }
        
        /// <summary>
        /// 是否聚集性发病
        /// </summary>
        public string IsAggregation { get; set; }
        
        /// <summary>
        /// 2周内是否接触过中高风险区发热患者
        /// </summary>
        public string IsContactHotPatient { get; set; }
        
        /// <summary>
        /// 2周内是否接触过确诊新冠阳性患者
        /// </summary>
        public string IsContactNewCoronavirus { get; set; }
        
        /// <summary>
        /// 最近14天内您是否在集中隔离医学观察场所留观
        /// </summary>
        public string IsFocusIsolated { get; set; }
        
        /// <summary>
        /// 2周内是否有境外旅居史
        /// </summary>
        public string IsBeenAbroad { get; set; }
        
        /// <summary>
        /// 具体国家/地区
        /// </summary>
        public string CountrySpecific { get; set; }
        
        /// <summary>
        /// 境外开始日期
        /// </summary>
        public string AbroadStartTime { get; set; }
        
        /// <summary>
        /// 境外结束日期
        /// </summary>
        public string AbroadEndTime { get; set; }
        
        /// <summary>
        /// 回国日期
        /// </summary>
        public string ReturnTime { get; set; }
    }
}