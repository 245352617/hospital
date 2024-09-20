namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Domain.Services;
    using Volo.Abp.Guids;
    using JetBrains.Annotations;

    /// <summary>
    /// 护士交班 领域服务
    /// </summary>
    public class NurseHandoverManager : DomainService
    {   
        private readonly IGuidGenerator _guidGenerator;
        private readonly INurseHandoverRepository _nurseHandoverRepository;

        #region constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nurseHandoverRepository"></param>
        /// <param name="guidGenerator"></param>
        public NurseHandoverManager(INurseHandoverRepository nurseHandoverRepository, IGuidGenerator guidGenerator)
        {
            _nurseHandoverRepository = nurseHandoverRepository;
            _guidGenerator = guidGenerator;
        }        
        #endregion

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pIID">triage分诊患者id</param>
        /// <param name="patientId">患者id</param>
        /// <param name="visitNo">就诊号</param>
        /// <param name="patientName">患者姓名</param>
        /// <param name="sex">性别编码</param>
        /// <param name="sexName">性别名称</param>
        /// <param name="age">年龄</param>
        /// <param name="triageLevel">分诊级别</param>
        /// <param name="triageLevelName">分诊级别名称</param>
        /// <param name="areaCode">区域编码</param>
        /// <param name="areaName">区域名称</param>
        /// <param name="inDeptTime">入科时间</param>
        /// <param name="diagnoseName">诊断</param>
        /// <param name="bed">床号</param>
        /// <param name="isNoThree">是否三无</param>
        /// <param name="criticallyIll">是否病危</param>
        /// <param name="content">交班内容</param>
        /// <param name="test">检验</param>
        /// <param name="inspect">检查</param>
        /// <param name="emr">电子病历</param>
        /// <param name="inOutVolume">出入量</param>
        /// <param name="vitalSigns">生命体征</param>
        /// <param name="medicine">药物</param>
        /// <param name="latestStatus">最新现状</param>
        /// <param name="background">背景</param>
        /// <param name="assessment">评估</param>
        /// <param name="proposal">建议</param>
        /// <param name="devices">设备</param>
        /// <param name="handoverTime">交班时间</param>
        /// <param name="handoverNurseCode">交班护士编码</param>
        /// <param name="handoverNurseName">交班护士名称</param>
        /// <param name="successionNurseCode">接班护士编码</param>
        /// <param name="successionNurseName">接班护士名称</param>
        /// <param name="handoverDate">交班日期</param>
        /// <param name="shiftSettingId">班次id</param>
        /// <param name="shiftSettingName">班次名称</param>
        /// <param name="status">交班状态，0：未提交，1：提交交班</param>
        /// <param name="creationCode">创建人编码</param>
        /// <param name="creationName">创建人名称</param>
        /// <param name="totalPatient">查询的全部患者</param>
        /// <param name="dutyNurseName"></param>
        public async Task<NurseHandover> CreateAsync(Guid pIID,        // triage分诊患者id
            string patientId,             // 患者id
            int? visitNo,                 // 就诊号
            string patientName,           // 患者姓名
            string sex,                   // 性别编码
            string sexName,               // 性别名称
            string age,                   // 年龄
            string triageLevel,           // 分诊级别
            string triageLevelName,       // 分诊级别名称
            string areaCode,              // 区域编码
            string areaName,              // 区域名称
            DateTime inDeptTime,          // 入科时间
            string diagnoseName,          // 诊断
            string bed,                   // 床号
            bool isNoThree,               // 是否三无
            bool criticallyIll,           // 是否病危
            string content,               // 交班内容
            string test,                  // 检验
            string inspect,               // 检查
            string emr,                   // 电子病历
            string inOutVolume,           // 出入量
            string vitalSigns,            // 生命体征
            string medicine,              // 药物
            string latestStatus,          // 最新现状
            string background,            // 背景
            string assessment,            // 评估
            string proposal,              // 建议
            string devices,               // 设备
            DateTime handoverTime,        // 交班时间
            [NotNull] string handoverNurseCode,// 交班护士编码
            [NotNull] string handoverNurseName,// 交班护士名称
            [NotNull] string successionNurseCode,// 接班护士编码
            [NotNull] string successionNurseName,// 接班护士名称
            [NotNull] DateTime handoverDate,// 交班日期
            [NotNull] Guid shiftSettingId,// 班次id
            [NotNull] string shiftSettingName,// 班次名称
            int status,                   // 交班状态，0：未提交，1：提交交班
            [NotNull] string creationCode,// 创建人编码
            [NotNull] string creationName,// 创建人名称
            int totalPatient,              // 查询的全部患者
            string dutyNurseName
            ) 
        {
           var nurseHandover = new NurseHandover(_guidGenerator.Create(), 
                pIID,   // triage分诊患者id
                patientId,      // 患者id
                visitNo,        // 就诊号
                patientName,    // 患者姓名
                sex,            // 性别编码
                sexName,        // 性别名称
                age,            // 年龄
                triageLevel,    // 分诊级别
                triageLevelName,// 分诊级别名称
                areaCode,       // 区域编码
                areaName,       // 区域名称
                inDeptTime,     // 入科时间
                diagnoseName,   // 诊断
                bed,            // 床号
                isNoThree,      // 是否三无
                criticallyIll,  // 是否病危
                content,        // 交班内容
                test,           // 检验
                inspect,        // 检查
                emr,            // 电子病历
                inOutVolume,    // 出入量
                vitalSigns,     // 生命体征
                medicine,       // 药物
                latestStatus,   // 最新现状
                background,     // 背景
                assessment,     // 评估
                proposal,       // 建议
                devices,        // 设备
                handoverTime,   // 交班时间
                handoverNurseCode,// 交班护士编码
                handoverNurseName,// 交班护士名称
                successionNurseCode,// 接班护士编码
                successionNurseName,// 接班护士名称
                handoverDate,   // 交班日期
                shiftSettingId, // 班次id
                shiftSettingName,// 班次名称
                status,         // 交班状态，0：未提交，1：提交交班
                creationCode,   // 创建人编码
                creationName,   // 创建人名称
                totalPatient,dutyNurseName    // 查询的全部患者
                );

            return await _nurseHandoverRepository.InsertAsync(nurseHandover);
        }
        #endregion Create

    }
}
