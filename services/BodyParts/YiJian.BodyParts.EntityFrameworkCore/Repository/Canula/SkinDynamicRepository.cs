using YiJian.BodyParts.EntityFrameworkCore;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:人体图动态表
    /// </summary>
    public class SkinDynamicRepository : BaseRepository<DbContext, SkinDynamic, Guid>, ISkinDynamicRepository
    {
        public SkinDynamicRepository(IDbContextProvider<DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        #region 实现接口

        #endregion
    }
}
