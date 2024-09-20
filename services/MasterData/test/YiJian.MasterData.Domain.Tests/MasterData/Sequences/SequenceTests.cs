namespace YiJian.MasterData.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 序列 领域实体测试
    /// </summary>
    public class SequenceTests : MasterDataDomainTestBase
    {
        public SequenceTests()
        {
        }

        [Fact]
        public void ModifyTest()
        {

            // Arrange 为测试做准备工作

            // 编码
            string code = "Recipe";
            // 名称
            string name = "医嘱号";
            // 序列值
            int value = 0;
            // 格式
            string format = null; //"YYYYMMDD";
            // 序列值长度
            int length = 4;
            // 日期
            DateTime date = DateTime.Now;
            // 备注
            string memo = "医嘱";

            var sequence = new Sequence(code,   // 编码
                name,           // 名称
                value,          // 序列值
                format,         // 格式
                length,         // 序列值长度
                date,           // 日期
                memo            // 备注
                );

            // Act 运行实际测试的代码

            sequence.Increase();

            // Assert 断言，检验结果

            sequence.Value.ShouldBe(1);
            sequence.GetResult().ShouldBe("0001");

        }
    }
}
