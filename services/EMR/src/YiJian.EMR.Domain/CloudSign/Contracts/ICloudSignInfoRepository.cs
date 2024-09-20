using System;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.CloudSign.Entities;

namespace YiJian.EMR.CloudSign.Contracts
{
    /// <summary>
    /// 描述：云签信息表数据访问接口
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 12:15:10
    /// </summary>
    public interface ICloudSignInfoRepository : IRepository<CloudSignInfo, Guid>
    {
    }
}
