using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IPatientInfoAppService : IApplicationService
    {
        /// <summary>
        /// 根据输入项获取患者分诊数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PatientInfoDto>> GetPatientInfoByInputAsync(PatientInfoInput input);

        /// <summary>
        /// 根据输入项查询患者已有的档案信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<List<PatientInfoDto>>> GetPatientInfoListByInputAsync(SelectAlreadyTriageDto input);

        /// <summary>
        /// 根据输入项获取患者病历号
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<PatientInfoFromHis>>> GetPatientRecordByHl7MsgAsync(CreateOrGetPatientIdInput input);

        /// <summary>
        /// 根据输入项获取患者病历号列表
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<PatientOutput>>> GetPatientRecordListAsync(CreateOrGetPatientIdInput input);

        /// <summary>
        /// 根据输入项创建患者病历号（建档）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PatientInfoFromHis>> CreatePatientRecordByHl7MsgAsync(CreateOrGetPatientIdInput input);

        /// <summary>
        /// 修改绿色通道
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdatePatientGreenRoadAsync(UpdatePatientGreenRoadDto dto);

        /// <summary>
        /// 修改发病时间
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> ModifyOnsetTimeAsync(ModifyOnsetTimeDto dto);

        /// <summary>
        /// 修改绿色通道
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> OpenGreenRoadAsync(UpdatePatientGreenRoadDto dto);

        /// <summary>
        /// 分诊确认保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult<PatientInfoDto>> SaveTriageRecordAsync(CreateOrUpdatePatientDto dto);

        /// <summary>
        /// 根据输入项获取患者建档分页信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<PatientInfoDto>>> GetPatientInfoListAsync(PatientInfoWhereInput input);

        /// <summary>
        /// 根据配置项获取患者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<PatientInfoExportExcelDto>>> GetPatientInfoBySettingListAsync(
            PatientInfoWhereInput input);

        /// <summary>
        /// 根据输入项获取患者状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<List<TriagePatientInfoDto>>> GetPatientStatusAsync(PatientStatusInput input);

        /// <summary>
        /// 六大中心获取生命体征信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<JsonResult<VitalSignInfoToSixCenterDto>> GetVitalSignInfoToSixCenterAsync(Guid pid);

        /// <summary>
        /// 急诊根据id获取患者信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<JsonResult<PatientInfoDto>> GetPatientInfoByEcisAsync(Guid pid);

        /// <summary>
        /// 院前分诊获取生命体征
        /// </summary>
        /// <param name="patientInfoId">患者Id</param>
        /// <returns></returns>
        Task<JsonResult<IotDataDto>> GetVitalSignFromIotServerAsync(Guid patientInfoId);


        /// <summary>
        /// 查询患者评分
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<IEnumerable<ScoreDto>>> GetScoreAsync(Guid triagePatientInfoId);

        /// <summary>
        /// 接收院前分诊患者
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult> SyncPatientFromPreHospitalAsync(PatientInfoMqDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// 暂停/开启 叫号
        /// 目前只有北大要求
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="isSuspend">是否暂停（0：暂停，1：开启）</param>
        /// <returns></returns>
        Task<JsonResult> SuspendAsync(Guid pid, bool isSuspend);

        /// <summary>
        /// 通过证件类型查找新冠问卷
        /// </summary>
        /// <param name="idTypeCode">证件类型编码</param>
        /// <param name="idCardNo">证件号码</param>
        /// <param name="barcode">二维码内容</param>
        /// <returns></returns>
        Task<JsonResult<object>> GetQuestionnaireAsync(string idTypeCode, string idCardNo, string barcode);

        /// <summary>
        /// 获取患者信息修改记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<PatientInfoChangeDto>>> GetChangeRecord(PatientInfoChangeInput dto);
        /// <summary>
        /// 根据输入项获取患者告知分页信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<PatientInformExportExcelDto>>> GetPatientInformListByInputAsync(PatientInformQueryDto input);

        /// <summary>
        /// 根据条件获取患者告知信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<PatientInformExportExcelDto>>> GetPatientInformListAsync(PatientInformQueryDto input);

        /// <summary>
        /// 已分诊患者，返回到未分诊队列
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> ReturnToNoTriage(Guid id);
    }
}