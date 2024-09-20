using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUglify.Helpers;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using YiJian.Common;
using YiJian.ECIS.ShareModel.Etos;
using YiJian.ECIS.ShareModel.Etos.Pdas;
using YiJian.Nursing.Config;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;
using YiJian.Patient;
using YiJian.Rpc;

namespace YiJian.Nursing.Pda
{
    /// <summary>
    /// 描述：同步信息到pda服务
    /// 创建人： yangkai
    /// 创建时间：2022/11/30 11:23:49
    /// </summary>
    [RemoteService(false)]
    public class PdaAppService : NursingAppService, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly PatientAppService _patientAppService;
        private readonly IRecipeExecRepository _recipeExecRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly GrpcClient _grpcClient;
        private readonly INursingConfigRepository _nursingConfigRepository;
        private readonly ILogger<PdaAppService> _logger;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="patientAppService"></param>
        /// <param name="recipeExecRepository"></param>
        /// <param name="prescribeRepository"></param>
        /// <param name="grpcClient"></param>
        /// <param name="nursingConfigRepository"></param>
        /// <param name="logger"></param>
        /// <param name="recipeRepository"></param>
        public PdaAppService(IConfiguration configuration
            , PatientAppService patientAppService
            , IRecipeExecRepository recipeExecRepository
            , IPrescribeRepository prescribeRepository
            , GrpcClient grpcClient
            , INursingConfigRepository nursingConfigRepository
            , ILogger<PdaAppService> logger
            , IRecipeRepository recipeRepository)
        {
            _configuration = configuration;
            _patientAppService = patientAppService;
            _recipeExecRepository = recipeExecRepository;
            _recipeRepository = recipeRepository;
            _prescribeRepository = prescribeRepository;
            _grpcClient = grpcClient;
            _nursingConfigRepository = nursingConfigRepository;
            _logger = logger;
        }

