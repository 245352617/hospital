using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.Domain.PhraseCatalogues.Contracts;
using YiJian.Health.Report.Domain.PhraseCatalogues.Entities;
using YiJian.Health.Report.EntityFrameworkCore;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 常用语
    /// </summary>
    public class PhraseRepository : EfCoreRepository<ReportDbContext, Phrase, int>, IPhraseRepository
    {
        public PhraseRepository(IDbContextProvider<ReportDbContext> dbContextProvider) 
            : base(dbContextProvider)
        { 
        
        }

        /// <summary>
        /// 添加常用词
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Phrase> CreateAsync(Phrase entity)
        {
            var db = await GetDbContextAsync();
            var ret = await db.Phrases.AddAsync(entity);
            return ret.Entity;
        }

        /// <summary>
        /// 更新常用词
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Phrase> ModifyAsync(Phrase entity)
        {
            var db = await GetDbContextAsync();
            var ret = db.Phrases.Update(entity);
            return ret.Entity;
        }

        /// <summary>
        /// 删除常用词
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns> 
        public async Task DeleteAsync(int[] ids)
        {
            var db = await GetDbContextAsync();
            var entities = await db.Phrases.Where(w => ids.Contains(w.Id)).ToListAsync();
            db.Phrases.RemoveRange(entities);
        }

        /// <summary>
        /// 检测同一个目录下是否有重复的标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="catalogueId"></param>
        /// <returns></returns>
        public async Task<bool> CheckTitleAsync(string title, int catalogueId)
        {
            var db = await GetDbContextAsync();
            return await db.Phrases.AnyAsync(w => w.Title.Trim() == title.Trim() && w.CatalogueId == catalogueId);
        }

        /// <summary>
        /// 检测所有的常用词的内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns> 
        public async Task<List<PhraseCatalogue>> CheckPhrasesAsync(int[] ids)
        {
            var db = await GetDbContextAsync();
            var query = from p in db.Phrases.Where(w => ids.Contains(w.Id))
                        join c in db.PhraseCatalogues
                        on p.CatalogueId equals c.Id
                        select c;
            return await query.ToListAsync();  
        }

    }
}
