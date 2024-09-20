namespace YiJian.MasterData.Labs.Position
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检验标本采集部位 仓储测试
    /// </summary>
    public class LabSpecimenPositionRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ILabSpecimenPositionRepository _labSpecimenPositionRepository;

        public LabSpecimenPositionRepositoryTests()
        {
            _labSpecimenPositionRepository = GetRequiredService<ILabSpecimenPositionRepository>();

            //TODO: (检验标本采集部位 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
