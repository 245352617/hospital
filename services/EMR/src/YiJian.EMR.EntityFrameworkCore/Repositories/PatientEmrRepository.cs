using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 患者电子病历
    /// </summary>
    public class PatientEmrRepository : EfCoreRepository<EMRDbContext, PatientEmr, Guid>, IPatientEmrRepository
    {
        /// <summary>
        /// 患者的电子病历xml文档
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PatientEmrRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 根据就诊流水号，获取关联的病历信息
        /// </summary>
        /// <param name="visitSerialNo"></param>
        /// <returns></returns>
        public async Task<List<ViewVisitSerialEmr>> GetEmrByVisitSerialAsync(string visitSerialNo)
        {
            var db = await GetDbContextAsync();
            var query = db.ViewVisitSerialEmrs.Where(w => w.VisitNo == visitSerialNo.Trim());
            return await query.ToListAsync();
        }
    }
}
