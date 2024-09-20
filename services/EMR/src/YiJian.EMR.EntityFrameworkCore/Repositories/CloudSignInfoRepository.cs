using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.CloudSign.Contracts;
using YiJian.EMR.CloudSign.Entities;
using YiJian.EMR.EntityFrameworkCore;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 描述：云签信息表数据访类
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 12:17:06
    /// </summary>
    public class CloudSignInfoRepository : EfCoreRepository<EMRDbContext, CloudSignInfo, Guid>, ICloudSignInfoRepository
    {
        public CloudSignInfoRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
