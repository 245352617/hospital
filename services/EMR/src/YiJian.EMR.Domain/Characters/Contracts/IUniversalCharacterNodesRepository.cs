using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Characters.Entities;
using YiJian.EMR.DailyExpressions.Entities;

namespace YiJian.EMR.Characters.Contracts
{
    /// <summary>
    /// 通用字符节点内容
    /// </summary>
    public interface IUniversalCharacterNodesRepository : IRepository<UniversalCharacterNode, int>
    { 
        /// <summary>
        /// 添加通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<int> CreateAsync(UniversalCharacterNode model);

        /// <summary>
        /// 更新通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> UpdateAsync(UniversalCharacterNode model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> RemoveAsync(int id);

    }
}
