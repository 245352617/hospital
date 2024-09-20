using System.Collections.Generic;
using Volo.Abp.Application.Services;

namespace YiJian.Recipe.Platform
{
    /// <summary>
    /// 平台相关的配置、接口
    /// Author: ywlin
    /// Date: 2021-11-23
    /// </summary>
    public interface IPlatformAppService : IApplicationService
    {
        /// <summary>
        /// 获取急诊科室编码
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetEcisDepartmentCodes();
    }
}
