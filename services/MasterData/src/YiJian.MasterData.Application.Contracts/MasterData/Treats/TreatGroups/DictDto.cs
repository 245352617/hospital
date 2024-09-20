using System.Collections.Generic;

namespace YiJian.MasterData;

public class DictDto
{
    /// <summary>
    /// 字典编码
    /// </summary>
    public string DictionariesCode { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string DictionariesName { get; set; }

    /// <summary>
    /// 诊疗分组
    /// </summary>
    public List<TreatCatalogDto> TreatGroup { get; set; }
}