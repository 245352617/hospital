namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:导管字典-通用业务 应用服务测试
    /// </summary>
    public class DictAppServiceTests : NursingApplicationTestBase
    {      
        private readonly IDictRepository _dictRepository;
        private readonly DictManager _dictManager;
        private readonly IDictAppService _dictAppService;
        
        public DictAppServiceTests()
        {
            _dictRepository = GetRequiredService<IDictRepository>();
            _dictManager = GetRequiredService<DictManager>();  
            _dictAppService = GetRequiredService<IDictAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作
            var input = new DictCreation()
            {
                Id = default,
                ParaCode = "",  // 参数代码
                ParaName = "",  // 参数名称
                DictCode = "",  // 字典代码
                DictValue = "", // 字典值
                DictDesc = "",  // 字典值说明
                ParentId = "",  // 上级代码
                DictStandard = "",// 字典标准（国标、自定义）
                HisCode = "",   // HIS对照代码
                HisName = "",   // HIS对照
                DeptCode = "",  // 科室代码
                ModuleCode = "",// 模块代码
                Sort = 0,       // 排序
                IsDefault = true,// 是否默认
                IsEnable = true,// 是否启用
                ValidState = 0  // 有效状态（1-有效，0-无效）
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _dictAppService.CreateAsync(input)
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
            var input = new DictUpdate()
            {
                ParaName = "",  // 参数名称
                DictCode = "",  // 字典代码
                DictValue = "", // 字典值
                DictDesc = "",  // 字典值说明
                ParentId = "",  // 上级代码
                DictStandard = "",// 字典标准（国标、自定义）
                HisCode = "",   // HIS对照代码
                HisName = "",   // HIS对照
                DeptCode = "",  // 科室代码
                ModuleCode = "",// 模块代码
                Sort = 0,       // 排序
                IsDefault = true,// 是否默认
                IsEnable = true,// 是否启用
                ValidState = 0  // 有效状态（1-有效，0-无效）
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _dictAppService.UpdateAsync(input)
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

            var dictData = await _dictAppService.GetAsync(id);

            // Assert 断言，检验结果

            dictData.ShouldNotBeNull();
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

            await _dictAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _dictRepository.GetCountAsync()).ShouldBe(0);
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

            var dictData = await _dictAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            dictData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作

            var input = new GetDictPagedInput();

            // Act 运行实际测试的代码

            var dictData = await _dictAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            dictData.Items.Count.ShouldBe(1);
            */
        }
    }
}
