namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Handover.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 护士交班id 仓储测试
    /// </summary>
    public class NursePatientsRepositoryTests : HandoverEntityFrameworkCoreTestBase
    {
        private readonly INursePatientsRepository _nursePatientsRepository;

        public NursePatientsRepositoryTests()
        {
            _nursePatientsRepository = GetRequiredService<INursePatientsRepository>();

            //TODO: (护士交班id 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
