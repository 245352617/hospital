using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 医学类型
    /// </summary>
    public enum MedicalTypeEnum
    {
        [Description("西医")] WesternMedicine = 1,
        [Description("中医")] ChineseMedicine = 2
    }
}