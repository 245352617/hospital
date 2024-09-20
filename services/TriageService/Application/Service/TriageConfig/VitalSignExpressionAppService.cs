using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 生命体征评分表达式接口
    /// </summary>
    [Auth("VitalSignExpression")]
    [Authorize]
    public class VitalSignExpressionAppService : ApplicationService, IVitalSignExpressionAppService
    {
        private readonly IRepository<VitalSignExpression> _vitalSignExpressionRepository;
        private readonly NLogHelper _log;
        private readonly ICapPublisher _capPublisher;


        public VitalSignExpressionAppService(IRepository<VitalSignExpression> vitalSignExpressionRepository,
            NLogHelper log, ICapPublisher capPublisher)
        {
            _vitalSignExpressionRepository = vitalSignExpressionRepository;
            _log = log;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 获取生命体征评分表达式集合
        /// </summary>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<VitalSignExpressionDto>>> GetVitalSignExpressionListAsync()
        {
            try
            {
                var model = await _vitalSignExpressionRepository.GetListAsync();
                var dto = model.BuildAdapter().AdaptToType<List<VitalSignExpressionDto>>();

                return JsonResult<List<VitalSignExpressionDto>>.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.Warning("【VitalSignExpressionService】【GetVitalSignExpressionListAsync】" +
                             $"【获取生命体征评分表达式集合错误】【Msg：{e}】");
                return JsonResult<List<VitalSignExpressionDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取单个生命体征评分表达式
        /// </summary>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<VitalSignExpressionDto>> GetVitalSignExpressionAsync(Guid id)
        {
            try
            {
                var model = await _vitalSignExpressionRepository.GetAsync(x => x.Id == id);
                var dto = model.BuildAdapter().AdaptToType<VitalSignExpressionDto>();
                return JsonResult<VitalSignExpressionDto>.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.Warning("【VitalSignExpressionService】【GetVitalSignExpressionAsync】" +
                             $"【获取单个生命体征评分表达式错误】【Msg：{e}】");
                return JsonResult<VitalSignExpressionDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 更新生命体征评分表达式
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateVitalSignExpressionAsync(Guid id, CreateOrUpdateVitalSignExpressionDto dto)
        {
            try
            {
                _log.Info("【VitalSignExpressionService】【UpdateVitalSignExpressionAsync】" +
                          "【更新生命体征评分表达式开始】");
                var model = await _vitalSignExpressionRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (model != null)
                {
                    model.NdLevelExpression = dto.NdLevelExpression;
                    model.RdLevelExpression = dto.RdLevelExpression;
                    model.StLevelExpression = dto.StLevelExpression;
                    model.ThALevelExpression = dto.ThALevelExpression;
                    model.ThBLevelExpression = dto.ThBLevelExpression;
                    model.ModUser = CurrentUser.UserName;
                    await _vitalSignExpressionRepository.UpdateAsync(model);
                    _log.Info("【VitalSignExpressionService】【UpdateVitalSignExpressionAsync】" +
                              "【更新生命体征评分表达式结束】");
                    //同步到急诊masterdata
                    await _capPublisher.PublishAsync("masterdata.vitalSignExpression.save.from.triage",
                        model.BuildAdapter().AdaptToType<VitalSignExpressionDto>());

                    return JsonResult.Ok();
                }

                _log.Error("【VitalSignExpressionService】【UpdateVitalSignExpressionAsync】" +
                           "【更新生命体征评分表达式失败】【Msg：不存在该评分表达式】");
                return JsonResult.Fail("不存在该评分表达式");
            }
            catch (Exception e)
            {
                _log.Warning("【VitalSignExpressionService】【UpdateVitalSignExpressionAsync】" +
                             $"【更新生命体征评分表达式错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 新增生命体征评分表达式
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        public async Task<JsonResult> CreateVitalSignExpressionAsync(CreateOrUpdateVitalSignExpressionDto dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<VitalSignExpression>();
                model.DefaultNdLevelExpression = dto.NdLevelExpression;
                model.DefaultRdLevelExpression = dto.RdLevelExpression;
                model.DefaultStLevelExpression = dto.StLevelExpression;
                model.DefaultThALevelExpression = dto.ThALevelExpression;
                model.DefaultThBLevelExpression = dto.ThBLevelExpression;
                model.AddUser = CurrentUser.UserName;
                await _vitalSignExpressionRepository.InsertAsync(model.SetId(GuidGenerator.Create()));
                //同步到急诊masterdata
                await _capPublisher.PublishAsync("masterdata.vitalSignExpression.save.from.triage",
                    model.BuildAdapter().AdaptToType<VitalSignExpressionDto>());

                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning("【VitalSignExpressionService】【CreateVitalSignExpressionAsync】" +
                             $"【新增生命体征评分表达式错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 一键重置生命体征评分表达式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> ResetAllVitalSignExpressionAsync(ResetVitalSignExpressionDto input)
        {
            try
            {
                var model = await _vitalSignExpressionRepository
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == input.Id);
                model.NdLevelExpression = model.DefaultNdLevelExpression;
                model.RdLevelExpression = model.DefaultRdLevelExpression;
                model.StLevelExpression = model.DefaultStLevelExpression;
                model.ThALevelExpression = model.DefaultThALevelExpression;
                model.ThBLevelExpression = model.DefaultThBLevelExpression;
                await _vitalSignExpressionRepository.UpdateAsync(model);
                //同步到急诊masterdata
                await _capPublisher.PublishAsync("masterdata.vitalSignExpression.save.from.triage",
                    model.BuildAdapter().AdaptToType<VitalSignExpressionDto>());

                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning("【VitalSignExpressionService】【ResetAllVitalSignExpressionAsync】" +
                             $"【一键重置生命体征评分表达式错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 单个重置生命体征评分表达式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> ResetVitalSignExpressionAsync(ResetVitalSignExpressionDto input)
        {
            try
            {
                var model = await _vitalSignExpressionRepository
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == input.Id);
                switch (input.Level)
                {
                    case "NdLevelExpression":
                        model.NdLevelExpression = model.DefaultNdLevelExpression;
                        break;
                    case "RdLevelExpression":
                        model.RdLevelExpression = model.DefaultRdLevelExpression;
                        break;
                    case "StLevelExpression":
                        model.StLevelExpression = model.DefaultStLevelExpression;
                        break;
                    case "ThALevelExpression":
                        model.ThALevelExpression = model.DefaultThALevelExpression;
                        break;
                    case "ThBLevelExpression":
                        model.ThBLevelExpression = model.DefaultThBLevelExpression;
                        break;
                }

                await _vitalSignExpressionRepository.UpdateAsync(model);
                //同步到急诊masterdata
                await _capPublisher.PublishAsync("masterdata.vitalSignExpression.save.from.triage",
                    model.BuildAdapter().AdaptToType<VitalSignExpressionDto>());

                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning("【VitalSignExpressionService】【ResetVitalSignExpressionAsync】" +
                             $"【单个重置生命体征评分表达式错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除生命体征评分表达式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteVitalSignExpressionAsync(Guid id)
        {
            try
            {
                var model = await _vitalSignExpressionRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (model != null)
                {
                    model.DeleteUser = CurrentUser.UserName;
                    await _vitalSignExpressionRepository.DeleteAsync(model);

                    //同步删除到急诊masterdata
                    await _capPublisher.PublishAsync("masterdata.vitalSignExpression.delete.from.triage",
                        model.ItemName);

                    return JsonResult.Ok();
                }

                _log.Error("【VitalSignExpressionService】【DeleteVitalSignExpressionAsync】【删除生命体征评分表达式失败】【Msg：表达式已经被删除】");
                return JsonResult.Fail("表达式已经被删除");
            }
            catch (Exception e)
            {
                _log.Warning("【VitalSignExpressionService】【DeleteVitalSignExpressionAsync】" +
                             $"【删除生命体征评分表达式错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }


        /// <summary>
        /// 获取生命体征评分结果
        /// </summary>
        /// <returns></returns>
        [Auth("VitalSignExpression" + PermissionDefinition.Separator + "GradeResult", "计算评分结果")]
        public async Task<JsonResult<VitalSignGradeResultDto>> GetGradeResultAsync(VitalSignGradeInput input)
        {
            try
            {
                var res = new VitalSignGradeResultDto();
                var jr = await GetVitalSignExpressionListAsync();
                if (jr.Data != null && jr.Data.Count > 0)
                {
                    var dt = new DataTable();
                    var triageConfigAppService = ServiceProvider.GetRequiredService<ITriageConfigAppService>();
                    var dicts = await triageConfigAppService.GetTriageConfigByRedisAsync(
                        input: ((int)TriageDict.TriageLevel).ToString());
                    var gradeLevel = "";
                    foreach (var dto in jr.Data)
                    {
                        switch (dto.ItemName)
                        {
                            case "DBP":
                                if (!input.Dbp.IsNullOrWhiteSpace())
                                {
                                    var currentLevel = ComputeExpression(input.Dbp, dto, dt);
                                    if (!currentLevel.IsNullOrWhiteSpace())
                                    {
                                        ComputeLastTriageLevel(currentLevel, dicts[TriageDict.TriageLevel.ToString()],
                                            ref gradeLevel, out var color);
                                        res.DbpColor = color;
                                    }
                                }

                                break;
                            case "Breath":
                                if (!input.Breath.IsNullOrWhiteSpace())
                                {
                                    var currentLevel = ComputeExpression(input.Breath, dto, dt);
                                    if (!currentLevel.IsNullOrWhiteSpace())
                                    {
                                        ComputeLastTriageLevel(currentLevel, dicts[TriageDict.TriageLevel.ToString()],
                                            ref gradeLevel, out var color);
                                        res.BreathColor = color;
                                    }
                                }

                                break;
                            case "SBP":
                                if (!input.Sbp.IsNullOrWhiteSpace())
                                {
                                    var currentLevel = ComputeExpression(input.Sbp, dto, dt);
                                    if (!currentLevel.IsNullOrWhiteSpace())
                                    {
                                        ComputeLastTriageLevel(currentLevel, dicts[TriageDict.TriageLevel.ToString()],
                                            ref gradeLevel, out var color);
                                        res.SbpColor = color;
                                    }
                                }

                                break;
                            case "Temp":
                                if (!input.Temp.IsNullOrWhiteSpace())
                                {
                                    var currentLevel = ComputeExpression(input.Temp, dto, dt);
                                    if (!currentLevel.IsNullOrWhiteSpace())
                                    {
                                        ComputeLastTriageLevel(currentLevel, dicts[TriageDict.TriageLevel.ToString()],
                                            ref gradeLevel, out var color);
                                        res.TempColor = color;
                                    }
                                }

                                break;
                            case "HeartRate":
                                if (!input.HeartRate.IsNullOrWhiteSpace())
                                {
                                    var currentLevel = ComputeExpression(input.HeartRate, dto, dt);
                                    if (!currentLevel.IsNullOrWhiteSpace())
                                    {
                                        ComputeLastTriageLevel(currentLevel, dicts[TriageDict.TriageLevel.ToString()],
                                            ref gradeLevel, out var color);
                                        res.HeartRateColor = color;
                                    }
                                }

                                break;
                            case "SPO2":
                                if (!input.Spo2.IsNullOrWhiteSpace())
                                {
                                    var currentLevel = ComputeExpression(input.Spo2, dto, dt);
                                    if (!currentLevel.IsNullOrWhiteSpace())
                                    {
                                        ComputeLastTriageLevel(currentLevel, dicts[TriageDict.TriageLevel.ToString()],
                                            ref gradeLevel, out var color);
                                        res.Spo2Color = color;
                                    }
                                }

                                break;
                        }
                    }

                    var currentTriageLevel = input.CurrentTriageLevel;
                    ComputeLastTriageLevel(gradeLevel, dicts[TriageDict.TriageLevel.ToString()],
                        ref currentTriageLevel, out var colorL);

                    res.TriageLevel = currentTriageLevel;
                    res.TriageLevelName = dicts[TriageDict.TriageLevel.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == currentTriageLevel)?.TriageConfigName;

                    _log.Info("获取生命体征评分结果成功");
                    return JsonResult<VitalSignGradeResultDto>.Ok(data: res);
                }

                _log.Info("获取生命体征评分结果失败，原因：不存在生命体征评分规则配置");
                return JsonResult<VitalSignGradeResultDto>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.Error($"获取生命体征评分结果错误，原因：{e}");
                return JsonResult<VitalSignGradeResultDto>.Fail(e.Message);
            }
        }

        #region 非 API

        /// <summary>
        /// 计算表达式
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dto"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string ComputeExpression(string value, VitalSignExpressionDto dto, DataTable dt)
        {
            var gradeLevel = "";
            if (!dto.StLevelExpression.IsNullOrWhiteSpace() && Convert.ToBoolean(dt.Compute(dto.StLevelExpression.Replace("value", value).Replace("&&", "and").Replace("||", "or"), "")))
            {
                gradeLevel = TriageLevel.FirstLv.GetDescriptionByEnum();
            }

            if (gradeLevel.IsNullOrWhiteSpace() && !dto.NdLevelExpression.IsNullOrWhiteSpace() && Convert.ToBoolean(dt.Compute(dto.NdLevelExpression.Replace("value", value).Replace("&&", "and").Replace("||", "or"), "")))
            {
                gradeLevel = TriageLevel.SecondLv.GetDescriptionByEnum();
            }

            if (dto.ItemName == "DBP")
            {
                _log.Info($"res：{dto.RdLevelExpression.Replace("value", value).Replace("&&", "and").Replace("||", "or")}");
            }

            if (gradeLevel.IsNullOrWhiteSpace() && !dto.RdLevelExpression.IsNullOrWhiteSpace() && Convert.ToBoolean(dt.Compute(dto.RdLevelExpression.Replace("value", value).Replace("&&", "and").Replace("||", "or"), "")))
            {
                gradeLevel = TriageLevel.ThirdLv.GetDescriptionByEnum();
            }

            if (gradeLevel.IsNullOrWhiteSpace() && !dto.ThALevelExpression.IsNullOrWhiteSpace() && Convert.ToBoolean(dt.Compute(dto.ThALevelExpression.Replace("value", value).Replace("&&", "and").Replace("||", "or"), "")))
            {
                gradeLevel = TriageLevel.FourthALv.GetDescriptionByEnum();
            }

            if (gradeLevel.IsNullOrWhiteSpace() && !dto.ThBLevelExpression.IsNullOrWhiteSpace() && Convert.ToBoolean(dt.Compute(dto.ThBLevelExpression.Replace("value", value).Replace("&&", "and").Replace("||", "or"),
                    "")))
            {
                gradeLevel = TriageLevel.FourthBLv.GetDescriptionByEnum();
            }

            return gradeLevel;
        }

        /// <summary>
        /// 比较分诊级别大小返回最大级别与颜色
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <param name="dicts"></param>
        /// <param name="lastLevel"></param>
        /// <param name="color"></param>
        private void ComputeLastTriageLevel(string currentLevel, List<TriageConfigDto> dicts, ref string lastLevel,
            out string color)
        {
            color = "";
            var dict = dicts.FirstOrDefault(x => x.TriageConfigCode == currentLevel);
            if (dict != null)
            {
                color = dict.ExtensionField1;
            }

            if (lastLevel.IsNullOrWhiteSpace())
            {
                lastLevel = currentLevel;
            }
            else
            {
                var level = lastLevel;
                var gradeLevelDict = dicts.FirstOrDefault(x => x.TriageConfigCode == level);
                if (gradeLevelDict != null)
                {
                    if (dict != null && dict.Sort <= gradeLevelDict.Sort)
                    {
                        lastLevel = currentLevel;
                    }
                }
            }
        }

        #endregion
    }
}