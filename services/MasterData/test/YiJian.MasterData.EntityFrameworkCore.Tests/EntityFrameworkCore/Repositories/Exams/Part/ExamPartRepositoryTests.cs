namespace YiJian.MasterData.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检查部位 仓储测试
    /// </summary>
    public class ExamPartRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IExamPartRepository _examPartRepository;

        public ExamPartRepositoryTests()
        {
            _examPartRepository = GetRequiredService<IExamPartRepository>();

            //TODO: (检查部位 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
