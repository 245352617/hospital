using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Patient.Application.Service.HospitalApplyRecord.PKU.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Users;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 住院申请API
    /// </summary>
    [Authorize]
    public class HospitalApplyRecordAppService : EcisPatientAppService, IHospitalApplyRecordAppService
    {
        private readonly IFreeSql _freeSql;
        private readonly ITransferRecordAppService _transferRecordAppService;
        private readonly ILogger<HospitalApplyRecordAppService> _log;
        private readonly IHospitalApi _hisApi;
        private ICapPublisher _capPublisher;

        /// <summary>
        /// 住院申请Api
        /// </summary>
        /// <param name="freeSql"></param>
        /// <param name="transferRecordAppService"></param>
        /// <param name="hospitalClientAppService"></param>
        public HospitalApplyRecordAppService(IFreeSql freeSql
            , ITransferRecordAppService transferRecordAppService
            , ILogger<HospitalApplyRecordAppService> log
            , IHospitalApi hisApi
            , ICapPublisher capPublisher)
        {
            _freeSql = freeSql;
            _transferRecordAppService = transferRecordAppService;
            _log = log;
            _hisApi = hisApi;
            _capPublisher = capPublisher;

        }

        /// <summary>
        /// 根据主键获取病人转住院申请记录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<HospitalApplyRecordDto>>> GetHospitalApplyRecordAsync(
            HospitalApplyRecordWhereInput input, CancellationToken cancellationToken)
        {
            List<HospitalApplyRecord> recordList = await _freeSql.Select<HospitalApplyRecord>()
                .WhereIf(input.PI_ID != Guid.Empty, w => w.PI_ID == input.PI_ID)
                .WhereIf(!string.IsNullOrEmpty(input.PatientID), w => w.PatientID == input.PatientID)
                .WhereIf(!input.VisitNo.IsNullOrWhiteSpace(), w => w.VisitNo == input.VisitNo)
                .OrderByDescending(o => o.Id)
                .ToListAsync(cancellationToken: cancellationToken);
            List<HospitalApplyRecordDto> result = recordList.BuildAdapter().AdaptToType<List<HospitalApplyRecordDto>>();

            return RespUtil.Ok(data: result);
        }

        /// <summary>
        /// 保存转住院申请
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> SaveHospitalApplyRecordAsync(CreateHospitalApplyRecordDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                string userCode = CurrentUser.UserName;
                string userName = CurrentUser.FindClaimValue("fullName");
                if (string.IsNullOrEmpty(dto.DoctorCode))
                {
                    dto.DoctorCode = userCode;
                }

                AdmissionRecord admissionRecord = _freeSql.Select<AdmissionRecord>().Where(w => w.PI_ID == dto.PI_ID).First();
                //主要诊断信息
                DiagnoseRecord diagnoseRecord = _freeSql.Select<DiagnoseRecord>().Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && x.DiagnoseType == "主要诊断" && x.PI_ID == dto.PI_ID && !x.IsDeleted).First();

                bool hasGreenApplay = _freeSql.Select<HospitalApplyRecord>().Any(x => x.PI_ID == admissionRecord.PI_ID && x.Status == HospitalApplyRecordStatus.已申请 && x.IsGreen == true);
                if (hasGreenApplay)
                {
                    throw new Exception("该患者已经登记绿通入院，不允许再次修改住院申请");
                }

                HospitalApplyRecord hospitalApplyRecord = dto.To<HospitalApplyRecord>();
                hospitalApplyRecord.ApplyTime = DateTime.Now;
                hospitalApplyRecord.ApplyCode = userCode;
                hospitalApplyRecord.ApplyName = userName;
                if (admissionRecord.IsOpenGreenChannl)
                {
                    hospitalApplyRecord.IsGreen = true;
                }

                //作废其他住院申请
                _freeSql.Update<HospitalApplyRecord>().Set(s => s.Status == HospitalApplyRecordStatus.作废)
                    .Where(w => w.PI_ID == dto.PI_ID && w.Status == HospitalApplyRecordStatus.已申请)
                    .ExecuteAffrows();

                _freeSql.Insert(hospitalApplyRecord).ExecuteAffrows();


                HospitalApplyRespDto hospitalApplyRespDto = await _hisApi.SaveInHospital(dto);
                // 绿通患者直接办理入院登记，需要返回住院号（非绿通患者只开入院通知书）
                if (admissionRecord.IsOpenGreenChannl && hospitalApplyRespDto == null)
                {
                    throw new Exception("该患是绿通患者，自动入院登记，HIS需要返回住院号");
                }

                _freeSql.Update<HospitalApplyRecord>()
                    .Set(x => x.HospitalApplyNo, (hospitalApplyRespDto == null ? "-" : hospitalApplyRespDto.ZYH))
                    .Where(w => w.PI_ID == dto.PI_ID && w.Status == HospitalApplyRecordStatus.已申请)
                .ExecuteAffrows();

                TransferRecord transferRecord = new TransferRecord()
                {
                    PI_ID = admissionRecord.PI_ID,
                    PatientID = admissionRecord.PatientID,
                    VisitNo = admissionRecord.VisitNo,
                    TransferTime = DateTime.Now,
                    OperatorCode = userCode,
                    OperatorName = userName,
                    TransferTypeCode = TransferType.ToHospital,
                    TransferType = TransferType.ToHospital.GetDescription(),
                    FromAreaCode = admissionRecord.AreaCode,
                    ToAreaCode = TransferType.ToHospital.ToString(),
                    ToArea = TransferType.ToHospital.GetDescription(),
                    FromDeptCode = admissionRecord.DeptCode,
                    ToDeptCode = hospitalApplyRecord.InpatientDepartmentCode,
                    ToDept = hospitalApplyRecord.InpatientDepartmentName,
                    TransferReasonCode = "ToHospital",
                    TransferReason = "转住院",
                };

                _freeSql.Insert(transferRecord).ExecuteAffrows();

                TimeAxisRecord timeAxisRecord = new TimeAxisRecord()
                {
                    PI_ID = admissionRecord.PI_ID,
                    TimePointCode = TimePoint.ToHospitalTime.ToString(),
                    Time = DateTime.Now,
                }.SetTimePointName();
                _freeSql.Insert(timeAxisRecord).ExecuteAffrows();
                _ = _capPublisher.PublishAsync("patient.visitstatus.changed",
                           new { Id = dto.PI_ID, TransferArea = "转住院" },
                           cancellationToken: cancellationToken);
                try
                {
                    await _hisApi.SaveVisitRecordAsync(admissionRecord, diagnoseRecord, TransferType.ToHospital, userCode, userName);
                }
                catch (Exception) { }

                return await Task.FromResult(RespUtil.Ok<string>());
            }
            catch (Exception e)
            {
                return await Task.FromResult(RespUtil.Error<string>(msg: e.Message));
            }
        }

        /// <summary>
        /// 删除住院申请
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<string>> DeleteHospitalApplyRecordAsync(HospitalApplyRecordWhereInput input, CancellationToken cancellationToken)
        {
            await _freeSql.Delete<HospitalApplyRecord>().Where(w => w.Id == input.Id)
                .ExecuteAffrowsAsync(cancellationToken);
            return RespUtil.Ok<string>();
        }

        public async Task<List<TreatmentInfo>> GetTreatmentInfosAsync()
        {
            return await _hisApi.GetTreatmentInfosAsync();
        }

        /// <summary>
        /// 打印中心入院申请单调用
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ReportDto> GetRecordByPrintAsync(Guid PI_ID)
        {
            var report = new ReportDto();
            var treatmentInfos = await _hisApi.GetTreatmentInfosAsync();
            //获取最新一条转住院申请
            var dto = await _freeSql.Select<HospitalApplyRecord>().Where(x => x.PI_ID == PI_ID)
                .OrderByDescending(x => x.Id).FirstAsync<HospitalApplyRecordDto>();
            if (dto != null)
            {
                dto.TenDaysAdmissionStr = dto.TenDaysAdmission ? "是" : "否";
                dto.StampBase = string.Empty;
                var hospital = new List<HospitalApplyRecordDto> { dto };
                report.HospitalApplyRecord = hospital;

                //直接把名称给到前台进行渲染
                foreach (var item in report.HospitalApplyRecord)
                    item.TreatmentName = treatmentInfos.FirstOrDefault(c => c.XZXH == item.TreatmentNo)?.XZMC;

                var admission = await _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == PI_ID)
                    .FirstAsync<AdmissionRecordDto>();
                var diagnoseName = "";
                var diagnoseList = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.IsDeleted == false && x.DiagnoseClassCode == DiagnoseClass.开立)
                    .WhereIf(PI_ID != Guid.Empty, x => x.PI_ID == PI_ID)
                    .OrderBy(o => o.Sort)
                    .ToListAsync(a => new
                    {
                        a.DiagnoseCode,
                        a.DiagnoseName,
                        a.MedicalType,
                        a.DiagnoseTypeCode,
                        a.Sort,
                        a.Remark
                    });
                foreach (var diagnose in diagnoseList.GroupBy(g => g.MedicalType).ToList())
                {
                    int index = 1;
                    foreach (var d in diagnose)
                    {
                        var diagnoseTypeCode = d.DiagnoseTypeCode == "Suspected" ? "?" : "";
                        var remark = string.IsNullOrEmpty(d.Remark) ? "" : $"({d.Remark})";
                        diagnoseName += index + "." + d.DiagnoseName + remark + diagnoseTypeCode + ",";
                        index++;
                    }
                }
                admission.DiagnoseName = diagnoseName;
                var record = new List<AdmissionRecordDto> { admission };
                report.AdmissionRecords = record;
            }
            return report;
        }
    }
}