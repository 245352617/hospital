using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验单信息
/// </summary>
[Comment("检验单信息")]
public class LabReportInfo : FullAuditedAggregateRoot<int>
{
    #region Properties

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(40)]
    [Comment("名称")]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [Comment("编码")]
    public int Code { get; set; }

    /// <summary>
    /// 样本采集类型
    /// </summary>
    [StringLength(50)]
    [Comment("执行科室编码")]
    public string SampleCollectType { get; set; }

    /// <summary>
    /// 注意信息
    /// </summary>
    [StringLength(200)]
    [Comment("注意信息")]
    public string Remark { get; set; }

    /// <summary>
    /// 指引单大类
    /// </summary>
    [StringLength(50)]
    [Comment("指引单大类")]
    public string CatelogName { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [StringLength(50)]
    [Comment("执行科室名称")]
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 试管名称
    /// </summary>
    [StringLength(50)]
    [Comment("试管名称")]
    public string TestTubeName { get; set; }

    /// <summary>
    /// 门诊合并号
    /// </summary>
    [StringLength(50)]
    [Comment("门诊合并号")]
    public string MergerNo { get; set; }

    #endregion

    #region Fun

    ///// <summary>
    ///// 修改
    ///// </summary>
    //public void Modify([NotNull] string name)
    //{ }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update([NotNull] string Name,
        string SampleCollectType,
        string Remark,
        string CatelogName,
        string ExecDeptName,
        string TestTubeName,
        string MergerNo
    )
    {
        this.Name= Name;
        this.SampleCollectType = SampleCollectType;
        this.Remark = Remark;
        this.CatelogName = CatelogName;
        this.ExecDeptName = ExecDeptName;
        this.TestTubeName = TestTubeName;
        this.MergerNo = MergerNo;
    }
    #endregion
}