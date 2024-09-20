using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 时间节点枚举
    /// </summary>
    public enum TimePoint
    {
        /// <summary>
        /// 分诊时间
        /// </summary>
        [Description("分诊")] TriageTime = 0,

        /// <summary>
        /// 就诊时间
        /// </summary>
        [Description("就诊")] VisitTime = 1,

        /// <summary>
        /// 入科时间
        /// </summary>
        [Description("入科时间")] InDeptTime = 2,

        /// <summary>
        /// 开立首条处方时间
        /// </summary>
        [Description("开立首条处方")] FirstPrescriptionTime = 3,

        /// <summary>
        /// 开立首条检验时间
        /// </summary>
        [Description("开立首条检验")] FirstLabTime = 4,

        /// <summary>
        /// 开立首条检查时间
        /// </summary>
        [Description("开立首条检查")] FirstInspectTime = 5,

        /// <summary>
        /// 首条检验报告时间
        /// </summary>
        [Description("首条检验报告")] FirstLabReportTime = 6,

        /// <summary>
        /// 首条检查报告时间
        /// </summary>
        [Description("首条检查报告")] FirstInspectReportTime = 7,

        /// <summary>
        /// 转就诊区时间
        /// </summary>
        [Description("转就诊区")] ToOutpatientAreaTime = 8,

        /// <summary>
        /// 转留观区时间
        /// </summary>
        [Description("转留观区")] ToObservationAreaTime = 9,

        /// <summary>
        /// 转抢救区时间
        /// </summary>
        [Description("转抢救区")] ToRescueAreaTime = 10,

        /// <summary>
        /// 转住院时间
        /// </summary>
        [Description("转住院")] ToHospitalTime = 11,


        /// <summary>
        /// 结束就诊时间
        /// </summary>
        [Description("结束就诊")] EndVisitTime = 12,

        /// <summary>
        /// 出科时间
        /// </summary>
        [Description("出科")] OutDeptTime = 13,

        /// <summary>
        /// 挂号时间
        /// </summary>
        [Description("挂号时间")] RegisterTime = 14,


        /// <summary>
        /// 取消挂号时间
        /// </summary>
        [Description("取消挂号时间")] BackNumberTime = 16,

        /// <summary>
        /// 死亡时间
        /// </summary>
        [Description("死亡时间")] DeathTime = 17,

        /// <summary>
        /// 手术申请时间
        /// </summary>
        [Description("手术申请时间")] ApplyOperationTime = 18,

        /// <summary>
        /// 开始接诊时间
        /// </summary>
        [Description("开始接诊时间")] StartVisitTime = 19,

        /// <summary>
        /// 会诊申请时间
        /// </summary>
        [Description("会诊申请时间")] GroupConsultationApplyTime = 21,

        /// <summary>
        /// 会诊时间
        /// </summary>
        [Description("会诊时间")] GroupConsultationTime = 22,


        /// <summary>
        ///检验提交时间
        /// </summary>
        [Description("检验提交时间")] TestTime = 23,

        /// <summary>
        /// 检查提交时间
        /// </summary>
        [Description("检查提交时间")] InspectTime = 24,

        /// <summary>
        /// 医嘱提交时间
        /// </summary>
        [Description("医嘱提交时间")] DrugsTime = 25,
        /// <summary>
        /// 退号时间
        /// </summary>
        [Description("退号时间")] CancellationTime = 26,

        /// <summary>
        /// 召回时间
        /// </summary>
        [Description("召回时间")] ReCallVisit = 27,


        /// <summary>
        /// 退回候诊
        /// </summary>
        [Description("退回候诊")] ReWaitingVisit = 28,

        /// <summary>
        /// 过号时间
        /// </summary>
        [Description("过号时间")] ExpireTime = 29,

        /// <summary>
        /// 超时结束
        /// </summary>
        [Description("超时结束")] AutoEndVisitTime = 30,

        /// <summary>
        /// 超时过号
        /// </summary>
        [Description("超时过号")] AutoExpireTime = 31,

    }
}