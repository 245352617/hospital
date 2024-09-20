using System;
using Volo.Abp.Domain.Repositories;
using YiJian.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 医嘱回调HIS返回的结果
    /// </summary>
    public interface IMedDetailResultRepository : IRepository<MedDetailResult, Guid>
    {

    }
}
