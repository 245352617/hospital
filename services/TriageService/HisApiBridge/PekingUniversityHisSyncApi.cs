using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TriageService.Extensions;
using TriageService.HisApiBridge.Model;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 北大医院 HIS Api 部分方法
    /// 同步HIS挂号列表及相关方法
    /// 获取挂号患者列表及相关方法
    /// </summary>
    public partial class PekingUniversityHisApi : IHisApi, IPekingUniversityHisApi
    {
        #region 同步HIS挂号列表 及 相关方法
        /// <summary>
        /// 同步 HIS 挂号列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> SyncRegisterPatientFromHis()
        {
            _log.LogDebug($"同步HIS挂号列表开始");
            Stopwatch swTotal = Stopwatch.StartNew();

            using var uow = this._unitOfWorkManager.Begin();

            Stopwatch stopwatch = Stopwatch.StartNew();
            var dicts = await _triageConfigService.GetTriageConfigByRedisAsync(isEnable: -1, isDeleted: -1);
            long spanGetDicts = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 获取北大 HIS 急诊科挂号患者列表，直接从北大数据库视图中获取
            IEnumerable<HisRegisterPatient> hisPatientInfos = await this.GetHisPatientInfoAsync();
            long spanGetHisPatientInfos = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 通过对比his与缓存与数据库中的数据 获取挂号列表新增、修改的患者数据
            var (newPatients, updatePatients) = await this.GetHisNewAndUpdatePatients(hisPatientInfos);
            long spanGetAddAndUpdateList = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            HashSet<Guid> patientIDList = new HashSet<Guid>(); //需要发送MQ的患者ID列表，使用HashSet以去重

            try
            {
                foreach (var hisNewPatient in newPatients)
                {
                    // 新增患者信息
                    var patientInfo = NewPatientInfo(dicts, hisNewPatient);
                    _log.LogDebug($"同步HIS数据，最终需要新增患者-{patientInfo.PatientName}-信息：{JsonSerializer.Serialize(patientInfo, options)}");
                    await this._patientInfoRepository.InsertAsync(patientInfo);
                    if (patientInfo.VisitStatus != VisitStatus.NotTriageYet)
                        patientIDList.Add(patientInfo.Id);
                }
                long spanInsertNewPatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                // 预检分诊数据库一天内的患者
                var oneDayPatients = await this._patientInfoRepository
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.ScoreInfo)
                    .Where(x => x.RegisterInfo.Any(y => y.RegisterTime >= DateTime.Now.AddDays(-1)))
                    .ToListAsync();
                long spanGetOneDayPatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                // 预检分诊、HIS 都存在的患者
                var bothExistsPatients = oneDayPatients
                    // 虽然此处 PatientInfo 与 RegisterInfo 是一对多关系，实际上在先挂号后分诊模式上，他们是一对一的关系  by: ywlin 2022-01-06
                    .Where(x => x.RegisterInfo.Any(
                        y => hisPatientInfos.Select(_ => _.RegisterNo).Contains(y.RegisterNo)));
                long spanGetBothExistsPatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                foreach (var hisPatient in updatePatients)
                {
                    var existsPatient = bothExistsPatients.FirstOrDefault(x =>
                        x.PatientId == hisPatient.PatientId &&
                        x.RegisterInfo.Any(y => y.RegisterNo == hisPatient.RegisterNo));
                    if (existsPatient != null)
                    {
                        existsPatient = MergePatientInfo(existsPatient, dicts, hisPatient);
                        _log.LogDebug($"同步HIS数据，最终需要更新患者-{existsPatient.PatientName}-信息：{JsonSerializer.Serialize(existsPatient, options)}");
                        // 若患者信息已存在，则更新患者信息
                        //await this._patientInfoRepository.UpdateAsync(dbExistsPatients);
                        bool IsDoUpdate = await this._patientInfoRepository.UpdateRecordAsync(existsPatient);
                        if (IsDoUpdate)
                            patientIDList.Add(existsPatient.Id);
                    }
                    else
                    {
                        // 新增患者信息
                        var patientInfo = NewPatientInfo(dicts, hisPatient);
                        _log.LogDebug($"同步HIS数据，最终需要新增患者-{patientInfo.PatientName}-信息：{JsonSerializer.Serialize(patientInfo, options)}");
                        await this._patientInfoRepository.InsertAsync(patientInfo);
                        if (patientInfo.VisitStatus != VisitStatus.NotTriageYet)
                            patientIDList.Add(patientInfo.Id);
                    }
                }
                long spanUpdatePatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                // 在 HIS 挂号列表不存在的患者，需要标识成已过号（已就诊）
                var expirePatients = oneDayPatients.Except(bothExistsPatients);
                foreach (var item in expirePatients)
                {
                    if (item.VisitStatus != VisitStatus.Treated)
                    {
                        // HIS 挂号列表视图已经不存在（已经超过就诊时间），标识为已就诊（实际应该是已过号之类的状态）
                        item.VisitStatus = VisitStatus.Treated;
                        _log.LogDebug($"同步HIS数据，已超过就诊时间，设置患者-{item.PatientName}-为已就诊状态");
                        //await this._patientInfoRepository.UpdateAsync(item);
                        bool IsDoUpdate = await this._patientInfoRepository.UpdateRecordAsync(item);
                    }
                }
                long spanExpirePatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                await uow.CompleteAsync();

                long spanUowCompleteAsync = stopwatch.StopThenGetElapsedMilliseconds();

                _log.LogDebug($"同步HIS挂号列表成功（），共耗时：{spanGetDicts + spanGetHisPatientInfos + spanGetAddAndUpdateList + spanInsertNewPatients + spanGetOneDayPatients + spanGetBothExistsPatients + spanUpdatePatients + spanExpirePatients}，" +
                                    $"获得字典耗时：{spanGetDicts}，" +
                                    $"从北大数据库视图中获取数据耗时：{spanGetHisPatientInfos}，" +
                                    $"通过对比his与缓存与数据库中的数据 获取挂号列表新增、修改的患者数据耗时：{spanGetAddAndUpdateList}，" +
                                    $"新增患者耗时：{spanInsertNewPatients}，" +
                                    $"获得预检分诊数据库一天内的患者耗时：{spanGetOneDayPatients}，" +
                                    $"预检分诊、HIS 都存在的患者耗时：{spanGetBothExistsPatients}，" +
                                    $"新增患者信息，耗时：{spanUpdatePatients}，" +
                                    $"标识已过号，耗时：{spanExpirePatients}，" +
                                    $"spanUowCompleteAsync，耗时：{spanUowCompleteAsync}");

            }
            catch (Exception)
            {
                _log.LogError("同步HIS挂号列表发生错误");
                await uow.RollbackAsync();
                throw;
            }

            swTotal.Stop();
            _log.LogTrace($"同步HIS挂号列表结束，共耗时：{swTotal.ElapsedMilliseconds}");

            // 发送MQ消息，通知Patient服务新增/更新患者信息
            if (patientIDList.Count != 0)
            {
                await SendUpdateMQ(patientIDList, dicts);
            }

            return JsonResult.Ok("同步成功.");
        }

        /// <summary>
        /// 获取挂号列表新增、修改的患者数据
        /// 通过对比his与缓存与数据库中的数据
        /// 同步数据使用
        /// </summary>
        /// <param name="hisPatientInfos"></param>
        /// <returns></returns>
        private async Task<(IEnumerable<HisRegisterPatient>, IEnumerable<HisRegisterPatient>)>
            GetHisNewAndUpdatePatients(IEnumerable<HisRegisterPatient> hisPatientInfos)
        {
            _log.LogInformation("同步HIS挂号列表，获取挂号列表新增、修改的患者数据方法GetHisNewAndUpdatePatients开始");
            Stopwatch stopwatch = Stopwatch.StartNew();

            // 新增的患者
            List<HisRegisterPatient> newPatients;
            // 修改的患者
            IEnumerable<HisRegisterPatient> updatePatients;
            string cacheKey = $"{_configuration["ServiceName"]}:RegisterPatients";
            if (await _redis.KeyExistsAsync(cacheKey))
            {
                Stopwatch sw = Stopwatch.StartNew();

                // 从缓存获取上一次同步的数据
                RedisValue json = await _redis.StringGetAsync(cacheKey);
                List<HisRegisterPatient> patientRedisCache = json.HasValue ? JsonSerializer.Deserialize<List<HisRegisterPatient>>(json) : new List<HisRegisterPatient>();

                var spanGetFromCache = sw.StopThenGetElapsedMillisecondsThenRestart();

                // 缓存当前同步的数据
                await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(hisPatientInfos));

                var spanDoCache = sw.StopThenGetElapsedMillisecondsThenRestart();

                // 比对缓存的上一次同步的数据，拿到新增的患者列表
                newPatients = hisPatientInfos.Where(x =>
                    !patientRedisCache.Any(y => x.PatientId == y.PatientId && x.RegisterNo == y.RegisterNo)).ToList();

                var spanGetNewPatients = sw.StopThenGetElapsedMillisecondsThenRestart();

                // 比对缓存的上一次同步的数据，拿到信息更新了的患者列表
                updatePatients = hisPatientInfos.Where(x => patientRedisCache.Any(y =>
                    x.PatientId == y.PatientId && x.RegisterNo == y.RegisterNo &&
                    (x.PatientName != y.PatientName || x.DoctorCode != y.DoctorCode || x.DoctorName != y.DoctorName || x.BeginTime != y.BeginTime
                     || x.EndTime != y.EndTime
                     || x.JZZT != y.JZZT || x.HKDZ != y.HKDZ || x.RefundStatus != y.RefundStatus
                     || x.TriageLevel != y.TriageLevel || x.QueueId != y.QueueId
                     || x.ContactsPhone != y.ContactsPhone || x.DateOfBirth != y.DateOfBirth)
                ));

                var spanGetUpdatePatients = sw.StopThenGetElapsedMillisecondsThenRestart();

                List<PatientInfo> allPatientList = new List<PatientInfo>();

                string compareType = _configuration["PekingUniversity:HIS_GetPatientListFunc"].ParseToString();
                if (compareType == "GetDataTheCal")
                {
                    // 更改为只比对最近5天的数据，提升访问速度
                    var allPatientList5Days = await this._patientInfoRepository.AsNoTracking()
                                                              .Include(c => c.RegisterInfo)
                                                              .Where(x => x.CreationTime >= DateTime.Now.AddDays(-5)) // 限定只查询最近5天的数据，提升访问速度
                                                              .ToListAsync();
                    allPatientList = allPatientList5Days.AsParallel().Where(x => x.RegisterInfo.Any(
                                                                  y => hisPatientInfos.Select(z => z.RegisterNo).Contains(y.RegisterNo))).ToList();
                }
                else
                {
                    allPatientList = await this._patientInfoRepository.AsNoTracking()
                                              .Include(c => c.RegisterInfo)
                                              .Where(x => x.RegisterInfo.Any(
                                                  y => hisPatientInfos.Select(z => z.RegisterNo).Contains(y.RegisterNo)))
                                              .ToListAsync();
                }

                var spanGetPatientsFromDB = sw.StopThenGetElapsedMillisecondsThenRestart();

                //添加存在进入缓存但未存入数据库的数据
                {
                    Stopwatch swIn = Stopwatch.StartNew();
                    // 比对缓存的上一次同步的数据，拿到缓存和his同时存在的患者列表
                    var hisAndCacheBothPatients =
                        hisPatientInfos.Where(x => patientRedisCache.Any(y => x.PatientId == y.PatientId && x.RegisterNo == y.RegisterNo)).ToList();
                    long span1 = swIn.StopThenGetElapsedMillisecondsThenRestart();
                    // 比对数据库列表和缓存列表，获得数据库和缓存中同时存在的患者列表
                    var existPatientList =
                        allPatientList.Where(x => patientRedisCache.Any(y => x.PatientId == y.PatientId && x.RegisterInfo.Any(y => hisPatientInfos.Select(z => z.RegisterNo).Contains(y.RegisterNo)))).ToList();
                    long span2 = swIn.StopThenGetElapsedMillisecondsThenRestart();
                    // 比对数据库列表和缓存列表，获得数据库和缓存中同时存在的患者列表
                    foreach (var item in hisAndCacheBothPatients)
                    {
                        //判断数据库是否存在数据
                        if (!existPatientList.AsParallel().Any(p => p.PatientId == item.PatientId && p.RegisterInfo.Any(y => item.RegisterNo == y.RegisterNo)))
                        {
                            newPatients.Add(item);
                        }
                    }
                    long span3 = swIn.StopThenGetElapsedMillisecondsThenRestart();
                    _log.LogDebug($"同步HIS数据：GetHisNewAndUpdatePatients方法里的内部方法\n" +
                                            $"共耗时：{span1 + span2 + span3}ms，其中：\n" +
                                            $"span1：{span1}ms，hisAndCacheBothPatients count:{hisAndCacheBothPatients.Count}\n" +
                                            $"span2：{span2}ms，existPatientList count:{existPatientList.Count}\n" +
                                            $"span3：{span3}ms");
                }

                var spanCompare1 = sw.StopThenGetElapsedMillisecondsThenRestart();

                //手动删缓存会导致同步挂号产生两条数据，添加数据库校验
                {
                    //数据库中有缓存里面没有
                    var existPatientList = allPatientList.Where(
                        x => !patientRedisCache.Any(y => x.PatientId == y.PatientId && x.RegisterInfo.Any(y => hisPatientInfos.Select(z => z.RegisterNo).Contains(y.RegisterNo))));
                    newPatients.RemoveAll(x => existPatientList.Select(y => y.PatientId).Contains(x.PatientId) && existPatientList.Any(y => y.RegisterInfo.Select(p => p.RegisterNo).Contains(x.RegisterNo)));
                }

                var spanRemove1 = sw.StopThenGetElapsedMilliseconds();

                _log.LogInformation($"同步HIS挂号列表：GetHisNewAndUpdatePatients方法有缓存分支，" +
                                            $"获取挂号列表新增、修改的患者数据结束，共耗时：{spanGetFromCache + spanDoCache + spanGetNewPatients + spanGetUpdatePatients + spanGetPatientsFromDB + spanCompare1 + spanRemove1}ms，其中：，" +
                                            $"获得挂号列表缓存：{spanGetFromCache}ms，" +
                                            $"缓存当前同步的数据：{spanDoCache}ms，" +
                                            $"获得新增患者列表：{spanGetNewPatients}ms，" +
                                            $"获得修改患者列表：{spanGetUpdatePatients}ms，" +
                                            $"获得数据库患者列表：{spanGetPatientsFromDB}ms，compareType = {(string.IsNullOrEmpty(compareType) ? "old" : compareType)}，" +
                                            $"添加存在进入缓存但未存入数据库的数据：{spanCompare1}ms，" +
                                            $"移除数据库中有缓存里面没有：{spanRemove1}ms，" +
                                            $"新增患者数量：{newPatients.Count}，" +
                                            $"修改患者数量：{updatePatients.Count()}" +
                                            $"原redis缓存患者数量：{patientRedisCache.Count}" +
                                            $"由HIS同步获得的患者数量：{hisPatientInfos.Count()}"
                                            );

                //_log.LogInformation("本次缓存患者{AddNumber}，his患者{UpdateNumber}", JsonConvert.SerializeObject(patientCache?.ToList().Select(p => p.RegisterNo)), JsonConvert.SerializeObject(hisPatients?.ToList().Select(p => p.RegisterNo)));
            }
            else
            {
                _log.LogInformation($"同步HIS挂号列表：GetHisNewAndUpdatePatients方法无缓存分支");

                // 缓存当前同步的数据
                await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(hisPatientInfos));
                // 保证没有缓存对比时，数据对比不出错
                var existsPatient = await this._patientInfoRepository
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.ScoreInfo)
                    // 虽然此处 PatientInfo 与 RegisterInfo 是一对多关系，实际上在先挂号后分诊模式先，他们是一对一的关系  by: ywlin 2022-01-06
                    .Where(x => x.RegisterInfo.Any(
                        y => hisPatientInfos.Select(z => z.RegisterNo).Contains(y.RegisterNo)))
                    .ToListAsync();
                // 比对缓存的上一次同步的数据，拿到新增的患者列表
                newPatients = hisPatientInfos.Where(x => !existsPatient.Any(y => x.PatientId == y.PatientId)).ToList();
                // 比对缓存的上一次同步的数据，拿到信息更新了的患者列表
                updatePatients = hisPatientInfos.Where(x => existsPatient.Any(y => x.PatientId == y.PatientId));
            }

            stopwatch.Stop();
            _log.LogInformation($"同步HIS挂号列表，获取挂号列表新增、修改的患者数据方法GetHisNewAndUpdatePatients结束，共耗时：{stopwatch.StopThenGetElapsedMilliseconds()}ms");

            return (newPatients, updatePatients);
        }

        /// <summary>
        /// HIS 患者信息转换成预检分诊患者信息
        /// 新增
        /// 初始化分诊结果信息、初始化入院情况信息
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="hisPatient"></param>
        /// <returns></returns>
        private PatientInfo NewPatientInfo(Dictionary<string, List<TriageConfigDto>> dicts,
            HisRegisterPatient hisPatient)
        {
            // 患者分诊状态（默认暂存，当预检分诊已存在患者信息时使用预检分诊现有状态）
            var patientInfo = new PatientInfo() { TriageStatus = 0, RegisterInfo = new List<RegisterInfo>() }
                .SetId(_guidGenerator.Create());
            // 患者就诊状态同步（默认待就诊）
            VisitStatus visitStatus = GetVisitStatus(dicts, hisPatient, patientInfo.TriageStatus);

            // 费别
            var feeType = dicts[TriageDict.Faber.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.ChargeType);

            // 病患基本信息，HisRegisterPatient到PatientInfo的映射设置
            hisPatient.BuildAdapter()
                .ForkConfig(forked => forked.ForType<HisRegisterPatient, PatientInfo>()
                    .Map(dest => dest.Birthday, src => src.DateOfBirth)
                    .Map(dest => dest.Address, src => src.HKDZ)
                    .Map(dest => dest.ContactsAddress, src => src.HKDZ)
                    .Map(dest => dest.Sex,
                        src => src.Sex == 1 ? "Sex_Man" : (src.Sex == 2 ? "Sex_Woman" : "Sex_Unknown"))
                    .Map(dest => dest.SexName, src => src.Sex == 1 ? "男" : (src.Sex == 2 ? "女" : "未知"))
                    // 医保控费相关 S
                    .Map(dest => dest.PatnId, src => src.PatnId)
                    .Map(dest => dest.CurrMDTRTId, src => src.CurrMDTRTId)
                    .Map(dest => dest.PoolArea, src => src.PoolArea)
                    .Map(dest => dest.InsureType, src => src.InsureType)
                    .Map(dest => dest.OutSetlFlag, src => src.OutSetlFlag)
                    // 医保控费相关 E
                    .Ignore(dest => dest.IsTop)
                    .Ignore(dest => dest.IsUntreatedOver)
                    .Ignore(dest => dest.RegisterInfo))
                    .AdaptTo(patientInfo);
            //patientInfo = dicts.SetPatientInfo(patientInfo);
            // 费别
            patientInfo.ChargeType = feeType?.TriageConfigCode;
            patientInfo.ChargeTypeName = feeType?.TriageConfigName;
            // 就诊状态
            patientInfo.VisitStatus = visitStatus;
            // 就诊号
            if (string.IsNullOrEmpty(patientInfo.VisitNo))
            {
                patientInfo.VisitNo = hisPatient.VisitNo;
            }

            // 限制患者基本信息为只读
            patientInfo.IsBasicInfoReadOnly = true;
            // 当从外部获取新冠问卷时，不在ECIS中录入个人史信息
            patientInfo.IsCovidExamFromOuterSystem = true;

            // 通过队列号查询对应的科室/诊室
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.QueueId);

            // 挂号信息
            var registerInfo = new RegisterInfo(_guidGenerator.Create(), patientInfo.Id);

            var hisRegisterAdapter = hisPatient.BuildAdapter()
                .ForkConfig(forked => forked.ForType<HisRegisterPatient, RegisterInfo>()
                    .Map(dest => dest.RegisterDeptCode, src => src.DepartCode)
                    .Map(dest => dest.RegisterDoctorName, src => src.DoctorName)
                    .Map(dest => dest.RegisterDoctorCode, src => src.DoctorCode));
            registerInfo = hisRegisterAdapter.AdaptTo(registerInfo);
            registerInfo.RegisterDeptCode = deptConfig?.TriageConfigCode;
            patientInfo.RegisterInfo.Add(registerInfo);

            // 初始化分诊结果信息
            patientInfo.ConsequenceInfo ??= new ConsequenceInfo()
            {
                PatientInfoId = patientInfo.Id,
            }
                .SetId(_guidGenerator.Create());
            // 这个东西吧，你加了这个第一次保存的时候会丢失生命体征信息，那段代码我不想改
            // patientInfo.VitalSignInfo ??= new VitalSignInfo()
            //     {
            //         PatientInfoId = patientInfo.Id,
            //     }
            //     .SetId((_guidGenerator.Create()));

            // 初始化入院情况信息
            patientInfo.AdmissionInfo ??= new AdmissionInfo()
            {
                PatientInfoId = patientInfo.Id,
            }
            .SetId((_guidGenerator.Create()));

            // 如果有分诊级别就是已分诊，添加分诊信息
            if (hisPatient.TriageLevel.HasValue)
            {
                // 分诊级别
                var triageLevelConfig = dicts[TriageDict.TriageLevel.ToString()]
                    .FirstOrDefault(x => x.TriageConfigCode == $"TriageLevel_{hisPatient.TriageLevel:D3}");
                if (triageLevelConfig != null)
                {
                    patientInfo.ConsequenceInfo.ActTriageLevel = triageLevelConfig.TriageConfigCode;
                    patientInfo.ConsequenceInfo.ActTriageLevelName = triageLevelConfig.TriageConfigName;
                    // 当已有队列ID时，我们认为该患者已在旧系统分诊
                    patientInfo.TriageStatus = 1;
                    // 如果没有分诊时间，同步分诊时间（因为挂号列表视图并未提供，故而设置为当前时间）
                    patientInfo.TriageTime ??= DateTime.Now;
                    // 如果没有排队号，同步排队号（因为挂号列表视图并未提供排队号，故而排队号设置为“暂无”）
                    patientInfo.CallingSn ??= "暂无";
                }

                if (!string.IsNullOrEmpty(hisPatient.QueueId) && deptConfig != null)
                {
                    // 修改分诊科室
                    patientInfo.ConsequenceInfo.TriageDeptCode = deptConfig.TriageConfigCode;
                    patientInfo.ConsequenceInfo.TriageDeptName = deptConfig.TriageConfigName;
                }
            }

            // 其它默认值
            // 1. 国籍
            if (dicts.Keys.Contains(TriageDict.Country.ToString()))
            {
                var countries = dicts[TriageDict.Country.ToString()].Where(x => x.IsDeleted == 0 && x.IsDisable > 0)
                    .OrderBy(x => x.Sort);
                var countryCode = patientInfo.PatientName.IsChinese() ? "Country_001" : "Country_005";
                var countryConfig = countries.FirstOrDefault(x => x.TriageConfigCode == countryCode);
                patientInfo.CountryCode = countryCode;
                patientInfo.CountryName = countryConfig?.TriageConfigName;
            }

            // 2. 人群
            if (dicts.Keys.Contains(TriageDict.Crowd.ToString()))
            {
                var crowds = dicts[TriageDict.Crowd.ToString()].Where(x => x.IsDeleted == 0 && x.IsDisable > 0)
                    .OrderBy(x => x.Sort);
                var crowd = crowds.FirstOrDefault();
                patientInfo.CrowdCode = crowd?.TriageConfigCode;
                patientInfo.CrowdName = crowd?.TriageConfigName;
            }

            // 3. 来院方式
            if (dicts.Keys.Contains(TriageDict.ToHospitalWay.ToString()))
            {
                var toHospitalWays = dicts[TriageDict.ToHospitalWay.ToString()]
                    .Where(x => x.IsDeleted == 0 && x.IsDisable > 0).OrderBy(x => x.Sort);
                // 默认来院方式为步行
                var toHospitalWay = toHospitalWays.FirstOrDefault(x => x.TriageConfigName == "步行");
                patientInfo.ToHospitalWayCode = toHospitalWay?.TriageConfigCode;
                patientInfo.ToHospitalWayName = toHospitalWay?.TriageConfigName;
            }

            // 4. 就诊类型
            if (dicts[TriageDict.TypeOfVisit.ToString()] != null)
            {
                var typeOfVisits = dicts[TriageDict.TypeOfVisit.ToString()]
                    .Where(x => x.IsDeleted == 0 && x.IsDisable > 0).OrderBy(x => x.Sort);
                var typeOfVisit = typeOfVisits.FirstOrDefault();
                patientInfo.TypeOfVisitCode = typeOfVisit?.TriageConfigCode;
                patientInfo.TypeOfVisitName = typeOfVisit?.TriageConfigName;
            }

            return patientInfo;
        }

        /// <summary>
        /// HIS 患者信息转换成预检分诊患者信息
        /// 合并更新
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="dicts"></param>
        /// <param name="hisPatient"></param>
        /// <returns></returns>
        private PatientInfo MergePatientInfo(PatientInfo patientInfo, Dictionary<string, List<TriageConfigDto>> dicts,
            HisRegisterPatient hisPatient)
        {
            // 患者就诊状态同步（默认待就诊）
            VisitStatus visitStatus = GetVisitStatus(dicts, hisPatient, patientInfo.TriageStatus, patientInfo);

            // 费别
            var feeType = dicts[TriageDict.Faber.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.ChargeType);

            // 初始化分诊信息
            patientInfo.ConsequenceInfo ??= new ConsequenceInfo()
            {
                PatientInfoId = patientInfo.Id,
            }.SetId(_guidGenerator.Create());

            //结束就诊时间更变的情况下，设置是否变更就诊，并在挂号列表隐藏
            if (hisPatient.EndTime.HasValue && patientInfo.EndTime.HasValue && hisPatient.EndTime != patientInfo.EndTime)
            {
                patientInfo.ConsequenceInfo.ChangeTriage = false;
            }

            // 病患基本信息
            hisPatient.BuildAdapter()
                .ForkConfig(forked => forked.ForType<HisRegisterPatient, PatientInfo>()
                    .Map(dest => dest.Birthday, src => src.DateOfBirth)
                    // .Map(dest => dest.Address, src => src.HKDZ)
                    // .Map(dest => dest.ContactsAddress, src => src.HKDZ)
                    .Map(dest => dest.Sex,
                        src => src.Sex == 1 ? "Sex_Man" : (src.Sex == 2 ? "Sex_Woman" : "Sex_Unknown"))
                    .Map(dest => dest.SexName, src => src.Sex == 1 ? "男" : (src.Sex == 2 ? "女" : "未知"))
                    // 医保控费相关 S
                    .Map(dest => dest.PatnId, src => src.PatnId)
                    .Map(dest => dest.CurrMDTRTId, src => src.CurrMDTRTId)
                    .Map(dest => dest.PoolArea, src => src.PoolArea)
                    .Map(dest => dest.InsureType, src => src.InsureType)
                    .Map(dest => dest.OutSetlFlag, src => src.OutSetlFlag)
                    // 医保控费相关 E
                    .Ignore(dest => dest.IsTop)
                    .Ignore(dest => dest.IsUntreatedOver)
                    .Ignore(dest => dest.RegisterInfo)
                    .Ignore(dest => dest.VisitNo))
                .AdaptTo(patientInfo);
            //patientInfo = dicts.SetPatientInfo(patientInfo);
            // 费别
            patientInfo.ChargeType = feeType?.TriageConfigCode;
            patientInfo.ChargeTypeName = feeType?.TriageConfigName;
            // 地址同步
            if (patientInfo.Address.IsNullOrWhiteSpace() && !hisPatient.HKDZ.IsNullOrWhiteSpace())
                patientInfo.Address = hisPatient.HKDZ;
            if (patientInfo.ContactsAddress.IsNullOrWhiteSpace() && !hisPatient.HKDZ.IsNullOrWhiteSpace())
                patientInfo.ContactsAddress = hisPatient.HKDZ;
            // 就诊状态
            patientInfo.VisitStatus = visitStatus;
            // 就诊号在分诊的时候，从推送分诊信息接口获取，此处不需要再同步视图的就诊号（视图的就诊号可能不存在）
            //// 就诊号
            //if (string.IsNullOrEmpty(patientInfo.VisitNo))
            //{
            //    patientInfo.VisitNo = hisPatient.VisitNo;
            //}
            //// 限制患者基本信息为只读
            //patientInfo.IsBasicInfoReadOnly = true;
            //// 当从外部获取新冠问卷时，不在ECIS中录入个人史信息
            //patientInfo.IsCovidExamFromOuterSystem = true;

            // 通过队列号查询对应的科室/诊室
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.QueueId);

            var hisRegisterAdapter = hisPatient.BuildAdapter()
                    .ForkConfig(forked => forked.ForType<HisRegisterPatient, RegisterInfo>()
                    .Map(dest => dest.RegisterDeptCode, src => src.DepartCode)
                    .Map(dest => dest.RegisterDoctorCode, src => src.DoctorCode));
            var existsRegisterInfo =
                patientInfo.RegisterInfo.FirstOrDefault(x => x.RegisterNo == hisPatient.RegisterNo);
            hisRegisterAdapter.AdaptTo(existsRegisterInfo);
            if (existsRegisterInfo != null)
            {
                existsRegisterInfo.RegisterDeptCode = hisPatient.DepartCode;
                existsRegisterInfo.RegisterDoctorCode = hisPatient.DoctorCode;
                existsRegisterInfo.RegisterDoctorName = hisPatient.DoctorName;

                // 是否退号
                existsRegisterInfo.DeleteUser = hisPatient.RefundStatus == 1 ? _currentUser?.UserName : null;
                existsRegisterInfo.IsCancelled = hisPatient.RefundStatus == 1;
                existsRegisterInfo.CancellationTime = hisPatient.RefundStatus == 1 ? (DateTime?)DateTime.Now : null;
            }

            if (!hisPatient.TriageLevel.HasValue)
                return patientInfo;

            // 如果有分诊级别就是已分诊，添加分诊信息，分诊级别
            var triageLevelConfig = dicts[TriageDict.TriageLevel.ToString()]
                .FirstOrDefault(x => x.TriageConfigCode == $"TriageLevel_{hisPatient.TriageLevel:D3}");
            // 当预检分诊中没有分诊信息或者没有分诊级别，则使用HIS的分诊信息跟分诊级别，并且标识为已分诊
            if (triageLevelConfig != null && string.IsNullOrWhiteSpace(patientInfo.ConsequenceInfo?.ActTriageLevel))
            {
                // 修改实际分诊级别
                patientInfo.ConsequenceInfo.ActTriageLevel = triageLevelConfig.TriageConfigCode;
                patientInfo.ConsequenceInfo.ActTriageLevelName = triageLevelConfig.TriageConfigName;

                patientInfo.TriageStatus = 1;
                // 如果没有分诊时间，同步分诊时间（因为挂号列表视图并未提供，故而设置为当前时间）
                patientInfo.TriageTime ??= DateTime.Now;
                // 如果没有排队号，同步排队号（因为挂号列表视图并未提供排队号，故而排队号设置为“暂无”）
                patientInfo.CallingSn ??= "暂无";
            }

            // 有分诊级别的情况下才同步分诊队列（科室）
            if (!string.IsNullOrEmpty(hisPatient.QueueId) && deptConfig != null)
            {
                // 修改分诊科室
                patientInfo.ConsequenceInfo.TriageDeptCode = deptConfig.TriageConfigCode;
                patientInfo.ConsequenceInfo.TriageDeptName = deptConfig.TriageConfigName;
            }

            return patientInfo;
        }

        /// <summary>
        /// 根据输入的PatientInfo和HisRegisterPatient信息，获得VisitStatus
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="hisPatient"></param>
        /// <param name="triageStatus"></param>
        /// <returns></returns>
        private VisitStatus GetVisitStatus(Dictionary<string, List<TriageConfigDto>> dicts,
            HisRegisterPatient hisPatient, int triageStatus, PatientInfo patientInfo = null)
        {
            VisitStatus visitStatus;
            // 通过队列号查询对应的科室/诊室
            //var deptConfig = dicts[TriageDict.TriageDepartment.ToString()]
            //    .FirstOrDefault(x => x.HisConfigCode == hisPatient.QueueId);
            if (hisPatient.JZZT == 9)
            {
                // 二次分诊患者，由于其HIS还是结束就诊状态，而系统状态是待就诊状态
                // 需要避免HIS覆盖系统原待就诊状态
                if (patientInfo != null &&
                    patientInfo.EndTime != null &&
                    patientInfo.VisitStatus == VisitStatus.WattingTreat)
                {
                    _log.LogTrace($"获得可能二次分诊患者：其患者名为:{patientInfo.PatientId}," +
                                        $"其患者名为:{patientInfo.PatientName}," +
                                        $"hisPatient.JZZT:{hisPatient.JZZT}," +
                                        $"patientInfo.VisitStatus:{patientInfo.VisitStatus}," +
                                        $"hisPatient.EndTime:{hisPatient.EndTime}," +
                                        $"patientInfo.EndTime:{patientInfo.EndTime},"
                                        );
                    visitStatus = VisitStatus.WattingTreat;
                    // 假如HIS的结束时间比系统的更晚（至少30秒），证明二次分诊也已结束就诊，那就需要把状态设为结束就诊状态
                    if (hisPatient.EndTime > patientInfo.EndTime + TimeSpan.FromSeconds(30))
                    {
                        visitStatus = VisitStatus.Treated;
                    }
                }
                else
                {
                    visitStatus = VisitStatus.Treated;
                }
            }
            else if (hisPatient.JZZT == 1)
            {
                // 正在就诊
                visitStatus = VisitStatus.Treating;
            }
            else if (!string.IsNullOrEmpty(hisPatient.QueueId)
                        //&& deptConfig != null
                        && hisPatient.TriageLevel.HasValue
                        && hisPatient.TriageLevel.Value > 0)
            {
                // 如有有分诊等级跟分诊科室，则是已分诊
                visitStatus = VisitStatus.WattingTreat;
            }
            else
            {
                // 如果HIS挂号列表没有状态，则使用预检分诊原有状态
                visitStatus = triageStatus == 1 ? VisitStatus.WattingTreat : VisitStatus.NotTriageYet;
            }
            //else if (!string.IsNullOrEmpty(hisPatient.QueueId) && hisPatient.TriageLevel.HasValue && hisPatient.TriageLevel.Value > 0 && deptConfig != null)
            //{
            //    visitStatus = VisitStatus.WattingTreat;
            //}
            //if (hisPatient.EndTime.HasValue)
            //{
            //    visitStatus = VisitStatus.Treated;
            //}
            //else if (hisPatient.BeginTime.HasValue)
            //{
            //    visitStatus = VisitStatus.Treating;
            //}
            //else if (!string.IsNullOrEmpty(hisPatient.QueueId) && hisPatient.TriageLevel.HasValue && hisPatient.TriageLevel.Value > 0 && deptConfig != null)
            //{
            //    visitStatus = VisitStatus.WattingTreat;
            //}

            return visitStatus;
        }

        /// <summary>
        /// 获取北大 HIS 急诊科挂号患者列表，直接从北大数据库视图中获取
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<HisRegisterPatient>> GetHisPatientInfoAsync()
        {
            _log.LogTrace("开始获取北大 HIS 急诊科挂号患者列表，直接从北大数据库视图中获取");
            Stopwatch stopwatch = Stopwatch.StartNew();

            string sql = @" Select PatientID, PatientName, GHXH RegisterNo, mzhm VisitNo, Sex, DateOfBirth, GHSJ RegisterTime, DLID QueueId, KSDM DepartCode,
                            YSDM DoctorCode, KSSJ BeginTime, JSSJ EndTime, JZZT, THBZ RefundStatus, SFZH IdentityNo, BRXZ ChargeType, XZMC ChargeTypeName, 
                            JTDH ContactsPhone, FZJB TriageLevel, HKDZ, YGXM DoctorName,JZHM InvoiceNum,ZDMC ExtendField1,
                            PATN_ID PatnId, CURR_MDTRT_ID CurrMDTRTId, POOLAREA PoolArea,Insutype InsureType,out_setl_flag OutSetlFlag
                            from v_jhjk_hzlb 
                            Where GHSJ >= DATEADD(Day, -1, GetDate())
                        ";
            var connectionStringKey = _configuration.GetConnectionString("PekingUniversityHIS");
            var hisPatientInfos = await this._dapperRepository.QueryListAsync<HisRegisterPatient>(sql,
                dbKey: "PekingUniversityHIS", connectionStringKey: connectionStringKey);

            stopwatch.Stop();
            _log.LogTrace($"由北大数据库视图中获得急诊科挂号患者列表数量：{hisPatientInfos.Count()}，共耗时：{stopwatch.ElapsedMilliseconds}");

            return hisPatientInfos;
        }

        /// <summary>
        /// 根据患者ID列表，发送更新消息到MQ，以便后续Patient更新患者信息
        /// </summary>
        /// <param name="patientIDList"></param>
        /// <returns></returns>
        private async Task<bool> SendUpdateMQ(HashSet<Guid> patientIDList, Dictionary<string, List<TriageConfigDto>> dicts)
        {
            using var uow = this._unitOfWorkManager.Begin();

            List<PatientInfo> patientsLIst =
                    await this._patientInfoRepository
                        .Include(c => c.RegisterInfo)
                        .Include(c => c.ConsequenceInfo)
                        .Include(c => c.VitalSignInfo)
                        .Include(c => c.AdmissionInfo)
                        .Include(c => c.ScoreInfo)
                        .Where(p => patientIDList.Contains(p.Id))
                        .Where(p => p.VisitStatus != VisitStatus.NotTriageYet) //未分诊的不推送
                        .ToListAsync();
            var triageDirectionList = dicts[TriageDict.TriageDirection.ToString()]
                                        .Where(x => x.IsDisable == 1 && x.IsDeleted == 0);

            foreach (PatientInfo patient in patientsLIst)
            {
                if (patient.RegisterInfo?.FirstOrDefault()?.IsCancelled == true)
                {
                    _log.LogDebug($"定时同步HIS数据，发现患者-{patient.PatientName}-，获得退号患者，其PatientInfo对象为{JsonSerializer.Serialize(patient, options)}");
                }

                var patientMqDto = new PatientInfoMqDto
                {
                    PatientInfo = patient.BuildAdapter().AdaptToType<PatientInfoDto>(),
                    RegisterInfo = patient.RegisterInfo?.FirstOrDefault()?.BuildAdapter().AdaptToType<RegisterInfoDto>(),
                    ConsequenceInfo = patient.ConsequenceInfo?.BuildAdapter().AdaptToType<ConsequenceInfoDto>(),
                    VitalSignInfo = patient.VitalSignInfo?.BuildAdapter().AdaptToType<VitalSignInfoDto>(),
                    AdmissionInfo = patient.AdmissionInfo?.BuildAdapter().AdaptToType<AdmissionInfoDto>(),
                    ScoreInfo = patient.ScoreInfo?.BuildAdapter().AdaptToType<List<ScoreInfoDto>>()
                };

                // 设置分诊去向
                // 假设在HIS挂号和分诊的数据，分诊去向为空，那么根据分诊科室名称，设置分诊去向
                if (string.IsNullOrEmpty(patientMqDto.ConsequenceInfo.TriageTarget) || string.IsNullOrEmpty(patientMqDto.ConsequenceInfo.TriageTargetName))
                {
                    string targetName = "急诊";
                    switch (patientMqDto.ConsequenceInfo.TriageDeptName)
                    {
                        case "急诊抢救室":
                            targetName = "抢救区";
                            break;
                        case "急诊观察区":
                            targetName = "留观区";
                            break;
                        default:
                            break;
                    }
                    patientMqDto.ConsequenceInfo.TriageTargetName = targetName;
                    patientMqDto.ConsequenceInfo.TriageTarget = triageDirectionList.Where(x => x.TriageConfigName == targetName).Select(x => x.TriageConfigCode).FirstOrDefault();
                }

                // 推送MQ消息到医生站
                _log.LogInformation($"定时同步HIS数据，发现患者-{patient.PatientName}-，将推送患者信息到MQ，该患者排队号为：{patient.CallingSn}");
                await _rabbitMqAppService.PublishEcisPatientSyncPatientAsync(new List<PatientInfoMqDto> { patientMqDto });
            }

            return true;
        }
        #endregion

        #region 获取挂号患者列表 及 相关方法
        /// <summary>
        /// 获取挂号患者列表
        /// 挂号超过24小时的不会被查询到结果中
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListAsync(GetRegisterPagedListInput input)
        {
            _log.LogDebug($"北大获取挂号患者列表，参数：{JsonSerializer.Serialize(input, options)}");
            Stopwatch stopwatch = Stopwatch.StartNew();

            var query = GetQuery(input); //当前科室所有数据（包括已就诊，已退号），主要为了页面的统计信息
            var deptAllList = await query.AsNoTracking().ToListAsync();
            var deptAviList = deptAllList.WhereIf(!string.IsNullOrEmpty(input.SearchText),
                                                    x => x.PatientName.Contains(input.SearchText)
                                                 || x.CallingSn.Contains(input.SearchText)
                                                 || x.ContactsPhone.Contains(input.SearchText))
                .Where(x => x.VisitStatus == VisitStatus.NotTriageYet
                         || x.VisitStatus == VisitStatus.WattingTreat
                         || x.VisitStatus == VisitStatus.Treating
                         || x.VisitStatus == VisitStatus.Suspend)
                .Where(patient => !patient.RegisterInfo.OrderByDescending(y => y.RegisterTime).First().IsCancelled) //非退号
                ;
            long spanGetListOrg = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            foreach (var item in deptAviList)
            {
                // 假如没有首诊医生，则显示就诊医生为分诊时设置的就诊医生
                if (string.IsNullOrEmpty(item.DoctorName) &&
                     item.ConsequenceInfo != null &&
                     !string.IsNullOrEmpty(item.ConsequenceInfo.DoctorCode))
                {
                    item.DoctorName = item.ConsequenceInfo.DoctorName;
                    _log.LogDebug($"患者-{item.PatientName}-,ID:{item.Id},其首诊医生为空" +
                        $"，使用分诊记录医生-{item.ConsequenceInfo.DoctorCode}:{item.ConsequenceInfo.DoctorName}-为其诊断医生，用户数据为：{JsonSerializer.Serialize(item, options)}");
                }
                // 假如是二次分诊患者，医生名称不显示
                if (item.VisitStatus == VisitStatus.WattingTreat && item.EndTime.HasValue)
                {
                    item.DoctorName = "";
                }
            }

            long spanSetStatusAndDoctor = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            #region 患者排序
            int outValue = int.MaxValue;
            var orderedList = deptAviList.OrderByDescending(x =>
            {
                var retVal = 0;
                switch (x.VisitStatus)
                {
                    case VisitStatus.NotTriageYet:
                        retVal = -1;
                        break;
                    case VisitStatus.Treated:
                        retVal = -3;
                        break;
                    case VisitStatus.Suspend:
                        retVal = 0;
                        break;
                    default:
                        retVal = 1;
                        break;
                }
                return retVal;
            })
             .ThenBy(x => x.TriageStatus == 0 ? "" : x.ConsequenceInfo.ActTriageLevel) // 分诊等级排序，暂存0，其他按照实际分诊级别排序
             .ThenByDescending(x => x.VisitStatus) // 就诊状态倒叙排序
             .ThenBy(x => x.TriageTime?.Date)
             .ThenBy(x =>
             {
                 outValue = int.MaxValue;
                 if (string.IsNullOrEmpty(x.CallingSn))
                     return outValue;
                 else if (x.CallingSn == "暂无")
                     return outValue;
                 else
                 {
                     int.TryParse(x.CallingSn.Substring(1), out outValue);
                     return outValue;
                 }
             }) // 队列号升序排序，解决转科室患者重排后由于分诊时间在前但是实际队列号在后所造成的排序问题
             .ThenBy(x => x.TriageTime) // 分诊时间升序排序
             ;
            if (input.DeptCode == "未分诊")
            {
                // 挂号时间排序，非分诊队列需要按降序排序
                orderedList = orderedList.ThenByDescending(x => x.RegisterInfo.Max(y => y.RegisterTime));
            }
            else
            {
                // 挂号时间排序，越早排序越前
                orderedList = orderedList.ThenBy(x => x.RegisterInfo.Max(y => y.RegisterTime));
            }
            #endregion

            // var covidExams = await this.Covid19ExamRepository.Where(x => list.Select(y => y.PatientId).Contains(x.PatientId)).ToListAsync();
            long spanOrderBy = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            RegisterPatientInfoResultDto result = new RegisterPatientInfoResultDto();
            result.Items = orderedList
                .Skip((input.Index - 1) * input.PageSize).Take(input.PageSize)
                .BuildAdapter().AdaptToType<List<RegisterPatientInfoDto>>();
            long spanAdaptToDtos = stopwatch.StopThenGetElapsedMillisecondsThenRestart();
            var deptGroupList = orderedList.GroupBy(x => x.ConsequenceInfo.TriageDeptCode);
            foreach (RegisterPatientInfoDto item in result.Items)
            {
                // item.HasFinishedCovid19Exam = covidExams.Any(x => x.PatientId == item.PatientId);
                item.HasFinishedCovid19Exam = false;
                if (string.IsNullOrEmpty(item.TriageDeptCode))
                {
                    item.WaittingForNumber = 0;
                }
                else
                {
                    var patientInfo = orderedList.FirstOrDefault(x => x.Id == item.TriagePatientInfoId);
                    item.WaittingForNumber = deptGroupList.Where(group => group.Key == item.TriageDeptCode)
                                                          .SelectMany(group => group)
                                                          .IndexOf(patientInfo);
                }
                item.RegisterNo = item.RegisterInfo.Count() > 0 ? item.RegisterInfo.FirstOrDefault().RegisterNo : null;
                item.RegisterTime = item.RegisterInfo.Count() > 0
                    ? (DateTime?)item.RegisterInfo.FirstOrDefault().RegisterTime
                    : null;

                // 特殊处理，如果时3级患者且前面等候人数为0，则前面等候人数为1
                // 这是为了避免3级患者来了发现前面等候人数为0，立即去科室，结果发现前面还有患者，导致患者不满意
                if (item.WaittingForNumber == 0 && item.ActTriageLevel == "TriageLevel_003")
                {
                    item.WaittingForNumber = 1;
                }
            }
            long spanForeachDtos = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 分页数据总数
            result.TotalCount = orderedList.Count();
            // 挂号总数包含已就诊的患者
            result.TotalRegisterCount = deptAllList.Count();
            result.TotalRegisterRefundCount = deptAllList.Where(x => IsRefundRegister(x)).Count(); // 查询挂号记录是否退号（只查询最近的一条挂号记录）
            result.TotalTreatedCount = deptAllList.Where(x => x.VisitStatus == VisitStatus.Treated).Count();
            result.TotalTreatingCount = orderedList.Where(x => x.VisitStatus == VisitStatus.Treating).Count();
            result.TotalWaitingCount = orderedList.Where(x => x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Suspend).Count();
            long spanSetReturnInfo = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 检查患者列表信息是否有问题
            CheckPatientInfoList(orderedList, input);
            long spanCheckPatientInfoList = stopwatch.StopThenGetElapsedMillisecondsThenRestart();
            _log.LogInformation($"获取挂号患者列表：GetRegisterPagedListAsync\n" +
                            $"获取挂号原始列表：{spanGetListOrg}ms，\n" +
                            $"特殊设置状态和医生名：{spanSetStatusAndDoctor}ms\n" +
                            $"挂号列表进行排序形成新列表：{spanOrderBy}ms\n" +
                            $"转换成Dto列表：{spanAdaptToDtos}ms\n" +
                            $"Foreach Dto进行数据设置：{spanForeachDtos}ms\n" +
                            $"设置返回对象数据：{spanSetReturnInfo}ms\n" +
                            $"检查患者列表信息是否有问题：{spanCheckPatientInfoList}\n" +
                            $"共耗时：{spanGetListOrg + spanSetStatusAndDoctor + spanOrderBy + spanAdaptToDtos + spanForeachDtos + spanSetReturnInfo + spanCheckPatientInfoList}");
            return JsonResult<RegisterPatientInfoResultDto>.Ok(data: result);
        }

        private IQueryable<PatientInfo> GetQuery(GetRegisterPagedListInput input)
        {
            return _patientInfoRepository.Include(x => x.RegisterInfo)
                                         .Include(x => x.ConsequenceInfo)
                                         .Include(x => x.VitalSignInfo)
                                         // 只查询挂号时间在24小时以内的
                                         .Where(x => x.RegisterInfo.Any(x => x.RegisterTime >= DateTime.Now.AddDays(-1)))
                                         // 查询指定科室
                                         .WhereIf(!string.IsNullOrEmpty(input.DeptCode) && input.DeptCode != "全部患者" && input.DeptCode != "未分诊",
                                             x => x.ConsequenceInfo.TriageDeptCode == input.DeptCode.Trim())
                                         // 科室为未分诊，查询未分诊和暂停的患者
                                         .WhereIf(input.DeptCode == "未分诊",
                                             x => x.VisitStatus == VisitStatus.NotTriageYet)
                                         .Where(x => x.IsDeleted == false&& string.IsNullOrWhiteSpace(x.TransferArea))
                                         ;
        }

        /// <summary>
        /// 是否退号的判断
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private bool IsRefundRegister(PatientInfo patient)
        {
            return patient.RegisterInfo.Count() > 0 &&
                   patient.RegisterInfo.OrderByDescending(y => y.RegisterTime).First().IsCancelled;
        }

        /// <summary>
        /// 检查数据，当遇到可能有问题数据时，记入log
        /// </summary>
        /// <param name="list">患者列表</param>
        /// <param name="input">科室</param>
        /// <returns></returns>
        private bool CheckPatientInfoList(IEnumerable<PatientInfo> list, GetRegisterPagedListInput input)
        {
            bool isPass = true;
            foreach (PatientInfo item in list)
            {
                if (item.EndTime != null)
                {
                    isPass = false;
                    //_log.LogWarning($"发现需注意数据：患者{item.Id}, {item.PatientId}，{item.PatientName}的结束就诊时间不为空");
                    _log.LogDebug($"发现需注意数据：患者{item.Id}, {item.PatientId}，{item.PatientName}的结束就诊时间不为空，其数据为：{JsonSerializer.Serialize(item, options)}");
                }

                if (item.TriageTime == null && !(input.DeptCode == "未分诊" || input.DeptCode == "全部患者"))
                {
                    isPass = false;
                    //_log.LogWarning($"发现可能异常数据：患者{item.Id}, {item.PatientId}，{item.PatientName}的分诊时间为空，且非未分诊或全部患者");
                    _log.LogDebug($"发现可能异常数据：患者{item.Id}, {item.PatientId}，{item.PatientName}的分诊时间为空，且非未分诊或全部患者，其数据为：{JsonSerializer.Serialize(item, options)}");
                }
            }
            return isPass;
        }
        #endregion

        #region 获取挂号患者列表 及 相关方法，对接叫号系统版本
        /// <summary>
        /// 获取挂号患者列表
        /// 挂号超过24小时的不会被查询到结果中
        /// 排序列表由叫号系统提供
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<JsonResult<RegisterPatientInfoResultDto>> GetRegisterPagedListV2Async(GetRegisterPagedListInput input)
        {
            _log.LogDebug($"北大获取挂号患者列表V2，参数：{JsonSerializer.Serialize(input, options)}");
            Stopwatch stopwatch = Stopwatch.StartNew();

            var query = GetQuery(input); //当前科室所有数据（包括已就诊，已退号），主要为了页面的统计信息

            var deptAllList = await query.AsNoTracking().ToListAsync();
            var deptCurrentList = deptAllList.WhereIf(!string.IsNullOrEmpty(input.SearchText),
                                                        x => x.PatientName.Contains(input.SearchText)
                                                     || (x.CallingSn ?? "").Contains(input.SearchText)
                                                     || (x.ContactsPhone ?? "").Contains(input.SearchText))
                                            .Where(x => x.VisitStatus == VisitStatus.NotTriageYet
                                                     || x.VisitStatus == VisitStatus.WattingTreat
                                                     || x.VisitStatus == VisitStatus.Treating
                                                     || x.VisitStatus == VisitStatus.Suspend)
                                            .Where(patient => !patient.RegisterInfo.OrderByDescending(y => y.RegisterTime).First().IsCancelled) //非退号
                                            ;
            long spanGetListOrg = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            foreach (var item in deptCurrentList)
            {
                // 假如没有首诊医生，则显示就诊医生为分诊时设置的就诊医生
                if (string.IsNullOrEmpty(item.DoctorName) &&
                     item.ConsequenceInfo != null &&
                     !string.IsNullOrEmpty(item.ConsequenceInfo.DoctorCode))
                {
                    item.DoctorName = item.ConsequenceInfo.DoctorName;
                    _log.LogDebug($"患者-{item.PatientName}-,ID:{item.Id},其首诊医生为空" +
                        $"，使用分诊记录医生-{item.ConsequenceInfo.DoctorCode}:{item.ConsequenceInfo.DoctorName}-为其诊断医生，用户数据为：{JsonSerializer.Serialize(item, options)}");
                }
            }

            long spanSetStatusAndDoctor = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            #region 患者排序
            //由叫号系统获得排序好的患者列表，再结合预检分诊列表进行排序
            List<CallPatientInfo> callPatientInfos = await _callApi.GetOrderListFromCallAsync(input.DeptCode, pageSize: 100);
            // ToDo: 根据 callPatientInfos 结合预检分诊列表进行排序
            List<PatientInfo> orderedList = HandledOrderList(callPatientInfos, deptCurrentList);
            #endregion

            long spanOrderBy = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            RegisterPatientInfoResultDto result = new RegisterPatientInfoResultDto();
            result.Items = orderedList
                .Skip((input.Index - 1) * input.PageSize).Take(input.PageSize)
                .BuildAdapter().AdaptToType<List<RegisterPatientInfoDto>>();
            long spanAdaptToDtos = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            var deptGroupList = orderedList.GroupBy(x => x.ConsequenceInfo.TriageDeptCode);
            foreach (RegisterPatientInfoDto item in result.Items)
            {
                item.HasFinishedCovid19Exam = false;

                if (string.IsNullOrEmpty(item.TriageDeptCode))
                {
                    item.WaittingForNumber = 0;
                }
                else
                {
                    var patientInfo = orderedList.FirstOrDefault(x => x.Id == item.TriagePatientInfoId);
                    item.WaittingForNumber = deptGroupList.Where(group => group.Key == item.TriageDeptCode)
                                                          .SelectMany(group => group)
                                                          .IndexOf(patientInfo);
                }

                item.RegisterNo = item.RegisterInfo.Count() > 0 ? item.RegisterInfo.FirstOrDefault().RegisterNo : null;
                item.RegisterTime = item.RegisterInfo.Count() > 0
                    ? (DateTime?)item.RegisterInfo.FirstOrDefault().RegisterTime
                    : null;

                // 特殊处理，如果时3级患者且前面等候人数为0，则前面等候人数为1
                // 这是为了避免3级患者来了发现前面等候人数为0，立即去科室，结果发现前面还有患者，导致患者不满意
                if (item.WaittingForNumber == 0 && item.ActTriageLevel == "TriageLevel_003")
                {
                    item.WaittingForNumber = 1;
                }
            }
            long spanForeachDtos = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 分页数据总数
            result.TotalCount = orderedList.Count();
            // 挂号总数包含已就诊的患者
            result.TotalRegisterCount = deptAllList.Count();
            result.TotalRegisterRefundCount = deptAllList.Where(x => IsRefundRegister(x)).Count(); // 查询挂号记录是否退号（只查询最近的一条挂号记录）
            result.TotalTreatedCount = deptAllList.Where(x => x.VisitStatus == VisitStatus.Treated).Count();
            result.TotalTreatingCount = orderedList.Where(x => x.VisitStatus == VisitStatus.Treating).Count();
            result.TotalWaitingCount = orderedList.Where(x => x.VisitStatus == VisitStatus.WattingTreat || x.VisitStatus == VisitStatus.Suspend).Count();
            long spanSetReturnInfo = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 检查患者列表信息是否有问题
            CheckPatientInfoList(orderedList, input);
            long spanCheckPatientInfoList = stopwatch.StopThenGetElapsedMillisecondsThenRestart();
            _log.LogInformation($"获取挂号患者列表：GetRegisterPagedListAsync\n" +
                            $"获取挂号原始列表：{spanGetListOrg}ms，\n" +
                            $"特殊设置状态和医生名：{spanSetStatusAndDoctor}ms\n" +
                            $"挂号列表进行排序形成新列表：{spanOrderBy}ms\n" +
                            $"转换成Dto列表：{spanAdaptToDtos}ms\n" +
                            $"Foreach Dto进行数据设置：{spanForeachDtos}ms\n" +
                            $"设置返回对象数据：{spanSetReturnInfo}ms\n" +
                            $"检查患者列表信息是否有问题：{spanCheckPatientInfoList}\n" +
                            $"共耗时：{spanGetListOrg + spanSetStatusAndDoctor + spanOrderBy + spanAdaptToDtos + spanForeachDtos + spanSetReturnInfo + spanCheckPatientInfoList}");
            return JsonResult<RegisterPatientInfoResultDto>.Ok(data: result);
        }

        /// <summary>
        /// 根据 callPatientInfo 结合预检分诊列表进行排序
        /// </summary>
        /// <param name="callPatientInfos"></param>
        /// <param name="deptCurrentList"></param>
        /// <returns></returns>
        private List<PatientInfo> HandledOrderList(List<CallPatientInfo> callPatientInfos, IEnumerable<PatientInfo> deptCurrentList)
        {
            List<PatientInfo> orderedList = new List<PatientInfo>();
            IEnumerable<RegisterInfo> registerInfos = deptCurrentList.SelectMany(x => x.RegisterInfo);
            foreach (CallPatientInfo callPatientInfo in callPatientInfos)
            {
                RegisterInfo registerInfo = registerInfos.FirstOrDefault(x => x.RegisterNo == callPatientInfo.RegisterNo);
                if (registerInfo == null) continue;
                PatientInfo current = deptCurrentList.FirstOrDefault(x => x.Id == registerInfo.PatientInfoId);
                if (current != null)
                {
                    orderedList.Add(current);
                }
            }
            orderedList.Reverse();
            //IEnumerable<PatientInfo> treatingList = deptCurrentList.Where(x => x.VisitStatus == VisitStatus.Treating);
            //int outValue = int.MaxValue;
            //treatingList.OrderBy(x => x.TriageStatus == 0 ? "" : x.ConsequenceInfo.ActTriageLevel)
            //    .ThenBy(x => x.TriageTime?.Date)
            //    .ThenBy(x =>
            //    {
            //        outValue = int.MaxValue;
            //        if (string.IsNullOrEmpty(x.CallingSn))
            //            return outValue;
            //        else if (x.CallingSn == "暂无")
            //            return outValue;
            //        else
            //        {
            //            int.TryParse(x.CallingSn.Substring(1), out outValue);
            //            return outValue;
            //        }
            //    })
            // .ThenBy(x => x.TriageTime);
            //treatingList = treatingList.Reverse();
            //foreach (PatientInfo item in treatingList)
            //{
            ///  orderedList.Prepend(item);
            //}

            IEnumerable<PatientInfo> notTriageYetList = deptCurrentList
                .Where(x => !orderedList.Select(y => y.Id).Contains(x.Id))
                .Where(x => x.VisitStatus == VisitStatus.NotTriageYet);
            notTriageYetList = notTriageYetList.OrderByDescending(x => x.RegisterInfo.Max(y => y.RegisterTime));
            orderedList.AddRange(notTriageYetList);

            return orderedList;
        }
        #endregion

        #region 同步HIS挂号列表 及 相关方法，对接叫号系统版本
        /// <summary>
        /// 同步 HIS 挂号列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> SyncRegisterPatientFromHisV2()
        {
            _log.LogDebug($"同步HIS挂号列表开始");
            Stopwatch swTotal = Stopwatch.StartNew();

            using var uow = this._unitOfWorkManager.Begin();

            Stopwatch stopwatch = Stopwatch.StartNew();
            var dicts = await _triageConfigService.GetTriageConfigByRedisAsync(isEnable: -1, isDeleted: -1);
            long spanGetDicts = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 获取北大 HIS 急诊科挂号患者列表，直接从北大数据库视图中获取
            IEnumerable<HisRegisterPatient> hisPatientInfos = await this.GetHisPatientInfoV2Async();
            long spanGetHisPatientInfos = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            // 通过对比his与缓存与数据库中的数据 获取挂号列表新增、修改的患者数据
            var (newPatients, updatePatients) = await this.GetHisNewAndUpdatePatientsV2(hisPatientInfos);
            long spanGetAddAndUpdateList = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

            HashSet<Guid> mqPatientIDList = new HashSet<Guid>(); //需要发送MQ的患者ID列表，使用HashSet以去重
            HashSet<string> refundRegisterNoList = new HashSet<string>(); //退号的患者挂号序号列表，使用HashSet以去重

            try
            {
                // 预检分诊数据库一天内的患者
                var oneDayPatients = await this._patientInfoRepository
                    .AsNoTracking()
                    .Include(c => c.ConsequenceInfo)
                    .Include(c => c.VitalSignInfo)
                    .Include(c => c.RegisterInfo)
                    .Include(c => c.AdmissionInfo)
                    .Include(c => c.ScoreInfo)
                    .Where(x => x.RegisterInfo.Any(y => y.RegisterTime >= DateTime.Now.AddDays(-1)))
                    .ToListAsync();
                long spanGetOneDayPatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                // 新增患者
                foreach (var hisNewPatient in newPatients)
                {
                    // 再次验证该新增患者 registerNo 是否已存在数据库，存在就把该患者移动到 updatePatients
                    if (oneDayPatients.Any(x => x.RegisterInfo?.Any(y => hisNewPatient.RegisterNo == y.RegisterNo) == true))
                    {
                        _log.LogDebug($"同步HIS数据，在新增患者前发现该患者: {hisNewPatient.PatientName} registerNo:{hisNewPatient.RegisterNo} 已在数据库存在，将其移动到 updatePatients 列表");
                        updatePatients.Append(hisNewPatient);
                        break;
                    }
                    var patientInfo = NewPatientInfoV2(dicts, hisNewPatient);
                    _log.LogDebug($"同步HIS数据，最终需要新增患者-{patientInfo.PatientName}-信息：{JsonSerializer.Serialize(patientInfo, options)}");
                    await this._patientInfoRepository.InsertAsync(patientInfo);
                    if (patientInfo.VisitStatus != VisitStatus.NotTriageYet)
                        mqPatientIDList.Add(patientInfo.Id);
                }
                long spanInsertNewPatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                // 预检分诊、HIS 都存在的患者
                var bothExistsPatients = oneDayPatients.Where(x => x.RegisterInfo.Any(y => hisPatientInfos.Select(_ => _.RegisterNo).Contains(y.RegisterNo)));
                //更新患者
                foreach (var hisPatient in updatePatients)
                {
                    var existsPatient = bothExistsPatients.FirstOrDefault(x =>
                                                            x.PatientId == hisPatient.PatientId &&
                                                            x.RegisterInfo.Any(y => y.RegisterNo == hisPatient.RegisterNo));
                    if (existsPatient != null)
                    {
                        existsPatient = MergePatientInfoV2(existsPatient, dicts, hisPatient);
                        _log.LogDebug($"同步HIS数据，最终需要更新患者-{existsPatient.PatientName}-信息：{JsonSerializer.Serialize(existsPatient, options)}");

                        bool IsDoUpdate = await this._patientInfoRepository.UpdateRecordAsync(existsPatient);
                        if (IsDoUpdate)
                        {
                            mqPatientIDList.Add(existsPatient.Id);
                            if (existsPatient.RegisterInfo.FirstOrDefault()?.IsCancelled == true)
                            {
                                refundRegisterNoList.Add(existsPatient.RegisterInfo?.FirstOrDefault()?.RegisterNo);
                            }
                        }

                    }
                }
                long spanUpdatePatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                //// 在 HIS 挂号列表不存在的患者，需要标识成已过号（已就诊）
                //var expirePatients = oneDayPatients.Except(bothExistsPatients);
                //foreach (var item in expirePatients)
                //{
                //    if (item.VisitStatus != VisitStatus.Treated)
                //    {
                //        // HIS 挂号列表视图已经不存在（已经超过就诊时间），标识为已就诊（实际应该是已过号之类的状态）
                //        item.VisitStatus = VisitStatus.Treated;
                //        _log.LogDebug($"同步HIS数据，已超过就诊时间，设置患者-{item.PatientName}-为已就诊状态");
                //        bool IsDoUpdate = await this._patientInfoRepository.UpdateRecordAsync(item);
                //    }
                //}
                long spanExpirePatients = stopwatch.StopThenGetElapsedMillisecondsThenRestart();

                await uow.CompleteAsync();

                long spanUowCompleteAsync = stopwatch.StopThenGetElapsedMilliseconds();

                _log.LogDebug($"同步HIS挂号列表成功（），共耗时：{spanGetDicts + spanGetHisPatientInfos + spanGetAddAndUpdateList + spanInsertNewPatients + spanGetOneDayPatients + spanUpdatePatients + spanExpirePatients}，" +
                                    $"获得字典耗时：{spanGetDicts}，" +
                                    $"从北大数据库视图中获取数据耗时：{spanGetHisPatientInfos}，" +
                                    $"通过对比his与缓存与数据库中的数据 获取挂号列表新增、修改的患者数据耗时：{spanGetAddAndUpdateList}，" +
                                    $"新增患者耗时：{spanInsertNewPatients}，" +
                                    $"获得预检分诊数据库一天内的患者耗时：{spanGetOneDayPatients}，" +
                                    $"新增患者信息，耗时：{spanUpdatePatients}，" +
                                    $"标识已过号，耗时：{spanExpirePatients}，" +
                                    $"spanUowCompleteAsync，耗时：{spanUowCompleteAsync}");

            }
            catch (Exception)
            {
                _log.LogError("同步HIS挂号列表发生错误");
                await uow.RollbackAsync();
                throw;
            }

            swTotal.Stop();
            _log.LogTrace($"同步HIS挂号列表结束，共耗时：{swTotal.ElapsedMilliseconds}");

            // 发送MQ消息，通知Patient服务新增/更新患者信息
            if (mqPatientIDList.Count != 0)
            {
                await SendUpdateMQ(mqPatientIDList, dicts);
            }
            // 发送MQ消息，通知Call服务该患者已退号
            if (refundRegisterNoList.Count != 0)
            {
                await SendRefundPatientsToCall(refundRegisterNoList);
            }


            return JsonResult.Ok("同步成功.");
        }

        /// <summary>
        /// 获取北大 HIS 急诊科挂号患者列表，直接从北大数据库视图中获取
        /// 排除已就诊和就诊中患者
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<HisRegisterPatient>> GetHisPatientInfoV2Async()
        {
            _log.LogTrace("开始获取北大 HIS 急诊科挂号患者列表，直接从北大数据库视图中获取");
            Stopwatch stopwatch = Stopwatch.StartNew();

            string sql = @" Select PatientID, PatientName, GHXH RegisterNo, mzhm VisitNo, Sex, DateOfBirth, GHSJ RegisterTime, DLID QueueId, KSDM DepartCode, 
                            YSDM DoctorCode, KSSJ BeginTime, JSSJ EndTime, JZZT, THBZ RefundStatus, SFZH IdentityNo, BRXZ ChargeType, XZMC ChargeTypeName,
                            JTDH ContactsPhone, FZJB TriageLevel, HKDZ, YGXM DoctorName,JZHM InvoiceNum,ZDMC ExtendField1, 
                            PATN_ID PatnId, CURR_MDTRT_ID CurrMDTRTId, POOLAREA PoolArea,Insutype InsureType,out_setl_flag OutSetlFlag
                            from v_jhjk_hzlb 
                            Where GHSJ >= DATEADD(Day, -1, GetDate())
                            and (JZZT != 9 or JZZT IS NULL)
                        ";
            var connectionStringKey = _configuration.GetConnectionString("PekingUniversityHIS");
            var hisPatientInfos = await this._dapperRepository.QueryListAsync<HisRegisterPatient>
                (sql, dbKey: "PekingUniversityHIS", connectionStringKey: connectionStringKey);

            stopwatch.Stop();
            _log.LogTrace($"由北大数据库视图中获得急诊科挂号患者列表数量：{hisPatientInfos.Count()}，共耗时：{stopwatch.ElapsedMilliseconds}");
            return hisPatientInfos;
        }

        /// <summary>
        /// 获取挂号列表新增、修改的患者数据
        /// 通过对比his与缓存与数据库中的数据
        /// 同步数据使用
        /// 对接叫号系统版本
        /// </summary>
        /// <param name="hisPatients"></param>
        /// <returns></returns>
        private async Task<(IEnumerable<HisRegisterPatient>, IEnumerable<HisRegisterPatient>)>
            GetHisNewAndUpdatePatientsV2(IEnumerable<HisRegisterPatient> hisPatients)
        {
            _log.LogInformation("同步HIS挂号列表，获取挂号列表新增、修改的患者数据方法GetHisNewAndUpdatePatientsV2开始");

            string cacheKey = $"{_configuration["ServiceName"]}:RegisterPatientsV2";
            IEnumerable<HisRegisterPatient> newHisPatients;
            IEnumerable<HisRegisterPatient> updateHisPatients;

            Stopwatch stopwatch = Stopwatch.StartNew();
            if (await _redis.KeyExistsAsync(cacheKey))
            {
                Stopwatch sw = Stopwatch.StartNew();

                // 从缓存获取上一次同步的数据
                RedisValue json = await _redis.StringGetAsync(cacheKey);
                List<HisRegisterPatient> patientRedisCache = json.HasValue ? JsonSerializer.Deserialize<List<HisRegisterPatient>>(json) : new List<HisRegisterPatient>();

                var spanGetFromCache = sw.StopThenGetElapsedMillisecondsThenRestart();

                // 缓存当前同步的数据
                await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(hisPatients));

                var spanDoCache = sw.StopThenGetElapsedMillisecondsThenRestart();

                //// 比对缓存的上一次同步的数据，拿到新增的患者列表
                //newHisPatients = hisPatients.Where(x =>
                //    !patientRedisCache.Any(y => x.PatientId == y.PatientId && x.RegisterNo == y.RegisterNo)).ToList();

                var spanGetNewPatients = sw.StopThenGetElapsedMillisecondsThenRestart();

                // 比对缓存的上一次同步的数据，拿到信息更新了的患者列表
                updateHisPatients = hisPatients.Where(x => patientRedisCache.Any(y =>
                    x.PatientId == y.PatientId && x.RegisterNo == y.RegisterNo
                    && (x.PatientName != y.PatientName
                     || x.HKDZ != y.HKDZ
                     || x.RefundStatus != y.RefundStatus
                     || x.ContactsPhone != y.ContactsPhone
                     || x.DateOfBirth != y.DateOfBirth)
                ));

                var spanGetUpdatePatients = sw.StopThenGetElapsedMillisecondsThenRestart();

                List<string> dbExistRegisterNo = new List<string>();

                {
                    // 只比对最近几天的数据，提升访问速度
                    var allDBPatientListSomeDays = await this._patientInfoRepository
                                                             .AsNoTracking()
                                                             .Include(c => c.RegisterInfo)
                                                             .Where(x => x.CreationTime >= DateTime.Now.AddDays(-2)) // 限定只查询最近几天的数据，提升访问速度
                                                             .ToListAsync();
                    var allDBPatientHisHasList = allDBPatientListSomeDays.AsParallel().Where(x => x.RegisterInfo.Any(
                                                                  y => hisPatients.Select(z => z.RegisterNo).Contains(y.RegisterNo))).ToList();
                    allDBPatientHisHasList.ForEach(x =>
                    {
                        dbExistRegisterNo.AddRange(x.RegisterInfo.Select(x => x.RegisterNo));
                    });
                }
                var spanGetPatientsFromDB = sw.StopThenGetElapsedMillisecondsThenRestart();

                // 比对缓存的上一次同步的数据，拿到新增的患者列表
                newHisPatients = hisPatients.Where(x => !dbExistRegisterNo.Any(y => y == x.RegisterNo)).ToList();

                _log.LogInformation($"同步HIS挂号列表：GetHisNewAndUpdatePatients方法有缓存分支，" +
                                            $"获取挂号列表新增、修改的患者数据结束，共耗时：{spanGetFromCache + spanDoCache + spanGetNewPatients + spanGetUpdatePatients + spanGetPatientsFromDB}ms，其中：，" +
                                            $"获得挂号列表缓存：{spanGetFromCache}ms，" +
                                            $"缓存当前同步的数据：{spanDoCache}ms，" +
                                            $"获得新增患者列表：{spanGetNewPatients}ms，" +
                                            $"获得修改患者列表：{spanGetUpdatePatients}ms，" +
                                            $"新增患者数量：{newHisPatients.Count()}，" +
                                            $"修改患者数量：{updateHisPatients.Count()}" +
                                            $"原redis缓存患者数量：{patientRedisCache.Count}" +
                                            $"由HIS同步获得的患者数量：{hisPatients.Count()}"
                                            );
            }
            else
            {
                _log.LogInformation($"同步HIS挂号列表：GetHisNewAndUpdatePatients方法无缓存分支");

                // 缓存当前同步的数据
                await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(hisPatients));
                // 保证没有缓存对比时，数据对比不出错
                var dbExistsPatients = await this._patientInfoRepository
                                                  .AsNoTracking()
                                                  .Include(c => c.RegisterInfo)
                                                  // 虽然此处 PatientInfo 与 RegisterInfo 是一对多关系，实际上在先挂号后分诊模式先，他们是一对一的关系  by: ywlin 2022-01-06
                                                  .Where(x => x.CreationTime >= DateTime.Now.AddDays(-3)) // 限定只查询最近几天的数据，提升访问速度
                                                  .Where(x => x.RegisterInfo.Any(y => hisPatients.Select(z => z.RegisterNo).Contains(y.RegisterNo)))
                                                  .ToListAsync();
                List<string> dbExistRegisterNo = new List<string>();
                dbExistsPatients.ForEach(x =>
                {
                    dbExistRegisterNo.AddRange(x.RegisterInfo.Select(x => x.RegisterNo));
                });
                // 更新的患者列表
                updateHisPatients = hisPatients.Where(x => dbExistRegisterNo.Any(y => y == x.RegisterNo));
                // 减去更新的患者列表，获得新增的
                newHisPatients = hisPatients.Except(updateHisPatients);
            }

            stopwatch.Stop();
            _log.LogInformation($"同步HIS挂号列表，获取挂号列表新增、修改的患者数据方法GetHisNewAndUpdatePatients结束，共耗时：{stopwatch.StopThenGetElapsedMilliseconds()}ms");

            return (newHisPatients, updateHisPatients);
        }

        /// <summary>
        /// HIS 患者信息转换成预检分诊患者信息
        /// 新增
        /// 初始化分诊结果信息、初始化入院情况信息
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="hisPatient"></param>
        /// <returns></returns>
        private PatientInfo NewPatientInfoV2(Dictionary<string, List<TriageConfigDto>> dicts,
            HisRegisterPatient hisPatient)
        {
            // 患者分诊状态（默认暂存，当预检分诊已存在患者信息时使用预检分诊现有状态）
            var patientInfo = new PatientInfo() { TriageStatus = 0, RegisterInfo = new List<RegisterInfo>() }
                .SetId(_guidGenerator.Create());

            // 费别
            var feeType = dicts[TriageDict.Faber.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.ChargeType);

            // 病患基本信息，HisRegisterPatient到PatientInfo的映射设置
            hisPatient.BuildAdapter()
                .ForkConfig(forked => forked.ForType<HisRegisterPatient, PatientInfo>()
                    .Map(dest => dest.Birthday, src => src.DateOfBirth)
                    .Map(dest => dest.Address, src => src.HKDZ)
                    .Map(dest => dest.ContactsAddress, src => src.HKDZ)
                    .Map(dest => dest.Sex,
                        src => src.Sex == 1 ? "Sex_Man" : (src.Sex == 2 ? "Sex_Woman" : "Sex_Unknown"))
                    .Map(dest => dest.SexName, src => src.Sex == 1 ? "男" : (src.Sex == 2 ? "女" : "未知"))
                    // 医保控费相关 S
                    .Map(dest => dest.PatnId, src => src.PatnId)
                    .Map(dest => dest.CurrMDTRTId, src => src.CurrMDTRTId)
                    .Map(dest => dest.PoolArea, src => src.PoolArea)
                    .Map(dest => dest.InsureType, src => src.InsureType)
                    .Map(dest => dest.OutSetlFlag, src => src.OutSetlFlag)
                    // 医保控费相关 E
                    .Ignore(dest => dest.IsTop)
                    .Ignore(dest => dest.IsUntreatedOver)
                    .Ignore(dest => dest.RegisterInfo))
                    .AdaptTo(patientInfo);

            patientInfo.VisitStatus = VisitStatus.NotTriageYet;

            // 费别
            patientInfo.ChargeType = feeType?.TriageConfigCode;
            patientInfo.ChargeTypeName = feeType?.TriageConfigName;

            // 就诊号
            if (string.IsNullOrEmpty(patientInfo.VisitNo))
            {
                patientInfo.VisitNo = hisPatient.VisitNo;
            }

            // 限制患者基本信息为只读
            patientInfo.IsBasicInfoReadOnly = true;
            // 当从外部获取新冠问卷时，不在ECIS中录入个人史信息
            patientInfo.IsCovidExamFromOuterSystem = true;

            // 通过队列号查询对应的科室/诊室
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.QueueId);

            // 挂号信息
            var registerInfo = new RegisterInfo(_guidGenerator.Create(), patientInfo.Id);

            var hisRegisterAdapter = hisPatient.BuildAdapter()
                .ForkConfig(forked => forked.ForType<HisRegisterPatient, RegisterInfo>()
                    .Map(dest => dest.RegisterDeptCode, src => src.DepartCode)
                    .Map(dest => dest.RegisterDoctorName, src => src.DoctorName)
                    .Map(dest => dest.RegisterDoctorCode, src => src.DoctorCode));
            registerInfo = hisRegisterAdapter.AdaptTo(registerInfo);
            registerInfo.RegisterDeptCode = deptConfig?.TriageConfigCode;
            patientInfo.RegisterInfo.Add(registerInfo);

            // 初始化分诊结果信息
            patientInfo.ConsequenceInfo ??= new ConsequenceInfo()
            {
                PatientInfoId = patientInfo.Id,
            }.SetId(_guidGenerator.Create());

            // 初始化入院情况信息
            patientInfo.AdmissionInfo ??= new AdmissionInfo()
            {
                PatientInfoId = patientInfo.Id,
            }
            .SetId((_guidGenerator.Create()));

            // 其它默认值
            // 1. 国籍
            if (dicts.Keys.Contains(TriageDict.Country.ToString()))
            {
                var countries = dicts[TriageDict.Country.ToString()].Where(x => x.IsDeleted == 0 && x.IsDisable > 0)
                    .OrderBy(x => x.Sort);
                var countryCode = patientInfo.PatientName.IsChinese() ? "Country_001" : "Country_005";
                var countryConfig = countries.FirstOrDefault(x => x.TriageConfigCode == countryCode);
                patientInfo.CountryCode = countryCode;
                patientInfo.CountryName = countryConfig?.TriageConfigName;
            }

            // 2. 人群
            if (dicts.Keys.Contains(TriageDict.Crowd.ToString()))
            {
                var crowds = dicts[TriageDict.Crowd.ToString()].Where(x => x.IsDeleted == 0 && x.IsDisable > 0)
                    .OrderBy(x => x.Sort);
                var crowd = crowds.FirstOrDefault();
                patientInfo.CrowdCode = crowd?.TriageConfigCode;
                patientInfo.CrowdName = crowd?.TriageConfigName;
            }

            // 3. 来院方式
            if (dicts.Keys.Contains(TriageDict.ToHospitalWay.ToString()))
            {
                var toHospitalWays = dicts[TriageDict.ToHospitalWay.ToString()]
                    .Where(x => x.IsDeleted == 0 && x.IsDisable > 0).OrderBy(x => x.Sort);
                // 默认来院方式为步行
                var toHospitalWay = toHospitalWays.FirstOrDefault(x => x.TriageConfigName == "步行");
                patientInfo.ToHospitalWayCode = toHospitalWay?.TriageConfigCode;
                patientInfo.ToHospitalWayName = toHospitalWay?.TriageConfigName;
            }

            // 4. 就诊类型
            if (dicts[TriageDict.TypeOfVisit.ToString()] != null)
            {
                var typeOfVisits = dicts[TriageDict.TypeOfVisit.ToString()]
                    .Where(x => x.IsDeleted == 0 && x.IsDisable > 0).OrderBy(x => x.Sort);
                var typeOfVisit = typeOfVisits.FirstOrDefault();
                patientInfo.TypeOfVisitCode = typeOfVisit?.TriageConfigCode;
                patientInfo.TypeOfVisitName = typeOfVisit?.TriageConfigName;
            }

            return patientInfo;
        }

        /// <summary>
        /// HIS 患者信息转换成预检分诊患者信息
        /// 合并更新
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="dicts"></param>
        /// <param name="hisPatient"></param>
        /// <returns></returns>
        private PatientInfo MergePatientInfoV2(PatientInfo patientInfo, Dictionary<string, List<TriageConfigDto>> dicts,
            HisRegisterPatient hisPatient)
        {
            // 费别
            var feeType = dicts[TriageDict.Faber.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.ChargeType);

            // 初始化分诊信息
            patientInfo.ConsequenceInfo ??= new ConsequenceInfo()
            {
                PatientInfoId = patientInfo.Id,
            }.SetId(_guidGenerator.Create());

            // 病患基本信息
            hisPatient.BuildAdapter()
                .ForkConfig(forked => forked.ForType<HisRegisterPatient, PatientInfo>()
                    .Map(dest => dest.Birthday, src => src.DateOfBirth)
                    .Map(dest => dest.Sex,
                        src => src.Sex == 1 ? "Sex_Man" : (src.Sex == 2 ? "Sex_Woman" : "Sex_Unknown"))
                    .Map(dest => dest.SexName, src => src.Sex == 1 ? "男" : (src.Sex == 2 ? "女" : "未知"))
                    // 医保控费相关 S
                    .Map(dest => dest.PatnId, src => src.PatnId)
                    .Map(dest => dest.CurrMDTRTId, src => src.CurrMDTRTId)
                    .Map(dest => dest.PoolArea, src => src.PoolArea)
                    .Map(dest => dest.InsureType, src => src.InsureType)
                    .Map(dest => dest.OutSetlFlag, src => src.OutSetlFlag)
                    // 医保控费相关 E
                    .Ignore(dest => dest.IsTop)
                    .Ignore(dest => dest.IsUntreatedOver)
                    .Ignore(dest => dest.RegisterInfo)
                    .Ignore(dest => dest.VisitNo))
                .AdaptTo(patientInfo);

            // 费别
            patientInfo.ChargeType = feeType?.TriageConfigCode;
            patientInfo.ChargeTypeName = feeType?.TriageConfigName;
            // 地址同步
            if (patientInfo.Address.IsNullOrWhiteSpace() && !hisPatient.HKDZ.IsNullOrWhiteSpace())
                patientInfo.Address = hisPatient.HKDZ;
            if (patientInfo.ContactsAddress.IsNullOrWhiteSpace() && !hisPatient.HKDZ.IsNullOrWhiteSpace())
                patientInfo.ContactsAddress = hisPatient.HKDZ;

            // 通过队列号查询对应的科室/诊室
            var deptConfig = dicts[TriageDict.TriageDepartment.ToString()]
                .FirstOrDefault(x => x.HisConfigCode == hisPatient.QueueId);

            var existsRegisterInfo = patientInfo.RegisterInfo.FirstOrDefault(x => x.RegisterNo == hisPatient.RegisterNo);

            if (existsRegisterInfo != null)
            {
                // 是否退号
                existsRegisterInfo.DeleteUser = hisPatient.RefundStatus == 1 ? _currentUser?.UserName : null;
                existsRegisterInfo.IsCancelled = hisPatient.RefundStatus == 1;
                existsRegisterInfo.CancellationTime = hisPatient.RefundStatus == 1 ? (DateTime?)DateTime.Now : null;
            }

            return patientInfo;
        }

        /// <summary>
        /// 根据患者ID列表，发送更新消息到MQ，以便后续Patient更新患者信息
        /// </summary>
        private async Task<bool> SendRefundPatientsToCall(HashSet<string> refundRegisterNoList)
        {
            await _rabbitMqAppService.PublishEcisRefundPatientToCallAsync(refundRegisterNoList);
            return true;
        }
        #endregion
    }
}