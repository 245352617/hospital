namespace YiJian.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 分诊患者id 应用服务测试
    /// </summary>
    public class OperationApplyAppServiceTests : RecipeApplicationTestBase
    {      
        private readonly IOperationApplyRepository _operationApplyRepository;
        private readonly OperationApplyManager _operationApplyManager;
        private readonly IOperationApplyAppService _operationApplyAppService;
        
        public OperationApplyAppServiceTests()
        {
            _operationApplyRepository = GetRequiredService<IOperationApplyRepository>();
            _operationApplyManager = GetRequiredService<OperationApplyManager>();  
            _operationApplyAppService = GetRequiredService<IOperationApplyAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new OperationApplyCreation()
            {
                Id = default,
                PIId = default, // 分诊患者id
                PatientId = "", // 患者唯一标识(HIS)
                PatientName = "",// 患者姓名
                Sex = "",       // 患者性别
                Age = "",       // 年龄
                IDNo = "",      // 身份证号
                BirthDay = null,// 生日
                ApplyNum = "",  // 申请单号
                ApplicantId = "",// 申请人Id
                ApplicantName = "",// 申请人名称
                ApplyTime = null,// 申请时间
                BloodType = "", // 血型
                RHCode = "",    // RH阴性阳性
                Height = 0,     // 身高
                Weight = 0,     // 体重
                ProposedOperationCode = "",// 拟施手术编码
                ProposedOperationName = "",// 拟施手术名称
                OperationLevel = "",// 手术等级
                ApplyDeptCode = "",// 申请科室编码
                ApplyDeptName = "",// 申请科室名称
                OperationLocation = "",// 手术位置
                DoctorCode = "",// 手术医生编码
                DoctorName = "",// 手术医生名称
                OperationAssistant = "",// 手术助手
                OperationPlanTime = null,// 手术计划时间
                OperationDuration = "",// 手术时长
                Remark = "",    // 备注
                Status = default,// 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                SubmitDate = null,// 手术申请提交日期
                OperationType = "",// 手术类型
                DiagnoseCode = "",// 术前诊断编码
                DiagnoseName = "",// 术前诊断名称
                Anesthesiologist = "",// 麻醉医生
                AnesthesiologistAssistant = "",// 麻醉助手
                TourNurse = "", // 巡回护士
                InstrumentNurse = "",// 器械护士
                AnaestheticCode = "",// 麻醉方式编码
                AnaestheticName = "",// 麻醉方式名称
                Sequence = 0    // 手术台次
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _operationApplyAppService.CreateAsync(input)
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
            var input = new OperationApplyUpdate()
            {
                PatientId = "", // 患者唯一标识(HIS)
                PatientName = "",// 患者姓名
                Sex = "",       // 患者性别
                Age = "",       // 年龄
                IDNo = "",      // 身份证号
                BirthDay = null,// 生日
                ApplyNum = "",  // 申请单号
                ApplicantId = "",// 申请人Id
                ApplicantName = "",// 申请人名称
                ApplyTime = null,// 申请时间
                BloodType = "", // 血型
                RHCode = "",    // RH阴性阳性
                Height = 0,     // 身高
                Weight = 0,     // 体重
                ProposedOperationCode = "",// 拟施手术编码
                ProposedOperationName = "",// 拟施手术名称
                OperationLevel = "",// 手术等级
                ApplyDeptCode = "",// 申请科室编码
                ApplyDeptName = "",// 申请科室名称
                OperationLocation = "",// 手术位置
                DoctorCode = "",// 手术医生编码
                DoctorName = "",// 手术医生名称
                OperationAssistant = "",// 手术助手
                OperationPlanTime = null,// 手术计划时间
                OperationDuration = "",// 手术时长
                Remark = "",    // 备注
                Status = default,// 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                SubmitDate = null,// 手术申请提交日期
                OperationType = "",// 手术类型
                DiagnoseCode = "",// 术前诊断编码
                DiagnoseName = "",// 术前诊断名称
                Anesthesiologist = "",// 麻醉医生
                AnesthesiologistAssistant = "",// 麻醉助手
                TourNurse = "", // 巡回护士
                InstrumentNurse = "",// 器械护士
                AnaestheticCode = "",// 麻醉方式编码
                AnaestheticName = "",// 麻醉方式名称
                Sequence = 0    // 手术台次
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _operationApplyAppService.UpdateAsync(input)
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

            var operationApplyData = await _operationApplyAppService.GetAsync(id);

            // Assert 断言，检验结果

            operationApplyData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            Guid id;

            // Act 运行实际测试的代码

            await _operationApplyAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _operationApplyRepository.GetCountAsync()).ShouldBe(0);
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

            var operationApplyData = await _operationApplyAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            operationApplyData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetOperationApplyPagedInput();

            // Act 运行实际测试的代码

            var operationApplyData = await _operationApplyAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            operationApplyData.Items.Count.ShouldBe(1);
            */
        }
    }
}
