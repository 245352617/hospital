namespace YiJian.MasterData.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 检查目录 仓储测试
    /// </summary>
    public class ExamCatalogRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IExamCatalogRepository _examCatalogRepository;

        public ExamCatalogRepositoryTests()
        {
            _examCatalogRepository = GetRequiredService<IExamCatalogRepository>();

            //TODO: (检查目录 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
