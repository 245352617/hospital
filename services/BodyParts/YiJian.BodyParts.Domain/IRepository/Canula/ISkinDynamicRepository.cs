using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:人体图动态表
    /// </summary>
    public interface ISkinDynamicRepository : IRepository<SkinDynamic, Guid>, IBaseRepository<SkinDynamic, Guid>
    {
        #region 定义接口

        #endregion
    }
}
