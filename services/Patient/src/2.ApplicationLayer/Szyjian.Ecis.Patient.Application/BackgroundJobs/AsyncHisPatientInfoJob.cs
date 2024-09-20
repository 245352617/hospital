using FreeSql;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Caching;

namespace Szyjian.Ecis.Patient.BackgroundJob
{
    /// <summary>
    /// 同步his患者信息
    /// </summary>
    public class AsyncHisPatientInfoJob : IBackgroundJob
    {
        private const string HisUser = "his_users";
        private readonly ILogger<AsyncHisPatientInfoJob> _log;
        private readonly IDiagnoseRecordRepository _diagnoseRecordRepository;
        private readonly IHisClientAppService _hisClientAppService;
        private readonly IDistributedCache<List<His_Users>> _cache;
        private readonly IFreeSql _freeSql;
        private readonly IHisUserService _hisUserService;
        private readonly IHisViewService _hisViewService;


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log"></param>
        /// <param name="hisClientAppService"></param>
        /// <param name="cache"></param>
        /// <param name="freeSql"></param>
        /// <param name="hisUserService"></param>
        /// <param name="hisViewService"></param>
        /// <param name="diagnoseRecordRepository"></param>
        public AsyncHisPatientInfoJob(ILogger<AsyncHisPatientInfoJob> log
            , IHisClientAppService hisClientAppService
            , IDistributedCache<List<His_Users>> cache
            , IFreeSql freeSql
            , IHisUserService hisUserService
            , IHisViewService hisViewService
            , IDiagnoseRecordRepository diagnoseRecordRepository)
        {
            _log = log;
            _hisClientAppService = hisClientAppService;
            _cache = cache;
            _freeSql = freeSql;
            _hisUserService = hisUserService;
            _hisViewService = hisViewService;
            _diagnoseRecordRepository = diagnoseRecordRepository;
        }

        /// <summary>
        /// 同步患者诊断信息
        /// </summary>
        public async Task ExecuteAsync()
        {
            try
            {
                DateTime triageTime = DateTime.Now.AddHours(-5);
                List<AdmissionRecord> admissionRecords = await _freeSql.Select<AdmissionRecord>()
                    .Where(x => x.TriageTime > triageTime)
                    .ToListAsync();

                if (admissionRecords == null || !admissionRecords.Any()) return;

                IEnumerable<Guid> piids = admissionRecords.Select(x => x.PI_ID);
                List<DiagnoseRecord> diagnoseRecords = await _freeSql.Select<DiagnoseRecord>().Where(x => piids.Contains(x.PI_ID) && x.DiagnoseClassCode == DiagnoseClass.开立 && !x.IsDeleted).ToListAsync();
                IEnumerable<Guid> existsPiids = diagnoseRecords.Select(x => x.PI_ID).Distinct();

                admissionRecords = admissionRecords.Where(x => !existsPiids.Contains(x.PI_ID)).ToList();
                if (!admissionRecords.Any()) return;

                List<His_Users> hisUsers = await _cache.GetAsync(HisUser);
                if (hisUsers == null || !hisUsers.Any())
                {
                    hisUsers = await _hisUserService.GetHisUsersAsync();
                    await _cache.SetAsync("his_users", hisUsers, RedisPolicyHelper.GetRedisProcily(true, 30));
                }

                foreach (AdmissionRecord item in admissionRecords)
                {
                    if (string.IsNullOrEmpty(item.VisSerialNo))
                    {
                        string strVisitNo = await _hisClientAppService.GetPatientRegisterInfoByIdAsync(item.PatientID, item.RegisterNo);
                        item.VisSerialNo = strVisitNo;
                        _freeSql.Update<AdmissionRecord>()
                            .Where(x => x.AR_ID == item.AR_ID)
                            .Set(x => x.VisSerialNo, strVisitNo)
                            .ExecuteAffrows();
                    }

                    //根据在科患者获取接口中患者诊断
                    List<GetDiagnoseRecordBySocketDto> hisDiagnoseList = await _hisClientAppService.GetPatientDiagnoseByIdAsync(item.PatientID);

                    if (hisDiagnoseList == null || !hisDiagnoseList.Any()) continue;

                    hisDiagnoseList = hisDiagnoseList.Where(x => x.VisitId == item.VisSerialNo).ToList();
                    if (!hisDiagnoseList.Any()) continue;

                    hisDiagnoseList.ForEach(x =>
                    {
                        x.PI_ID = item.PI_ID;
                        x.VisitDate = item.VisitDate;
                    });
                    foreach (GetDiagnoseRecordBySocketDto hisDiagnose in hisDiagnoseList)
                    {
                        His_Users doctorData = hisUsers.FirstOrDefault(u => u.UserName == hisDiagnose.DiagnoseDoctor);
                        hisDiagnose.DiagnoseDoctorName = doctorData?.Name;
                    }

                    List<GetDiagnoseRecordBySocket> diagnoseRecordList = hisDiagnoseList.BuildAdapter().AdaptToType<List<GetDiagnoseRecordBySocket>>();

                    //调用patient删除新增诊断接口
                    await _diagnoseRecordRepository.HandleDiagnoseAsync(diagnoseRecordList);

                }
            }
            catch (Exception e)
            {
                _log.LogError("同步患者诊断信息异常:{Msg}", e);
            }
        }

