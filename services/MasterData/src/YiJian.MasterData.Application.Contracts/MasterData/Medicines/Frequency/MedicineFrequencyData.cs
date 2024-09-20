using System;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData;

/// <summary>
/// 药品频次字典 读取输出
/// </summary>
[Serializable]
public class MedicineFrequencyData
{
    public int Id { get; set; }

    /// <summary>
    /// 频次编码
    /// </summary>
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 频次名称
    /// </summary>
    public string FrequencyName { get; set; }

    /// <summary>
    /// 频次全称
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 在一个周期内执行的次数
    /// </summary>
    public int? FrequencyTimes { get; set; }

    /// <summary>
    /// 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时
    /// </summary>

    public string FrequencyUnit { get; set; }

    /// <summary>
    /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
    /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
    /// 日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。
    /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。 
    /// </summary>
    public string FrequencyExecDayTimes { get; set; }

    /// <summary>
    /// 频次周明细
    /// </summary>
    public string Weeks { get; set; }

    /// <summary>
    /// 频次分类 0：临时 1：长期 2：通用
    /// </summary>
    public MedicineFrequencyCatalog Catalog { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }
    
    
    /// <summary>
    /// 第三方id
    /// </summary>
    public string ThirdPartyId { get; set; }
}