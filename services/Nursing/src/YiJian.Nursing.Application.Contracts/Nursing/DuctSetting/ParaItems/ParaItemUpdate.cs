using System;
using System.Collections.Generic;
using Volo.Abp.Validation;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:护理项目表 修改输入
    /// </summary>
    [Serializable]
    public class ParaItemUpdate
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxDeptCodeLength),
            ErrorMessage = "科室编号最大长度不能超过{1}!")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 参数所属模块
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxModuleCodeLength),
            ErrorMessage = "参数所属模块最大长度不能超过{1}!")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxParaCodeLength),
            ErrorMessage = "项目代码最大长度不能超过{1}!")]
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxParaNameLength),
            ErrorMessage = "项目名称最大长度不能超过{1}!")]
        public string ParaName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxDisplayNameLength),
            ErrorMessage = "显示名称最大长度不能超过{1}!")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 评分代码
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxScoreCodeLength),
            ErrorMessage = "评分代码最大长度不能超过{1}!")]
        public string ScoreCode { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxGroupNameLength),
            ErrorMessage = "导管分类最大长度不能超过{1}!")]
        public string GroupName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxUnitNameLength),
            ErrorMessage = "单位名称最大长度不能超过{1}!")]
        public string UnitName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxValueTypeLength),
            ErrorMessage = "数据类型最大长度不能超过{1}!")]
        public string ValueType { get; set; }

        /// <summary>
        /// 文本类型
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxStyleLength),
            ErrorMessage = "文本类型最大长度不能超过{1}!")]
        public string Style { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxDecimalDigitsLength),
            ErrorMessage = "小数位数最大长度不能超过{1}!")]
        public string DecimalDigits { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxMaxValueLength),
            ErrorMessage = "最大值最大长度不能超过{1}!")]
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxMinValueLength),
            ErrorMessage = "最小值最大长度不能超过{1}!")]
        public string MinValue { get; set; }

        /// <summary>
        /// 高值
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxHighValueLength),
            ErrorMessage = "高值最大长度不能超过{1}!")]
        public string HighValue { get; set; }

        /// <summary>
        /// 低值
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxLowValueLength),
            ErrorMessage = "低值最大长度不能超过{1}!")]
        public string LowValue { get; set; }

        /// <summary>
        /// 是否预警
        /// </summary>
        public bool? Threshold { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxDataSourceLength),
            ErrorMessage = "默认值最大长度不能超过{1}!")]
        public string DataSource { get; set; }

        /// <summary>
        /// 导管项目是否静态显示
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxDictFlagLength),
            ErrorMessage = "导管项目是否静态显示最大长度不能超过{1}!")]
        public string DictFlag { get; set; }

        /// <summary>
        /// 是否评分
        /// </summary>
        public bool? GuidFlag { get; set; }

        /// <summary>
        /// 评分指引编号
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxGuidIdLength),
            ErrorMessage = "评分指引编号最大长度不能超过{1}!")]
        public string GuidId { get; set; }

        /// <summary>
        /// 特护单统计参数分类
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxStatisticalTypeLength),
            ErrorMessage = "特护单统计参数分类最大长度不能超过{1}!")]
        public string StatisticalType { get; set; }

        /// <summary>
        /// 绘制趋势图形
        /// </summary>
        public int DrawChartFlag { get; set; }

        /// <summary>
        /// 关联颜色
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxColorParaCodeLength),
            ErrorMessage = "关联颜色最大长度不能超过{1}!")]
        public string ColorParaCode { get; set; }

        /// <summary>
        /// 关联颜色名称
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxColorParaNameLength),
            ErrorMessage = "关联颜色名称最大长度不能超过{1}!")]
        public string ColorParaName { get; set; }

        /// <summary>
        /// 关联性状
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxPropertyParaCodeLength),
            ErrorMessage = "关联性状最大长度不能超过{1}!")]
        public string PropertyParaCode { get; set; }

        /// <summary>
        /// 关联性状名称
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxPropertyParaNameLength),
            ErrorMessage = "关联性状名称最大长度不能超过{1}!")]
        public string PropertyParaName { get; set; }

        /// <summary>
        /// 设备参数代码
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxDeviceParaCodeLength),
            ErrorMessage = "设备参数代码最大长度不能超过{1}!")]
        public string DeviceParaCode { get; set; }

        /// <summary>
        /// 设备参数类型（1-测量值，2-设定值）
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxDeviceParaTypeLength),
            ErrorMessage = "设备参数类型（1-测量值，2-设定值）最大长度不能超过{1}!")]
        public string DeviceParaType { get; set; }

        /// <summary>
        /// 是否生命体征项目
        /// </summary>
        public int? HealthSign { get; set; }

        /// <summary>
        /// 知识库代码
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxKBCodeLength),
            ErrorMessage = "知识库代码最大长度不能超过{1}!")]
        public string KBCode { get; set; }

        /// <summary>
        /// 护理概览参数
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxNuringViewCodeLength),
            ErrorMessage = "护理概览参数最大长度不能超过{1}!")]
        public string NuringViewCode { get; set; }

        /// <summary>
        /// 是否异常体征参数
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxAbnormalSignLength),
            ErrorMessage = "是否异常体征参数最大长度不能超过{1}!")]
        public string AbnormalSign { get; set; }

        /// <summary>
        /// 是否用药所属项目
        /// </summary>
        public bool? IsUsage { get; set; }

        /// <summary>
        /// 添加来源
        /// </summary>
        [DynamicStringLength(typeof(ParaItemConsts), nameof(ParaItemConsts.MaxAddSourceLength),
            ErrorMessage = "添加来源最大长度不能超过{1}!")]
        public string AddSource { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 项目参数类型，用于区分监护仪或者呼吸机等
        /// </summary>
        public DeviceTypeEnum ParaItemType { get; set; }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DictUpdate> CreateUpdateDictDtos { get; set; }
    }
}