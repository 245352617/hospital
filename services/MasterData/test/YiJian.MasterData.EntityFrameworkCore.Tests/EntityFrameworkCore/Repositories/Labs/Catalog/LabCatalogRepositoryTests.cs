namespace YiJian.MasterData.Labs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检验目录 仓储测试
    /// </summary>
    public class LabCatalogRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ILabCatalogRepository _labCatalogRepository;

        public LabCatalogRepositoryTests()
        {
            _labCatalogRepository = GetRequiredService<ILabCatalogRepository>();

            //TODO: (检验目录 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
