using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.ECIS.Core.Utils;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Etos.NurseExecutes;
using YiJian.ECIS.ShareModel.Etos.Pdas;
using YiJian.Nursing.Config;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.Pda
{
    /// <summary>
    /// 描述：处理pda回传请求
    /// 创建人： yangkai
    /// 创建时间：2022/12/1 15:30:03
    /// </summary>
    [RemoteService(false)]
    public class PdaHandler : NursingAppService, IDistributedEventHandler<ReceivePdaDataEto>, ITransientDependency
    {
        private readonly ILogger<PdaHandler> _logger;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeExecRepository _recipeExecRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly ITreatRepository _treatRepository;
        private readonly IRecipeHistoryRepository _recipeHistoryRepository;
        private readonly IRecipeExecRecordRepository _recipeExecRecordRepository;
        private readonly IRecipeExecHistoryRepository _recipeExecHistoryRepository;
        private readonly INursingConfigRepository _nursingConfigRepository;
        private readonly ICapPublisher _capPublisher;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="recipeRepository"></param>
        /// <param name="recipeHistoryRepository"></param>
        /// <param name="recipeExecHistoryRepository"></param>
        /// <param name="treatRepository"></param>
        /// <param name="recipeExecRecordRepository"></param>
        /// <param name="prescribeRepository"></param>
        /// <param name="capPublisher"></param>
        /// <param name="nursingConfigRepository"></param>
        /// <param name="recipeExecRepository"></param>
        public PdaHandler(ILogger<PdaHandler> logger
            , IRecipeRepository recipeRepository
            , IRecipeHistoryRepository recipeHistoryRepository
            , IRecipeExecHistoryRepository recipeExecHistoryRepository
            , ITreatRepository treatRepository
            , IRecipeExecRecordRepository recipeExecRecordRepository
            , IPrescribeRepository prescribeRepository
            , ICapPublisher capPublisher
            , INursingConfigRepository nursingConfigRepository
            , IRecipeExecRepository recipeExecRepository)
        {
            _logger = logger;
            _recipeRepository = recipeRepository;
            _recipeExecRepository = recipeExecRepository;
            _treatRepository = treatRepository;
            _recipeHistoryRepository = recipeHistoryRepository;
            _recipeExecRecordRepository = recipeExecRecordRepository;
            _recipeExecHistoryRepository = recipeExecHistoryRepository;
            _prescribeRepository = prescribeRepository;
            _capPublisher = capPublisher;
            _nursingConfigRepository = nursingConfigRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(ReceivePdaDataEto eventData)
        {
            if (eventData == null) return;
            _logger.LogInformation($"pda回传的数据{eventData.ToJsonString()}");

            if (eventData.Eventcode.ToLower() == "skintestresult")
            {
                ReceiveSkinResultEto receiveSkinResultEto = JsonConvert.DeserializeObject<ReceiveSkinResultEto>(eventData.Body.ToJsonString());

                await DoSkinResultAsync(receiveSkinResultEto);
            }

            if (eventData.Eventcode.ToLower() == "checkresult")
            {
                ReceiveCheckResultEto receiveExecuteResultEto = JsonConvert.DeserializeObject<ReceiveCheckResultEto>(eventData.Body.ToJsonString());
                if (receiveExecuteResultEto == null) return;

                if (receiveExecuteResultEto.OptType.ToLower() == "check")
                {
                    await DoRecipeCheckAsync(receiveExecuteResultEto);
                }

                if (receiveExecuteResultEto.OptType.ToLower() == "cancel")
                {
                    await DoRecipeCancelCheckAsync(receiveExecuteResultEto);
                }
            }


            if (eventData.Eventcode.ToLower() == "executeresult")
            {
                ReceiveExecuteResultEto receiveExecuteResultEto = JsonConvert.DeserializeObject<ReceiveExecuteResultEto>(eventData.Body.ToJsonString());
                if (receiveExecuteResultEto == null) return;

                if (receiveExecuteResultEto.OptType.ToLower() == "execute")
                {
                    await DoRecipeExecuteAsync(receiveExecuteResultEto);
                }

                if (receiveExecuteResultEto.OptType.ToLower() == "cancel")
                {
                    await DoRecipeCancelAsync(receiveExecuteResultEto);
                }
            }
        }

        /// <summary>
        /// pda回传皮试结果
        /// </summary>
        /// <param name="skinEto"></param>
        /// <returns></returns>
        private async Task DoSkinResultAsync(ReceiveSkinResultEto skinEto)
        {
            IUnitOfWork uow = UnitOfWorkManager.Begin();
            try
            {
                Prescribe prescribe = await (await _prescribeRepository.GetQueryableAsync()).Where(x => x.RecipeId == skinEto.OrderId).FirstOrDefaultAsync();
                if (prescribe == null || !prescribe.NeedSkinTest()) return;

                prescribe.SkinTestResult = skinEto.SkinTestResult;
                RecipeHistory history = new RecipeHistory(GuidGenerator.Create())
                {
                    RecipeId = prescribe.RecipeId,
                    Operation = EDoctorsAdviceOperation.SkinTest,
                    OperatorCode = skinEto.NurseCode,
                    OperatorName = skinEto.NurseName,
                    OperaTime = skinEto.SkinTestTime,
                    Remark = $"皮试结果：{(skinEto.SkinTestResult ? "阳" : "阴")}"
                };

                await _prescribeRepository.UpdateAsync(prescribe);
                await _recipeHistoryRepository.InsertAsync(history);
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// pda回传核对结果
        /// </summary>
        /// <param name="checkEto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task DoRecipeCheckAsync(ReceiveCheckResultEto checkEto)
        {
            IUnitOfWork uow = UnitOfWorkManager.Begin();
            try
            {
                RecipeExec recipeExec = await (await _recipeExecRepository.GetQueryableAsync()).Where(x => x.Id == checkEto.PlacerGroupNumber).FirstOrDefaultAsync();
                if (recipeExec == null || recipeExec.ExecuteStatus != ExecuteStatusEnum.UnCheck) return;

                recipeExec.ExecuteStatus = ExecuteStatusEnum.PreExec;
                recipeExec.CheckNurseCode = checkEto.NurseCode;
                recipeExec.CheckNurseName = checkEto.NurseName;
                recipeExec.CheckTime = checkEto.Optime;
                await _recipeExecRepository.UpdateAsync(recipeExec);

                RecipeExecHistory recipeExecHistory = new RecipeExecHistory
                {
                    RecipeId = Guid.Empty,
                    RecipeExecId = recipeExec.Id,
                    OperationTime = recipeExec.CheckTime,
                    NurseCode = recipeExec.CheckNurseCode,
                    NurseName = recipeExec.CheckNurseName,
                    OperationContent = "核对"
                };
                await _recipeExecHistoryRepository.InsertAsync(recipeExecHistory);
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// pda取消核对
        /// </summary>
        /// <param name="checkEto"></param>
        /// <returns></returns>
        private async Task DoRecipeCancelCheckAsync(ReceiveCheckResultEto checkEto)
        {
            IUnitOfWork uow = UnitOfWorkManager.Begin();
            try
            {
                RecipeExec recipeExec = await (await _recipeExecRepository.GetQueryableAsync()).Where(x => x.Id == checkEto.PlacerGroupNumber).FirstOrDefaultAsync();
                if (recipeExec == null || recipeExec.ExecuteStatus != ExecuteStatusEnum.PreExec) return;

                recipeExec.CheckNurseCode = string.Empty;
                recipeExec.CheckNurseName = string.Empty;
                recipeExec.CheckTime = null;
                recipeExec.TwoCheckNurseCode = string.Empty;
                recipeExec.TwoCheckNurseName = string.Empty;
                recipeExec.TwoCheckTime = null;
                recipeExec.ExecuteStatus = ExecuteStatusEnum.UnCheck;
                await _recipeExecRepository.UpdateAsync(recipeExec);

                RecipeExecHistory recipeExecHistory = new RecipeExecHistory
                {
                    RecipeId = Guid.Empty,
                    RecipeExecId = recipeExec.Id,
                    OperationTime = checkEto.Optime,
                    NurseCode = checkEto.NurseCode,
                    NurseName = checkEto.NurseName,
                    OperationContent = "取消核对"
                };
                await _recipeExecHistoryRepository.InsertAsync(recipeExecHistory);
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// pda执行医嘱
        /// </summary>
        /// <param name="execEto"></param>
        private async Task DoRecipeExecuteAsync(ReceiveExecuteResultEto execEto)
        {
            IUnitOfWork uow = UnitOfWorkManager.Begin();
            try
            {
                RecipeExec recipeExec = await _recipeExecRepository.FirstOrDefaultAsync(x => x.Id == execEto.PlacerGroupNumber);
                if (recipeExec == null) return;

                List<Recipe> recipes = await _recipeRepository.GetListAsync(x => x.RecipeNo == recipeExec.RecipeNo);
                foreach (var recipe in recipes)
                {
                    recipe.Status = EDoctorsAdviceStatus.Executed;
                }

                recipeExec.ExecuteType = "PDA";
                recipeExec.ExecuteNurseCode = execEto.NurseCode;
                recipeExec.ExecuteNurseName = execEto.NurseName;
                recipeExec.ExecuteNurseTime = execEto.Optime;
                recipeExec.ExecuteStatus = ExecuteStatusEnum.Exec;
                recipeExec.TotalExecDosage = recipeExec.ReserveDosage;
                recipeExec.ReserveDosage = 0;
                recipeExec.TotalRemainDosage = 0;

                RecipeExecHistory history = new RecipeExecHistory
                {
                    RecipeId = recipeExec.RecipeId,
                    RecipeExecId = recipeExec.Id,
                    PlanExcuteTime = recipeExec.PlanExcuteTime,
                    NurseCode = execEto.NurseCode,
                    NurseName = execEto.NurseName,
                    OperationType = EDoctorsAdviceOperation.Execute,
                    OperationTime = execEto.Optime,
                    OperationContent = "执行"
                };

                RecipeExecRecord recipeExecRecord = new(Guid.NewGuid())
                {
                    RecipeExecId = recipeExec.Id,
                    DosageQty = recipeExec.TotalExecDosage,
                    RemainDosage = recipeExec.TotalRemainDosage,
                    Unit = recipeExec.Unit,
                    ExcuteNurseCode = execEto.NurseCode,
                    ExcuteNurseName = execEto.NurseName,
                    ExcuteNurseTime = execEto.Optime
                };

                #region 处理附加处置
                List<Guid> recipeIds = recipes.Select(x => x.Id).ToList();
                List<Treat> treats = await _treatRepository.GetListAsync(x => recipeIds.Contains(x.AdditionalItemsId.Value));
                IEnumerable<Guid> treatRecipeIds = treats.Select(x => x.RecipeId);
                List<Recipe> treatRecipes = await _recipeRepository.GetListAsync(x => treatRecipeIds.Contains(x.Id));
                foreach (Recipe treatRecipe in treatRecipes)
                {
                    treatRecipe.Status = EDoctorsAdviceStatus.Executed;
                    recipes.Add(treatRecipe);
                }
                #endregion

                await _recipeRepository.UpdateManyAsync(recipes);
                await _recipeExecRepository.UpdateAsync(recipeExec);
                await _recipeExecRecordRepository.InsertAsync(recipeExecRecord);
                await _recipeExecHistoryRepository.InsertAsync(history);
                await WriteToNursingRecordAsync(new List<RecipeExec>() { recipeExec }, string.Empty);
                await uow.CompleteAsync();

                List<Guid> allRecipeIds = recipes.Select(p => p.Id).ToList();
                var eto = new SyncDoctorsAdviceEto()
                {
                    Ids = allRecipeIds,
                    OperationCode = execEto.NurseCode,
                    OperationName = execEto.NurseName,
                    OperationTime = execEto.Optime,
                    DoctorsAdviceStatus = EDoctorsAdviceStatus.Executed
                };
                await _capPublisher.PublishAsync("syncadvice.nursingservice.to.recipeservice", eto);
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 执行记录写到护理记录单
        /// </summary>
        /// <param name="recipeExecs"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        private async Task WriteToNursingRecordAsync(IEnumerable<RecipeExec> recipeExecs, string signature)
        {
            if (!await IsSyncAsync()) return;
            NursingConfig config = await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == ConfigKeyConsts.CATEGORYCODES);
            if (config == null || string.IsNullOrEmpty(config.Value)) return;

            IEnumerable<string> recipeNos = recipeExecs.Select(x => x.RecipeNo);
            List<Recipe> recipeList = await _recipeRepository.GetListAsync(x => recipeNos.Contains(x.RecipeNo));
            IEnumerable<Guid> recipeIds = recipeList.Select(x => x.Id);

            List<Prescribe> prescribes = await _prescribeRepository.GetListAsync(x => recipeIds.Contains(x.RecipeId));

            List<RecipeExecEto> recipeExecEtos = new List<RecipeExecEto>();
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                RecipeExecEto recipeExecEto = new RecipeExecEto();
                recipeExecEto.PiId = recipeExec.PIID;
                recipeExecEto.RecipeExecId = recipeExec.Id;

                IEnumerable<Recipe> recipes = recipeList.Where(x => x.RecipeNo == recipeExec.RecipeNo);
                IEnumerable<string> recipeNames = new List<string>();
                foreach (Recipe recipe in recipes)
                {
                    Prescribe pre = prescribes.FirstOrDefault(x => x.RecipeId == recipe.Id && x.DosageUnit.Trim().ToLower() != "ml");
                    if (pre == null)
                    {
                        recipeNames = recipeNames.Prepend(recipe.Name);
                    }
                    else
                    {
                        recipeNames = recipeNames.Append(string.Format("{0}{1}{2}", recipe.Name, pre.DosageQty.ToString("#0.##"), pre.DosageUnit));
                    }
                }

                recipeExecEto.RecipeName = string.Join(@"+", recipeNames);
                recipeExecEto.CategoryCode = recipes.First().CategoryCode;

                var prescribe = prescribes.FirstOrDefault(x => x.RecipeId == recipeExec.RecipeId);
                if (prescribe == null) prescribe = new Prescribe();

                recipeExecEto.UsageCode = prescribe.UsageCode;
                recipeExecEto.UsageName = prescribe.UsageName;
                recipeExecEto.ExecDosage = recipeExec.TotalExecDosage.ToString("#0.##");
                recipeExecEto.Unit = recipeExec.Unit;
                recipeExecEto.OperateCode = recipeExec.ExecuteNurseCode;
                recipeExecEto.OperateName = recipeExec.ExecuteNurseName;
                recipeExecEto.OperateTime = recipeExec.ExecuteNurseTime.Value;
                recipeExecEto.Signature = signature;
                recipeExecEtos.Add(recipeExecEto);
            }

            string[] categoryCodes = config.Value.SplitDayTimeArray();
            recipeExecEtos = recipeExecEtos.Where(x => categoryCodes.Contains(x.CategoryCode)).ToList();

            if (recipeExecEtos.Any())
            {
                await _capPublisher.PublishAsync("recipeexec.to.nursingrecord", recipeExecEtos);
            }
        }

        /// <summary>
        /// 是否需要同步到护理记录单配置
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsSyncAsync()
        {
            NursingConfig syncConfig = await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == ConfigKeyConsts.SYNCTONURSINGDOC);
            if (syncConfig == null) return false;
            if (bool.TryParse(syncConfig.Value, out bool result))
            {
                return result;
            }

            return false;
        }

        /// <summary>
        /// pda取消执行
        /// </summary>
        /// <param name="execEto"></param>
        /// <returns></returns>
        private async Task DoRecipeCancelAsync(ReceiveExecuteResultEto execEto)
        {
            IUnitOfWork uow = UnitOfWorkManager.Begin();
            try
            {
                RecipeExec recipeExec = await _recipeExecRepository.FirstOrDefaultAsync(x => x.Id == execEto.PlacerGroupNumber);
                if (recipeExec == null || recipeExec.ExecuteStatus != ExecuteStatusEnum.Exec) return;

                recipeExec.TotalExecDosage = 0;
                recipeExec.TotalRemainDosage = 0;
                recipeExec.TotalDiscardDosage = 0;
                recipeExec.ReserveDosage = recipeExec.TotalDosage;
                recipeExec.IsDiscard = false;
                recipeExec.ExecuteNurseCode = string.Empty;
                recipeExec.ExecuteNurseName = string.Empty;
                recipeExec.ExecuteNurseTime = null;
                recipeExec.ExecuteStatus = ExecuteStatusEnum.PreExec;
                await _recipeExecRepository.UpdateAsync(recipeExec);

                RecipeExecHistory recipeExecHistory = new RecipeExecHistory
                {
                    RecipeId = Guid.Empty,
                    RecipeExecId = recipeExec.Id,
                    OperationTime = execEto.Optime,
                    NurseCode = execEto.NurseCode,
                    NurseName = execEto.NurseName,
                    OperationContent = "取消执行"
                };
                await _recipeExecHistoryRepository.InsertAsync(recipeExecHistory);
                await CancelRecipeExecRecordAsync(recipeExec);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 删除护理记录单记录
        /// </summary>
        /// <param name="recipeExec"></param>
        /// <returns></returns>
        private async Task CancelRecipeExecRecordAsync(RecipeExec recipeExec)
        {
            RecipeExecEto recipeExecEto = new RecipeExecEto();
            recipeExecEto.RecipeExecId = recipeExec.Id;
            await _capPublisher.PublishAsync("cancel.recipeexec.to.nursingrecord", recipeExecEto);
        }
    }
}
