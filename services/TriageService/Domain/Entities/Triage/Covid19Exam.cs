using SamJan.MicroService.PreHospital.Core.BaseEntities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 新冠问卷信息
    /// </summary>
    public class Covid19Exam : BaseEntity<Guid>
    {
        public Covid19Exam(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 患者 ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Description("患者姓名")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Description("联系电话")]
        [StringLength(20)]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Description("身份证号")]
        [StringLength(20)]
        public string IdentityNo { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        [Description("体温")]
        public decimal Temperature { get; set; }

        /// <summary>
        /// 是否发热
        /// </summary>
        [Description("是否发热")]
        public bool IsHot { get; set; }

        /// <summary>
        /// 是否干咳
        /// </summary>
        [Description("是否干咳")]
        public bool IsCough { get; set; }

        /// <summary>
        /// 是否乏力
        /// </summary>
        [Description("是否乏力")]
        public bool IsFeeble { get; set; }

        /// <summary>
        /// 是否嗅觉、味觉减退
        /// </summary>
        [Description("是否嗅觉、味觉减退")]
        public bool IsHearingAndSmellingLoss { get; set; }

        /// <summary>
        /// 是否鼻塞
        /// </summary>
        [Description("是否鼻塞")]
        public bool IsStuffyNose { get; set; }

        /// <summary>
        /// 是否流涕
        /// </summary>
        [Description("是否流涕")]
        public bool IsNoseRunning { get; set; }

        /// <summary>
        /// 是否咽痛
        /// </summary>
        [Description("是否咽痛")]
        public bool IsSoreThroat { get; set; }

        /// <summary>
        /// 是否结膜炎
        /// </summary>
        [Description("是否结膜炎")]
        public bool IsConjunctivitis { get; set; }

        /// <summary>
        /// 是否肌痛
        /// </summary>
        [Description("是否肌痛")]
        public bool IsMusclePain { get; set; }

        /// <summary>
        /// 是否腹泻
        /// </summary>
        [Description("是否腹泻")]
        public bool IsDiarrhea { get; set; }

        /// <summary>
        /// 近14天内您是否去过境外以及境内中高风险地区，或有病例报告的社区
        /// </summary>
        [Description("近14天内您是否去过境外以及境内中高风险地区，或有病例报告的社区")]
        public bool IsMediumAndHighRisk { get; set; }

        /// <summary>
        /// 近14天内您是否接触过来自境外以及境内中高风险地区的人员
        /// </summary>
        [Description("近14天内您是否接触过来自境外以及境内中高风险地区的人员")]
        public bool IsContactHotPatient { get; set; }

        /// <summary>
        /// 近14天内是否接触过确诊病例或无症状感染者(核酸检测阳性者)
        /// </summary>
        [Description("近14天内是否接触过确诊病例或无症状感染者(核酸检测阳性者)")]
        public bool IsContactNewCoronavirus { get; set; }

        /// <summary>
        /// 14天内您的家庭、办公室、学校或托幼机构班次、车间等集体单位是否出现2例及以上发热和/或呼吸道症状的聚集性病例
        /// </summary>
        [Description("14天内您的家庭、办公室、学校或托幼机构班次、车间等集体单位是否出现2例及以上发热和/或呼吸道症状的聚集性病例")]
        public bool IsAggregation { get; set; }

        /// <summary>
        /// 14天内是否有境外旅居史（0: 境内本市；1: 境内非本市; 2:  境外）
        /// </summary>
        [Description("14天内是否有境外旅居史（0: 境内本市；1: 境内非本市; 2:  境外）")]
        public int BeenAbroadStatus { get; set; }

        /// <summary>
        /// 具体国家或地区
        /// </summary>
        /// <example>澳门</example>
        [Description("具体国家或地区")]
        [StringLength(1024)]
        public string CountrySpecific { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        /// <example>广东</example>
        [Description("省")]
        [StringLength(1024)]
        public string ProvinceSpecific { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        /// <example>深圳</example>
        [Description("市")]
        [StringLength(1024)]
        public string CitySpecific { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        /// <example>南山</example>
        [Description("区")]
        [StringLength(1024)]
        public string DistrictSpecific { get; set; }

        /// <summary>
        /// 患者签名（图片地址或base64）
        /// </summary>
        [Description("患者签名（图片地址或base64）")]
        [StringLength(1024)]
        public string PatientSignPicture { get; set; }

        /// <summary>
        /// 接诊医生签名（图片地址或base64）
        /// </summary>
        [Description("接诊医生签名（图片地址或base64）")]
        public string DoctorSignPicture { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        public DateTime Date { get; set; }
    }
}
