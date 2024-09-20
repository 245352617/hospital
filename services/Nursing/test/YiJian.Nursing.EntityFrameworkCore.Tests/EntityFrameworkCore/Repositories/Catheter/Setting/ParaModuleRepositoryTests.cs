namespace YiJian.Nursing.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Nursing.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:模块参数 仓储测试
    /// </summary>
    public class ParaModuleRepositoryTests : NursingEntityFrameworkCoreTestBase
    {
        private readonly IParaModuleRepository _paraModuleRepository;

        public ParaModuleRepositoryTests()
        {
            _paraModuleRepository = GetRequiredService<IParaModuleRepository>();

        }
        
    }
}
