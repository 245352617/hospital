using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Health.Report.Hospitals
{
    /// <summary>
    /// 医院接口客户端
    /// </summary>
    public interface IHospitalClientAppService : IApplicationService
    {
        /// <summary>
        /// 查询云签
        /// </summary>
        /// <param name="relBizNo"></param>
        /// <returns></returns>   
        public Task<string> QueryStampBaseAsync(string relBizNo);
    }
}