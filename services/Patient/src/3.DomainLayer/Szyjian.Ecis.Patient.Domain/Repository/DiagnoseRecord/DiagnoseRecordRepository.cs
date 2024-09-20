using FreeSql;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.DependencyInjection;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 快速诊断仓储
    /// </summary>
    public class DiagnoseRecordRepository : IDiagnoseRecordRepository, ITransientDependency
    {
        private readonly ILogger<DiagnoseRecordRepository> _log;
        private readonly IFreeSql _freeSql;
        private UnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log"></param>
        /// <param name="freeSql"></param>
        public DiagnoseRecordRepository(ILogger<DiagnoseRecordRepository> log, IFreeSql freeSql)
        {
            _log = log;
            _freeSql = freeSql;
            _unitOfWorkManager = new UnitOfWorkManager(_freeSql);
        }

        /// <summary>
        /// 批量插入快速诊断
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<bool> AddRangeAsync(IEnumerable<DiagnoseRecord> entities)
        {
            try
            {
                var diagnoseRecords = entities as DiagnoseRecord[] ?? entities.ToArray();
                if (diagnoseRecords.Length <= 0)
                {
                    _log.LogError("Add diagnoseRecord list error.Msg:没有可保存的数据");
                    return false;
                }

                var rows = await _freeSql.Insert(diagnoseRecords).ExecuteAffrowsAsync();
                if (rows == diagnoseRecords.Length)
                {
                    _log.LogInformation("Add diagnoseRecord list success");
                    return true;
                }

                _log.LogError("Add diagnoseRecord list error.Msg:保存的数量与提交的数量不对应");
                return false;
            }
            catch (Exception e)
            {
                _log.LogError("Add diagnoseRecord list error.Msg:{Msg}", e);
                return false;
            }
        }

        /// <summary>
        /// 删除再新增患者诊断
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task HandleDiagnoseAsync(List<GetDiagnoseRecordBySocket> list)
        {
            if (list == null || !list.Any()) return;

            using IUnitOfWork unitOfWork = _unitOfWorkManager.Begin();
            try
            {
                List<DiagnoseRecord> diagnoseList = new List<DiagnoseRecord>();
                foreach (GetDiagnoseRecordBySocket patDiag in list)
                {
                    DiagnoseRecord diagRecord = new DiagnoseRecord();
                    diagRecord.PI_ID = patDiag.PI_ID;
                    diagRecord.PatientID = patDiag.BrId;
                    diagRecord.VisitNo = patDiag.VisitId;
                    diagRecord.VisitDate = patDiag.VisitDate;
                    diagRecord.DiagnoseClassCode = DiagnoseClass.开立;
                    diagRecord.DiagnoseClass = "开立";
                    diagRecord.DiagnoseCode = patDiag.DiagnoseCode;
                    diagRecord.DiagnoseName = patDiag.DiagnoseName;
                    diagRecord.Icd10 = patDiag.AdmissionSituation;
                    diagRecord.DoctorCode = patDiag.DiagnoseDoctor;
                    diagRecord.DoctorName = patDiag.DiagnoseDoctorName;
                    diagRecord.DiagnoseTypeCode = "Commonly";
                    diagRecord.DiagnoseType = "一般诊断";
                    diagRecord.Remark = patDiag.DiagnoseDescribed?.Trim();
                    DateTime dateNow = DateTime.Now;
                    DateTime.TryParse(patDiag.DiagnoseTime, out dateNow);
                    diagRecord.CreationTime = dateNow;
                    diagRecord.AddUserCode = patDiag.DiagnoseDoctor;
                    diagRecord.AddUserName = patDiag.DiagnoseDoctorName;

                    diagRecord.MedicalType = patDiag.CWType == "2" ? MedicalTypeEnum.ChineseMedicine : MedicalTypeEnum.WesternMedicine;

                    diagnoseList.Add(diagRecord);
                }

                Guid pi_Id = list.First().PI_ID;
                List<DiagnoseRecord> oldDiagnoseRecords = _freeSql.Select<DiagnoseRecord>().Where(x => x.PI_ID == pi_Id && x.DiagnoseClassCode == DiagnoseClass.开立).ToList();

                IEnumerable<DiagnoseRecord> addDiagnoseList = diagnoseList.Except(oldDiagnoseRecords, new DiagnoseRecord());

                IEnumerable<DiagnoseRecord> deleteDiagnoseList = oldDiagnoseRecords.Except(diagnoseList, new DiagnoseRecord());

                int addRows = 0;
                if (addDiagnoseList.Any())
                {
                    _log.LogInformation("同步增加的诊断：" + addDiagnoseList.ToJson());
                    addRows = await _freeSql.Insert(addDiagnoseList).ExecuteAffrowsAsync();
                }

                if (deleteDiagnoseList.Any())
                {
                    IEnumerable<int> ids = deleteDiagnoseList.Select(x => x.PD_ID);
                    await _freeSql.Delete<DiagnoseRecord>().Where(x => ids.Contains(x.PD_ID)).ExecuteAffrowsAsync();
                }
                unitOfWork.Commit();
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();
                _log.LogError("DeleteAddDiagnose error.ErrorMsg:{Msg}", e);
            }
        }
    }
}