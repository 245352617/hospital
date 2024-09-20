using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData
{
    /// <summary>
    /// 描    述 ：护士医嘱类别仓储接口
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/24 16:33:18
    /// </summary>
    public interface INursingRecipeTypeRepository : IRepository<NursingRecipeType, Guid>
    {
    }
}
