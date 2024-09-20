namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Nursing.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:护理项目表 仓储测试
    /// </summary>
    public class ParaItemRepositoryTests : NursingEntityFrameworkCoreTestBase
    {
        private readonly IParaItemRepository _paraItemRepository;

        public ParaItemRepositoryTests()
        {
            _paraItemRepository = GetRequiredService<IParaItemRepository>();

        }
        
    }
}
