using Newtonsoft.Json;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.DDPs.Dto;

/// <summary>
/// 医嘱信息
/// </summary>
public class DoctorAdvices4HisDto
{

    /// <summary>
    /// 医嘱编码
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// 医嘱名称
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///  医嘱项目分类编码
    /// </summary>
    [JsonProperty("categoryCode")]
    public string CategoryCode { get; set; }

    /// <summary>
    /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
    /// </summary>
    [JsonProperty("categoryName")]
    public string CategoryName { get; set; }

    /// <summary>
    /// 内部分方方号
    /// </summary>
    [JsonProperty("prescriptionNo")]
    public string PrescriptionNo { get; set; }

    /// <summary>
    /// 医嘱号（组号）
    /// </summary> 
    [JsonProperty("recipeNo")]
    public string RecipeNo { get; set; }

    /// <summary>
    /// 医嘱子号（同组下参数修改）
    /// </summary>
    [JsonProperty("recipeGroupNo")]
    public int RecipeGroupNo { get; set; } = 1;

    /// <summary>
    /// 开嘱时间
    /// </summary>
    [JsonProperty("applyTime")]
    public DateTime ApplyTime { get; set; }

    /// <summary>
    /// 申请医生编码
    /// </summary>
    [JsonProperty("applyDoctorCode")]
    public string ApplyDoctorCode { get; set; }

    /// <summary>
    /// 申请医生
    /// </summary>
    [JsonProperty("applyDoctorName")]
    public string ApplyDoctorName { get; set; }

    /// <summary>
    /// 申请科室编码
    /// </summary>
    [JsonProperty("applyDeptCode")]
    public string ApplyDeptCode { get; set; }

    /// <summary>
    /// 申请科室名称
    /// </summary>
    [JsonProperty("applyDeptName")]
    public string ApplyDeptName { get; set; }

    /// <summary>
    /// 规培生代码
    /// </summary>
    [JsonProperty("traineeCode")]
    public string TraineeCode { get; set; }

    /// <summary>
    /// 规培生名称
    /// </summary>
    [JsonProperty("traineeName")]
    public string TraineeName { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary>
    [JsonProperty("execDeptCode")]
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [JsonProperty("execDeptName")]
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 付费类型编码
    /// </summary>
    [JsonProperty("payTypeCode")]
    public string PayTypeCode { get; set; }

    /// <summary>
    /// 付费类型名称: 0=自费,1=医保,2=其它
    /// </summary>
    [JsonProperty("payType")]
    public ERecipePayType PayType { get; set; }

    /// <summary>
    /// 单价
    /// </summary> 
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// 总费用
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// 医保目录编码
    /// </summary>
    [JsonProperty("insuranceCode")]
    public string InsuranceCode { get; set; }

    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    [JsonProperty("insuranceType")]
    public int InsuranceType { get; set; }

    /// <summary>
    /// 是否慢性病
    /// </summary>
    [JsonProperty("isChronicDisease")]
    public bool? IsChronicDisease { get; set; }

    /// <summary>
    /// 医嘱说明
    /// </summary>
    [JsonProperty("remarks")]
    public string Remarks { get; set; }

    /// <summary>
    /// 收费类型编码
    /// </summary>
    // [JsonProperty("chargeCode")]
    // public string ChargeCode { get; set; }

    /// <summary>
    /// 收费类型名称
    /// </summary>
    // [JsonProperty("chargeName")]
    // public string ChargeName { get; set; }

    /// <summary>
    /// 收费大类代码
    /// </summary>
    [JsonProperty("feeTypeMainCode")]
    public string FeeTypeMainCode { get; set; }

    /// <summary>
    /// 收费小类代码
    /// </summary>
    [JsonProperty("feeTypeSubCode")]
    public string FeeTypeSubCode { get; set; }

    /// <summary>
    /// 医嘱类型编码（用于标记临嘱、长嘱、出院带药等）
    /// </summary>
    [JsonProperty("prescribeTypeCode")]
    public string PrescribeTypeCode { get; set; }

}
