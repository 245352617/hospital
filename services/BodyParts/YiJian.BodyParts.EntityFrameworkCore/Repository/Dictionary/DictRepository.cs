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
using YiJian.BodyParts.Dtos;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:字典-通用业务
    /// </summary>
    public class DictRepository : BaseRepository<EntityFrameworkCore.DbContext, Dict, Guid>, IDictRepository
    {
        public DictRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 根据科室和模块合集获取参数字典
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public async Task<List<DictDto>> GetIcuDicts(string deptCode, List<string> modules)
        {
            try
            {
                List<Dict> depDicts = await DbContext.Dict.Where(x => x.DeptCode == deptCode && x.ValidState == 1 && modules.Contains(x.ModuleCode)).ToListAsync();
                List<Dict> sysDicts = await DbContext.Dict.Where(x => x.DeptCode == "system" && x.ValidState == 1).ToListAsync();
                List<DictDto> depDictDtps = depDicts.Union(sysDicts)
                    .Select(d => new DictDto
                    {
                        Id = d.Id,
                        ParaCode = d.ParaCode,
                        ParaName = d.ParaName,
                        DictCode = d.DictCode,
                        DictValue = d.DictValue,
                        DictDesc = string.IsNullOrWhiteSpace(d.DictDesc) ? d.DictValue : d.DictValue + "：" + d.DictDesc,
                        DictStandard = d.DictStandard,
                        HisCode = d.HisCode,
                        HisName = d.HisName,
                        DeptCode = d.DeptCode,
                        ModuleCode = d.ModuleCode,
                        SortNum = d.SortNum,
                        IsDefault = d.IsDefault,
                        IsEnable = d.IsEnable
                    }).ToList();

                return depDictDtps;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据科室和模块类型获取参数字典
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<List<DictDto>> GetDictByModuleType(string deptCode, string moduleType)
        {
            try
            {
                List<DictDto> depDictDtps = await
                    (from s in DbContext.IcuParaModule.Where(x => x.DeptCode == deptCode && x.ModuleType == moduleType && x.IsEnable == true && x.ValidState == 1)
                     join c in DbContext.Dict.Where(x => x.ValidState == 1) on s.ModuleCode equals c.ModuleCode
                     select new DictDto
                     {
                         Id = c.Id,
                         ParaCode = c.ParaCode,
                         ParaName = c.ParaName,
                         DictCode = c.DictCode,
                         DictValue = c.DictValue,
                         DictDesc = string.IsNullOrWhiteSpace(c.DictDesc) ? c.DictValue : c.DictValue + "：" + c.DictDesc,
                         DictStandard = c.DictStandard,
                         HisCode = c.HisCode,
                         HisName = c.HisName,
                         DeptCode = c.DeptCode,
                         ModuleCode = c.ModuleCode,
                         SortNum = c.SortNum,
                         IsDefault = c.IsDefault,
                         IsEnable = c.IsEnable
                     }).ToListAsync();

                return depDictDtps;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据科室、参数代码、模块类型获取参数字典
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<List<Dict>> GetDictByModuleParaAndType(string deptCode, string paraCode, string moduleType)
        {
            try
            {
                List<Dict> dicts = await
                    (from s in DbContext.IcuParaModule.Where(x => x.DeptCode == deptCode && x.ModuleType == moduleType && x.IsEnable == true && x.ValidState == 1)
                     join c in DbContext.Dict.Where(x => x.ParaCode == paraCode && x.ValidState == 1) on s.ModuleCode equals c.ModuleCode
                     select c).ToListAsync();

                return dicts;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
