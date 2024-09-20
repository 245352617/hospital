using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class PreHospitalTriageServiceDataContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<TriageConfig> _triageConfigRepository;
        private readonly IRepository<TriageConfigTypeDescription> _triageConfigTypeDescriptionRepository;
        private readonly IRepository<ScoreManage> _scoreManageRepository;
        private readonly IRepository<VitalSignExpression> _vitalSignExpressionRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<LevelTriageRelationDirection> _levelTriageRelationDirectionsRepository;
        private readonly IRepository<TableSetting> _tableSettingRepository;
        private readonly IRepository<RegisterMode> _registerModeRepository;
        private readonly NLogHelper _log;
        private readonly IRepository<ScoreDict, Guid> _scoreDictRepository;


        #region SMS

        private readonly IConfiguration _configuration;
        private readonly IRepository<TagSettings, Guid> _tagSettingsRepository;
        private readonly IRepository<DutyTelephone, Guid> _dutyTelephoneRepository;
        private readonly IRepository<TextMessageTemplate, Guid> _textMessageTemplateRepository;


        #endregion

        public PreHospitalTriageServiceDataContributor(IRepository<TriageConfig> triageConfigRepository,
            IGuidGenerator guidGenerator,
            IRepository<VitalSignExpression> vitalSignExpressionRepository,
            IRepository<TriageConfigTypeDescription> triageConfigTypeDescriptionRepository,
            IRepository<LevelTriageRelationDirection> levelTriageRelationDirectionsRepository,
            IRepository<TableSetting> tableSettingRepository,
            IRepository<RegisterMode> registerModeRepository,
            NLogHelper log, IRepository<ScoreManage> scoreManageRepository, IRepository<ScoreDict, Guid> scoreDictRepository, IRepository<DutyTelephone, Guid> dutyTelephoneSettingsRepository, IRepository<TagSettings, Guid> tagSettingsRepository, IRepository<TextMessageTemplate, Guid> textMessageTemplateRepository, IConfiguration configuration)
        {
            _triageConfigRepository = triageConfigRepository;
            _guidGenerator = guidGenerator;
            _vitalSignExpressionRepository = vitalSignExpressionRepository;
            _triageConfigTypeDescriptionRepository = triageConfigTypeDescriptionRepository;
            _levelTriageRelationDirectionsRepository = levelTriageRelationDirectionsRepository;
            _tableSettingRepository = tableSettingRepository;
            _registerModeRepository = registerModeRepository;
            _log = log;
            _scoreManageRepository = scoreManageRepository;
            _scoreDictRepository = scoreDictRepository;
            _dutyTelephoneRepository = dutyTelephoneSettingsRepository;
            _tagSettingsRepository = tagSettingsRepository;
            _textMessageTemplateRepository = textMessageTemplateRepository;
            _configuration = configuration;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            //门诊
            await CreateOutpatientDepartmentDataSeedAsync();
            await CreateGroupInjuryTypeDataSeedAsync();
            await CreateVitalSignExpressionDataSeedAsync();
            await CreateTriageDeptDataSeedAsync();
            await CreateTriageDirectionDataSeedAsync();
            await CreateGreenRoadDataSeedAsync();
            await CreateToHospitalWayDataSeedAsync();
            await CreateTriageConfigTypeDescriptionAsync();
            await CreateLevelTriageRelationDirectionAsync();
            await CreateTriageLevelAsync();
            await CreateKeyDiseasesAsync();
            await CreateIdentityTypeAsync();
            await CreateTableSettingDataSeedAsync();
            await CreateScoreTypeAsync();
            await CreateFastTackDataSeedAsync();
            await CreateSexDataSeedAsync();
            await CreateScoreManageAsync();
            await CreateTriageReportTypeDataSeedAsync();
            await CreateTriageTypeOfVisitDataSeedAsync();
            await CreateNarrationDataSeedAsync();
            await CreateMindDataSeedAsync();
            await CreateFaberDataSeedAsync();
            await CreateVitalSignRemarkDataSeedAsync();
            await CreateRegisterModeDataSeedAsync();
            await CreateCardiogramDataSeedAsync();
            await CreateLastDirectionDataSeedAsync();
            // 证件类型
            await CreateIdTypeDataSeedAsync();
            // 人群
            await CreateCrowdDataSeedAsync();
            // 就诊原因
            await CreateVisitReasonDataSeedAsync();
            // 关系
            await CreateSocietyRelationDataSeedAsync();
            //// 参保地
            //await CreateInsuplcAdmdvs();

            // 评分字典
            await CreateScoreDictAsync();
            // 国籍
            await CreateCountriesDataSeedAsync();
            // 分诊分区
            await CreateTriageAreaDataSeedAsync();
            // 分诊变更原因
            await CreateChangeTriageResonDataSeedAsync();
            //转诊限制
            await CreateReferralLimitDataSeedAsync();

            // 特约记账类型
            await CreateSpecialAccountTypeDataSeedAsync();

            if (Convert.ToBoolean(_configuration["Settings:SMS:IsEnabled"]))
            {
                await CreateTagSettingsDataSeedAsync();
                await CreateTextMessageTemplateDataSeedAsync();
            }

        }

        /// <summary>
        /// 创建院前分诊群伤类型种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateGroupInjuryTypeDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(t =>
                    t.TriageConfigType == (int)TriageDict.GroupInjuryType) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "001",
                            TriageConfigName = "生产事故",
                            Remark = "生产事故",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GroupInjuryType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "002",
                            TriageConfigName = "车祸伤",
                            Remark = "车祸伤",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GroupInjuryType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "003",
                            TriageConfigName = "治安事件",
                            Remark = "治安事件",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GroupInjuryType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "004",
                            TriageConfigName = "群体食物中毒",
                            Remark = "群体食物中毒",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GroupInjuryType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateGroupInjuryTypeDataSeedAsync】" +
                             $"【创建院前分诊群伤类型种子数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建生命体征评分表达式种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateVitalSignExpressionDataSeedAsync()
        {
            try
            {
                if (await _vitalSignExpressionRepository.CountAsync() <= 0)
                {
                    var list = new List<VitalSignExpression>
                    {
                        new VitalSignExpression
                        {
                            ItemName = "SPO2",
                            StLevelExpression = "value<80",
                            NdLevelExpression = "(value>=80) && (value<90)",
                            RdLevelExpression = "(value>=90) && (value<94)",
                            DefaultStLevelExpression = "value<80",
                            DefaultNdLevelExpression = "(value>=80) && (value<90)",
                            DefaultRdLevelExpression = "(value>=90) && (value<94)",
                        }.SetId(_guidGenerator.Create()),
                        new VitalSignExpression
                        {
                            ItemName = "SBP",
                            StLevelExpression = "(value<70) || (value>=200)",
                            NdLevelExpression = "((value>=70) && (value<=90)) || ((value>=180) && (value<200))",
                            RdLevelExpression = "(value>=140) && (value<180)",
                            ThALevelExpression = "(value>90) && (value<140)",
                            DefaultStLevelExpression = "(value<70) || (value>=200)",
                            DefaultNdLevelExpression = "((value>=70) && (value<=90)) || ((value>=180) && (value<200))",
                            DefaultRdLevelExpression = "(value>=140) && (value<180)",
                            DefaultThALevelExpression = "(value>90) && (value<140)"
                        }.SetId(_guidGenerator.Create()),
                        new VitalSignExpression
                        {
                            ItemName = "Breath"
                        }.SetId(_guidGenerator.Create()),
                        new VitalSignExpression
                        {
                            ItemName = "Temp",
                            NdLevelExpression = "value>39",
                            RdLevelExpression = "(value>38) && (value<=39)",
                            ThALevelExpression = "(value>=36) && (value<=38)",
                            DefaultNdLevelExpression = "value>39",
                            DefaultRdLevelExpression = "(value>38) && (value<=39)",
                            DefaultThALevelExpression = "(value>=36) && (value<=38)"
                        }.SetId(_guidGenerator.Create()),
                        new VitalSignExpression
                        {
                            ItemName = "HeartRate",
                            NdLevelExpression = "(value<5) || (value>=130)",
                            DefaultNdLevelExpression = "(value<5) || (value>=130)"
                        }.SetId(_guidGenerator.Create()),
                        new VitalSignExpression
                        {
                            ItemName = "DBP",
                            NdLevelExpression = "value>=110",
                            RdLevelExpression = "(value>=90) && (value<110)",
                            DefaultNdLevelExpression = "value>=110",
                            DefaultRdLevelExpression = "(value>=90) && (value<110)"
                        }.SetId(_guidGenerator.Create())
                    };

                    await _vitalSignExpressionRepository.GetDbSet().AddRangeAsync(list);
                    await _vitalSignExpressionRepository.GetDbContext().SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateVitalSignExpressionDataSeedAsync】" +
                             $"【创建生命体征评分表达式种子数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建分诊科室种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateTriageDeptDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(t =>
                    t.TriageConfigType == (int)TriageDict.TriageDepartment) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "2000",
                            TriageConfigName = "急诊抢救室",
                            Remark = "卒中",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageDepartment
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTriageDeptDataSeedAsync】" +
                             $"【创建分诊科室种子数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建分诊去向种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateTriageDirectionDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.TriageDirection).ToListAsync();

                //if (await _triageConfigRepository.CountAsync(t =>
                //    t.TriageConfigType == (int)TriageDict.TriageDirection) <= 0)
                //{
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "001",
                            TriageConfigName = "死亡",
                            Remark = "卒中",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageDirection
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "002",
                            TriageConfigName = "留观区",
                            Remark = "胸痛",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageDirection
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "003",
                            TriageConfigName = "抢救区",
                            Remark = "孕产妇",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageDirection
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "004",
                            TriageConfigName = "专科",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageDirection
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                          new TriageConfig
                        {
                            TriageConfigCode = "005",
                            TriageConfigName = "门诊科室",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageDirection
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                var dbContext = _triageConfigRepository.GetDbContext();

                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                    //   dbContext.Entry(item).State = EntityState.Added;
                });
                //不用医院初始code有可能重复，建议用name
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigName == y.TriageConfigName));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTriageDirectionDataSeedAsync】" +
                             $"【创建分诊科室种子数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建绿色通道种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateGreenRoadDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(
                    t => t.TriageConfigType == (int)TriageDict.GreenRoad) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "001",
                            TriageConfigName = "卒中",
                            Remark = "卒中",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "002",
                            TriageConfigName = "胸痛",
                            Remark = "胸痛",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "003",
                            TriageConfigName = "孕产妇",
                            Remark = "孕产妇",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "004",
                            TriageConfigName = "高热惊厥",
                            Remark = "高热惊厥",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "005",
                            TriageConfigName = "颅脑损伤",
                            Remark = "颅脑损伤",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "006",
                            TriageConfigName = "多发创伤",
                            Remark = "多发创伤",
                            Sort = 6,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "007",
                            TriageConfigName = "上消化道出血",
                            Remark = "上消化道出血",
                            Sort = 7,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "008",
                            TriageConfigName = "新生儿",
                            Remark = "新生儿",
                            Sort = 8,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "009",
                            TriageConfigName = "中毒",
                            Remark = "中毒",
                            Sort = 9,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.GreenRoad
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateGreenRoadDataSeedAsync】" +
                             $"【创建绿色通道种子数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建来院方式种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateToHospitalWayDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(t =>
                    t.TriageConfigType == (int)TriageDict.ToHospitalWay) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "001",
                            TriageConfigName = "本院120",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ToHospitalWay
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "002",
                            TriageConfigName = "步行来院",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ToHospitalWay
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "003",
                            TriageConfigName = "平车",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ToHospitalWay
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "004",
                            TriageConfigName = "搀扶",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ToHospitalWay
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "005",
                            TriageConfigName = "轮椅",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ToHospitalWay
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "006",
                            TriageConfigName = "外院120",
                            Sort = 6,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ToHospitalWay
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "007",
                            TriageConfigName = "110",
                            Sort = 7,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ToHospitalWay
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateToHospitalWayDataSeedAsync】" +
                             $"【创建来院方式种子数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建分诊级别关联去向
        /// </summary>
        /// <returns></returns>
        private async Task CreateLevelTriageRelationDirectionAsync()
        {
            try
            {
                if (await _levelTriageRelationDirectionsRepository.CountAsync() <= 0)
                {
                    var list = new List<LevelTriageRelationDirection>
                    {
                        new LevelTriageRelationDirection
                        {
                            LevelTriageDirectionCode = "LevelRedOne",
                            TriageLevelCode = "TriageLevel_001",
                            OtherDirectionCode = "TriageDirection_003",
                            TriageDirectionCode = "TriageDirection_003",
                            Sort = 1
                        }.SetId(_guidGenerator.Create()),
                        new LevelTriageRelationDirection
                        {
                            LevelTriageDirectionCode = "LevelRedTwo",
                            TriageLevelCode = "TriageLevel_002",
                            OtherDirectionCode = "TriageDirection_003",
                            TriageDirectionCode = "TriageDirection_003",
                            Sort = 2
                        }.SetId(_guidGenerator.Create()),
                        new LevelTriageRelationDirection
                        {
                            LevelTriageDirectionCode = "LevelYellow",
                            TriageLevelCode = "TriageLevel_003",
                            OtherDirectionCode = "TriageDirection_002",
                            TriageDirectionCode = "TriageDirection_002",
                            Sort = 3
                        }.SetId(_guidGenerator.Create()),
                        new LevelTriageRelationDirection
                        {
                            LevelTriageDirectionCode = "LevelGreenOne",
                            TriageLevelCode = "TriageLevel_004",
                            OtherDirectionCode = "TriageDirection_004",
                            TriageDirectionCode = "TriageDirection_004",
                            Sort = 4
                        }.SetId(_guidGenerator.Create()),
                        new LevelTriageRelationDirection
                        {
                            LevelTriageDirectionCode = "LevelGreenTwo",
                            TriageLevelCode = "TriageLevel_005",
                            OtherDirectionCode = "",
                            TriageDirectionCode = "TriageDirection_004",
                            Sort = 5
                        }.SetId(_guidGenerator.Create())
                    };
                    await _levelTriageRelationDirectionsRepository.GetDbSet().AddRangeAsync(list);
                    await _levelTriageRelationDirectionsRepository.GetDbContext().SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateLevelTriageRelationDirectionAsync】" +
                             $"【创建分诊级别关联去向数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建分诊级别
        /// </summary>
        /// <returns></returns>
        private async Task CreateTriageLevelAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(c =>
                    c.TriageConfigType == (int)TriageDict.TriageLevel) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "001",
                            TriageConfigName = "Ⅰ 级",
                            Remark = "Ⅰ 级（红区)",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageLevel
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "002",
                            TriageConfigName = "Ⅱ 级",
                            Remark = "Ⅱ 级（红区）",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageLevel
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "003",
                            TriageConfigName = "Ⅲ 级",
                            Remark = "Ⅲ 级（黄区）",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageLevel
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "004",
                            TriageConfigName = "Ⅳa 级",
                            Remark = "Ⅳa 级（绿区）",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageLevel
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "005",
                            TriageConfigName = "Ⅳb 级",
                            Remark = "Ⅳb 级（绿区）",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.TriageLevel
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };
                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateLevelTriageRelationDirectionAsync】" +
                             $"【创建分诊级别关联去向数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建重点病种
        /// </summary>
        /// <returns></returns>
        private async Task CreateKeyDiseasesAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(c =>
                    c.TriageConfigType == (int)TriageDict.KeyDiseases) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "001",
                            TriageConfigName = "急性呼吸衰歇",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "002",
                            TriageConfigName = "急性创伤",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "003",
                            TriageConfigName = "急性颅脑损伤",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "004",
                            TriageConfigName = "急性心力衰竭",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "005",
                            TriageConfigName = "急性心肌梗死",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "006",
                            TriageConfigName = "急性脑卒中",
                            Sort = 6,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "007",
                            TriageConfigName = "危重孕产妇",
                            Sort = 7,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "008",
                            TriageConfigName = "小儿热性惊厥",
                            Sort = 8,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.KeyDiseases
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };
                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateKeyDiseasesAsync】" +
                             $"【创建重点病种数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 常见身份类型
        /// </summary>
        /// <returns></returns>
        private async Task CreateIdentityTypeAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(c =>
                    c.TriageConfigType == (int)TriageDict.IdentityType) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "I001",
                            TriageConfigName = "不详",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I002",
                            TriageConfigName = "教师",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I003",
                            TriageConfigName = "国家公务员",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I004",
                            TriageConfigName = "专业技术人员",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I005",
                            TriageConfigName = "职员",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I006",
                            TriageConfigName = "企业管理人员",
                            Sort = 6,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I007",
                            TriageConfigName = "工人",
                            Sort = 7,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I008",
                            TriageConfigName = "农民",
                            Sort = 8,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I009",
                            TriageConfigName = "学生",
                            Sort = 9,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I010",
                            TriageConfigName = "现役军人",
                            Sort = 10,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I011",
                            TriageConfigName = "自由职业者",
                            Sort = 11,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I012",
                            TriageConfigName = "个体经营者",
                            Sort = 12,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I013",
                            TriageConfigName = "无业人员",
                            Sort = 13,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I014",
                            TriageConfigName = "退（离）休人员",
                            Sort = 14,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "I015",
                            TriageConfigName = "其他",
                            Sort = 15,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdentityType
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };
                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateKeyDiseasesAsync】" +
                             $"【创建重点病种数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 评分类型
        /// </summary>
        /// <returns></returns>
        private async Task CreateScoreTypeAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.ScoreType).ToListAsync();
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "Start",
                            TriageConfigName = "Start",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Ache",
                            TriageConfigName = "Ache",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "ESI",
                            TriageConfigName = "ESI",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Would",
                            TriageConfigName = "Would",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Rems",
                            TriageConfigName = "Rems",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "GCS",
                            TriageConfigName = "GCS",
                            Sort = 6,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Mews",
                            TriageConfigName = "Mews",
                            Sort = 7,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "FALL",
                            TriageConfigName = "FALL",
                            Sort = 8,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "qSOFA",
                            TriageConfigName = "qSOFA",
                            Sort = 9,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.ScoreType
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };
                list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                    });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateKeyDiseasesAsync】" +
                             $"【创建重点病种数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建院前分诊类型描述
        /// </summary>
        /// <returns></returns>
        private async Task CreateTriageConfigTypeDescriptionAsync()
        {
            try
            {
                var currentList = await this._triageConfigTypeDescriptionRepository.GetListAsync();
                /// 分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准
                var list = new List<TriageConfigTypeDescription>
                    {
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1001",
                            TriageConfigTypeName = "绿色通道",
                            Remark = "绿色通道",
                            Sort = 1
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1002",
                            TriageConfigTypeName = "群伤事件",
                            Remark = "群伤事件",
                            Sort = 2
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1003",
                            TriageConfigTypeName = "费别",
                            Remark = "费别",
                            Sort = 3
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1004",
                            TriageConfigTypeName = "来院方式",
                            Remark = "来院方式",
                            Sort = 4
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1005",
                            TriageConfigTypeName = "科室配置",
                            Remark = "科室配置",
                            Sort = 5
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1006",
                            TriageConfigTypeName = "分诊去向",
                            Remark = "分诊去向",
                            Sort = 6
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1007",
                            TriageConfigTypeName = "分诊级别",
                            Remark = "分诊级别",
                            Sort = 7
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1008",
                            TriageConfigTypeName = "身份类型",
                            Remark = "身份类型",
                            Sort = 8
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1009",
                            TriageConfigTypeName = "重点病种",
                            Remark = "重点病种",
                            Sort = 9
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1010",
                            TriageConfigTypeName = "症状或体征",
                            Remark = "症状或体征",
                            Sort = 10
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1011",
                            TriageConfigTypeName = "神志",
                            Remark = "神志",
                            Sort = 11
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1012",
                            TriageConfigTypeName = "就诊类型",
                            Remark = "就诊类型",
                            Sort = 12
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1013",
                            TriageConfigTypeName = "分诊评分类型",
                            Remark = "分诊评分类型",
                            Sort = 13
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1015",
                            TriageConfigTypeName = "性别",
                            Remark = "性别",
                            Sort = 15
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1016",
                            TriageConfigTypeName = "分诊报表类型",
                            Remark = "分诊报表类型",
                            Sort = 16
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1017",
                            TriageConfigTypeName = "主诉",
                            Remark = "主诉",
                            Sort = 17
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1018",
                            TriageConfigTypeName = "民族",
                            Remark = "民族",
                            Sort = 18
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1019",
                            TriageConfigTypeName = "生命体征备注",
                            Remark = "生命体征备注",
                            Sort = 19
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1020",
                            TriageConfigTypeName = "分诊-挂号模式",
                            Remark = "分诊-挂号模式",
                            Sort = 20
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1021",
                            TriageConfigTypeName = "心电图",
                            Remark = "心电图",
                            Sort = 21
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1022",
                            TriageConfigTypeName = "最终去向",
                            Remark = "最终去向",
                            Sort = 22
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1023",
                            TriageConfigTypeName = "证件类型",
                            Remark = "证件类型",
                            Sort = 23
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1024",
                            TriageConfigTypeName = "人群",
                            Remark = "人群",
                            Sort = 24
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1025",
                            TriageConfigTypeName = "就诊原因",
                            Remark = "就诊原因",
                            Sort = 25
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1026",
                            TriageConfigTypeName = "关系",
                            Remark = "与联系人关系",
                            Sort = 26
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1027",
                            TriageConfigTypeName = "参保地",
                            Remark = "参保地",
                            Sort = 27
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1033",
                            TriageConfigTypeName = "转诊限制",
                            Remark = "转诊限制",
                            Sort = 28
                        }.SetId(_guidGenerator.Create()),
                        new TriageConfigTypeDescription
                        {
                            TriageConfigTypeCode = "1034",
                            TriageConfigTypeName = "特约记账",
                            Remark = "特约记账",
                            Sort = 29
                        }.SetId(_guidGenerator.Create())
                    };
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigTypeCode == y.TriageConfigTypeCode));
                await _triageConfigTypeDescriptionRepository.GetDbSet().AddRangeAsync(insertList);

                await _triageConfigTypeDescriptionRepository.GetDbContext().SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTriageConfigTypeDescriptionAsync】" +
                             $"【创建院前分诊类型描述数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建分诊列表数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateTableSettingDataSeedAsync()
        {
            try
            {
                var list = new List<TableSetting>
                    {
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "registerNo",
                            ColumnName = "就诊号",
                            DefaultColumnName = "就诊号",
                            ColumnWidth = 104,
                            DefaultColumnWidth = 104,
                            SequenceNo = 1,
                            DefaultSequenceNo = 1,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "patientId",
                            ColumnName = "病人编号",
                            DefaultColumnName = "病人编号",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 2,
                            DefaultSequenceNo = 2,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "patientName",
                            ColumnName = "病人姓名",
                            DefaultColumnName = "病人姓名",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 3,
                            DefaultSequenceNo = 3,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "sex",
                            ColumnName = "性别",
                            DefaultColumnName = "性别",
                            ColumnWidth = 50,
                            DefaultColumnWidth = 50,
                            SequenceNo = 4,
                            DefaultSequenceNo = 4,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "sexName",
                            ColumnName = "性别",
                            DefaultColumnName = "性别",
                            ColumnWidth = 50,
                            DefaultColumnWidth = 50,
                            SequenceNo = 4,
                            DefaultSequenceNo = 4,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "birthday",
                            ColumnName = "出生日期",
                            DefaultColumnName = "出生日期",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 5,
                            DefaultSequenceNo = 5,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "triageTime",
                            ColumnName = "分诊时间",
                            DefaultColumnName = "分诊时间",
                            ColumnWidth = 160,
                            DefaultColumnWidth = 160,
                            SequenceNo = 7,
                            DefaultSequenceNo = 7,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "triageTimeConsuming",
                            ColumnName = "分诊耗时",
                            DefaultColumnName = "分诊耗时",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 8,
                            DefaultSequenceNo = 8,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "actTriageLevel",
                            ColumnName = "级别",
                            DefaultColumnName = "级别",
                            ColumnWidth = 50,
                            DefaultColumnWidth = 50,
                            SequenceNo = 9,
                            DefaultSequenceNo = 9,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "triageDeptCode",
                            ColumnName = "分诊科室",
                            DefaultColumnName = "分诊科室",
                            ColumnWidth = 90,
                            DefaultColumnWidth = 90,
                            SequenceNo = 9,
                            DefaultSequenceNo = 9,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "triageTarget",
                            ColumnName = "去向",
                            DefaultColumnName = "去向",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 10,
                            DefaultSequenceNo = 10,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "otherTarget",
                            ColumnName = "其他去向",
                            DefaultColumnName = "其他去向",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 11,
                            DefaultSequenceNo = 11,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "chargeType",
                            ColumnName = "费别",
                            DefaultColumnName = "费别",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 12,
                            DefaultSequenceNo = 12,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "greenRoad",
                            ColumnName = "绿色通道",
                            DefaultColumnName = "绿色通道",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 13,
                            DefaultSequenceNo = 13,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "spO2",
                            ColumnName = "SPO2",
                            DefaultColumnName = "SPO2",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 14,
                            DefaultSequenceNo = 14,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "temp",
                            ColumnName = "体温",
                            DefaultColumnName = "体温",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 14,
                            DefaultSequenceNo = 14,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "heartRate",
                            ColumnName = "心率",
                            DefaultColumnName = "心率",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 14,
                            DefaultSequenceNo = 14,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "breathRate",
                            ColumnName = "呼吸",
                            DefaultColumnName = "呼吸",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 14,
                            DefaultSequenceNo = 14,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "sbp",
                            ColumnName = "收缩压",
                            DefaultColumnName = "收缩压",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 14,
                            DefaultSequenceNo = 14,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "sdp",
                            ColumnName = "舒张压",
                            DefaultColumnName = "舒张压",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 14,
                            DefaultSequenceNo = 14,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),

                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "groupInjury",
                            ColumnName = "群伤",
                            DefaultColumnName = "群伤",
                            ColumnWidth = 80,
                            DefaultColumnWidth = 80,
                            SequenceNo = 15,
                            DefaultSequenceNo = 15,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        // new TableSetting
                        // {
                        //     TableCode = TableCodeEnum.TriageTable.ToString(),
                        //     ColumnValue = "testResult",
                        //     ColumnName = "检验结果",
                        //     DefaultColumnName = "检验结果",
                        //     ColumnWidth = 100,
                        //     DefaultColumnWidth = 100,
                        //     SequenceNo = 17,
                        //     DefaultSequenceNo = 17,
                        //     ShowOverflowTooltip = true,
                        //     DefaultShowOverflowTooltip = true,
                        //     Visible = true,
                        //     DefaultVisible = true,
                        // }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "report",
                            ColumnName = "上报",
                            DefaultColumnName = "上报",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 18,
                            DefaultSequenceNo = 18,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "toHospitalWay",
                            ColumnName = "来院方式",
                            DefaultColumnName = "来院方式",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 19,
                            DefaultSequenceNo = 19,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "age",
                            ColumnName = "年龄",
                            DefaultColumnName = "年龄",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 20,
                            DefaultSequenceNo = 20,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "diseaseCode",
                            ColumnName = "重点病种",
                            DefaultColumnName = "重点病种",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 21,
                            DefaultSequenceNo = 21,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "remark",
                            ColumnName = "备注",
                            DefaultColumnName = "备注",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 22,
                            DefaultSequenceNo = 22,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "narration",
                            ColumnName = "主诉",
                            DefaultColumnName = "主诉",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 23,
                            DefaultSequenceNo = 23,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "rFID",
                            ColumnName = "RFID",
                            DefaultColumnName = "RFID",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 23,
                            DefaultSequenceNo = 23,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "triageUserCode",
                            ColumnName = "分诊人代码",
                            DefaultColumnName = "分诊人代码",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 23,
                            DefaultSequenceNo = 23,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "triageUserName",
                            ColumnName = "分诊人名称",
                            DefaultColumnName = "分诊人名称",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 23,
                            DefaultSequenceNo = 23,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "lastDirectionName",
                            ColumnName = "最终去向",
                            DefaultColumnName = "最终去向",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 24,
                            DefaultSequenceNo = 24,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "firstDoctorCode",
                            ColumnName = "首诊医生编码",
                            DefaultColumnName = "首诊医生编码",
                            ColumnWidth = 120,
                            DefaultColumnWidth = 100,
                            SequenceNo = 25,
                            DefaultSequenceNo = 25,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "firstDoctorName",
                            ColumnName = "首诊医生",
                            DefaultColumnName = "首诊医生",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 25,
                            DefaultSequenceNo = 25,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "lastDirectionCode",
                            ColumnName = "最终去向代码",
                            DefaultColumnName = "最终去向代码",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 26,
                            DefaultSequenceNo = 26,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "lastDirectionName",
                            ColumnName = "最终去向",
                            DefaultColumnName = "最终去向",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 27,
                            DefaultSequenceNo = 27,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "idTypeName",
                            ColumnName = "证件类型",
                            DefaultColumnName = "证件类型",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 28,
                            DefaultSequenceNo = 28,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "crowdName",
                            ColumnName = "人群",
                            DefaultColumnName = "人群",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 29,
                            DefaultSequenceNo = 29,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "gestationalWeeks",
                            ColumnName = "孕周",
                            DefaultColumnName = "孕周",
                            ColumnWidth = 45,
                            DefaultColumnWidth = 45,
                            SequenceNo = 30,
                            DefaultSequenceNo = 30,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "visitReasonName",
                            ColumnName = "就诊原因",
                            DefaultColumnName = "就诊原因",
                            ColumnWidth = 100,
                            DefaultColumnWidth = 100,
                            SequenceNo = 31,
                            DefaultSequenceNo = 31,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "bloodGlucose",
                            ColumnName = "血糖",
                            DefaultColumnName = "血糖",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 32,
                            DefaultSequenceNo = 32,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "isCancelledText",
                            ColumnName = "是否作废",
                            DefaultColumnName = "是否作废",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 32,
                            DefaultSequenceNo = 32,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "identityNo",
                            ColumnName = "身份证号",
                            DefaultColumnName = "身份证号",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 33,
                            DefaultSequenceNo = 33,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "contactsPhone",
                            ColumnName = "联系电话",
                            DefaultColumnName = "联系电话",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 34,
                            DefaultSequenceNo = 34,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "address",
                            ColumnName = "患者住址",
                            DefaultColumnName = "患者住址",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 35,
                            DefaultSequenceNo = 35,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = true,
                            DefaultVisible = true,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "contactsPerson",
                            ColumnName = "监护人",
                            DefaultColumnName = "监护人",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 36,
                            DefaultSequenceNo = 36,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "guardianIdCardNo",
                            ColumnName = "监护人身份证号码",
                            DefaultColumnName = "监护人身份证号码",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 36,
                            DefaultSequenceNo = 36,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "guardianPhone",
                            ColumnName = "监护人电话",
                            DefaultColumnName = "监护人电话",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 37,
                            DefaultSequenceNo = 37,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "pastMedicalHistory",
                            ColumnName = "既往史",
                            DefaultColumnName = "既往史",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 38,
                            DefaultSequenceNo = 38,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "allergyHistory",
                            ColumnName = "过敏史",
                            DefaultColumnName = "过敏史",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 39,
                            DefaultSequenceNo = 39,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "callingSn",
                            ColumnName = "排队号",
                            DefaultColumnName = "排队号",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 40,
                            DefaultSequenceNo = 40,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                        new TableSetting
                        {
                            TableCode = TableCodeEnum.TriageTable.ToString(),
                            ColumnValue = "typeOfVisit",
                            ColumnName = "就诊类型",
                            DefaultColumnName = "就诊类型",
                            ColumnWidth = 60,
                            DefaultColumnWidth = 60,
                            SequenceNo = 41,
                            DefaultSequenceNo = 41,
                            ShowOverflowTooltip = true,
                            DefaultShowOverflowTooltip = true,
                            Visible = false,
                            DefaultVisible = false,
                        }.SetId(_guidGenerator.Create()),
                    };
                var currentList = await _tableSettingRepository.Where(x => x.TableCode == TableCodeEnum.TriageTable.ToString()).ToListAsync();
                var insertList = list.Where(x => !currentList.Any(y => y.ColumnValue == x.ColumnValue)).ToList();
                //if (await _tableSettingRepository.CountAsync(t =>
                //    t.TableCode == TableCodeEnum.TriageTable.ToString()) <= 0)
                //{
                await _tableSettingRepository.GetDbSet().AddRangeAsync(insertList);
                await _tableSettingRepository.GetDbContext().SaveChangesAsync();
                //}
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTableSettingDataSeedAsync】" +
                             $"【创建[表格配置 - 分诊列表]种子数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建快速通道种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateFastTackDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(t =>
                    t.TriageConfigType == (int)TriageDict.FastTrack) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.FastTrack,
                            TriageConfigCode = "21231100",
                            TriageConfigName = "万丰所",
                            IsDisable = 1,
                            Sort = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.FastTrack,
                            TriageConfigCode = "21231101",
                            TriageConfigName = "黄田所",
                            IsDisable = 1,
                            Sort = 2
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.FastTrack,
                            TriageConfigCode = "21231102",
                            TriageConfigName = "塘尾所",
                            IsDisable = 1,
                            Sort = 3
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.FastTrack,
                            TriageConfigCode = "21231104",
                            TriageConfigName = "新安所",
                            IsDisable = 1,
                            Sort = 5
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;

                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning(
                    $"【PreHospitalTriageServiceDataContributor】【CreateTableSettingDataSeedAsync】【创建快速通道种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 增加性别种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateSexDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x => x.TriageConfigType == (int)TriageDict.Sex) <=
                    0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Sex,
                            TriageConfigCode = "Man",
                            TriageConfigName = "男",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Sex,
                            TriageConfigCode = "Woman",
                            TriageConfigName = "女",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Sex,
                            TriageConfigCode = "Unknown",
                            TriageConfigName = "未知",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTableSettingDataSeedAsync】" +
                             $"【增加性别种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 创建评分管理
        /// </summary>
        /// <returns></returns>
        private async Task CreateScoreManageAsync()
        {
            try
            {
                if (await _scoreManageRepository.CountAsync() <= 0)
                {
                    var list = new List<ScoreManage>
                    {
                        new ScoreManage
                        {
                            ScoreType = "Start",
                            ScoreName = "Start评分",
                            Remark = "Start评分",
                            Sort = 6
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "Ache",
                            ScoreName = "疼痛评分",
                            Remark = "疼痛评分",
                            Sort = 1
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "ESI",
                            ScoreName = "ESI评分",
                            Remark = "ESI评分",
                            Sort = 2
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "Trauma",
                            ScoreName = "创伤评分",
                            Remark = "创伤评分",
                            Sort = 3
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "Rems",
                            ScoreName = "REMS评分",
                            Remark = "REMS评分",
                            Sort = 7
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "GCS",
                            ScoreName = "GCS评分",
                            Remark = "GCS评分",
                            Sort = 4
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "Mews",
                            ScoreName = "MEWS评分",
                            Remark = "MEWS评分",
                            Sort = 5
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "Judgment",
                            ScoreName = "判定依据",
                            Remark = "判定依据",
                            Sort = 9
                        }.SetId(_guidGenerator.Create()),
                        new ScoreManage
                        {
                            ScoreType = "FALL",
                            ScoreName = "跌倒评分",
                            Remark = "跌倒评分",
                            Sort = 8
                        }.SetId(_guidGenerator.Create())
                    };

                    await _scoreManageRepository.GetDbSet().AddRangeAsync(list);
                    await _scoreManageRepository.GetDbContext().SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateScoreManageAsync】" +
                             $"【创建评分管理数据错误】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建分诊报表类型种子数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateTriageReportTypeDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.TriageReportType) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TriageReportType,
                            TriageConfigCode = "001",
                            TriageConfigName = "统计报表",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TriageReportType,
                            TriageConfigCode = "002",
                            TriageConfigName = "查询报表",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TriageReportType,
                            TriageConfigCode = "003",
                            TriageConfigName = "其它报表",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTriageReportTypeDataSeedAsync】" +
                             $"【创建分诊报表类型种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 创建就诊类型
        /// </summary>
        /// <returns></returns>
        private async Task CreateTriageTypeOfVisitDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.TypeOfVisit) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TypeOfVisit,
                            TriageConfigCode = "001",
                            TriageConfigName = "初诊",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TypeOfVisit,
                            TriageConfigCode = "002",
                            TriageConfigName = "门诊复诊",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TypeOfVisit,
                            TriageConfigCode = "003",
                            TriageConfigName = "出院复诊",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TypeOfVisit,
                            TriageConfigCode = "004",
                            TriageConfigName = "急诊复诊",
                            Sort = 4,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.TypeOfVisit,
                            TriageConfigCode = "005",
                            TriageConfigName = "手术后复诊",
                            Sort = 5,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTriageTypeOfVisitDataSeedAsync】" +
                             $"【创建分诊就诊类型种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 创建主诉数据种子
        /// </summary>
        /// <returns></returns>
        private async Task CreateNarrationDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.Narration) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "001",
                            TriageConfigName = "咽痛及咳嗽",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "002",
                            TriageConfigName = "发热",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "003",
                            TriageConfigName = "头晕",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "004",
                            TriageConfigName = "头痛",
                            Sort = 4,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "005",
                            TriageConfigName = "胸闷心悸",
                            Sort = 5,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "006",
                            TriageConfigName = "气促",
                            Sort = 6,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "007",
                            TriageConfigName = "腹痛",
                            Sort = 7,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "008",
                            TriageConfigName = "腹泻",
                            Sort = 8,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "009",
                            TriageConfigName = "呕吐",
                            Sort = 9,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "010",
                            TriageConfigName = "腰疼",
                            Sort = 10,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "011",
                            TriageConfigName = "外伤",
                            Sort = 11,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "012",
                            TriageConfigName = "犬咬伤",
                            Sort = 12,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "013",
                            TriageConfigName = "其他动物咬伤",
                            Sort = 13,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "014",
                            TriageConfigName = "手足口？",
                            Sort = 14,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Narration,
                            TriageConfigCode = "015",
                            TriageConfigName = "其他",
                            Sort = 15,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateNarrationDataSeedAsync】" +
                             $"【创建分诊主诉种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 新增意识数据种子
        /// </summary>
        /// <returns></returns>
        private async Task CreateMindDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.Mind) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Mind,
                            TriageConfigCode = "001",
                            TriageConfigName = "清楚",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Mind,
                            TriageConfigCode = "002",
                            TriageConfigName = "模糊",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Mind,
                            TriageConfigCode = "003",
                            TriageConfigName = "无反应",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateMindSeedAsync】" +
                             $"【创建分诊报表类型种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 新增费别数据种子
        /// </summary>
        /// <returns></returns>
        private async Task CreateFaberDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.Faber) <= 0)
                {
                    var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "001",
                            TriageConfigName = "新农合",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "002",
                            TriageConfigName = "城乡居民",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "003",
                            TriageConfigName = "跨省新农合",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "004",
                            TriageConfigName = "职工医保(省内)",
                            Sort = 4,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "005",
                            TriageConfigName = "自费(普通)",
                            Sort = 5,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "006",
                            TriageConfigName = "市直慢性病",
                            Sort = 6,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "007",
                            TriageConfigName = "单位记账(普通)",
                            Sort = 7,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "008",
                            TriageConfigName = "职工医保(县区)",
                            Sort = 8,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "009",
                            TriageConfigName = "离休",
                            Sort = 9,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "010",
                            TriageConfigName = "职工医保(跨省)",
                            Sort = 10,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Faber,
                            TriageConfigCode = "011",
                            TriageConfigName = "新居民社保",
                            Sort = 11,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;

                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateFaberDataSeedAsync】" +
                             $"【创建费别类型种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 生命体征种子数据
        /// </summary>
        private async Task CreateVitalSignRemarkDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.VitalSignRemark) <= 0)
                {
                    var list = new List<TriageConfig>
                    {

                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.VitalSignRemark,
                            TriageConfigCode = "002",
                            TriageConfigName = "拒测",
                            Sort = 1,
                            IsDisable = 1,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.VitalSignRemark,
                            TriageConfigCode = "003",
                            TriageConfigName = "开药",
                            Sort = 2,
                            IsDisable = 1,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.VitalSignRemark,
                            TriageConfigCode = "004",
                            TriageConfigName = "死亡",
                            Sort = 3,
                            IsDisable = 1,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.VitalSignRemark,
                            TriageConfigCode = "005",
                            TriageConfigName = "疫苗",
                            Sort = 4,
                            IsDisable = 1,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.VitalSignRemark,
                            TriageConfigCode = "006",
                            TriageConfigName = "紧急挂号",
                            Sort = 5,
                            IsDisable = 1,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy()
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateVitalSignRemarkDataSeedAsync】" +
                             $"【创建生命体征种子数据错误】【Msg：{e}】");
            }
        }


        /// <summary>
        /// 挂号模式
        /// </summary>
        /// <returns></returns>
        private async Task CreateRegisterModeDataSeedAsync()
        {
            try
            {
                if (await _registerModeRepository.CountAsync() <= 0)
                {
                    var list = new List<RegisterMode>
                    {
                       new RegisterMode(){
                           Code = "RegisterBeforeTriage",
                           Name = "挂号 --> 分诊",
                           IsActive = true,
                           Remark = "挂号 --> 分诊",
                       }.SetId(_guidGenerator.Create()),
                       new RegisterMode{
                           Code = "TriageBeforeRegister",
                           Name = "分诊 --> 挂号",
                           IsActive = false,
                           Remark = "分诊 --> 挂号",
                       }.SetId(_guidGenerator.Create()),
                    };

                    var dbContext = _registerModeRepository.GetDbContext();
                    list.ForEach(item => { dbContext.Entry(item).State = EntityState.Added; });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateVitalSignRemarkDataSeedAsync】" +
                             $"【创建生命体征种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 生命体征心电图种子数据
        /// </summary>
        private async Task CreateCardiogramDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.Cardiogram) <= 0)
                {
                    var list = new List<TriageConfig>
                    {

                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Cardiogram,
                            TriageConfigCode = "001",
                            TriageConfigName = "否",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Cardiogram,
                            TriageConfigCode = "002",
                            TriageConfigName = "是",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.Cardiogram,
                            TriageConfigCode = "003",
                            TriageConfigName = "拒测",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateVitalSignRemarkDataSeedAsync】" +
                             $"【创建生命体征种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 生命体征心电图种子数据
        /// </summary>
        private async Task CreateLastDirectionDataSeedAsync()
        {
            try
            {
                if (await _triageConfigRepository.CountAsync(x =>
                    x.TriageConfigType == (int)TriageDict.LastDirection) <= 0)
                {
                    var list = new List<TriageConfig>
                    {

                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.LastDirection,
                            TriageConfigCode = "001",
                            TriageConfigName = "回家",
                            Sort = 1,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.LastDirection,
                            TriageConfigCode = "002",
                            TriageConfigName = "转院",
                            Sort = 2,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.LastDirection,
                            TriageConfigCode = "003",
                            TriageConfigName = "转科",
                            Sort = 3,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigType = (int) TriageDict.LastDirection,
                            TriageConfigCode = "004",
                            TriageConfigName = "输液区",
                            Sort = 4,
                            IsDisable = 1,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };

                    var dbContext = _triageConfigRepository.GetDbContext();
                    list.ForEach(item =>
                    {
                        item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                        dbContext.Entry(item).State = EntityState.Added;
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateVitalSignRemarkDataSeedAsync】" +
                             $"【创建生命体征种子数据错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 就诊原因
        /// </summary>
        /// <returns></returns>
        private async Task CreateVisitReasonDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.VisitReason).ToListAsync();
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "Stomachache",
                            TriageConfigName = "腹痛",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.VisitReason
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Fever",
                            TriageConfigName = "发热",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.VisitReason
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Headache",
                            TriageConfigName = "头痛",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.VisitReason
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Injured",
                            TriageConfigName = "创伤",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.VisitReason
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Angina",
                            TriageConfigName = "心脏绞痛",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.VisitReason
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateVisitReasonDataSeedAsync】" +
                             $"【创建就诊原因】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 人群
        /// </summary>
        /// <returns></returns>
        private async Task CreateCrowdDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.Crowd).ToListAsync();
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "Normal",
                            TriageConfigName = "普通",
                            Sort = 1,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.Crowd,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Pregnant",
                            TriageConfigName = "孕妇",
                            Sort = 2,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.Crowd,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "Child",
                            TriageConfigName = "儿童",
                            Sort = 3,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.Crowd,
                            UnDeletable = true,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateCrowdDataSeedAsync】" +
                             $"【创建人群】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        /// <returns></returns>
        private async Task CreateIdTypeDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.IdType).ToListAsync();
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "01",
                            TriageConfigName = "居民身份证",
                            HisConfigCode = "01",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "02",
                            TriageConfigName = "居民户口簿",
                            HisConfigCode = "02",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "03",
                            TriageConfigName = "护照",
                            HisConfigCode = "02",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "04",
                            TriageConfigName = "军官证",
                            HisConfigCode = "04",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "05",
                            TriageConfigName = "驾驶证",
                            HisConfigCode = "05",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "06",
                            TriageConfigName = "港澳居民来往内地通行证",
                            HisConfigCode = "06",
                            Sort = 6,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "07",
                            TriageConfigName = "台湾居民来往内地通行证",
                            HisConfigCode = "07",
                            Sort = 7,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "99",
                            TriageConfigName = "其他法定有效证件",
                            HisConfigCode = "99",
                            Sort = 8,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "109",
                            TriageConfigName = "深圳市居民健康卡",
                            HisConfigCode = "109",
                            Sort = 9,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "110",
                            TriageConfigName = "健康档案",
                            HisConfigCode = "110",
                            Sort = 10,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "111",
                            TriageConfigName = "出生医学证明",
                            HisConfigCode = "111",
                            Sort = 11,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "112",
                            TriageConfigName = "深圳市社会保障卡电脑号",
                            HisConfigCode = "112",
                            Sort = 12,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "999",
                            TriageConfigName = "其他",
                            HisConfigCode = "999",
                            Sort = 13,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.IdType
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateCrowdDataSeedAsync】" +
                             $"【创建人群】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建与联系人关系
        /// </summary>
        /// <returns></returns>
        private async Task CreateSocietyRelationDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.SocietyRelation).ToListAsync();
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "0",
                            TriageConfigName = "本人或户主",
                            HisConfigCode = "0",
                            Sort = 1,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "1",
                            TriageConfigName = "配偶",
                            HisConfigCode = "1",
                            Sort = 2,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "2",
                            TriageConfigName = "子",
                            HisConfigCode = "2",
                            Sort = 3,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "3",
                            TriageConfigName = "女",
                            HisConfigCode = "3",
                            Sort = 4,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "4",
                            TriageConfigName = "孙",
                            HisConfigCode = "4",
                            Sort = 5,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "5",
                            TriageConfigName = "父母",
                            HisConfigCode = "5",
                            Sort = 6,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "6",
                            TriageConfigName = "祖父母或外祖父母",
                            HisConfigCode = "6",
                            Sort = 7,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "7",
                            TriageConfigName = "亲属",
                            HisConfigCode = "7",
                            Sort = 8,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "8",
                            TriageConfigName = "朋友",
                            HisConfigCode = "8",
                            Sort = 9,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9",
                            TriageConfigName = "同事",
                            HisConfigCode = "9",
                            Sort = 10,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "10",
                            TriageConfigName = "父子",
                            HisConfigCode = "10",
                            Sort = 11,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "11",
                            TriageConfigName = "父女",
                            HisConfigCode = "11",
                            Sort = 12,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "12",
                            TriageConfigName = "母子",
                            HisConfigCode = "12",
                            Sort = 13,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "20",
                            TriageConfigName = "儿媳女婿",
                            HisConfigCode = "20",
                            Sort = 21,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "21",
                            TriageConfigName = "母女",
                            HisConfigCode = "21",
                            Sort = 22,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "22",
                            TriageConfigName = "夫妻",
                            HisConfigCode = "22",
                            Sort = 23,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "24",
                            TriageConfigName = "兄弟姐妹",
                            HisConfigCode = "24",
                            Sort = 25,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "31",
                            TriageConfigName = "兄弟姐妹",
                            HisConfigCode = "31",
                            Sort = 32,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "32",
                            TriageConfigName = "同事",
                            HisConfigCode = "32",
                            Sort = 33,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "33",
                            TriageConfigName = "亲属",
                            HisConfigCode = "33",
                            Sort = 34,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "34",
                            TriageConfigName = "其它",
                            HisConfigCode = "34",
                            Sort = 35,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "41",
                            TriageConfigName = "朋友",
                            HisConfigCode = "41",
                            Sort = 42,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "42",
                            TriageConfigName = "母",
                            HisConfigCode = "42",
                            Sort = 43,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "43",
                            TriageConfigName = "(岳)父",
                            HisConfigCode = "43",
                            Sort = 44,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "44",
                            TriageConfigName = "(岳)母",
                            HisConfigCode = "44",
                            Sort = 45,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "51",
                            TriageConfigName = "祖父",
                            HisConfigCode = "51",
                            Sort = 52,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "52",
                            TriageConfigName = "祖母",
                            HisConfigCode = "52",
                            Sort = 53,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "53",
                            TriageConfigName = "(外)祖父",
                            HisConfigCode = "53",
                            Sort = 54,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "54",
                            TriageConfigName = "(外)祖母",
                            HisConfigCode = "54",
                            Sort = 55,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "61",
                            TriageConfigName = "兄",
                            HisConfigCode = "61",
                            Sort = 62,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "62",
                            TriageConfigName = "弟",
                            HisConfigCode = "62",
                            Sort = 63,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "63",
                            TriageConfigName = "姐",
                            HisConfigCode = "63",
                            Sort = 64,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "64",
                            TriageConfigName = "妹",
                            HisConfigCode = "64",
                            Sort = 65,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "65",
                            TriageConfigName = "公公",
                            HisConfigCode = "65",
                            Sort = 66,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "66",
                            TriageConfigName = "婆媳",
                            HisConfigCode = "66",
                            Sort = 67,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "73",
                            TriageConfigName = "本人",
                            HisConfigCode = "73",
                            Sort = 74,
                            IsDisable = 1, TriageConfigType = (int) TriageDict.SocietyRelation
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateSocietyRelationDataSeedAsync】" +
                             $"【创建关系】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 参保地
        /// </summary>
        /// <returns></returns>
        private async Task CreateInsuplcAdmdvs()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.InsuplcAdmdv).ToListAsync();
                var list = new List<TriageConfig>
                {
                };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateSocietyRelationDataSeedAsync】" +
                             $"【创建参保地】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 新增评分字典种子数据
        /// </summary>
        private async Task CreateScoreDictAsync()
        {
            try
            {
                if (await _scoreDictRepository.AnyAsync())
                {
                    return;
                }

                var list = new List<ScoreDict>();

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 1,
                    Remark = "昏迷程度以睁眼．语言．运动三者分数加总来评估，正常人的昏迷指数是满分15分， 昏迷程度越重者的昏迷指数越低分轻度昏迷:13分到14分。中度昏迷:9-12分。重度昏迷:3-8分。低于3分:因插管气切无法发声的重度昏迷者会有2T的评分。",
                    Sort = 4,
                    IsDeleted = false,
                    DisplayText = "GCS评分"
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 2,
                    Sort = 1,
                    IsDeleted = false,
                    DisplayText = "睁眼",
                    ParentId = list[0].Id
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 2,
                    Sort = 2,
                    IsDeleted = false,
                    DisplayText = "语言",
                    ParentId = list[0].Id
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 2,
                    Sort = 3,
                    IsDeleted = false,
                    DisplayText = "动作",
                    ParentId = list[0].Id
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 1,
                    IsDeleted = false,
                    DisplayText = "无睁眼",
                    ParentId = list[1].Id,
                    Grade = 1
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 2,
                    IsDeleted = false,
                    DisplayText = "疼痛刺激睁眼",
                    ParentId = list[1].Id,
                    Grade = 2
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 3,
                    IsDeleted = false,
                    DisplayText = "语言吩咐睁眼",
                    ParentId = list[1].Id,
                    Grade = 3
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 4,
                    IsDeleted = false,
                    DisplayText = "自发睁眼",
                    ParentId = list[1].Id,
                    Grade = 4
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 1,
                    IsDeleted = false,
                    DisplayText = "无发音",
                    ParentId = list[2].Id,
                    Grade = 1
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 2,
                    IsDeleted = false,
                    DisplayText = "只能发音",
                    ParentId = list[2].Id,
                    Grade = 2
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 3,
                    IsDeleted = false,
                    DisplayText = "只能说出(不适当)单词",
                    ParentId = list[2].Id,
                    Grade = 3
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 4,
                    IsDeleted = false,
                    DisplayText = "语言错乱",
                    ParentId = list[2].Id,
                    Grade = 4
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 5,
                    IsDeleted = false,
                    DisplayText = "正常交谈",
                    ParentId = list[2].Id,
                    Grade = 5
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 1,
                    IsDeleted = false,
                    DisplayText = "无反应",
                    ParentId = list[3].Id,
                    Grade = 1
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 2,
                    IsDeleted = false,
                    DisplayText = "异常伸展(去脑状态)",
                    ParentId = list[3].Id,
                    Grade = 2
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 3,
                    IsDeleted = false,
                    DisplayText = "异常屈曲(去皮层状态)",
                    ParentId = list[3].Id,
                    Grade = 3
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 4,
                    IsDeleted = false,
                    DisplayText = "对疼痛刺激屈曲反应",
                    ParentId = list[3].Id,
                    Grade = 4
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 5,
                    IsDeleted = false,
                    DisplayText = "对疼痛刺激定位反应",
                    ParentId = list[3].Id,
                    Grade = 5
                }.SetId(_guidGenerator.Create()));

                list.Add(new ScoreDict
                {
                    Category = "GCS",
                    Level = 3,
                    Sort = 6,
                    IsDeleted = false,
                    DisplayText = "按吩咐动作",
                    ParentId = list[3].Id,
                    Grade = 6
                }.SetId(_guidGenerator.Create()));

                var dbContext = _scoreDictRepository.GetDbContext();
                foreach (var item in list)
                {
                    dbContext.Entry(item).State = EntityState.Added;
                }

                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                _log.Warning($"新增评分字典种子数据错误，原因：{e}");
            }
        }

        /// <summary>
        /// 国籍
        /// </summary>
        /// <returns></returns>
        private async Task CreateCountriesDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.Country).ToListAsync();
                var list = new List<TriageConfig>
                {
                    new TriageConfig
                    {
                        TriageConfigCode = "001",
                        TriageConfigName = "中国内地",
                        HisConfigCode = "",
                        Sort = 1,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.Country,
                        UnDeletable = true,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "002",
                        TriageConfigName = "中国香港",
                        HisConfigCode = "",
                        Sort = 2,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.Country,
                        UnDeletable = true,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "003",
                        TriageConfigName = "中国澳门",
                        HisConfigCode = "",
                        Sort = 3,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.Country,
                        UnDeletable = true,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "004",
                        TriageConfigName = "中国台湾",
                        HisConfigCode = "",
                        Sort = 4,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.Country,
                        UnDeletable = true,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "005",
                        TriageConfigName = "非中国籍",
                        HisConfigCode = "",
                        Sort = 5,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.Country,
                        UnDeletable = true,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateCountriesDataSeedAsync】" +
                             $"【创建国籍】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 分诊分区 
        /// </summary>
        /// <returns></returns>
        private async Task CreateTriageAreaDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.TriageArea).ToListAsync();
                var list = new List<TriageConfig>
                {
                    new TriageConfig
                    {
                        TriageConfigCode = "001",
                        TriageConfigName = "绿区",
                        HisConfigCode = "",
                        Sort = 1,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.TriageArea,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "002",
                        TriageConfigName = "黄区",
                        HisConfigCode = "",
                        Sort = 2,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.TriageArea
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "003",
                        TriageConfigName = "红区",
                        HisConfigCode = "",
                        Sort = 3,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.TriageArea
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "004",
                        TriageConfigName = "灰区",
                        HisConfigCode = "",
                        Sort = 4,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.TriageArea
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTriageAreaDataSeedAsync】" +
                             $"【创建分诊分区】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建变更分诊原因
        /// </summary>
        /// <returns></returns>
        private async Task CreateChangeTriageResonDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.ChangeTriageReason).ToListAsync();
                var list = new List<TriageConfig>
                {
                    new TriageConfig
                    {
                        TriageConfigCode = "001",
                        TriageConfigName = "护士专业知识不足、问诊欠系统全面",
                        HisConfigCode = "",
                        Sort = 1,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "002",
                        TriageConfigName = "医生主观因素",
                        HisConfigCode = "",
                        Sort = 2,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "003",
                        TriageConfigName = "医生变更",
                        HisConfigCode = "",
                        Sort = 3,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "004",
                        TriageConfigName = "生命体征测量结果或检查结果指示",
                        HisConfigCode = "",
                        Sort = 4,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "005",
                        TriageConfigName = "过号重排",
                        HisConfigCode = "",
                        Sort = 5,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "006",
                        TriageConfigName = "特高/本院职工或家属、",
                        HisConfigCode = "",
                        Sort = 6,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "007",
                        TriageConfigName = "患者或家属隐瞒或表述不清",
                        HisConfigCode = "",
                        Sort = 7,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "008",
                        TriageConfigName = "患者或家属主观要求或坚持",
                        HisConfigCode = "",
                        Sort = 8,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "009",
                        TriageConfigName = "患者症状体征不典型",
                        HisConfigCode = "",
                        Sort = 9,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "010",
                        TriageConfigName = "患者病情交化",
                        HisConfigCode = "",
                        Sort = 10,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "011",
                        TriageConfigName = "问一症状不同疾病",
                        HisConfigCode = "",
                        Sort = 11,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "012",
                        TriageConfigName = "电脑故障/操作故障",
                        HisConfigCode = "",
                        Sort = 12,
                        IsDisable = 1, TriageConfigType = (int) TriageDict.ChangeTriageReason
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateChangeTriageReasonDataSeedAsync】" +
                             $"【创建分诊变更原因】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 创建标签设置种子数据
        /// </summary>
        private async Task CreateTagSettingsDataSeedAsync()
        {
            try
            {
                if (await _tagSettingsRepository.AnyAsync())
                {
                    return;
                }

                var list = new List<TagSettings>
                {
                    new TagSettings(_guidGenerator.Create())
                    {
                        Name = "一线",
                        IsSendMessage = false
                    },
                    new TagSettings(_guidGenerator.Create())
                    {
                        Name = "二线",
                        IsSendMessage = true
                    },
                    new TagSettings(_guidGenerator.Create())
                    {
                        Name = "院前120",
                        IsSendMessage = true,
                    },
                    new TagSettings(_guidGenerator.Create())
                    {
                        Name = "医生一线",
                        IsSendMessage = true,
                    },
                    new TagSettings(_guidGenerator.Create())
                    {
                        Name = "护士一线",
                        IsSendMessage = true,
                    },
                    new TagSettings(_guidGenerator.Create())
                    {
                        Name = "医生二线",
                        IsSendMessage = true,
                    },
                    new TagSettings(_guidGenerator.Create())
                    {
                        Name = "EICU",
                        IsSendMessage = true,
                    },
                };

                var db = _tagSettingsRepository.GetDbContext();
                foreach (var settings in list)
                {
                    db.Entry(settings).State = EntityState.Added;
                }

                if (await db.SaveChangesAsync() > 0)
                {
                    await CreateDutyTelephoneDataSeedAsync(list);
                }
            }
            catch (Exception e)
            {
                _log.Error($"创建标签设置种子数据错误，原因：{e}");
            }
        }

        /// <summary>
        /// 创建值班电话种子数据
        /// </summary>
        private async Task CreateDutyTelephoneDataSeedAsync(List<TagSettings> tagSettingsList)
        {
            try
            {
                if (await _dutyTelephoneRepository.AnyAsync())
                {
                    return;
                }

                var list = new List<DutyTelephone>
                {
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "心血管内科",
                        Sort = 1,
                        No = "51089",
                        Telephone = "19926551089",
                        GreenRoads = "",
                        IsEnabled = false,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "心血管内科",
                        Sort = 2,
                        No = "55730",
                        Telephone = "18126255730",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "二线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "应急隔离病房（板房区域）1楼护士站",
                        Sort = 3,
                        No = "57875",
                        Telephone = "19926551089",
                        GreenRoads = "",
                        IsEnabled = false,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "应急隔离病房（板房区域）2楼护士站",
                        Sort = 4,
                        No = "54605",
                        Telephone = "19129964605",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "应急隔离病房（板房区域）医护办",
                        Sort = 5,
                        No = "54886",
                        Telephone = "19129964886",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "应急隔离病房（板房区域）值班手机",
                        Sort = 6,
                        No = "52802",
                        Telephone = "19129962802",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "神经内科",
                        Sort = 7,
                        No = "50689",
                        Telephone = "19926550689",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "神经内科",
                        Sort = 8,
                        No = "50242",
                        Telephone = "18126270242",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "二线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "急诊科",
                        Sort = 8,
                        No = "55703",
                        Telephone = "18126255703",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "院前120").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "急诊科",
                        Sort = 8,
                        No = "55708",
                        Telephone = "18126255708",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "医生一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "急诊科",
                        Sort = 8,
                        No = "50939",
                        Telephone = "18123830939",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "医生二线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "急诊科",
                        Sort = 8,
                        No = "57829",
                        Telephone = "18129917829",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "护士一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "急诊科",
                        Sort = 8,
                        No = "53059",
                        Telephone = "19926453059",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "EICU").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "心血管外科",
                        Sort = 8,
                        No = "51503",
                        Telephone = "18123651503",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "心血管外科",
                        Sort = 8,
                        No = "55747",
                        Telephone = "18126255747",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "二线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "骨科",
                        Sort = 8,
                        No = "55741",
                        Telephone = "18126255741",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "一线").Id,
                    },
                    new DutyTelephone(_guidGenerator.Create())
                    {
                        Dept = "骨科",
                        Sort = 8,
                        No = "55013",
                        Telephone = "18123755013",
                        GreenRoads = "",
                        IsEnabled = true,
                        TagSettingId = tagSettingsList.First(x=>x.Name == "二线").Id,
                    }
                };

                var index = 0;
                var db = _dutyTelephoneRepository.GetDbContext();
                foreach (var item in list)
                {
                    item.Sort = index++;
                    db.Entry(item).State = EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Error($"创建值班电话种子数据错误，原因：{e}");
            }
        }

        /// <summary>
        /// 创建短信模板种子数据
        /// </summary>
        private async Task CreateTextMessageTemplateDataSeedAsync()
        {
            try
            {
                if (await _textMessageTemplateRepository.AnyAsync())
                {
                    return;
                }

                var template = new TextMessageTemplate
                {
                    IsDeleted = false,
                    Content = "【南医大深圳医院】院前急救有新的患者开通了绿色通道，请及时前往救治！\r\n" +
                              "病历号：{PatientId}\r\n" +
                              "就诊人：{PatientName}\r\n" +
                              "就诊时间：{StartTriageTime}\r\n" +
                              "预计到达：{ExpectTime}\r\n" +
                              "绿色通道：{GreenRoadName}\r\n" +
                              "开通人员：{TriageUserName}\r\n"
                };

                await _textMessageTemplateRepository.InsertAsync(template);
            }
            catch (Exception e)
            {
                _log.Error($"创建短信模板种子数据错误，原因：{e}");
            }
        }

        /// <summary>
        /// 门诊 
        /// </summary>
        /// <returns></returns>
        private async Task CreateOutpatientDepartmentDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.OutpatientDepartment).ToListAsync();
                var list = new List<TriageConfig>
                {
                    new TriageConfig
                    {
                        TriageConfigCode = "001",
                        TriageConfigName = "妇科(普通)",
                        HisConfigCode = "3",
                        ExtensionField1 = "1",
                        ExtensionField2 = "18",
                        ExtensionField3 = "pd_dlb18",
                        Sort = 1,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "002",
                        TriageConfigName = "妇科(副高)",
                        HisConfigCode = "31",
                        ExtensionField1 = "3",
                        ExtensionField2 = "17",
                        ExtensionField3 = "pd_dlb17",
                        Sort = 2,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "003",
                        TriageConfigName = "妇科(正高)",
                        HisConfigCode = "311",
                        ExtensionField1 = "3",
                        ExtensionField2 = "25",
                        ExtensionField3 = "pd_dlb25",
                        Sort = 3,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "004",
                        TriageConfigName = "妇科(特专)",
                        HisConfigCode = "3110",
                        ExtensionField1 = "2",
                        ExtensionField2 = "42",
                        ExtensionField3 = "pd_dlb42",
                        Sort = 4,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "005",
                        TriageConfigName = "妇科(复)",
                        HisConfigCode = "31100",
                        ExtensionField1 = "2",
                        ExtensionField2 = "42",
                        ExtensionField3 = "pd_dlb42",
                        Sort = 5,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "006",
                        TriageConfigName = "妇科正高(优专)",
                        HisConfigCode = "3111",
                        ExtensionField1 = "2",
                        ExtensionField2 = "25",
                        ExtensionField3 = "pd_dlb25",
                        Sort = 6,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "007",
                        TriageConfigName = "妇科(优普)",
                        HisConfigCode = "33",
                        ExtensionField1 = "1",
                        ExtensionField2 = "18",
                        ExtensionField3 = "pd_dlb18",
                        Sort = 7,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "008",
                        TriageConfigName = "妇科(优专)",
                        HisConfigCode = "34",
                        ExtensionField1 = "3",
                        ExtensionField2 = "17",
                        ExtensionField3 = "pd_dlb17",
                        Sort = 8,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "009",
                        TriageConfigName = "产科(副高)",
                        HisConfigCode = "38",
                        ExtensionField1 = "3",
                        ExtensionField2 = "17",
                        ExtensionField3 = "pd_dlb17",
                        Sort = 9,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0010",
                        TriageConfigName = "产科(正高)",
                        HisConfigCode = "381",
                        ExtensionField1 = "2",
                        ExtensionField2 = "25",
                        ExtensionField3 = "pd_dlb25",
                        Sort = 10,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0011",
                        TriageConfigName = "产科(普通)",
                        HisConfigCode = "39",
                        ExtensionField1 = "1",
                        ExtensionField2 = "18",
                        ExtensionField3 = "pd_dlb18",
                        Sort = 11,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0012",
                        TriageConfigName = "妇产科(简易)",
                        HisConfigCode = "399",
                        ExtensionField1 = "1",
                        ExtensionField2 = "1809",
                        ExtensionField3 = "pd_dlb1809",
                        Sort = 12,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0013",
                        TriageConfigName = "耳鼻喉科(普)",
                        HisConfigCode = "4",
                        ExtensionField1 = "1",
                        ExtensionField2 = "14",
                        ExtensionField3 = "pd_dlb14",
                        Sort = 13,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0014",
                        TriageConfigName = "耳鼻喉科(副高)",
                        HisConfigCode = "41",
                        ExtensionField1 = "3",
                        ExtensionField2 = "13",
                        ExtensionField3 = "pd_dlb13",
                        Sort = 14,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0015",
                        TriageConfigName = "耳鼻喉科(正高)",
                        HisConfigCode = "411",
                        ExtensionField1 = "3",
                        ExtensionField2 = "43",
                        ExtensionField3 = "pd_dlb43",
                        Sort = 15,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0016",
                        TriageConfigName = "耳鼻喉科(病房)",
                        HisConfigCode = "4111",
                        ExtensionField1 = "1",
                        ExtensionField2 = "614",
                        ExtensionField3 = "pd_dlb614",
                        Sort = 16,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0017",
                        TriageConfigName = "耳鼻喉科(优普)",
                        HisConfigCode = "43",
                        ExtensionField1 = "1",
                        ExtensionField2 = "14",
                        ExtensionField3 = "pd_dlb14",
                        Sort = 17,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0018",
                        TriageConfigName = "耳鼻喉科(优副)",
                        HisConfigCode = "44",
                        ExtensionField1 = "3",
                        ExtensionField2 = "13",
                        ExtensionField3 = "pd_dlb13",
                        Sort = 18,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0019",
                        TriageConfigName = "耳鼻喉科(优正)",
                        HisConfigCode = "45",
                        ExtensionField1 = "3",
                        ExtensionField2 = "43",
                        ExtensionField3 = "pd_dlb43",
                        Sort = 19,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0020",
                        TriageConfigName = "眼科(普)",
                        HisConfigCode = "5",
                        ExtensionField1 = "1",
                        ExtensionField2 = "12",
                        ExtensionField3 = "pd_dlb12",
                        Sort = 20,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0021",
                        TriageConfigName = "眼科(副高)",
                        HisConfigCode = "51",
                        ExtensionField1 = "3",
                        ExtensionField2 = "11",
                        ExtensionField3 = "pd_dlb11",
                        Sort = 21,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0022",
                        TriageConfigName = "眼科(正高)",
                        HisConfigCode = "511",
                        ExtensionField1 = "3",
                        ExtensionField2 = "44",
                        ExtensionField3 = "pd_dlb44",
                        Sort = 22,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0023",
                        TriageConfigName = "眼科(优普)",
                        HisConfigCode = "53",
                        ExtensionField1 = "1",
                        ExtensionField2 = "12",
                        ExtensionField3 = "pd_dlb12",
                        Sort = 23,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0024",
                        TriageConfigName = "眼科(优专副高)",
                        HisConfigCode = "54",
                        ExtensionField1 = "3",
                        ExtensionField2 = "11",
                        ExtensionField3 = "pd_dlb11",
                        Sort = 24,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0025",
                        TriageConfigName = "眼科(正高优专)",
                        HisConfigCode = "55",
                        ExtensionField1 = "3",
                        ExtensionField2 = "44",
                        ExtensionField3 = "pd_dlb44",
                        Sort = 25,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0026",
                        TriageConfigName = "皮肤科(普)",
                        HisConfigCode = "7",
                        ExtensionField1 = "1",
                        ExtensionField2 = "10",
                        ExtensionField3 = "pd_dlb10",
                        Sort = 26,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0027",
                        TriageConfigName = "皮肤科(副高)",
                        HisConfigCode = "71",
                        ExtensionField1 = "3",
                        ExtensionField2 = "9",
                        ExtensionField3 = "pd_dlb9",
                        Sort = 27,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0028",
                        TriageConfigName = "皮肤科(正高)",
                        HisConfigCode = "711",
                        ExtensionField1 = "3",
                        ExtensionField2 = "41",
                        ExtensionField3 = "pd_dlb41",
                        Sort = 28,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0029",
                        TriageConfigName = "皮肤科(优普)",
                        HisConfigCode = "73",
                        ExtensionField1 = "1",
                        ExtensionField2 = "10",
                        ExtensionField3 = "pd_dlb10",
                        Sort = 29,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0030",
                        TriageConfigName = "皮肤科(副高优专)",
                        HisConfigCode = "74",
                        ExtensionField1 = "3",
                        ExtensionField2 = "9",
                        ExtensionField3 = "pd_dlb9",
                        Sort = 30,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0031",
                        TriageConfigName = "皮肤科(正高优专)",
                        HisConfigCode = "75",
                        ExtensionField1 = "3",
                        ExtensionField2 = "41",
                        ExtensionField3 = "pd_dlb41",
                        Sort = 31,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0032",
                        TriageConfigName = "儿科(普)",
                        HisConfigCode = "8",
                        ExtensionField1 = "1",
                        ExtensionField2 = "8",
                        ExtensionField3 = "pd_dlb8",
                        Sort = 32,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0033",
                        TriageConfigName = "儿科(副高)",
                        HisConfigCode = "81",
                        ExtensionField1 = "3",
                        ExtensionField2 = "7",
                        ExtensionField3 = "pd_dlb7",
                        Sort = 33,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0034",
                        TriageConfigName = "儿科(正高)",
                        HisConfigCode = "811",
                        ExtensionField1 = "3",
                        ExtensionField2 = "28",
                        ExtensionField3 = "pd_dlb28",
                        Sort = 34,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0035",
                        TriageConfigName = "儿科(优普)",
                        HisConfigCode = "83",
                        ExtensionField1 = "1",
                        ExtensionField2 = "8",
                        ExtensionField3 = "pd_dlb8",
                        Sort = 35,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0036",
                        TriageConfigName = "儿科(副高优专)",
                        HisConfigCode = "84",
                        ExtensionField1 = "3",
                        ExtensionField2 = "7",
                        ExtensionField3 = "pd_dlb7",
                        Sort = 36,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0037",
                        TriageConfigName = "儿科(正高优专)",
                        HisConfigCode = "85",
                        ExtensionField1 = "3",
                        ExtensionField2 = "28",
                        ExtensionField3 = "pd_dlb28",
                        Sort = 37,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0038",
                        TriageConfigName = "口腔科(普)",
                        HisConfigCode = "10",
                        ExtensionField1 = "1",
                        ExtensionField2 = "24",
                        ExtensionField3 = "pd_dlb24",
                        Sort = 38,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0039",
                        TriageConfigName = "口腔科(副高)",
                        HisConfigCode = "101",
                        ExtensionField1 = "3",
                        ExtensionField2 = "23",
                        ExtensionField3 = "pd_dlb23",
                        Sort = 39,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0040",
                        TriageConfigName = "口腔科(正高)",
                        HisConfigCode = "1011",
                        ExtensionField1 = "3",
                        ExtensionField2 = "30",
                        ExtensionField3 = "pd_dlb30",
                        Sort = 40,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0041",
                        TriageConfigName = "口腔科(优普)",
                        HisConfigCode = "103",
                        ExtensionField1 = "1",
                        ExtensionField2 = "24",
                        ExtensionField3 = "pd_dlb24",
                        Sort = 41,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0042",
                        TriageConfigName = "口腔科(副高优专)",
                        HisConfigCode = "104",
                        ExtensionField1 = "3",
                        ExtensionField2 = "23",
                        ExtensionField3 = "pd_dlb23",
                        Sort = 42,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0043",
                        TriageConfigName = "口腔科(正高优专)",
                        HisConfigCode = "105",
                        ExtensionField1 = "2",
                        ExtensionField2 = "30",
                        ExtensionField3 = "pd_dlb30",
                        Sort = 43,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                    new TriageConfig
                    {
                        TriageConfigCode = "0044",
                        TriageConfigName = "皮肤科特诊",
                        HisConfigCode = "26",
                        ExtensionField1 = "1",
                        ExtensionField2 = "53",
                        ExtensionField3 = "pd_dlb53",
                        Sort = 44,
                        IsDisable = 1,
                        TriageConfigType = (int)TriageDict.OutpatientDepartment,
                    }.SetId(_guidGenerator.Create()).GetNamePy(),
                };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateTriageAreaDataSeedAsync】" +
                             $"【创建门诊】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// 转诊限制
        /// </summary>
        /// <returns></returns>
        private async Task CreateReferralLimitDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.ReferralLimit).ToListAsync();
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "001",
                            TriageConfigName = "妇科",
                            HisConfigCode = "",
                            Sort = 1,
                            IsDisable = 1,
                            ExtraCode="",
                            TriageConfigType = (int) TriageDict.ReferralLimit,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "002",
                            TriageConfigName = "产科",
                            HisConfigCode = "",
                            Sort = 2,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.ReferralLimit,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateReferralLimitDataSeedAsync】" +
                             $"【创建转诊限制】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }



        /// <summary>
        /// 特约记账类型
        /// </summary>
        /// <returns></returns>
        private async Task CreateSpecialAccountTypeDataSeedAsync()
        {
            try
            {
                var currentList = await this._triageConfigRepository.Where(x => x.TriageConfigType == (int)TriageDict.SpecialAccountType).ToListAsync();
                var list = new List<TriageConfig>
                    {
                        new TriageConfig
                        {
                            TriageConfigCode = "1001",
                            TriageConfigName = "龙岗区保健办",
                            HisConfigCode = "",
                            Sort = 1,
                            IsDisable = 1,
                            ExtraCode="",
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9015",
                            TriageConfigName = "东部高速公路大队",
                            HisConfigCode = "",
                            Sort = 2,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9016",
                            TriageConfigName = "梅毒孕妇及新生儿免费治疗",
                            HisConfigCode = "",
                            Sort = 3,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9017",
                            TriageConfigName = "胸痛优先",
                            HisConfigCode = "",
                            Sort = 4,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9018",
                            TriageConfigName = "救助站",
                            HisConfigCode = "",
                            Sort = 5,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9019",
                            TriageConfigName = "新冠肺炎筛查",
                            HisConfigCode = "",
                            Sort = 6,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "1028",
                            TriageConfigName = "地贫筛查",
                            HisConfigCode = "",
                            Sort = 7,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9001",
                            TriageConfigName = "门诊病人欠费",
                            HisConfigCode = "",
                            Sort = 8,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9020",
                            TriageConfigName = "驿站欠费",
                            HisConfigCode = "",
                            Sort = 9,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "1032",
                            TriageConfigName = "计生记账",
                            HisConfigCode = "",
                            Sort = 10,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9021",
                            TriageConfigName = "看守所",
                            HisConfigCode = "",
                            Sort = 11,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9022",
                            TriageConfigName = "兵检",
                            HisConfigCode = "",
                            Sort = 12,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9023",
                            TriageConfigName = "血透患者核酸检测记帐",
                            HisConfigCode = "",
                            Sort = 13,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9024",
                            TriageConfigName = "GCP",
                            HisConfigCode = "",
                            Sort = 14,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "1023",
                            TriageConfigName = "铅中毒",
                            HisConfigCode = "",
                            Sort = 15,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9002",
                            TriageConfigName = "绿色通道",
                            HisConfigCode = "",
                            Sort = 16,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9003",
                            TriageConfigName = "治安案件欠费",
                            HisConfigCode = "",
                            Sort = 17,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9004",
                            TriageConfigName = "交通意外欠费",
                            HisConfigCode = "",
                            Sort = 18,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9005",
                            TriageConfigName = "一线签字",
                            HisConfigCode = "",
                            Sort = 19,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9006",
                            TriageConfigName = "弃婴",
                            HisConfigCode = "",
                            Sort = 20,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9007",
                            TriageConfigName = "三无人员",
                            HisConfigCode = "",
                            Sort = 21,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9010",
                            TriageConfigName = "五套班子",
                            HisConfigCode = "",
                            Sort = 22,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9011",
                            TriageConfigName = "门诊欠费",
                            HisConfigCode = "",
                            Sort = 23,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9012",
                            TriageConfigName = "本院二保",
                            HisConfigCode = "",
                            Sort = 24,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9013",
                            TriageConfigName = "区属二保",
                            HisConfigCode = "",
                            Sort = 25,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "9014",
                            TriageConfigName = "医疗纠纷",
                            HisConfigCode = "",
                            Sort = 26,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                        new TriageConfig
                        {
                            TriageConfigCode = "504",
                            TriageConfigName = "医联体转诊",
                            HisConfigCode = "",
                            Sort = 27,
                            IsDisable = 1,
                            TriageConfigType = (int) TriageDict.SpecialAccountType,
                        }.SetId(_guidGenerator.Create()).GetNamePy(),
                    };
                list.ForEach(item =>
                {
                    item.TriageConfigCode = (TriageDict)item.TriageConfigType + "_" + item.TriageConfigCode;
                });

                var dbContext = _triageConfigRepository.GetDbContext();
                var insertList = list.Where(x => !currentList.Any(y => x.TriageConfigCode == y.TriageConfigCode));
                await _triageConfigRepository.GetDbSet().AddRangeAsync(insertList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.Warning("【PreHospitalTriageServiceDataContributor】【CreateSpecialAccountTypeDataSeedAsync】" +
                             $"【创建特约记账类型】【Msg：{e}】");
                await Task.CompletedTask;
            }
        }
    }
}