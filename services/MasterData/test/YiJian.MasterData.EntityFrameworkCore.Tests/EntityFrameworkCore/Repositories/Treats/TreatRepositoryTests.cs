namespace YiJian.MasterData.Treats
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 诊疗项目字典 仓储测试
    /// </summary>
    public class TreatRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly ITreatRepository _treatRepository;

        public TreatRepositoryTests()
        {
            _treatRepository = GetRequiredService<ITreatRepository>();

            //TODO: (诊疗项目字典 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
