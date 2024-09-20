namespace YiJian.MasterData.Labs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检验项目 仓储测试
    /// </summary>
    public class LabProjectRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ILabProjectRepository _labProjectRepository;

        public LabProjectRepositoryTests()
        {
            _labProjectRepository = GetRequiredService<ILabProjectRepository>();

            //TODO: (检验项目 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
