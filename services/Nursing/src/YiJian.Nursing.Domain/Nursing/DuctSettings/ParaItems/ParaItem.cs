using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:护理项目表（基础配置表）
    /// </summary>
    [Comment("表:护理项目表（基础配置表）")]
    public class ParaItem : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 科室编号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("科室编号")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 参数所属模块
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("参数所属模块")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        [StringLength(20)]
        [Required]
        [Comment("项目代码")]
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(80)]
        [Required]
        [Comment("项目名称")]
        public string ParaName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [CanBeNull]
        [StringLength(80)]
        [Comment("显示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 评分代码
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        [Comment("评分代码")]
        public string ScoreCode { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("导管分类")]
        public string GroupName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("单位名称")]
        public string UnitName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("数据类型")]
        public string ValueType { get; set; }

        /// <summary>
        /// 文本类型
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("文本类型")]
        public string Style { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("小数位数")]
        public string DecimalDigits { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("最大值")]
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("最小值")]
        public string MinValue { get; set; }

        /// <summary>
        /// 高值
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("高值")]
        public string HighValue { get; set; }

        /// <summary>
        /// 低值
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("低值")]
        public string LowValue { get; set; }

        /// <summary>
        /// 是否预警
        /// </summary>
        [Comment("是否预警")]
        public bool Threshold { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Comment("排序号")]
        public int Sort { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        [Comment("默认值")]
        public string DataSource { get; set; }

        /// <summary>
        /// 导管项目是否静态显示
        /// </summary>
        [CanBeNull]
        [StringLength(1)]
        [Comment("导管项目是否静态显示")]
        public string DictFlag { get; set; }

        /// <summary>
        /// 是否评分
        /// </summary>
        [Comment("是否评分")]
        public bool? GuidFlag { get; set; }

        /// <summary>
        /// 评分指引编号
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        [Comment("评分指引编号")]
        public string GuidId { get; set; }

        /// <summary>
        /// 特护单统计参数分类
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("特护单统计参数分类")]
        public string StatisticalType { get; set; }

        /// <summary>
        /// 绘制趋势图形
        /// </summary>
        [Required]
        [Comment("绘制趋势图形")]
        public int DrawChartFlag { get; set; }

        /// <summary>
        /// 关联颜色
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("关联颜色")]
        public string ColorParaCode { get; set; }

        /// <summary>
        /// 关联颜色名称
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        [Comment("关联颜色名称")]
        public string ColorParaName { get; set; }

        /// <summary>
        /// 关联性状
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("关联性状")]
        public string PropertyParaCode { get; set; }

        /// <summary>
        /// 关联性状名称
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        [Comment("关联性状名称")]
        public string PropertyParaName { get; set; }


        /// <summary>
        /// 设备参数代码
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("设备参数代码")]
        public string DeviceParaCode { get; set; }


        /// <summary>
        /// 设备参数类型（1-测量值，2-设定值）
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        [Comment("设备参数类型（1-测量值，2-设定值）")]
        public string DeviceParaType { get; set; }

        /// <summary>
        /// 是否生命体征项目
        /// </summary>
        [Comment("是否生命体征项目")]
        public int? HealthSign { get; set; }

        /// <summary>
        /// 知识库代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("知识库代码")]
        public string KBCode { get; set; }

        /// <summary>
        /// 护理概览参数
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("护理概览参数")]
        public string NuringViewCode { get; set; }

        /// <summary>
        /// 是否异常体征参数
        /// </summary>
        [CanBeNull]
        [StringLength(1)]
        [Comment("是否异常体征参数")]
        public string AbnormalSign { get; set; }


        /// <summary>
        /// 是否用药所属项目
        /// </summary>
        [Comment("是否用药所属项目")]
        public bool? IsUsage { get; set; }

        /// <summary>
        /// 添加来源
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("添加来源")]
        public string AddSource { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 项目参数类型，用于区分监护仪或者呼吸机等
        /// </summary>
        [Comment("项目参数类型，用于区分监护仪或者呼吸机等")]
        public DeviceTypeEnum ParaItemType { get; set; }

        #region constructor
        /// <summary>
        /// 表:护理项目表构造器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deptCode">科室编号</param>
        /// <param name="moduleCode">参数所属模块</param>
        /// <param name="paraCode">项目代码</param>
        /// <param name="paraName">项目名称</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="scoreCode">评分代码</param>
        /// <param name="groupName">导管分类</param>
        /// <param name="unitName">单位名称</param>
        /// <param name="valueType">数据类型</param>
        /// <param name="style">文本类型</param>
        /// <param name="decimalDigits">小数位数</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="highValue">高值</param>
        /// <param name="lowValue">低值</param>
        /// <param name="threshold">是否预警</param>
        /// <param name="sort">排序号</param>
        /// <param name="dataSource">默认值</param>
        /// <param name="dictFlag">导管项目是否静态显示</param>
        /// <param name="guidFlag">是否评分</param>
        /// <param name="guidId">评分指引编号</param>
        /// <param name="statisticalType">特护单统计参数分类</param>
        /// <param name="drawChartFlag">绘制趋势图形</param>
        /// <param name="colorParaCode">关联颜色</param>
        /// <param name="colorParaName">关联颜色名称</param>
        /// <param name="propertyParaCode">关联性状</param>
        /// <param name="propertyParaName">关联性状名称</param>
        /// <param name="deviceParaCode">设备参数代码</param>
        /// <param name="deviceParaType">设备参数类型（1-测量值，2-设定值）</param>
        /// <param name="healthSign">是否生命体征项目</param>
        /// <param name="kBCode">知识库代码</param>
        /// <param name="nuringViewCode">护理概览参数</param>
        /// <param name="abnormalSign">是否异常体征参数</param>
        /// <param name="isUsage">是否用药所属项目</param>
        /// <param name="addSource">添加来源</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="paraItemType">项目参数类型，用于区分监护仪或者呼吸机等</param>
        public ParaItem(Guid id,
            string deptCode,              // 科室编号
            string moduleCode,            // 参数所属模块
            [NotNull] string paraCode,    // 项目代码
            [NotNull] string paraName,    // 项目名称
            string displayName,           // 显示名称
            string scoreCode,             // 评分代码
            string groupName,             // 导管分类
            string unitName,              // 单位名称
            string valueType,             // 数据类型
            string style,                 // 文本类型
            string decimalDigits,         // 小数位数
            string maxValue,              // 最大值
            string minValue,              // 最小值
            string highValue,             // 高值
            string lowValue,              // 低值
            bool threshold,               // 是否预警
            int sort,                  // 排序号
            string dataSource,            // 默认值
            string dictFlag,              // 导管项目是否静态显示
            bool? guidFlag,               // 是否评分
            string guidId,                // 评分指引编号
            string statisticalType,       // 特护单统计参数分类
            [NotNull] int drawChartFlag,  // 绘制趋势图形
            string colorParaCode,         // 关联颜色
            string colorParaName,         // 关联颜色名称
            string propertyParaCode,      // 关联性状
            string propertyParaName,      // 关联性状名称
            string deviceParaCode,        // 设备参数代码
            string deviceParaType,        // 设备参数类型（1-测量值，2-设定值）
            int? healthSign,              // 是否生命体征项目
            string kBCode,                // 知识库代码
            string nuringViewCode,        // 护理概览参数
            string abnormalSign,          // 是否异常体征参数
            bool? isUsage,                // 是否用药所属项目
            string addSource,             // 添加来源
            bool isEnable,                // 是否启用
            DeviceTypeEnum paraItemType   // 项目参数类型，用于区分监护仪或者呼吸机等
            ) : base(id)
        {
            //科室编号
            DeptCode = Check.Length(deptCode, "科室编号", ParaItemConsts.MaxDeptCodeLength);

            Modify(moduleCode,// 参数所属模块
                paraCode,       // 项目代码
                paraName,       // 项目名称
                displayName,    // 显示名称
                scoreCode,      // 评分代码
                groupName,      // 导管分类
                unitName,       // 单位名称
                valueType,      // 数据类型
                style,          // 文本类型
                decimalDigits,  // 小数位数
                maxValue,       // 最大值
                minValue,       // 最小值
                highValue,      // 高值
                lowValue,       // 低值
                threshold,      // 是否预警
                sort,        // 排序号
                dataSource,     // 默认值
                dictFlag,       // 导管项目是否静态显示
                guidFlag,       // 是否评分
                guidId,         // 评分指引编号
                statisticalType,// 特护单统计参数分类
                drawChartFlag,  // 绘制趋势图形
                colorParaCode,  // 关联颜色
                colorParaName,  // 关联颜色名称
                propertyParaCode,// 关联性状
                propertyParaName,// 关联性状名称
                deviceParaCode, // 设备参数代码
                deviceParaType, // 设备参数类型（1-测量值，2-设定值）
                healthSign,     // 是否生命体征项目
                kBCode,         // 知识库代码
                nuringViewCode, // 护理概览参数
                abnormalSign,   // 是否异常体征参数
                isUsage,        // 是否用药所属项目
                addSource,      // 添加来源
                isEnable,       // 是否启用
                paraItemType    // 项目参数类型，用于区分监护仪或者呼吸机等
                );
        }
        #endregion

        #region Modify
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleCode">参数所属模块</param>
        /// <param name="paraCode">项目代码</param>
        /// <param name="paraName">项目名称</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="scoreCode">评分代码</param>
        /// <param name="groupName">导管分类</param>
        /// <param name="unitName">单位名称</param>
        /// <param name="valueType">数据类型</param>
        /// <param name="style">文本类型</param>
        /// <param name="decimalDigits">小数位数</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="highValue">高值</param>
        /// <param name="lowValue">低值</param>
        /// <param name="threshold">是否预警</param>
        /// <param name="sort">排序号</param>
        /// <param name="dataSource">默认值</param>
        /// <param name="dictFlag">导管项目是否静态显示</param>
        /// <param name="guidFlag">是否评分</param>
        /// <param name="guidId">评分指引编号</param>
        /// <param name="statisticalType">特护单统计参数分类</param>
        /// <param name="drawChartFlag">绘制趋势图形</param>
        /// <param name="colorParaCode">关联颜色</param>
        /// <param name="colorParaName">关联颜色名称</param>
        /// <param name="propertyParaCode">关联性状</param>
        /// <param name="propertyParaName">关联性状名称</param>
        /// <param name="deviceParaCode">设备参数代码</param>
        /// <param name="deviceParaType">设备参数类型（1-测量值，2-设定值）</param>
        /// <param name="healthSign">是否生命体征项目</param>
        /// <param name="kBCode">知识库代码</param>
        /// <param name="nuringViewCode">护理概览参数</param>
        /// <param name="abnormalSign">是否异常体征参数</param>
        /// <param name="isUsage">是否用药所属项目</param>
        /// <param name="addSource">添加来源</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="paraItemType">项目参数类型，用于区分监护仪或者呼吸机等</param>
        public void Modify(string moduleCode,// 参数所属模块
            [NotNull] string paraCode,    // 项目代码
            [NotNull] string paraName,    // 项目名称
            string displayName,           // 显示名称
            string scoreCode,             // 评分代码
            string groupName,             // 导管分类
            string unitName,              // 单位名称
            string valueType,             // 数据类型
            string style,                 // 文本类型
            string decimalDigits,         // 小数位数
            string maxValue,              // 最大值
            string minValue,              // 最小值
            string highValue,             // 高值
            string lowValue,              // 低值
            bool threshold,               // 是否预警
            int sort,                  // 排序号
            string dataSource,            // 默认值
            string dictFlag,              // 导管项目是否静态显示
            bool? guidFlag,               // 是否评分
            string guidId,                // 评分指引编号
            string statisticalType,       // 特护单统计参数分类
            [NotNull] int drawChartFlag,  // 绘制趋势图形
            string colorParaCode,         // 关联颜色
            string colorParaName,         // 关联颜色名称
            string propertyParaCode,      // 关联性状
            string propertyParaName,      // 关联性状名称
            string deviceParaCode,        // 设备参数代码
            string deviceParaType,        // 设备参数类型（1-测量值，2-设定值）
            int? healthSign,              // 是否生命体征项目
            string kBCode,                // 知识库代码
            string nuringViewCode,        // 护理概览参数
            string abnormalSign,          // 是否异常体征参数
            bool? isUsage,                // 是否用药所属项目
            string addSource,             // 添加来源
            bool isEnable,          // 是否启用
            DeviceTypeEnum paraItemType   // 项目参数类型，用于区分监护仪或者呼吸机等
            )
        {

            //参数所属模块
            ModuleCode = Check.Length(moduleCode, "参数所属模块", ParaItemConsts.MaxModuleCodeLength);

            //项目代码
            ParaCode = Check.NotNull(paraCode, "项目代码", ParaItemConsts.MaxParaCodeLength);

            //项目名称
            ParaName = Check.NotNull(paraName, "项目名称", ParaItemConsts.MaxParaNameLength);

            //显示名称
            DisplayName = Check.Length(displayName, "显示名称", ParaItemConsts.MaxDisplayNameLength);

            //评分代码
            ScoreCode = Check.Length(scoreCode, "评分代码", ParaItemConsts.MaxScoreCodeLength);

            //导管分类
            GroupName = Check.Length(groupName, "导管分类", ParaItemConsts.MaxGroupNameLength);

            //单位名称
            UnitName = Check.Length(unitName, "单位名称", ParaItemConsts.MaxUnitNameLength);

            //数据类型
            ValueType = Check.Length(valueType, "数据类型", ParaItemConsts.MaxValueTypeLength);

            //文本类型
            Style = Check.Length(style, "文本类型", ParaItemConsts.MaxStyleLength);

            //小数位数
            DecimalDigits = Check.Length(decimalDigits, "小数位数", ParaItemConsts.MaxDecimalDigitsLength);

            //最大值
            MaxValue = Check.Length(maxValue, "最大值", ParaItemConsts.MaxMaxValueLength);

            //最小值
            MinValue = Check.Length(minValue, "最小值", ParaItemConsts.MaxMinValueLength);

            //高值
            HighValue = Check.Length(highValue, "高值", ParaItemConsts.MaxHighValueLength);

            //低值
            LowValue = Check.Length(lowValue, "低值", ParaItemConsts.MaxLowValueLength);

            //是否预警
            Threshold = threshold;

            //排序号
            Sort = sort;

            //默认值
            DataSource = Check.Length(dataSource, "默认值", ParaItemConsts.MaxDataSourceLength);

            //导管项目是否静态显示
            DictFlag = Check.Length(dictFlag, "导管项目是否静态显示", ParaItemConsts.MaxDictFlagLength);

            //是否评分
            GuidFlag = guidFlag;

            //评分指引编号
            GuidId = Check.Length(guidId, "评分指引编号", ParaItemConsts.MaxGuidIdLength);

            //特护单统计参数分类
            StatisticalType = Check.Length(statisticalType, "特护单统计参数分类", ParaItemConsts.MaxStatisticalTypeLength);

            //绘制趋势图形
            DrawChartFlag = Check.NotNull(drawChartFlag, "绘制趋势图形");

            //关联颜色
            ColorParaCode = Check.Length(colorParaCode, "关联颜色", ParaItemConsts.MaxColorParaCodeLength);

            //关联颜色名称
            ColorParaName = Check.Length(colorParaName, "关联颜色名称", ParaItemConsts.MaxColorParaNameLength);

            //关联性状
            PropertyParaCode = Check.Length(propertyParaCode, "关联性状", ParaItemConsts.MaxPropertyParaCodeLength);

            //关联性状名称
            PropertyParaName = Check.Length(propertyParaName, "关联性状名称", ParaItemConsts.MaxPropertyParaNameLength);

            //设备参数代码
            DeviceParaCode = Check.Length(deviceParaCode, "设备参数代码", ParaItemConsts.MaxDeviceParaCodeLength);

            //设备参数类型（1-测量值，2-设定值）
            DeviceParaType = Check.Length(deviceParaType, "设备参数类型（1-测量值，2-设定值）", ParaItemConsts.MaxDeviceParaTypeLength);

            //是否生命体征项目
            HealthSign = healthSign;

            //知识库代码
            KBCode = Check.Length(kBCode, "知识库代码", ParaItemConsts.MaxKBCodeLength);

            //护理概览参数
            NuringViewCode = Check.Length(nuringViewCode, "护理概览参数", ParaItemConsts.MaxNuringViewCodeLength);

            //是否异常体征参数
            AbnormalSign = Check.Length(abnormalSign, "是否异常体征参数", ParaItemConsts.MaxAbnormalSignLength);

            //是否用药所属项目
            IsUsage = isUsage;

            //添加来源
            AddSource = Check.Length(addSource, "添加来源", ParaItemConsts.MaxAddSourceLength);

            //是否启用
            IsEnable = isEnable;

            //项目参数类型，用于区分监护仪或者呼吸机等
            ParaItemType = paraItemType;

        }
        #endregion

        #region constructor
        private ParaItem()
        {
            // for EFCore
        }
        #endregion
    }
}
