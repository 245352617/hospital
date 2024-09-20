using System;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:护理项目表 读取输出
    /// </summary>
    [Serializable]
    public class ParaItemData
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 参数所属模块
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 评分代码
        /// </summary>
        public string ScoreCode { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 文本类型
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public string DecimalDigits { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue { get; set; }

        /// <summary>
        /// 高值
        /// </summary>
        public string HighValue { get; set; }

        /// <summary>
        /// 低值
        /// </summary>
        public string LowValue { get; set; }

        /// <summary>
        /// 是否预警
        /// </summary>
        public bool Threshold { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 导管项目是否静态显示
        /// </summary>
        public string DictFlag { get; set; }

        /// <summary>
        /// 是否评分
        /// </summary>
        public bool? GuidFlag { get; set; }

        /// <summary>
        /// 评分指引编号
        /// </summary>
        public string GuidId { get; set; }

        /// <summary>
        /// 特护单统计参数分类
        /// </summary>
        public string StatisticalType { get; set; }

        /// <summary>
        /// 绘制趋势图形
        /// </summary>
        public int DrawChartFlag { get; set; }

        /// <summary>
        /// 关联颜色
        /// </summary>
        public string ColorParaCode { get; set; }

        /// <summary>
        /// 关联颜色名称
        /// </summary>
        public string ColorParaName { get; set; }

        /// <summary>
        /// 关联性状
        /// </summary>
        public string PropertyParaCode { get; set; }

        /// <summary>
        /// 关联性状名称
        /// </summary>
        public string PropertyParaName { get; set; }

        /// <summary>
        /// 设备参数代码
        /// </summary>
        public string DeviceParaCode { get; set; }

        /// <summary>
        /// 设备参数类型（1-测量值，2-设定值）
        /// </summary>
        public string DeviceParaType { get; set; }

        /// <summary>
        /// 是否生命体征项目
        /// </summary>
        public int? HealthSign { get; set; }

        /// <summary>
        /// 知识库代码
        /// </summary>
        public string KBCode { get; set; }

        /// <summary>
        /// 护理概览参数
        /// </summary>
        public string NuringViewCode { get; set; }

        /// <summary>
        /// 是否异常体征参数
        /// </summary>
        public string AbnormalSign { get; set; }

        /// <summary>
        /// 是否用药所属项目
        /// </summary>
        public bool? IsUsage { get; set; }

        /// <summary>
        /// 添加来源
        /// </summary>
        public string AddSource { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 项目参数类型，用于区分监护仪或者呼吸机等
        /// </summary>
        public DeviceTypeEnum ParaItemType { get; set; }
    }
}