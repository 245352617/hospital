using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public enum DictionariesType
    {
        [Description("列表设置")] ListSettings,
        [Description("床位设置")] BedSettings,
        [Description("区域")] Area,
        [Description("流转理由")] CirculationReasons,
        [Description("科室")] Department,
        [Description("诊断")] Diagnosis,
        [Description("出科理由")] OutDeptReason,
        [Description("危重登记")] EmergencyLevel,
        [Description("重点病种")] KeyDiseases,

    }
}