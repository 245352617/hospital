using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 报卡类型
    /// </summary>
    public enum ECardReportingType
    {
        [Description("无")] Not = 0,
        [Description("普通传染病")] CommonInfectiousDiseases = 1,
        [Description("性病")] VenerealDisease = 2,
        [Description("结核病")] Tuberculosis = 3,
        [Description("病毒性肝炎")] ViralHepatitis = 4,
        [Description("艾滋病")] Aids = 5,
        [Description("健康状况询问与医学建议卡片")] HealthInquiry = 7,
        [Description("狂犬病")] Rabies = 15,
        [Description("脑卒中登记")] StrokeRegistration = 12,
        [Description("食源性")] Foodborne = 27,
        [Description("肿瘤登记")] TumorRegistration = 11,
        [Description("心肌梗死")] MyocardialInfarct = 20,
    }
}