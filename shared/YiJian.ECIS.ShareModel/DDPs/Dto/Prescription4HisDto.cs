using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Dto;


/// <summary>
/// 处方Dto（可理解成分方后的一张单，即打印后的一张纸）
/// </summary>
public class Prescription4HisDto
{
    /// <summary>
    /// 内部分方方号
    /// </summary>
    [JsonProperty("prescriptionNo")]
    public string PrescriptionNo { get; set; }

    /// <summary>
    /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
    /// </summary> 
    [JsonProperty("itemType")]
    public int ItemType { get; set; }

    /// <summary>
    /// lis（检验）集合
    /// </summary>
    [JsonProperty("lisList")]
    public List<Lis4HisDto> LisList { get; set; }

    /// <summary>
    /// pas（检查）集合
    /// </summary>
    [JsonProperty("pacsList")]
    public List<Pacs4HisDto> PacsList { get; set; }

    /// <summary>
    /// 药品处方集合
    /// </summary>
    [JsonProperty("prescribeList")]
    public List<Prescribe4HisDto> PrescribeList { get; set; }

    /// <summary>
    /// 处置集合
    /// </summary>
    [JsonProperty("treatList")]
    public List<Treat4HisDto> TreatList { get; set; }
}