using YiJian.Recipe;

namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 会诊邀请医生 领域实体测试
    /// </summary>
    public class InviteDoctorTests : RecipeDomainTestBase
    {
        public InviteDoctorTests()
        {  
        }

        [Fact]
        public void ModifyTest()
        {  
            /*
            // Arrange 为测试做准备工作

            // 
            Guid id = default;
            // 会诊id
            Guid groupConsultationId = default;
            // 医生编码
            string code = "";
            // 医生名称
            string name = "";
            // 科室编码
            string deptCode = "";
            // 科室名称
            string deptName = "";
            // 状态，0：已邀请，1：已报到
            CheckInStatus checkInStatus = default;
            // 报道时间
            DateTime? checkInTime = null;
            // 意见
            string opinion = "";

            var inviteDoctor = new InviteDoctor(_guidGenerator.Create(), 
                groupConsultationId,// 会诊id
                code,           // 医生编码
                name,           // 医生名称
                deptCode,       // 科室编码
                deptName,       // 科室名称
                checkInStatus,  // 状态，0：已邀请，1：已报到
                checkInTime,    // 报道时间
                opinion         // 意见
                );

            // Act 运行实际测试的代码

            inviteDoctor.Modify(code,   // 医生编码
                name,           // 医生名称
                deptCode,       // 科室编码
                deptName,       // 科室名称
                checkInStatus,  // 状态，0：已邀请，1：已报到
                checkInTime,    // 报道时间
                opinion         // 意见
                );

            // Assert 断言，检验结果

            inviteDoctor.GroupConsultationId.ShouldNotBeNull();
            */            
        }
    }
}
