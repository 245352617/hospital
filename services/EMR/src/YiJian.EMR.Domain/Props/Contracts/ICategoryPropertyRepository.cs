using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Props.Entities;

namespace YiJian.EMR.Props.Contracts
{
    /// <summary>
    /// 电子病历属性
    /// </summary>
    public interface ICategoryPropertyRepository : IRepository<CategoryProperty, Guid>
    {

    }
}
