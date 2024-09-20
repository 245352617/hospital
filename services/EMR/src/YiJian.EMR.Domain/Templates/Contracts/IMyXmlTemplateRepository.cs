using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Templates.Contracts
{
    /// <summary>
    /// 我的XML电子病例模板(通用模板，科室模板，个人模板)
    /// </summary>
    public interface IMyXmlTemplateRepository : IRepository<MyXmlTemplate, Guid>
    {

    }
}
