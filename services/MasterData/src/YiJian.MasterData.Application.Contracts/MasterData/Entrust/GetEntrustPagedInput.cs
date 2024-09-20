using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData;

/// <summary>
/// 嘱托 分页排序输入
/// </summary>
[Serializable]
public class GetEntrustPagedInput : PageBase
{
    /// <summary>
    /// 名称或拼音
    /// </summary>
    public string Filter { get; set; }
  }
