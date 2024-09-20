using System;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描    述 ：护士医嘱类别仓储类
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/24 16:36:16
    /// </summary>
    public class NursingRecipeTypeRepository : MasterDataRepositoryBase<NursingRecipeType, Guid>, INursingRecipeTypeRepository
    {
        public NursingRecipeTypeRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
