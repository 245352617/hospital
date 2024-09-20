using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData;

/// <summary>
/// 药品频次字典
/// FrequencyUnit :0T   1D   1W  2D  1H  2H  3H  4H  st
/// eg:FrequencyCode	FrequencyName	FrequencyTimes	FrequencyUnit	ExecTimes	                FrequencyWeek	     Catalog
///      qd	             1次/天	             1	            1D	            08:00       	                  	           0
///    FrequencyCode	FrequencyName	FrequencyTimes	FrequencyUnit	ExecuteDayTime	            FrequencyWeek        Catalog
///      q6h	         1次/6小时	         4	            1D	        08:00,14:00,20:00,02:00	                           1
///    FrequencyCode	FrequencyName	FrequencyTimes	FrequencyUnit	ExecuteDayTime	            FrequencyWeek        Catalog
///      tiw135	         3次/周	             3	            1W          08：00,08：00,08：00；	     周一,周三,周五        1
/// </summary>
[Comment("药品频次字典")]
public class MedicineFrequency : FullAuditedAggregateRoot<int>, IIsActive
{
    #region Properties

    /// <summary>
    /// 频次编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("频次编码")]
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 频次名称
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("频次名称")]
    public string FrequencyName { get; set; }

    /// <summary>
    /// 频次全称
    /// </summary>
    [StringLength(200)]
    [Comment("频次全称")]
    public string FullName { get; set; }

    /// <summary>
    /// 频次系数
    /// </summary>
    [Required]
    [Comment("频次系数")]
    public int Times { get; set; }

    /// <summary>
    /// 频次单位
    /// </summary>
    [StringLength(50)]
    [Comment("频次单位")]
    public string Unit { get; set; }

    /// <summary>
    /// 执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开
    /// </summary>
    [StringLength(200)]
    [Comment("执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开")]
    public string ExecDayTimes { get; set; }

    /// <summary>
    /// 频次周明细
    /// </summary>
    [StringLength(200)]
    [Comment("频次周明细")]
    public string Weeks { get; set; }

    /// <summary>
    /// 频次分类 0：临时 1：长期 2：通用
    /// </summary>
    [Required]
    [Comment("频次分类 0：临时 1：长期 2：通用")]
    public MedicineFrequencyCatalog Catalog { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    [Comment("排序号")]
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(100)]
    [Comment("备注")]
    public string Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 第三方id
    /// </summary>
    [StringLength(20)]
    [Comment("第三方id")]
    public string ThirdPartyId { get; set; }

    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">频次名称</param>
    /// <param name="times">频次系数</param>
    /// <param name="unit">频次单位</param>
    /// <param name="execTimes">执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开</param>
    /// <param name="weeks">频次周明细</param>
    /// <param name="catalog">频次分类 0：临时 1：长期 2：通用</param>
    /// <param name="indexNo">排序号</param>
    /// <param name="remark">备注</param>
    /// <param name="isActive">是否启用</param>
    /// <param name="fullName">频次全称</param>
    /// <param name="thirdPartyId"></param>
    public void Modify([NotNull] string name, // 频次名称
        [NotNull] int times, // 频次系数
        [NotNull] string unit, // 频次单位
        string execTimes, // 执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开
        string weeks, // 频次周明细
        [NotNull] MedicineFrequencyCatalog catalog, // 频次分类 0：临时 1：长期 2：通用
        [NotNull] int indexNo, // 排序号
        string remark, // 备注
        bool isActive, // 是否启用
        string fullName, string thirdPartyId
    )
    {
        //频次名称
        FrequencyName = name;
        FullName = Check.NotNull(fullName, "频次全称", MedicineFrequencyConsts.MaxFullNameLength);

        //频次系数
        Times = Check.NotNull(times, "频次系数");

        //频次单位
        Unit = Check.NotNull(unit, "频次单位", MedicineFrequencyConsts.MaxUnitLength);

        //执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开
        ExecDayTimes = execTimes;

        //频次周明细
        Weeks = Check.Length(weeks, "频次周明细", MedicineFrequencyConsts.MaxWeeksLength);

        //频次分类 0：临时 1：长期 2：通用
        Catalog = Check.NotNull(catalog, "频次分类");

        //排序号
        Sort = Check.NotNull(indexNo, "排序号");

        //备注
        Remark = Check.Length(remark, "备注", MedicineFrequencyConsts.MaxRemarkLength);

        //是否启用
        IsActive = isActive;

        ThirdPartyId = thirdPartyId;
    }

    #endregion

    /// <summary>
    /// 同步修改
    /// </summary>
    /// <param name="frequencyCode"></param>
    /// <param name="frequencyName"></param>
    /// <param name="fullName"></param>
    /// <param name="times"></param>
    /// <param name="execDayTimes"></param>
    public void Update(string frequencyCode, string frequencyName, string fullName, int times, string execDayTimes)
    {
        FrequencyCode = frequencyCode;
        FrequencyName = frequencyName;
        FullName = fullName;
        Times = times;
        ExecDayTimes = execDayTimes;
    }
}