namespace YiJian.MasterData.Labs.Position
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检验标本采集部位 应用服务测试
    /// </summary>
    public class LabSpecimenPositionAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly ILabSpecimenPositionRepository _labSpecimenPositionRepository;
        private readonly ILabSpecimenPositionAppService _labSpecimenPositionAppService;
        
        public LabSpecimenPositionAppServiceTests()
        {
            _labSpecimenPositionRepository = GetRequiredService<ILabSpecimenPositionRepository>();
            _labSpecimenPositionAppService = GetRequiredService<ILabSpecimenPositionAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new LabSpecimenPositionCreation()
            {
                SpecimenCode = "",// 标本编码
                SpecimenName = "",// 标本名称
                PositionCode = "",// 采集部位编码
                PositionName = "",// 采集部位名称
                IndexNo = 0,    // 排序号
                PyCode = "",    // 拼音码
                IsActive = true
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _labSpecimenPositionAppService.CreateAsync(input)
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
            var input = new LabSpecimenPositionUpdate()
            {
                SpecimenName = "",// 标本名称
                PositionCode = "",// 采集部位编码
                PositionName = "",// 采集部位名称
                IndexNo = 0,    // 排序号
                PyCode = "",    // 拼音码
                IsActive = true
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _labSpecimenPositionAppService.UpdateAsync(input)
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

            var labSpecimenPositionData = await _labSpecimenPositionAppService.GetAsync(id);

            // Assert 断言，检验结果

            labSpecimenPositionData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            await _labSpecimenPositionAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _labSpecimenPositionRepository.GetCountAsync()).ShouldBe(0);
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

            var labSpecimenPositionData = await _labSpecimenPositionAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            labSpecimenPositionData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetLabSpecimenPositionPagedInput();

            // Act 运行实际测试的代码

            var labSpecimenPositionData = await _labSpecimenPositionAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            labSpecimenPositionData.Items.Count.ShouldBe(1);
            */
        }
    }
}
