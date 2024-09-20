using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;


namespace YiJian.Recipe
{
    /// <summary>
    /// 分诊患者id 领域服务
    /// </summary>
    public class OperationApplyManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IOperationApplyRepository _operationApplyRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operationApplyRepository"></param>
        /// <param name="guidGenerator"></param>
        public OperationApplyManager(IOperationApplyRepository operationApplyRepository, IGuidGenerator guidGenerator)
        {
            _operationApplyRepository = operationApplyRepository;
            _guidGenerator = guidGenerator;
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pIId">分诊患者id</param>
        /// <param name="patientId">患者唯一标识(HIS)</param>
        /// <param name="patientName">患者姓名</param>
        /// <param name="sex">患者性别</param>
        /// <param name="sexName"></param>
        /// <param name="age">年龄</param>
        /// <param name="iDNo">身份证号</param>
        /// <param name="birthDay">生日</param>
        /// <param name="applyNum">申请单号</param>
        /// <param name="applicantId">申请人Id</param>
        /// <param name="applicantName">申请人名称</param>
        /// <param name="applyTime">申请时间</param>
        /// <param name="bloodType">血型</param>
        /// <param name="rHCode">RH阴性阳性</param>
        /// <param name="height">身高</param>
        /// <param name="weight">体重</param>
        /// <param name="proposedOperationCode">拟施手术编码</param>
        /// <param name="proposedOperationName">拟施手术名称</param>
        /// <param name="operationLevel">手术等级</param>
        /// <param name="applyDeptCode">申请科室编码</param>
        /// <param name="applyDeptName">申请科室名称</param>
        /// <param name="operationLocation">手术位置</param>
        /// <param name="doctorCode">手术医生编码</param>
        /// <param name="doctorName">手术医生名称</param>
        /// <param name="operationAssistant">手术助手</param>
        /// <param name="operationPlanTime">手术计划时间</param>
        /// <param name="operationDuration">手术时长</param>
        /// <param name="remark">备注</param>
        /// <param name="status">手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回</param>
        /// <param name="submitDate">手术申请提交日期</param>
        /// <param name="operationType">手术类型</param>
        /// <param name="diagnoseCode">术前诊断编码</param>
        /// <param name="diagnoseName">术前诊断名称</param>
        /// <param name="anesthesiologist">麻醉医生</param>
        /// <param name="anesthesiologistAssistant">麻醉助手</param>
        /// <param name="tourNurse">巡回护士</param>
        /// <param name="instrumentNurse">器械护士</param>
        /// <param name="anaestheticCode">麻醉方式编码</param>
        /// <param name="anaestheticName">麻醉方式名称</param>
        /// <param name="sequence">手术台次</param>
        public async Task<OperationApply> CreateAsync(Guid id, Guid pIId, // 分诊患者id
            string patientId, // 患者唯一标识(HIS)
            string patientName, // 患者姓名
            string sex, // 患者性别
            string sexName, // 性别
            string age, // 年龄
            string iDNo, // 身份证号
            DateTime? birthDay, // 生日
            string applyNum, // 申请单号
            string applicantId, // 申请人Id
            string applicantName, // 申请人名称
            DateTime? applyTime, // 申请时间
            string bloodType, // 血型
            string rHCode, // RH阴性阳性
            decimal height, // 身高
            decimal weight, // 体重
            string proposedOperationCode, // 拟施手术编码
            string proposedOperationName, // 拟施手术名称
            string operationLevel, // 手术等级
            string applyDeptCode, // 申请科室编码
            string applyDeptName, // 申请科室名称
            string operationLocation, // 手术位置
            string doctorCode, // 手术医生编码
            string doctorName, // 手术医生名称
            string operationAssistant, // 手术助手
            DateTime? operationPlanTime, // 手术计划时间
            string operationDuration, // 手术时长
            string remark, // 备注
            OperationApplyStatus status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
            DateTime? submitDate, // 手术申请提交日期
            string operationType, // 手术类型
            string diagnoseCode, // 术前诊断编码
            string diagnoseName, // 术前诊断名称
            string anesthesiologist, // 麻醉医生
            string anesthesiologistAssistant, // 麻醉助手
            string tourNurse, // 巡回护士
            string instrumentNurse, // 器械护士
            string anaestheticCode, // 麻醉方式编码
            string anaestheticName, // 麻醉方式名称
            int sequence // 手术台次
        )
        {
            var operationApply = new OperationApply(id,
                pIId, // 分诊患者id
                patientId, // 患者唯一标识(HIS)
                patientName, // 患者姓名
                sex, // 患者性别
                sexName, // 患者性别

                age, // 年龄
                iDNo, // 身份证号
                birthDay, // 生日
                applyNum, // 申请单号
                applicantId, // 申请人Id
                applicantName, // 申请人名称
                applyTime, // 申请时间
                bloodType, // 血型
                rHCode, // RH阴性阳性
                height, // 身高
                weight, // 体重
                proposedOperationCode, // 拟施手术编码
                proposedOperationName, // 拟施手术名称
                operationLevel, // 手术等级
                applyDeptCode, // 申请科室编码
                applyDeptName, // 申请科室名称
                operationLocation, // 手术位置
                doctorCode, // 手术医生编码
                doctorName, // 手术医生名称
                operationAssistant, // 手术助手
                operationPlanTime, // 手术计划时间
                operationDuration, // 手术时长
                remark, // 备注
                status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                submitDate, // 手术申请提交日期
                operationType, // 手术类型
                diagnoseCode, // 术前诊断编码
                diagnoseName, // 术前诊断名称
                anesthesiologist, // 麻醉医生
                anesthesiologistAssistant, // 麻醉助手
                tourNurse, // 巡回护士
                instrumentNurse, // 器械护士
                anaestheticCode, // 麻醉方式编码
                anaestheticName, // 麻醉方式名称
                sequence // 手术台次
            );

            return await _operationApplyRepository.InsertAsync(operationApply);
        }

        #endregion Create
    }
}