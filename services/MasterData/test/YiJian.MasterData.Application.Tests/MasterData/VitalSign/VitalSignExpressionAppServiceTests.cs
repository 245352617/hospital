namespace YiJian.MasterData.VitalSign
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 评分项 应用服务测试
    /// </summary>
    public class VitalSignExpressionAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly IVitalSignExpressionRepository _vitalSignExpressionRepository;
        private readonly VitalSignExpressionManager _vitalSignExpressionManager;
        private readonly IVitalSignExpressionAppService _vitalSignExpressionAppService;
        
        public VitalSignExpressionAppServiceTests()
        {
            _vitalSignExpressionRepository = GetRequiredService<IVitalSignExpressionRepository>();
            _vitalSignExpressionManager = GetRequiredService<VitalSignExpressionManager>();  
            _vitalSignExpressionAppService = GetRequiredService<IVitalSignExpressionAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new VitalSignExpressionCreation()
            {
                Id = default,
                ItemName = "",  // 评分项
                StLevelExpression = "",// Ⅰ级评分表达式
                NdLevelExpression = "",// Ⅱ级评分表达式
                RdLevelExpression = "",// Ⅲ级评分表达式
                ThALevelExpression = "",// Ⅳa级评分表达式
                ThBLevelExpression = "",// Ⅳb级评分表达式
                DefaultStLevelExpression = "",// 默认Ⅰ级评分表达式
                DefaultNdLevelExpression = "",// 默认Ⅱ级评分表达式
                DefaultRdLevelExpression = "",// 默认Ⅲ级评分表达式
                DefaultThALevelExpression = "",// 默认Ⅳa级评分表达式
                DefaultThBLevelExpression = ""// 默认Ⅳb级评分表达式
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _vitalSignExpressionAppService.CreateAsync(input)
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
            var input = new VitalSignExpressionUpdate()
            {
                StLevelExpression = "",// Ⅰ级评分表达式
                NdLevelExpression = "",// Ⅱ级评分表达式
                RdLevelExpression = "",// Ⅲ级评分表达式
                ThALevelExpression = "",// Ⅳa级评分表达式
                ThBLevelExpression = "",// Ⅳb级评分表达式
                DefaultStLevelExpression = "",// 默认Ⅰ级评分表达式
                DefaultNdLevelExpression = "",// 默认Ⅱ级评分表达式
                DefaultRdLevelExpression = "",// 默认Ⅲ级评分表达式
                DefaultThALevelExpression = "",// 默认Ⅳa级评分表达式
                DefaultThBLevelExpression = ""// 默认Ⅳb级评分表达式
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _vitalSignExpressionAppService.UpdateAsync(input)
                            );

            // Assert 断言，检验结果

            
            */
        }

        [Fact]
        public async Task GetAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            var vitalSignExpressionData = await _vitalSignExpressionAppService.GetAsync(id);

            // Assert 断言，检验结果

            vitalSignExpressionData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            await _vitalSignExpressionAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _vitalSignExpressionRepository.GetCountAsync()).ShouldBe(0);
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

            var vitalSignExpressionData = await _vitalSignExpressionAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            vitalSignExpressionData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetVitalSignExpressionPagedInput();

            // Act 运行实际测试的代码

            var vitalSignExpressionData = await _vitalSignExpressionAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            vitalSignExpressionData.Items.Count.ShouldBe(1);
            */
        }
    }
}
