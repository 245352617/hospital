namespace YiJian.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using YiJian.Recipe;

    /// <summary>
    /// 检验项 领域实体测试
    /// </summary>
    public class LisTests : RecipeDomainTestBase
    {
        public LisTests()
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
            // 状态描述
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
            // 临床症状
            string clinicalSymptom = "";
            // 标本编码
            string specimenCode = "";
            // 标本
            string specimen = "";
            // 标本采集部位编码
            string specimenPartCode = "";
            // 标本采集部位
            string specimenPart = "";
            // 标本容器代码
            string specimenContainerCode = "";
            // 标本容器
            string specimenContainer = "";
            // 标本容器颜色 0 红帽 1 蓝帽 2 紫帽
            int? specimenContainerColor = null;
            // 标本说明
            string specimenDescription = "";
            // 标本采集时间
            DateTime? specimenCollectDT = null;
            // 标本接收时间
            DateTime? specimenReceivedDT = null;
            // 检验类别编码
            string catalogCode = "";
            // 检验类别
            string catalog = "";
            // 出报告时间
            DateTime? reportDT = null;
            // 确认报告医生编码
            string reportDoctorCode = "";
            // 确认报告医生
            string reportDoctor = "";
            // 报告标识
            bool hasReport = true;
            // 单价
            decimal price = 0;
            // 总费用
            decimal totalFee = 0;
            // 位置编码
            string positionCode = "";
            // 位置
            string position = "";
            // 是否紧急
            bool isEmergency = true;
            // 明细项
            ICollection<LisTarget> targets = default;

            var lis = new Lis(_guidGenerator.Create(), 
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
                status,         // 状态描述
                statusDescription,// 状态描述
                payTypeCode,    // 付费类型编码
                payType,        // 付费类型：0-Self-自费项目 1-Insurance-医保项目 2-Other-其它
                PayTypeDescription,// 付费类型描述：自费项目 医保项目
                recipeFeeStatusCode,// 收费状态码
                recipeFeeStatus,// 收费状态
                insuranceCode,  // 医保目录编码
                insurance,      // 医保目录：0-自费项目1-医保项目(甲、乙)
                insuranceDescription,// 医保目录描述
                isReceiptPrinted,// 申请单打印标识
                rePrintReceiptCount,// 补打次数 默认0次
                rePrintReceiptTime,// 补打时间
                rePrintReceiptor,// 补打人
                isRecipePrinted,// 医嘱单打印标识
                signCert,       // 签名证书
                signValue,      // 签名值
                timestampValue, // 时间戳值
                signFlow,       // Base64 编码格式的签章图片
                pIID,           // 病人标识
                clinicalSymptom,// 临床症状
                specimenCode,   // 标本编码
                specimen,       // 标本
                specimenPartCode,// 标本采集部位编码
                specimenPart,   // 标本采集部位
                specimenContainerCode,// 标本容器代码
                specimenContainer,// 标本容器
                specimenContainerColor,// 标本容器颜色 0 红帽 1 蓝帽 2 紫帽
                specimenDescription,// 标本说明
                specimenCollectDT,// 标本采集时间
                specimenReceivedDT,// 标本接收时间
                catalogCode,    // 检验类别编码
                catalog,        // 检验类别
                reportDT,       // 出报告时间
                reportDoctorCode,// 确认报告医生编码
                reportDoctor,   // 确认报告医生
                hasReport,      // 报告标识
                price,          // 单价
                totalFee,       // 总费用
                positionCode,   // 位置编码
                position,       // 位置
                isEmergency,    // 是否紧急
                targets         // 明细项
                );

            // Act 运行实际测试的代码

            lis.Modify(specimenCode,// 标本编码
                specimen,       // 标本
                specimenPartCode,// 标本采集部位编码
                specimenPart,   // 标本采集部位
                specimenContainerCode,// 标本容器代码
                specimenContainer,// 标本容器
                specimenContainerColor,// 标本容器颜色 0 红帽 1 蓝帽 2 紫帽
                specimenDescription,// 标本说明
                specimenCollectDT,// 标本采集时间
                specimenReceivedDT,// 标本接收时间
                catalogCode,    // 检验类别编码
                catalog,        // 检验类别
                reportDT,       // 出报告时间
                reportDoctorCode,// 确认报告医生编码
                reportDoctor,   // 确认报告医生
                hasReport,      // 报告标识
                price,          // 单价
                totalFee,       // 总费用
                positionCode,   // 位置编码
                position,       // 位置
                isEmergency,    // 是否紧急
                targets         // 明细项
                );

            // Assert 断言，检验结果

            lis.ClinicalSymptom.ShouldNotBeNull();
            */
        }
    }
}

