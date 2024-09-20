using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Domain.PhraseCatalogues.Entities;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.Domain.PhraseCatalogues.Contracts
{
    /// <summary>
    /// 常用语目录
    /// </summary>
    public interface IPhraseCatalogueRepository : IRepository<PhraseCatalogue, int>
    {
        /// <summary>
        /// 获取所有分类下的目录短语集合
        /// </summary>
        /// <param name="templateType"></param>
        /// <param name="belonger"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public Task<List<PhraseCatalogue>> GetAllAsync(ETemplateType templateType, string belonger, string searchText);

        /// <summary>
        /// 获取常用语目录
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public Task<Dictionary<ETemplateType, List<PhraseCatalogue>>> CatalogueMapAsync(string deptId, string doctorId);

        /// <summary>
        /// 获取制定目录下的所有常用词记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<PhraseCatalogue> GetPhraseListByCatalogueAsync(int id);

        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<PhraseCatalogue> CreateAsync(PhraseCatalogue entity);

        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<PhraseCatalogue> ModifyAsync(PhraseCatalogue entity);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task DeleteAsync(int[] ids);

        /// <summary>
        /// 判断该标题是否存在
        /// </summary>
        /// <param name="title"></param>
        /// <param name="templateType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> CheckTitleAsync(string title, ETemplateType templateType, Guid userId);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<PhraseCatalogue>> GetListAsync(int[] ids);

    }
}
