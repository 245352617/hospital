using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Utils;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 护理
    /// </summary>
    public class NursingRecipeManager : DomainService
    {
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly IRecipeExecRepository _recipeExecRepository;
        private readonly ITreatRepository _treatRepository;
        private readonly RecipeSplitor _recipeSplitor;
        private readonly ILogger<NursingRecipeManager> _logger;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="prescribeRepository"></param>
        /// <param name="recipeExecRepository"></param>
        /// <param name="treatRepository"></param>
        /// <param name="logger"></param>
        public NursingRecipeManager(IPrescribeRepository prescribeRepository
            , IRecipeExecRepository recipeExecRepository
            , ITreatRepository treatRepository
            , ILogger<NursingRecipeManager> logger)
        {
            _prescribeRepository = prescribeRepository;
            _recipeExecRepository = recipeExecRepository;
            _treatRepository = treatRepository;
            _logger = logger;
            _recipeSplitor = new RecipeSplitor();
        }

        /// <summary>
        /// 医嘱拆顿到医嘱执行表
        /// </summary>
        /// <param name="nurseDate">护理排班日期</param>
        /// <param name="recipes">医嘱项主表</param>
        /// <param name="isFirstSplit">是否首日拆顿</param>
        /// <param name="nursingSettings">医嘱执行配置</param>
        /// <returns></returns>
        public async Task<List<RecipeExec>> MedicalAdviceSplitAsync(DateTime nurseDate, List<Recipe> recipes, bool isFirstSplit, NursingSettings nursingSettings)
        {
            List<Guid> recipeIds = recipes.Select(p => p.Id).ToList();
            List<Prescribe> prescribesList = await _prescribeRepository.GetListAsync(x => recipeIds.Contains(x.RecipeId));
            List<Treat> treatList = await _treatRepository.GetListAsync(x => recipeIds.Contains(x.AdditionalItemsId.Value));

            //去掉附加处置
            IEnumerable<Guid> attachIds = treatList.Select(x => x.RecipeId);
            recipes.RemoveAll(x => attachIds.Contains(x.Id));

            List<RecipeExec> execList = new List<RecipeExec>();
            var groupRecipes = recipes.GroupBy(g => g.RecipeNo);
            foreach (var groupRecipe in groupRecipes)
            {
                var recipeList = groupRecipe.ToList();
                GroupSplit(nurseDate, recipeList, prescribesList, isFirstSplit, nursingSettings, execList);
            }
            execList = await CheckExecExistsAsync(execList);
            return execList;
        }

        /// <summary>
        /// 对成组的医嘱进行拆顿处理
        /// </summary>
        /// <param name="nurseDate"></param>
        /// <param name="recipeList"></param>
        /// <param name="prescribes"></param>
        /// <param name="isFirstSplit"></param>
        /// <param name="nursingSettings"></param>
        /// <param name="execList"></param>
        private void GroupSplit(DateTime nurseDate, List<Recipe> recipeList, List<Prescribe> prescribes, bool isFirstSplit, NursingSettings nursingSettings, List<RecipeExec> execList)
        {
            var baseRecipe = recipeList.Find(x => x.ItemType == ERecipeItemType.Prescribe);//非药物医嘱，返回null
            baseRecipe ??= recipeList.FirstOrDefault();
            if (baseRecipe == null)
            {
                return;
            }

            var recipeIds = recipeList.Select(p => p.Id).ToList();
            var prescribeList = prescribes?.Where(p => recipeIds.Contains(p.RecipeId)).ToList();
            var basePrescribe = prescribeList?.FirstOrDefault();
            basePrescribe = basePrescribe.CloneJson();

            if (basePrescribe != null)//执行单剂量汇总 存在单位不一致的情况 待产品给出解决方案
            {
                basePrescribe.QtyPerTimes = prescribeList.Sum(p => p.QtyPerTimes);
                basePrescribe.DosageQty = prescribeList.Where(x => x.DosageUnit.Trim().ToLower() == "ml").Sum(p => p.DosageQty);
            }

            string frequencyCode = "ST";
            if (basePrescribe != null && !string.IsNullOrEmpty(basePrescribe.FrequencyCode))
            {
                frequencyCode = basePrescribe.FrequencyCode;
            }

            var frequency = new Frequency()
            {
                FrequencyCode = frequencyCode,//这里用code，name是中文描述，如果是非药物医嘱，code会为空，默认提供ST。
                Times = basePrescribe?.FrequencyTimes,
                Unit = basePrescribe?.FrequencyUnit,
                ExecuteDayTime = basePrescribe?.FrequencyExecDayTimes,
                StartTime = baseRecipe?.StartTime ?? baseRecipe.ApplyTime,  //如果填写了开始时间，则使用开始时间，如果没有填写开始时间，则使用开嘱时间。
                EndTime = baseRecipe.Status == EDoctorsAdviceStatus.Stopped ? baseRecipe.StopTime : baseRecipe?.EndTime   //已停嘱则使用停嘱时间作为结束时间
            };

            if (nursingSettings.NeedSplit)
            {
                bool isLongRecipe = baseRecipe?.PrescribeTypeCode == "PrescribeLong" ? true : false;
                IEnumerable<DateTime> planTimes;
                try
                {
                    planTimes = _recipeSplitor.Split(frequency, nurseDate, isLongRecipe, nursingSettings.WorkDayOffset, nursingSettings.WorkStartDayTime, nursingSettings.WorkSendDayTime, isFirstSplit, basePrescribe != null ? basePrescribe.LongDays : 1);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    _logger.LogError("拆顿时间解析失败,按照ST执行。医嘱数据:{0}", baseRecipe.ToJson());
                    planTimes = new List<DateTime>() { baseRecipe.ApplyTime.AddMinutes(nursingSettings.TempOffsetMinutes) };
                }

                foreach (var planTime in planTimes)
                {
                    RecipeExec exec;
                    if (basePrescribe == null)
                    {
                        exec = RecipeExec.Create(GuidGenerator.Create(), baseRecipe, planTime);
                    }
                    else
                    {
                        exec = RecipeExec.CreatePrescribe(GuidGenerator.Create(), baseRecipe, basePrescribe, planTime);
                    }
                    execList.Add(exec);
                }
            }
            else//不拆顿，直接生成一条执行记录
            {
                DateTime planTime = baseRecipe.ApplyTime.AddMinutes(nursingSettings.TempOffsetMinutes);
                RecipeExec exec;
                if (basePrescribe == null)
                {
                    exec = RecipeExec.Create(GuidGenerator.Create(), baseRecipe, planTime);
                }
                else
                {
                    exec = RecipeExec.CreatePrescribe(GuidGenerator.Create(), baseRecipe, basePrescribe, planTime);
                }
                execList.Add(exec);
            }
        }

        /// <summary>
        /// 检查执行记录在数据库是否存在
        /// </summary>
        /// <param name="execList"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<List<RecipeExec>> CheckExecExistsAsync(List<RecipeExec> execList)
        {
            List<RecipeExec> execs = new List<RecipeExec>();
            IEnumerable<string> recipeNos = execList.Select(x => x.RecipeNo);
            List<RecipeExec> orderExecList = await _recipeExecRepository.GetListAsync(x => recipeNos.Contains(x.RecipeNo));
            foreach (RecipeExec exec in execList)
            {
                //已经存在的医嘱不重复生成
                if (!orderExecList.Exists(p => p.PlatformType == exec.PlatformType && p.PIID == exec.PIID && p.RecipeNo == exec.RecipeNo && p.PlanExcuteTime == exec.PlanExcuteTime))
                {
                    execs.Add(exec);
                }
            }
            return execs;
        }
    }
}
