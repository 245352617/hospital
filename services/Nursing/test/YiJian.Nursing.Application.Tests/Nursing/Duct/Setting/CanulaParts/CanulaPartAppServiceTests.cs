namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:人体图-编号字典 应用服务测试
    /// </summary>
    public class CanulaPartAppServiceTests : NursingApplicationTestBase
    {      
        private readonly ICanulaPartRepository _canulaPartRepository;
        private readonly ICanulaPartAppService _canulaPartAppService;
        
        public CanulaPartAppServiceTests()
        {
            _canulaPartRepository = GetRequiredService<ICanulaPartRepository>();
            _canulaPartAppService = GetRequiredService<ICanulaPartAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作
            var input = new CanulaPartCreation()
            {
                Id = default,
                DeptCode = "",  // 科室代码
                ModuleCode = "",// 模块代码
                PartName = "",  // 部位名称
                PartNumber = "",// 部位编号
                Sort = 0,       // 排序
                IsEnable = true,// 是否可用
                IsDeleted = true// 是否删除
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _canulaPartAppService.CreateAsync(input)
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
            var input = new CanulaPartUpdate()
            {
                ModuleCode = "",// 模块代码
                PartName = "",  // 部位名称
                PartNumber = "",// 部位编号
                Sort = 0,       // 排序
                IsEnable = true,// 是否可用
                IsDeleted = true// 是否删除
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _canulaPartAppService.UpdateAsync(input)
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

            var canulaPartData = await _canulaPartAppService.GetAsync(id);

            // Assert 断言，检验结果

            canulaPartData.ShouldNotBeNull();
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

            await _canulaPartAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _canulaPartRepository.GetCountAsync()).ShouldBe(0);
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

            var canulaPartData = await _canulaPartAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            canulaPartData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {
            await Task.CompletedTask;
            /*
            // Arrange 为测试做准备工作

            var input = new GetCanulaPartPagedInput();

            // Act 运行实际测试的代码

            var canulaPartData = await _canulaPartAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            canulaPartData.Items.Count.ShouldBe(1);
            */
        }
    }
}
