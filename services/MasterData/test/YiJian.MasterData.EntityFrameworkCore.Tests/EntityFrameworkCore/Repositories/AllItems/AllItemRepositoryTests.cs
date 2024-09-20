namespace YiJian.MasterData.AllItems
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 诊疗检查检验药品项目合集 仓储测试
    /// </summary>
    public class AllItemRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IAllItemRepository _allItemRepository;

        public AllItemRepositoryTests()
        {
            _allItemRepository = GetRequiredService<IAllItemRepository>();

            //TODO: (诊疗检查检验药品项目合集 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
