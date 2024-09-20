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
    /// 通用字符
    /// </summary>
    public interface IUniversalCharacterRepository : IRepository<UniversalCharacter, int>
    {
        /// <summary>
        /// 获取所有通用字符
        /// </summary>
        /// <returns></returns> 
        public Task<List<UniversalCharacter>> GetAllAsync();

        /// <summary>
        /// 获取所有通用字符分类，不包含内容
        /// </summary>
        /// <returns></returns> 
        public Task<List<UniversalCharacter>> GetCategoriesAsync();

        /// <summary>
        /// 获取指定目录下的所有字符内容
        /// </summary>
        /// <returns></returns> 
        public Task<UniversalCharacter> GetNodesByIdAsync(int id);

        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> CreateAsync(UniversalCharacter model);

        /// <summary>
        /// 更新分类内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> UpdateAsync(UniversalCharacter model);

        /// <summary>
        /// 移除分类内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> RemoveAsync(int id);
    }
}
