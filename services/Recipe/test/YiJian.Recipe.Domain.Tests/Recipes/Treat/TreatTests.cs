namespace YiJian.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using YiJian.Recipe;

    /// <summary>
    /// 诊疗项 领域实体测试
    /// </summary>
    public class TreatTests : RecipeDomainTestBase
    {
        public TreatTests()
        {
        }

        [Fact]
        public void Should_Modify_OK_Test()
        {
            /*
            // Arrange 为测试做准备工作

            // 
            Guid id = default;
            // 编码
            string recipeCode = "";
            // 名称
            string recipeName = "";
            // 临床诊断（冗余设计）
            string diagnosis = "";
            // 医嘱类型编码
            string prescribeTypeCode = "";
            // 医嘱类型：临嘱、长嘱、出院带药等
            string prescribeType = "";
            // 医嘱项目分类编码 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
            string recipeCategoryCode = "";
            // 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
            string recipeCategory = "";
            // 医嘱说明
            string remarks = "";
            // 是否补录
            bool isBackTracking = true;
            // 处方号
            string prescriptionNo = "";
            // 医嘱号
            string recipeNo = "";
            // 医嘱分组号：分组顺序1,2,3
            int recipeGroupNo = 0;
            // 申请医生编码
            string applyDoctorCode = "";
            // 申请医生
            string applyDoctor = "";
            // 申请科室编码
            string applyDeptCode = "";
            // 申请科室
            string applyDept = "";
            // 管培生代码
            string traineeCode = "";
            // 管培生
            string trainee = "";
            // 执行科室编码
            string execDeptCode = "";
            // 执行科室
            string execDept = "";
            // 状态编码
            string statusCode = "";
            // 状态
            RecipeStatus status = default;
            // 状态描述
            string statusDescription = "";
            // 付费类型编码
            string payTypeCode = "";
            // 付费类型：0-Self-自费项目 1-Insurance-医保项目 2-Other-其它
            RecipePayType payType = default;
            // 付费类型描述：自费项目 医保项目
            string PayTypeDescription = "";
            // 医保目录编码
            string insuranceCode = "";
            // 医保目录：0-自费项目1-医保项目(甲、乙)
            InsuranceCatalog insurance = default;
            // 医保目录描述
            string insuranceDescription = "";
            // 病人标识
            Guid pIID = default;
            // 用量
            int amount = 0;
            // 用量单位
            string Unit = "";
            // 单价
            decimal price = 0;
            // 总费用
            decimal totalFee = 0;

            var treat = new Treat(_guidGenerator.Create(), 
                recipeCode,// 编码
                recipeName,     // 名称
                diagnosis,      // 临床诊断（冗余设计）
                prescribeTypeCode,// 医嘱类型编码
                prescribeType,  // 医嘱类型：临嘱、长嘱、出院带药等
                recipeCategoryCode,// 医嘱项目分类编码 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
                recipeCategory, // 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
                remarks,        // 医嘱说明
                isBackTracking, // 是否补录
                prescriptionNo, // 处方号
                recipeNo,       // 医嘱号
                recipeGroupNo,    // 医嘱分组号：分组顺序1,2,3
                applyDoctorCode,// 申请医生编码
                applyDoctor,    // 申请医生
                applyDeptCode,  // 申请科室编码
                applyDept,      // 申请科室
                traineeCode,    // 管培生代码
                trainee,        // 管培生
                execDeptCode,   // 执行科室编码
                execDept,       // 执行科室
                statusCode,     // 状态编码
                status,         // 状态
                statusDescription,// 状态描述
                payTypeCode,    // 付费类型编码
                payType,        // 付费类型：0-Self-自费项目 1-Insurance-医保项目 2-Other-其它
                PayTypeDescription,// 付费类型描述：自费项目 医保项目
                recipeFeeStatusCode,// 收费状态码
                recipeFeeStatus,// 收费状态
                insuranceCode,  // 医保目录编码
                insurance,      // 医保目录：0-自费项目1-医保项目(甲、乙)
                insuranceDescription,// 医保目录描述
                pIID,           // 病人标识
                amount,         // 用量
                Unit,     // 用量单位
                price,          // 单价
                totalFee        // 总费用
                );

            // Act 运行实际测试的代码

            treat.Modify(Unit,// 用量单位
                price,          // 单价
                totalFee        // 总费用
                );

            // Assert 断言，检验结果

            treat.Amount.ShouldNotBeNull();
            */
        }
    }
}

