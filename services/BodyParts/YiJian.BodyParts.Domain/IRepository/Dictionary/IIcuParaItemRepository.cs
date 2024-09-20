using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:护理项目表
    /// </summary>
    public interface IIcuParaItemRepository : IRepository<IcuParaItem, Guid>, IBaseRepository<IcuParaItem, Guid>
    {
        /// <summary>
        /// 查询出量参数代码
        /// </summary>
        /// <param name="paraname"></param>
        /// <returns></returns>
        Task<string> GetParaCode(string paraname);

        /// <summary>
        /// 根据科室和模块合集获取参数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        Task<List<IcuParaItem>> GetIcuParaItems(string deptCode, List<string> modules);

        /// <summary>
        /// 根据科室和模块获取参数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<List<IcuParaItem>> GetParaItemByModuleCode(string deptCode, string moduleCode);

        /// <summary>
        /// 根据模块类型获取参数列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task<List<IcuParaItem>> GetItemByModuleType(string deptCode, string moduleType);

        /// <summary>
        /// 根据模块名称获取趋势图列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        Task<List<IcuParaItem>> GetChartItemByDeptCode(string deptCode);

        /// <summary>
        /// 根据模块类型获取所有科室参数列表
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task<List<IcuParaItem>> GetDeptItemByType(string moduleType);

        /// <summary>
        /// 通过科室编码+类型名称获取所有参数编码集合（自动包含系统项目）
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<List<IcuParaItem>> GetParaCodeListByDeptCodeAndModuleName(string deptCode, string moduleName);

        /// <summary>
        /// 通过科室编码+类型名称+参数名称获取对应参数编码（自动包含系统项目）
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleName"></param>
        /// <param name="paraName"></param>
        /// <returns></returns>
        Task<List<IcuParaItem>> GetParaCOdeByDeptCodeAndModuleNameAndParaName(string deptCode, string moduleName, string paraName);
    }
}
