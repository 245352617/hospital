using Abp.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Volo.Abp.EventBus.Distributed;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描    述:订阅危急值信息
    /// 创 建 人:杨凯
    /// 创建时间:2023/10/10 16:04:19
    /// </summary>
    [AllowAnonymous]
    public class CrisisHandler : EcisPatientAppService, IDistributedEventHandler<CrisisHandlerEto>
    {
        private readonly ILogger<CrisisHandler> _logger;
        private readonly IFreeSql _freeSql;

        public CrisisHandler(ILogger<CrisisHandler> logger
            , IFreeSql freeSql)
        {
            _logger = logger;
            _freeSql = freeSql;
        }

        public Task HandleEventAsync(CrisisHandlerEto eventData)
        {
            if (eventData == null || eventData.DicDatas == null || !eventData.DicDatas.Any())
            {
                return Task.CompletedTask;
            }
            _logger.LogInformation("接收到的危急值数据:{0}", eventData.ToJsonString());
            List<CrisisEto> crisisEtos = eventData.DicDatas;
            IEnumerable<string> medicalRecordNos = crisisEtos.Select(x => x.MedicalRecordNo);
            List<Crisis> oldCrisiss = _freeSql.Select<Crisis>().Where(x => medicalRecordNos.Contains(x.MedicalRecordNo)).ToList();
            IEnumerable<string> exits = oldCrisiss.Select(x => x.MedicalRecordNo);
            medicalRecordNos = medicalRecordNos.Except(exits);

            crisisEtos = crisisEtos.Where(x => medicalRecordNos.Contains(x.MedicalRecordNo)).ToList();
            List<string> visitNo = crisisEtos.Select(x => x.VisitNo).Distinct().ToList();
            List<AdmissionRecord> admissionRecords = _freeSql.Select<AdmissionRecord>().Where(x => visitNo.Contains(x.VisitNo)).OrderByDescending(x => x.RegisterTime).ToList();
            List<Crisis> crisiss = new List<Crisis>();
            DateTime unixStartTime = new DateTime(1970, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc);
            foreach (CrisisEto crisisEto in crisisEtos)
            {
                AdmissionRecord admissionRecord = admissionRecords.FirstOrDefault(x => !string.IsNullOrEmpty(x.VisSerialNo) && x.VisSerialNo == crisisEto.VisSerialNo);
                if (admissionRecord == null)
                {
                    admissionRecord = admissionRecords.FirstOrDefault(x => x.VisitNo == crisisEto.VisitNo);
                }
                if (admissionRecord == null) continue;

                long reportTime = long.Parse(crisisEto.ReporterTime);
                Crisis crisis = new Crisis()
                {
                    Id = Guid.NewGuid(),
                    PI_ID = admissionRecord.PI_ID,
                    MedicalRecordNo = crisisEto.MedicalRecordNo,
                    ApplyDoctorCode = crisisEto.ApplyDoctorCode,
                    ApplyDoctorName = crisisEto.ApplyDoctorName,
                    CrisisName = crisisEto.CrisisName,
                    CrisisValue = crisisEto.CrisisValue,
                    ReporterCode = crisisEto.ReporterCode,
                    ReporterName = crisisEto.ReporterName,
                    ReporterTime = unixStartTime.AddMilliseconds(reportTime),
                    SampleNo = crisisEto.SampleNo,
                    CrisisDetails = crisisEto.CrisisDetails,
                };
                crisiss.Add(crisis);
            }

            if (crisiss.Any())
            {
                var resp = _freeSql.GetRepository<Crisis>();
                resp.Insert(crisiss);
            }

            return Task.CompletedTask;
        }
    }
}
