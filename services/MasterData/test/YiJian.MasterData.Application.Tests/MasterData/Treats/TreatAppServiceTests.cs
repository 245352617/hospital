namespace YiJian.MasterData.Treats
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 诊疗项目字典 应用服务测试
    /// </summary>
    public class TreatAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly ITreatRepository _treatRepository;
        private readonly ITreatAppService _treatAppService;
        
        public TreatAppServiceTests()
        {
            _treatRepository = GetRequiredService<ITreatRepository>();
            _treatAppService = GetRequiredService<ITreatAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new TreatCreation()
            {
                Code = "",      // 编码
                Name = "",      // 名称
                PyCode = "",    // 拼音码
                WbCode = "",    // 五笔
                Price = 0,      // 单价
                OtherPrice = null,// 其它价格
                CategoryCode = "",// 诊疗处置类别代码
                Category = "",  // 诊疗处置类别
                Specification = "",// 规格
                Unit = "",      // 单位
                FrequencyCode = "",// 默认频次代码
                ExecDeptCode = "",// 执行科室代码
                ExecDept = "",  // 执行科室
                FeeTypeMain = "",// 收费大类代码
                FeeTypeSub = "" // 收费小类代码
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _treatAppService.CreateAsync(input)
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
            var input = new TreatUpdate()
            {
                Name = "",      // 名称
                PyCode = "",    // 拼音码
                WbCode = "",    // 五笔
                Price = 0,      // 单价
                OtherPrice = null,// 其它价格
                CategoryCode = "",// 诊疗处置类别代码
                Category = "",  // 诊疗处置类别
                Specification = "",// 规格
                Unit = "",      // 单位
                FrequencyCode = "",// 默认频次代码
                ExecDeptCode = "",// 执行科室代码
                ExecDept = "",  // 执行科室
                FeeTypeMain = "",// 收费大类代码
                FeeTypeSub = "" // 收费小类代码
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _treatAppService.UpdateAsync(input)
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

            var treatData = await _treatAppService.GetAsync(id);

            // Assert 断言，检验结果

            treatData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            await _treatAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _treatRepository.GetCountAsync()).ShouldBe(0);
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

            var treatData = await _treatAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            treatData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetTreatPagedInput();

            // Act 运行实际测试的代码

            var treatData = await _treatAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            treatData.Items.Count.ShouldBe(1);
            */
        }
    }
}
