namespace YiJian.Nursing.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:模块参数 领域实体测试
    /// </summary>
    public class ParaModuleTests : NursingDomainTestBase
    {
        public ParaModuleTests()
        {  
        }

        [Fact]
        public void ModifyTest()
        {  
            /*
            // Arrange 为测试做准备工作

            // 
            Guid id = default;
            // 模块代码
            string moduleCode = "";
            // 模块名称
            string moduleName = "";
            // 模块显示名称
            string displayName = "";
            // 科室代码
            string deptCode = "";
            // 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
            string moduleType = "";
            // 是否血流内导管
            bool isBloodFlow = true;
            // 模块拼音
            string py = "";
            // 排序
            int sortNum = 0;
            // 是否启用
            bool isEnable = true;
            // 是否有效(1-有效，0-无效)
            int validState = 0;

            var paraModule = new ParaModule(_guidGenerator.Create(), 
                moduleCode,// 模块代码
                moduleName,     // 模块名称
                displayName,    // 模块显示名称
                deptCode,       // 科室代码
                moduleType,     // 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
                isBloodFlow,    // 是否血流内导管
                py,             // 模块拼音
                sortNum,        // 排序
                isEnable,       // 是否启用
                validState      // 是否有效(1-有效，0-无效)
                );

            // Act 运行实际测试的代码

            paraModule.Modify(moduleName,// 模块名称
                displayName,    // 模块显示名称
                deptCode,       // 科室代码
                moduleType,     // 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
                isBloodFlow,    // 是否血流内导管
                py,             // 模块拼音
                sortNum,        // 排序
                isEnable,       // 是否启用
                validState      // 是否有效(1-有效，0-无效)
                );

            // Assert 断言，检验结果

            paraModule.ModuleCode.ShouldNotBeNull();
            */            
        }
    }
}
