using System;
using Volo.Abp.Validation;

namespace YiJian.MasterData.Sequences;

/// <summary>
/// 获取流水号
/// </summary>
[Serializable]
public class GetSequenceInput
{   

    /// <summary>
    /// 编码
    /// </summary>
    [DynamicStringLength(typeof(SequenceConsts), nameof(SequenceConsts.MaxCodeLength), ErrorMessage = "编码最大长度不能超过{1}!")]
    public string Code { get; set; }
}
