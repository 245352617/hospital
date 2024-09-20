using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 医院his对接
    /// </summary>
    public class HisClientAppService : EcisPatientAppService, IHisClientAppService
    {
        private readonly ILogger<HisClientAppService> _log;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 医院his对接
        /// </summary>
        /// <param name="log"></param>
        /// <param name="remoteServices"></param>
        /// <param name="freeSql"></param>
        public HisClientAppService(ILogger<HisClientAppService> log, IHttpClientFactory httpClientFactory)
        {
            _log = log;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 调用接口获取患者诊断
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetDiagnoseRecordBySocketDto>> GetPatientDiagnoseByIdAsync(string patientId)
        {
            try
            {
                List<GetDiagnoseRecordBySocketDto> diagnoseRecordList = new List<GetDiagnoseRecordBySocketDto>();
                using (HttpClient client = _httpClientFactory.CreateClient(BaseAddress.HIS))
                {
                    string uri = $"socket/diagnosis/outpatientDiagnosis?patientId={patientId}";
                    HttpResponseMessage recipeExecResponse = await client.GetAsync(uri);
                    if (recipeExecResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        _log.LogError("获取his诊断数据失败");
                        return diagnoseRecordList;
                    }

                    string xmlString = await recipeExecResponse.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(xmlString))
                    {
                        string message = XmlUtil.ReplaceLowOrderASCIICharacters(xmlString);
                        BSXml bsxml = (BSXml)XmlUtil.Deserialize(typeof(BSXml), message);

                        NewDataSet newDataSet = bsxml.data.newDataSet;
                        if (newDataSet != null)
                        {
                            diagnoseRecordList = bsxml.data.newDataSet.diagnoseRecordList;
                        }

                    }
                    _log.LogInformation($"socket/diagnosis/outpatientDiagnosis 调用接口获取患者诊断成功");
                    return diagnoseRecordList;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 调用接口获取患者就诊号
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="registerNo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetPatientRegisterInfoByIdAsync(string patientId, string registerNo)
        {
            try
            {
                using (HttpClient client = _httpClientFactory.CreateClient(BaseAddress.HIS))
                {
                    string uri = $"socket/patientInfo/queryRegisterInfo?clinicId={patientId}&outRegistryId={registerNo}";
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        _log.LogError("获取患者就诊号数据失败");
                        return string.Empty;
                    }

                    string xmlString = await response.Content.ReadAsStringAsync();
                    List<PatientRegisterInfo> registerInfo = Deserialize<PatientRegisterInfo>(xmlString, "BSXml//data//NewDataSet//V_PLATFORM_OutPatientRegisteredQuery");
                    if (registerInfo != null && registerInfo.Count > 0)
                    {
                        return registerInfo.FirstOrDefault()?.VisitId;
                    }
                    _log.LogInformation($"socket/patientInfo/queryRegisterInfo 未获取患者就诊号");
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private static List<T> Deserialize<T>(string xml, string root = "data") where T : new()
        {
            List<T> list = new List<T>();
            XmlDocument doc = new XmlDocument();
            PropertyInfo[] propinfos = null;
            doc.LoadXml(xml);
            XmlNodeList nodeList = doc.SelectNodes(root);

            foreach (XmlNode node in nodeList)
            {
                T entity = new T();
                //初始化PropertyInfo
                if (propinfos == null)
                {
                    Type bjtype = entity.GetType();
                    propinfos = bjtype.GetProperties();
                }
                //填充Entity类的属性
                foreach (PropertyInfo propertyInfo in propinfos)
                {
                    XmlNode cnode = node.SelectSingleNode(propertyInfo.Name);
                    if (cnode == null) continue;
                    string v = cnode.InnerText;
                    if (v != null)
                        propertyInfo.SetValue(entity, Convert.ChangeType(v, propertyInfo.PropertyType), null);
                }
                list.Add(entity);
            }

            return list;
        }
    }
}