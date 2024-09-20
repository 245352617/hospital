using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YiJian.Common;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.Nursing.Config;
using YiJian.Nursing.Pda;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes.Entities;
using YiJian.Patient;
using YiJian.Rpc;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 护理服务：对接医嘱服务API
    /// </summary>
    [Authorize]
    public class RecipeAppService : NursingAppService, IRecipeAppService, ICapSubscribe
    {
        private readonly ILogger<RecipeAppService> _logger;
        private readonly NursingRecipeManager _nursingRecipeManager;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeHistoryRepository _recipeHistoryRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly IPacsRepository _pacsRepository;
        private readonly ILisRepository _lisRepository;
        private readonly ITreatRepository _treatRepository;
        private readonly IRecipeExecRepository _recipeExecRepository;
        private readonly INursingConfigRepository _nursingConfigRepository;
        private readonly IOwnMedicineRepository _ownMedicineRepository;
        private readonly PdaAppService _pdaAppService;
        private readonly GrpcClient _grpcClient;
        private readonly PatientAppService _patientAppService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="nursingRecipeManager"></param>
        /// <param name="recipeRepository"></param>
        /// <param name="recipeHistoryRepository"></param>
        /// <param name="prescribeRepository"></param>
        /// <param name="pacsRepository"></param>
        /// <param name="lisRepository"></param>
        /// <param name="treatRepository"></param>
        /// <param name="grpcClient"></param>
        /// <param name="recipeExecRepository"></param>
        /// <param name="nursingConfigRepository"></param>
        /// <param name="pdaAppService"></param>
        /// <param name="patientAppService"></param>
        /// <param name="ownMedicineRepository"></param>
        public RecipeAppService(ILogger<RecipeAppService> logger
            , NursingRecipeManager nursingRecipeManager
            , IRecipeRepository recipeRepository
            , IRecipeHistoryRepository recipeHistoryRepository
            , IPrescribeRepository prescribeRepository
            , IPacsRepository pacsRepository
            , ILisRepository lisRepository
            , ITreatRepository treatRepository
            , GrpcClient grpcClient
            , IRecipeExecRepository recipeExecRepository
            , INursingConfigRepository nursingConfigRepository
            , PdaAppService pdaAppService
            , PatientAppService patientAppService
            , IOwnMedicineRepository ownMedicineRepository)
        {
            _logger = logger;
            _nursingRecipeManager = nursingRecipeManager;
            _recipeRepository = recipeRepository;
            _recipeHistoryRepository = recipeHistoryRepository;
            _prescribeRepository = prescribeRepository;
            _pacsRepository = pacsRepository;
            _lisRepository = lisRepository;
            _treatRepository = treatRepository;
            _recipeExecRepository = recipeExecRepository;
            _nursingConfigRepository = nursingConfigRepository;
            _ownMedicineRepository = ownMedicineRepository;
            _pdaAppService = pdaAppService;
            _grpcClient = grpcClient;
            _patientAppService = patientAppService;
        }

        /// <summary>
        /// 同步医嘱消息接口
        /// </summary>
        /// <param name="etos"></param>
        /// <returns></returns>
        [CapSubscribe("submitadvice.recipeservice.to.nursingservice")]
        public async Task SubscribeDoctorAdviceCreatedAsync(List<SubmitDoctorsAdviceEto> etos)
        {
            using IUnitOfWork uow = UnitOfWorkManager.Begin();
            try
            {
                Guid? piId = etos.FirstOrDefault()?.DoctorsAdvice.PIID;
                if (!piId.HasValue)
                {
                    _logger.LogError("没有患者Id");
                    return;
                }
                AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(piId.Value);
                if (admissionRecordDto != null && !string.IsNullOrEmpty(admissionRecordDto.GreenRoad))
                {
                    foreach (SubmitDoctorsAdviceEto item in etos)
                    {
                        item.DoctorsAdvice.PayStatus = EPayStatus.HavePaid;
                    }
                }

                List<Recipe> recipes = await RecipeCreatedAsync(etos, true);
                await uow.SaveChangesAsync();

                await SplitAsync(recipes, admissionRecordDto);
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// 同步自备药消息接口
        /// </summary>
        /// <param name="ownMedicines"></param>
        /// <returns></returns>
        [CapSubscribe("ownmedicine.recipeservice.to.nursingservice")]
        public async Task SubscribeDoctorAdviceOwnmedicineCreatedAsync(List<OwnMedicineEto> ownMedicines)
        {
            var uow = UnitOfWorkManager.Begin();
            try
            {
                if (ownMedicines == null || !ownMedicines.Any())
                {
                    throw new ArgumentNullException(typeof(OwnMedicineEto).Name);
                }

                IEnumerable<int> ownMedicinesIds = ownMedicines.Select(x => x.Id);
                List<int> existIds = await (await _ownMedicineRepository.GetQueryableAsync()).Where(x => ownMedicinesIds.Contains(x.OwnMedicineId)).Select(x => x.OwnMedicineId).ToListAsync();

                ownMedicines = ownMedicines.Where(x => !existIds.Contains(x.Id)).ToList();
                if (!ownMedicines.Any()) return;
                List<OwnMedicine> ownMedicineEntities = ObjectMapper.Map<List<OwnMedicineEto>, List<OwnMedicine>>(ownMedicines);


                await _ownMedicineRepository.InsertManyAsync(ownMedicineEntities);
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// 医嘱作废消息接口
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("canceladvice.recipeservice.to.nursingservice")]
        public async Task SubscribeDoctorAdviceCanceledAsync(DoctorsAdviceStatusEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                if (eto == null)
                    throw new ArgumentNullException(typeof(DoctorsAdviceStatusEto).Name);

                await RecipeCancelResultAsync(eto);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// 医生工作站查询到的his医嘱状态同步到护士工作站
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("recipe.to.nursing.syncHisStatus")]
        public async Task SubscribeHisStatusAsync(DoctorsAdviceHisEto doctorsAdviceHisEto)
        {
            var uow = UnitOfWorkManager.Begin();
            try
            {
                if (doctorsAdviceHisEto == null)
                {
                    return;
                }

                IEnumerable<Guid> recipeIds = doctorsAdviceHisEto.DoctorsAdviceHisStatusList.Select(x => x.RecipeId);
                List<Recipe> recipes = await _recipeRepository.GetListAsync(x => x.PIID == doctorsAdviceHisEto.PIID && recipeIds.Contains(x.Id));

                List<RecipeExec> recipeExecs = await _recipeExecRepository.GetListAsync(x => recipeIds.Contains(x.RecipeId));

                List<RecipeExec> deleteRecipeExecs = new List<RecipeExec>();
                foreach (var recipe in recipes)
                {
                    var doctorsAdvice = doctorsAdviceHisEto.DoctorsAdviceHisStatusList.First(x => x.RecipeId == recipe.Id);
                    recipe.Status = doctorsAdvice.Status;
                    recipe.PayStatus = doctorsAdvice.PayStatus;

                    if (recipe.PayStatus == EPayStatus.HavePaid)
                    {
                        recipeExecs.Where(x => x.RecipeId == recipe.Id && x.ExecuteStatus == ExecuteStatusEnum.NoPay).ForEach(x => x.ExecuteStatus = ExecuteStatusEnum.UnCheck);
                    }

                    if (recipe.Status == EDoctorsAdviceStatus.Cancelled)
                    {
                        IEnumerable<RecipeExec> deleteList = recipeExecs.Where(x => x.RecipeId == recipe.Id);
                        recipeExecs.RemoveAll(deleteList);
                        deleteRecipeExecs.AddRange(deleteList);
                    }
                }

                await _recipeRepository.UpdateManyAsync(recipes);
                if (recipeExecs.Any())
                {
                    await _recipeExecRepository.UpdateManyAsync(recipeExecs);
                }
                if (deleteRecipeExecs.Any())
                {
                    await _recipeExecRepository.DeleteManyAsync(deleteRecipeExecs);
                }
                await uow.SaveChangesAsync();

                await _pdaAppService.RecipeExecuteToPdaAsync(doctorsAdviceHisEto.PIID, recipeExecs.Where(x => x.ExecuteStatus == ExecuteStatusEnum.UnCheck), "15");
                if (deleteRecipeExecs.Any())
                {
                    await _pdaAppService.ExecuteRecordToPdaAsync(doctorsAdviceHisEto.PIID, deleteRecipeExecs, "7");
                }

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// 医嘱停嘱消息接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CapSubscribe("stopadvice.recipeservice.to.nursingservice")]
        public async Task SubscribeDoctorAdviceStopedAsync(DoctorsAdviceStopEto input)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                if (input == null)
                    throw new ArgumentNullException(typeof(DoctorsAdviceStopEto).Name);

                await RecipeStopedAsync(input);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// 长嘱拆分任务
        /// </summary>
        [CapSubscribe("job.recipe.split")]
        public async Task PushHisMedicineToxicAsync()
        {
            DateTime currentTime = DateTime.Now;//下面lambda中直接使用DateTime.Now翻译成sql会变成GETDATE()有时区问题
            var uow = UnitOfWorkManager.Begin();
            try
            {
                //开嘱当天在开嘱接口进行拆顿，自动拆顿不在当天进行拆顿。
                var recipes = await (await _recipeRepository.GetQueryableAsync()).AsNoTracking().Where(w => w.PrescribeTypeCode == "PrescribeLong" && w.ApplyTime.Date < currentTime.Date).ToListAsync();
                if (recipes.Any())
                {
                    NursingSettings nursingSettings = await GetNursingSettingsAsync();
                    List<RecipeExec> execs = await _nursingRecipeManager.MedicalAdviceSplitAsync(currentTime, recipes, false, nursingSettings);
                    if (!execs.IsNullOrEmpty())
                    {
                        await _recipeExecRepository.InsertManyAsync(execs);
                    }
                }
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 医嘱开立接口处理
        /// </summary>
        /// <param name="etos"></param>
        /// <param name="isAdd">增量同步，还是全量同步</param>
        /// <returns></returns>
        public async Task<List<Recipe>> RecipeCreatedAsync(List<SubmitDoctorsAdviceEto> etos, bool isAdd)
        {
            List<Recipe> recipes = new();
            List<Prescribe> prescribes = new();
            List<Lis> liss = new();
            List<Pacs> pacss = new();
            List<Treat> treats = new();

            List<Frequency> frequencies = GetAllFrequencies();
            foreach (SubmitDoctorsAdviceEto eto in etos)
            {
                DoctorsAdviceEto advice = eto.DoctorsAdvice;
                if (advice == null) continue;

                Recipe recipe = ObjectMapper.Map<DoctorsAdviceEto, Recipe>(advice);
                recipes.Add(recipe);

                switch (advice.ItemType)
                {
                    case 0:
                        CreatePrescribes(eto, recipe.Id, frequencies, prescribes);
                        break;
                    case 1:
                        CreatePacss(eto, recipe.Id, pacss);
                        break;
                    case 2:
                        CreateLiss(eto, recipe.Id, liss);
                        break;
                    case 3:
                        CreateTreats(eto, recipe.Id, treats);
                        break;
                    default:
                        throw new NotImplementedException($"未实现的医嘱分类{advice.ItemType}！");
                }
            }

            //医嘱入库
            List<Guid> addRecipesIds = new List<Guid>(); //新增的医嘱Id
            if (recipes.Any())
            {
                if (isAdd)
                {
                    addRecipesIds = recipes.Select(p => p.Id).ToList();
                    await _recipeRepository.InsertManyAsync(recipes);
                }
                else
                {
                    Guid piId = recipes.First().PIID;
                    List<Recipe> oldRecipes = await _recipeRepository.GetListAsync(x => x.PIID == piId);

                    IEnumerable<string> recipeNos = recipes.Select(p => p.RecipeNo);
                    IEnumerable<string> existRecipeNos = oldRecipes.Where(x => recipeNos.Contains(x.RecipeNo)).Select(x => x.RecipeNo);
                    IEnumerable<Recipe> addRecipes = recipes.Where(x => !existRecipeNos.Contains(x.RecipeNo));
                    addRecipesIds = addRecipes.Select(p => p.Id).ToList();

                    IEnumerable<Recipe> deleteRecipes = oldRecipes.Where(x => !recipeNos.Contains(x.RecipeNo));

                    if (addRecipes.Any()) await _recipeRepository.InsertManyAsync(addRecipes); //新增
                    if (deleteRecipes.Any()) await _recipeRepository.DeleteManyAsync(deleteRecipes);//删除
                    recipes = addRecipes.ToList();
                }
            }

            if (prescribes.Any())
            {
                IEnumerable<Prescribe> addPrescribes = prescribes.Where(x => addRecipesIds.Contains(x.RecipeId));

                if (addPrescribes.Any()) await _prescribeRepository.InsertManyAsync(addPrescribes); //新增
            }

            if (pacss.Any())
            {
                IEnumerable<Pacs> addPacss = pacss.Where(x => addRecipesIds.Contains(x.RecipeId));

                if (addPacss.Any()) await _pacsRepository.InsertManyAsync(addPacss); //新增
            }

            if (liss.Any())
            {
                IEnumerable<Lis> addLiss = liss.Where(x => addRecipesIds.Contains(x.RecipeId));

                if (addLiss.Any()) await _lisRepository.InsertManyAsync(addLiss);  //新增
            }

            if (treats.Any())
            {
                IEnumerable<Treat> addTreats = treats.Where(x => addRecipesIds.Contains(x.RecipeId));

                if (addTreats.Any()) await _treatRepository.InsertManyAsync(addTreats);   //新增
            }

            return recipes;
        }

        /// <summary>
        /// 拆分医嘱
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="admissionRecordDto"></param>
        /// <returns></returns>
        public async Task SplitAsync(List<Recipe> recipes, AdmissionRecordDto admissionRecordDto = null)
        {
            NursingSettings nursingSettings = await GetNursingSettingsAsync();
            List<RecipeExec> recipeExecs = await _nursingRecipeManager.MedicalAdviceSplitAsync(DateTime.Now, recipes, true, nursingSettings);
            if (!recipeExecs.IsNullOrEmpty())
            {
                await _recipeExecRepository.InsertManyAsync(recipeExecs);
                IEnumerable<RecipeExec> unCheckRecipeExecs = recipeExecs.Where(x => x.ExecuteStatus == ExecuteStatusEnum.UnCheck);
                if (unCheckRecipeExecs.Any())
                {
                    Guid piId = recipeExecs.First().PIID;
                    await _pdaAppService.RecipeExecuteToPdaAsync(piId, unCheckRecipeExecs, "15", admissionRecordDto);
                }
            }
        }

        private async Task<NursingSettings> GetNursingSettingsAsync()
        {
            NursingConfig config = await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == ConfigKeyConsts.NURSINGSETTING);
            if (config == null || string.IsNullOrEmpty(config.Value))
            {
                return new NursingSettings();
            }

            return JsonConvert.DeserializeObject<NursingSettings>(config.Value);
        }

        /// <summary>
        /// 获取全部药品频次信息
        /// </summary>
        /// <returns></returns>
        private List<Frequency> GetAllFrequencies()
        {
            return _grpcClient.GetAllFrequencies();
        }

        /// <summary>
        /// 创建药品对象
        /// </summary>
        /// <param name="eto"></param>
        /// <param name="recipeId"></param>
        /// <param name="frequencies"></param>
        /// <param name="prescribes"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private void CreatePrescribes(SubmitDoctorsAdviceEto eto, Guid recipeId, List<Frequency> frequencies, List<Prescribe> prescribes)
        {
            PrescribeEto prescribeEto = eto.Prescribe;
            if (prescribeEto == null) throw new ArgumentNullException(nameof(eto.Prescribe), $"医嘱开立的时候医嘱类型是药物处方，但是药物处方信息为空！");

            if (prescribeEto.DoctorsAdviceId != recipeId) throw new ArgumentException($"医嘱开立的时候医嘱表Id{recipeId:D}和关联药物处方表中的医嘱Id{prescribeEto.DoctorsAdviceId:D}不一致！");
            Prescribe prescribe = ObjectMapper.Map<PrescribeEto, Prescribe>(prescribeEto);
            if (frequencies.Exists(p => p.FrequencyCode == prescribe.FrequencyCode))
            {
                Frequency freq = frequencies.Find(w => w.FrequencyCode == prescribe.FrequencyCode);
                prescribe.UpdateFrequncy(freq);
            }

            prescribes.Add(prescribe);
        }

        /// <summary>
        /// 创建检查对象
        /// </summary>
        /// <param name="eto"></param>
        /// <param name="recipeId"></param>
        /// <param name="pacss"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private void CreatePacss(SubmitDoctorsAdviceEto eto, Guid recipeId, List<Pacs> pacss)
        {
            PacsEto pacsEto = eto.Pacs;
            if (pacsEto == null) throw new ArgumentNullException(nameof(eto.Pacs), $"医嘱开立的时候医嘱类型是检查，但是检查信息为空！");

            if (pacsEto.DoctorsAdviceId != recipeId) throw new ArgumentException($"医嘱开立的时候医嘱表Id{recipeId:D}和关联检查表中的医嘱Id{pacsEto.DoctorsAdviceId:D}不一致！");

            Pacs pacs = ObjectMapper.Map<PacsEto, Pacs>(pacsEto);
            pacss.Add(pacs);
        }

        /// <summary>
        /// 创建检验对象
        /// </summary>
        /// <param name="eto"></param>
        /// <param name="recipeId"></param>
        /// <param name="liss"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private void CreateLiss(SubmitDoctorsAdviceEto eto, Guid recipeId, List<Lis> liss)
        {
            LisEto lisEto = eto.Lis;
            if (lisEto == null) throw new ArgumentNullException(nameof(eto.Lis), $"医嘱开立的时候医嘱类型是检验，但是检验信息为空！");
            if (lisEto.DoctorsAdviceId != recipeId) throw new ArgumentException($"医嘱开立的时候医嘱表Id{recipeId:D}和关联检验表中的医嘱Id{lisEto.DoctorsAdviceId:D}不一致！");

            Lis lis = ObjectMapper.Map<LisEto, Lis>(lisEto);
            liss.Add(lis);
        }

        /// <summary>
        /// 创建处置对象
        /// </summary>
        /// <param name="eto"></param>
        /// <param name="recipeId"></param>
        /// <param name="treats"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private void CreateTreats(SubmitDoctorsAdviceEto eto, Guid recipeId, List<Treat> treats)
        {
            TreatEto treatEto = eto.Treat;
            if (treatEto == null) throw new ArgumentNullException(nameof(eto.Treat), $"医嘱开立的时候医嘱类型是诊疗，但是诊疗信息为空！");
            if (treatEto.DoctorsAdviceId != recipeId) throw new ArgumentException($"医嘱开立的时候医嘱表Id{recipeId:D}和关联诊疗表中的医嘱Id{treatEto.DoctorsAdviceId:D}不一致！");

            Treat treat = ObjectMapper.Map<TreatEto, Treat>(treatEto);
            treats.Add(treat);
        }

        /// <summary>
        /// 医嘱作废接口处理
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        private async Task RecipeCancelResultAsync(DoctorsAdviceStatusEto eto)
        {
            Guid piid = eto.PIID;
            EDoctorsAdviceStatus status = eto.Status;
            List<Guid> ids = eto.RecipeIds;
            List<Recipe> recipes = await _recipeRepository.GetListAsync(x => x.PIID == piid && ids.Contains(x.Id));
            if (!recipes.Any()) return;

            List<RecipeHistory> histories = new List<RecipeHistory>();
            foreach (Recipe recipe in recipes)
            {
                recipe.Status = EDoctorsAdviceStatus.Cancelled;
                RecipeHistory history = new RecipeHistory(GuidGenerator.Create())
                {
                    RecipeId = recipe.Id,
                    Operation = EDoctorsAdviceOperation.Cancel,
                    OperatorCode = eto.OperatorCode,
                    OperatorName = eto.OperatorName,
                    OperaTime = DateTime.Now
                };
                histories.Add(history);
            }

            //对医嘱执行表中的记录也需要进行作废操作
            List<RecipeExec> recipeExecs = await _recipeExecRepository.GetListAsync(x => x.PIID == piid && ids.Contains(x.RecipeId));
            if (recipeExecs.Any())
            {
                IEnumerable<RecipeExec> delRecipeExecs = recipeExecs.Where(x => x.ExecuteStatus != ExecuteStatusEnum.Exec);

                await _recipeExecRepository.DeleteManyAsync(delRecipeExecs.Select(x => x.Id));
                await _pdaAppService.ExecuteRecordToPdaAsync(piid, delRecipeExecs, "7");
            }

            await _recipeRepository.UpdateManyAsync(recipes);
            await _recipeHistoryRepository.InsertManyAsync(histories);
        }

        /// <summary>
        /// 医嘱停嘱接口处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task RecipeStopedAsync(DoctorsAdviceStopEto input)
        {
            Guid piid = input.PIID;
            List<Guid> recipeIds = input.RecipeIds;
            List<Recipe> recipes = await _recipeRepository.GetListAsync(x => x.PIID == input.PIID && recipeIds.Contains(x.Id));
            if (!recipes.Any()) return;

            List<RecipeHistory> histories = new List<RecipeHistory>();
            foreach (var recipe in recipes)
            {
                recipe.Status = input.Status;
                var history = new RecipeHistory(GuidGenerator.Create())
                {
                    RecipeId = recipe.Id,
                    Operation = EDoctorsAdviceOperation.Stop,
                    OperatorCode = input.OperatorCode,
                    OperatorName = input.OperatorName,
                    OperaTime = DateTime.Now
                };
                histories.Add(history);
            }

            List<RecipeExec> deleteList = new List<RecipeExec>();
            List<RecipeExec> recipeExecs = await _recipeExecRepository.GetListAsync(p => p.PIID == input.PIID && recipeIds.Contains(p.RecipeId));
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                if (recipeExec.ExecuteStatus != ExecuteStatusEnum.Exec && recipeExec.PlanExcuteTime > input.StopTime)
                {
                    deleteList.Add(recipeExec);
                }
            }

            await _recipeRepository.UpdateManyAsync(recipes);
            await _recipeHistoryRepository.InsertManyAsync(histories);
            if (deleteList.Any())
            {
                await _pdaAppService.ExecuteRecordToPdaAsync(piid, deleteList, "7");
                await _recipeExecRepository.DeleteManyAsync(deleteList);
            }
        }

        /// <summary>
        /// 获取药物医嘱
        /// </summary>
        /// <param name="piId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RecipeDto>> GetPrescribeRecipeAsync(Guid piId, string query)
        {
            if (piId == Guid.Empty)
            {
                throw new Exception("请求参数为空");
            }

            List<RecipeDto> recipeInfoList = (from recipe in (await _recipeRepository.GetQueryableAsync()).Where(x => x.PIID == piId && x.CategoryCode == "Medicine")
                                              join prescribeRepository in (await _prescribeRepository.GetQueryableAsync()).AsQueryable()
                                            on recipe.Id equals prescribeRepository.RecipeId into leftjoin2
                                              from prescribe in leftjoin2

                                              select new RecipeDto()
                                              {
                                                  Id = recipe.Id,
                                                  RecipeNo = recipe.RecipeNo,
                                                  Code = recipe.Code,
                                                  Name = recipe.Name,
                                                  Specification = prescribe.Specification,
                                                  DosageQty = prescribe.DosageQty,
                                                  DosageUnit = prescribe.DosageUnit,
                                                  UsageCode = prescribe.UsageCode,
                                                  UsageName = prescribe.UsageName,
                                                  ApplyTime = recipe.ApplyTime
                                              }).ToList();

            foreach (RecipeDto item in recipeInfoList)
            {
                item.AliasPyCode = item.Name.FirstLetterPY();
            }

            if (!string.IsNullOrEmpty(query))
            {
                IEnumerable<string> recipeNos = recipeInfoList.Where(x => x.Name.Contains(query) || x.AliasPyCode.StartsWith(query.ToUpper())).Select(x => x.RecipeNo);

                recipeInfoList = recipeInfoList.Where(x => recipeNos.Contains(x.RecipeNo)).ToList();
            }
            recipeInfoList = recipeInfoList.OrderBy(x => x.RecipeNo).ThenBy(x => x.ApplyTime).ToList();

            return recipeInfoList;
        }
    }
}
