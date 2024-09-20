using System.ComponentModel;

namespace YiJian.Recipe.Enums
{
    public enum EConsultationOpinions
    {
        [Description("疑似患者")] SuspectedPatient,
        [Description("医学观察对象")] MedicalObservation,
        [Description(" 复工复产复学 ")] ReturnToWork,
    }
}