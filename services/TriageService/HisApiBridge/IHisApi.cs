using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.TriageService.LGHis;
using SamJan.MicroService.TriageService.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// HIS Api 抽象
    /// </summary>
    public interface IHisApi
    {
        /// <summary>
        /// 保存分诊前
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="patient"></param>
        /// <param name="isUpdated"></param>
        /// <param name="hasChangedDoctor"></param>
        /// <returns></returns>
        virtual public Task<PatientInfo> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo patient,
            bool isUpdated, bool hasChangedDoctor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存分诊前
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dtoPatient"></param>
        /// <param name="currentDBPatient"></param>
        /// <param name="isNeedNewCallSn"></param>
        /// <param name="hasChangedDoctor"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        virtual Task<CommonHttpResult<PatientInfo>> BeforeSaveTriageRecordAsync(CreateOrUpdatePatientDto dto, PatientInfo dtoPatient, PatientInfo currentDBPatient, bool isNeedNewCallSn, bool hasChangedDoctor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 挂号/预约/分诊
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="doctorId">医生代码</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="isModify">是否修改分诊信息（false：新增；true：修改）</param>
        /// <param name="hasChangedDoctor">修改预约科室、医生</param>
        /// <param name="isFirstTimePush">是否第一次分诊</param>
        /// <returns></returns>
        public Task<PatientInfo> RegisterPatientAsync(PatientInfo patientInfo, string doctorId, string doctorName,
            bool isModify, bool hasChangedDoctor, bool isFirstTimePush = true);

        /// <summary>
        /// 同步挂号列表
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> SyncRegisterPatientFromHis();

        /// <summary>
        /// 获取医生列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        Task<JsonResult<List<DoctorSchedule>>> GetDoctorScheduleAsync(string deptCode, DateTime? regDate);

        /// <summary>
        /// 获取护士列表
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<EmployeeDto>>> GetNurseScheduleAsync();

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="cardType">证件类别</param>
        /// <param name="identityNo">身份证号码</param>
        /// <param name="visitNo">就诊号</param>
        /// <param name="patientName">姓名</param>
        /// <param name="phone">电话号码</param>
        /// <param name="registerNo">挂号流水号</param>
        /// <returns></returns>
        Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordAsync(string cardType, string identityNo,
            string visitNo, string patientName, string phone = "", string registerNo = "");

        /// <summary>
        /// 患者建档
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordAsync(CreateOrGetPatientIdInput input);

        /// <summary>
        /// 三无人员建档
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PatientInfoFromHis>> CreateNoThreePatientRecordAsync(CreateOrGetPatientIdInput input);

        /// <summary>
        /// 修改患者档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> RevisePerson(PatientModifyDto input);

        /// <summary>
        /// 建档前验证
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isInfant"></param>
        /// <returns></returns>
        Task<JsonResult> ValidateBeforeCreatePatient(CreateOrGetPatientIdInput input, out bool isInfant);

        /// <summary>
        /// 取消挂号信息
        /// </summary>
        /// <param name="regSerialNo">挂号流水号</param>
        /// <returns></returns>
        Task<JsonResult> CancelRegisterInfoAsync(string regSerialNo);

        /// <summary>
        /// 获取生命体征信息
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<VitalSignsInfoByJinWan>> GetHisVitalSignsAsync(string serialNumber);

        /// <summary>
        /// 获取base64签名
        /// </summary>
        /// <param name="empCode">工号</param>
        /// <returns></returns>
        Task<JsonResult<string>> GetStampBase64Async(string empCode);

        /// <summary>
        /// 暂停/恢复叫号（挂起状态，医生站不能呼叫、接诊患者）
        /// </summary>
        /// <param name="patientId">患者Id</param>
        /// <param name="isSuspend">是否暂停（0：暂停，1：开启）</param>
        /// <returns></returns>
        Task<JsonResult> SuspendCalling(string patientId, bool isSuspend);

        /// <summary>
        /// 门诊患者信息查询
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<PatientInfoFromHis> GetPatienInfoBytIdAsync(string patientId);

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <param name="hisRegisterInfoQueryDto"></param>
        /// <returns></returns>
        Task<List<PatientRespDto>> GetRegisterInfoAsync(HisRegisterInfoQueryDto hisRegisterInfoQueryDto);

        /// <summary>
        /// 查询挂号列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<List<RegisterInfoHisDto>>> GetRegisterInfoListAsync(RegisterInfoInput input);
        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="visitNum"></param>
        /// <returns></returns>
        Task<HisResponseDto> payCurRegisterAsync(string visitNum);


        /// <summary>
        /// 获取医保信息
        /// </summary>
        /// <param name="electronCertNo"></param>
        /// <param name="extraCode"></param>
        /// <returns></returns>
        Task<InsuranceDto> GetInsuranceInfoByElectronCert(string electronCertNo, string extraCode);

        /// <summary>
        /// 获取挂号患者列表
        /// 挂号超过24小时的不会被查询到结果中
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(GetRegisterPagedListInput input);

        /// <summary>
        /// 获取挂号患者列表，排序结果由叫号系统提供
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        virtual Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListV2Async(GetRegisterPagedListInput input)
        {
            throw new NotImplementedException();
        }

        virtual Task<JsonResult> SyncRegisterPatientFromHisV2()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 已分诊患者，返回到未分诊队列
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> ReturnToNoTriage(Guid id);

        /// <summary>
        /// 优先
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        virtual Task<bool> MoveToTop(string registerNo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取消优先
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        virtual Task<bool> CancelMoveToTop(string registerNo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 暂停/恢复叫号
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="isSuspend"></param>
        /// <returns></returns>
        virtual Task SuspendCallingV2(string patientId, bool isSuspend)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 过号重排
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        virtual Task<bool> ReturnToQueue(string registerNo)
        {
            throw new NotImplementedException();
        }
    }
}