using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.MasterData;

[Serializable]
public class DepartmentCreation
{
    /// <summary>
    /// 科室名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空！")]
    [Display(Name = "科室名称")]
    [StringLength(50, ErrorMessage = "科室名称最大长度不能超过50!")]
    public string Name { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 挂号科室编码
    /// </summary>
    public string RegisterCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActived { get; set; }
}
