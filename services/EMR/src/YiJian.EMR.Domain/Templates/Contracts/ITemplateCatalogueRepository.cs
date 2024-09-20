using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Templates.Contracts
{
    /// <summary>
    /// 模板目录结构
    /// </summary>
    public interface ITemplateCatalogueRepository : IRepository<TemplateCatalogue, Guid>
    {

    }
}
