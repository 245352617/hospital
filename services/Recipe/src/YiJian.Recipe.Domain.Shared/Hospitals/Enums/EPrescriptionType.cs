namespace YiJian.Hospitals.Enums
{
    /// <summary>
    /// 0.普通处方 
    /// 1.危急处方 
    /// 2.精神处方   
    /// 3.麻醉处方
    /// </summary>
    public enum EPrescriptionType
    {
        /// <summary>
        /// 0.普通处方
        /// </summary>
        PutongChufang = 0,

        /// <summary>
        /// 1.危急处方
        /// </summary>
        WeijiChufang = 1,

        /// <summary>
        /// 2.精神处方
        /// </summary>
        JingshenChufang = 2,

        /// <summary>
        /// 3.麻醉处方
        /// </summary>
        MazuiChufang = 3,
    }

}
