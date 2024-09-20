using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 医嘱审计
    /// </summary>
    public interface IDoctorsAdviceAuditRepository : IRepository<DoctorsAdviceAudit, Guid>
    {
    }

}
