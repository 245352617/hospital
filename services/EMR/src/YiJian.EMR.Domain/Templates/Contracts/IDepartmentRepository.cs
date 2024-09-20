using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Templates.Contracts
{
    /// <summary>
    /// 使用过改病历模板的科室名单
    /// </summary>
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
    }
}
