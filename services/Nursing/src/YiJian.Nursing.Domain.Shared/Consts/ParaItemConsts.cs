namespace YiJian.Nursing
{
    /// <summary>
    /// 表:护理项目表 表字段长度常量
    /// </summary>
    public class ParaItemConsts
    {
        /// <summary>
        /// 科室编号(20)
        /// </summary>
        public static int MaxDeptCodeLength { get; set; } = 20;
        /// <summary>
        /// 参数所属模块(20)
        /// </summary>
        public static int MaxModuleCodeLength { get; set; } = 20;
        /// <summary>
        /// 项目代码(20)
        /// </summary>
        public static int MaxParaCodeLength { get; set; } = 20;
        /// <summary>
        /// 项目名称(80)
        /// </summary>
        public static int MaxParaNameLength { get; set; } = 80;
        /// <summary>
        /// 显示名称(80)
        /// </summary>
        public static int MaxDisplayNameLength { get; set; } = 80;
        /// <summary>
        /// 评分代码(50)
        /// </summary>
        public static int MaxScoreCodeLength { get; set; } = 50;
        /// <summary>
        /// 导管分类(40)
        /// </summary>
        public static int MaxGroupNameLength { get; set; } = 40;
        /// <summary>
        /// 单位名称(20)
        /// </summary>
        public static int MaxUnitNameLength { get; set; } = 20;
        /// <summary>
        /// 数据类型(20)
        /// </summary>
        public static int MaxValueTypeLength { get; set; } = 20;
        /// <summary>
        /// 文本类型(20)
        /// </summary>
        public static int MaxStyleLength { get; set; } = 20;
        /// <summary>
        /// 小数位数(20)
        /// </summary>
        public static int MaxDecimalDigitsLength { get; set; } = 20;
        /// <summary>
        /// 最大值(20)
        /// </summary>
        public static int MaxMaxValueLength { get; set; } = 20;
        /// <summary>
        /// 最小值(20)
        /// </summary>
        public static int MaxMinValueLength { get; set; } = 20;
        /// <summary>
        /// 高值(20)
        /// </summary>
        public static int MaxHighValueLength { get; set; } = 20;
        /// <summary>
        /// 低值(20)
        /// </summary>
        public static int MaxLowValueLength { get; set; } = 20;
        /// <summary>
        /// 默认值(10)
        /// </summary>
        public static int MaxDataSourceLength { get; set; } = 10;
        /// <summary>
        /// 导管项目是否静态显示(1)
        /// </summary>
        public static int MaxDictFlagLength { get; set; } = 1;
        /// <summary>
        /// 评分指引编号(50)
        /// </summary>
        public static int MaxGuidIdLength { get; set; } = 50;
        /// <summary>
        /// 特护单统计参数分类(20)
        /// </summary>
        public static int MaxStatisticalTypeLength { get; set; } = 20;
        /// <summary>
        /// 关联颜色(20)
        /// </summary>
        public static int MaxColorParaCodeLength { get; set; } = 20;
        /// <summary>
        /// 关联颜色名称(255)
        /// </summary>
        public static int MaxColorParaNameLength { get; set; } = 255;
        /// <summary>
        /// 关联性状(20)
        /// </summary>
        public static int MaxPropertyParaCodeLength { get; set; } = 20;
        /// <summary>
        /// 关联性状名称(255)
        /// </summary>
        public static int MaxPropertyParaNameLength { get; set; } = 255;
        /// <summary>
        /// 设备参数代码(40)
        /// </summary>
        public static int MaxDeviceParaCodeLength { get; set; } = 40;
        /// <summary>
        /// 设备参数类型（1-测量值，2-设定值）(10)
        /// </summary>
        public static int MaxDeviceParaTypeLength { get; set; } = 10;
        /// <summary>
        /// 知识库代码(20)
        /// </summary>
        public static int MaxKBCodeLength { get; set; } = 20;
        /// <summary>
        /// 护理概览参数(20)
        /// </summary>
        public static int MaxNuringViewCodeLength { get; set; } = 20;
        /// <summary>
        /// 是否异常体征参数(1)
        /// </summary>
        public static int MaxAbnormalSignLength { get; set; } = 1;
        /// <summary>
        /// 添加来源(20)
        /// </summary>
        public static int MaxAddSourceLength { get; set; } = 20;
    }
}