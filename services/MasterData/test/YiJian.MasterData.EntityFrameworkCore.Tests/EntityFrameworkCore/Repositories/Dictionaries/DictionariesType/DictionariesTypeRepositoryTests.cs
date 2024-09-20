namespace YiJian.MasterData.DictionariesType
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 字典类型编码 仓储测试
    /// </summary>
    public class DictionariesTypeRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IDictionariesTypeRepository _dictionariesTypeRepository;

        public DictionariesTypeRepositoryTests()
        {
            _dictionariesTypeRepository = GetRequiredService<IDictionariesTypeRepository>();

            //TODO: (字典类型编码 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
