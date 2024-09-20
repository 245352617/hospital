using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;


namespace YiJian.Nursing
{
    /// <summary>
    /// 表:护理项目表 领域服务
    /// </summary>
    public class ParaItemManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IParaItemRepository _paraItemRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraItemRepository"></param>
        /// <param name="guidGenerator"></param>
        public ParaItemManager(IParaItemRepository paraItemRepository, IGuidGenerator guidGenerator)
        {
            _paraItemRepository = paraItemRepository;
            _guidGenerator = guidGenerator;
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
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
        /// <param name="sortNum">排序号</param>
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
        public async Task<ParaItem> CreateAsync(string deptCode, // 科室编号
            string moduleCode, // 参数所属模块
            [NotNull] string paraCode, // 项目代码
            [NotNull] string paraName, // 项目名称
            string displayName, // 显示名称
            string scoreCode, // 评分代码
            string groupName, // 导管分类
            string unitName, // 单位名称
            string valueType, // 数据类型
            string style, // 文本类型
            string decimalDigits, // 小数位数
            string maxValue, // 最大值
            string minValue, // 最小值
            string highValue, // 高值
            string lowValue, // 低值
            bool threshold, // 是否预警
            int sortNum, // 排序号
            string dataSource, // 默认值
            string dictFlag, // 导管项目是否静态显示
            bool? guidFlag, // 是否评分
            string guidId, // 评分指引编号
            string statisticalType, // 特护单统计参数分类
            [NotNull] int drawChartFlag, // 绘制趋势图形
            string colorParaCode, // 关联颜色
            string colorParaName, // 关联颜色名称
            string propertyParaCode, // 关联性状
            string propertyParaName, // 关联性状名称
            string deviceParaCode, // 设备参数代码
            string deviceParaType, // 设备参数类型（1-测量值，2-设定值）
            int? healthSign, // 是否生命体征项目
            string kBCode, // 知识库代码
            string nuringViewCode, // 护理概览参数
            string abnormalSign, // 是否异常体征参数
            bool? isUsage, // 是否用药所属项目
            string addSource, // 添加来源
            bool isEnable, // 是否启用
            DeviceTypeEnum paraItemType // 项目参数类型，用于区分监护仪或者呼吸机等
        )
        {
            var paraItem = new ParaItem(_guidGenerator.Create(),
                deptCode, // 科室编号
                moduleCode, // 参数所属模块
                paraCode, // 项目代码
                paraName, // 项目名称
                displayName, // 显示名称
                scoreCode, // 评分代码
                groupName, // 导管分类
                unitName, // 单位名称
                valueType, // 数据类型
                style, // 文本类型
                decimalDigits, // 小数位数
                maxValue, // 最大值
                minValue, // 最小值
                highValue, // 高值
                lowValue, // 低值
                threshold, // 是否预警
                sortNum, // 排序号
                dataSource, // 默认值
                dictFlag, // 导管项目是否静态显示
                guidFlag, // 是否评分
                guidId, // 评分指引编号
                statisticalType, // 特护单统计参数分类
                drawChartFlag, // 绘制趋势图形
                colorParaCode, // 关联颜色
                colorParaName, // 关联颜色名称
                propertyParaCode, // 关联性状
                propertyParaName, // 关联性状名称
                deviceParaCode, // 设备参数代码
                deviceParaType, // 设备参数类型（1-测量值，2-设定值）
                healthSign, // 是否生命体征项目
                kBCode, // 知识库代码
                nuringViewCode, // 护理概览参数
                abnormalSign, // 是否异常体征参数
                isUsage, // 是否用药所属项目
                addSource, // 添加来源
                isEnable, // 是否启用
                paraItemType // 项目参数类型，用于区分监护仪或者呼吸机等
            );

            return await _paraItemRepository.InsertAsync(paraItem);
        }

        #endregion Create
    }
}