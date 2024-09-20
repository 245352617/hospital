using System;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Exams;


/// <summary>
/// 检查明细项 读取输出
/// </summary>
[Serializable]
public class ExamTargetData
{
    public int Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string TargetCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string TargetName { get; set; }

    /// <summary>
    /// 项目编码
    /// </summary>
    public string ProjectCode { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string TargetUnit { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public decimal Qty { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 其它价格
    /// </summary>
    public decimal OtherPrice { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    public string Specification { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    public string WbCode { get; set; }

    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    public InsuranceCatalog InsuranceType { get; set; }

    /// <summary>
    /// 特殊标识
    /// </summary>
    public string SpecialFlag { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// 项目类型
    /// </summary>
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目归类
    /// </summary>
    public string ProjectMerge { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    public string MeducalInsuranceCode { get; set; }

    /// <summary>
    /// 医保二级编码
    /// </summary>
    public string YBInneCode { get; set; }


}