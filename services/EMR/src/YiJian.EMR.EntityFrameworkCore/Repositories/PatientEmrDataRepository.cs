using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Entities; 

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 绑定的电子病历提取的数据
    /// </summary>
    public class PatientEmrDataRepository : EfCoreRepository<EMRDbContext, PatientEmrData, Guid>, IPatientEmrDataRepository
    {

        /// <summary>
        /// 绑定的电子病历提取的数据
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PatientEmrDataRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
         
    }

}
