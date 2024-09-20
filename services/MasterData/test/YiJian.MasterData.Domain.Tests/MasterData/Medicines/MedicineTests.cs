namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 药品字典 领域实体测试
    /// </summary>
    public class MedicineTests : MasterDataDomainTestBase
    {
        public MedicineTests()
        {  
        }

        [Fact]
        public void ModifyTest()
        {  
            /*
            // Arrange 为测试做准备工作

            // 药品编码
            string code = "";
            // 药品名称
            string name = "";
            // 学名
            string scientificName = "";
            // 别名
            string alias = "";
            // 
            string categoryCode = "";
            // 中药分类
            string category = "";
            // 默认剂量
            double defaultDosage = default;
            // 剂量
            double? dosageQty = null;
            // 剂量单位
            string dosageUnit = "";
            // 基本单位
            string basicUnit = "";
            // 基本单位价格
            decimal basicUnitPrice = 0;
            // 包装价格
            decimal bigPackPrice = 0;
            // 大包装量
            int bigPackAmount = 0;
            // 包装单位
            string bigPackUnit = "";
            // 小包装单价
            decimal smallPackSinglePrice = 0;
            // 小包装单位
            string smallPackUnit = "";
            // 小包装量
            int smallPackAmount = 0;
            // 儿童价格
            decimal? childrenPrice = null;
            // 批发价格
            decimal? fixPrice = null;
            // 零售价格
            decimal? retPrice = null;
            // 医保类型：0自费,1甲类,2乙类，3丙类
            string insureType = "";
            // 医保类型代码
            int insureCode = 0;
            // 医保支付比例
            int? payRate = null;
            // 厂家
            string factory = "";
            // 厂家代码
            string factoryCode = "";
            // 批次号
            string batchNo = "";
            // 失效期
            DateTime? expirDate = null;
            // 重量
            double? weight = null;
            // 重量单位
            string weightUnit = "";
            // 体积
            double? volum = null;
            // 体积单位
            string volumUnit = "";
            // 备注
            string remark = "";
            // 皮试药
            bool? isSkinTest = null;
            // 复方药
            bool? isCompound = null;
            // 麻醉药
            bool? isDrunk = null;
            // 精神药  0非精神药,1一类精神药,2二类精神药
            int? toxicLevel = null;
            // 高危药
            bool? isHighRisk = null;
            // 冷藏药
            bool? isRefrigerated = null;
            // 肿瘤药
            bool? isTumour = null;
            // 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
            int? antibioticLevel = null;
            // 贵重药
            bool? isPrecious = null;
            // 胰岛素
            bool? isInsulin = null;
            // 兴奋剂
            bool? isAnaleptic = null;
            // 试敏药
            bool? isAllergyTest = null;
            // 限制性用药标识
            bool? isLimited = null;
            // 限制性用药描述
            string limitedNote = "";
            // 规格
            string specification = "";
            // 剂型
            string dosageForm = "";
            // 
            string execDeptCode = "";
            // 执行科室
            string execDept = "";
            // 
            string usageCode = "";
            // 
            string usageName = "";
            // 
            string frequencyCode = "";
            // 
            string frequencyName = "";
            // 是否启用
            bool isActive = true;
            // 急救药
            bool? isEmergency = null;
            // 药房代码
            string pharmacyCode = "";
            // 药房
            string pharmacy = "";
            // 抗生素权限
            int antibioticPermission = 0;
            // 处方权
            int prescriptionPermission = 0;
            // 库存
            int stock = 0;
            // 基药标准  N普通,Y国基,P省基,C市基
            string baseFlag = "";
            // 药物用途
            string usage = "";
            // 是否急救
            bool isFirstAid = true;
            // 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
            int unpack = 0;

            var medicine = new Medicine(code,   // 药品编码
                name,           // 药品名称
                scientificName, // 学名
                alias,          // 别名
                categoryCode,
                category,       // 中药分类
                defaultDosage,  // 默认剂量
                dosageQty,      // 剂量
                dosageUnit,     // 剂量单位
                basicUnit,      // 基本单位
                basicUnitPrice, // 基本单位价格
                bigPackPrice,   // 包装价格
                bigPackAmount,  // 大包装量
                bigPackUnit,    // 包装单位
                smallPackSinglePrice,// 小包装单价
                smallPackUnit,  // 小包装单位
                smallPackAmount,// 小包装量
                childrenPrice,  // 儿童价格
                fixPrice,       // 批发价格
                retPrice,       // 零售价格
                insureType,     // 医保类型：0自费,1甲类,2乙类，3丙类
                insureCode,     // 医保类型代码
                payRate,        // 医保支付比例
                factory,        // 厂家
                factoryCode,    // 厂家代码
                batchNo,        // 批次号
                expirDate,      // 失效期
                weight,         // 重量
                weightUnit,     // 重量单位
                volum,          // 体积
                volumUnit,      // 体积单位
                remark,         // 备注
                isSkinTest,     // 皮试药
                isCompound,     // 复方药
                isDrunk,        // 麻醉药
                toxicLevel,     // 精神药  0非精神药,1一类精神药,2二类精神药
                isHighRisk,     // 高危药
                isRefrigerated, // 冷藏药
                isTumour,       // 肿瘤药
                antibioticLevel,// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
                isPrecious,     // 贵重药
                isInsulin,      // 胰岛素
                isAnaleptic,    // 兴奋剂
                isAllergyTest,  // 试敏药
                isLimited,      // 限制性用药标识
                limitedNote,    // 限制性用药描述
                specification,  // 规格
                dosageForm,     // 剂型
                execDeptCode,
                execDept,       // 执行科室
                usageCode,
                usageName,
                frequencyCode,
                frequencyName,
                isActive,       // 是否启用
                isEmergency,    // 急救药
                pharmacyCode,   // 药房代码
                pharmacy,       // 药房
                antibioticPermission,// 抗生素权限
                prescriptionPermission,// 处方权
                stock,          // 库存
                baseFlag,       // 基药标准  N普通,Y国基,P省基,C市基
                usage,          // 药物用途
                isFirstAid,     // 是否急救
                unpack          // 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
                );

            // Act 运行实际测试的代码

            medicine.Modify(name,   // 药品名称
                alias,          // 别名
                categoryCode,
                category,       // 中药分类
                basicUnitPrice, // 基本单位价格
                remark,         // 备注
                isEmergency,    // 急救药
                usage,          // 药物用途
                isFirstAid,     // 是否急救
                unpack          // 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
                );

            // Assert 断言，检验结果

            medicine.MedicineCode.ShouldNotBeNull();
            */            
        }
    }
}
