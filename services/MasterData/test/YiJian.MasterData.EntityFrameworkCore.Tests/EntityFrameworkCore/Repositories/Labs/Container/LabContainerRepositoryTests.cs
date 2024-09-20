namespace YiJian.MasterData.Labs.Container
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 容器编码 仓储测试
    /// </summary>
    public class LabContainerRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ILabContainerRepository _labContainerRepository;

        public LabContainerRepositoryTests()
        {
            _labContainerRepository = GetRequiredService<ILabContainerRepository>();

            //TODO: (容器编码 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
