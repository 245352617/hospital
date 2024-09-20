using Volo.Abp.Domain.Repositories;
using YiJian.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 医嘱映射表
    /// </summary>
    public interface IDoctorsAdviceMapRepository : IRepository<DoctorsAdviceMap, long>
    {

    }
}
