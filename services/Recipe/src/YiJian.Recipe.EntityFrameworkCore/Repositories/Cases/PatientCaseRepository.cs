using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Cases.Contracts;
using YiJian.Cases.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipe.Repositories.Cases
{
    /// <summary>
    /// 患者病历信息
    /// </summary>
    public class PatientCaseRepository : EfCoreRepository<RecipeDbContext, PatientCase, int>, IPatientCaseRepository
    {
        /// <summary>
        /// 患者病历信息
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PatientCaseRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }


        /// <summary>
        /// 获取当前患者最新的病历信息
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        public async Task<PatientCase> GetPatientCaseAsync(Guid piid)
        {
            var db = await GetDbContextAsync();
            var entity = await db.PatientCases.Where(w => w.Piid == piid).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return entity;
        }

    }
}