        /// <summary>
        /// 同步执行单到pda
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="recipeExecs"></param>
        /// <param name="orderStatus">医嘱状态 1：待执行 2：核对中 3：执行中 4：已执行 5：拒执行 6：撤销执行 7：已停嘱 15：待核对</param>
        /// <param name="admissionRecordDto"></param>
        /// <returns></returns>
        public async Task RecipeExecuteToPdaAsync(Guid PI_ID, IEnumerable<RecipeExec> recipeExecs, string orderStatus, AdmissionRecordDto admissionRecordDto = null)
        {
            if (!await IsSyncToPdaAsync())
            {
                return;
            }

            if (admissionRecordDto == null)
            {
                admissionRecordDto = await _patientAppService.GetPatientInfoAsync(PI_ID);
            }
            List<SeparationUsages> separationUsages = _grpcClient.GetSeparationUsages();

            IEnumerable<string> recipeNos = recipeExecs.Select(x => x.RecipeNo);
            List<Recipe> recipes = await _recipeRepository.GetListAsync(x => recipeNos.Contains(x.RecipeNo));
            IEnumerable<Guid> recipeIds = recipes.Select(x => x.Id);

            List<Prescribe> prescribes = await _prescribeRepository.GetListAsync(x => recipeIds.Contains(x.RecipeId));

            List<PdaRecipeEto> pdaRecipeEtos = new List<PdaRecipeEto>();
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                IEnumerable<Recipe> currentRecipes = recipes.Where(x => x.RecipeNo == recipeExec.RecipeNo);
                foreach (Recipe recipe in currentRecipes)
                {
                    PdaRecipeEto pdaRecipeEto = new PdaRecipeEto();
                    pdaRecipeEto.PatientId = admissionRecordDto.PatientID;
                    pdaRecipeEto.PatientNo = admissionRecordDto.RegisterNo;
                    pdaRecipeEto.PlacerGroupNumber = recipeExec.Id.ToString();
                    pdaRecipeEto.PlacerOrderNumber = recipe.Id.ToString();
                    pdaRecipeEto.ParentGroupNumber = recipe.RecipeNo;
                    pdaRecipeEto.OrderId = recipe.Id.ToString();
                    pdaRecipeEto.AdviceItemCode = recipe.Code;
                    pdaRecipeEto.AdviceItemName = recipe.Name;
                    pdaRecipeEto.AdviceContent = recipe.Name;
                    pdaRecipeEto.DoubleCheckFlag = "0";//等待产品给出哪些需要双核对
                    if (recipe.ItemType == ERecipeItemType.Prescribe)
                    {
                        Prescribe prescribe = prescribes.FirstOrDefault(x => x.RecipeId == recipe.Id);
                        pdaRecipeEto.SkinTestFlag = prescribe.NeedSkinTest() ? 1 : 0;
                        pdaRecipeEto.SkinTestResultCode = string.Empty;
                        if (prescribe.SkinTestResult.HasValue)
                        {
                            pdaRecipeEto.SkinTestResultCode = prescribe.SkinTestResult.Value ? "2" : "1";
                        }
                        if (prescribe.SkinTestSignChoseResult.HasValue && (prescribe.SkinTestSignChoseResult.Value == 0 || prescribe.SkinTestSignChoseResult.Value == 2))
                        {
                            pdaRecipeEto.SkinTestResultDate = recipe.ApplyTime;
                            pdaRecipeEto.SkinTestNurseCode = recipe.ApplyDoctorCode;
                            pdaRecipeEto.SkinTestNurseName = recipe.ApplyDoctorName;
                        }
                        pdaRecipeEto.Frequency = prescribe.FrequencyCode;
                        pdaRecipeEto.DrugRoute = prescribe.UsageCode;
                        pdaRecipeEto.DrugRouteName = prescribe.UsageName;
                        pdaRecipeEto.WayType = ((int)GetWayType(prescribe.UsageCode, separationUsages)).ToString();
                        pdaRecipeEto.ExecType = ((int)GetExecType(prescribe.UsageCode, separationUsages)).ToString();
                        pdaRecipeEto.AdviceType = "1";
                        pdaRecipeEto.Dose = prescribe.DosageQty.ToString("#0.##");
                        pdaRecipeEto.Unit = prescribe.DosageUnit;
                    }

                    if (recipe.ItemType == ERecipeItemType.Pacs)
                    {
                        pdaRecipeEto.WayType = "6";
                        pdaRecipeEto.ExecType = "10";
                        pdaRecipeEto.Frequency = "st";
                        pdaRecipeEto.AdviceType = "3";
                    }

                    if (recipe.ItemType == ERecipeItemType.Lis)
                    {
                        pdaRecipeEto.WayType = "5";
                        pdaRecipeEto.ExecType = "8";
                        pdaRecipeEto.Frequency = "st";
                        pdaRecipeEto.AdviceType = "2";
                    }

                    if (recipe.ItemType == ERecipeItemType.Treat)
                    {
                        pdaRecipeEto.WayType = "5";
                        pdaRecipeEto.ExecType = "9";
                        pdaRecipeEto.Frequency = "st";
                        pdaRecipeEto.AdviceType = "4";
                    }

                    pdaRecipeEto.StartTime = recipe.ApplyTime;
                    pdaRecipeEto.PlanExecTime = recipeExec.PlanExcuteTime;
                    pdaRecipeEto.TimeLimit = recipe.PrescribeTypeCode == "PrescribeTemp" ? "0" : "1";
                    pdaRecipeEto.OrderStatus = orderStatus;

                    pdaRecipeEtos.Add(pdaRecipeEto);
                }
            }

            if (!pdaRecipeEtos.Any()) return;

            PdaBaseEto<List<PdaRecipeEto>> pdaBaseEto = new PdaBaseEto<List<PdaRecipeEto>>()
            {
                Eventcode = "E00001",
                DateTime = DateTime.Now,
                Body = pdaRecipeEtos
            };

