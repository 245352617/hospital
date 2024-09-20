using System;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 启用
/// </summary>
[Serializable]
public class ActiveInput
{   
    #region Properties

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Active { get; set; }
    #endregion Properties
}
