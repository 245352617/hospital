using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 入科记录 API
    /// </summary>
    public interface IAdmissionRecordAppService : IApplicationService
    {
        /// <summary>
        /// 更新入科记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>主键自增Id</returns>
        Task<ResponseResult<int>> UpdateAdmissionRecordAsync(UpdateAdmissionRecordDto dto);

        /// <summary>
        /// 急诊诊疗患者列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default);


        /// <summary>
        /// 急诊诊疗患者列表改版
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionRecordPaged2Async(
            AdmissionRecordInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 历史诊疗患者列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetHistoryAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 待结果患者列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionWaitRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 我的患者列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetMyAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 我的关注列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetMyFollowAdmissionRecordPagedAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default);


        /// <summary>
        /// 通过入科流水号获取用户入科后所在区域信息
        /// </summary>
        /// <param name="pI_ID">PIID 分诊库患者基本信息表主键ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<AreaDto>> GetPatientAreaByPiidAsync(Guid pI_ID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 通过Id获取指定用户入科信息
        /// </summary>
        /// <param name="aR_ID">自增Id</param>
        /// <param name="pI_ID">PVID 分诊库患者基本信息表主键ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<AdmissionRecordDto>> GetAdmissionRecordByIdAsync(int aR_ID, Guid pI_ID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 患者出科
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> PatientOutDeptAsync(UpdateAdmissionByOutDeptDto dto,
            CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> PatientWaitingAsync(int id,
            CancellationToken cancellationToken);

        /// <summary>
        /// 结束就诊
        /// </summary>
        /// <param name="aR_ID">入科记录表id</param>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> EndVisitAsync(int aR_ID, EndVisitDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> UpdatePatientInfoAsync(UpdatePatientInfoDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 患者开始治疗或入科
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> PatientStartVisitAsync(PatientStartVisitDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 召回就诊
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<AdmissionRecordDto>>
            ReCallVisitAsync(ReCallVisitDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// 同步病患接口
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SyncPatientAsync(TriagePatientInfosMqDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 同步病患排序接口
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SyncPatientSortAsync(ReSortPatientListDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 同步病患叫号状态
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SyncPatientCallStatusAsync(SyncPatientCallStatusDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取Admission记录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<PagedResultDto<AdmissionRecordDto>>> GetAdmissionRecordAsync(
            AdmissionRecordInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 列表打印获取数据信息
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        Task<ResponseResult<Dictionary<string, object>>> GetPrintPatientAsync(Guid pI_ID);

        /// <summary>
        /// 保存代办人信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SaveAgentInfoAsync(AgentInfoCreateDto dto);

        // /// <summary>
        // /// 检查患者信息是否存在
        // /// </summary>
        // /// <param name="jobTime">作业发起时间</param> 
        // public Task CheckPatientExistAsync(string jobTime);

        /// <summary>
        /// 由视图更新入科记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns>主键自增Id</returns>
        Task<ResponseResult<int>> UpdateAdmissionRecordByViewAsync(List<UpdateAdmissionRecordByViewDto> list);

        /// <summary>
        /// 查询当前抢救区和留观区正在就诊的患者
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<List<GetVisitAdmissionRecordDto>>> GetVisitAdmissionRecordAsync();

        /// <summary>
        /// 根据PIID获取患者信息（不用token 不给前端用 其他内部服务调用）
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        Task<ResponseResult<AdmissionRecordDto>> GetPatientInfoByIdAsync(Guid pI_ID);
    }
}