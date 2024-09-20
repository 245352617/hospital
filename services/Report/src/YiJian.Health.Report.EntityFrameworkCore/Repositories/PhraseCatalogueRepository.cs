using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore; 
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;
using YiJian.Health.Report.Domain.PhraseCatalogues.Contracts;
using YiJian.Health.Report.Domain.PhraseCatalogues.Entities;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 常用语目录
    /// </summary>
    public class PhraseCatalogueRepository : EfCoreRepository<ReportDbContext, PhraseCatalogue, int>, IPhraseCatalogueRepository
    {
        /// <summary>
        /// 常用语目录
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PhraseCatalogueRepository(IDbContextProvider<ReportDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取所有分类下的目录短语集合
        /// </summary>
        /// <param name="templateType"></param>
        /// <param name="belonger"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<List<PhraseCatalogue>> GetAllAsync(ETemplateType templateType, string belonger, string searchText)
        {
            var db = await GetDbContextAsync(); 
            var phraseCatalogue = await db.PhraseCatalogues
                .AsNoTracking()
                .Where(w=>w.TemplateType== templateType && w.Belonger == belonger)
                .ToListAsync();

            if (!phraseCatalogue.Any()) return new List<PhraseCatalogue>();

            var ids = phraseCatalogue.Select(s=>s.Id).ToList();

            var phrasesList = await db.Phrases
                .Where(w=> ids.Contains(w.CatalogueId))
                .WhereIf(!searchText.IsNullOrEmpty(),w=>w.Title.Contains(searchText))
                .ToListAsync();

            foreach (var item in phraseCatalogue)
            { 
                item.Phrases = phrasesList.Where(w => w.CatalogueId == item.Id).OrderBy(o=>o.Sort).ThenBy(o=>o.Id).ToList();
            }  

            return phraseCatalogue;
        }

        /// <summary>
        /// 获取常用语目录
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="doctorId"></param> 
        /// <returns></returns>
        public async Task<Dictionary<ETemplateType, List<PhraseCatalogue>>> CatalogueMapAsync(string deptId, string doctorId)
        { 
            Dictionary<ETemplateType, List<PhraseCatalogue>> dic = new(); 
            var db = await GetDbContextAsync();

            //通用的，全院的
            var general = await db.PhraseCatalogues
                .AsNoTracking()
                .Where(w => w.TemplateType == ETemplateType.General) 
                .OrderBy(o=>o.Sort)
                .ThenBy(o=>o.Id)
                .ToListAsync();
            var department = await db.PhraseCatalogues
                .AsNoTracking()
                .Where(w => w.TemplateType == ETemplateType.Department)
                .WhereIf(!deptId.IsNullOrEmpty(),w=>w.Belonger== deptId)
                .OrderBy(o=>o.Sort)
                .ThenBy(o=>o.Id)
                .ToListAsync();
            var personal = await db.PhraseCatalogues
                .AsNoTracking()
                .OrderBy(o=>o.Sort)
                .ThenBy(o=>o.Id)
                .Where(w => w.TemplateType == ETemplateType.Personal)
                .WhereIf(!doctorId.IsNullOrEmpty(),w=>w.Belonger== doctorId)
                .ToListAsync();

            dic.Add(ETemplateType.General,general);
            dic.Add(ETemplateType.Department,department);
            dic.Add(ETemplateType.Personal,personal);

            return dic;
        }

        /// <summary>
        /// 获取制定目录下的所有常用词记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PhraseCatalogue> GetPhraseListByCatalogueAsync(int id)
        {
            var db = await GetDbContextAsync();

            var  ret = await db.PhraseCatalogues
                .Where(w=>w.Id== id)
                .Include(i=>i.Phrases.OrderBy(o=>o.Sort).ThenBy(o=>o.Id))
                .OrderBy(o=>o.Sort)
                .ThenBy(o=>o.Id)
                .FirstOrDefaultAsync(); 
            return ret; 
        }

        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<PhraseCatalogue> CreateAsync(PhraseCatalogue entity)
        {
            var db = await GetDbContextAsync();
            var ret = await db.PhraseCatalogues.AddAsync(entity);
            return ret.Entity;
        }


        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<PhraseCatalogue> ModifyAsync(PhraseCatalogue entity)
        {
            var db = await GetDbContextAsync();
            var ret = db.PhraseCatalogues.Update(entity);
            return ret.Entity; 
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int[] ids)
        {
            var db = await GetDbContextAsync();
            var entities = await db.PhraseCatalogues.Where(w=>ids.Contains(w.Id)).ToListAsync();
            db.PhraseCatalogues.RemoveRange(entities);
        }

        /// <summary>
        /// 判断该标题是否存在
        /// </summary>
        /// <param name="title"></param>
        /// <param name="templateType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> CheckTitleAsync(string title,ETemplateType templateType,Guid userId)
        { 
            var db = await GetDbContextAsync();
            return await db.PhraseCatalogues.AnyAsync(w => w.Title.Trim() == title.Trim() && w.TemplateType == templateType && w.CreatorId == userId);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<PhraseCatalogue>> GetListAsync(int[] ids)
        {
            var db = await GetDbContextAsync();
            var entities = await db.PhraseCatalogues.Where(w => ids.Contains(w.Id)).ToListAsync();
            return entities;
        }

    }
}
