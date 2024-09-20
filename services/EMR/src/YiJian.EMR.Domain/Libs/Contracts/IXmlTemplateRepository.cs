using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Libs.Entities;

namespace YiJian.EMR.Libs
{
    /// <summary>
    /// xml模板文档
    /// </summary>
    public interface IXmlTemplateRepository : IRepository<XmlTemplate, Guid>
    { 
    }
}
