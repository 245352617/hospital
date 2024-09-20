using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.MasterData.Treats;

public class TreatProjectModelDto : EntityDto<int>
{
    public new int Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary> 
    public string TreatCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary> 
    public string TreatName { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary> 
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary> 
    public string WbCode { get; set; }

    /// <summary>
    /// 单价
    /// </summary> 
    public decimal Price { get; set; }

    /// <summary>
    /// 其它价格 加收金额
    /// </summary> 
    public decimal? OtherPrice { get; set; }
    /// <summary>
    /// 加收标志	
    /// </summary>
    public bool Additional { get; set; }

    /// <summary>
    /// 诊疗处置类别代码
    /// </summary> 
    public string CategoryCode { get; set; }

    /// <summary>
    /// 诊疗处置类别名称
    /// </summary> 
    public string CategoryName { get; set; }

    /// <summary>
    /// 规格
    /// </summary> 
    public string Specification { get; set; }

    /// <summary>
    /// 单位
    /// </summary> 
    public string Unit { get; set; }

    /// <summary>
    /// 默认频次代码
    /// </summary> 
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 执行科室代码
    /// </summary> 
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary> 
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 收费大类代码
    /// </summary> 
    public string FeeTypeMainCode { get; set; }

    /// <summary>
    /// 收费小类代码
    /// </summary> 
    public string FeeTypeSubCode { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary> 
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 项目归类 --龙岗字典所需
    /// </summary> 
    public string ProjectMerge { get; set; }

    public string ChargeCode { get; set; }

    public string ChargeName { get; set; }

    public string DictionaryCode { get; set; }

    public string DictionaryName { get; set; }

    public string MeducalInsuranceCode { get; set; }

    public string YBInneCode { get; set; }
}
