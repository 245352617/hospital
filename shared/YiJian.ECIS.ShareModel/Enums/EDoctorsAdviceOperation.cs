using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 医嘱和医嘱执行动作类型枚举
/// </summary>
public enum EDoctorsAdviceOperation
{
    /// <summary>
    /// 0.未知
    /// </summary>
    [Description("未知")]
    Unknown = 0,

    #region 医生操作

    /// <summary>
    /// 1.开嘱
    /// </summary>
    [Description("开嘱")]
    Submit = 1,

    /// <summary>
    /// 3.作废
    /// </summary>
    [Description("作废")]
    Cancel = 3,

    /// <summary>
    /// 4.停嘱
    /// </summary>
    [Description("停嘱")]
    Stop = 4,

    #endregion  医生操作


    #region 护士操作

    /// <summary>
    /// 2.确认（复核）
    /// </summary>
    [Description("复核")]
    Confirm = 2,

    /// <summary>
    /// 6.驳回
    /// </summary>
    [Description("驳回")]
    Reject = 6,

    /// <summary>
    /// 7.执行
    /// </summary>
    [Description("执行")]
    Execute = 7,

    /// <summary>
    /// 9.停复核
    /// </summary>
    [Description("停复核")]
    StopConfirm = 9,

    /// <summary>
    /// 12.核对
    /// </summary>
    [Description("核对")]
    Check = 12,

    /// <summary>
    /// 13.拒绝
    /// </summary>
    [Description("拒绝")]
    Refuse = 13,

    /// <summary>
    /// 14.取消执行
    /// </summary>
    [Description("取消执行")]
    UndoRefuse = 14,

    /// <summary>
    /// 15.暂停，输液专用
    /// </summary>
    [Description("暂停")]
    Suspend = 15,

    /// <summary>
    /// 16.继续，输液专用
    /// </summary>
    [Description("继续")]
    Continue = 16,

    /// <summary>
    /// 17.完成，输液专用
    /// </summary>
    [Description("完成")]
    Complete = 17,

    /// <summary>
    /// 18.配药，输液专用
    /// </summary>
    [Description("配药")]
    Dispense = 18,

    /// <summary>
    /// 19.开始，输液专用
    /// </summary>
    [Description("开始")]
    StartInfusion = 19,

    /// <summary>
    /// 20.皮试，阳性或者阴性
    /// </summary>
    [Description("皮试")]
    SkinTest = 20

    #endregion  护士操作
}
