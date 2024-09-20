using Abp.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描    述:危急值服务
    /// 创 建 人:杨凯
    /// 创建时间:2023/9/26 16:07:47
    /// </summary>
    [Authorize]
    public class CrisisAppService : EcisPatientAppService, ICrisisAppService
    {
        private readonly IFreeSql _freeSql;
        private IConfiguration _configuration;
        private ILogger<CrisisAppService> _logger;

        public CrisisAppService(IFreeSql freeSql, IConfiguration configuration, ILogger<CrisisAppService> logger)
        {
            _freeSql = freeSql;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取危急值列表
        /// </summary>
        /// <param name="queryCrisisDto"></param>
        /// <returns></returns>
        public ResponseResult<PagedResultDto<CrisisDto>> GetList(QueryCrisisDto queryCrisisDto)
        {
            if (queryCrisisDto == null)
            {
                return RespUtil.Error(data: new PagedResultDto<CrisisDto>(), msg: "请求参数为空");
            }

            PagedResultDto<CrisisDto> crisisDtos = QueryCrisisDto(queryCrisisDto);
            return RespUtil.Ok(data: crisisDtos);
        }

        /// <summary>
        /// 获取患者危急值列表
        /// </summary>
        /// <param name="pi_id"></param>
        /// <returns></returns>
        public ResponseResult<PagedResultDto<CrisisDto>> GetPatientList(QueryCrisisDto queryCrisisDto)
        {
            if (queryCrisisDto == null || queryCrisisDto.PI_ID == Guid.Empty)
            {
                return RespUtil.Error(data: new PagedResultDto<CrisisDto>(), msg: "请求参数为空");
            }

            PagedResultDto<CrisisDto> crisisDtos = QueryCrisisDto(queryCrisisDto);
            return RespUtil.Ok(data: crisisDtos);
        }

        /// <summary>
        /// 更新危急值信息
        /// </summary>
        /// <param name="queryCrisisDto"></param>
        /// <returns></returns>
        public ResponseResult<bool> UpdateCrisis(CrisisDto crisisDto)
        {
            if (crisisDto == null)
            {
                return RespUtil.Error(data: false, msg: "请求参数为空");
            }

            _freeSql.Update<Crisis>()
                .Where(x => x.Id == crisisDto.Id)
                .SetIf(!string.IsNullOrEmpty(crisisDto.HandleCode), x => x.HandleCode, crisisDto.HandleCode)
                .SetIf(!string.IsNullOrEmpty(crisisDto.HandleName), x => x.HandleName, crisisDto.HandleName)
                .SetIf(!string.IsNullOrEmpty(crisisDto.NursingCode), x => x.NursingCode, crisisDto.NursingCode)
                .SetIf(!string.IsNullOrEmpty(crisisDto.NursingName), x => x.NursingName, crisisDto.NursingName)
                .SetIf(crisisDto.NursingReceiveTime.HasValue, x => x.NursingReceiveTime, crisisDto.NursingReceiveTime)
                .SetIf(!string.IsNullOrEmpty(crisisDto.DoctorCode), x => x.DoctorCode, crisisDto.DoctorCode)
                .SetIf(!string.IsNullOrEmpty(crisisDto.DoctorName), x => x.DoctorName, crisisDto.DoctorName)
                .SetIf(crisisDto.DoctorReceiveTime.HasValue, x => x.DoctorReceiveTime, crisisDto.DoctorReceiveTime)
                .Set(x => x.IsHandle, true)
                .ExecuteAffrows();

            Crisis crisis = _freeSql.Select<Crisis>().Where(x => x.Id == crisisDto.Id).First();
            PublishData(crisis);
            return RespUtil.Ok(data: true);
        }

        /// <summary>
        /// 查询危急值列表
        /// </summary>
        /// <param name="queryCrisisDto"></param>
        /// <returns></returns>
        private PagedResultDto<CrisisDto> QueryCrisisDto(QueryCrisisDto queryCrisisDto)
        {
            List<CrisisDto> crisisDtos = _freeSql.Select<AdmissionRecord, Crisis>().InnerJoin((a, b) => a.PI_ID == b.PI_ID)
                 .WhereIf(queryCrisisDto.StartDate.HasValue, (a, b) => b.ReporterTime > queryCrisisDto.StartDate.Value)
                 .WhereIf(queryCrisisDto.EndDate.HasValue, (a, b) => b.ReporterTime <= queryCrisisDto.EndDate.Value)
                 .WhereIf(!string.IsNullOrEmpty(queryCrisisDto.QueryParam), (a, b) => a.PatientName.Contains(queryCrisisDto.QueryParam))
                 .WhereIf(queryCrisisDto.IsHandle.HasValue, (a, b) => b.IsHandle == queryCrisisDto.IsHandle.Value)
                 .WhereIf(!string.IsNullOrEmpty(queryCrisisDto.HandleCode), (a, b) => b.HandleCode == queryCrisisDto.HandleCode)
                 .WhereIf(queryCrisisDto.PI_ID.HasValue, (a, b) => b.PI_ID == queryCrisisDto.PI_ID.Value)
                 .Count(out long totalCount)
                 .Page(queryCrisisDto.Index, queryCrisisDto.PageSize)
                 .OrderByDescending((a, b) => b.ReporterTime)
                 .ToList<CrisisDto>((a, b) => new CrisisDto()
                 {
                     Id = b.Id,
                     HandleCode = b.HandleCode,
                     HandleName = b.HandleName,
                     PI_ID = b.PI_ID,
                     PatientName = a.PatientName,
                     SexName = a.SexName,
                     Age = a.Age,
                     DeptName = a.DeptName,
                     BedNo = a.Bed,
                     MedicalRecordNo = b.MedicalRecordNo,
                     Phone = a.ContactsPhone,
                     ApplyDoctorCode = b.ApplyDoctorCode,
                     ApplyDoctorName = b.ApplyDoctorName,
                     CrisisName = b.CrisisName,
                     CrisisValue = b.CrisisValue,
                     ReporterCode = b.ReporterCode,
                     ReporterName = b.ReporterName,
                     ReporterTime = b.ReporterTime,
                     NursingCode = b.NursingCode,
                     NursingName = b.NursingName,
                     NursingReceiveTime = b.NursingReceiveTime,
                     DoctorCode = b.DoctorCode,
                     DoctorName = b.DoctorName,
                     DoctorReceiveTime = b.DoctorReceiveTime,
                     IsHandle = b.IsHandle,
                     SampleNo = b.SampleNo,
                     CrisisDetails = b.CrisisDetails
                 });

            PagedResultDto<CrisisDto> res = new PagedResultDto<CrisisDto>
            {
                TotalCount = totalCount,
                Items = crisisDtos
            };

            return res;
        }

        private void PublishData(Crisis data)
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
            string eventName = "CrisisBack";// routingKey的值
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
                _logger.LogInformation($"推送给DDP的危急值结果:{msg}");
                byte[] body = Encoding.UTF8.GetBytes(msg);
                channel.BasicPublish(exchange: exchangeName, routingKey: eventName,
                    mandatory: true, basicProperties: properties, body: body);//发布消息        
            }
        }
    }
}
