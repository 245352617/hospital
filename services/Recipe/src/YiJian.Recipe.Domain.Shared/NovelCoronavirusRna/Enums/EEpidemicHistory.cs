using System.ComponentModel;

namespace YiJian.Recipe.Enums
{
    public enum EEpidemicHistory
    {
        [Description("无")] Not,
        [Description("发病前14天内有武汉地区或其他有本地病例持续传播地区的旅行史或居住史")] WithinFourteenDaysThereAreCases,
        [Description("发病前14天内有武汉地区或其他有本地病例持续传播地区的旅行史或居住史")] WithinFourteenDaysContactPerson,
        [Description("有聚集性发病或与新型冠状病毒感染者有流行病学关联")] InfectedPatient,


    }
}