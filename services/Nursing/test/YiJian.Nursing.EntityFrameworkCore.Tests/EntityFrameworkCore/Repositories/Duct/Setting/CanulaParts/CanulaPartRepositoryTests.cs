namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Nursing.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:人体图-编号字典 仓储测试
    /// </summary>
    public class CanulaPartRepositoryTests : NursingEntityFrameworkCoreTestBase
    {
        private readonly ICanulaPartRepository _canulaPartRepository;

        public CanulaPartRepositoryTests()
        {
            _canulaPartRepository = GetRequiredService<ICanulaPartRepository>();

        }
        
    }
}
