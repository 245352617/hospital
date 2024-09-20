namespace YiJian.Nursing
{

    /// <summary>
    /// 表:模块参数 表字段长度常量
    /// </summary>
    public class ParaModuleConsts
    {
        /// <summary>
        /// 模块代码(50)
        /// </summary>
        public static int MaxModuleCodeLength { get; set; } = 50;
        /// <summary>
        /// 模块名称(80)
        /// </summary>
        public static int MaxModuleNameLength { get; set; } = 80;
        /// <summary>
        /// 模块显示名称(80)
        /// </summary>
        public static int MaxDisplayNameLength { get; set; } = 80;
        /// <summary>
        /// 科室代码(20)
        /// </summary>
        public static int MaxDeptCodeLength { get; set; } = 20;
        /// <summary>
        /// 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）(20)
        /// </summary>
        public static int MaxModuleTypeLength { get; set; } = 20;
        /// <summary>
        /// 模块拼音(40)
        /// </summary>
        public static int MaxPyLength { get; set; } = 40;
    }
}