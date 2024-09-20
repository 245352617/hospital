namespace YiJian.MasterData
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检查申请注意事项 应用服务测试
    /// </summary>
    public class ExamNoteAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly IExamNoteRepository _examNoteRepository;
        private readonly IExamNoteAppService _examNoteAppService;
        
        public ExamNoteAppServiceTests()
        {
            _examNoteRepository = GetRequiredService<IExamNoteRepository>();
            _examNoteAppService = GetRequiredService<IExamNoteAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new ExamNoteCreation()
            {
                Code = "",      // 注意事项编码
                Name = "",      // 注意事项名称
                DeptCode = "",  // 科室编码
                Dept = "",      // 科室
                DisplayName = "",// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
                DescTemplate = "",// 检查申请描述模板
                IsActive = true // 是否启用
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _examNoteAppService.CreateAsync(input)
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
            var input = new ExamNoteUpdate()
            {
                Name = "",      // 注意事项名称
                DeptCode = "",  // 科室编码
                Dept = "",      // 科室
                DisplayName = "",// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
                DescTemplate = "",// 检查申请描述模板
                IsActive = true // 是否启用
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _examNoteAppService.UpdateAsync(input)
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

            var examNoteData = await _examNoteAppService.GetAsync(id);

            // Assert 断言，检验结果

            examNoteData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            await _examNoteAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _examNoteRepository.GetCountAsync()).ShouldBe(0);
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

            var examNoteData = await _examNoteAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            examNoteData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetExamNotePagedInput();

            // Act 运行实际测试的代码

            var examNoteData = await _examNoteAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            examNoteData.Items.Count.ShouldBe(1);
            */
        }
    }
}
