using System;

namespace YiJian.MasterData.Sequences;

/// <summary>
/// 序列 读取输出
/// </summary>
[Serializable]
public class SequenceData
{        
    public int Id { get; set; }
    
    /// <summary>
    /// 编码
    /// </summary>
    public string  Code { get; set; }
    
    /// <summary>
    /// 名称
    /// </summary>
    public string  Name { get; set; }
    
    /// <summary>
    /// 序列值
    /// </summary>
    public int  Value { get; set; }
    
    /// <summary>
    /// 格式
    /// </summary>
    public string  Format { get; set; }
    
    /// <summary>
    /// 序列值长度
    /// </summary>
    public int  Length { get; set; }
    
    /// <summary>
    /// 日期
    /// </summary>
    public DateTime  Date { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    public string  Memo { get; set; }
    
}