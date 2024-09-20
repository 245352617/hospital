using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 统计分析Api
    /// </summary>
    [Auth("PatientDataAnalysis")]
    [Authorize]
    public class PatientDataAnalysisAppService : ApplicationService, IPatientDataAnalysisAppService
    {
        private readonly ILogger<PatientDataAnalysisAppService> _log;
        private readonly IRepository<GroupInjuryInfo> _groupIRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly IChangeRecordRepository _infoChangeRecords;

        private ITriageConfigAppService _triageConfigService;
        private ITriageConfigAppService TriageConfigService => LazyGetRequiredService(ref _triageConfigService);

        public PatientDataAnalysisAppService(IPatientInfoRepository patientInfoRepository, ILogger<PatientDataAnalysisAppService> log,
            IRepository<GroupInjuryInfo> groupIRepository, IChangeRecordRepository infoChangeRecords)
        {
            _patientInfoRepository = patientInfoRepository;
            _log = log;
            _groupIRepository = groupIRepository;
            _infoChangeRecords= infoChangeRecords;
        }

        /// <summary>
        /// 获取统计分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("PatientDataAnalysis" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<PatientAnalysisDto>> GetPatientDataAnalysisAsync(PatientInfoWhereInput input)
        {
            try
            {
                var dict = await TriageConfigService.GetTriageConfigByRedisAsync();
                if (dict != null)
                {
                    var patients = await _patientInfoRepository
                        .Include(c => c.ScoreInfo)
                        .Include(c => c.ConsequenceInfo)
                        .Include(c => c.VitalSignInfo)
                        .WhereIf(input.StartTime != null && input.EndTime != null && input.StartTime < input.EndTime,
                            t => t.TriageTime >= input.StartTime && t.TriageTime <= input.EndTime)
                        .Where(w => w.TriageStatus == 1)
                        .Select(p => new PatientInfo()
                        {
                            PatientId = p.PatientId,
                            SexName = p.SexName,
                            Birthday = p.Birthday,
                            IsNoThree = p.IsNoThree,
                            ScoreInfo = p.ScoreInfo.Select(s => new ScoreInfo() { ScoreType = s.ScoreType }).ToList(),
                            ConsequenceInfo = new ConsequenceInfo()
                            {
                                ActTriageLevel = p.ConsequenceInfo.ActTriageLevel,
                                TriageDeptCode = p.ConsequenceInfo.TriageDeptCode,
                                ChangeLevel = p.ConsequenceInfo.ChangeLevel,
                                ChangeDept = p.ConsequenceInfo.ChangeDept,
                                TriageTargetCode = p.ConsequenceInfo.TriageTargetCode
                            },
                            VitalSignInfo = new VitalSignInfo()
                            {
                                Sbp = p.VitalSignInfo.Sbp,
                                Sdp = p.VitalSignInfo.Sdp,
                                Temp = p.VitalSignInfo.Temp,
                                BreathRate = p.VitalSignInfo.BreathRate,
                                HeartRate = p.VitalSignInfo.HeartRate,
                                Remark = p.VitalSignInfo.Remark
                            },
                        })
                        .ToListAsync();
                    var patientAnalysis = new PatientAnalysisDto();

                    #region 分诊级别
                    var triageLevel = dict[TriageDict.TriageLevel.ToString()].OrderBy(o => o.Sort).ToList();
                    //分诊级别柱状图统计
                    var level = triageLevel?
                        .GroupJoin(patients, triage => triage.TriageConfigCode,
                            patient => patient.ConsequenceInfo?.ActTriageLevel, (triage, patient) => new
                            {
                                Triage = triage,
                                Patient = patient
                            })
                        .SelectMany(s => s.Patient.DefaultIfEmpty(),
                            (triage, patient) => new
                            {
                                PatientId = patient?.Id,
                                TriageConfigName = triage.Triage?.TriageConfigName
                            })
                        .GroupBy(g => g.TriageConfigName)
                        .ToList();
                    patientAnalysis.TraigeLevelAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = level.Select(t => t.Key).ToArray(),
                        ItemData = level.Select(t =>
                                t.Count() == 1 ? t.FirstOrDefault()?.PatientId == null ? 0 : t.Count() : t.Count())
                            .ToArray(),
                    };
                    #endregion

                    #region 患者性别统计
                    var manCount = patients.Count(c => c.SexName.Contains("男"));
                    var womanCount = patients.Count(c => c.SexName.Contains("女"));
                    var sexPatientItem = new List<PatientAnalysisItem>()
                    {
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "男",
                            PersonNum = manCount
                        },
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "女",
                            PersonNum = womanCount
                        },
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "未知",
                            PersonNum = patients.Count()-manCount-womanCount
                        }
                    };
                    patientAnalysis.TraigeSexAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = sexPatientItem.Select(t => t.AnalysisItem).ToArray(),
                        ItemData = sexPatientItem.Select(t => t.PersonNum).ToArray()
                    };
                    #endregion

                    #region 分诊变更原因统计
                    List<PatientInfoChangeRecord> records = await _infoChangeRecords
                            .WhereIf(input.StartTime != null && input.EndTime != null && input.StartTime < input.EndTime,
                            t => t.CreationTime >= input.StartTime && t.CreationTime <= input.EndTime)
                            .Where(x => x.IsDeleted == false)
                            .Where(x => !string.IsNullOrEmpty(x.ChangeReason))
                            .OrderBy(x => x.PI_Id)
                            .OrderBy(x => x.ChangeReason)
                            .OrderByDescending(o => o.CreationTime)
                            .ToListAsync();
                    List<TriageConfigDto> changeTriage = dict[TriageDict.ChangeTriageReason.ToString()];

                    // 去除同一患者同一原因同一时间的重复变更记录（同一时间变更多个字段会生成多条数据，实际统计只计算一次）
                    List<PatientInfoChangeRecord> changeRecords = new List<PatientInfoChangeRecord>();
                    foreach (var item in records)
                    {
                        var lastRecord = changeRecords.LastOrDefault();
                        if (lastRecord == null)
                        {
                            changeRecords.Add(item);
                            continue;
                        }
                        if (lastRecord.PI_Id == item.PI_Id && lastRecord.ChangeReason == item.ChangeReason)
                        {
                            // 假设相差超过3秒则认为是不同的变更
                            if ((lastRecord.CreationTime - item.CreationTime).TotalSeconds > 3)
                                changeRecords.Add(item);
                            else
                                continue;
                        }
                        else
                        {
                            changeRecords.Add(item);
                        }
                    }

                    var listTransfer = changeRecords
                         .GroupBy(gg => string.IsNullOrEmpty(gg.ChangeReason) ? "空" : gg.ChangeReason)
                         .Select(s => new
                         {
                             ItemName = s.Key,
                             ItemList = s.Count()
                         }).ToList();
                    patientAnalysis.TriageTransferAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = listTransfer.OrderByDescending(x => x.ItemList).Select(t => t.ItemName).ToArray(),
                        ItemData = listTransfer.OrderByDescending(x => x.ItemList).Select(t => t.ItemList).ToArray()
                    };
                    #endregion

                    #region 顶部分诊红黄区统计

                    var levelPersonAnalysis = new LevelPersonAnalysis
                    {
                        RedTriagePerson = patients.Count(t =>
                            t.ConsequenceInfo?.ActTriageLevel == "TriageLevel_001" ||
                            t.ConsequenceInfo?.ActTriageLevel == "TriageLevel_002"),
                        YellowTriagePerson =
                            patients.Count(t => t.ConsequenceInfo?.ActTriageLevel == "TriageLevel_003"),
                        GreenTriagePerson = patients.Count(t =>
                            t.ConsequenceInfo?.ActTriageLevel == "TriageLevel_004" ||
                            t.ConsequenceInfo?.ActTriageLevel == "TriageLevel_005"),
                        NoLevelPerson = patients.Count(t => t.ConsequenceInfo?.ActTriageLevel == ""),
                        TotalTriagePerson = patients.Count()
                    };
                    patientAnalysis.LevelPersonAnalysis = levelPersonAnalysis;

                    var triageDept = dict[TriageDict.TriageDepartment.ToString()]?.Where(x => x.IsDeleted != 1 && x.IsDisable == 1);
                    var dept = triageDept.GroupJoin(patients, triage => triage.TriageConfigCode,
                            patient => patient.ConsequenceInfo?.TriageDeptCode,
                            (triage, patient) => new { Triage = triage, Patient = patient })
                        .SelectMany(s => s.Patient.DefaultIfEmpty(),
                            (triage, patient) => new
                            {
                                triage.Triage?.TriageConfigName,
                                PatientId = patient?.Id
                            })
                        .GroupBy(g => g.TriageConfigName)
                        .ToList();
                    patientAnalysis.TraigeDepartmentAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = dept.Select(t => t.Key).ToArray(),
                        ItemData = dept.Select(t =>
                                t.Count() == 1 ? t.FirstOrDefault()?.PatientId == null ? 0 : t.Count() : t.Count())
                            .ToArray(),
                    };

                    #endregion

                    //分诊年龄统计
                    patientAnalysis.TraigeAgeAnalysis = AnalysisByAge(patients);

                    #region 修改级别统计
                    var modifyLevelPatientItem = new List<PatientAnalysisItem>()
                    {
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "未修改",
                            PersonNum = patients.Count(t =>
                                string.IsNullOrEmpty(t.ConsequenceInfo.ChangeLevel) ||
                                t.ConsequenceInfo.ChangeLevel.Contains("undefined=>"))
                        },
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "已修改",
                            PersonNum = patients.Count(t =>
                                !string.IsNullOrEmpty(t.ConsequenceInfo.ChangeLevel) &&
                                !t.ConsequenceInfo.ChangeLevel.Contains("undefined=>"))
                        }
                    };

                    patientAnalysis.LevelModifyAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = modifyLevelPatientItem.Select(t => t.AnalysisItem).ToArray(),
                        ItemData = modifyLevelPatientItem.Select(t => t.PersonNum).ToArray(),
                    };
                    #endregion

                    #region 修改部门统计
                    var modifyDeptPatientItem = new List<PatientAnalysisItem>()
                    {
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "未修改",
                            PersonNum = patients.Count(t =>
                                string.IsNullOrEmpty(t.ConsequenceInfo.ChangeDept) ||
                                t.ConsequenceInfo.ChangeDept.Contains("undefined=>"))
                        },
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "已修改",
                            PersonNum = patients.Count(t =>
                                !string.IsNullOrEmpty(t.ConsequenceInfo.ChangeDept) &&
                                !t.ConsequenceInfo.ChangeDept.Contains("undefined=>"))
                        }
                    };

                    patientAnalysis.DeptModifyAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = modifyDeptPatientItem.Select(t => t.AnalysisItem).ToArray(),
                        ItemData = modifyDeptPatientItem.Select(t => t.PersonNum).ToArray(),
                    };
                    #endregion

                    #region 是否三无统计
                    var noThreePatientItem = new List<PatientAnalysisItem>()
                    {
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "三无人员",
                            PersonNum = patients.Count(t => t.IsNoThree)
                        },
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "已知患者信息",
                            PersonNum = patients.Count(t => !t.IsNoThree)
                        }
                    };

                    patientAnalysis.NoThreenPersonAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = noThreePatientItem.Select(t => t.AnalysisItem).ToArray(),
                        ItemData = noThreePatientItem.Select(t => t.PersonNum).ToArray(),
                    };
                    #endregion

                    #region 评分项统计
                    var scorePatientItem = new List<PatientAnalysisItem>()
                    {
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "生命体征评分",
                            PersonNum = patients.Count(t => t.VitalSignInfo != null && !string.IsNullOrWhiteSpace(
                                t.VitalSignInfo?.Sbp + t.VitalSignInfo?.Sdp + t.VitalSignInfo?.Temp +
                                t.VitalSignInfo?.BreathRate + t.VitalSignInfo?.HeartRate + t.VitalSignInfo?.Remark
                            ))
                        },
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "判定依据评分",
                            PersonNum = patients.Sum(t => t.ScoreInfo?.Count(s => s.ScoreType == "Judgment") ?? 0)
                        },
                        new PatientAnalysisItem
                        {
                            AnalysisItem = "评分表评分",
                            PersonNum = patients.Sum(t =>
                                t.ScoreInfo?.FirstOrDefault(s => s.ScoreType != "Judgment") == null
                                    ? 0
                                    : 1) //判断不是判定依据评分的类型，如果空说明没有评分项评分，不为空则有一项
                        }
                    };

                    patientAnalysis.TraigeScoreAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = scorePatientItem.Select(t => t.AnalysisItem).ToArray(),
                        ItemData = scorePatientItem.Select(t => t.PersonNum).ToArray(),
                    };
                    #endregion

                    #region 分诊去向统计
                    var direction = dict[TriageDict.TriageDirection.ToString()];

                    //分诊去向统计   
                    //var target = patient
                    //    .GroupBy(t => string.IsNullOrEmpty(t.ConsequenceInfo.TriageTargetCode) ? "未选择去向" :
                    //    direction.FirstOrDefault(f => f.TriageConfigCode == t.ConsequenceInfo.TriageTargetCode)?.TriageConfigName)
                    //    .Select(s => new PatientAnalysisItem { AnalysisItem = s.Key, PersonNum = s.Count() })
                    //    .ToList();
                    var listTarget = patients
                        .GroupBy(gg => string.IsNullOrEmpty(gg.TriageUserName) ? "无" : gg.TriageUserName)
                        .Select(s => new TriageTarget
                        {
                            ItemName = s.Key,
                            ItemList = direction.GroupJoin(s.ToList(), dit => dit.TriageConfigCode,
                                    item => item.ConsequenceInfo?.TriageTargetCode,
                                    (dit, item) => new { dit, item })
                                .SelectMany(m => m.item.DefaultIfEmpty(),
                                    (dit, item) => new
                                    {
                                        dit.dit.TriageConfigName,
                                        PatientId = item?.Id
                                    }).GroupBy(g => g.TriageConfigName)
                                .Select(t => new PatientAnalysisItem
                                {
                                    AnalysisItem = t.Key,
                                    PersonNum = t.Count() == 1
                                        ? t.FirstOrDefault()?.PatientId == null ? 0 : t.Count()
                                        : t.Count()
                                }).ToList()
                        }).ToList();

                    // .ToDictionary(d => d.Key,
                    //     d => d.GroupBy(g =>
                    //             string.IsNullOrEmpty(g.ConsequenceInfo?.TriageTargetCode)
                    //                 ? "未选择去向"
                    //                 : direction.FirstOrDefault(f =>
                    //                     f.TriageConfigCode == g.ConsequenceInfo?.TriageTargetCode)?.TriageConfigName)
                    //         .ToDictionary(dd => dd.Key, dd => dd.Count()).ToList());
                    patientAnalysis.TriageTargetAnalysis = listTarget;
                    #endregion

                    #region 群伤事件统计
                    var groupInjuryListConfig = dict?[TriageDict.GroupInjuryType.ToString()];
                    var patientGroup = patients.Where(w => w.GroupInjuryInfoId != Guid.Empty)
                        .GroupBy(g => g.GroupInjuryInfoId.ToString()).Select(s => s.Key).ToList();
                    var injuryId = string.Join(",", patientGroup!);
                    var group = await _groupIRepository.Where(w =>
                        injuryId.Contains(w.Id.ToString())).ToListAsync();
                    // if (group.Count >0)
                    // {
                    var groupInjury = groupInjuryListConfig.GroupJoin(group, triage => triage.TriageConfigCode,
                            group => group?.GroupInjuryCode,
                            (triage, group) => new { Triage = triage, Group = group })
                        .SelectMany(s => s.Group.DefaultIfEmpty(),
                            (triage, group) => new
                            {
                                triage.Triage?.TriageConfigName,
                                GroupInjuryInfoId = group?.Id,
                                GroupCount = patients.Count(w => w.GroupInjuryInfoId == group?.Id)
                            })
                        .GroupBy(g => g.TriageConfigName)
                        .ToList();
                    patientAnalysis.GroupInjuryTypeAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = groupInjury.Select(t => t.Key).ToArray(),
                        ItemData = groupInjury.Select(t =>
                            t.Sum(f => f.GroupCount)
                        ).ToArray()
                    };
                    // }
                    #endregion

                    #region 绿色通道统计
                    var greenRoad = dict?[TriageDict.GreenRoad.ToString()];
                    var greenAnalysis = greenRoad.GroupJoin(patients, triage => triage.TriageConfigCode,
                            patient => patient.GreenRoadCode,
                            (triage, patient) => new { Triage = triage, Patient = patient })
                        .SelectMany(s => s.Patient.DefaultIfEmpty(),
                            (triage, patient) => new
                            {
                                PatientId = patient?.Id,
                                triage.Triage?.TriageConfigName
                            })
                        .GroupBy(g => g.TriageConfigName)
                        .ToList();


                    //patient.GroupBy(t => string.IsNullOrEmpty(t.GreenRoadCode) ? "无" :
                    //     direction.FirstOrDefault(f => f.TriageConfigCode == t.GreenRoadCode)?.TriageConfigName)
                    //    .ToList();
                    patientAnalysis.GreenRoadAnalysis = new PatientAnalysisItemResult
                    {
                        AnalysisItem = greenAnalysis.Select(t => t.Key).ToArray(),
                        ItemData = greenAnalysis.Select(t =>
                                t.Count() == 1 ? t.FirstOrDefault()?.PatientId == null ? 0 : t.Count() : t.Count())
                            .ToArray(),
                    };
                    #endregion

                    return JsonResult<PatientAnalysisDto>.Ok(data: patientAnalysis);
                }

                _log.LogError("【PatientDataAnalysisService】【GetPatientDataAnalysisAsync】【获取统计分析错误】【Msg：从Redis获取字典失败】");
                return JsonResult<PatientAnalysisDto>.Fail(msg: "获取统计分析结果失败！请重试！");
            }
            catch (Exception e)
            {
                _log.LogWarning($"【PatientDataAnalysisService】【GetPatientDataAnalysisAsync】【获取统计分析错误】【Msg：{e}】");
                return JsonResult<PatientAnalysisDto>.Fail(data: e.Message);
            }
        }

        /// <summary>
        /// 分诊患者年龄统计
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private static PatientAnalysisItemResult AnalysisByAge(List<PatientInfo> patient)
        {
            //var ageGroups = patient.GroupBy(g => string.IsNullOrEmpty(g.Age) || !g.Age.Contains("岁") ? "0" : g.Age?.Split('岁')[0])
            //                 .ToList();
            var ageGroups = patient.Select(x =>
                {
                    int age = -1;
                    if (x.Birthday.HasValue)
                    {
                        int totalDiffMonths = (DateTime.Today.Year - x.Birthday.Value.Year) * 12 +
                                              (DateTime.Today.Month - x.Birthday.Value.Month) +
                                              (DateTime.Today.Day >= x.Birthday.Value.Day ? 0 : -1) /*是否不足1月*/;
                        age = (int)(totalDiffMonths / 12u);
                    }

                    return new { x.PatientId, Age = age };
                })
                .GroupBy(g => g.Age)
                .ToList();

            var agePatientItem = new List<PatientAnalysisItem>()
            {
                new PatientAnalysisItem
                {
                    AnalysisItem = "未知",
                    PersonNum = ageGroups.Where(t => t.Key < 0).Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "5岁以下",
                    PersonNum = ageGroups.Where(t => t.Key >= 0 && t.Key < 5).Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "5-14岁",
                    PersonNum = ageGroups.Where(t => t.Key >= 5 && t.Key <= 14)
                        .Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "15-29岁",
                    PersonNum = ageGroups.Where(t => t.Key >= 15 && t.Key <= 29)
                        .Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "30-44岁",
                    PersonNum = ageGroups.Where(t => t.Key >= 30 && t.Key <= 44)
                        .Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "45-59岁",
                    PersonNum = ageGroups.Where(t => t.Key >= 45 && t.Key <= 59)
                        .Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "60-74岁",
                    PersonNum = ageGroups.Where(t => t.Key >= 60 && t.Key <= 74)
                        .Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "75-84岁",
                    PersonNum = ageGroups.Where(t => t.Key >= 75 && t.Key <= 84)
                        .Sum(t => t.Count())
                },
                new PatientAnalysisItem
                {
                    AnalysisItem = "85岁以上",
                    PersonNum = ageGroups.Where(t => t.Key >= 85).Sum(t => t.Count())
                }
            };
            var ageAnalysis = new PatientAnalysisItemResult
            {
                AnalysisItem = agePatientItem.Select(t => t.AnalysisItem).ToArray(),
                ItemData = agePatientItem.Select(t => t.PersonNum).ToArray(),
            };

            return ageAnalysis;
        }
    }
}