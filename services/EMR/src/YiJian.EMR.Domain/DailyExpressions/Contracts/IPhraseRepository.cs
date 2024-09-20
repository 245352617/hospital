using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.DailyExpressions.Entities;
using YiJian.EMR.DataBinds.Entities;

namespace YiJian.EMR.DailyExpressions.Contracts
{
    /// <summary>
    /// 病历常用语
    /// </summary>
    public interface IPhraseRepository : IRepository<Phrase, int>
    {

        /// <summary>
        /// 添加常用词
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<Phrase> CreateAsync(Phrase entity);

        /// <summary>
        /// 更新常用词
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<Phrase> ModifyAsync(Phrase entity);

        /// <summary>
        /// 删除常用词
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns> 
        public Task DeleteAsync(int[] ids);

        /// <summary>
        /// 检测同一个目录下是否有重复的标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="catalogueId"></param>
        /// <returns></returns>
        public Task<bool> CheckTitleAsync(string title, int catalogueId);

        /// <summary>
        /// 检测所有的常用词的内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns> 
        public Task<List<PhraseCatalogue>> CheckPhrasesAsync(int[] ids);

    }
}
