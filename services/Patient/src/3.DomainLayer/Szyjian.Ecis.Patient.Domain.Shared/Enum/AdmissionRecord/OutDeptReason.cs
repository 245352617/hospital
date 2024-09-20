namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 出科原因枚举
    /// </summary>
    public enum OutDeptReason
    {
        正常出科 = 0,
        转住院 = 1,
        死亡 = 2,
        转输液区 = 3,
        其他 = 4
    }
}