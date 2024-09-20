namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 交班日期 领域服务测试
    /// </summary>
    public class NurseHandoverManagerTests : HandoverDomainTestBase
    {
        private readonly NurseHandoverManager _nurseHandoverManager;

        public NurseHandoverManagerTests()
        {            
            _nurseHandoverManager = GetRequiredService<NurseHandoverManager>(); 

            //TODO: (交班日期 领域服务单元测试)……

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
