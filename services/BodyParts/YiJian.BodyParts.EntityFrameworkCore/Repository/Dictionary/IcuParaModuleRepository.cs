using YiJian.BodyParts.EntityFrameworkCore;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:模块参数
    /// </summary>
    public class IcuParaModuleRepository : BaseRepository<EntityFrameworkCore.DbContext, IcuParaModule, Guid>, IIcuParaModuleRepository
    {
        public IcuParaModuleRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }


        /// <summary>
        /// 通过科室编号+模块名称获取对应的模块编号
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleName"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<string> GetModuleCodeAsync(string deptCode, string moduleName, string moduleType)
        {
            var module = await base.DbContext.IcuParaModule.FirstOrDefaultAsync(f => f.DeptCode == deptCode && f.ModuleName == moduleName && f.ModuleType == moduleType && f.ValidState == 1);
            return module?.ModuleCode;
        }

        /// <summary>
        /// 根据科室、模块类型获取模块列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<List<IcuParaModule>> GetIcuParaModuleByType(string deptCode, string moduleType)
        {
            try
            {
                List<IcuParaModule> modules =
                    await DbContext.IcuParaModule.Where(x => x.DeptCode == deptCode && x.ModuleType == moduleType && x.IsEnable == true && x.ValidState == 1)
                    .OrderBy(x => x.SortNum).ToListAsync();

                return modules;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取观察项、出入量模块列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public async Task<List<IcuParaModule>> GetIcuParaModuleByVsIo(string deptCode)
        {
            try
            {
                List<IcuParaModule> modules =
                    await DbContext.IcuParaModule.Where(x => x.DeptCode == deptCode && (x.ModuleType == "VS" || x.ModuleType == "IO") && x.IsEnable == true && x.ValidState == 1)
                    .OrderBy(x => x.SortNum).ToListAsync();

                return modules;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
