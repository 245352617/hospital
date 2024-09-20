namespace YiJian.MasterData.Exams.Notes;

/// <summary>
/// 检查申请注意事项 表字段长度常量
/// </summary>
public class ExamNotesConsts
{  
    /// <summary>
    /// 注意事项编码(20)
    /// </summary>
    public static int MaxCodeLength { get; set; } = 20;
    /// <summary>
    /// 注意事项名称(200)
    /// </summary>
    public static int MaxNameLength { get; set; } = 500;
    /// <summary>
    /// 科室编码(20)
    /// </summary>
    public static int MaxDeptCodeLength { get; set; } = 20;
    /// <summary>
    /// 科室(50)
    /// </summary>
    public static int MaxDeptLength { get; set; } = 50;
    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单(50)
    /// </summary>
    public static int MaxDisplayNameLength { get; set; } = 50;
    /// <summary>
    /// 检查申请描述模板(50)
    /// </summary>
    public static int MaxDescTemplateLength { get; set; } = 50;
}