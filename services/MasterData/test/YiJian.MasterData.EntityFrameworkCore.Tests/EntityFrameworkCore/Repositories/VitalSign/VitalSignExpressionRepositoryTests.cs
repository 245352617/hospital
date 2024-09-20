namespace YiJian.MasterData.VitalSign
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 评分项 仓储测试
    /// </summary>
    public class VitalSignExpressionRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IVitalSignExpressionRepository _vitalSignExpressionRepository;

        public VitalSignExpressionRepositoryTests()
        {
            _vitalSignExpressionRepository = GetRequiredService<IVitalSignExpressionRepository>();

            //TODO: (评分项 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
