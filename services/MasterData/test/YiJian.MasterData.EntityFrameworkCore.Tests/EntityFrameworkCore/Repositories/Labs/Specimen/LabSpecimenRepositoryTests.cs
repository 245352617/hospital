namespace YiJian.MasterData.Labs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检验标本 仓储测试
    /// </summary>
    public class LabSpecimenRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ILabSpecimenRepository _labSpecimenRepository;

        public LabSpecimenRepositoryTests()
        {
            _labSpecimenRepository = GetRequiredService<ILabSpecimenRepository>();

            //TODO: (检验标本 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
