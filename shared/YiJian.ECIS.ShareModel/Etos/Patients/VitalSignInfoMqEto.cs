namespace YiJian.ECIS.ShareModel.Etos.Patients;

/// <summary>
/// 生命体征同步
/// </summary>
public class VitalSignInfoMqEto
{
    /// <summary>
    /// VitalSignInfo表主键Id  不需要前端传值
    /// </summary>
    public Guid PI_ID { get; set; }

    /// <summary>
    /// 收缩压
    /// </summary>
    public int? Sbp { get; set; }

    /// <summary>
    /// 舒张压
    /// </summary>
    public int? Sdp { get; set; }

    /// <summary>
    /// 血氧饱和度
    /// </summary>
    public decimal? SpO2 { get; set; }

    /// <summary>
    /// 呼吸
    /// </summary>
    public int? BreathRate { get; set; }

    /// <summary>
    /// 体温
    /// </summary>
    public decimal? Temp { get; set; }

    /// <summary>
    /// 心率
    /// </summary>
    public int? HeartRate { get; set; }

    /// <summary>
    /// 血糖（单位 mmol/L）
    /// </summary>
    public float? BloodGlucose { get; set; }

    /// <summary>
    /// 体重
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// 脉搏
    /// </summary>
    public int? Pulse { get; set; }

    /// <summary>
    /// 备注Code
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 备注名称
    /// </summary>
    public string RemarkName { get; set; }

    /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; }

    /// <summary>
    /// 心电图 Code
    /// </summary>
    public string CardiogramCode { get; set; }

    /// <summary>
    /// 心电图 名称
    /// </summary>
    public string CardiogramName { get; set; }

    /// <summary>
    /// 操作人Id
    /// </summary>
    public Guid CreatorId { get; set; }

    /// <summary>
    /// 操作人编码
    /// </summary>
    public string OperationCode { get; set; }

    /// <summary>
    /// 操作人名称
    /// </summary>
    public string OperationName { get; set; }

    /// <summary>
    /// 登陆token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// 签名图片
    /// </summary>
    public string Signature { get; set; } = string.Empty;
}