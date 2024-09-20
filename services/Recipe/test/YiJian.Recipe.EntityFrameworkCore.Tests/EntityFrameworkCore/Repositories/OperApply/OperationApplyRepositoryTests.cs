namespace YiJian.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Recipe.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 分诊患者id 仓储测试
    /// </summary>
    public class OperationApplyRepositoryTests : RecipeEntityFrameworkCoreTestBase
    {
        private readonly IOperationApplyRepository _operationApplyRepository;

        public OperationApplyRepositoryTests()
        {
            _operationApplyRepository = GetRequiredService<IOperationApplyRepository>();

            //TODO: (分诊患者id 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
