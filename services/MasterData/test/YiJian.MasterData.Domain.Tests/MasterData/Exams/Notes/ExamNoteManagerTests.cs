namespace YiJian.MasterData
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检查申请注意事项 领域服务测试
    /// </summary>
    public class ExamNoteManagerTests : MasterDataDomainTestBase
    {
      

        public ExamNoteManagerTests()
        {            
           
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        { 
            /*
            // Arrange 为测试做准备工作

            // 注意事项编码
            string code = "";
            // 注意事项名称
            string name = "";
            // 科室编码
            string deptCode = "";
            // 科室
            string dept = "";
            // 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
            string displayName = "";
            // 检查申请描述模板
            string descTemplate = "";
            // 是否启用
            bool isActive = true;

            // Act 运行实际测试的代码

            var examNote = await _examNoteManager.CreateAsync(code,   // 注意事项编码
                name,           // 注意事项名称
                deptCode,       // 科室编码
                dept,           // 科室
                displayName,    // 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
                descTemplate,   // 检查申请描述模板
                isActive        // 是否启用
                );

            // Assert 断言，检验结果

            examNote.Id.ShouldNotBeNull();
            */
        }

    }
}
