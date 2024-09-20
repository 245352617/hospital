using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:字典-通用业务
    /// </summary>
    public interface IDictRepository : IRepository<Dict, Guid>, IBaseRepository<Dict, Guid>
    {
        /// <summary>
        /// 根据科室和模块合集获取参数字典
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        Task<List<DictDto>> GetIcuDicts(string deptCode, List<string> modules);

        /// <summary>
        /// 根据科室和模块类型获取参数字典
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task<List<DictDto>> GetDictByModuleType(string deptCode, string moduleType);

        /// <summary>
        /// 根据科室、参数代码、模块类型获取参数字典
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task<List<Dict>> GetDictByModuleParaAndType(string deptCode, string paraCode, string moduleType);
    }
}
