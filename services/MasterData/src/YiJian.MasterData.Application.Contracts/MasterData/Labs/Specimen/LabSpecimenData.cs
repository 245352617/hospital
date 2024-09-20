using System;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验标本 读取输出
/// </summary>
[Serializable]
public class LabSpecimenData
{        
    public int Id { get; set; }
    
    /// <summary>
    /// 标本编码
    /// </summary>
    public string  SpecimenCode { get; set; }
    
    /// <summary>
    /// 标本名称
    /// </summary>
    public string  SpecimenName { get; set; }
    /// <summary>
    /// 采集部位编码
    /// </summary>
    public string  SpecimenPartCode { get; set; }
    
    /// <summary>
    /// 采集部位名称
    /// </summary>
    public string  SpecimenPartName { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int  Sort { get; set; }
    
    /// <summary>
    /// 拼音码
    /// </summary>
    public string  PyCode { get; set; }
    
    /// <summary>
    /// 五笔
    /// </summary>
    public string  WbCode { get; set; }
    
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }
    
}