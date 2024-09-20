using System;

namespace YiJian.MasterData;


/// <summary>
/// 检查申请注意事项 读取输出
/// </summary>
[Serializable]
public class ExamNoteData
{        
    public int Id { get; set; }
    
    /// <summary>
    /// 注意事项编码
    /// </summary>
    public string  NoteCode { get; set; }
    
    /// <summary>
    /// 注意事项名称
    /// </summary>
    public string  NoteName { get; set; }
    
    /// <summary>
    /// 执行科室编码
    /// </summary>
    public string  ExecDeptCode { get; set; }
    
    /// <summary>
    /// 执行科室名称
    /// </summary>
    public string  ExecDeptName { get; set; }
    
    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
    /// </summary>
    public string  DisplayName { get; set; }
    
    /// <summary>
    /// 检查申请描述模板
    /// </summary>
    public string  DescTemplate { get; set; }
    
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }
    
}