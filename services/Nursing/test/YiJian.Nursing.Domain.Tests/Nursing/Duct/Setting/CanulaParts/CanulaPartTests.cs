namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:人体图-编号字典 领域实体测试
    /// </summary>
    public class CanulaPartTests : NursingDomainTestBase
    {
        public CanulaPartTests()
        {  
        }

        [Fact]
        public void ModifyTest()
        {  
            /*
            // Arrange 为测试做准备工作

            // 
            Guid id = default;
            // 科室代码
            string deptCode = "";
            // 模块代码
            string moduleCode = "";
            // 部位名称
            string partName = "";
            // 部位编号
            string partNumber = "";
            // 排序
            int sort = 0;
            // 是否可用
            bool isEnable = true;
            // 是否删除
            bool isDeleted = true;

            var canulaPart = new CanulaPart(_guidGenerator.Create(), 
                deptCode,// 科室代码
                moduleCode,     // 模块代码
                partName,       // 部位名称
                partNumber,     // 部位编号
                sort,           // 排序
                isEnable,       // 是否可用
                isDeleted       // 是否删除
                );

            // Act 运行实际测试的代码

            canulaPart.Modify(moduleCode,// 模块代码
                partName,       // 部位名称
                partNumber,     // 部位编号
                sort,           // 排序
                isEnable,       // 是否可用
                isDeleted       // 是否删除
                );

            // Assert 断言，检验结果

            canulaPart.DeptCode.ShouldNotBeNull();
            */            
        }
    }
}
