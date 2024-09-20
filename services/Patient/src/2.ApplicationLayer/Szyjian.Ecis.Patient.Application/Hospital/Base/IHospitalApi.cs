using Patient.Application.Service.HospitalApplyRecord.PKU.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    ///  医院集成接口 Api
    /// </summary>
    public interface IHospitalApi
    {
        /// <summary>
        /// 保存就诊记录
        /// </summary>
        /// <param name="admission"></param>
        /// <param name="diagnoseRecord"></param>
        /// <param name="destination"></param>
        /// <param name="doctorCode"></param>
        /// <returns></returns>
        Task<string> SaveVisitRecordAsync(AdmissionRecord admission, DiagnoseRecord diagnoseRecord, TransferType destination, string doctorCode, string doctorName);

        /// <summary>
        /// 顺呼
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<AdmissionRecordDto>> TerminalCallingAsync(TerminalCallDto input,
            CancellationToken cancellationToken);

        /// <summary>
        /// 重呼
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<AdmissionRecordDto>> TerminalReCallAsync(TerminalCallDto input,
        CancellationToken cancellationToken);

        /// <summary>
        /// 过号（移除队列）接口
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<AdmissionRecordDto>> OutQueueAsync(OutQueueDto input,
            CancellationToken cancellationToken);

        /// <summary>
        /// 获取历史诊断
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<DiagnoseWithDeptDto>>> GetHistoryDiagnoseListAsync(
            string patientId, Guid pI_ID);

        /// <summary>
        /// 保存诊断
        /// </summary>
        /// <param name="admissionRecordDto"></param>
        /// <param name="deleteRecords"></param>
        /// <param name="addRecords"></param>
        /// <returns></returns>
        Task<bool> SaveRecipeJzAsync(AdmissionRecord admissionRecordDto, IEnumerable<DiagnoseRecord> deleteRecords, IEnumerable<DiagnoseRecord> addRecords, string doctorCode);

        /// <summary>
        /// 保存入院通知
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<HospitalApplyRespDto> SaveInHospital(CreateHospitalApplyRecordDto dto);

        /// <summary>
        /// 同步his诊断
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <returns></returns>
        Task SyncHisHistoryDiagnoseListAsync(AdmissionRecord admissionRecord);

        /// <summary>
        /// 诊断、就诊记录、医嘱状态变更
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> ModifyRecordStatusAsync(AdmissionRecord admissionRecord);

        /// <summary>
        /// 保存绿色通道
        /// </summary>
        /// <param name="admissionRecord"></param>
        /// <param name="isGreenChannl"></param>
        /// <returns></returns>
        Task EmergencyGreenChannel(AdmissionRecord admissionRecord, bool isGreenChannl);

        /// <summary>
        /// 获取诊疗小组信息
        /// </summary>
        /// <returns></returns>
        Task<List<TreatmentInfo>> GetTreatmentInfosAsync();
    }
}
