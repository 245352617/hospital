using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:皮肤详细信息记录表
    /// </summary>
    public interface IIcuSkinRepository : IRepository<IcuSkin, Guid>, IBaseRepository<IcuSkin, Guid>
    {
        #region 定义接口

        #endregion
    }
}
