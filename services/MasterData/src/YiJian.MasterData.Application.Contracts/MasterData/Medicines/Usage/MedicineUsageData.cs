using System;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品用法字典 读取输出
/// </summary>
[Serializable]
public class MedicineUsageData
{
    public int Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string UsageCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string UsageName { get; set; }

    /// <summary>
    /// 全称
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 是否单次
    /// </summary>
    public bool IsSingle { get; set; }

    /// <summary>
    /// 分类  1：输液  2：注射  3：治疗  4：服药  10其他
    /// </summary>
    public MedicineUsageCatalog Catalog { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔码
    /// </summary>
    public string WbCode { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 诊疗项目 描述：一个或多个项目，多个以,隔开
    /// </summary>
    public string TreatCode { get; set; }

    /// <summary>
    /// 诊疗项目 描述：一个或多个项目，多个以,隔开
    /// </summary>
    public string TreatName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 是否回车组合
    /// </summary>
    public bool IsEnterCombination { get; set; }

    /// <summary>
    /// PyCode 长度
    /// </summary>
    public int PyCodeLen
    {
        get
        {
            return PyCode.Length;
        }
    }

}