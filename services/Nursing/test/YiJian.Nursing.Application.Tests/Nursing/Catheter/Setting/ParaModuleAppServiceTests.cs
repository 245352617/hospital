namespace YiJian.Nursing.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:模块参数 应用服务测试
    /// </summary>
    public class ParaModuleAppServiceTests : NursingApplicationTestBase
    {      
        private readonly IParaModuleRepository _paraModuleRepository;
        private readonly ParaModuleManager _paraModuleManager;
        private readonly IParaModuleAppService _paraModuleAppService;
        
        public ParaModuleAppServiceTests()
        {
            _paraModuleRepository = GetRequiredService<IParaModuleRepository>();
            _paraModuleManager = GetRequiredService<ParaModuleManager>();  
            _paraModuleAppService = GetRequiredService<IParaModuleAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作
            var input = new ParaModuleCreation()
            {
                Id = default,
                ModuleCode = "",// 模块代码
                ModuleName = "",// 模块名称
                DisplayName = "",// 模块显示名称
                DeptCode = "",  // 科室代码
                ModuleType = "",// 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
                IsBloodFlow = true,// 是否血流内导管
                Py = "",        // 模块拼音
                SortNum = 0,    // 排序
                IsEnable = true,// 是否启用
                ValidState = 0  // 是否有效(1-有效，0-无效)
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _paraModuleAppService.CreateAsync(input)
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
            var input = new ParaModuleUpdate()
            {
                ModuleName = "",// 模块名称
                DisplayName = "",// 模块显示名称
                DeptCode = "",  // 科室代码
                ModuleType = "",// 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
                IsBloodFlow = true,// 是否血流内导管
                Py = "",        // 模块拼音
                SortNum = 0,    // 排序
                IsEnable = true,// 是否启用
                ValidState = 0  // 是否有效(1-有效，0-无效)
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _paraModuleAppService.UpdateAsync(input)
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

            var paraModuleData = await _paraModuleAppService.GetAsync(id);

            // Assert 断言，检验结果

            paraModuleData.ShouldNotBeNull();
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

            await _paraModuleAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _paraModuleRepository.GetCountAsync()).ShouldBe(0);
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

            var paraModuleData = await _paraModuleAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            paraModuleData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作

            var input = new GetParaModulePagedInput();

            // Act 运行实际测试的代码

            var paraModuleData = await _paraModuleAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            paraModuleData.Items.Count.ShouldBe(1);
            */
        }
    }
}
