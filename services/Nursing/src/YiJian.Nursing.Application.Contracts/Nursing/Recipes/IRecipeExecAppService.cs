using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.Nursing.RecipeExecutes.Entities;
using YiJian.Nursing.Recipes.Dtos;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：医嘱执行操作接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 10:50:26
    /// </summary>
    public interface IRecipeExecAppService : IApplicationService
    {
        /// <summary>
        /// 查询医嘱分页列表
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        Task<ResultDataDto> QueryRecipePageListAsync(QueryRecipeDto queryRecipeDto);

        /// <summary>
        /// 拉取his医嘱
        /// </summary>
        /// <param name="piId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task PullHisRecipeAsync(Guid piId, string patientId);

        /// <summary>
        /// 获取核对和执行列表
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        Task<List<CheckExecDto>> GetCheckExecListAsync(QueryRecipeDto queryRecipeDto);

        /// <summary>
        /// 获取取消核对列表
        /// </summary>
        /// <param name="queryRecipeDto"></param>
        /// <returns></returns>
        Task<List<RecipeExecDto>> GetCancelCheckExecListAsync(QueryRecipeDto queryRecipeDto);

        /// <summary>
        /// 核对及批量核对
        /// </summary>
        /// <param name="checkExecDtos"></param>
        /// <returns></returns>
        Task<bool> CkeckAsync(List<CheckExecDto> checkExecDtos);

        /// <summary>
        /// 二次核对及批量二次核对
        /// </summary>
        /// <param name="checkExecDtos"></param>
        /// <returns></returns>
        Task<bool> TwoCkeckAsync(List<CheckExecDto> checkExecDtos);

        /// <summary>
        /// 一键执行
        /// </summary>
        /// <param name="batchExecDto"></param>
        /// <returns></returns>
        Task<bool> BatchExecAsync(BatchExecDto batchExecDto);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="execDto"></param>
        /// <returns></returns>
        Task<bool> ExecAsync(ExecDto execDto);

        /// <summary>
        /// 取消核对
        /// </summary>
        /// <param name="cancelDto"></param>
        /// <returns></returns>
        Task<bool> CancelCheckAsync(BaseRequestDto cancelDto);

        /// <summary>
        /// 一键取消核对
        /// </summary>
        /// <param name="cancelDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCancelCheckAsync(List<BaseRequestDto> cancelDtos);

        /// <summary>
        /// 取消二次核对
        /// </summary>
        /// <param name="cancelDto"></param>
        /// <returns></returns>
        Task<bool> CancelTwoCheckAsync(BaseRequestDto cancelDto);

        /// <summary>
        /// 取消执行
        /// </summary>
        /// <param name="cancelDto"></param>
        /// <returns></returns>
        Task<bool> CancelExecAsync(BaseRequestDto cancelDto);

        /// <summary>
        /// 一键取消执行
        /// </summary>
        /// <param name="cancelDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCancelExecAsync(List<BaseRequestDto> cancelDtos);

        /// <summary>
        /// 更新备用量
        /// </summary>
        /// <param name="execDto"></param>
        /// <returns></returns>
        Task<bool> UpdateReserveDosageAsync(ExecDto execDto);

        /// <summary>
        /// 获取执行记录
        /// </summary>
        /// <param name="recipeExecId"></param>
        /// <returns></returns>
        Task<List<RecipeExecRecord>> GetRecipeExecRecordListAsync(Guid recipeExecId);
    }
}
