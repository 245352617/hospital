using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using YiJian.Apis;
using YiJian.Common;
using YiJian.ECIS.DDP;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Etos.NurseExecutes;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Nursing.Config;
using YiJian.Nursing.Pda;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes.Dtos;
using YiJian.Nursing.Recipes.Entities;
using YiJian.Patient;
using YiJian.Rpc;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：医嘱执行操作
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 10:48:45
    /// </summary>
    [Authorize]
    public class RecipeExecAppService : NursingAppService, IRecipeExecAppService
    {
        private readonly ILogger<RecipeExecAppService> _logger;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly ITreatRepository _treatRepository;
        private readonly IRecipeExecRepository _recipeExecRepository;
        private readonly IRecipeExecRecordRepository _recipeExecRecordRepository;
        private readonly IRecipeExecHistoryRepository _recipeExecHistoryRepository;
        private readonly PdaAppService _pdaAppService;
        private readonly ICapPublisher _capPublisher;
        private readonly RecipeAppService _recipeAppService;
        private readonly PatientAppService _patientAppService;
        private readonly IConfiguration _configuration;
        private readonly INursingConfigRepository _nursingConfigRepository;
        private readonly DdpHospital _ddpHospital;
        private readonly DdpSwitch _ddpSwitch;
        private readonly IDdpApiService _ddpApiService;
        private readonly GrpcClient _grpcClient;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="recipeRepository"></param>
        /// <param name="prescribeRepository"></param>
        /// <param name="treatRepository"></param>
        /// <param name="recipeExecRepository"></param>
        /// <param name="recipeExecHistoryRepository"></param>
        /// <param name="pdaAppService"></param>
        /// <param name="capPublisher"></param>
        /// <param name="recipeExecRecordRepository"></param>
        /// <param name="configuration"></param>
        /// <param name="nursingConfigRepository"></param>
        /// <param name="ddpHospital"></param>
        /// <param name="ddpSwitch"></param>
        /// <param name="grpcClient"></param>
        /// <param name="patientAppService"></param>
        /// <param name="recipeAppService"></param>
        public RecipeExecAppService(ILogger<RecipeExecAppService> logger
            , IRecipeRepository recipeRepository
            , IPrescribeRepository prescribeRepository
            , ITreatRepository treatRepository
            , IRecipeExecRepository recipeExecRepository
            , IRecipeExecHistoryRepository recipeExecHistoryRepository
            , PdaAppService pdaAppService
            , ICapPublisher capPublisher
            , IRecipeExecRecordRepository recipeExecRecordRepository
            , IConfiguration configuration
            , INursingConfigRepository nursingConfigRepository
            , IOptions<DdpHospital> ddpHospital
            , DdpSwitch ddpSwitch
            , GrpcClient grpcClient
            , PatientAppService patientAppService
            , RecipeAppService recipeAppService)
        {
            _logger = logger;
            _recipeRepository = recipeRepository;
            _prescribeRepository = prescribeRepository;
            _treatRepository = treatRepository;
            _recipeExecRepository = recipeExecRepository;
            _recipeExecHistoryRepository = recipeExecHistoryRepository;
            _pdaAppService = pdaAppService;
            _capPublisher = capPublisher;
            _recipeExecRecordRepository = recipeExecRecordRepository;
            _recipeAppService = recipeAppService;
            _patientAppService = patientAppService;
            _configuration = configuration;
            _nursingConfigRepository = nursingConfigRepository;
            _ddpHospital = ddpHospital.Value;
            _ddpSwitch = ddpSwitch;
            _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
            _grpcClient = grpcClient;
        }

        /// <summary>
        /// 一键执行
        /// </summary>
        /// <param name="batchExecDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [UnitOfWork]
        public async Task<bool> BatchExecAsync(BatchExecDto batchExecDto)
        {
            List<CheckExecDto> checkExecDtos = batchExecDto.ExecArr;
            if (checkExecDtos == null || !checkExecDtos.Any())
            {
                throw new Exception("请求参数为空");
            }

            IEnumerable<Guid> recipeExecIds = checkExecDtos.Select(x => x.RecipeExecId);
            List<RecipeExec> recipeExecList = await _recipeExecRepository.GetListAsync(x => recipeExecIds.Contains(x.Id));
            if (!recipeExecList.Any())
            {
                throw new Exception("没有找到需要执行的医嘱");
            }

            List<RecipeExecRecord> recipeExecRecordList = new List<RecipeExecRecord>();
            foreach (RecipeExec recipeExec in recipeExecList)
            {
                if (recipeExec.ExecuteStatus != ExecuteStatusEnum.PreExec) continue;

                var checkExecDto = checkExecDtos.FirstOrDefault(x => x.RecipeExecId == recipeExec.Id);
                if (checkExecDto == null) continue;

                recipeExec.ExecuteStatus = ExecuteStatusEnum.Exec;
                recipeExec.ExecuteNurseCode = checkExecDto.OperateCode;
                recipeExec.ExecuteNurseName = checkExecDto.OperateName;
                recipeExec.ExecuteNurseTime = checkExecDto.OperateTime;
                recipeExec.ReserveDosage = checkExecDto.ReserveDosage - checkExecDto.TotalExecDosage;
                recipeExec.TotalExecDosage = checkExecDto.TotalExecDosage;
                recipeExec.TotalRemainDosage = checkExecDto.TotalRemainDosage;
                recipeExec.Unit = checkExecDto.Unit;

                //记录执行记录
                RecipeExecRecord recipeExecRecord = new(Guid.NewGuid())
                {
                    RecipeExecId = recipeExec.Id,
                    DosageQty = checkExecDto.TotalExecDosage,
                    RemainDosage = checkExecDto.TotalRemainDosage,
                    Unit = recipeExec.Unit,
                    ExcuteNurseCode = checkExecDto.OperateCode,
                    ExcuteNurseName = checkExecDto.OperateName,
                    ExcuteNurseTime = checkExecDto.OperateTime
                };
                recipeExecRecordList.Add(recipeExecRecord);
            }
            await _recipeExecRepository.UpdateManyAsync(recipeExecList);
            await _recipeExecRecordRepository.InsertManyAsync(recipeExecRecordList);

            await WriteToNursingRecordAsync(recipeExecList, batchExecDto.Signature);
            await WriteOperationRecordAsync(checkExecDtos, "一键执行");
            await ExecStatusToPdaAsync(recipeExecList, "4");

            await SendToDoctorAsync(recipeExecList, EDoctorsAdviceStatus.Executed);
            return true;
        }

        /// <summary>
        /// 取消核对
        /// </summary>
        /// <param name="cancelDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<bool> CancelCheckAsync(BaseRequestDto cancelDto)
        {
            if (cancelDto == null)
            {
                throw new Exception("请求参数为空");
            }

            RecipeExec recipeExec = await _recipeExecRepository.FirstOrDefaultAsync(x => x.Id == cancelDto.RecipeExecId);
            if (recipeExec == null)
            {
                throw new Exception("没有找到要取消的医嘱");
            }

            if (recipeExec.ExecuteStatus != ExecuteStatusEnum.PreExec)
            {
                throw new Exception("医嘱不是待执行状态不能取消核对");
            }

            if (!string.IsNullOrEmpty(recipeExec.TwoCheckNurseCode))
            {
                throw new Exception("请先取消二次核对");
            }

            if (recipeExec.CheckNurseCode != cancelDto.OperateCode)
            {
                throw new Exception("核对人不是当前用户不能取消核对");
            }

            recipeExec.CheckNurseCode = string.Empty;
            recipeExec.CheckNurseName = string.Empty;
            recipeExec.CheckTime = null;
            recipeExec.ExecuteStatus = ExecuteStatusEnum.UnCheck;
            await _recipeExecRepository.UpdateAsync(recipeExec);
            await WriteOperationRecordAsync(cancelDto, "取消核对");
            await CheckStatusToPdaAsync(new List<RecipeExec>() { recipeExec }, "15");

            return true;
        }

        /// <summary>
        /// 一键取消核对
        /// </summary>
        /// <param name="cancelDtos"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> BatchCancelCheckAsync(List<BaseRequestDto> cancelDtos)
        {
            if (cancelDtos == null || !cancelDtos.Any())
            {
                throw new Exception("请求参数为空");
            }

            IEnumerable<Guid> recipeExecIds = cancelDtos.Select(x => x.RecipeExecId);
            string operateCode = cancelDtos.First().OperateCode;
            List<RecipeExec> recipeExecs = await _recipeExecRepository.GetListAsync(x => recipeExecIds.Contains(x.Id) && x.ExecuteStatus == ExecuteStatusEnum.PreExec);

            if (recipeExecs == null || !recipeExecs.Any())
            {
                throw new Exception("没有找到要取消执行的医嘱");
            }

            List<BaseRequestDto> twoCheckcancelDtos = new List<BaseRequestDto>();
            List<BaseRequestDto> checkcancelDtos = new List<BaseRequestDto>();
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                if (recipeExec.TwoCheckNurseCode == operateCode)
                {
                    recipeExec.TwoCheckNurseCode = string.Empty;
                    recipeExec.TwoCheckNurseName = string.Empty;
                    recipeExec.TwoCheckTime = null;
                    BaseRequestDto cancelDto = cancelDtos.Find(x => x.RecipeExecId == recipeExec.Id);
                    twoCheckcancelDtos.Add(cancelDto);
                }

                if (string.IsNullOrEmpty(recipeExec.TwoCheckNurseCode) && recipeExec.CheckNurseCode == operateCode)
                {
                    recipeExec.CheckNurseCode = string.Empty;
                    recipeExec.CheckNurseName = string.Empty;
                    recipeExec.CheckTime = null;
                    recipeExec.ExecuteStatus = ExecuteStatusEnum.UnCheck;
                    BaseRequestDto cancelDto = cancelDtos.Find(x => x.RecipeExecId == recipeExec.Id);
                    checkcancelDtos.Add(cancelDto);
                }
            }

            await _recipeExecRepository.UpdateManyAsync(recipeExecs);
            if (twoCheckcancelDtos.Any())
            {
                await WriteOperationRecordAsync(twoCheckcancelDtos, "一键取消二次核对");
            }
            if (checkcancelDtos.Any())
            {
                await WriteOperationRecordAsync(checkcancelDtos, "一键取消核对");
            }

            IEnumerable<RecipeExec> unCheckRecipeExecs = recipeExecs.Where(x => x.ExecuteStatus == ExecuteStatusEnum.UnCheck);
            if (unCheckRecipeExecs.Any())
            {
                await CheckStatusToPdaAsync(unCheckRecipeExecs, "15");
            }

            return true;
        }

        /// <summary>
        /// 取消执行
        /// </summary>
        /// <param name="cancelDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [UnitOfWork]
        public async Task<bool> CancelExecAsync(BaseRequestDto cancelDto)
        {
            if (cancelDto == null)
            {
                throw new Exception("请求参数为空");
            }

            RecipeExec recipeExec = await _recipeExecRepository.FirstOrDefaultAsync(x => x.Id == cancelDto.RecipeExecId);
            if (recipeExec == null)
            {
                throw new Exception("没有找到要取消执行的医嘱");
            }

            if (recipeExec.ExecuteStatus != ExecuteStatusEnum.Exec)
            {
                throw new Exception("医嘱不是已执行状态不能取消执行");
            }

            if (recipeExec.ExecuteNurseCode != cancelDto.OperateCode)
            {
                throw new Exception("不是执行人不能取消执行");
            }

            List<RecipeExecRecord> execRecordList = await _recipeExecRecordRepository.GetListAsync(x => x.RecipeExecId == recipeExec.Id && x.ExecRecordStatus == ExecRecordStatusEnum.Exec);
            if (!execRecordList.Any())
            {
                throw new Exception("没有执行记录不能取消执行");
            }

            recipeExec.TotalExecDosage = 0;
            recipeExec.TotalRemainDosage = 0;
            recipeExec.TotalDiscardDosage = 0;
            recipeExec.ReserveDosage = recipeExec.TotalDosage;
            recipeExec.IsDiscard = false;
            recipeExec.ExecuteNurseCode = string.Empty;
            recipeExec.ExecuteNurseName = string.Empty;
            recipeExec.ExecuteNurseTime = null;
            recipeExec.ExecuteStatus = ExecuteStatusEnum.PreExec;
            execRecordList.ForEach(x => x.ExecRecordStatus = ExecRecordStatusEnum.Cancel);

            await _recipeExecRepository.UpdateAsync(recipeExec);
            await _recipeExecRecordRepository.UpdateManyAsync(execRecordList);

            await SendToDoctorAsync(new List<RecipeExec>() { recipeExec }, EDoctorsAdviceStatus.Submitted);
            await CancelRecipeExecRecordAsync(recipeExec);
            await WriteOperationRecordAsync(cancelDto, "取消执行");
            await ExecStatusToPdaAsync(new List<RecipeExec>() { recipeExec }, "6");

            return true;
        }

        /// <summary>
        /// 一键取消执行
        /// </summary>
        /// <param name="cancelDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork]
        public async Task<bool> BatchCancelExecAsync(List<BaseRequestDto> cancelDtos)
        {
            if (cancelDtos == null || !cancelDtos.Any())
            {
                throw new Exception("请求参数为空");
            }

            IEnumerable<Guid> recipeExecIds = cancelDtos.Select(x => x.RecipeExecId);
            string operateCode = cancelDtos.First().OperateCode;
            List<RecipeExec> recipeExecs = await _recipeExecRepository.GetListAsync(x => recipeExecIds.Contains(x.Id) && x.ExecuteNurseCode == operateCode && x.ExecuteStatus == ExecuteStatusEnum.Exec);

            if (recipeExecs == null || !recipeExecs.Any())
            {
                throw new Exception("没有找到要取消执行的医嘱");
            }

            List<RecipeExecRecord> execRecordList = await _recipeExecRecordRepository.GetListAsync(x => recipeExecIds.Contains(x.RecipeExecId) && x.ExecRecordStatus == ExecRecordStatusEnum.Exec);
            if (!execRecordList.Any())
            {
                throw new Exception("没有执行记录不能取消执行");
            }

            foreach (RecipeExec recipeExec in recipeExecs)
            {
                recipeExec.TotalExecDosage = 0;
                recipeExec.TotalRemainDosage = 0;
                recipeExec.TotalDiscardDosage = 0;
                recipeExec.ReserveDosage = recipeExec.TotalDosage;
                recipeExec.IsDiscard = false;
                recipeExec.ExecuteNurseCode = string.Empty;
                recipeExec.ExecuteNurseName = string.Empty;
                recipeExec.ExecuteNurseTime = null;
                recipeExec.ExecuteStatus = ExecuteStatusEnum.PreExec;
            }
            execRecordList.ForEach(x => x.ExecRecordStatus = ExecRecordStatusEnum.Cancel);

            await _recipeExecRepository.UpdateManyAsync(recipeExecs);
            await _recipeExecRecordRepository.UpdateManyAsync(execRecordList);

            await SendToDoctorAsync(recipeExecs, EDoctorsAdviceStatus.Submitted);
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                await CancelRecipeExecRecordAsync(recipeExec);
            }
            await WriteOperationRecordAsync(cancelDtos, "一键取消执行");
            await ExecStatusToPdaAsync(recipeExecs, "6");

            return true;
        }

        /// <summary>
        /// 取消二次核对
        /// </summary>
        /// <param name="cancelDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<bool> CancelTwoCheckAsync(BaseRequestDto cancelDto)
        {
            if (cancelDto == null)
            {
                throw new Exception("请求参数为空");
            }

            var recipeExec = await _recipeExecRepository.FirstOrDefaultAsync(x => x.Id == cancelDto.RecipeExecId);
            if (recipeExec == null)
            {
                throw new Exception("没有找到要取消核对的医嘱");
            }

            if (recipeExec.ExecuteStatus != ExecuteStatusEnum.PreExec)
            {
                throw new Exception("医嘱不是待执行状态不能取消核对");
            }

            if (recipeExec.TwoCheckNurseCode != cancelDto.OperateCode)
            {
                throw new Exception("二次核对人不是当前用户不能取消二次核对");
            }

            recipeExec.TwoCheckNurseCode = string.Empty;
            recipeExec.TwoCheckNurseName = string.Empty;
            recipeExec.TwoCheckTime = null;
            await _recipeExecRepository.UpdateAsync(recipeExec);
            await WriteOperationRecordAsync(cancelDto, "取消二次核对");

            return true;
        }

        /// <summary>
        /// 核对及批量核对
        /// </summary>
        /// <param name="checkExecDtos"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<bool> CkeckAsync(List<CheckExecDto> checkExecDtos)
        {
            if (checkExecDtos == null || !checkExecDtos.Any())
            {
                throw new Exception("请求参数为空");
            }

            var recipeExecIds = checkExecDtos.Select(x => x.RecipeExecId);
            List<RecipeExec> recipeExecList = await _recipeExecRepository.GetListAsync(x => recipeExecIds.Contains(x.Id));
            if (!recipeExecList.Any())
            {
                throw new Exception("没有找到需要核对的医嘱");
            }

            foreach (var recipeExec in recipeExecList)
            {
                if (recipeExec.ExecuteStatus != ExecuteStatusEnum.UnCheck) continue;
                var checkExecDto = checkExecDtos.FirstOrDefault(x => x.RecipeExecId == recipeExec.Id);
                if (checkExecDto == null) continue;

                recipeExec.ExecuteStatus = ExecuteStatusEnum.PreExec;
                recipeExec.CheckNurseCode = checkExecDto.OperateCode;
                recipeExec.CheckNurseName = checkExecDto.OperateName;
                recipeExec.CheckTime = checkExecDto.OperateTime;
            }

            await _recipeExecRepository.UpdateManyAsync(recipeExecList);
            await WriteOperationRecordAsync(checkExecDtos, "核对");
            await CheckStatusToPdaAsync(recipeExecList, "1");

            return true;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="execDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [UnitOfWork]
        public async Task<bool> ExecAsync(ExecDto execDto)
        {
            if (execDto == null)
            {
                throw new Exception("请求参数为空");
            }

            RecipeExec recipeExec = await _recipeExecRepository.FirstOrDefaultAsync(x => x.Id == execDto.RecipeExecId);
            if (recipeExec == null)
            {
                throw new Exception("没有找到要执行的医嘱");
            }

            if (recipeExec.ExecuteStatus != ExecuteStatusEnum.PreExec && recipeExec.ExecuteStatus != ExecuteStatusEnum.Exec)
            {
                throw new Exception("医嘱不是可执行状态");
            }

            if (recipeExec.ReserveDosage < execDto.Dosage)
            {
                throw new Exception("备用量不足");
            }

            recipeExec.ExecuteStatus = ExecuteStatusEnum.Exec;
            recipeExec.TotalExecDosage += execDto.Dosage;
            if (execDto.IsDiscard.HasValue && execDto.IsDiscard.Value)
            {
                recipeExec.TotalDiscardDosage = recipeExec.ReserveDosage - execDto.Dosage;
                recipeExec.TotalRemainDosage = 0m;
                recipeExec.ReserveDosage = 0m;
            }
            else
            {
                recipeExec.TotalRemainDosage = recipeExec.ReserveDosage - execDto.Dosage;
                recipeExec.ReserveDosage -= execDto.Dosage;
            }
            recipeExec.ExecuteNurseCode = execDto.OperateCode;
            recipeExec.ExecuteNurseName = execDto.OperateName;
            recipeExec.ExecuteNurseTime = execDto.OperateTime;
            recipeExec.Unit = execDto.Unit;

            //记录执行记录
            RecipeExecRecord recipeExecRecord = new(Guid.NewGuid())
            {
                RecipeExecId = recipeExec.Id,
                DosageQty = execDto.Dosage,
                Unit = execDto.Unit,
                ExcuteNurseCode = execDto.OperateCode,
                ExcuteNurseName = execDto.OperateName,
                ExcuteNurseTime = execDto.OperateTime
            };
            if (execDto.IsDiscard.HasValue && execDto.IsDiscard.Value)
            {
                recipeExecRecord.DiscardDosage = recipeExec.TotalDiscardDosage;
            }
            else
            {
                recipeExecRecord.RemainDosage = recipeExec.TotalRemainDosage;
            }

            await _recipeExecRepository.UpdateAsync(recipeExec);
            await _recipeExecRecordRepository.InsertAsync(recipeExecRecord);

            await WriteToNursingRecordAsync(new List<RecipeExec>() { recipeExec }, execDto.Signature);
            await WriteOperationRecordAsync(execDto, "执行");
            await ExecStatusToPdaAsync(new List<RecipeExec>() { recipeExec }, "4");

            await SendToDoctorAsync(new List<RecipeExec>() { recipeExec }, EDoctorsAdviceStatus.Executed);
            return true;
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

        /// <summary>
        /// 获取核对和执行列表
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task<List<CheckExecDto>> GetCheckExecListAsync(QueryRecipeDto queryRecipeDto)
        {
            if (queryRecipeDto == null || queryRecipeDto.PiId == Guid.Empty || !queryRecipeDto.ExecuteStatus.HasValue)
            {
                throw new Exception("请求参数为空");
            }

            HandlerParam(queryRecipeDto);

            NursingConfig config = await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == ConfigKeyConsts.EXECUTIONMEDICALORDERS);
            if (config != null && config.Value == "true")
            {
                queryRecipeDto.AreaCodes = null;
            }

            var elist = await (from recipeExec in (await _recipeExecRepository.GetQueryableAsync())
                                .Where(x => x.PIID == queryRecipeDto.PiId && x.ExecuteStatus == (ExecuteStatusEnum)queryRecipeDto.ExecuteStatus.Value)

                               join recipe in (await _recipeRepository.GetQueryableAsync())
                               .WhereIf(!string.IsNullOrEmpty(queryRecipeDto.CategoryCode), x => x.CategoryCode == queryRecipeDto.CategoryCode)
                               .WhereIf((queryRecipeDto.AreaCodes != null && queryRecipeDto.AreaCodes.Any()), x => queryRecipeDto.AreaCodes.Contains(x.AreaCode))
                               .WhereIf(queryRecipeDto.StartTime.HasValue, x => x.ApplyTime >= queryRecipeDto.StartTime)
                               .WhereIf(queryRecipeDto.EndTime.HasValue, x => x.ApplyTime < queryRecipeDto.EndTime)
                               on recipeExec.RecipeNo equals recipe.RecipeNo

                               join prescribeRepository in (await _prescribeRepository.GetQueryableAsync())
                               on recipe.Id equals prescribeRepository.RecipeId into leftjoin1
                               from prescribe in leftjoin1.DefaultIfEmpty()
                               select new { Recipe = recipe, Prescribe = prescribe, RecipeExec = recipeExec }
                  ).ToListAsync();

            List<CheckExecDto> checkExecDtoList = (from item in elist
                                                   select new CheckExecDto
                                                   {
                                                       RecipeExecId = item.RecipeExec.Id,
                                                       RecipeNo = item.Recipe.RecipeNo,
                                                       RecipeGroupNo = item.Recipe.RecipeGroupNo,
                                                       ApplyTime = item.Recipe.ApplyTime,
                                                       PlanExcuteTime = item.RecipeExec.PlanExcuteTime,
                                                       ExecuteStatus = (int)item.RecipeExec.ExecuteStatus,
                                                       CategoryCode = item.Recipe.CategoryCode,
                                                       CategoryName = item.Recipe.CategoryName,
                                                       Name = item.Recipe.Name,
                                                       DosageQty = item.Prescribe?.DosageQty,
                                                       DosageUnit = item.Prescribe?.DosageUnit,
                                                       UsageCode = item.Prescribe?.UsageCode,
                                                       UsageName = item.Prescribe?.UsageName,
                                                       FrequencyCode = item.Prescribe?.FrequencyCode,
                                                       FrequencyName = item.Prescribe?.FrequencyName,
                                                       ReserveDosage = item.RecipeExec.ReserveDosage,
                                                       TotalExecDosage = item.RecipeExec.TotalExecDosage,
                                                       TotalRemainDosage = item.RecipeExec.TotalRemainDosage,
                                                       Unit = "ml",
                                                   }).ToList();
            if (queryRecipeDto.CategoryCode == ParamConsts.MEDICINE_CODE)
            {
                checkExecDtoList = checkExecDtoList.Where(x => queryRecipeDto.UsageCodes.Contains(x.UsageCode)).ToList();
            }

            foreach (CheckExecDto checkExecDto in checkExecDtoList)
            {
                checkExecDto.ExecuteStatusText = ((ExecuteStatusEnum)checkExecDto.ExecuteStatus).GetDescription();
                checkExecDto.OperateCode = CurrentUser.UserName;
                checkExecDto.OperateName = CurrentUser.FindClaimValue("fullName");
                checkExecDto.OperateTime = DateTime.Now;
            }

            checkExecDtoList = checkExecDtoList.OrderBy(x => x.PlanExcuteTime)
                       .ThenBy(x => x.RecipeNo)
                       .ThenBy(x => x.ApplyTime)
                       .ToList();

            return checkExecDtoList;
        }

        /// <summary>
        /// 获取取消核对和取消执行列表
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RecipeExecDto>> GetCancelCheckExecListAsync(QueryRecipeDto queryRecipeDto)
        {
            if (queryRecipeDto == null || queryRecipeDto.PiId == Guid.Empty)
            {
                throw new Exception("请求参数为空");
            }

            List<RecipeExecDto> recipeDtoList = new List<RecipeExecDto>();
            HandlerParam(queryRecipeDto);
            recipeDtoList = await QueryRecipeExecDataAsync(queryRecipeDto);
            if (queryRecipeDto.CategoryCode == ParamConsts.MEDICINE_CODE)
            {
                recipeDtoList = recipeDtoList.Where(x => queryRecipeDto.UsageCodes.Contains(x.UsageCode)).ToList();
            }

            foreach (RecipeExecDto recipeDto in recipeDtoList)
            {
                recipeDto.ExecuteStatusText = recipeDto.ExecuteStatus.GetDescription();
            }

            recipeDtoList = recipeDtoList
                            .OrderBy(o => o.PlanExcuteTime)
                            .ThenBy(o => o.RecipeNo)
                            .ThenBy(o => o.ApplyTime)
                            .ToList();

            return recipeDtoList;
        }

        /// <summary>
        /// 获取医嘱类别
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<RecipeTypeDto>> GetRecipeTypeListAsync(QueryRecipeDto queryRecipeDto)
        {
            if (queryRecipeDto == null || queryRecipeDto.PiId == Guid.Empty)
            {
                throw new Exception("请求参数为空");
            }

            List<RecipeTypeDto> recipeTypeDtoList = new List<RecipeTypeDto>
            {
                new RecipeTypeDto() { TypeName = ParamConsts.ALL_NAME }
            };
            List<RecipeExecDto> recipeDtoList = new List<RecipeExecDto>();
            recipeDtoList = await QueryRecipeExecDataAsync(queryRecipeDto);
            List<NursingRecipeTypeDto> nursingRecipeTypes = _grpcClient.GetAllNursingRecipeTypes();
            IEnumerable<string> typeNames = nursingRecipeTypes.Select(x => x.TypeName).Distinct();
            foreach (string item in typeNames)
            {
                IEnumerable<string> usageCodes = nursingRecipeTypes.Where(x => x.TypeName == item).Select(x => x.UsageCode);
                int count = recipeDtoList.DistinctBy(x => x.RecipeExecId).Count(x => usageCodes.Contains(x.UsageCode));

                recipeTypeDtoList.Add(new RecipeTypeDto()
                {
                    TypeName = item,
                    Count = count
                });
            }

            int treatCount = recipeDtoList.Count(x => x.CategoryCode == ParamConsts.TREAT_CODE);
            recipeTypeDtoList.Add(new RecipeTypeDto()
            {
                TypeName = ParamConsts.TREAT_NAME,
                Count = treatCount,
            });

            int labCount = recipeDtoList.Count(x => x.CategoryCode == ParamConsts.LAB_CODE);
            recipeTypeDtoList.Add(new RecipeTypeDto()
            {
                TypeName = ParamConsts.LAB_NAME,
                Count = labCount,
            });

            int examCount = recipeDtoList.Count(x => x.CategoryCode == ParamConsts.EXAM_CODE);
            recipeTypeDtoList.Add(new RecipeTypeDto()
            {
                TypeName = ParamConsts.EXAM_NAME,
                Count = examCount,
            });
            return recipeTypeDtoList;
        }

        /// <summary>
        /// 查询医嘱分页列表
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<ResultDataDto> QueryRecipePageListAsync(QueryRecipeDto queryRecipeDto)
        {
            if (queryRecipeDto == null || queryRecipeDto.PiId == Guid.Empty)
            {
                throw new Exception("请求参数为空");
            }

            List<RecipeExecDto> recipeDtoList = new List<RecipeExecDto>();
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                if (_configuration["HospitalCode"] == "PKU")
                {
                    //若存在不在同步
                    bool exist = await _recipeRepository.AnyAsync(x => x.PIID == queryRecipeDto.PiId);
                    if (!exist)
                    {
                        await QueryHisDataAsync(queryRecipeDto, uow);
                        await uow.SaveChangesAsync();
                    }
                }

                //await CheckPatientAsync(queryRecipeDto.PiId);
                //await uow.SaveChangesAsync();

                HandlerParam(queryRecipeDto);
                recipeDtoList = await QueryRecipeExecDataAsync(queryRecipeDto);
                if (queryRecipeDto.CategoryCode == ParamConsts.MEDICINE_CODE)
                {
                    recipeDtoList = recipeDtoList.Where(x => queryRecipeDto.UsageCodes.Contains(x.UsageCode)).ToList();
                }

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
                throw;
            }

            foreach (RecipeExecDto recipeDto in recipeDtoList)
            {
                recipeDto.ExecuteStatusText = recipeDto.ExecuteStatus.GetDescription();
            }

            IEnumerable<RecipeExecOrderDto> recipeExecOrderDtos = recipeDtoList.Select(x => new RecipeExecOrderDto()
            {
                RecipeNo = x.RecipeNo,
                PlanExcuteTime = x.PlanExcuteTime.Date
            }).Distinct(new RecipeExecOrderDto());
            foreach (RecipeExecOrderDto recipeExecOrderDto in recipeExecOrderDtos)
            {
                List<RecipeExecDto> items = recipeDtoList.Where(x => x.RecipeNo == recipeExecOrderDto.RecipeNo && x.PlanExcuteTime.Date == recipeExecOrderDto.PlanExcuteTime.Date).OrderBy(x => x.PlanExcuteTime).ToList();

                List<Guid> recipeExecIds = items.Select(x => x.RecipeExecId).Distinct().ToList();
                for (int i = 0; i < recipeExecIds.Count; i++)
                {
                    IEnumerable<RecipeExecDto> recipes = items.Where(x => x.RecipeExecId == recipeExecIds[i]);
                    foreach (RecipeExecDto recipe in recipes)
                    {
                        recipe.Order = $"{i + 1}/{recipeExecIds.Count}";
                    }
                }
            }

            Dictionary<string, int> statisticsData = new Dictionary<string, int>();
            int totalCount = recipeDtoList.Select(x => x.RecipeExecId).Distinct().Count();
            statisticsData.Add("total", totalCount);

            var properties = Enum.GetValues(typeof(ExecuteStatusEnum));
            foreach (var property in properties)
            {
                string key = property.ToString();
                key = key[0].ToString().ToLower() + key[1..];
                statisticsData.Add(key, 0);
            }

            var groups = recipeDtoList.Select(x => new { x.ExecuteStatus, x.RecipeExecId }).CustomDistinctBy(x => x.RecipeExecId).GroupBy(x => x.ExecuteStatus);

            foreach (var group in groups)
            {
                int count = group.Count();
                string key = group.Key.ToString();
                key = key[0].ToString().ToLower() + key[1..];
                if (statisticsData.ContainsKey(key))
                {
                    statisticsData[key] = count;
                }
            }

            List<RecipeExecDto> data = recipeDtoList
                .OrderBy(o => o.PlanExcuteTime)
                .ThenBy(o => o.RecipeNo)
                .ThenBy(o => o.ApplyTime)
                .Skip(queryRecipeDto.SkipCount)
                .Take(queryRecipeDto.Size)
                .ToList();

            ResultDataDto resultData = new ResultDataDto()
            {
                RecipeDtos = data,
                StatisticsData = statisticsData,
                Count = totalCount
            };
            return resultData;
        }

        /// <summary>
        /// 处理查询请求参数
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        private void HandlerParam(QueryRecipeDto queryRecipeDto)
        {
            switch (queryRecipeDto.CategoryCode)
            {
                case ParamConsts.LAB_NAME: queryRecipeDto.CategoryCode = ParamConsts.LAB_CODE; return;
                case ParamConsts.EXAM_NAME: queryRecipeDto.CategoryCode = ParamConsts.EXAM_CODE; return;
                case ParamConsts.TREAT_NAME: queryRecipeDto.CategoryCode = ParamConsts.TREAT_CODE; return;
                case ParamConsts.ALL_NAME: queryRecipeDto.CategoryCode = string.Empty; return;
                default:
                    {
                        List<NursingRecipeTypeDto> nursingRecipeTypes = _grpcClient.GetAllNursingRecipeTypes();
                        queryRecipeDto.UsageCodes = nursingRecipeTypes.Where(x => x.TypeName == queryRecipeDto.CategoryCode).Select(x => x.UsageCode).ToList();
                        queryRecipeDto.CategoryCode = ParamConsts.MEDICINE_CODE;
                        return;
                    }
            }
        }

        /// <summary>
        /// 根据条件查询执行单数据
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        private async Task<List<RecipeExecDto>> QueryRecipeExecDataAsync(QueryRecipeDto queryRecipeDto)
        {
            NursingConfig config = await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == ConfigKeyConsts.EXECUTIONMEDICALORDERS);
            if (config != null && config.Value == "true")
            {
                queryRecipeDto.AreaCodes = null;
            }

            List<RecipeExecDto> recipeDtoList = new List<RecipeExecDto>();
            var recipeInfoList = (from recipe in (await _recipeRepository.GetQueryableAsync())
                    .Where(x => x.PIID == queryRecipeDto.PiId)
                    .WhereIf(!string.IsNullOrEmpty(queryRecipeDto.CategoryCode), x => x.CategoryCode == queryRecipeDto.CategoryCode)
                    .WhereIf((queryRecipeDto.AreaCodes != null && queryRecipeDto.AreaCodes.Any()), x => queryRecipeDto.AreaCodes.Contains(x.AreaCode))
                    .WhereIf(queryRecipeDto.StartTime.HasValue, x => x.ApplyTime >= queryRecipeDto.StartTime)
                        .WhereIf(queryRecipeDto.EndTime.HasValue, x => x.ApplyTime < queryRecipeDto.EndTime).AsQueryable()

                                  join recipeExec in (await _recipeExecRepository.GetQueryableAsync())
                                    .WhereIf(queryRecipeDto.ExecuteStatus.HasValue, x => x.ExecuteStatus == (ExecuteStatusEnum)queryRecipeDto.ExecuteStatus.Value).AsQueryable()
                                    on recipe.RecipeNo equals recipeExec.RecipeNo

                                  join prescribeRepository in (await _prescribeRepository.GetQueryableAsync()).AsQueryable()
                                  on recipe.Id equals prescribeRepository.RecipeId into leftjoin2
                                  from prescribe in leftjoin2.DefaultIfEmpty()

                                  select new { Recipe = recipe, Prescribe = prescribe, RecipeExec = recipeExec }).ToList();

            recipeDtoList = (from item in recipeInfoList
                             select new RecipeExecDto
                             {
                                 RecipeId = item.Recipe.Id,
                                 RecipeExecId = item.RecipeExec.Id,
                                 RecipeNo = item.Recipe.RecipeNo,
                                 RecipeGroupNo = item.Recipe.RecipeGroupNo,
                                 PlatformType = (int)item.Recipe.PlatformType,
                                 ExecuteStatus = item.RecipeExec.ExecuteStatus,
                                 CategoryCode = item.Recipe.CategoryCode,
                                 CategoryName = item.Recipe.CategoryName,
                                 PrescribeTypeCode = item.Recipe.PrescribeTypeCode,
                                 PrescribeTypeName = item.Recipe.PrescribeTypeName,
                                 ApplyTime = item.Recipe.ApplyTime,
                                 Code = item.Recipe.Code,
                                 Name = item.Recipe.Name,
                                 Specification = item.Prescribe?.Specification,
                                 DosageQty = item.Prescribe?.DosageQty,
                                 DosageUnit = item.Prescribe?.DosageUnit,
                                 UsageCode = item.Prescribe?.UsageCode,
                                 UsageName = item.Prescribe?.UsageName,
                                 FrequencyCode = item.Prescribe?.FrequencyCode,
                                 FrequencyName = item.Prescribe?.FrequencyName,
                                 LongDays = item.Prescribe?.LongDays,
                                 SkinTestResult = item.Prescribe?.SkinTestResult,
                                 Price = item.Recipe.Price,
                                 ApplyDoctorCode = item.Recipe.ApplyDoctorCode,
                                 ApplyDoctorName = item.Recipe.ApplyDoctorName,
                                 PlanExcuteTime = item.RecipeExec.PlanExcuteTime,
                                 CheckNurseCode = item.RecipeExec.CheckNurseCode,
                                 CheckNurseName = item.RecipeExec.CheckNurseName,
                                 CheckTime = item.RecipeExec.CheckTime,
                                 TwoCheckNurseCode = item.RecipeExec.TwoCheckNurseCode,
                                 TwoCheckNurseName = item.RecipeExec.TwoCheckNurseName,
                                 TwoCheckTime = item.RecipeExec.TwoCheckTime,
                                 ExcuteNurseCode = item.RecipeExec.ExecuteNurseCode,
                                 ExcuteNurseName = item.RecipeExec.ExecuteNurseName,
                                 ExcuteNurseTime = item.RecipeExec.ExecuteNurseTime,
                                 Dosage = item.RecipeExec.TotalDosage,
                                 ReserveDosage = item.RecipeExec.ReserveDosage,
                                 TotalDosage = item.RecipeExec.TotalExecDosage,
                                 TotalRemainDosage = item.RecipeExec.TotalRemainDosage,
                                 TotalDiscardDosage = item.RecipeExec.TotalDiscardDosage,
                                 Unit = item.RecipeExec.Unit,
                                 IsDiscard = item.RecipeExec.IsDiscard,
                                 IsPrint = item.RecipeExec.IsPrint,
                                 RecieveQty = item.Recipe.RecieveQty,
                                 RecieveUnit = item.Recipe.RecieveUnit,
                                 Remark = item.Recipe.Remarks,
                                 AreaCode = item.Recipe.AreaCode
                             }).ToList();

            return recipeDtoList;
        }

        /// <summary>
        /// 检查患者信息，绿通患者直接将未缴费变为已缴费
        /// </summary>
        /// <param name="piId"></param>
        private async Task CheckPatientAsync(Guid piId)
        {
            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(piId);
            if (admissionRecordDto == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(admissionRecordDto.GreenRoad))
            {
                return;
            }

            List<Recipe> recipes = await _recipeRepository.GetListAsync(x => x.PIID == piId);
            foreach (Recipe recipe in recipes)
            {
                if (recipe.PayStatus == EPayStatus.NoPayment)
                {
                    recipe.PayStatus = EPayStatus.HavePaid;
                }
            }

            List<RecipeExec> recipeExecs = await _recipeExecRepository.GetListAsync(x => x.PIID == piId);
            List<RecipeExec> updateRecipeExecs = new List<RecipeExec>();
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                if (recipeExec.ExecuteStatus == ExecuteStatusEnum.NoPay)
                {
                    recipeExec.ExecuteStatus = ExecuteStatusEnum.UnCheck;
                    updateRecipeExecs.Add(recipeExec);
                }
            }

            if (recipes.Any()) await _recipeRepository.UpdateManyAsync(recipes);
            if (updateRecipeExecs.Any())
            {
                await _recipeExecRepository.UpdateManyAsync(updateRecipeExecs);
                await _pdaAppService.RecipeExecuteToPdaAsync(piId, updateRecipeExecs, "15", admissionRecordDto);
            }
        }

        /// <summary>
        /// 拉取his医嘱
        /// </summary>
        /// <param name="piId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task PullHisRecipeAsync(Guid piId, string patientId)
        {
            if (string.IsNullOrEmpty(patientId) || piId == Guid.Empty)
            {
                throw new Exception("请求参数为空");
            }

            var queryRecipeDto = new QueryRecipeDto()
            {
                PiId = piId,
                PatientId = patientId
            };

            using var uow = UnitOfWorkManager.Begin();
            try
            {
                if (_configuration["HospitalCode"] == "PKU")
                {
                    await QueryHisDataAsync(queryRecipeDto, uow);
                    await uow.SaveChangesAsync();
                }

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
        /// 查询his医嘱数据
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private async Task QueryHisDataAsync(QueryRecipeDto queryRecipeDto, IUnitOfWork uow)
        {
            return;
            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(queryRecipeDto.PiId);
            if (admissionRecordDto == null)
            {
                throw new Exception("没有查到患者信息");
            }

            List<SubmitDoctorsAdviceEto> submitDoctorsAdviceEtos = new List<SubmitDoctorsAdviceEto>();
            if (_ddpHospital.DdpSwitch)
            {
                PKUQueryHisRecipeRequest req = new PKUQueryHisRecipeRequest();
                req.PatientId = queryRecipeDto.PatientId;
                req.VisitNo = admissionRecordDto.VisSerialNo;
                var ddpResponse = await _ddpApiService.QueryHisAllRecipeListAsync(req);
                submitDoctorsAdviceEtos = ddpResponse.Data;
                foreach (var submitDoctor in submitDoctorsAdviceEtos)
                {
                    submitDoctor.DoctorsAdvice.Id = Guid.NewGuid();
                    submitDoctor.DoctorsAdvice.PIID = queryRecipeDto.PiId;
                    submitDoctor.DoctorsAdvice.PayStatus = EPayStatus.HavePaid;
                    submitDoctor.DoctorsAdvice.PrescribeTypeCode = "PrescribeTemp";
                    submitDoctor.DoctorsAdvice.PrescribeTypeName = "临";

                    if (submitDoctor.DoctorsAdvice.ItemType == 0)
                    {
                        submitDoctor.Prescribe.Id = Guid.NewGuid();
                        submitDoctor.Prescribe.DoctorsAdviceId = submitDoctor.DoctorsAdvice.Id;
                        submitDoctor.DoctorsAdvice.RecipeNo = submitDoctor.DoctorsAdvice.RecipeNo + submitDoctor.DoctorsAdvice.RecipeGroupNo;
                        submitDoctor.DoctorsAdvice.CategoryCode = "Medicine";
                        submitDoctor.DoctorsAdvice.CategoryName = "药物";
                        submitDoctor.Lis = null;
                        submitDoctor.Pacs = null;
                        submitDoctor.Treat = null;
                    }

                    if (submitDoctor.DoctorsAdvice.ItemType == 2)
                    {
                        submitDoctor.Lis.Id = Guid.NewGuid();
                        submitDoctor.Lis.DoctorsAdviceId = submitDoctor.DoctorsAdvice.Id;
                        submitDoctor.DoctorsAdvice.CategoryCode = "Lab";
                        submitDoctor.DoctorsAdvice.CategoryName = "检验";
                        submitDoctor.Prescribe = null;
                        submitDoctor.Pacs = null;
                        submitDoctor.Treat = null;
                    }

                    if (submitDoctor.DoctorsAdvice.ItemType == 1)
                    {
                        submitDoctor.Pacs.Id = Guid.NewGuid();
                        submitDoctor.Pacs.DoctorsAdviceId = submitDoctor.DoctorsAdvice.Id;
                        submitDoctor.DoctorsAdvice.CategoryCode = "Exam";
                        submitDoctor.DoctorsAdvice.CategoryName = "检查";
                        submitDoctor.Prescribe = null;
                        submitDoctor.Lis = null;
                        submitDoctor.Treat = null;
                    }

                    if (submitDoctor.DoctorsAdvice.ItemType == 3)
                    {
                        submitDoctor.Treat.Id = Guid.NewGuid();
                        submitDoctor.Treat.DoctorsAdviceId = submitDoctor.DoctorsAdvice.Id;
                        submitDoctor.DoctorsAdvice.CategoryCode = "Treat";
                        submitDoctor.DoctorsAdvice.CategoryName = "处置";
                        submitDoctor.Prescribe = null;
                        submitDoctor.Lis = null;
                        submitDoctor.Pacs = null;
                    }
                }
            }

            if (submitDoctorsAdviceEtos.Any())
            {
                List<Recipe> recipes = await _recipeAppService.RecipeCreatedAsync(submitDoctorsAdviceEtos, false);
                await uow.SaveChangesAsync();
                if (recipes.Any())
                {
                    await _recipeAppService.SplitAsync(recipes);
                }
            }
        }

        /// <summary>
        /// 二次核对及批量二次核对
        /// </summary>
        /// <param name="checkExecDtos"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<bool> TwoCkeckAsync(List<CheckExecDto> checkExecDtos)
        {
            if (checkExecDtos == null || !checkExecDtos.Any())
            {
                throw new Exception("请求参数为空");
            }

            var recipeExecIds = checkExecDtos.Select(x => x.RecipeExecId);
            List<RecipeExec> recipeExecList = await _recipeExecRepository.GetListAsync(x => recipeExecIds.Contains(x.Id));
            if (!recipeExecList.Any())
            {
                throw new Exception("没有找到需要核对的医嘱");
            }

            foreach (var recipeExec in recipeExecList)
            {
                if (recipeExec.ExecuteStatus != ExecuteStatusEnum.PreExec) continue;
                var checkExecDto = checkExecDtos.FirstOrDefault(x => x.RecipeExecId == recipeExec.Id);
                if (checkExecDto == null) continue;

                recipeExec.TwoCheckNurseCode = checkExecDto.OperateCode;
                recipeExec.TwoCheckNurseName = checkExecDto.OperateName;
                recipeExec.TwoCheckTime = checkExecDto.OperateTime;
            }

            await _recipeExecRepository.UpdateManyAsync(recipeExecList);
            await WriteOperationRecordAsync(checkExecDtos, "二次核对");
            return true;
        }

        /// <summary>
        /// 更新备用量
        /// </summary>
        /// <param name="execDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateReserveDosageAsync(ExecDto execDto)
        {
            if (execDto == null)
            {
                throw new Exception("请求参数为空");
            }

            RecipeExec recipeExec = await _recipeExecRepository.FirstOrDefaultAsync(x => x.Id == execDto.RecipeExecId);
            if (recipeExec == null)
            {
                throw new Exception("没有找到医嘱");
            }

            recipeExec.ReserveDosage = execDto.ReserveDosage;

            await _recipeExecRepository.UpdateAsync(recipeExec);
            return true;
        }

        /// <summary>
        /// 同步到医生站
        /// </summary>
        /// <param name="recipeExecList"></param>
        /// <param name="status"></param>
        private async Task SendToDoctorAsync(IEnumerable<RecipeExec> recipeExecList, EDoctorsAdviceStatus status)
        {
            RecipeExec recipeExec = recipeExecList.First();
            IEnumerable<string> recipeNos = recipeExecList.Select(x => x.RecipeNo);
            List<Recipe> recipeList = await _recipeRepository.GetListAsync(x => recipeNos.Contains(x.RecipeNo));
            List<Guid> recipeIds = recipeList.Select(x => x.Id).ToList();
            //查询附加处置
            List<Treat> treatList = await _treatRepository.GetListAsync(x => recipeIds.Contains(x.AdditionalItemsId.Value));

            recipeIds.AddRange(treatList.Select(x => x.RecipeId));
            if (!recipeIds.IsNullOrEmpty())
            {
                var eto = new SyncDoctorsAdviceEto()
                {
                    Ids = recipeIds,
                    OperationCode = recipeExec.ExecuteNurseCode,
                    OperationName = recipeExec.ExecuteNurseName,
                    OperationTime = recipeExec.ExecuteNurseTime.HasValue ? recipeExec.ExecuteNurseTime.Value : DateTime.Now,
                    DoctorsAdviceStatus = status
                };
                await _capPublisher.PublishAsync("syncadvice.nursingservice.to.recipeservice", eto);
            }
        }

        /// <summary>
        /// 同步执行状态到Pda
        /// </summary>
        /// <param name="recipeExecList"></param>
        /// <param name="orderStatus">4：已执行 6：撤销执行</param>
        /// <returns></returns>
        private async Task ExecStatusToPdaAsync(IEnumerable<RecipeExec> recipeExecList, string orderStatus)
        {
            RecipeExec recipeExec = recipeExecList.First();
            //同步到PDA
            await _pdaAppService.ExecuteRecordToPdaAsync(recipeExec.PIID, recipeExecList, orderStatus);
        }

        /// <summary>
        /// 同步核对状态到Pda
        /// </summary>
        /// <param name="recipeExecList"></param>
        /// <param name="orderStatus">1：待执行 15：待核对</param>
        /// <returns></returns>
        private async Task CheckStatusToPdaAsync(IEnumerable<RecipeExec> recipeExecList, string orderStatus)
        {
            RecipeExec recipeExec = recipeExecList.First();
            //同步到PDA
            await _pdaAppService.ExecuteStatusToPdaAsync(recipeExec.PIID, recipeExecList, orderStatus);
        }

        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <param name="operationContent"></param>
        /// <returns></returns>
        private async Task WriteOperationRecordAsync(BaseRequestDto baseRequest, string operationContent)
        {
            List<BaseRequestDto> baseRequestDtos = new List<BaseRequestDto>
            {
                baseRequest
            };
            await WriteOperationRecordAsync(baseRequestDtos, operationContent);
        }

        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="baseRequestDtos"></param>
        /// <param name="operationContent"></param>
        /// <returns></returns>
        private async Task WriteOperationRecordAsync(IEnumerable<BaseRequestDto> baseRequestDtos, string operationContent)
        {
            List<RecipeExecHistory> recipeExecHistoryList = new List<RecipeExecHistory>();
            foreach (var baseRequestDto in baseRequestDtos)
            {
                RecipeExecHistory recipeExecHistory = new RecipeExecHistory();
                recipeExecHistory.RecipeId = Guid.Empty;
                recipeExecHistory.RecipeExecId = baseRequestDto.RecipeExecId;
                recipeExecHistory.OperationTime = baseRequestDto.OperateTime;
                recipeExecHistory.NurseCode = baseRequestDto.OperateCode;
                recipeExecHistory.NurseName = baseRequestDto.OperateName;
                recipeExecHistory.OperationContent = operationContent;
                recipeExecHistoryList.Add(recipeExecHistory);
            }
            await _recipeExecHistoryRepository.InsertManyAsync(recipeExecHistoryList);
        }

        /// <summary>
        /// 获取执行记录
        /// </summary>
        /// <param name="recipeExecId"></param>
        /// <returns></returns>
        public async Task<List<RecipeExecRecord>> GetRecipeExecRecordListAsync(Guid recipeExecId)
        {
            if (recipeExecId == Guid.Empty)
            {
                throw new Exception("请求参数为空");
            }

            List<RecipeExecRecord> recipeExecRecords = (await _recipeExecRecordRepository.GetListAsync(x => x.RecipeExecId == recipeExecId && x.ExecRecordStatus == ExecRecordStatusEnum.Exec)).OrderBy(x => x.ExcuteNurseTime).ToList();

            return recipeExecRecords;
        }

        /// <summary>
        /// 获取瓶贴打印
        /// </summary>
        /// <param name="recipeExecId"></param>
        /// <returns></returns>
        [HttpGet]
        [NonUnify]
        [AllowAnonymous]
        public async Task<BottleLableDto> GetBottleLableAsync(Guid recipeExecId)
        {
            List<RecipeExecDto> recipeDtoList = new List<RecipeExecDto>();
            List<AdmissionRecordDto> admissionRecords = new List<AdmissionRecordDto>();
            var recipeInfoList = (from recipe in (await _recipeRepository.GetQueryableAsync()).AsQueryable()
                                  join recipeExec in (await _recipeExecRepository.GetQueryableAsync()).Where(x => x.Id == recipeExecId).AsQueryable()
                                    on recipe.RecipeNo equals recipeExec.RecipeNo
                                  join prescribeRepository in (await _prescribeRepository.GetQueryableAsync()).AsQueryable()
                                  on recipe.Id equals prescribeRepository.RecipeId into leftjoin2
                                  from prescribe in leftjoin2.DefaultIfEmpty()
                                  select new { Recipe = recipe, Prescribe = prescribe, RecipeExec = recipeExec }).ToList();

            recipeDtoList = (from item in recipeInfoList
                             select new RecipeExecDto
                             {
                                 RecipeId = item.Recipe.Id,
                                 RecipeExecId = item.RecipeExec.Id,
                                 PI_Id = item.Recipe.PIID,
                                 RecipeNo = item.Recipe.RecipeNo,
                                 RecipeGroupNo = item.Recipe.RecipeGroupNo,
                                 PlatformType = (int)item.Recipe.PlatformType,
                                 ExecuteStatus = item.RecipeExec.ExecuteStatus,
                                 CategoryCode = item.Recipe.CategoryCode,
                                 CategoryName = item.Recipe.CategoryName,
                                 PrescribeTypeCode = item.Recipe.PrescribeTypeCode,
                                 PrescribeTypeName = item.Recipe.PrescribeTypeName,
                                 ApplyTime = item.Recipe.ApplyTime,
                                 Code = item.Recipe.Code,
                                 Name = item.Recipe.Name,
                                 Specification = item.Prescribe?.Specification,
                                 DosageQty = item.Prescribe?.DosageQty,
                                 DosageUnit = item.Prescribe?.DosageUnit,
                                 UsageCode = item.Prescribe?.UsageCode,
                                 UsageName = item.Prescribe?.UsageName,
                                 FrequencyCode = item.Prescribe?.FrequencyCode,
                                 FrequencyName = item.Prescribe?.FrequencyName,
                                 LongDays = item.Prescribe?.LongDays,
                                 SkinTestResult = item.Prescribe?.SkinTestResult,
                                 Price = item.Recipe.Price,
                                 ApplyDoctorCode = item.Recipe.ApplyDoctorCode,
                                 ApplyDoctorName = item.Recipe.ApplyDoctorName,
                                 PlanExcuteTime = item.RecipeExec.PlanExcuteTime,
                                 CheckNurseCode = item.RecipeExec.CheckNurseCode,
                                 CheckNurseName = item.RecipeExec.CheckNurseName,
                                 CheckTime = item.RecipeExec.CheckTime,
                                 TwoCheckNurseCode = item.RecipeExec.TwoCheckNurseCode,
                                 TwoCheckNurseName = item.RecipeExec.TwoCheckNurseName,
                                 TwoCheckTime = item.RecipeExec.TwoCheckTime,
                                 ExcuteNurseCode = item.RecipeExec.ExecuteNurseCode,
                                 ExcuteNurseName = item.RecipeExec.ExecuteNurseName,
                                 ExcuteNurseTime = item.RecipeExec.ExecuteNurseTime,
                                 Dosage = item.RecipeExec.TotalDosage,
                                 ReserveDosage = item.RecipeExec.ReserveDosage,
                                 TotalDosage = item.RecipeExec.TotalExecDosage,
                                 TotalRemainDosage = item.RecipeExec.TotalRemainDosage,
                                 TotalDiscardDosage = item.RecipeExec.TotalDiscardDosage,
                                 Unit = item.RecipeExec.Unit,
                                 IsDiscard = item.RecipeExec.IsDiscard,
                                 IsPrint = item.RecipeExec.IsPrint
                             }).ToList();

            Guid recipeId = recipeDtoList.First().RecipeId;
            DateTime planExcuteTime = recipeDtoList.First().PlanExcuteTime;
            List<RecipeExec> recipeExecList = await _recipeExecRepository.GetListAsync(x => x.RecipeId == recipeId);
            List<Guid> ids = recipeExecList.Where(x => x.PlanExcuteTime.Date == planExcuteTime.Date).OrderBy(x => x.PlanExcuteTime).Select(x => x.Id).ToList();
            int total = ids.Count();
            int index = ids.IndexOf(recipeExecId) + 1;
            foreach (RecipeExecDto item in recipeDtoList)
            {
                item.Order = $"{index}/{total}";
            }

            Guid? pi_Id = recipeDtoList.FirstOrDefault()?.PI_Id;
            if (pi_Id.HasValue)
            {
                AdmissionRecordDto admissionRecord = await _patientAppService.GetPatientInfoAsync(pi_Id.Value);
                admissionRecords.Add(admissionRecord);
            }

            return new BottleLableDto()
            {
                recipeExecs = recipeDtoList,
                admissionRecords = admissionRecords
            };
        }

        /// <summary>
        /// 更新是否已经打印
        /// </summary>
        /// <param name="recipeExecIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateIsPrintAsync(List<Guid> recipeExecIds)
        {
            if (!recipeExecIds.Any())
            {
                throw new Exception("请求参数为空");
            }

            List<RecipeExec> recipeExecs = await _recipeExecRepository.GetListAsync(x => recipeExecIds.Contains(x.Id));
            if (!recipeExecs.Any())
            {
                throw new Exception("没有找到要更新的数据");
            }

            recipeExecs.ForEach(x => x.IsPrint = true);
            await _recipeExecRepository.UpdateManyAsync(recipeExecs);
            return true;
        }

        /// <summary>
        /// 查询新医嘱
        /// </summary>
        /// <param name="piIds"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Dictionary<Guid, int>> GetNewRecipeCountAsync(List<Guid> piIds)
        {
            List<RecipeExec> recipeExec = await _recipeExecRepository.GetListAsync(x => piIds.Contains(x.PIID) && x.ExecuteStatus == ExecuteStatusEnum.UnCheck);

            Dictionary<Guid, int> result = new Dictionary<Guid, int>();
            foreach (var piid in piIds)
            {
                int count = recipeExec.Count(x => x.PIID == piid);
                result.Add(piid, count);
            }

            return result;
        }
    }
}
