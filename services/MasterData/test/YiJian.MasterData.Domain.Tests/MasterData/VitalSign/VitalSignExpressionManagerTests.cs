namespace YiJian.MasterData.VitalSign
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 评分项 领域服务测试
    /// </summary>
    public class VitalSignExpressionManagerTests : MasterDataDomainTestBase
    {
        private readonly VitalSignExpressionManager _vitalSignExpressionManager;

        public VitalSignExpressionManagerTests()
        {            
            _vitalSignExpressionManager = GetRequiredService<VitalSignExpressionManager>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        { 
            /*
            // Arrange 为测试做准备工作

            // 
            Guid id = default;
            // 评分项
            string itemName = "";
            // Ⅰ级评分表达式
            string stLevelExpression = "";
            // Ⅱ级评分表达式
            string ndLevelExpression = "";
            // Ⅲ级评分表达式
            string rdLevelExpression = "";
            // Ⅳa级评分表达式
            string thALevelExpression = "";
            // Ⅳb级评分表达式
            string thBLevelExpression = "";
            // 默认Ⅰ级评分表达式
            string defaultStLevelExpression = "";
            // 默认Ⅱ级评分表达式
            string defaultNdLevelExpression = "";
            // 默认Ⅲ级评分表达式
            string defaultRdLevelExpression = "";
            // 默认Ⅳa级评分表达式
            string defaultThALevelExpression = "";
            // 默认Ⅳb级评分表达式
            string defaultThBLevelExpression = "";

            // Act 运行实际测试的代码

            var vitalSignExpression = await _vitalSignExpressionManager.CreateAsync(_guidGenerator.Create(), 
                itemName,// 评分项
                stLevelExpression,// Ⅰ级评分表达式
                ndLevelExpression,// Ⅱ级评分表达式
                rdLevelExpression,// Ⅲ级评分表达式
                thALevelExpression,// Ⅳa级评分表达式
                thBLevelExpression,// Ⅳb级评分表达式
                defaultStLevelExpression,// 默认Ⅰ级评分表达式
                defaultNdLevelExpression,// 默认Ⅱ级评分表达式
                defaultRdLevelExpression,// 默认Ⅲ级评分表达式
                defaultThALevelExpression,// 默认Ⅳa级评分表达式
                defaultThBLevelExpression// 默认Ⅳb级评分表达式
                );

            // Assert 断言，检验结果

            vitalSignExpression.Id.ShouldNotBeNull();
            */
        }

    }
}
