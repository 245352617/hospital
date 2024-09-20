namespace YiJian.MasterData.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检查明细项 仓储测试
    /// </summary>
    public class ExamTargetRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IExamTargetRepository _examTargetRepository;

        public ExamTargetRepositoryTests()
        {
            _examTargetRepository = GetRequiredService<IExamTargetRepository>();

            //TODO: (检查明细项 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
