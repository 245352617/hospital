namespace YiJian.MasterData.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 序列 应用服务测试
    /// </summary>
    public class SequenceAppServiceTests : MasterDataApplicationTestBase
    {
        private readonly ISequenceRepository _sequenceRepository;
        private readonly SequenceManager _sequenceManager;
        private readonly ISequenceAppService _sequenceAppService;

        public SequenceAppServiceTests()
        {
            _sequenceRepository = GetRequiredService<ISequenceRepository>();
            _sequenceManager = GetRequiredService<SequenceManager>();
            _sequenceAppService = GetRequiredService<ISequenceAppService>();
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {
            /*
            // Arrange 为测试做准备工作
            var input = new SequenceCreation()
            {
                Code = "",                      // 编码
                Name = "",                      // 名称
                Value = 0,                      // 序列值
                Format = "",                    // 格式
                Length = 0,                     // 序列值长度
                Date = default,                 // 日期
                Memo = ""                       // 备注
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _sequenceAppService.CreateAsync(input)
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
            var input = new SequenceUpdate()
            {
                Name = "",                      // 名称
                Value = 0,                      // 序列值
                Format = "",                    // 格式
                Length = 0,                     // 序列值长度
                Date = default,                 // 日期
                Memo = ""                       // 备注
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _sequenceAppService.UpdateAsync(input)
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

            var sequenceData = await _sequenceAppService.GetAsync(id);

            // Assert 断言，检验结果

            sequenceData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            await _sequenceAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _sequenceRepository.GetCountAsync()).ShouldBe(0);
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

            var sequenceData = await _sequenceAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            sequenceData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {
            /*
            // Arrange 为测试做准备工作

            var input = new GetSequencePagedInput();

            // Act 运行实际测试的代码

            var sequenceData = await _sequenceAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            sequenceData.Items.Count.ShouldBe(1);
            */
        }

        #region GetSequence
        /// <summary>
        /// 获取流水号
        /// </summary>
        [Fact]
        public async Task GetSequenceAsyncTestAsync()
        {   /*
            // Arrange
            var input = new GetSequenceInput()
            {
                Code = ""       // 编码
            };


            // Act

            var result = await WithUnitOfWorkAsync(() => 
                            _sequenceAppService.GetSequenceAsync(code: input.Code)
                            );

            // Assert

            */
        }
        #endregion GetSequence


    }
}