        /// <summary>
        /// 同步患者入科信息
        /// </summary>
        /// <returns></returns> 
        public async Task SyncPatientBasicInfoAsync()
        {
            try
            {
                List<V_JHJK_HZLB> pushData = await _hisViewService.GetRegisterListAsync();
                IEnumerable<decimal?> registerNos = pushData.Select(x => x.GHXH);
                List<string> registerNoList = new List<string>();
                foreach (decimal? item in registerNos)
                {
                    registerNoList.Add(item?.ToString());
                }
                List<AdmissionRecord> admissionRecords = _freeSql.Select<AdmissionRecord>().Where(x => registerNoList.Contains(x.RegisterNo)).ToList();
                admissionRecords = admissionRecords.Where(x => string.IsNullOrEmpty(x.VisSerialNo)).ToList();
                if (!admissionRecords.Any()) return;

                List<UpdateAdmissionRecordByViewDto> list = new List<UpdateAdmissionRecordByViewDto>();
                foreach (AdmissionRecord admissionRecord in admissionRecords)
                {
                    string strVisitNo = await _hisClientAppService.GetPatientRegisterInfoByIdAsync(admissionRecord.PatientID, admissionRecord.RegisterNo);
                    V_JHJK_HZLB item = pushData.FirstOrDefault(x => x.GHXH.ToString() == admissionRecord.RegisterNo);
                    if (item == null) continue;

                    UpdateAdmissionRecordByViewDto dto = new UpdateAdmissionRecordByViewDto();
                    dto.PatientID = item.PatientID.ToString();
                    dto.PatientName = item.PatientName;
                    dto.VisitDate = item.KSSJ;
                    dto.Bed = item.BRCH;
                    dto.InDeptTime = item.KSSJ;
                    dto.FirstDoctorCode = item.YSDM;
                    dto.FirstDoctorName = item.YGXM;
                    dto.RegisterNo = item.GHXH.ToString();
                    dto.VisSerialNo = strVisitNo;
                    dto.OutDeptTime = item.JSSJ;
                    dto.CallingDoctorName = item.WZJB;
                    dto.OutDeptReasonCode = item.CKQX;

                    list.Add(dto);
                }

                await UpdateAdmissionRecordByViewAsync(admissionRecords, list);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "同步患者入科时间信息异常>>>" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 同步患者入科信息
        /// </summary>
        /// <param name="admissionRecords"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private async Task UpdateAdmissionRecordByViewAsync(List<AdmissionRecord> admissionRecords, List<UpdateAdmissionRecordByViewDto> list)
        {
            foreach (AdmissionRecord admissionRecord in admissionRecords)
            {
                UpdateAdmissionRecordByViewDto dto = list.FirstOrDefault(x => x.PatientID == admissionRecord.PatientID && x.RegisterNo == admissionRecord.RegisterNo);
                if (dto == null) return;

                #region 更新就诊记录

                //视图出科去向字段在表中无合适字段存储，暂存HisDeptCode
                await _freeSql.Update<AdmissionRecord>()
                    .SetIf(!string.IsNullOrWhiteSpace(dto.FirstDoctorCode), a => a.FirstDoctorCode, dto.FirstDoctorCode)
                    .SetIf(!string.IsNullOrWhiteSpace(dto.FirstDoctorName), a => a.FirstDoctorName, dto.FirstDoctorName)
                     .SetIf(dto.VisitDate != null, a => a.VisitDate, dto.VisitDate)
                     .SetIf(!string.IsNullOrWhiteSpace(dto.VisitNo), a => a.VisitNo, dto.VisitNo)
                     .SetIf(!string.IsNullOrWhiteSpace(dto.VisSerialNo), a => a.VisSerialNo, dto.VisSerialNo)
                      .SetIf(!string.IsNullOrWhiteSpace(dto.CallingDoctorName), a => a.CallingDoctorName, dto.CallingDoctorName)
                      .SetIf(dto.OutDeptReason != null, a => a.HisDeptCode, dto.OutDeptReason)

                    .Where(x => x.PatientID == admissionRecord.PatientID)
                    .Where(x => x.RegisterNo == admissionRecord.RegisterNo)
                    .Where(x => x.AreaName == "抢救区" || x.AreaName == "留观区")
                    .ExecuteAffrowsAsync();

                #endregion
            }
        }
    }

    public class RedisPolicyHelper
    {
        /// <summary>
        /// 使用绝对还是滑动过期，不使用策略就默认为长期保存
        /// </summary>
        /// <param name="abHd">true绝对过期; false:滑动过期</param>
        /// <param name="minutes">默认1小时过期</param>
        /// <returns></returns>
        public static DistributedCacheEntryOptions GetRedisProcily(bool abHd, int minutes = 60)
        {
            DistributedCacheEntryOptions policy = new DistributedCacheEntryOptions();
            minutes = minutes < 1 ? 60 : minutes;
            if (abHd)
                policy.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
            else
                policy.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return policy;
        }
    }
}