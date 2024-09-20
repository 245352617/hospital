using System;

namespace YiJian.MasterData.Labs.Position;


/// <summary>
/// 检验标本采集部位 读取输出
/// </summary>
[Serializable]
public class LabSpecimenPositionData
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
    
    public bool  IsActive { get; set; }
    
}