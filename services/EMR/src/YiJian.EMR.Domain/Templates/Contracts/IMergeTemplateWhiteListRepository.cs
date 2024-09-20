using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Templates.Contracts
{
    /// <summary>
    /// 合并模板白名单
    /// </summary>
    public interface IMergeTemplateWhiteListRepository : IRepository<MergeTemplateWhiteList, int>
    {
        /// <summary>
        /// 获取所有的白名单列表内容
        /// </summary>
        /// <returns></returns>
        public Task<IList<MergeTemplateWhiteList>> GetListAsync();

        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddAsync(MergeTemplateWhiteList model);

        /// <summary>
        /// 根据白名单id移除白名单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> RemoveAsync(int id);
    }
}
