namespace YiJian.MasterData.Labs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检验明细项 仓储测试
    /// </summary>
    public class LabTargetRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ILabTargetRepository _labTargetRepository;

        public LabTargetRepositoryTests()
        {
            _labTargetRepository = GetRequiredService<ILabTargetRepository>();

            //TODO: (检验明细项 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
