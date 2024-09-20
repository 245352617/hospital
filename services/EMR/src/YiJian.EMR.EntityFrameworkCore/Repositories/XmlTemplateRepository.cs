using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Libs;
using YiJian.EMR.Libs.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// xml病历模板
    /// </summary>
    public class XmlTemplateRepository : EfCoreRepository<EMRDbContext, XmlTemplate, Guid>, IXmlTemplateRepository
    {
        /// <summary>
        /// xml病历模板
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public XmlTemplateRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }
    }
}
