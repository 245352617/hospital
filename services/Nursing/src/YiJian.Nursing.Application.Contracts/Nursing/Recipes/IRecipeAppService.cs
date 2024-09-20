using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 医嘱服务接口
    /// </summary>
    public interface IRecipeAppService : IApplicationService, ICapSubscribe
    {
        /// <summary>
        /// 获取药物医嘱
        /// </summary>
        /// <param name="piId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<RecipeDto>> GetPrescribeRecipeAsync(Guid piId, string query);
    }
}
