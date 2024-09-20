using BeetleX.Http.Clients;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Models.Responses;
using YiJian.EMR.HospitalClients.Dto;

namespace YiJian.EMR.HttpClients
{
    /// <summary>
    /// 医院系统请求客户端(龙岗中心医院)
    /// </summary>
    [JsonFormater]
    public interface IHospitalProxyService
    {
        /// <summary>
        /// 诊断、就诊记录、医嘱状态变更
        /// </summary>
        /// <param name="model">医嘱变更的状态集合</param> 
        /// <returns></returns> 
        [Post(Route = "api/ecis/uploadMedicalRecord")]
        public Task<CommonResult<string>> UploadMedicalRecordAsync(UploadMedicalRecordDto model);



    }
}
