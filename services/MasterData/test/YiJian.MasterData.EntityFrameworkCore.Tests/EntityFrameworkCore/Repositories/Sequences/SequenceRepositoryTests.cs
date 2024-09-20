namespace YiJian.MasterData.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 编码 仓储测试
    /// </summary>
    public class SequenceRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ISequenceRepository _sequenceRepository;

        public SequenceRepositoryTests()
        {
            _sequenceRepository = GetRequiredService<ISequenceRepository>();

            //TODO: (编码 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
