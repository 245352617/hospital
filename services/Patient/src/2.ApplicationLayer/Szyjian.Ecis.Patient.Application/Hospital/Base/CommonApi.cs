using Patient.Application.Service.HospitalApplyRecord.PKU.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Hospital.Base
{
    /// <summary>
    /// 通用 HIS Api
    /// </summary>
    public class CommonApi : IHospitalApi
    {
        public Task<ResponseResult<IEnumerable<DiagnoseWithDeptDto>>> GetHistoryDiagnoseListAsync(string patientId, Guid pI_ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveRecipeJzAsync(AdmissionRecord admissionRecordDto, IEnumerable<DiagnoseRecord> deleteRecords, IEnumerable<DiagnoseRecord> addRecords, string doctorCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PatientRegistrationAsync(CreateHospitalApplyRecordDto input, AdmissionRecord admissionRecord,
            IEnumerable<DiagnoseRecord> diagnoseRecords)
        {
            throw new NotImplementedException();
        }

        public Task SyncHisHistoryDiagnoseListAsync(AdmissionRecord admissionRecord)
        {
            return Task.CompletedTask;
        }

        public Task<string> SaveVisitRecordAsync(AdmissionRecord admission, DiagnoseRecord diagnoseRecord, TransferType destination, string doctorCode, string doctorName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<AdmissionRecordDto>> TerminalCallingAsync(TerminalCallDto input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<AdmissionRecordDto>> TerminalReCallAsync(TerminalCallDto input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<AdmissionRecordDto>> OutQueueAsync(OutQueueDto input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task EmergencyGreenChannel(AdmissionRecord admissionRecord, bool isGreenChannl)
        {
            return Task.CompletedTask;
        }

        public Task<HospitalApplyRespDto> SaveInHospital(CreateHospitalApplyRecordDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<string>> ModifyRecordStatusAsync(AdmissionRecord admissionRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<TreatmentInfo>> GetTreatmentInfosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
