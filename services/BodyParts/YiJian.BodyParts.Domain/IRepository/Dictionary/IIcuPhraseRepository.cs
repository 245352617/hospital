using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:常用语模板
    /// </summary>
    public interface IIcuPhraseRepository : IRepository<IcuPhrase, Guid>, IBaseRepository<IcuPhrase, Guid>
    {
        #region 定义接口
        Task<bool> CreatePhraseInfo(CreateUpdateIcuPhraseDto icuPhraseDto);
        #endregion
    }
}
