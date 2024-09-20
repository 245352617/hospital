using BeetleX.Http.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.Health.Report.Hospitals.Dto;

namespace YiJian.Health.Report.Hospitals
{
    /// <summary>
    /// 医院系统请求客户端(龙岗中心医院)
    /// </summary>
    [JsonFormater]
    public interface IHospitalProxyService
    {

        /// <summary>
        /// 查询云签
        /// </summary>
        /// <param name="relBizNo"></param>
        /// <returns></returns>
        [Post(Route = "v1.0/cloudsign/getstamp")]
        public Task<StampResponseDto> QueryStampBaseAsync(Dictionary<string, string> relBizNo);
    }
}
