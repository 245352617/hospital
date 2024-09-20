using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 字典-基础字典设置表 仓储
    /// </summary>
    public class DictSourceRepository : BaseRepository<EntityFrameworkCore.DbContext, DictSource, Guid>, IDictSourceRepository
    {
        public DictSourceRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 根据分类获取配置
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public async Task<List<DictSource>> GetDictSourceByModuleCode(string moduleCode)
        {
            try
            {
                List<DictSource> dictSources = await DbContext.DictSource.Where(x => x.ModuleCode == "Threetube_Inspect").ToListAsync();

                return dictSources;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 根据科室、分类、参数名称获取配置
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="paraName"></param>
        /// <returns></returns>
        public async Task<List<DictSource>> GetSourceByModuleAndName(string deptCode, string moduleCode, string paraName)
        {
            try
            {
                List<DictSource> dictSources = await (from a in DbContext.DictSource.Where(x => x.DeptCode == deptCode && x.ModuleCode == moduleCode && x.ParaName == paraName)
                                                      join b in DbContext.DictSource on a.Id equals b.Pid
                                                      select b).ToListAsync();

                return dictSources;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 根据科室、分类、参数名称集合获取配置
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="paraNames"></param>
        /// <returns></returns>
        public async Task<List<DictSource>> GetSourceByModuleAndNames(string deptCode, string moduleCode, List<string> paraNames)
        {
            try
            {
                List<DictSource> dictSources = await (from a in DbContext.DictSource.Where(x => x.DeptCode == deptCode && x.ModuleCode == moduleCode && paraNames.Contains(x.ParaName))
                                                      join b in DbContext.DictSource on a.Id equals b.Pid
                                                      select new DictSource { ModuleName = a.ParaName, ParaCode = b.ParaCode }).ToListAsync();

                return dictSources;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 创建数据字典
        /// </summary>
        /// <param name="dictSources"></param>
        /// <returns></returns>
        public async Task<bool> AddRangeAsync(List<DictSource> dictSources)
        {
            try
            {
                await DbContext.AddRangeAsync(dictSources);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
