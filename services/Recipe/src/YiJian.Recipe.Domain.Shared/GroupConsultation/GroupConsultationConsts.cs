namespace YiJian.Recipes.GroupConsultation
{
    /// <summary>
    /// 会诊管理 表字段长度常量
    /// </summary>
    public class GroupConsultationConsts
    {
        /// <summary>
        /// 患者id(4000)
        /// </summary>
        public static int MaxPatientIdLength { get; set; } = 4000;
        /// <summary>
        /// 会诊类型(50)
        /// </summary>
        public static int MaxTypeCodeLength { get; set; } = 50;
        public static int MaxTypeNameLength { get; set; } = 100;
        /// <summary>
        /// 会诊目的编码(50)
        /// </summary>
        public static int MaxObjectiveCodeLength { get; set; } = 50;
        /// <summary>
        /// 会诊目的内容(200)
        /// </summary>
        public static int MaxObjectiveContentLength { get; set; } = 200;
        /// <summary>
        /// 申请科室编码(50)
        /// </summary>
        public static int MaxApplyDeptCodeLength { get; set; } = 50;
        /// <summary>
        /// 申请科室名称(100)
        /// </summary>
        public static int MaxApplyDeptNameLength { get; set; } = 100;
        /// <summary>
        /// 申请人编码(50)
        /// </summary>
        public static int MaxApplyCodeLength { get; set; } = 50;
        /// <summary>
        /// 申请人名称(100)
        /// </summary>
        public static int MaxApplyNameLength { get; set; } = 100;
        /// <summary>
        /// 联系方式(20)
        /// </summary>
        public static int MaxMobileLength { get; set; } = 20;
        /// <summary>
        /// 地点(50)
        /// </summary>
        public static int MaxPlaceLength { get; set; } = 50;

    }
}