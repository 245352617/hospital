namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.Recipe.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 会诊邀请医生 仓储测试
    /// </summary>
    public class InviteDoctorRepositoryTests : RecipeEntityFrameworkCoreTestBase
    {
        private readonly IInviteDoctorRepository _inviteDoctorRepository;

        public InviteDoctorRepositoryTests()
        {
            _inviteDoctorRepository = GetRequiredService<IInviteDoctorRepository>();

            //TODO: (会诊邀请医生 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}
