namespace YiJian.MasterData
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检查申请注意事项 仓储测试
    /// </summary>
    public class ExamNoteRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IExamNoteRepository _examNoteRepository;

        public ExamNoteRepositoryTests()
        {
            _examNoteRepository = GetRequiredService<IExamNoteRepository>();

            //TODO: (检查申请注意事项 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
