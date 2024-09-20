using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:模块参数
    /// </summary>
    public interface IIcuParaModuleRepository : IRepository<IcuParaModule, Guid>, IBaseRepository<IcuParaModule, Guid>
    {
        /// <summary>
        /// 通过科室编号+模块名称获取对应的模块编号
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleName"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public Task<string> GetModuleCodeAsync(string deptCode, string moduleName, string moduleType);

        /// <summary>
        /// 根据科室、模块类型获取模块列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task<List<IcuParaModule>> GetIcuParaModuleByType(string deptCode, string moduleType);

        /// <summary>
        /// 获取观察项、出入量模块列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        Task<List<IcuParaModule>> GetIcuParaModuleByVsIo(string deptCode);
    }
}
