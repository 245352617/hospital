using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Libs.Entities;

namespace YiJian.EMR.Libs
{
    /// <summary>
    /// 目录树结构仓储接口
    /// </summary>
    public interface ICatalogueRepository : IRepository<Catalogue, Guid>
    {
        
    }
}
