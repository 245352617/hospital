using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.Recipe
{
    public interface INovelCoronavirusRnaAppService : IApplicationService
    {
        /// <summary>
        /// 保存新冠rna检测申请
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        Task<NovelCoronavirusRna> SaveAsync(NovelCoronavirusRnaCreate dto);

        /// <summary>
        /// 获取新冠检测申请单
        /// </summary>
        /// <param name="doctorsAdviceId"></param>
        /// <param name="piid"></param>
        /// <returns></returns>
        Task<NovelCoronavirusRnaDto> GetAsync(Guid doctorsAdviceId, Guid piid);
    }
}