using System;

namespace YiJian.MasterData.Labs.Container;

/// <summary>
/// 容器编码 读取输出
/// </summary>
[Serializable]
public class LabContainerData
{        
    public int Id { get; set; }
    
    /// <summary>
    /// 容器编码
    /// </summary>
    public string  ContainerCode { get; set; }
    
    /// <summary>
    /// 容器名称
    /// </summary>
    public string  ContainerName { get; set; }
    
    /// <summary>
    /// 容器颜色
    /// </summary>
    public string  ContainerColor { get; set; }
    
    public bool  IsActive { get; set; }
    
}