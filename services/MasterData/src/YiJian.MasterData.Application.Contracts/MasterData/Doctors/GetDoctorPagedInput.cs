using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData;

/// <summary>
/// 医生信息 分页排序输入
/// </summary>
public class GetDoctorPagedInput : PageBase
{
    /// <summary>
    /// 医生编码
    /// </summary>
    public string DoctorCode { get; set; }
    /// <summary>
    /// 医生名称
    /// </summary>
    public string DoctorName { get; set; }

    /// <summary>
    /// 人员类型		1.急诊医生  2.急诊护士 0.其他人员
    /// </summary>
    public int DoctorType { get; set; } = -1;
}
