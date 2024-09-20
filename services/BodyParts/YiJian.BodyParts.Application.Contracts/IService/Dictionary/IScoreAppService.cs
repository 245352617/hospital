using YiJian.BodyParts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace YiJian.BodyParts.IService
{
    public interface IScoreAppService : ITransientDependency, IApplicationService
    {

        /// <summary>
        /// 查询所有评分
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <returns></returns>
        Task<JsonResult<List<IcuScoreStandardDto>>> SelectScoreStandardExcute(string deptCode);

        /// <summary>
        /// 根据模块id获取评分项列表
        /// </summary>
        /// <param name="Id">模块Id</param>
        /// <returns></returns>

        Task<JsonResult<List<IcuScoreStandardDetailDto>>> GetScoreStandardDetailList(Guid Id);

        /// <summary>
        /// 根据评分项id获取评分项选项
        /// </summary>
        /// <param name="Id">模块Id</param>
        /// <returns></returns>

        Task<JsonResult<List<IcuScoreResultDto>>> GetScoreResultList(Guid Id);

        /// <summary>
        /// 评分设置-根据模块ID取措施详情数据列表
        /// </summary>
        /// <param name="Id">模块ID</param>
        /// <returns></returns>
        Task<JsonResult<List<IcuScoreMeasureDetailDto>>> GetScoreMeasureAndDetailList(Guid Id);

        /// <summary>
        /// 评分设置-根据模块ID取评估内容数据列表
        /// </summary>
        /// <param name="Id">模块ID</param>
        /// <returns></returns>
        Task<JsonResult<List<IcuScoreStandardAndDetailDto>>> GetScoreStandardAndDetailList(Guid Id);

        /// <summary>
        /// 增加评分项目（模块）
        /// </summary>
        /// <param name="icuScoreStandard"></param>
        /// <returns></returns>
        Task<JsonResult> AddScoreStandrd(IcuScoreStandardDto icuScoreStandard);

        /// <summary>
        /// 增加或修改评分项（模块下评分项）
        /// </summary>
        /// <param name="icuScoreDetail"></param>
        /// <returns></returns>

        Task<JsonResult> AddScoreDetail(IcuScoreStandardDetailDto icuScoreDetail);

        /// <summary>
        /// 增加或修改评分项选项
        /// </summary>
        /// <param name="icuScoreResult"></param>
        /// <returns></returns>
        Task<JsonResult> AddScoreResult(IcuScoreResultDto icuScoreResult);

        /// <summary>
        /// 增加或修改措施类型
        /// </summary>
        /// <param name="icuScoreMeasure"></param>
        /// <returns></returns>
        Task<JsonResult> AddMeasure(IcuScoreMeasureDto icuScoreMeasure);


        /// <summary>
        /// 增加或修改措施类型的选项
        /// </summary>
        /// <param name="icuScoreMeasureDetail"></param>
        /// <returns></returns>
        Task<JsonResult> AddMeasureDetail(IcuScoreMeasureDetailDto icuScoreMeasureDetail);


        /// <summary>
        /// 删除评分项目（模块）
        /// </summary>
        /// <param name="icuScoreStandard"></param>
        /// <returns></returns>
        Task<JsonResult> DeletecoreStandrd(IcuScoreStandardDto icuScoreStandard);


        /// <summary>
        /// 删除评分项
        /// </summary>
        /// <param name="icuScoreDetail"></param>
        /// <returns></returns>
        Task<JsonResult> DeletecoreDetail(IcuScoreStandardDetailDto icuScoreDetail);

        /// <summary>
        /// 删除评分项选项
        /// </summary>
        /// <param name="icuScoreResult"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteScoreResult(IcuScoreResultDto icuScoreResult);

        /// <summary>
        /// 删除措施类型
        /// </summary>
        /// <param name="icuScoreMeasure"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteMeasure(IcuScoreMeasureDto icuScoreMeasure);

        /// <summary>
        /// 删除措施项
        /// </summary>
        /// <param name="icuScoreMeasureDetail"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteMeasureDetail(IcuScoreMeasureDetailDto icuScoreMeasureDetail);

        /// <summary>
        /// 复制评分,从一个科室到另一个科室
        /// </summary>
        /// <param name="oldDeptCode">源科室代码</param>
        /// <param name="newDeptCode">目标科室代码</param>
        /// <returns></returns>

        Task<JsonResult> CopyScoreInfo(string oldDeptCode, string newDeptCode);

    }
}
