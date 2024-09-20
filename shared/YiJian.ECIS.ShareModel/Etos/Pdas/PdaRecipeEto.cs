namespace YiJian.ECIS.ShareModel.Etos.Pdas;

/// <summary>
/// 描述：pda执行单同步eto
/// 创建人： yangkai
/// 创建时间：2022/11/28 13:52:16
/// </summary>
public class PdaRecipeEto
{
    /// <summary>
    /// 患者id
    /// </summary>
    public string PatientId { get; set; } = string.Empty;

    /// <summary>
    /// 患者住院流水号
    /// </summary>
    public string PatientNo { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱组号
    /// </summary>
    public string PlacerGroupNumber { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱组内序号
    /// </summary>
    public string PlacerOrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱父组号
    /// </summary>
    public string ParentGroupNumber { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱ID
    /// </summary>
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱项编码
    /// </summary>
    public string AdviceItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱项名称
    /// </summary>
    public string AdviceItemName { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱内容
    /// </summary>
    public string AdviceContent { get; set; } = string.Empty;

    /// <summary>
    /// 是否双人核对 1：需要双人核对 0: 无需双人核对
    /// </summary>
    public string DoubleCheckFlag { get; set; } = string.Empty;

    /// <summary>
    /// 是否需要皮试 1：需要皮试 0: 不需要皮试
    /// </summary>
    public int SkinTestFlag { get; set; }

    /// <summary>
    /// 皮试结果Code skinTestFlag为1时，皮试结果: 1:阴性 2:阳性 3:超阳性
    /// </summary>
    public string SkinTestResultCode { get; set; } = string.Empty;

    /// <summary>
    /// 皮试时间
    /// </summary>
    public DateTime? SkinTestResultDate { get; set; }

    /// <summary>
    /// 皮试人code
    /// </summary>
    public string SkinTestNurseCode { get; set; } = string.Empty;

    /// <summary>
    /// 皮试人姓名
    /// </summary>
    public string SkinTestNurseName { get; set; } = string.Empty;

    /// <summary>
    /// 频率
    /// </summary>
    public string Frequency { get; set; } = string.Empty;

    /// <summary>
    /// 剂量
    /// </summary>
    public string Dose { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// 开嘱时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 计划执行时间
    /// </summary>
    public DateTime PlanExecTime { get; set; }

    /// <summary>
    /// 给药方式
    /// </summary>
    public string DrugRoute { get; set; } = string.Empty;

    /// <summary>
    /// 给药方式名称
    /// </summary>
    public string DrugRouteName { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱类型 1：药物类 2：检验类 3：检查类 4：治疗类 5：其他类
    /// </summary>
    public string AdviceType { get; set; } = "1";

    /// <summary>
    /// 用法类型 1：服药单 2：静推单 3：输液单 4：注射单 5：治疗单 6：检查单
    /// </summary>
    public string WayType { get; set; } = string.Empty;

    /// <summary>
    /// 执行类别 1：口服 2：注射 3：输液 4:雾化 8:检验 9:治疗 10:检查
    /// </summary>
    public string ExecType { get; set; } = string.Empty;

    /// <summary>
    /// 期限标志 0、临时 1、长期
    /// </summary>
    public string TimeLimit { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱状态 1：待执行 2：核对中 3：执行中 4：已执行 5：拒执行 6：撤销执行 7：已停嘱 15：待核对
    /// </summary>
    public string OrderStatus { get; set; } = string.Empty;
}

/// <summary>
/// 用法类型枚举
/// </summary>
public enum EWayType
{
    /// <summary>
    /// 服药单
    /// </summary>
    Medicine = 1,

    /// <summary>
    /// 静推单
    /// </summary>
    StaticThrust = 2,

    /// <summary>
    /// 输液单
    /// </summary>
    Infusion = 3,

    /// <summary>
    /// 注射单
    /// </summary>
    Injection = 4,

    /// <summary>
    /// 治疗单
    /// </summary>
    Treatment = 5,

    /// <summary>
    /// 检查单
    /// </summary>
    Exam = 6
}

/// <summary>
/// 执行类别枚举
/// </summary>
public enum EExecType
{
    /// <summary>
    /// 口服
    /// </summary>
    Oral = 1,

    /// <summary>
    /// 注射
    /// </summary>
    Injection = 2,
    /// <summary>
    /// 输液单
    /// </summary>
    Infusion = 3,

    /// <summary>
    /// 雾化
    /// </summary>
    Atomization = 4
}
