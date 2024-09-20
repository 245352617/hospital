namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 1.诊断 2.就诊记录 3.医嘱
    /// </summary>
    public enum ERecordType
    {
        /// <summary>
        /// 1.诊断
        /// </summary>
        Diagnosis = 1,

        /// <summary>
        /// 2.就诊记录
        /// </summary>
        MedicalRecords = 2,

        /// <summary>
        /// 3.医嘱
        /// </summary>
        DoctorsAdvice = 3,

    }
}
