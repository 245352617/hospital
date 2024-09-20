using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Common;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Nursing.Config;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Patient;
using YiJian.Rpc;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：执行单打印数据接口服务
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:22:59
    /// </summary>
    [Authorize]
    public class ExecutePrintAppService : NursingAppService, IExecutePrintAppService
    {
        private readonly PatientAppService _patientAppService;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly IRecipeExecRepository _recipeExecRepository;
        private readonly INursingConfigRepository _nursingConfigRepository;
        private readonly NursingConfigAppService _nursingConfigAppService;
        private readonly GrpcClient _grpcClient;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="patientAppService"></param>
        /// <param name="recipeRepository"></param>
        /// <param name="prescribeRepository"></param>
        /// <param name="recipeExecRepository"></param>
        /// <param name="nursingConfigRepository"></param>
        /// <param name="nursingConfigAppService"></param>
        /// <param name="grpcClient"></param>
        public ExecutePrintAppService(PatientAppService patientAppService
            , IRecipeRepository recipeRepository
            , IPrescribeRepository prescribeRepository
            , IRecipeExecRepository recipeExecRepository
            , INursingConfigRepository nursingConfigRepository
            , NursingConfigAppService nursingConfigAppService
            , GrpcClient grpcClient)
        {
            _patientAppService = patientAppService;
            _recipeRepository = recipeRepository;
            _prescribeRepository = prescribeRepository;
            _recipeExecRepository = recipeExecRepository;
            _nursingConfigRepository = nursingConfigRepository;
            _nursingConfigAppService = nursingConfigAppService;
            _grpcClient = grpcClient;
        }

        /// <summary>
        /// 获取执行单打印数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [HttpPost]
        [AllowAnonymous]
        public async Task<object> GetExecutePrintDataAsync(ExecuteQueryInput input)
        {
            if (input == null || input.PI_Id == Guid.Empty)
            {
                throw new ArgumentNullException("input");
            }

            switch (input.CardType)
            {
                case CardTypeEnum.Execute: return await QueryExecutePrintDataAsync(input);
                case CardTypeEnum.Infusion: return await QueryInfusionPrintDataAsync(input);
                case CardTypeEnum.Oral: return await QueryOralPrintDataAsync(input);
                case CardTypeEnum.Injection: return await QueryInjectionPrintDataAsync(input);
                default: throw new ArgumentOutOfRangeException("CardType");
            }
        }

        /// <summary>
        /// 注射卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<InjectionPrintDto> QueryInjectionPrintDataAsync(ExecuteQueryInput input)
        {
            InjectionPrintDto injectionPrintDto = new InjectionPrintDto();
            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(input.PI_Id);
            injectionPrintDto.DeptName = admissionRecordDto.TriageDeptName;
            injectionPrintDto.ExcuteDate = input.ExecuteDate.ToString("yyyy.MM.dd");
            injectionPrintDto.PrintDate = DateTime.Now.ToString("yyyy/MM/dd");

            var recipes = (from exec in (await _recipeExecRepository.GetQueryableAsync()).AsNoTracking()
               .Where(x => x.PIID == input.PI_Id)
               .Where(x => x.ExecuteNurseTime.HasValue && x.ExecuteNurseTime.Value.Date == input.ExecuteDate.Date)

                           join r in (await _recipeRepository.GetQueryableAsync()).AsNoTracking()
                          .WhereIf(!string.IsNullOrEmpty(input.PrescribeTypeCode), x => x.PrescribeTypeCode == input.PrescribeTypeCode)
                           on exec.RecipeNo equals r.RecipeNo into temp1
                           from temp in temp1.DefaultIfEmpty()

                           join p in (await _prescribeRepository.GetQueryableAsync()).AsNoTracking()
                           on temp.Id equals p.RecipeId into temp2
                           from re in temp2.DefaultIfEmpty()
                           select new
                           {
                               Name = temp.Name,
                               DosageQty = re != null ? re.DosageQty : 0m,
                               UsageCode = re != null ? re.UsageCode : string.Empty,
                               UsageName = re != null ? re.UsageName : string.Empty,
                               FrequencyCode = re != null ? re.FrequencyCode : string.Empty,
                               ExecuteNurseTime = exec.ExecuteNurseTime,
                               ExecuteNurseName = exec.ExecuteNurseName,
                               CheckTime = exec.CheckTime,
                               CheckNurseName = exec.CheckNurseName
                           }).ToList();

            NursingSettings nursingSettings = await GetNursingSettingsAsync();
            List<InjectionDetail> injectionDetails = new List<InjectionDetail>();
            foreach (var recipe in recipes)
            {
                //if (!nursingSettings.IsInjection(recipe.UsageCode))
                //{
                //    continue;
                //}
                InjectionDetail injectionDetail = new InjectionDetail();
                injectionDetail.Bed = admissionRecordDto.Bed;
                injectionDetail.Name = admissionRecordDto.PatientName;
                injectionDetail.RecipeName = recipe.Name;
                injectionDetail.DosageQty = recipe.DosageQty.ToString();
                injectionDetail.FrequencyCode = recipe.FrequencyCode;
                injectionDetail.UsageName = recipe.UsageName;
                injectionDetail.ExecuteTime = recipe.ExecuteNurseTime?.ToString("HH:mm");
                injectionDetail.ExecuteName = recipe.ExecuteNurseName;
                injectionDetail.CheckTime = recipe.CheckTime?.ToString("HH:mm");
                injectionDetail.CheckName = recipe.CheckNurseName;
                injectionDetails.Add(injectionDetail);
            }
            injectionPrintDto.InjectionDetails = injectionDetails;

            return injectionPrintDto;
        }

        /// <summary>
        /// 口服卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<OralPrintDto> QueryOralPrintDataAsync(ExecuteQueryInput input)
        {
            OralPrintDto oralPrintDto = new OralPrintDto();
            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(input.PI_Id);
            oralPrintDto.DeptName = admissionRecordDto.TriageDeptName;
            oralPrintDto.ExcuteDate = input.ExecuteDate.ToString("yyyy.MM.dd");
            oralPrintDto.PrintDate = DateTime.Now.ToString("yyyy/MM/dd");

            var recipes = (from r in (await _recipeRepository.GetQueryableAsync()).AsNoTracking()
                           .Where(x => x.PIID == input.PI_Id)
                           .Where(x => x.CategoryCode == "Medicine")
                           .Where(x => x.Status != EDoctorsAdviceStatus.Cancelled)
                           .Where(x => x.Status != EDoctorsAdviceStatus.Stopped)
                           .Where(x => x.ApplyTime.Date == input.ExecuteDate.Date)
                          .WhereIf(!string.IsNullOrEmpty(input.PrescribeTypeCode), x => x.PrescribeTypeCode == input.PrescribeTypeCode)

                           join p in (await _prescribeRepository.GetQueryableAsync())
                           on r.Id equals p.RecipeId into temp2
                           from re in temp2.DefaultIfEmpty()
                           select new
                           {
                               Name = r.Name,
                               DosageQty = re != null ? re.DosageQty : 0m,
                               UsageCode = re != null ? re.UsageCode : string.Empty,
                               UsageName = re != null ? re.UsageName : string.Empty,
                               FrequencyCode = re != null ? re.FrequencyCode : string.Empty,
                               FrequencyExecDayTimes = re != null ? re.FrequencyExecDayTimes : string.Empty,
                               Remarks = r.Remarks
                           }).ToList();

            List<OralDetail> oralDetails = new List<OralDetail>();
            foreach (var recipe in recipes)
            {
                if (!recipe.UsageName.Contains("口服"))
                {
                    continue;
                }
                OralDetail oralDetail = new OralDetail();
                oralDetail.Bed = admissionRecordDto.Bed;
                oralDetail.Name = admissionRecordDto.PatientName;
                oralDetail.RecipeName = recipe.Name;
                oralDetail.DosageQty = recipe.DosageQty.ToString();
                oralDetail.Remark = recipe.Remarks;
                oralDetail.FrequencyCode = recipe.FrequencyCode;
                oralDetail.UsageName = recipe.UsageName;
                oralDetail.FrequencyExecDayTimes = recipe.FrequencyExecDayTimes;
                oralDetails.Add(oralDetail);
            }
            oralPrintDto.OralDetails = oralDetails;

            return oralPrintDto;
        }

        /// <summary>
        /// 输液巡视卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<InfusionPrintDto> QueryInfusionPrintDataAsync(ExecuteQueryInput input)
        {
            InfusionPrintDto infusionPrintDto = new InfusionPrintDto();
            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(input.PI_Id);
            infusionPrintDto.Name = admissionRecordDto.PatientName;
            infusionPrintDto.ExcuteDate = input.ExecuteDate.ToString("yyyy.MM.dd");
            infusionPrintDto.DeptName = admissionRecordDto.TriageDeptName;
            infusionPrintDto.Bed = admissionRecordDto.Bed;

            var recipes = (from exec in (await _recipeExecRepository.GetQueryableAsync()).AsNoTracking()
                           .Where(x => x.PIID == input.PI_Id)
                           .Where(x => x.ExecuteNurseTime.HasValue && x.ExecuteNurseTime.Value.Date == input.ExecuteDate.Date)

                           join r in (await _recipeRepository.GetQueryableAsync()).AsNoTracking()
                          .WhereIf(!string.IsNullOrEmpty(input.PrescribeTypeCode), x => x.PrescribeTypeCode == input.PrescribeTypeCode)
                           on exec.RecipeNo equals r.RecipeNo into temp1
                           from temp in temp1.DefaultIfEmpty()

                           join p in (await _prescribeRepository.GetQueryableAsync())
                           on temp.Id equals p.RecipeId into temp2
                           from re in temp2.DefaultIfEmpty()
                           select new
                           {
                               Name = temp.Name,
                               DosageQty = re != null ? re.DosageQty : 0m,
                               UsageCode = re != null ? re.UsageCode : string.Empty,
                               FrequencyName = re != null ? re.FrequencyName : string.Empty,
                               Speed = re != null ? re.Speed : string.Empty,
                               ExecuteNurseTime = exec.ExecuteNurseTime,
                               ExecuteNurseName = exec.ExecuteNurseName,
                           }).ToList();

            NursingSettings nursingSettings = await GetNursingSettingsAsync();
            List<InfusionDetail> infusionDetails = new List<InfusionDetail>();
            foreach (var recipe in recipes)
            {
                //if (!nursingSettings.IsInfusion(recipe.UsageCode))
                //{
                //    continue;
                //}
                InfusionDetail infusionDetail = new InfusionDetail();
                infusionDetail.RecipeName = recipe.Name;
                infusionDetail.DosageQty = recipe.DosageQty.ToString();
                infusionDetail.FrequencyName = recipe.FrequencyName;
                infusionDetail.Speed = recipe.Speed;
                infusionDetail.ExecuteTime = recipe.ExecuteNurseTime?.ToString("HH:mm");
                infusionDetail.ExecuteName = recipe.ExecuteNurseName;
                infusionDetails.Add(infusionDetail);
            }
            infusionPrintDto.InfusionDetails = infusionDetails;

            return infusionPrintDto;
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
        /// 查询获取医嘱执行单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<ExecutePrintDto> QueryExecutePrintDataAsync(ExecuteQueryInput query)
        {
            ExecutePrintDto executePrintDto = new ExecutePrintDto();
            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(query.PI_Id);
            if (admissionRecordDto == null)
            {
                throw new Exception("没有查到患者信息");
            }

            executePrintDto.Name = admissionRecordDto.PatientName;
            executePrintDto.Sex = admissionRecordDto.SexName;
            executePrintDto.Age = admissionRecordDto.Age;
            executePrintDto.PrintTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            executePrintDto.DeptName = admissionRecordDto.TriageDeptName;
            executePrintDto.Bed = admissionRecordDto.Bed;
            executePrintDto.VisitNo = admissionRecordDto.VisitNo;
            if (query.PrescribeTypeCode == "PrescribeTemp")
            {
                executePrintDto.PrescribeTypeName = "临时医嘱";
            }
            else if (query.PrescribeTypeCode == "PrescribeLong")
            {
                executePrintDto.PrescribeTypeName = "长期医嘱";
            }
            else
            {
                executePrintDto.PrescribeTypeName = "全部";
            }

            HandlerParam(query);
            var recipes = (from recipe in (await _recipeRepository.GetQueryableAsync())
                                    .Where(x => x.PIID == query.PI_Id)
                                    .WhereIf(!string.IsNullOrEmpty(query.CategoryCode), x => x.CategoryCode == query.CategoryCode)
                                    .WhereIf((query.AreaCodes != null && query.AreaCodes.Any()), x => query.AreaCodes.Contains(x.AreaCode))
                                    .WhereIf(!string.IsNullOrEmpty(query.PrescribeTypeCode), x => x.PrescribeTypeCode == query.PrescribeTypeCode)
                                    .WhereIf(query.StartTime.HasValue, x => x.ApplyTime >= query.StartTime)
                                    .WhereIf(query.EndTime.HasValue, x => x.ApplyTime < query.EndTime).AsQueryable()

                           join recipeExec in (await _recipeExecRepository.GetQueryableAsync())
                             .WhereIf(query.ExecuteStatus.HasValue, x => x.ExecuteStatus == (ExecuteStatusEnum)query.ExecuteStatus.Value).AsQueryable()
                             on recipe.RecipeNo equals recipeExec.RecipeNo

                           join prescribeRepository in (await _prescribeRepository.GetQueryableAsync()).AsQueryable()
                           on recipe.Id equals prescribeRepository.RecipeId into leftjoin2
                           from prescribe in leftjoin2.DefaultIfEmpty()

                           select new
                           {
                               RecipeExecId = recipeExec.Id,
                               ApplyTime = recipe.ApplyTime,
                               Name = recipe.Name,
                               RecipeNo = recipe.RecipeNo,
                               DosageQty = prescribe != null ? prescribe.DosageQty : 0m,
                               UsageName = prescribe != null ? prescribe.UsageName : string.Empty,
                               UsageCode = prescribe != null ? prescribe.UsageCode : string.Empty,
                               ApplyDoctorName = recipe.ApplyDoctorName,
                               PlanExcuteTime = recipeExec.PlanExcuteTime,
                               ExecuteNurseTime = recipeExec.ExecuteNurseTime,
                               ExecuteNurseName = recipeExec.ExecuteNurseName,
                               TwoCheckTime = recipeExec.TwoCheckTime,
                               TwoCheckNurseName = recipeExec.TwoCheckNurseName,
                               CheckTime = recipeExec.CheckTime,
                               CheckNurseName = recipeExec.CheckNurseName
                           }).ToList();

            List<ExecuteDetail> executeDetails = new List<ExecuteDetail>();
            foreach (var recipe in recipes)
            {
                ExecuteDetail executeDetail = new ExecuteDetail();
                executeDetail.RecipeExecId = recipe.RecipeExecId;
                executeDetail.ApplyDate = recipe.ApplyTime.ToString("yyyy-MM-dd");
                executeDetail.ApplyTime = recipe.ApplyTime.ToString("HH:mm");
                executeDetail.RecipeNo = recipe.RecipeNo;
                executeDetail.RecipeName = recipe.Name;
                executeDetail.DosageQty = recipe.DosageQty.ToString("#0.##");
                executeDetail.UsageName = recipe.UsageName;
                executeDetail.UsageCode = recipe.UsageCode;
                executeDetail.ApplyDoctorName = recipe.ApplyDoctorName;
                executeDetail.PlanExcuteTime = recipe.PlanExcuteTime;
                executeDetail.ExecuteTime = recipe.ExecuteNurseTime?.ToString("HH:mm");
                executeDetail.ExecuteName = recipe.ExecuteNurseName;
                executeDetail.TwoCheckTime = recipe.TwoCheckTime?.ToString("HH:mm");
                executeDetail.TwoCheckNurseName = recipe.TwoCheckNurseName;
                executeDetail.CheckTime = recipe.CheckTime?.ToString("HH:mm");
                executeDetail.CheckName = recipe.CheckNurseName;
                executeDetails.Add(executeDetail);
            }

            if (query.CategoryCode == ParamConsts.MEDICINE_CODE)
            {
                executeDetails = executeDetails.Where(x => query.UsageCodes.Contains(x.UsageCode)).ToList();
            }

            executeDetails = executeDetails.OrderBy(x => x.PlanExcuteTime)
                                           .ThenBy(x => x.RecipeNo)
                                           .ThenBy(x => x.ApplyTime)
                                           .ToList();

            executePrintDto.ExecuteDetails = executeDetails;
            return executePrintDto;
        }

        /// <summary>
        /// 处理查询请求参数
        /// </summary>
        /// <param name="query"></param>
        private void HandlerParam(ExecuteQueryInput query)
        {
            switch (query.CategoryCode)
            {
                case ParamConsts.LAB_NAME: query.CategoryCode = ParamConsts.LAB_CODE; return;
                case ParamConsts.EXAM_NAME: query.CategoryCode = ParamConsts.EXAM_CODE; return;
                case ParamConsts.TREAT_NAME: query.CategoryCode = ParamConsts.TREAT_CODE; return;
                case ParamConsts.ALL_NAME: query.CategoryCode = string.Empty; return;
                default:
                    {
                        List<NursingRecipeTypeDto> nursingRecipeTypes = _grpcClient.GetAllNursingRecipeTypes();
                        query.UsageCodes = nursingRecipeTypes.Where(x => x.TypeName == query.CategoryCode).Select(x => x.UsageCode).ToList();
                        query.CategoryCode = ParamConsts.MEDICINE_CODE;
                        return;
                    }
            }
        }
    }
}
