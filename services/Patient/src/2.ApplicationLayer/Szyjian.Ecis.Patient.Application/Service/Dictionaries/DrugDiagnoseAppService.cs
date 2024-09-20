using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 快速诊断字典API
    /// </summary>
    [Authorize]
    public class DrugDiagnoseAppService : EcisPatientAppService, IDrugDiagnoseAppService, ICapSubscribe
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<DrugDiagnoseAppService> _log;
        private readonly IConfiguration _configuration;

        public DrugDiagnoseAppService(IFreeSql freeSql, ILogger<DrugDiagnoseAppService> log, IConfiguration configuration)
        {
            _freeSql = freeSql;
            _log = log;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取诊断记录字典列表
        /// </summary>
        /// <param name="whereInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<PagedResultDto<DrugDiagnoseDto>>> GetDrugDiagnoseListAsync(
            DrugDiagnoseWhereInput whereInput,
            CancellationToken cancellationToken)
        {
            var list = await _freeSql.Select<DrugDiagnose>()
                .WhereIf(!string.IsNullOrEmpty(whereInput.InputParam),
                    w => w.DiagnoseCode.Contains(whereInput.InputParam) ||
                         w.PyCode.Contains(whereInput.InputParam) || w.DiagnoseName.Contains(whereInput.InputParam))
                .WhereIf(whereInput.DiagType != -1, x => x.DiagType == whereInput.DiagType)
                .Count(out var totalCount)
                // 诊断按匹配程度进行排序 Begein
                .OrderByIf(!whereInput.InputParam.IsNullOrWhiteSpace(),
                    x => x.PyCode != null && x.PyCode.IndexOf(whereInput.InputParam) >= 0
                        ? x.PyCode.IndexOf(whereInput.InputParam)
                        : 9999)
                .OrderByIf(!whereInput.InputParam.IsNullOrWhiteSpace(),
                    x => x.DiagnoseName != null && x.DiagnoseName.IndexOf(whereInput.InputParam) >= 0
                        ? x.DiagnoseName.IndexOf(whereInput.InputParam)
                        : 9999)
                .OrderByIf(!whereInput.InputParam.IsNullOrWhiteSpace(), x => x.PyCode.Length)
                .OrderByIf(!whereInput.InputParam.IsNullOrWhiteSpace(), x => x.DiagnoseName.Length)
                // 诊断按匹配程度进行排序 End
                .Page(whereInput.PageIndex, whereInput.PageSize)
                .ToListAsync<DrugDiagnoseDto>(cancellationToken);
            var userName = CurrentUser.FindClaimValue("name");
            var patientRecord =
                await _freeSql.Select<DiagnoseRecord>().Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && x.AddUserCode == userName &&
                                                                  x.PI_ID == whereInput.PI_ID && !x.IsDeleted)
                    .ToListAsync(cancellationToken: cancellationToken);
            foreach (var record in patientRecord)
            {
                list.Remove(list.Find(x => x.DiagnoseCode == record.DiagnoseCode));
            }
            var collectList = await _freeSql.Select<DiagnoseRecord>().Where(x =>
                    x.DiagnoseClassCode == DiagnoseClass.收藏 && x.DoctorCode == userName && !x.IsDeleted)
                .ToListAsync(cancellationToken: cancellationToken);
            var pageList = list.OrderBy(x => x.Sort).Take(whereInput.PageSize).ToList();
            foreach (var page in pageList)
            {
                if (collectList.Any(r => r.DiagnoseCode == page.DiagnoseCode))
                {
                    page.IsCollected = true;
                    page.PD_ID = collectList.Where(r => r.DiagnoseCode == page.DiagnoseCode).FirstOrDefault().PD_ID;
                }
            }
            var res = new PagedResultDto<DrugDiagnoseDto>
            {
                TotalCount = totalCount,
                Items = pageList
            };
            return RespUtil.Ok(data: res);
        }

        /// <summary>
        /// 原同步龙岗诊断字典信息，使用All对比方式，4w条数据效率比较慢
        /// </summary>
        /// <param name="eventData"></param>
        //[CapSubscribe("sync.diagnose.from.masterdata")]
        public async Task HandleEventAsync(DrugDiagnoseHis eventData)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (eventData.DicType != 6)
            {
                return;
            }

            if (eventData.DicDatas.Count == 0)
            {
                return;
            }

            var newDiagnoseList = new List<DrugDiagnose>();
            var currentDiagnoseList = await _freeSql.Select<DrugDiagnose>().ToListAsync();
            var hisDiagnoseList = JsonConvert.DeserializeObject<List<DiagnoseEto>>(
                JsonConvert.SerializeObject(eventData.DicDatas));
            hisDiagnoseList?.ForEach(x =>
            {
                newDiagnoseList.Add(new DrugDiagnose()
                {
                    DiagnoseCode = x.HisCode,
                    DiagnoseName = x.DiagName,
                    PyCode = x.SpellCode,
                    DiagType = x.DiagType,
                    Icd10 = x.Icd10,
                    CardRepType = string.IsNullOrEmpty(x.CardrepType) ? 0 : Enum.Parse<ECardReportingType>(x.CardrepType)
                });
            });
            var updateDiagnose = new List<DrugDiagnose>();
            var addDiagnose = newDiagnoseList.Where(x => currentDiagnoseList.All(a => a.DiagnoseCode != x.DiagnoseCode)).ToList();
            //var addDiagnoseList = newDiagnoseList.Except(currentDiagnoseList);
            var deleteDiagnose = currentDiagnoseList.Where(x => newDiagnoseList.All(a => a.DiagnoseCode != x.DiagnoseCode)).ToList();
            //去掉已删除的项
            currentDiagnoseList.RemoveAll(deleteDiagnose);
            //去掉新增的项
            currentDiagnoseList.RemoveAll(addDiagnose);
            currentDiagnoseList.ForEach(x =>
            {
                if (newDiagnoseList.Any(g =>
                        g.DiagnoseCode == x.DiagnoseCode &&
                        g.DiagnoseName != x.DiagnoseName &&
                        g.DiagType != x.DiagType &&
                        x.PyCode != g.PyCode &&
                        x.Icd10 != g.Icd10 &&
                        x.CardRepType != g.CardRepType))
                {
                    updateDiagnose.Add(x);
                }
            });
            if (addDiagnose.Any())
            {
                await _freeSql.Insert(addDiagnose).ExecuteAffrowsAsync();
            }
            //_log.LogInformation($"新增诊断，数量：{addDiagnose.Count()}，NameList：{addDiagnose.Select(x => x.DiagnoseCode).JoinAsString("|")}");
            _log.LogInformation($"新增诊断，数量：{addDiagnose.Count()}");

            if (updateDiagnose.Any())
            {
                updateDiagnose.ForEach((Action<DrugDiagnose>)(d =>
                {
                    var data = newDiagnoseList.FirstOrDefault((Func<DrugDiagnose, bool>)(s => s.DiagnoseCode == d.DiagnoseCode));
                    if (data != null)
                    {
                        _freeSql.Update<DrugDiagnose>()
                            .Set(s => s.PyCode, data.PyCode)
                            .Set(s => s.DiagnoseName, data.DiagnoseName)
                            .Set(s => s.Icd10, data.Icd10)
                            .Set(s => s.DiagType, data.DiagType)
                            .Set(s => s.CardRepType, data.CardRepType)
                            .Where(s => s.DiagnoseCode == d.DiagnoseCode)
                            .ExecuteAffrowsAsync();
                    }
                }));
            }
            //_log.LogInformation($"更新诊断，数量：{updateDiagnose.Count()}，NameList：{updateDiagnose.Select(x => x.DiagnoseCode).JoinAsString("|")}");
            _log.LogInformation($"更新诊断，数量：{updateDiagnose.Count()}");


            if (deleteDiagnose.Any())
            {
                await _freeSql.Delete<DrugDiagnose>(deleteDiagnose).ExecuteAffrowsAsync();
            }
            //_log.LogInformation($"删除诊断，数量：{deleteDiagnose.Count()}，NameList：{deleteDiagnose.Select(x => x.DiagnoseCode).JoinAsString("|")}");
            _log.LogInformation($"删除诊断，数量：{deleteDiagnose.Count()}");


            stopwatch.Stop();
            _log.LogInformation($"原同步诊断字典信息，耗时：{stopwatch.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// 同步龙岗诊断字典信息，使用Except对比 by hushitong 20230614
        /// </summary>
        //[AllowAnonymous]
        [CapSubscribe("sync.diagnose.from.masterdata")]
        public async Task SyncLGHisDrugDiagnoseAsync(DrugDiagnoseHis eventData)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                if (eventData.DicType != 6)
                {
                    return;
                }

                if (eventData.DicDatas.Count == 0)
                {
                    return;
                }

                var hisDiagnoseList = JsonConvert.DeserializeObject<List<DiagnoseEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
                var newDiagnoseList = new List<DrugDiagnose>();
                hisDiagnoseList?.ForEach(x =>
                {
                    newDiagnoseList.Add(new DrugDiagnose()
                    {
                        DiagnoseCode = x.HisCode,
                        DiagnoseName = x.DiagName,
                        PyCode = x.SpellCode,
                        DiagType = x.DiagType,
                        Icd10 = x.Icd10,
                        CardRepType = string.IsNullOrEmpty(x.CardrepType) ? 0 : Enum.Parse<ECardReportingType>(x.CardrepType)
                    });
                });
                stopwatch.Stop();
                var timeJson2List = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                var currentDiagnoseList = await _freeSql.Select<DrugDiagnose>().ToListAsync();

                var addDiagnose = newDiagnoseList.Except(currentDiagnoseList, new DrugDiagnoseCodeComparer()).ToList();
                var deleteDiagnose = currentDiagnoseList.Except(newDiagnoseList, new DrugDiagnoseCodeComparer()).ToList();
                stopwatch.Stop();
                var timeGetAddnDel = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                //当前诊断列表去掉需删除的项
                currentDiagnoseList.RemoveAll(deleteDiagnose);
                //由His同步来的新诊断列表去掉需新增的项
                newDiagnoseList.RemoveAll(addDiagnose);

                var updateDiagnose = newDiagnoseList.Except(currentDiagnoseList, new DrugDiagnoseUpdateComparer()).ToList();
                stopwatch.Stop();
                var timeGetUpdateList = stopwatch.ElapsedMilliseconds;

                // 是否更新数据库，默认 true
                // true：    真实更新数据库
                // false：   不真实更新数据库，只把更新数据库sql脚本记录到日志，要输出sql语句需要把useCustomBatch设置位True
                bool doUpdateDB = true;
                // 是否使用自定义插入batch插入方式，默认 false
                // true：    使用
                // false：   不使用，默认不推荐使用，速度会比freesql自动分批插入慢，只有在特定环境下需要用，如上面的需要输出sql语句
                bool useCustomBatch = false;
                // 数据库更新影响行数
                int effCount = 0;

                // 注：按性能来说，先Del数据再Add数据会更好，但是由于用户可能在使用中，使用先Add再Del对用户更友好

                #region Add
                {
                    stopwatch.Restart();
                    effCount = 0;
                    if (addDiagnose.Any())
                    {
                        if (!useCustomBatch)
                        {
                            effCount = await _freeSql.Insert(addDiagnose).ExecuteAffrowsAsync();
                            //await _freeSql.Insert(addDiagnose).ExecuteSqlBulkCopyAsync();
                        }
                        else
                        {
                            int batchSize = 1000; // 每批新增的数据量
                            int totalCount = addDiagnose.Count; // 获取总记录数

                            for (int i = 0; i < totalCount; i += batchSize)
                            {
                                int addCount = 0;
                                if (doUpdateDB)
                                {
                                    addCount = await _freeSql.Insert(addDiagnose.Skip(i).Take(batchSize)).ExecuteAffrowsAsync();
                                }
                                else
                                {
                                    addCount = batchSize;
                                    var str = _freeSql.Insert(addDiagnose.Skip(i).Take(batchSize)).ToSql();
                                    _log.LogInformation($"add sql: {str}");
                                }

                                effCount += addCount;
                            }
                        }
                    }
                    //_log.LogInformation($"effCount：{effCount}，新增诊断，数量：{addDiagnose.Count()}，NameList：{addDiagnose.Select(x => x.DiagnoseCode).JoinAsString("|")}");
                    _log.LogInformation($"effCount：{effCount}，新增诊断，数量：{addDiagnose.Count()}");
                    stopwatch.Stop();
                }
                var timeAdd = stopwatch.ElapsedMilliseconds;
                #endregion

                #region Update
                {
                    stopwatch.Restart();
                    effCount = 0;
                    if (updateDiagnose.Any())
                    {
                        if (!useCustomBatch)
                        {
                            effCount = await _freeSql.Update<DrugDiagnose>()
                                       .SetSource(updateDiagnose, a => a.DiagnoseCode)
                                       .UpdateColumns(s => new { s.PyCode, s.DiagnoseName, s.Icd10, s.DiagType, s.CardRepType })
                                       .ExecuteAffrowsAsync();
                        }
                        else
                        {
                            int batchSize = 1000; // 每批更新的数据量
                            int totalCount = updateDiagnose.Count; // 获取总记录数

                            for (int i = 0; i < totalCount; i += batchSize)
                            {
                                int updateCount = 0;
                                if (doUpdateDB)
                                {
                                    updateCount = await _freeSql.Update<DrugDiagnose>()
                                       .SetSource(updateDiagnose.Skip(i).Take(batchSize), a => a.DiagnoseCode)
                                       .UpdateColumns(s => new { s.PyCode, s.DiagnoseName, s.Icd10, s.DiagType, s.CardRepType })
                                       .ExecuteAffrowsAsync();
                                }
                                else
                                {
                                    updateCount = batchSize;
                                    var str = _freeSql.Update<DrugDiagnose>()
                                        .SetSource(updateDiagnose.Skip(i).Take(batchSize), a => a.DiagnoseCode)
                                        .UpdateColumns(s => new { s.PyCode, s.DiagnoseName, s.Icd10, s.DiagType, s.CardRepType })
                                        .ToSql();
                                    _log.LogInformation($"update sql: {str}");
                                }

                                effCount += updateCount;
                            }
                        }
                    }
                    //_log.LogInformation($"effCount：{effCount}，更新诊断，数量：{updateDiagnose.Count()}，NameList：{updateDiagnose.Select(x => x.DiagnoseCode).JoinAsString("|")}");
                    _log.LogInformation($"effCount：{effCount}，更新诊断，数量：{updateDiagnose.Count()}");
                    stopwatch.Stop();
                }
                var timeDoUpdate = stopwatch.ElapsedMilliseconds;
                #endregion

                #region Delete
                {
                    stopwatch.Restart();
                    effCount = 0;
                    if (deleteDiagnose.Any() && _configuration["HospitalCode"] == "LDC")
                    {
                        if (!useCustomBatch)
                        {
                            effCount = await _freeSql.Delete<DrugDiagnose>(deleteDiagnose).ExecuteAffrowsAsync();

                        }
                        else
                        {
                            int batchSize = 1000; // 每批删除的数据量
                            int totalCount = deleteDiagnose.Count; // 获取总记录数

                            for (int i = 0; i < totalCount; i += batchSize)
                            {
                                int deletedCount = 0;
                                if (doUpdateDB)
                                {
                                    deletedCount = await _freeSql.Delete<DrugDiagnose>(deleteDiagnose.Skip(i).Take(batchSize)).ExecuteAffrowsAsync();
                                }
                                else
                                {
                                    deletedCount = batchSize;
                                    var str = _freeSql.Delete<DrugDiagnose>(deleteDiagnose.Skip(i).Take(batchSize)).ToSql();
                                    _log.LogInformation($"delete sql: {str}");
                                }

                                effCount += deletedCount;
                            }
                        }
                    }
                    //_log.LogInformation($"effCount：{effCount}，删除诊断，数量：{deleteDiagnose.Count()}，NameList：{deleteDiagnose.Select(x => x.DiagnoseCode).JoinAsString("|")}");
                    _log.LogInformation($"effCount：{effCount}，删除诊断，数量：{deleteDiagnose.Count()}");
                    stopwatch.Stop();
                }
                long timeDoDel = stopwatch.ElapsedMilliseconds;
                #endregion

                _log.LogInformation($@"
                doUpdateDB ：{doUpdateDB}，
                useCustomBatch： {useCustomBatch}，
                把MQ获得数据转换为hisDiagnoseList耗时: {timeJson2List}ms，
                获得addDiagnose和deleteDiagnose耗时: {timeGetAddnDel}ms，
                获得updateDiagnose耗时: {timeGetUpdateList}ms，
                新增诊断耗时: {timeAdd}ms，
                更新诊断耗时: {timeDoUpdate}ms，
                删除诊断耗时: {timeDoDel}ms，
                共耗时：{timeJson2List + timeGetAddnDel + timeGetUpdateList + timeAdd + timeDoUpdate + timeDoDel}ms
                ");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "同步龙岗诊断字典信息失败");
            }
        }

        /// <summary>
        /// 诊断字典多种对比方式测试，不作为调用方法，只作为测试使用
        /// </summary>
        [AllowAnonymous]
        [NonAction]
        [HttpGet]
        public async Task<string> SyncLGHisDrugDiagnoseTestAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            StringBuilder msg = new StringBuilder();

            stopwatch.Start();
            var newDiagnoseList = await _freeSql.Select<DrugDiagnose>().ToListAsync();
            newDiagnoseList.RemoveRange(0, 77);
            var currentDiagnoseList = await _freeSql.Select<DrugDiagnose>().ToListAsync();
            currentDiagnoseList.RemoveRange(currentDiagnoseList.Count - 44, 33);
            stopwatch.Stop();
            long time1 = stopwatch.ElapsedMilliseconds;

            HashSet<string> hashSetAdd = new HashSet<string>();
            HashSet<string> hashSetDel = new HashSet<string>();
            var addStr = string.Empty;
            var deleteStr = string.Empty;

            #region All AsParallel
            //{
            //    stopwatch.Restart();
            //    var addDiagnose = newDiagnoseList.AsParallel().Where(x => currentDiagnoseList.All(a => a.DiagnoseCode != x.DiagnoseCode)).ToList();
            //    addStr = string.Join(",", addDiagnose.Select(x => x.DiagnoseCode));
            //    hashSetAdd.Add(addStr);
            //    stopwatch.Stop();
            //    long timeGetAddnDel = stopwatch.ElapsedMilliseconds;

            //    stopwatch.Restart();
            //    var deleteDiagnose = currentDiagnoseList.AsParallel().Where(x => newDiagnoseList.All(a => a.DiagnoseCode != x.DiagnoseCode)).ToList();
            //    deleteStr = string.Join(",", deleteDiagnose.Select(x => x.DiagnoseCode));
            //    hashSetDel.Add(deleteStr);
            //    stopwatch.Stop();
            //    long timeGetUpdateList = stopwatch.ElapsedMilliseconds;

            //    msg.AppendLine("----- All .AsParallel()-----");
            //    msg.AppendLine($"新增：{addDiagnose.Count}，删除：{deleteDiagnose.Count}，耗时：获得2列表：{timeJson2List}ms，得到新增列表：{timeGetAddnDel}ms，得到删除列表：{timeGetUpdateList}ms");
            //}
            #endregion

            #region Exists AsParallel
            {
                //stopwatch.Restart();
                //var addDiagnose = newDiagnoseList.AsParallel().Where(x => !currentDiagnoseList.Exists(a => a.DiagnoseCode == x.DiagnoseCode)).ToList();
                //addStr = string.Join(",", addDiagnose.Select(x => x.DiagnoseCode));
                //hashSetAdd.Add(addStr);
                //stopwatch.Stop();
                //long timeGetAddnDel = stopwatch.ElapsedMilliseconds;

                //stopwatch.Restart();
                //var deleteDiagnose = currentDiagnoseList.AsParallel().Where(x => !newDiagnoseList.Exists(a => a.DiagnoseCode == x.DiagnoseCode)).ToList();
                //deleteStr = string.Join(",", deleteDiagnose.Select(x => x.DiagnoseCode));
                //hashSetDel.Add(deleteStr);
                //stopwatch.Stop();
                //long timeGetUpdateList = stopwatch.ElapsedMilliseconds;


                //// 新增诊断
                //stopwatch.Restart();
                //if (addDiagnose.Any())
                //{
                //    await _freeSql.Insert(addDiagnose).ExecuteAffrowsAsync();
                //}
                //stopwatch.Stop();
                //long timeAdd = stopwatch.ElapsedMilliseconds;

                //// 更新诊断
                //stopwatch.Restart();
                //var updateDiagnose = new List<DrugDiagnose>();
                ////去掉已删除的项
                //currentDiagnoseList.RemoveAll(deleteDiagnose);
                ////去掉新增的项
                //currentDiagnoseList.RemoveAll(addDiagnose);
                //currentDiagnoseList.ForEach(x =>
                //{
                //    if (newDiagnoseList.Exists(g =>
                //            g.DiagnoseCode == x.DiagnoseCode &&
                //            g.DiagnoseName != x.DiagnoseName &&
                //            g.DiagType != x.DiagType &&
                //            x.PyCode != g.PyCode &&
                //            x.Icd10 != g.Icd10 &&
                //            x.CardRepType != g.CardRepType))
                //    {
                //        updateDiagnose.Add(x);
                //    }
                //});
                //stopwatch.Stop();
                //long timeDoUpdate = stopwatch.ElapsedMilliseconds;
                //stopwatch.Restart();
                //if (updateDiagnose.Any())
                //{
                //    updateDiagnose.ForEach(d =>
                //    {
                //        var data = newDiagnoseList.FirstOrDefault(s => s.DiagnoseCode == d.DiagnoseCode);
                //        if (data != null)
                //        {
                //            _freeSql.Update<DrugDiagnose>()
                //                .Set(s => s.PyCode, data.PyCode)
                //                .Set(s => s.DiagnoseName, data.DiagnoseName)
                //                .Set(s => s.Icd10, data.Icd10)
                //                .Set(s => s.DiagType, data.DiagType)
                //                .Set(s => s.CardRepType, data.CardRepType)
                //                .Where(s => s.DiagnoseCode == d.DiagnoseCode)
                //                .ExecuteAffrowsAsync();
                //        }
                //    });
                //}
                //stopwatch.Stop();
                //long timeDoDel = stopwatch.ElapsedMilliseconds;

                //// 删除诊断
                //stopwatch.Restart();
                //if (deleteDiagnose.Any())
                //{
                //    await _freeSql.Delete<DrugDiagnose>(deleteDiagnose).ExecuteAffrowsAsync();
                //}
                //stopwatch.Stop();
                //long time7 = stopwatch.ElapsedMilliseconds;

                //msg.AppendLine("----- Exists .AsParallel -----");
                //msg.AppendLine($"新增：{addDiagnose.Count}，删除：{deleteDiagnose.Count}，耗时：获得2列表：{timeJson2List}ms，得到新增列表：{timeGetAddnDel}ms，得到删除列表：{timeGetUpdateList}ms");
                //msg.AppendLine($"新增：{addDiagnose.Count}，更新：{updateDiagnose.Count}，删除：{deleteDiagnose.Count}，耗时：获得2列表：{timeJson2List}ms，得到新增列表：{timeGetAddnDel}ms，得到删除列表：{timeGetUpdateList}ms，新增诊断：{timeAdd}ms，获取更新诊断列表：{timeDoUpdate}ms，更新诊断：{timeDoDel}ms，删除诊断：{time7}ms");
            }
            #endregion

            #region Except HashCode
            {
                newDiagnoseList.RemoveRange(10000, 44);
                currentDiagnoseList.RemoveRange(20000, 44);

                stopwatch.Restart();
                var addDiagnose = newDiagnoseList.Except(currentDiagnoseList, new DrugDiagnoseCodeComparer()).ToList();
                addStr = string.Join(",", addDiagnose.Select(x => x.DiagnoseCode));
                hashSetAdd.Add(addStr);
                stopwatch.Stop();
                long time2 = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                var deleteDiagnose = currentDiagnoseList.Except(newDiagnoseList, new DrugDiagnoseCodeComparer()).ToList();
                deleteStr = string.Join(",", deleteDiagnose.Select(x => x.DiagnoseCode));
                hashSetDel.Add(deleteStr);
                stopwatch.Stop();
                long time3 = stopwatch.ElapsedMilliseconds;

                newDiagnoseList.RemoveAll(addDiagnose);
                currentDiagnoseList.RemoveAll(deleteDiagnose);
                newDiagnoseList.ForEach(x =>
                {
                    if (x.PyCode.Contains("FJB"))
                    {
                        x.DiagnoseName += "_Update";
                    }
                });
                stopwatch.Restart();
                var updateList = newDiagnoseList.Except(currentDiagnoseList, new DrugDiagnoseUpdateComparer());
                stopwatch.Stop();
                long time4 = stopwatch.ElapsedMilliseconds;

                msg.AppendLine("----- Except List -----");
                msg.AppendLine($"新增：{addDiagnose.Count}，删除：{deleteDiagnose.Count}，更新：{updateList.Count()}，");
                msg.AppendLine($"耗时：获得2列表：{time1}ms，得到新增列表：{time2}ms，得到删除列表：{time3}ms，获得update列表：{time4}ms");
            }
            #endregion

            #region Except string[]
            //{
            //    stopwatch.Restart();
            //    string[] newDiagnoseStrs= newDiagnoseList.Select(x => x.DiagnoseCode).ToArray();
            //    string[] currentDiagnoseStrs = currentDiagnoseList.Select(x => x.DiagnoseCode).ToArray();
            //    stopwatch.Stop();
            //    long time11 = stopwatch.ElapsedMilliseconds;

            //    stopwatch.Restart();
            //    var addDiagnose = newDiagnoseStrs.Where(x=> !currentDiagnoseStrs.Contains(x));
            //    addDiagnose.Count();
            //    stopwatch.Stop();
            //    long timeGetAddnDel = stopwatch.ElapsedMilliseconds;

            //    stopwatch.Restart();
            //    var deleteDiagnose = currentDiagnoseStrs.Where(x => !newDiagnoseStrs.Contains(x));
            //    deleteDiagnose.Count();
            //    stopwatch.Stop();
            //    long timeGetUpdateList = stopwatch.ElapsedMilliseconds;

            //    msg.AppendLine("----- Except string[] -----");
            //    msg.AppendLine($"新增：{addDiagnose.Count()}，删除：{deleteDiagnose.Count()}，耗时：{timeJson2List}ms，{time11}ms,{timeGetAddnDel}ms，{timeGetUpdateList}ms");
            //}
            #endregion

            msg.AppendLine($"hashSetAdd.Count: {hashSetAdd.Count}, hashSetDel.Count: {hashSetDel.Count}");
            return msg.ToString();
        }

        #region IEqualityComparer
        /// <summary>
        /// 自定义诊断比较器，只比较诊断编码
        /// </summary>
        public class DrugDiagnoseCodeComparer : IEqualityComparer<DrugDiagnose>
        {
            private readonly StringComparer comparer;

            public DrugDiagnoseCodeComparer()
            {
                comparer = StringComparer.Ordinal;
            }
            public bool Equals(DrugDiagnose x, DrugDiagnose y)
            {
                return comparer.Equals(x.DiagnoseCode, y.DiagnoseCode);
            }

            public int GetHashCode(DrugDiagnose obj)
            {
                unchecked // Overflow is fine, just wrap
                {
                    return comparer.GetHashCode(obj.DiagnoseCode);
                }
            }
        }

        /// <summary>
        /// 自定义诊断比较器
        /// </summary>
        public class DrugDiagnoseUpdateComparer : IEqualityComparer<DrugDiagnose>
        {
            private readonly StringComparer comparer;

            public DrugDiagnoseUpdateComparer()
            {
                comparer = StringComparer.Ordinal;
            }
            public bool Equals(DrugDiagnose x, DrugDiagnose y)
            {
                return comparer.Equals(x.DiagnoseCode, y.DiagnoseCode) && (
                        comparer.Equals(x.DiagnoseName, y.DiagnoseName) ||
                        comparer.Equals(x.DiagType, y.DiagType) ||
                        comparer.Equals(x.PyCode, y.PyCode) ||
                        comparer.Equals(x.Icd10, y.Icd10) ||
                        comparer.Equals(x.CardRepType, y.CardRepType)
                    );
            }

            public int GetHashCode(DrugDiagnose obj)
            {
                unchecked // Overflow is fine, just wrap
                {
                    var codeHash = obj.DiagnoseCode.GetHashCode();
                    var nameHash = obj.DiagnoseName.GetHashCode();
                    var typeHash = obj.DiagType.GetHashCode();
                    var pyCodeHash = obj.PyCode.GetHashCode();
                    var icd10Hash = obj.Icd10.GetHashCode();
                    var cardRepTypeHash = obj.CardRepType.GetHashCode();
                    return codeHash ^ nameHash ^ typeHash ^ pyCodeHash ^ icd10Hash ^ cardRepTypeHash;
                }
            }
        }
        #endregion
    }
}