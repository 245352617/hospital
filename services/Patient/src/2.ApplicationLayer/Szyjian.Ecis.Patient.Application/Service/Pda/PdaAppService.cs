using Abp.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描述：同步信息到pda服务
    /// 创建人： yangkai
    /// 创建时间：2022/11/29 13:50:17
    /// </summary>
    [RemoteService(false)]
    public class PdaAppService : EcisPatientAppService
    {
        private readonly IFreeSql _freeSql;
        private IConfiguration _configuration;
        private ILogger<PdaAppService> _logger;

        public PdaAppService(IFreeSql freeSql
            , IConfiguration configuration
            , ILogger<PdaAppService> logger)
        {
            _freeSql = freeSql;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 推送数据到pda
        /// </summary>
        /// <param name="patientEvent"></param>
        /// <param name="admission"></param>
        /// <returns></returns>
        public async Task PatientInfoToPdaAsync(EPatientEventType patientEvent, AdmissionRecord admission)
        {
            if (admission == null) return;
            if (string.IsNullOrEmpty(admission.RegisterNo)) return;

            // 查询诊断编码
            DiagnoseRecord lastDiagnoseRecord = await _freeSql.Select<DiagnoseRecord>()
                .Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && x.PI_ID == admission.PI_ID && !x.IsDeleted)
                .OrderByDescending(x => x.CreationTime)
                .FirstAsync();
            if (lastDiagnoseRecord == null) lastDiagnoseRecord = new DiagnoseRecord();

            Bed bed = await _freeSql.Select<Bed>().Where(x => x.BedName == admission.Bed).FirstAsync();

            PdaPatientEto pdaPatientEto = new PdaPatientEto();
            pdaPatientEto.PatientEvent = patientEvent;
            pdaPatientEto.PatientEventName = patientEvent.GetDescription();
            pdaPatientEto.PatientId = admission.PatientID;
            pdaPatientEto.PatientClass = "E";
            pdaPatientEto.VisitNum = admission.RegisterNo;
            pdaPatientEto.IdCard = string.Empty;
            pdaPatientEto.PatientNo = admission.VisitNo;
            pdaPatientEto.IdentifyNo = admission.IDNo;
            pdaPatientEto.PatientName = admission.PatientName;
            pdaPatientEto.Birthday = admission.Birthday;
            if (admission.Sex == "Sex_Man")
            {
                pdaPatientEto.Sex = PdaSex.MALE;
            }
            else if (admission.Sex == "Sex_Woman")
            {
                pdaPatientEto.Sex = PdaSex.FEMALE;
            }
            else
            {
                pdaPatientEto.Sex = PdaSex.UNKNOWN;
            }
            pdaPatientEto.PhoneNumberHome = admission.ContactsPhone;
            pdaPatientEto.PhoneNumberBus = string.Empty;
            pdaPatientEto.MaritalStatus = string.Empty;
            pdaPatientEto.EthnicGroup = string.Empty;
            pdaPatientEto.Nationality = string.Empty;
            pdaPatientEto.PointCare = admission.AreaCode;
            pdaPatientEto.Room = string.Empty;
            pdaPatientEto.Bed = admission.Bed;
            pdaPatientEto.BedOrderId = bed?.BedOrder ?? 0;
            pdaPatientEto.Org = admission.DeptCode;
            pdaPatientEto.OrgName = admission.TriageDeptName;
            pdaPatientEto.ReAdmissionIndicator = string.Empty;
            pdaPatientEto.HomeAddress = admission.HomeAddress;
            pdaPatientEto.OfficeAddress = string.Empty;
            pdaPatientEto.NationAddress = string.Empty;
            pdaPatientEto.VipIndicator = string.Empty;
            if (string.IsNullOrEmpty(admission.ChargeType) || admission.ChargeType == "Faber_001")
            {
                pdaPatientEto.PatientType = "自费";
            }
            else
            {
                pdaPatientEto.PatientType = "医保";
            }
            pdaPatientEto.NewbornBabyIndicator = string.Empty;
            pdaPatientEto.ProductionClassCode = admission.ChargeTypeName;
            pdaPatientEto.DoctorId = admission.DutyDoctorCode;
            pdaPatientEto.DoctorName = admission.DutyDoctorName;
            pdaPatientEto.NurseId = admission.DutyNurseCode;
            pdaPatientEto.NurseName = admission.DutyNurseName;
            pdaPatientEto.DiagnosisDescription = lastDiagnoseRecord.DiagnoseName;
            pdaPatientEto.DeathDate = admission.DeathTime;
            pdaPatientEto.DeathIndicator = string.Empty;
            pdaPatientEto.AdmitDateTime = admission.InDeptTime;
            pdaPatientEto.DischargeTime = admission.OutDeptTime;
            pdaPatientEto.TriageLevelStr = admission.TriageLevelName;
            pdaPatientEto.GreenPassFlag = string.IsNullOrEmpty(admission.GreenRoadCode) ? 0 : 1;
            pdaPatientEto.HeadboardSticker = GetBedHeadSticker(admission.BedHeadSticker);
            if (patientEvent == EPatientEventType.InDept || patientEvent == EPatientEventType.TransferDept)
            {
                pdaPatientEto.IndwellingBaseTime = DateTime.Now;
            }

            PdaBaseEto<PdaPatientEto> pdaBaseEto = new PdaBaseEto<PdaPatientEto>()
            {
                Eventcode = "E00000",
                DateTime = DateTime.Now,
                Body = pdaPatientEto
            };

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            _logger.LogInformation($"推送给PDA的数据{pdaBaseEto.ToJsonString(jsonSerializerSettings)}");
            PublishData(pdaBaseEto);
        }

        private string GetBedHeadSticker(string bedHeadSticker)
        {
            if (string.IsNullOrEmpty(bedHeadSticker)) return string.Empty;

            string[] bedHeadStickers = bedHeadSticker.Split(',', StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            foreach (string item in bedHeadStickers)
            {
                switch (item.ToLower())
                {
                    case "preventionoffalls": result.Add("跌"); break;
                    case "pressureproofsore": result.Add("压"); break;
                    case "fallproofwear": result.Add("坠"); break;
                    case "criticallyill": result.Add("危"); break;
                    case "wasseriouslyill": result.Add("重"); break;
                    default: break;
                }
            }

            return string.Join(",", result);
        }

        private void PublishData(PdaBaseEto<PdaPatientEto> data)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = _configuration["RabbitMq:Connection:HostName"];//RabbitMQ服务器地址
            factory.Port = Convert.ToInt32(_configuration["RabbitMq:Connection:Port"]);
            factory.UserName = _configuration["RabbitMq:Connection:UserName"];
            factory.Password = _configuration["RabbitMq:Connection:Password"];
            factory.VirtualHost = _configuration["RabbitMq:CAP:VirtualHost"];
            factory.ClientProvidedName = _configuration["RabbitMq:EventBus:ClientName"];
            factory.DispatchConsumersAsync = true;// 设置异步发消息
            string exchangeName = _configuration["RabbitMq:EventBus:ExchangeName"];//交换机的名字
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

                string msg = data.ToJsonString(jsonSerializerSettings);
                byte[] body = Encoding.UTF8.GetBytes(msg);
                channel.BasicPublish(exchange: exchangeName, routingKey: eventName,
                    mandatory: true, basicProperties: properties, body: body);//发布消息        
            }
        }
    }
}
