namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:护理项目表 领域实体测试
    /// </summary>
    public class ParaItemTests : NursingDomainTestBase
    {
        public ParaItemTests()
        {  
        }

        [Fact]
        public void ModifyTest()
        {  
            /*
            // Arrange 为测试做准备工作

            // 
            Guid id = default;
            // 科室编号
            string deptCode = "";
            // 参数所属模块
            string moduleCode = "";
            // 项目代码
            string paraCode = "";
            // 项目名称
            string paraName = "";
            // 显示名称
            string displayName = "";
            // 评分代码
            string scoreCode = "";
            // 导管分类
            string groupName = "";
            // 单位名称
            string unitName = "";
            // 数据类型
            string valueType = "";
            // 文本类型
            string style = "";
            // 小数位数
            string decimalDigits = "";
            // 最大值
            string maxValue = "";
            // 最小值
            string minValue = "";
            // 高值
            string highValue = "";
            // 低值
            string lowValue = "";
            // 是否预警
            bool threshold = true;
            // 排序号
            int sortNum = 0;
            // 默认值
            string dataSource = "";
            // 导管项目是否静态显示
            string dictFlag = "";
            // 是否评分
            bool? guidFlag = null;
            // 评分指引编号
            string guidId = "";
            // 特护单统计参数分类
            string statisticalType = "";
            // 绘制趋势图形
            int drawChartFlag = 0;
            // 关联颜色
            string colorParaCode = "";
            // 关联颜色名称
            string colorParaName = "";
            // 关联性状
            string propertyParaCode = "";
            // 关联性状名称
            string propertyParaName = "";
            // 设备参数代码
            string deviceParaCode = "";
            // 设备参数类型（1-测量值，2-设定值）
            string deviceParaType = "";
            // 是否生命体征项目
            int? healthSign = null;
            // 知识库代码
            string kBCode = "";
            // 护理概览参数
            string nuringViewCode = "";
            // 是否异常体征参数
            string abnormalSign = "";
            // 是否用药所属项目
            bool? isUsage = null;
            // 添加来源
            string addSource = "";
            // 是否启用
            bool isEnable = true;
            // 有效状态
            int validState = 0;
            // 项目参数类型，用于区分监护仪或者呼吸机等
            DeviceTypeEnum paraItemType = default;

            var paraItem = new ParaItem(_guidGenerator.Create(), 
                deptCode,// 科室编号
                moduleCode,     // 参数所属模块
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
                sortNum,        // 排序号
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
                validState,     // 有效状态
                paraItemType    // 项目参数类型，用于区分监护仪或者呼吸机等
                );

            // Act 运行实际测试的代码

            paraItem.Modify(moduleCode,// 参数所属模块
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
                sortNum,        // 排序号
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
                validState,     // 有效状态
                paraItemType    // 项目参数类型，用于区分监护仪或者呼吸机等
                );

            // Assert 断言，检验结果

            paraItem.DeptCode.ShouldNotBeNull();
            */            
        }
    }
}
