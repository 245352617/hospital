namespace HisApiMockService.Models.Medicals;


public class LisBacAntiItemResponse
{
    /// <summary>
    /// 抗生素编号
    /// </summary>
    public string AntCode { get; set; }

    /// <summary>
    /// 抗生素中文名
    /// </summary>
    public string AntChName { get; set; }

    /// <summary>
    /// 抗生素英文名
    /// </summary>
    public string AntEnName { get; set; }

    /// <summary>
    /// 抗生素定性结果
    /// </summary>
    public string AntResultDes { get; set; }

    /// <summary>
    /// 抗生素定量结果
    /// </summary>
    public string AntResultNum { get; set; }

    /// <summary>
    /// 抗生素参考剂量
    /// </summary>
    public string AntResultRange { get; set; }

    /// <summary>
    /// 耐药范围
    /// </summary>
    public string Rrange { get; set; }

    /// <summary>
    /// 敏感范围
    /// </summary>
    public string Srange { get; set; }

    /// <summary>
    /// 中介范围
    /// </summary>
    public string Irange { get; set; }

    /// <summary>
    /// 耐药标志
    /// </summary>
    public string Rflag { get; set; }

    /// <summary>
    /// 敏感标志
    /// </summary>
    public string Sflag { get; set; }

    /// <summary>
    /// 中介标志
    /// </summary>
    public string Iflag { get; set; }

    /// <summary>
    /// 无标志
    /// </summary>
    public string Nflag { get; set; }

    /// <summary>
    /// 测试方法
    /// </summary>
    public string TestMethod { get; set; }

    /// <summary>
    /// MIC值
    /// </summary>
    public string Mic { get; set; }

    /// <summary>
    /// SDD范围
    /// </summary>
    public string SddRef { get; set; }

    /// <summary>
    /// 参考用量
    /// </summary>
    public string DosageRef { get; set; }

    /// <summary>
    /// 用法及剂量
    /// </summary>
    public string UseDosage { get; set; }

    /// <summary>
    /// 抑菌环直径
    /// </summary>
    public string BacterCircleDia { get; set; }
}
