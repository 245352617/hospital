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
    /// 表:皮肤详细信息记录表
    /// </summary>
    public class IcuSkinRepository : BaseRepository<DbContext, IcuSkin, Guid>, IIcuSkinRepository
    {

        public IcuSkinRepository(IDbContextProvider<DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        #region 实现接口

        #endregion
    }
}
