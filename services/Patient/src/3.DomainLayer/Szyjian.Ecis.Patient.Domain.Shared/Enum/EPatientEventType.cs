using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 描述：pda患者信息同步etoPatientEvent字段枚举
    /// 创建人： yangkai
    /// 创建时间：2022/11/28 10:14:18
    /// </summary>
    public enum EPatientEventType
    {
        /// <summary>
        /// 住院登记
        /// </summary>
        [Description("住院登记")]
        ToHospital = 1,

        /// <summary>
        /// 转科
        /// </summary>
        [Description("转科")]
        TransferDept = 2,

        /// <summary>
        /// 住院结算
        /// </summary>
        [Description("住院结算")]
        HospitalSettlement = 3,

        /// <summary>
        /// 门诊登记
        /// </summary>
        [Description("门诊登记")]
        Outpatient = 4,

        /// <summary>
        /// 入科
        /// </summary>
        [Description("入科")]
        InDept = 10,

        /// <summary>
        /// 取消门诊、住院登记
        /// </summary>
        [Description("取消门诊、住院登记")]
        CancelOutpatient = 11,

        /// <summary>
        /// 取消住院结算
        /// </summary>
        [Description("取消住院结算")]
        CancelHospitalSettlement = 13,

        /// <summary>
        /// 出科
        /// </summary>
        [Description("出科")]
        OutDept = 16,

        /// <summary>
        /// 取消出科
        /// </summary>
        [Description("取消出科")]
        CancelOutDept = 25,

        /// <summary>
        /// 增加诊断
        /// </summary>
        [Description("增加诊断")]
        AddDiagnose = 31,

        /// <summary>
        /// 取消入科
        /// </summary>
        [Description("取消入科")]
        CancelInDept = 32,

        /// <summary>
        /// 借床
        /// </summary>
        [Description("借床")]
        BorrowBed = 33,

        /// <summary>
        /// 换床、包床
        /// </summary>
        [Description("换床、包床")]
        ChangeBed = 42,

        /// <summary>
        /// 更改患者信息
        /// </summary>
        [Description("更改患者信息")]
        UpdatePatient = 99
    }
}
