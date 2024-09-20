namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 药品字典 应用服务测试
    /// </summary>
    public class MedicineAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMedicineAppService _medicineAppService;
        
        public MedicineAppServiceTests()
        {
            _medicineRepository = GetRequiredService<IMedicineRepository>();
            _medicineAppService = GetRequiredService<IMedicineAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new MedicineCreation()
            {
                Code = "",      // 药品编码
                Name = "",      // 药品名称
                ScientificName = "",// 学名
                Alias = "",     // 别名
                CategoryCode = "",
                Category = "",  // 中药分类
                DefaultDosage = default,// 默认剂量
                DosageQty = null,// 剂量
                DosageUnit = "",// 剂量单位
                BasicUnit = "", // 基本单位
                BasicUnitPrice = 0,// 基本单位价格
                BigPackPrice = 0,// 包装价格
                BigPackAmount = 0,// 大包装量
                BigPackUnit = "",// 包装单位
                SmallPackSinglePrice = 0,// 小包装单价
                SmallPackUnit = "",// 小包装单位
                SmallPackAmount = 0,// 小包装量
                ChildrenPrice = null,// 儿童价格
                FixPrice = null,// 批发价格
                RetPrice = null,// 零售价格
                InsureType = "",// 医保类型：0自费,1甲类,2乙类，3丙类
                InsureCode = 0, // 医保类型代码
                PayRate = null, // 医保支付比例
                Factory = "",   // 厂家
                FactoryCode = "",// 厂家代码
                BatchNo = "",   // 批次号
                ExpirDate = null,// 失效期
                Weight = null,  // 重量
                WeightUnit = "",// 重量单位
                Volum = null,   // 体积
                VolumUnit = "", // 体积单位
                Remark = "",    // 备注
                IsSkinTest = null,// 皮试药
                IsCompound = null,// 复方药
                IsDrunk = null, // 麻醉药
                ToxicLevel = null,// 精神药  0非精神药,1一类精神药,2二类精神药
                IsHighRisk = null,// 高危药
                IsRefrigerated = null,// 冷藏药
                IsTumour = null,// 肿瘤药
                AntibioticLevel = null,// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
                IsPrecious = null,// 贵重药
                IsInsulin = null,// 胰岛素
                IsAnaleptic = null,// 兴奋剂
                IsAllergyTest = null,// 试敏药
                IsLimited = null,// 限制性用药标识
                LimitedNote = "",// 限制性用药描述
                Specification = "",// 规格
                DosageForm = "",// 剂型
                ExecDeptCode = "",
                ExecDept = "",  // 执行科室
                UsageCode = "",
                UsageName = "",
                FrequencyCode = "",
                FrequencyName = "",
                IsActive = true,// 是否启用
                IsEmergency = null,// 急救药
                PharmacyCode = "",// 药房代码
                Pharmacy = "",  // 药房
                AntibioticPermission = 0,// 抗生素权限
                PrescriptionPermission = 0,// 处方权
                Stock = 0,      // 库存
                BaseFlag = "",  // 基药标准  N普通,Y国基,P省基,C市基
                Usage = "",     // 药物用途
                IsFirstAid = true,// 是否急救
                Unpack = 0      // 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _medicineAppService.CreateAsync(input)
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
            var input = new MedicineUpdate()
            {
                Name = "",      // 药品名称
                Alias = "",     // 别名
                CategoryCode = "",
                Category = "",  // 中药分类
                BasicUnitPrice = 0,// 基本单位价格
                Remark = "",    // 备注
                IsEmergency = null,// 急救药
                Usage = "",     // 药物用途
                IsFirstAid = true,// 是否急救
                Unpack = 0      // 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _medicineAppService.UpdateAsync(input)
                            );

            // Assert 断言，检验结果

            
            */
        }

        [Fact]
        public async Task GetAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            var medicineData = await _medicineAppService.GetAsync(id);

            // Assert 断言，检验结果

            medicineData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            await _medicineAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _medicineRepository.GetCountAsync()).ShouldBe(0);
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

            var medicineData = await _medicineAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            medicineData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetMedicinePagedInput();

            // Act 运行实际测试的代码

            var medicineData = await _medicineAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            medicineData.Items.Count.ShouldBe(1);
            */
        }
    }
}
