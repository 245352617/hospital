namespace YiJian.MasterData.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检查申请项目 仓储测试
    /// </summary>
    public class ExamProjectRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IExamProjectRepository _examProjectRepository;

        public ExamProjectRepositoryTests()
        {
            _examProjectRepository = GetRequiredService<IExamProjectRepository>();

            //TODO: (检查申请项目 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
