namespace YiJian.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 分诊患者id 领域实体测试
    /// </summary>
    public class OperationApplyTests : RecipeDomainTestBase
    {
        public OperationApplyTests()
        {  
        }

        [Fact]
        public void ModifyTest()
        {  
            /*
            // Arrange 为测试做准备工作

            // 
            Guid id = default;
            // 分诊患者id
            Guid pIId = default;
            // 患者唯一标识(HIS)
            string patientId = "";
            // 患者姓名
            string patientName = "";
            // 患者性别
            string sex = "";
            // 年龄
            string age = "";
            // 身份证号
            string iDNo = "";
            // 生日
            DateTime? birthDay = null;
            // 申请单号
            string applyNum = "";
            // 申请人Id
            string applicantId = "";
            // 申请人名称
            string applicantName = "";
            // 申请时间
            DateTime? applyTime = null;
            // 血型
            string bloodType = "";
            // RH阴性阳性
            string rHCode = "";
            // 身高
            decimal height = 0;
            // 体重
            decimal weight = 0;
            // 拟施手术编码
            string proposedOperationCode = "";
            // 拟施手术名称
            string proposedOperationName = "";
            // 手术等级
            string operationLevel = "";
            // 申请科室编码
            string applyDeptCode = "";
            // 申请科室名称
            string applyDeptName = "";
            // 手术位置
            string operationLocation = "";
            // 手术医生编码
            string doctorCode = "";
            // 手术医生名称
            string doctorName = "";
            // 手术助手
            string operationAssistant = "";
            // 手术计划时间
            DateTime? operationPlanTime = null;
            // 手术时长
            string operationDuration = "";
            // 备注
            string remark = "";
            // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
            OperationApplyStatus status = default;
            // 手术申请提交日期
            DateTime? submitDate = null;
            // 手术类型
            string operationType = "";
            // 术前诊断编码
            string diagnoseCode = "";
            // 术前诊断名称
            string diagnoseName = "";
            // 麻醉医生
            string anesthesiologist = "";
            // 麻醉助手
            string anesthesiologistAssistant = "";
            // 巡回护士
            string tourNurse = "";
            // 器械护士
            string instrumentNurse = "";
            // 麻醉方式编码
            string anaestheticCode = "";
            // 麻醉方式名称
            string anaestheticName = "";
            // 手术台次
            int sequence = 0;

            var operationApply = new OperationApply(_guidGenerator.Create(), 
                pIId,   // 分诊患者id
                patientId,      // 患者唯一标识(HIS)
                patientName,    // 患者姓名
                sex,            // 患者性别
                age,            // 年龄
                iDNo,           // 身份证号
                birthDay,       // 生日
                applyNum,       // 申请单号
                applicantId,    // 申请人Id
                applicantName,  // 申请人名称
                applyTime,      // 申请时间
                bloodType,      // 血型
                rHCode,         // RH阴性阳性
                height,         // 身高
                weight,         // 体重
                proposedOperationCode,// 拟施手术编码
                proposedOperationName,// 拟施手术名称
                operationLevel, // 手术等级
                applyDeptCode,  // 申请科室编码
                applyDeptName,  // 申请科室名称
                operationLocation,// 手术位置
                doctorCode,     // 手术医生编码
                doctorName,     // 手术医生名称
                operationAssistant,// 手术助手
                operationPlanTime,// 手术计划时间
                operationDuration,// 手术时长
                remark,         // 备注
                status,         // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                submitDate,     // 手术申请提交日期
                operationType,  // 手术类型
                diagnoseCode,   // 术前诊断编码
                diagnoseName,   // 术前诊断名称
                anesthesiologist,// 麻醉医生
                anesthesiologistAssistant,// 麻醉助手
                tourNurse,      // 巡回护士
                instrumentNurse,// 器械护士
                anaestheticCode,// 麻醉方式编码
                anaestheticName,// 麻醉方式名称
                sequence        // 手术台次
                );

            // Act 运行实际测试的代码

            operationApply.Modify(patientId,// 患者唯一标识(HIS)
                patientName,    // 患者姓名
                sex,            // 患者性别
                age,            // 年龄
                iDNo,           // 身份证号
                birthDay,       // 生日
                applyNum,       // 申请单号
                applicantId,    // 申请人Id
                applicantName,  // 申请人名称
                applyTime,      // 申请时间
                bloodType,      // 血型
                rHCode,         // RH阴性阳性
                height,         // 身高
                weight,         // 体重
                proposedOperationCode,// 拟施手术编码
                proposedOperationName,// 拟施手术名称
                operationLevel, // 手术等级
                applyDeptCode,  // 申请科室编码
                applyDeptName,  // 申请科室名称
                operationLocation,// 手术位置
                doctorCode,     // 手术医生编码
                doctorName,     // 手术医生名称
                operationAssistant,// 手术助手
                operationPlanTime,// 手术计划时间
                operationDuration,// 手术时长
                remark,         // 备注
                status,         // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                submitDate,     // 手术申请提交日期
                operationType,  // 手术类型
                diagnoseCode,   // 术前诊断编码
                diagnoseName,   // 术前诊断名称
                anesthesiologist,// 麻醉医生
                anesthesiologistAssistant,// 麻醉助手
                tourNurse,      // 巡回护士
                instrumentNurse,// 器械护士
                anaestheticCode,// 麻醉方式编码
                anaestheticName,// 麻醉方式名称
                sequence        // 手术台次
                );

            // Assert 断言，检验结果

            operationApply.PIId.ShouldNotBeNull();
            */            
        }
    }
}
