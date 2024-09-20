using System;

namespace YiJian.MasterData;

/// <summary>
/// 检查部位 读取输出
/// </summary>
[Serializable]
public class ExamPartData
{        
    public int Id { get; set; }
    
    /// <summary>
    /// 检查部位编码
    /// </summary>
    public string  PartCode { get; set; }
    
    /// <summary>
    /// 检查部位名称
    /// </summary>
    public string  PartName { get; set; }
    
    /// <summary>
    /// 排序号
    /// </summary>
    public int  Sort { get; set; }
    
    /// <summary>
    /// 拼音码
    /// </summary>
    public string  PyCode { get; set; }
    
}