            PublishData(pdaBaseEto);
        }

        /// <summary>
        /// 执行状态同步到PDA
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="recipeExecs"></param>
        /// <param name="orderStatus"> 1：待执行 15：待核对 </param>
        /// <returns></returns>
        public async Task ExecuteStatusToPdaAsync(Guid PI_ID, IEnumerable<RecipeExec> recipeExecs, string orderStatus)
        {
            if (!await IsSyncToPdaAsync())
            {
                return;
            }

            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(PI_ID);
            List<PdaExecuteStatusEto> pdaExecuteRecordEtos = new List<PdaExecuteStatusEto>();
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                PdaExecuteStatusEto pdaExecuteRecordEto = new PdaExecuteStatusEto();
                pdaExecuteRecordEto.PatientId = admissionRecordDto.PatientID;
                pdaExecuteRecordEto.PatientNo = admissionRecordDto.RegisterNo;
                pdaExecuteRecordEto.PlacerGroupNumber = recipeExec.Id.ToString();
                pdaExecuteRecordEto.PlacerOrderNumber = string.Empty;
                pdaExecuteRecordEto.OrderStatus = orderStatus;
                pdaExecuteRecordEto.CheckBeforeNurseCode = recipeExec.CheckNurseCode;
                pdaExecuteRecordEto.CheckBeforeNurseName = recipeExec.CheckNurseName;
                pdaExecuteRecordEto.PlanExecTime = recipeExec.PlanExcuteTime;
                pdaExecuteRecordEto.CheckBeforeTime = recipeExec.CheckTime;
                pdaExecuteRecordEtos.Add(pdaExecuteRecordEto);
            }

            PdaBaseEto<List<PdaExecuteStatusEto>> pdaBaseEto = new PdaBaseEto<List<PdaExecuteStatusEto>>()
            {
                Eventcode = "E00003",
                DateTime = DateTime.Now,
                Body = pdaExecuteRecordEtos
            };

            PublishData(pdaBaseEto);
        }

        /// <summary>
        /// 执行单执行记录同步到pda
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="recipeExecs"></param>
        /// <param name="orderStatus">4：已执行 5：拒执行 6：撤销执行 7：已停嘱</param>
        /// <returns></returns>
        public async Task ExecuteRecordToPdaAsync(Guid PI_ID, IEnumerable<RecipeExec> recipeExecs, string orderStatus)
        {
            if (!await IsSyncToPdaAsync())
            {
                return;
            }

            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(PI_ID);
            List<PdaExecuteRecordEto> pdaExecuteRecordEtos = new List<PdaExecuteRecordEto>();
            foreach (RecipeExec recipeExec in recipeExecs)
            {
                PdaExecuteRecordEto pdaExecuteRecordEto = new PdaExecuteRecordEto();
                pdaExecuteRecordEto.PatientId = admissionRecordDto.PatientID;
                pdaExecuteRecordEto.PatientNo = admissionRecordDto.RegisterNo;
                pdaExecuteRecordEto.PlacerGroupNumber = recipeExec.Id.ToString();
                pdaExecuteRecordEto.PlacerOrderNumber = string.Empty;
                pdaExecuteRecordEto.OrderStatus = orderStatus;
                pdaExecuteRecordEto.StartNurseCode = recipeExec.ExecuteNurseCode;
                pdaExecuteRecordEto.StartNurseName = recipeExec.ExecuteNurseName;
                pdaExecuteRecordEto.PlanExecTime = recipeExec.PlanExcuteTime;
                pdaExecuteRecordEto.StartExecTime = recipeExec.ExecuteNurseTime;
                pdaExecuteRecordEtos.Add(pdaExecuteRecordEto);
            }

            PdaBaseEto<List<PdaExecuteRecordEto>> pdaBaseEto = new PdaBaseEto<List<PdaExecuteRecordEto>>()
            {
                Eventcode = "E00002",
                DateTime = DateTime.Now,
                Body = pdaExecuteRecordEtos
            };

            PublishData(pdaBaseEto);
        }

        /// <summary>
        /// 皮试结果同步到pda
        /// </summary>
        /// <param name="pdaSkinResultEtos"></param>
        /// <param name="recipeNo"></param>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        public async Task SkinResultToPdaAsync(IEnumerable<PdaSkinResultEto> pdaSkinResultEtos, string recipeNo, Guid PI_ID)
        {
            if (!await IsSyncToPdaAsync())
            {
                return;
            }

            if (pdaSkinResultEtos == null || !pdaSkinResultEtos.Any())
                return;

            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(PI_ID);
            RecipeExec recipeExecs = await (await _recipeExecRepository.GetQueryableAsync()).Where(x => x.RecipeNo == recipeNo).FirstOrDefaultAsync();

            pdaSkinResultEtos.ForEach(x =>
            {
                x.PatientId = admissionRecordDto.PatientID;
                x.PatientNo = admissionRecordDto.RegisterNo;
                x.ParentGroupNumber = recipeNo;
            });

            PdaBaseEto<List<PdaSkinResultEto>> pdaBaseEto = new PdaBaseEto<List<PdaSkinResultEto>>()
            {
                Eventcode = "E00098",
                DateTime = DateTime.Now,
                Body = pdaSkinResultEtos.ToList()
            };

            PublishData(pdaBaseEto);
        }

        /// <summary>
        /// 是否开启同步到Pda
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsSyncToPdaAsync()
        {
            NursingConfig nursingConfig = await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == ConfigKeyConsts.SYNCTOPDA);
            if (nursingConfig == null)
            {
                return false;
            }

            if (bool.TryParse(nursingConfig.Value, out bool result))
            {
                return result;
            }

            return false;
        }

        /// <summary>
        /// 获取执行类别
        /// </summary>
        /// <param name="UsageCode"></param>
        /// <param name="separationUsagesList"></param>
        /// <returns></returns>
        private EExecType GetExecType(string UsageCode, List<SeparationUsages> separationUsagesList)
        {
            EExecType execType = EExecType.Oral;
            SeparationUsages separationUsages = separationUsagesList.FirstOrDefault(x => x.UsageCode == UsageCode);
            if (separationUsages == null) return execType;

            if (separationUsages.Title == "注射单")
            {
                execType = EExecType.Injection;
            }

            if (separationUsages.Title == "输液单")
            {
                execType = EExecType.Infusion;
            }

            if (separationUsages.Title == "雾化单")
            {
                execType = EExecType.Atomization;
            }

            return execType;
        }

        /// <summary>
        /// 获取用法类型
        /// </summary>
        /// <param name="UsageCode"></param>
        /// <param name="separationUsagesList"></param>
        /// <returns></returns>
        private EWayType GetWayType(string UsageCode, List<SeparationUsages> separationUsagesList)
        {
            EWayType wayType = EWayType.Medicine;
            SeparationUsages separationUsages = separationUsagesList.FirstOrDefault(x => x.UsageCode == UsageCode);
            if (separationUsages == null) return wayType;

            if (separationUsages.Title == "注射单")
            {
                wayType = EWayType.Injection;
            }

            if (separationUsages.Title == "输液单")
            {
                wayType = EWayType.Infusion;
            }

            if (separationUsages.Title == "雾化单")
            {
                wayType = EWayType.Treatment;
            }

            return wayType;
        }

        /// <summary>
        /// 推送数据到mq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        private void PublishData<T>(PdaBaseEto<T> data) where T : class
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = _configuration["RabbitMQ:Connections:Default:HostName"];//RabbitMQ服务器地址
            factory.Port = Convert.ToInt32(_configuration["RabbitMQ:Connections:Default:Port"]);
            factory.UserName = _configuration["RabbitMQ:Connections:Default:UserName"];
            factory.Password = _configuration["RabbitMQ:Connections:Default:Password"];
            factory.ClientProvidedName = _configuration["RabbitMQ:EventBus:ClientName"];
            factory.DispatchConsumersAsync = true;// 设置异步发消息
            string exchangeName = _configuration["RabbitMQ:EventBus:ExchangeName"];//交换机的名字
            string eventName = "CommonEvents";// routingKey的值
            using var conn = factory.CreateConnection();

            using (var channel = conn.CreateModel())//创建信道
            {
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                channel.ExchangeDeclare(exchange: exchangeName, type: "direct", durable: true);//声明交换机
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                string msg = JsonConvert.SerializeObject(data, jsonSerializerSettings);
                _logger.LogInformation($"推送到pda的数据{msg}");
                byte[] body = Encoding.UTF8.GetBytes(msg);
                channel.BasicPublish(exchange: exchangeName, routingKey: eventName,
                    mandatory: true, basicProperties: properties, body: body);//发布消息        
            }
        }
    }
}
