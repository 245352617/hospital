using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:护理项目表
    /// </summary>
    public class IcuParaItemRepository : BaseRepository<EntityFrameworkCore.DbContext, IcuParaItem, Guid>, IIcuParaItemRepository
    {
        public IcuParaItemRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 查询出量参数代码
        /// </summary>
        /// <param name="paraname"></param>
        /// <returns></returns>
        public async Task<string> GetParaCode(string paraname)
        {
            string paracode = await DbContext.IcuParaItem.Where(x => x.DeptCode == "system" && x.ParaName == paraname).Select(x => x.ParaCode).FirstOrDefaultAsync();
            return paracode;
        }

        /// <summary>
        /// 根据科室和模块合集获取参数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public async Task<List<IcuParaItem>> GetIcuParaItems(string deptCode, List<string> modules)
        {
            try
            {
                List<IcuParaItem> depItems = await DbContext.IcuParaItem.Where(x => x.DeptCode == deptCode && x.ValidState == 1 && modules.Contains(x.ModuleCode)).ToListAsync();
                List<IcuParaItem> sysItems = await DbContext.IcuParaItem.Where(x => x.DeptCode == "system" && x.ValidState == 1).ToListAsync();
                depItems = depItems.Union(sysItems).ToList();
                depItems = depItems.Where((x, i) => depItems.FindIndex(z => z.ParaCode == x.ParaCode) == i).ToList();

                return depItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据科室和模块获取参数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public async Task<List<IcuParaItem>> GetParaItemByModuleCode(string deptCode, string moduleCode)
        {
            try
            {
                List<IcuParaItem> depItems = await DbContext.IcuParaItem.Where(x => x.DeptCode == deptCode && x.ModuleCode == moduleCode && x.ValidState == 1).ToListAsync();
                List<IcuParaItem> sysItems = await DbContext.IcuParaItem.Where(x => x.DeptCode == "system" && x.ValidState == 1).ToListAsync();
                depItems = depItems.Union(sysItems).ToList();
                depItems = depItems.Where((x, i) => depItems.FindIndex(z => z.ParaCode == x.ParaCode) == i).ToList();

                return depItems;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 根据模块类型获取参数列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<List<IcuParaItem>> GetItemByModuleType(string deptCode, string moduleType)
        {
            try
            {
                List<IcuParaItem> items = await
                    (from s in DbContext.IcuParaModule.AsNoTracking().Where(x => x.DeptCode == deptCode && x.ModuleType == moduleType && x.IsEnable == true && x.ValidState == 1)
                     join c in DbContext.IcuParaItem.AsNoTracking().Where(x => x.IsEnable == true && x.ValidState == 1) on s.ModuleCode equals c.ModuleCode
                     select c).ToListAsync();

                return items;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 根据模块名称获取趋势图列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public async Task<List<IcuParaItem>> GetChartItemByDeptCode(string deptCode)
        {
            try
            {
                List<IcuParaItem> depItems = await DbContext.IcuParaItem.Where(x => x.DeptCode == deptCode && x.DrawChartFlag == 1 && x.ValidState == 1).ToListAsync();
                List<IcuParaItem> sysItems = await DbContext.IcuParaItem.Where(x => x.DeptCode == "system" && x.DrawChartFlag == 1 && x.ValidState == 1).ToListAsync();
                depItems = depItems.Union(sysItems).ToList();
                depItems = depItems.Where((x, i) => depItems.FindIndex(z => z.ParaCode == x.ParaCode) == i).ToList();

                return depItems;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 根据模块类型获取所有科室参数列表
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<List<IcuParaItem>> GetDeptItemByType(string moduleType)
        {
            try
            {
                List<IcuParaItem> items = await
                    (from s in DbContext.IcuParaModule.Where(x => x.DeptCode != "system" && x.ModuleType == moduleType && x.IsEnable == true && x.ValidState == 1)
                     join c in DbContext.IcuParaItem.Where(x => x.IsEnable == true && x.ValidState == 1) on s.ModuleCode equals c.ModuleCode
                     select c).ToListAsync();

                return items;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<IcuParaItem>> GetParaCodeListByDeptCodeAndModuleName(string deptCode, string moduleName)
        {
            var query = (from module in DbContext.IcuParaModule
                         join item in DbContext.IcuParaItem on module.ModuleCode equals item.ModuleCode
                         where (module.DeptCode == deptCode || module.DeptCode == "system") && module.ModuleName == moduleName && module.ValidState == 1 && module.IsEnable && item.ValidState == 1 && item.IsEnable
                         select item).Distinct();
            return await query.ToListAsync();
        }

        public async Task<List<IcuParaItem>> GetParaCOdeByDeptCodeAndModuleNameAndParaName(string deptCode, string moduleName, string paraName)
        {
            var query = (from module in DbContext.IcuParaModule
                         join item in DbContext.IcuParaItem on module.ModuleCode equals item.ModuleCode
                         where (module.DeptCode == deptCode || module.DeptCode == "system") && module.ModuleName == moduleName && item.ParaName == paraName && module.ValidState == 1 && module.IsEnable && item.ValidState == 1 && item.IsEnable
                         select item);
            return await query.ToListAsync();
        }
    }
}
