using System;

namespace YiJian.MasterData;

public class DictionariesWhereInput
{
    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictionariesTypeCode { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string DictionariesName { get;set; }

    public Guid Id { get; set; }
}