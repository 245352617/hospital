namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:护理项目表 应用服务测试
    /// </summary>
    public class ParaItemAppServiceTests : NursingApplicationTestBase
    {      
        private readonly IParaItemRepository _paraItemRepository;
        private readonly ParaItemManager _paraItemManager;
        private readonly IParaItemAppService _paraItemAppService;
        
        public ParaItemAppServiceTests()
        {
            _paraItemRepository = GetRequiredService<IParaItemRepository>();
            _paraItemManager = GetRequiredService<ParaItemManager>();  
            _paraItemAppService = GetRequiredService<IParaItemAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作
            var input = new ParaItemCreation()
            {
                Id = default,
                DeptCode = "",  // 科室编号
                ModuleCode = "",// 参数所属模块
                ParaCode = "",  // 项目代码
                ParaName = "",  // 项目名称
                DisplayName = "",// 显示名称
                ScoreCode = "", // 评分代码
                GroupName = "", // 导管分类
                UnitName = "",  // 单位名称
                ValueType = "", // 数据类型
                Style = "",     // 文本类型
                DecimalDigits = "",// 小数位数
                MaxValue = "",  // 最大值
                MinValue = "",  // 最小值
                HighValue = "", // 高值
                LowValue = "",  // 低值
                Threshold = true,// 是否预警
                SortNum = 0,    // 排序号
                DataSource = "",// 默认值
                DictFlag = "",  // 导管项目是否静态显示
                GuidFlag = null,// 是否评分
                GuidId = "",    // 评分指引编号
                StatisticalType = "",// 特护单统计参数分类
                DrawChartFlag = 0,// 绘制趋势图形
                ColorParaCode = "",// 关联颜色
                ColorParaName = "",// 关联颜色名称
                PropertyParaCode = "",// 关联性状
                PropertyParaName = "",// 关联性状名称
                DeviceParaCode = "",// 设备参数代码
                DeviceParaType = "",// 设备参数类型（1-测量值，2-设定值）
                HealthSign = null,// 是否生命体征项目
                KBCode = "",    // 知识库代码
                NuringViewCode = "",// 护理概览参数
                AbnormalSign = "",// 是否异常体征参数
                IsUsage = null, // 是否用药所属项目
                AddSource = "", // 添加来源
                IsEnable = true,// 是否启用
                ValidState = 0, // 有效状态
                ParaItemType = default// 项目参数类型，用于区分监护仪或者呼吸机等
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _paraItemAppService.CreateAsync(input)
                                        );

            // Assert 断言，检验结果

            id.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task UpdateAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作
            var input = new ParaItemUpdate()
            {
                ModuleCode = "",// 参数所属模块
                ParaCode = "",  // 项目代码
                ParaName = "",  // 项目名称
                DisplayName = "",// 显示名称
                ScoreCode = "", // 评分代码
                GroupName = "", // 导管分类
                UnitName = "",  // 单位名称
                ValueType = "", // 数据类型
                Style = "",     // 文本类型
                DecimalDigits = "",// 小数位数
                MaxValue = "",  // 最大值
                MinValue = "",  // 最小值
                HighValue = "", // 高值
                LowValue = "",  // 低值
                Threshold = true,// 是否预警
                SortNum = 0,    // 排序号
                DataSource = "",// 默认值
                DictFlag = "",  // 导管项目是否静态显示
                GuidFlag = null,// 是否评分
                GuidId = "",    // 评分指引编号
                StatisticalType = "",// 特护单统计参数分类
                DrawChartFlag = 0,// 绘制趋势图形
                ColorParaCode = "",// 关联颜色
                ColorParaName = "",// 关联颜色名称
                PropertyParaCode = "",// 关联性状
                PropertyParaName = "",// 关联性状名称
                DeviceParaCode = "",// 设备参数代码
                DeviceParaType = "",// 设备参数类型（1-测量值，2-设定值）
                HealthSign = null,// 是否生命体征项目
                KBCode = "",    // 知识库代码
                NuringViewCode = "",// 护理概览参数
                AbnormalSign = "",// 是否异常体征参数
                IsUsage = null, // 是否用药所属项目
                AddSource = "", // 添加来源
                IsEnable = true,// 是否启用
                ValidState = 0, // 有效状态
                ParaItemType = default// 项目参数类型，用于区分监护仪或者呼吸机等
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _paraItemAppService.UpdateAsync(input)
                            );

            // Assert 断言，检验结果

            
            */
        }

        [Fact]
        public async Task GetAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            var paraItemData = await _paraItemAppService.GetAsync(id);

            // Assert 断言，检验结果

            paraItemData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            await _paraItemAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _paraItemRepository.GetCountAsync()).ShouldBe(0);
            */
        }

        [Fact]
        public async Task GetListAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作

            string filter = null;
            string sorting = null;

            // Act 运行实际测试的代码

            var paraItemData = await _paraItemAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            paraItemData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作

            var input = new GetParaItemPagedInput();

            // Act 运行实际测试的代码

            var paraItemData = await _paraItemAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            paraItemData.Items.Count.ShouldBe(1);
            */
        }
    }
}
