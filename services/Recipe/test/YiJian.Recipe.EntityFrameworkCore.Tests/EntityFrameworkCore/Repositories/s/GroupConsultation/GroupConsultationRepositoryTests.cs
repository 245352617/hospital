namespace YiJian.Recipes.GroupConsultation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Recipe.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 会诊管理 仓储测试
    /// </summary>
    public class GroupConsultationRepositoryTests : RecipeEntityFrameworkCoreTestBase
    {
        private readonly IGroupConsultationRepository _groupConsultationRepository;

        public GroupConsultationRepositoryTests()
        {
            _groupConsultationRepository = GetRequiredService<IGroupConsultationRepository>();

            //TODO: (会诊管理 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
