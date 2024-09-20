using DotNetCore.CAP;
using FreeSql;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
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
    /// 诊断记录API
    /// </summary>
    [Authorize]
    public class DiagnoseRecordAppService : EcisPatientAppService, IDiagnoseRecordAppService, ICapSubscribe
    {
        private readonly ILogger<DiagnoseRecordAppService> _log;
        private readonly IFreeSql _freeSql;
        private readonly PdaAppService _pdaAppService;
        private readonly IHospitalApi _hospitalApi;
        private readonly IHisClientAppService _hisClientAppService;
        private ICapPublisher _capPublisher;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log"></param>
        /// <param name="freeSql"></param>
        /// <param name="hisClientAppService"></param>
        /// <param name="pdaAppService"></param>
        /// <param name="hospitalApi"></param>
        /// <param name="capPublisher"></param>
        public DiagnoseRecordAppService(ILogger<DiagnoseRecordAppService> log
            , IFreeSql freeSql
            , IHisClientAppService hisClientAppService
            , PdaAppService pdaAppService
            , IHospitalApi hospitalApi
            , ICapPublisher capPublisher)
        {
            _log = log;
            _freeSql = freeSql;
            _hisClientAppService = hisClientAppService;
            _pdaAppService = pdaAppService;
            _hospitalApi = hospitalApi;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 开立诊断
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> SaveAsync(List<CreateDiagnoseRecordDto> dtoList,
            [BindRequired] Guid pI_ID,
            CancellationToken cancellationToken = default)
        {
            string currentUserCode = CurrentUser.UserName;
            bool isFirstPushGreenChannel = false;
            try
            {
                if (dtoList.Where(x => x.DoctorCode == currentUserCode).Where(x => !string.IsNullOrEmpty(x.DiagnoseCode)).GroupBy(g => g.DiagnoseCode)
                    .Select(s => s.Count()).Any(a => a > 1))
                {
                    return RespUtil.Error<string>(msg: "诊断编码重复");
                }

                if (dtoList.Where(x => x.DoctorCode == currentUserCode).GroupBy(g => g.DiagnoseName).Select(s => s.Count()).Any(a => a > 1))
                {
                    return RespUtil.Error<string>(msg: "诊断名称重复");
                }

                //查询患者信息
                AdmissionRecord admissionRecord = _freeSql.Select<AdmissionRecord>().Where(x => x.PI_ID == pI_ID).First();
                if (admissionRecord == null)
                {
                    throw new Exception("患者不存在");
                }

                if (string.IsNullOrWhiteSpace(admissionRecord.VisSerialNo))
                {
                    string strVisitNo = await _hisClientAppService.GetPatientRegisterInfoByIdAsync(admissionRecord.PatientID, admissionRecord.RegisterNo);
                    if (!string.IsNullOrWhiteSpace(strVisitNo))
                    {
                        admissionRecord.VisSerialNo = strVisitNo;
                        _freeSql.Update<AdmissionRecord>()
                            .SetSource(admissionRecord)
                            .ExecuteAffrows();
                    }
                }

                IEnumerable<string> codeList = dtoList.Select(t => t.DiagnoseCode);
                List<DrugDiagnose> drugDiagnose = await _freeSql.Select<DrugDiagnose>()
                    .Where(a => codeList.Contains(a.DiagnoseCode)).ToListAsync(cancellationToken: cancellationToken);
                IEnumerable<string> strList = dtoList.Where(a => !drugDiagnose.Select(s => s.DiagnoseCode).Contains(a.DiagnoseCode)).Select(s => s.DiagnoseName);
                if (strList.Any())
                {
                    return RespUtil.Error<string>(msg: $"诊断字典【{string.Join(",", strList)}】不存在");
                }

                //查询该患者所有诊断
                List<DiagnoseRecord> recordList = await _freeSql.Select<DiagnoseRecord>().Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && x.PI_ID == pI_ID && !x.IsDeleted).ToListAsync(cancellationToken: cancellationToken);

                List<DiagnoseRecord> addRecord = new List<DiagnoseRecord>();
                //查询需要删除的诊断
                List<DiagnoseRecord> deleteRecord = recordList.FindAll(f => !dtoList.Select(s => s.PD_ID).Contains(f.PD_ID));

                foreach (CreateDiagnoseRecordDto dto in dtoList)
                {
                    DrugDiagnose dict = drugDiagnose.FirstOrDefault(x => x.DiagnoseCode == dto.DiagnoseCode);
                    DiagnoseRecord record = recordList.FirstOrDefault(x => x.PD_ID == dto.PD_ID);
                    DiagnoseRecord model = dto.BuildAdapter().AdaptToType<DiagnoseRecord>();
                    model.DiagnoseClassCode = DiagnoseClass.开立;
                    model.DiagnoseClass = model.DiagnoseClassCode.GetDescription();
                    model.AddUserCode = dto.DoctorCode;
                    model.AddUserName = dto.DoctorName; //前面全部删了，所以取前端传参
                    model.CardRepType = dict?.CardRepType ?? ECardReportingType.Not;
                    model.SetDiagnoseTypeName();
                    model.PD_ID = 0;
                    if (record == null)
                    {
                        addRecord.Add(model);
                    }
                    else
                    {
                        if (record.DiagnoseCode != dto.DiagnoseCode ||
                            record.DiagnoseName != dto.DiagnoseName ||
                            record.DiagnoseTypeCode != dto.DiagnoseTypeCode ||
                            record.Remark != dto.Remark ||
                            record.PyCode != dto.PyCode ||
                            record.Sort != dto.Sort)
                        {
                            deleteRecord.Add(record);
                            model.EmrUsed = record.EmrUsed;
                            addRecord.Add(model);
                        }
                    }
                }



                if (deleteRecord.Any())
                {
                    IEnumerable<int> idList = deleteRecord.Select(x => x.PD_ID);
                    _freeSql.Delete<DiagnoseRecord>().Where(x => idList.Contains(x.PD_ID)).ExecuteAffrows();
                }

                if (addRecord.Any())
                {
                    _freeSql.Insert<DiagnoseRecord>(addRecord).ExecuteAffrows();
                }

                recordList.RemoveAll(deleteRecord);
                addRecord.AddRange(recordList);

                //如果就诊流水号为空，则通过保存就诊记录接口获取
                if (string.IsNullOrWhiteSpace(admissionRecord.VisSerialNo))
                {
                    Enum.TryParse(typeof(TransferType), admissionRecord.AreaCode, true, out var destination);
                    destination ??= TransferType.InDept;
                    var diagnoseRecord = recordList.FirstOrDefault(x => x.DiagnoseType == "主要诊断");

                    string strVisitNo = await _hospitalApi.SaveVisitRecordAsync(admissionRecord, diagnoseRecord, (TransferType)destination, CurrentUser.FindClaimValue("name"), CurrentUser.FindClaimValue("fullname"));
                    if (string.IsNullOrEmpty(strVisitNo))
                    {
                        throw new Exception("获取就诊流水号失败");
                    }
                    admissionRecord.VisSerialNo = strVisitNo;

                    _freeSql.Update<AdmissionRecord>()
                        .SetSource(admissionRecord)
                        .ExecuteAffrows();
                }

                if (string.IsNullOrEmpty(admissionRecord.FirstDoctorCode))
                {
                    admissionRecord.FirstDoctorCode = currentUserCode;
                    admissionRecord.FirstDoctorName = CurrentUser.FindClaimValue("fullName");

                    _freeSql.Update<AdmissionRecord>()
                            .SetSource(admissionRecord)
                            .ExecuteAffrows();
                }

                bool result = await _hospitalApi.SaveRecipeJzAsync(admissionRecord, deleteRecord, addRecord, currentUserCode);
                if (!result)
                {
                    throw new Exception("同步诊断到his失败");
                }

                var diagnoseCode = string.Empty;
                var diagnoseName = string.Empty;
                List<DiagnoseRecord> diagnoseList = addRecord
                .OrderBy(o => o.Sort).ToList();
                if (diagnoseList.Any())
                {
                    diagnoseCode = string.Join(',', diagnoseList.Select(x => x.DiagnoseCode));
                    diagnoseName = string.Join(',', diagnoseList.Select(x => x.DiagnoseName));
                }
                // 同步就诊信息到预检分诊
                await _capPublisher.PublishAsync("sync.visitinfo.from.patient.to.triage",
                    new
                    {
                        Id = admissionRecord.PI_ID,
                        DiagnoseCode = diagnoseCode,
                        DiagnoseName = diagnoseName
                    }, cancellationToken: cancellationToken);

                await _pdaAppService.PatientInfoToPdaAsync(EPatientEventType.AddDiagnose, admissionRecord);

                return RespUtil.Ok<string>(data: isFirstPushGreenChannel.ToString());
            }
            catch (Exception e)
            {
                _log.LogError("Message:{0}", e);
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 收藏诊断
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> SaveCollectionDiagnoseListAsync(
            List<CreateCollectionDiagnoseDto> dtoList,
            CancellationToken cancellationToken = default)
        {
            try
            {
                List<string> diagnoseCodeList = dtoList.Select(s => s.DiagnoseCode).ToList();
                List<DrugDiagnoseDto> diagnoseDictList = await _freeSql.Select<DrugDiagnose>()
                    .Where(x => diagnoseCodeList.Contains(x.DiagnoseCode)).ToListAsync<DrugDiagnoseDto>(cancellationToken);
                var collectedDiagnostics = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.DoctorCode == CurrentUser.UserName && x.DiagnoseClassCode == DiagnoseClass.收藏 &&
                                !x.IsDeleted)
                    .ToListAsync(a => new
                    {
                        a.DiagnoseCode,
                        a.DiagnoseName
                    }, cancellationToken);

                List<CreateCollectionDiagnoseDto> newDtoList = dtoList.Where(dto =>
                     collectedDiagnostics.Count(x =>
                         x.DiagnoseCode == dto.DiagnoseCode && x.DiagnoseName == dto.DiagnoseName) <= 0).ToList();

                List<DiagnoseRecord> modelList = newDtoList.BuildAdapter().AdaptToType<List<DiagnoseRecord>>();
                int maxSort = await _freeSql.Select<DiagnoseRecord>()
                       .Where(x => x.AddUserCode == CurrentUser.UserName
                                   && x.DiagnoseClassCode == DiagnoseClass.收藏)
                       .MaxAsync(a => a.Sort, cancellationToken);

                foreach (var item in modelList)
                {
                    var drug = diagnoseDictList.FirstOrDefault(x => x.DiagnoseCode == item.DiagnoseCode);
                    if (drug != null)
                    {
                        item.MedicalType = drug.DiagType == 2
                            ? MedicalTypeEnum.ChineseMedicine
                            : MedicalTypeEnum.WesternMedicine;
                    }
                    item.DiagnoseClassCode = DiagnoseClass.收藏;
                    item.DiagnoseClass = item.DiagnoseClassCode.GetDescription();
                    item.DoctorCode = CurrentUser.UserName;
                    item.DoctorName = CurrentUser.FindClaimValue("fullName");
                    item.AddUserCode = CurrentUser.UserName;
                    item.AddUserName = CurrentUser.FindClaimValue("fullName");
                    item.Sort = ++maxSort;
                }

                int rows = await _freeSql.Insert(modelList).ExecuteAffrowsAsync(cancellationToken);
                if (rows == modelList.Count)
                {
                    // 删除多余收藏诊断（每个医生最多收藏100条）
                    await _freeSql.Select<DiagnoseRecord>()
                        .Where(x => x.DiagnoseClassCode == DiagnoseClass.收藏
                                    && x.AddUserCode == CurrentUser.UserName)
                        .OrderByDescending(o => o.CreationTime)
                        .Page(2, 1000)
                        .ToDelete()
                        .ExecuteAffrowsAsync(cancellationToken);

                    _log.LogInformation("Save Collection Diagnose List Success");

                    return RespUtil.Ok<string>(msg: "收藏诊断成功");
                }

                _log.LogError("Save Collection Diagnose List Error.Msg:数据库写入失败");
                return RespUtil.Error<string>(msg: "收藏诊断失败！原因：保存数据失败！");
            }
            catch (Exception e)
            {
                _log.LogError("Save Collection Diagnose List Error.Msg:{Msg}", e);
                return RespUtil.Error<string>(msg: $"收藏诊断失败，原因：{e.Message}");
            }
        }

        /// <summary>
        /// 保存收藏诊断排序
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<string>> SaveCollectionDiagnoseSortAsync(
            List<UpdateCollectionDiagnoseSortDto> dtoList, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dtoList.Count <= 0)
                {
                    return RespUtil.Error(data: "传参不能为空");
                }

                List<int> idList = dtoList.Select(s => s.PD_ID).ToList();
                var collectionDiagnoseList = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.DiagnoseClassCode == DiagnoseClass.收藏
                                && x.AddUserCode == dtoList.FirstOrDefault().AddUserCode
                                && x.IsDeleted == false
                                && idList.Contains(x.PD_ID))
                    .ToListAsync(cancellationToken: cancellationToken);
                if (collectionDiagnoseList.Count <= 0)
                {
                    return RespUtil.Error(data: "该医生未收藏诊断");
                }

                foreach (var item in collectionDiagnoseList)
                {
                    var diagnose = dtoList.FirstOrDefault(x => x.PD_ID == item.PD_ID);
                    item.ModUserCode = CurrentUser.UserName;
                    item.ModUserName = CurrentUser.FindClaimValue("fullName");
                    item.Sort = 0;
                    if (diagnose != null)
                    {
                        item.Sort = diagnose.Sort;
                    }
                }

                var rows = await _freeSql.Update<DiagnoseRecord>()
                    .SetSource(collectionDiagnoseList)
                    .ExecuteAffrowsAsync(cancellationToken: cancellationToken);

                if (rows == dtoList.Count)
                {
                    _log.LogInformation("Save Collection Diagnose Sort Success");


                    return RespUtil.Ok(data: "保存收藏诊断排序成功！");
                }

                _log.LogError("Save Collection Diagnose Sort Error.Msg:数据库更新失败");
                return RespUtil.Error(data: "保存收藏诊断排序失败！数据库更新失败");
            }
            catch (Exception e)
            {
                _log.LogError("Save Collection Diagnose Sort Error.Msg:{Msg}", e);
                return RespUtil.InternalError(data: "保存收藏诊断排序失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 删除诊断、取消收藏
        /// </summary>
        /// <param name="dto">删除Dto</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> DeleteDiagnoseRecordAsync([FromBody] DeleteDiagnoseDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var rows = await _freeSql.Update<DiagnoseRecord>()
                    .Set(a => a.IsDeleted, true)
                    .Set(a => a.DelUserCode, CurrentUser.UserName)
                    .Set(a => a.DelUserName, CurrentUser.FindClaimValue("fullName"))
                    .Set(a => a.DeletionTime, DateTime.Now)
                    .WhereIf(dto.PD_ID > 0, x => x.PD_ID == dto.PD_ID)
                    .WhereIf(dto.PI_ID != Guid.Empty, x => x.PI_ID == dto.PI_ID)
                    .Where(x => x.DiagnoseClassCode == dto.DiagnoseClass)
                    .ExecuteAffrowsAsync(cancellationToken);
                if (rows > 0)
                {
                    _log.LogInformation("Delete diagnoseRecord success");
                    return RespUtil.Ok(data: "success", extra: "删除诊断记录成功");
                }

                _log.LogError("Delete diagnoseRecord error.ErrorMsg:数据库写入失败");
                return RespUtil.Error(data: "failed", extra: "删除诊断记录失败");
            }
            catch (Exception e)
            {
                _log.LogError("Delete diagnoseRecord error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<string>(extra: e.Message);
            }
        }

        /// <summary>
        /// 根据输入项查询诊断列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns> 
        public async Task<ResponseResult<IEnumerable<DiagnoseRecordDto>>> GetDiagnoseRecordListAsync(DiagnoseRecordInput input, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.IsDeleted == false)
                    .WhereIf(input.PD_ID > 0, x => x.PD_ID == input.PD_ID)
                    .WhereIf(input.PI_ID != Guid.Empty, x => x.PI_ID == input.PI_ID)
                    .WhereIf(!AbpStringExtensions.IsNullOrWhiteSpace(input.PatientID),
                        x => x.PatientID == input.PatientID)
                    .WhereIf(!AbpStringExtensions.IsNullOrWhiteSpace(input.DoctorCode),
                        x => x.DoctorCode == input.DoctorCode)
                    .Where(x => x.DiagnoseClassCode == input.DiagnoseClassCode)
                    .ToListAsync<DiagnoseRecordDto>(cancellationToken);

                list = list.OrderBy(o => o.Sort).ThenBy(o => o.CreationTime).ToList();
                _log.LogInformation("Get diagnoseRecord list success");
                return RespUtil.Ok<IEnumerable<DiagnoseRecordDto>>(data: list);
            }
            catch (Exception e)
            {
                _log.LogError("Get diagnoseRecord list error.ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<IEnumerable<DiagnoseRecordDto>>(extra: e.Message);
            }
        }

        /// <summary>
        /// 根据输入项查询诊断列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseResult<IEnumerable<DiagnoseRecordDto>>> GetDiagnoseRecordListV2Async(DiagnoseRecordInput input, CancellationToken cancellationToken = default)
        {
            return await GetDiagnoseRecordListAsync(input, cancellationToken);
        }

        /// <summary>
        /// 更新诊断被电子病历引用的标记
        /// </summary>
        /// <returns></returns> 
        [CapSubscribe("modify.diagnoseRecord.emrUsed")]
        [AllowAnonymous]
        public async Task<ResponseResult<bool>> ModifyDiagnoseRecordEmrUsedAsync(IList<int> pdid)
        {
            try
            {
                _ = await _freeSql.Update<DiagnoseRecord>()
                    .Set(s => s.EmrUsed, true)
                    .Where(w => pdid.Contains(w.PD_ID))
                    .ExecuteAffrowsAsync();
                return RespUtil.Ok<bool>(data: true);
            }
            catch (Exception e)
            {
                _log.LogError(e, $"更新诊断被电子病历引用的标记异常：{e.Message}");
                return RespUtil.Error<bool>(data: false);
            }
        }

        /// <summary>
        /// 查询快速诊断列表 CommonlyUsed：常用   Collection：收藏   RecentHistory：最近历史
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="type">类型  -1:全部  0：常用  1：收藏   2：最近历史（默认查询全部）</param>
        /// <returns></returns>
        public async Task<ResponseResult<Dictionary<string, IEnumerable<FastDiagnoseDto>>>>
            GetFastDiagnoseListAsync(
                string filter, FastDiagnoseType type = FastDiagnoseType.All)
        {
            var dict = new Dictionary<string, IEnumerable<FastDiagnoseDto>>();
            try
            {
                string str = "";
                if (string.IsNullOrEmpty(filter))
                {
                    str = "and 1=1";
                }
                else
                {
                    str =
                        $" and (PyCode like '%{filter}%' or DiagnoseName like '%{filter}%' or DiagnoseCode like '%{filter}%')";
                }

                var currentUser = CurrentUser.UserName;
                if (type == FastDiagnoseType.CommonlyUsed || type == FastDiagnoseType.All)
                {
                    var commonlyUsed = await _freeSql.Select<DiagnoseRecord>().WithSql(
                        @$"select top 100 DiagnoseCode,DiagnoseName,PyCode,MedicalType,Icd10 from Pat_DiagnoseRecord 
                        where  AddUserCode='{currentUser}' and DiagnoseClassCode = {(int)DiagnoseClass.开立}
                        {str}
                        and IsDeleted = 0
                        GROUP BY DiagnoseCode,DiagnoseName,PyCode,MedicalType,Icd10
                        order by count(DiagnoseName) DESC ").ToListAsync<FastDiagnoseDto>();
                    dict.TryAdd(FastDiagnoseType.CommonlyUsed.ToString(), commonlyUsed);
                }

                if (type == FastDiagnoseType.Collection || type == FastDiagnoseType.All)
                {
                    var collection = await _freeSql.Select<DiagnoseRecord>()
                        .Where(x => x.DiagnoseClassCode == DiagnoseClass.收藏
                                    && x.AddUserCode == currentUser
                                    && x.IsDeleted == false)
                        .WhereIf(!string.IsNullOrEmpty(filter),
                            x => x.DiagnoseCode.Contains(filter) || x.PyCode.Contains(filter) ||
                                 x.DiagnoseName.Contains(filter))
                        .ToListAsync<FastDiagnoseDto>();
                    dict.TryAdd(FastDiagnoseType.Collection.ToString(), collection);
                }

                if (type == FastDiagnoseType.RecentHistory || type == FastDiagnoseType.All)
                {
                    var recentHistory = await _freeSql.Select<DiagnoseRecord>()
                        .WithSql(
                            $@"select top 100 DiagnoseCode,DiagnoseName,PyCode,MedicalType,Icd10 from Pat_DiagnoseRecord
                                    where DiagnoseClassCode = {(int)DiagnoseClass.开立} 
                                    and AddUserCode = '{currentUser}' and IsDeleted = 0
                                    {str}
                                    group by DiagnoseCode,DiagnoseName,PyCode,MedicalType,Icd10
                                    order by max(CreationTime) desc ")
                        .ToListAsync<FastDiagnoseDto>();
                    dict.TryAdd(FastDiagnoseType.RecentHistory.ToString(), recentHistory);
                }

                return RespUtil.Ok(data: dict);
            }
            catch (Exception e)
            {
                dict.TryAdd(FastDiagnoseType.CommonlyUsed.ToString(), new List<FastDiagnoseDto>());
                dict.TryAdd(FastDiagnoseType.Collection.ToString(), new List<FastDiagnoseDto>());
                dict.TryAdd(FastDiagnoseType.RecentHistory.ToString(), new List<FastDiagnoseDto>());
                _log.LogError("DiagnoseRecordAppService Get Fast Diagnose List Error.Msg:{Msg}", e);
                return RespUtil.Error(data: dict);
            }
        }

        /// <summary>
        /// 获取历史诊断
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<ResponseResult<IEnumerable<DiagnoseWithDeptDto>>> GetHistoryDiagnoseListAsync(
            string patientId, Guid pI_ID)
        {
            return await _hospitalApi.GetHistoryDiagnoseListAsync(patientId, pI_ID);
        }







        /// <summary>
        /// 获取诊断证明
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="pI_ID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<DiagnoseCertificateDto>> GetDiagnoseCertificateByIdAsync(DiagnoseRecordInput input, CancellationToken cancellationToken = default)
        {
            var diagnoseCertificate = new DiagnoseCertificateDto();
            try
            {
                //获取患者信息
                var patient = await _freeSql.Select<AdmissionRecord>()
                   .WhereIf(input.PI_ID != Guid.Empty, x => x.PI_ID == input.PI_ID)
                   .WhereIf(!AbpStringExtensions.IsNullOrWhiteSpace(input.PatientID), x => x.PatientID == input.PatientID)
                   .FirstAsync<AdmissionRecordDto>(cancellationToken);

                if (patient == null)
                    return RespUtil.Error<DiagnoseCertificateDto>(msg: "患者不存在");

                diagnoseCertificate.PatientName = patient.PatientName;
                diagnoseCertificate.GenderName = patient.SexName;
                diagnoseCertificate.Age = patient.Age;
                diagnoseCertificate.VisitNo = patient.VisitNo;
                diagnoseCertificate.RegisterNo = patient.RegisterNo;

                //开始日期：默认值：患者就诊时间
                //结束日期：默认值：患者就诊时间 +1天（即默认天数是2）
                diagnoseCertificate.RestDateBegin = patient.VisitDate?.ToString("yyyy-MM-dd");
                diagnoseCertificate.RestDateEnd = patient.VisitDate.HasValue ? patient.VisitDate.Value.AddDays(1).ToString("yyyy-MM-dd") : null;
                diagnoseCertificate.AdvicedRestDays = 2;
                diagnoseCertificate.CompanyName = patient.Address;

                //诊断记录
                var list = await _freeSql.Select<DiagnoseRecord>()
                    .Where(x => x.IsDeleted == false)
                    .WhereIf(input.PD_ID > 0, x => x.PD_ID == input.PD_ID)
                    .WhereIf(input.PI_ID != Guid.Empty, x => x.PI_ID == input.PI_ID)
                    .WhereIf(!AbpStringExtensions.IsNullOrWhiteSpace(input.PatientID), x => x.PatientID == input.PatientID)
                    .WhereIf(!AbpStringExtensions.IsNullOrWhiteSpace(input.DoctorCode), x => x.DoctorCode == input.DoctorCode)
                    .Where(x => x.DiagnoseClassCode == input.DiagnoseClassCode)
                    .ToListAsync(a => new
                    {
                        a.Sort,
                        a.DiagnoseName,
                        a.DoctorCode,
                        a.DoctorName,
                        a.CreationTime
                    });
                list = list.OrderBy(o => o.Sort).ThenBy(o => o.CreationTime).ToList();

                var diagnoseStr = string.Empty;
                foreach (var diagnose in list)
                {
                    diagnoseStr += (list.IndexOf(diagnose) + 1) + ", " + diagnose.DiagnoseName + "\n";
                }
                diagnoseCertificate.Diagnose = diagnoseStr;

                //医生建议
                diagnoseCertificate.DoctorAdvice = input.DoctorAdvice;
                diagnoseCertificate.DoctorName = list.FirstOrDefault()?.DoctorName;

                diagnoseCertificate.DoctorSign = string.Empty;

                _log.LogInformation("Get DiagnoseCertificate success");
                return RespUtil.Ok(data: diagnoseCertificate);
            }
            catch (Exception e)
            {
                _log.LogError("Get DiagnoseCertificate error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<DiagnoseCertificateDto>(extra: e.Message);
            }
        }

        /// <summary>
        /// 关闭绿通状态
        /// </summary>
        /// <param name="ar_id"></param>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> CloseGreenChannelAsync([FromBody] CloseGreenChannelDto closeGreenChannelDto)
        {
            try
            {
                var result = await _freeSql.Update<AdmissionRecord>()
                     .Set(s => s.IsOpenGreenChannl, closeGreenChannelDto.IsOpen)
                     .Set(s => s.GreenRoadCode, string.Empty)
                     .Set(s => s.GreenRoadName, string.Empty)
                     .Where(w => w.AR_ID == closeGreenChannelDto.AR_ID)
                     .ExecuteAffrowsAsync();
                return RespUtil.Ok<bool>(data: result > 0);
            }
            catch (Exception e)
            {
                _log.LogError(e, $"关闭绿通状态异常：{e.Message}");
                return RespUtil.Error<bool>(data: false);
            }
        }

    }
}