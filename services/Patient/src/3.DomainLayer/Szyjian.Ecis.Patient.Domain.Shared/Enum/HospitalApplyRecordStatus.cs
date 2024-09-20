using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public enum HospitalApplyRecordStatus
    {
        [Description("作废")] 作废 = -1,
        [Description("已申请")] 已申请 = 0
    }
}