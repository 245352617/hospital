using Volo.Abp.Domain.Repositories;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：自备药仓储接口
    /// 创建人： yangkai
    /// 创建时间：2022/10/27 16:45:26
    /// </summary>
    public interface IOwnMedicineRepository : IRepository<OwnMedicine, int>
    {
    }
}
