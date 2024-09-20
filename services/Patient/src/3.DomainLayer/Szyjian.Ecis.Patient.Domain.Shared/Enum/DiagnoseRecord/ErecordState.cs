namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 1.作废（recordType=1，3时使用）
    /// 2.结束就诊 （ recordType=2）
    /// 3.暂挂（ recordType=2）
    /// </summary>
    public enum ErecordState
    {
        /// <summary>
        /// 1.诊断作废/结束就诊（recordType=1/2）
        /// </summary>
        InvalidOrEnd = 1,

        // /// <summary>
        // /// 2.处方、治疗项目作废
        // /// </summary>
        // End = 2,
        //
        // /// <summary>
        // /// 3.检验、检查组套作废（recordType=3）
        // /// </summary>
        // Pause = 3,
        //
        /// <summary>
        /// 4.就诊记录召回(recordType = 2，24小时内可以使用)
        /// 结束就诊的患者，改变状态到正在就诊
        /// </summary>
        ReCall = 4,

        /// <summary>
        /// 开启绿色通道(recordType=2)
        /// </summary>
        GreenChannlOn = 6,

        /// <summary>
        /// 关闭绿色通道(recordType = 2)
        /// </summary>
        GreenChannlOff = 7,

        /// <summary>
        /// 4.返回候诊区(recordType = 2)
        /// 过号的患者，返回叫号队列
        /// </summary>
        ReturnVisit = 8,

        /// <summary>
        /// 5.过号(recordType = 5)
        /// </summary>
        OutQueue = 5,
    }
}