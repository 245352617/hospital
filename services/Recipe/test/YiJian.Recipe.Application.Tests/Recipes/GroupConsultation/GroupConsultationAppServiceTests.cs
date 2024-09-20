using YiJian.Recipe;

namespace YiJian.Recipes.GroupConsultation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 会诊管理 应用服务测试
    /// </summary>
    public class GroupConsultationAppServiceTests : RecipeApplicationTestBase
    {      
        private readonly IGroupConsultationRepository _groupConsultationRepository;
        private readonly GroupConsultationManager _groupConsultationManager;
        private readonly IGroupConsultationAppService _groupConsultationAppService;
        
        public GroupConsultationAppServiceTests()
        {
            _groupConsultationRepository = GetRequiredService<IGroupConsultationRepository>();
            _groupConsultationManager = GetRequiredService<GroupConsultationManager>();  
            _groupConsultationAppService = GetRequiredService<IGroupConsultationAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new GroupConsultationCreation()
            {
                Id = default,
                PIID = default, // 分诊患者id
                PatientId = "", // 患者id
                TypeCode = "",  // 会诊类型
                TypeName = "",
                StartTime = DateTime.Now,// 会诊开始时间
                Status = default,// 会诊状态
                ObjectiveCode = "",// 会诊目的编码
                ObjectiveContent = "",// 会诊目的内容
                ApplyDeptCode = "",// 申请科室编码
                ApplyDeptName = "",// 申请科室名称
                ApplyCode = "", // 申请人编码
                ApplyName = "", // 申请人名称
                Mobile = "",    // 联系方式
                ApplyTime = null,// 申请时间
                Place = "",     // 地点
                VitalSigns = "",// 生命体征
                Test = "",      // 检验
                Inspect = "",   // 检查
                DoctorOrder = "",// 医嘱
                Diagnose = "",  // 诊断
                Content = "",   // 病历摘要
                Summary = "",   // 总结
                InviteDoctors = default// 会诊邀请医生
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _groupConsultationAppService.CreateAsync(input)
                                        );

            // Assert 断言，检验结果

            id.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task UpdateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new GroupConsultationUpdate()
            {
                PatientId = "", // 患者id
                TypeCode = "",  // 会诊类型
                TypeName = "",
                StartTime = DateTime.Now,// 会诊开始时间
                Status = default,// 会诊状态
                ObjectiveCode = "",// 会诊目的编码
                ObjectiveContent = "",// 会诊目的内容
                ApplyDeptCode = "",// 申请科室编码
                ApplyDeptName = "",// 申请科室名称
                ApplyCode = "", // 申请人编码
                ApplyName = "", // 申请人名称
                Mobile = "",    // 联系方式
                ApplyTime = null,// 申请时间
                Place = "",     // 地点
                VitalSigns = "",// 生命体征
                Test = "",      // 检验
                Inspect = "",   // 检查
                DoctorOrder = "",// 医嘱
                Diagnose = "",  // 诊断
                Content = "",   // 病历摘要
                Summary = "",   // 总结
                InviteDoctors = default// 会诊邀请医生
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _groupConsultationAppService.UpdateAsync(input)
                            );

            // Assert 断言，检验结果

            
            */
        }

        [Fact]
        public async Task GetAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            var groupConsultationData = await _groupConsultationAppService.GetAsync(id);

            // Assert 断言，检验结果

            groupConsultationData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            await _groupConsultationAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _groupConsultationRepository.GetCountAsync()).ShouldBe(0);
            */
        }

        [Fact]
        public async Task GetListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            string filter = null;
            string sorting = null;

            // Act 运行实际测试的代码

            var groupConsultationData = await _groupConsultationAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            groupConsultationData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetGroupConsultationPagedInput();

            // Act 运行实际测试的代码

            var groupConsultationData = await _groupConsultationAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            groupConsultationData.Items.Count.ShouldBe(1);
            */
        }
    }
}
