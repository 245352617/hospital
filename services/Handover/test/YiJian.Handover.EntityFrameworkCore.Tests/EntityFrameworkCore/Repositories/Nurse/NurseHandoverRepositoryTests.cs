namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Handover.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 交班日期 仓储测试
    /// </summary>
    public class NurseHandoverRepositoryTests : HandoverEntityFrameworkCoreTestBase
    {
        private readonly INurseHandoverRepository _nurseHandoverRepository;

        public NurseHandoverRepositoryTests()
        {
            _nurseHandoverRepository = GetRequiredService<INurseHandoverRepository>();

            //TODO: (交班日期 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
