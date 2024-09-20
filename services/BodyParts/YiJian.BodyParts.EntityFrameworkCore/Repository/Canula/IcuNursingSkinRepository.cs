using YiJian.BodyParts.EntityFrameworkCore;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:皮肤主表
    /// </summary>
    public class IcuNursingSkinRepository : BaseRepository<EntityFrameworkCore.DbContext, IcuNursingSkin, Guid>, IIcuNursingSkinRepository
    {
        public IcuNursingSkinRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

    }
}
