using YiJian.Recipe;

namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 会诊邀请医生 应用服务测试
    /// </summary>
    public class InviteDoctorAppServiceTests : RecipeApplicationTestBase
    {      
        private readonly IInviteDoctorRepository _inviteDoctorRepository;
        private readonly IInviteDoctorAppService _inviteDoctorAppService;
        
        public InviteDoctorAppServiceTests()
        {
            _inviteDoctorRepository = GetRequiredService<IInviteDoctorRepository>();
            _inviteDoctorAppService = GetRequiredService<IInviteDoctorAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new InviteDoctorCreation()
            {
                Id = default,
                GroupConsultationId = default,// 会诊id
                Code = "",      // 医生编码
                Name = "",      // 医生名称
                DeptCode = "",  // 科室编码
                DeptName = "",  // 科室名称
                CheckInStatus = default,// 状态，0：已邀请，1：已报到
                CheckInTime = null,// 报道时间
                Opinion = ""    // 意见
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _inviteDoctorAppService.CreateAsync(input)
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
            var input = new InviteDoctorUpdate()
            {
                Code = "",      // 医生编码
                Name = "",      // 医生名称
                DeptCode = "",  // 科室编码
                DeptName = "",  // 科室名称
                CheckInStatus = default,// 状态，0：已邀请，1：已报到
                CheckInTime = null,// 报道时间
                Opinion = ""    // 意见
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _inviteDoctorAppService.UpdateAsync(input)
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

            var inviteDoctorData = await _inviteDoctorAppService.GetAsync(id);

            // Assert 断言，检验结果

            inviteDoctorData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            await _inviteDoctorAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _inviteDoctorRepository.GetCountAsync()).ShouldBe(0);
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

            var inviteDoctorData = await _inviteDoctorAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            inviteDoctorData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetInviteDoctorPagedInput();

            // Act 运行实际测试的代码

            var inviteDoctorData = await _inviteDoctorAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            inviteDoctorData.Items.Count.ShouldBe(1);
            */
        }
    }
}
