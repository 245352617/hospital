using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 诊疗项
    /// </summary>
    public interface ITreatRepository : IRepository<Treat, Guid>
    {
    }

}
