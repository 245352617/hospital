using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描述：自备药仓储类
    /// 创建人： yangkai
    /// 创建时间：2022/10/27 18:58:31
    /// </summary>
    public class OwnMedicineRepository : NursingRepositoryBase<OwnMedicine, int>, IOwnMedicineRepository
    {
        public OwnMedicineRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
