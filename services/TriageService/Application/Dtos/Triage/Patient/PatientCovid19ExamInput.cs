using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 新冠问卷信息
    /// </summary>
    public class PatientCovid19ExamInput
    {
        /// <summary>
        /// 体温
        /// </summary>
        /// <example>36.5</example>
        public decimal Temperature { get; set; }

        /// <summary>
        /// 是否发热
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// 是否干咳
        /// </summary>
        public bool IsCough { get; set; }

        /// <summary>
        /// 是否乏力
        /// </summary>
        public bool IsFeeble { get; set; }

        /// <summary>
        /// 是否嗅觉、味觉减退
        /// </summary>
        public bool IsHearingAndSmellingLoss { get; set; }

        /// <summary>
        /// 是否鼻塞
        /// </summary>
        public bool IsStuffyNose { get; set; }

        /// <summary>
        /// 是否流涕
        /// </summary>
        public bool IsNoseRunning { get; set; }

        /// <summary>
        /// 是否咽痛
        /// </summary>
        public bool IsSoreThroat { get; set; }

        /// <summary>
        /// 是否结膜炎
        /// </summary>
        public bool IsConjunctivitis { get; set; }

        /// <summary>
        /// 是否肌痛
        /// </summary>
        public bool IsMusclePain { get; set; }

        /// <summary>
        /// 是否腹泻
        /// </summary>
        public bool IsDiarrhea { get; set; }

        /// <summary>
        /// 近14天内您是否去过境外以及境内中高风险地区，或有病例报告的社区
        /// </summary>
        public bool IsMediumAndHighRisk { get; set; }

        /// <summary>
        /// 近14天内您是否接触过来自境外以及境内中高风险地区的人员
        /// </summary>
        public bool IsContactHotPatient { get; set; }

        /// <summary>
        /// 近14天内是否接触过确诊病例或无症状感染者(核酸检测阳性者)
        /// </summary>
        public bool IsContactNewCoronavirus { get; set; }

        /// <summary>
        /// 14天内您的家庭、办公室、学校或托幼机构班次、车间等集体单位是否出现2例及以上发热和/或呼吸道症状的聚集性病例
        /// </summary>
        public bool IsAggregation { get; set; }

        /// <summary>
        /// 14天内是否有境外旅居史（0: 境内本市；1: 境内非本市; 2:  境外）
        /// </summary>
        public int BeenAbroadStatus { get; set; }

        /// <summary>
        /// 具体国家或地区
        /// </summary>
        /// <example>澳门</example>
        public string CountrySpecific { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        /// <example>广东</example>
        public string ProvinceSpecific { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        /// <example>深圳</example>
        public string CitySpecific { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        /// <example>南山</example>
        public string DistrictSpecific { get; set; }

        /// <summary>
        /// 患者签名（图片地址或base64）
        /// </summary>
        /// <example>http://192.168.1.162:8600/oos/picture/sign/114</example>
        public string PatientSignPicture { get; set; }

        /// <summary>
        /// 接诊医生签名（图片地址或base64）
        /// </summary>
        /// <example>http://192.168.1.162:8600/oos/picture/sign/114</example>
        public string DoctorSignPicture { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        /// <example>2021-10-10 13:09:12</example>
        public DateTime Date { get; set; }
    }
}
