using System.ComponentModel;

namespace YiJian.MasterData.Medicines;


/// <summary>
/// 频次分类 0：临时 1：长期 2：通用
/// </summary>
public enum MedicineFrequencyCatalog
{
    [Description("未定义")]
    None = -1,
    [Description("临时")]
    Temporary = 0,
    [Description("长期")]
    LongTerm = 1,
    [Description("通用")]
    General = 2
}
