namespace YiJian.Nursing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Nursing.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 表:导管字典-通用业务 仓储测试
    /// </summary>
    public class DictRepositoryTests : NursingEntityFrameworkCoreTestBase
    {
        private readonly IDictRepository _dictRepository;

        public DictRepositoryTests()
        {
            _dictRepository = GetRequiredService<IDictRepository>();

        }
        
    }
}
