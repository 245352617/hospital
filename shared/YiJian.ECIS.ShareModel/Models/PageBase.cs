using System.Text.Json.Serialization;

namespace YiJian.ECIS.ShareModel.Models;

/// <summary>
/// 请求的分页参数
/// </summary>
public class PageBase
{
    private int index;

    /// <summary>
    /// 当前页面，从1开始,默认第一页 1
    /// </summary>
    public int Index
    {
        get
        {
            return index < 1 ? 1 : index;
        }
        set
        {
            index = value;
        }
    }

    private int size;

    /// <summary>
    /// 每页大小，默认:10 
    /// </summary>
    public int Size
    {
        get
        {
            return size < 1 ? 1 : size;
        }
        set
        {
            size = value < 1 ? 10 : value;
        }
    }

    /// <summary>
    /// 跳过的行数
    /// </summary>   
    [JsonIgnore]
    public int SkipCount => (Index - 1) * Size;

    /// <summary>
    /// 获取分页组合键
    /// </summary>
    /// <returns></returns>
    [JsonIgnore]
    public string GetKey
    {
        get
        {
            return $"{Size}::{index}";
        }
    }
}
