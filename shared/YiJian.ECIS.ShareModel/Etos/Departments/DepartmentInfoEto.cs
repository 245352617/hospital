namespace YiJian.ECIS.ShareModel.Etos.Departments;

/// <summary>
/// 科室信息模型
/// </summary>
public class DepartmentInfoEto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 挂号科室编码
    /// </summary>
    public string RegisterCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsActived { get; set; }

}
