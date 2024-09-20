namespace YiJian.Nursing
{
    /// <summary>
    /// 表:导管字典-通用业务 表字段长度常量
    /// </summary>
    public class DictConsts
    {
        /// <summary>
        /// 参数代码(20)
        /// </summary>
        public static int MaxParaCodeLength { get; set; } = 20;
        /// <summary>
        /// 参数名称(40)
        /// </summary>
        public static int MaxParaNameLength { get; set; } = 40;
        /// <summary>
        /// 字典代码(20)
        /// </summary>
        public static int MaxDictCodeLength { get; set; } = 20;
        /// <summary>
        /// 字典值(80)
        /// </summary>
        public static int MaxDictValueLength { get; set; } = 80;
        /// <summary>
        /// 字典值说明(200)
        /// </summary>
        public static int MaxDictDescLength { get; set; } = 200;
        /// <summary>
        /// 上级代码(20)
        /// </summary>
        public static int MaxParentIdLength { get; set; } = 20;
        /// <summary>
        /// 字典标准（国标、自定义）(20)
        /// </summary>
        public static int MaxDictStandardLength { get; set; } = 20;
        /// <summary>
        /// HIS对照代码(20)
        /// </summary>
        public static int MaxHisCodeLength { get; set; } = 20;
        /// <summary>
        /// HIS对照(40)
        /// </summary>
        public static int MaxHisNameLength { get; set; } = 40;
        /// <summary>
        /// 科室代码(20)
        /// </summary>
        public static int MaxDeptCodeLength { get; set; } = 20;
        /// <summary>
        /// 模块代码(20)
        /// </summary>
        public static int MaxModuleCodeLength { get; set; } = 20;
    }
}