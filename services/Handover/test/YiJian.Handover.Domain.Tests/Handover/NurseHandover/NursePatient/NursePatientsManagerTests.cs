namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 护士交班id 领域服务测试
    /// </summary>
    public class NursePatientsManagerTests : HandoverDomainTestBase
    {
        private readonly NursePatientsManager _nursePatientsManager;

        public NursePatientsManagerTests()
        {            
            _nursePatientsManager = GetRequiredService<NursePatientsManager>(); 

            //TODO: (护士交班id 领域服务单元测试)……

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
