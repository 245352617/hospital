namespace YiJian.MasterData.DictionariesType
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 字典类型编码 领域服务测试
    /// </summary>
    public class DictionariesTypeManagerTests : MasterDataDomainTestBase
    {
        private readonly DictionariesTypeManager _dictionariesTypeManager;

        public DictionariesTypeManagerTests()
        {            
            _dictionariesTypeManager = GetRequiredService<DictionariesTypeManager>(); 

            //TODO: (字典类型编码 领域服务单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }

        [Fact]
        public async Task CreateAsyncTest()
        {
            //TODO: (准备新增测试)……
        }

    }
}
