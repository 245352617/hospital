using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;  
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 患者的电子病历xml文档
    /// </summary>
    public class PatientEmrXmlRepository : EfCoreRepository<EMRDbContext, PatientEmrXml, Guid>, IPatientEmrXmlRepository
    {

        /// <summary>
        /// 患者的电子病历xml文档
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PatientEmrXmlRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

    }
    
}
