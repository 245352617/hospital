using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 字典-基础字典设置表 仓储接口
    /// </summary>
    public interface IDictSourceRepository : IRepository<DictSource, Guid>, IBaseRepository<DictSource, Guid>
    {
        #region 定义接口

        /// <summary>
        /// 根据分类获取配置
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<List<DictSource>> GetDictSourceByModuleCode(string moduleCode);

        /// <summary>
        /// 根据科室、分类、参数名称获取配置
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="paraName"></param>
        /// <returns></returns>
        Task<List<DictSource>> GetSourceByModuleAndName(string deptCode, string moduleCode, string paraName);

        /// <summary>
        /// 根据科室、分类、参数名称集合获取配置
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="paraNames"></param>
        /// <returns></returns>
        Task<List<DictSource>> GetSourceByModuleAndNames(string deptCode, string moduleCode, List<string> paraNames);

        /// <summary>
        /// 创建数据字典
        /// </summary>
        /// <param name="dictSources"></param>
        /// <returns></returns>
        Task<bool> AddRangeAsync(List<DictSource> dictSources);
        #endregion
    }
}
