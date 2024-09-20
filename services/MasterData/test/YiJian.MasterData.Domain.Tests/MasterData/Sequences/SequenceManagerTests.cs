namespace YiJian.MasterData.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 序列 领域服务测试
    /// </summary>
    public class SequenceManagerTests : MasterDataDomainTestBase
    {
        private readonly SequenceManager _sequenceManager;

        public SequenceManagerTests()
        {
            _sequenceManager = GetRequiredService<SequenceManager>();
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {
            /*
            // Arrange 为测试做准备工作

            // 编码
            string code = "";
            // 名称
            string name = "";
            // 序列值
            int value = 0;
            // 格式
            string format = "";
            // 序列值长度
            int length = 0;
            // 日期
            DateTime date = DateTime.Now;
            // 备注
            string memo = "";

            // Act 运行实际测试的代码

            var sequence = await _sequenceManager.CreateAsync(code,   // 编码
                name,           // 名称
                value,          // 序列值
                format,         // 格式
                length,         // 序列值长度
                date,           // 日期
                memo            // 备注
                );

            // Assert 断言，检验结果

            sequence.Id.ShouldNotBeNull();
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
            // 编码
            string code = "";

            // Act

            var result = await WithUnitOfWorkAsync(() => _sequenceManager.GetSequenceAsync(code));

            // Assert

            */
        }
        #endregion GetSequence




    }
}
