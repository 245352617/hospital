using System.ComponentModel;

namespace YiJian.MasterData.Medicines;


/// <summary>
/// 用法分类  1：输液  2：注射  3：治疗  4：服药  10其他
/// </summary>
public enum MedicineUsageCatalog
{
    [Description("未定义")]
    None = 0,
    [Description("输液")]
    Infusion = 1,
    [Description("注射")]
    Injection = 2,
    [Description("治疗")]
    Treat = 3,
    [Description("服药")]
    TakeMedicine = 4,
    [Description("其它")]
    Other = 10
}
