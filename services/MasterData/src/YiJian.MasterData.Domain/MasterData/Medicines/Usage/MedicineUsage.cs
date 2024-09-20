using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品用法字典
/// </summary>
[Comment("药品用法字典")]
public class MedicineUsage : FullAuditedAggregateRoot<int>, IIsActive
{
    #region Properties

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("编码")]
    public string UsageCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("名称")]
    public string UsageName { get; set; }

    /// <summary>
    /// 全称
    /// </summary>
    [StringLength(200)]
    [Comment("全称")]
    public string FullName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500)]
    [Comment("备注")]
    public string Remark { get; set; }

    /// <summary>
    /// 是否单次
    /// </summary>
    [Comment("是否单次")]
    public bool IsSingle { get; set; }

    /// <summary>
    /// 是否回车组合
    /// </summary>
    [Comment("是否回车组合")]
    public bool IsEnterCombination { get; set; }

    /// <summary>
    /// 分类  1：输液  2：注射  3：治疗  4：服药  10其他
    /// </summary>
    [Comment("分类  1：输液  2：注射  3：治疗  4：服药  10其他")]
    public MedicineUsageCatalog Catalog { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("拼音码")]
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔码
    /// </summary>
    [StringLength(50)]
    [Comment("五笔码")]
    public string WbCode { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    [Comment("排序号")]
    public int Sort { get; set; }

    /// <summary>
    /// 诊疗项目 描述：一个或多个项目，多个以,隔开
    /// </summary>
    [StringLength(50)]
    [Comment("诊疗项目 描述：一个或多个项目，多个以,隔开")]
    public string TreatCode { get; set; }


    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 附加卡片类型	10.注射单,皮试单  08.雾化申请单  09.输液卡
    /// </summary>
    [StringLength(50)]
    [Comment("附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡")]
    public string AddCard { get; set; }

    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="catalog">分类  1：输液  2：注射  3：治疗  4：服药  10其他</param>
    /// <param name="indexNo">排序号</param>
    /// <param name="treatCode">诊疗项目 描述：一个或多个项目，多个以,隔开</param>
    /// <param name="isActive">是否启用</param>
    /// <param name="isEnterCombination"></param>
    /// <param name="fullName">全称</param>
    /// <param name="remark">备注</param>
    /// <param name="isSingle">是否单次</param>
    public void Modify([NotNull] string name, // 名称
        MedicineUsageCatalog catalog, // 分类  1：输液  2：注射  3：治疗  4：服药  10其他
        [NotNull] int indexNo, // 排序号
        string treatCode, // 诊疗项目 描述：一个或多个项目，多个以,隔开
        bool isActive, // 是否启用
        bool isEnterCombination,
        string fullName, string remark, bool isSingle
    )
    {
        //名称
        UsageName = name;
        FullName = Check.NotNull(fullName, "全称", MedicineUsageConsts.MaxFullNameLength);
        Remark = Check.NotNull(remark, "备注", MedicineUsageConsts.MaxRemarkLength);
        IsSingle = isSingle;
        //分类  1：输液  2：注射  3：治疗  4：服药  10其他
        Catalog = catalog;

        //拼音码
        PyCode = Check.NotNull(name.FirstLetterPY(), "拼音码", MedicineUsageConsts.MaxPyCodeLength);

        //五笔码
        WbCode = Check.Length(name.FirstLetterWB(), "五笔码", MedicineUsageConsts.MaxWbCodeLength);

        //排序号
        Sort = indexNo;

        //诊疗项目 描述：一个或多个项目，多个以,隔开
        TreatCode = Check.Length(treatCode, "诊疗项目", MedicineUsageConsts.MaxTreatCodeLength);

        //是否启用
        IsActive = isActive;

        IsEnterCombination = isEnterCombination;
    }

    #endregion

    /// <summary>
    /// 同步修改
    /// </summary>
    /// <param name="usageName"></param>
    /// <param name="fullName"></param>
    /// <param name="addCard"></param>
    /// <param name="treatCode"></param>
    public void Update(string usageName, string fullName, string addCard, string treatCode)
    {
        UsageName = usageName;
        FullName = fullName;
        AddCard = addCard;
        TreatCode = treatCode;
    }
}