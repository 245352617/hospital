using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Application.Services;
using YiJian.Recipe.Platform;
using YiJian.Recipe.PlatformDepartments;

namespace YiJian.Recipe.Platforms
{
    /// <summary>
    /// 平台相关接口
    /// Author: ywlin
    /// Date: 2021-11-23
    /// </summary>
    public class PlatformAppService : ApplicationService, IPlatformAppService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 平台相关接口
        /// </summary>
        /// <param name="configuration"></param>
        public PlatformAppService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// 获取急诊科室编码
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetEcisDepartmentCodes()
        {
            List<PlatformDepartment> departments = new();
            this._configuration.GetSection("PlatformDepartments").Bind(departments);

            return departments.Select(x => x.Code);
        }
    }
}
