namespace YiJian.MasterData.Labs.Container
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 容器编码 应用服务测试
    /// </summary>
    public class LabContainerAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly ILabContainerRepository _labContainerRepository;
        private readonly ILabContainerAppService _labContainerAppService;
        
        public LabContainerAppServiceTests()
        {
            _labContainerRepository = GetRequiredService<ILabContainerRepository>();
            _labContainerAppService = GetRequiredService<ILabContainerAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new LabContainerCreation()
            {
                ContainerCode = "",// 容器编码
                ContainerName = "",// 容器名称
                ContainerColor = "",// 容器颜色
                IsActive = true
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _labContainerAppService.CreateAsync(input)
                                        );

            // Assert 断言，检验结果

            id.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task UpdateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new LabContainerUpdate()
            {
                ContainerName = "",// 容器名称
                ContainerColor = "",// 容器颜色
                IsActive = true
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _labContainerAppService.UpdateAsync(input)
                            );

            // Assert 断言，检验结果

            
            */
        }

        [Fact]
        public async Task GetAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            var labContainerData = await _labContainerAppService.GetAsync(id);

            // Assert 断言，检验结果

            labContainerData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            await _labContainerAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _labContainerRepository.GetCountAsync()).ShouldBe(0);
            */
        }

        [Fact]
        public async Task GetListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            string filter = null;
            string sorting = null;

            // Act 运行实际测试的代码

            var labContainerData = await _labContainerAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            labContainerData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetLabContainerPagedInput();

            // Act 运行实际测试的代码

            var labContainerData = await _labContainerAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            labContainerData.Items.Count.ShouldBe(1);
            */
        }
    }
}
