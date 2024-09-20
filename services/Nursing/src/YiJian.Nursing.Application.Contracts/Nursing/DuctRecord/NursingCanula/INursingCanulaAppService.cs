using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS;

namespace YiJian.Nursing
{
    /// <summary>
    /// 护士导管护理API
    /// </summary>
    public interface INursingCanulaAppService : IApplicationService
    {
        /// <summary>
        /// 根据条件查询插管列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<List<NursingCanulaDto>>> SelectNursingCanulaListAsync(NursingCanulaInput input);

        /// <summary>
        /// 查询管道列表
        /// </summary>
        /// <param name="canulaId"></param>
        /// <param name="state"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<JsonResult<List<CanulaListDto>>> SelectCanulaListAsync(Guid canulaId, int state, DateTime? startTime, DateTime? endTime);

        /// <summary>
        /// 查询管道属性项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pI_ID"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<JsonResult<NursingCanulaDto>> SelectNursingCanulaInfoAsync(Guid id,
            Guid pI_ID, string moduleCode);

        /// <summary>
        /// 新增或修改管道属性
        /// </summary>
        /// <param name="neworupdate"></param>
        /// <param name="nursingCanula"></param>
        /// <returns></returns>
        Task<JsonResult> CreateNursingCanulaInfoAsync(string neworupdate, NursingCanulaDto nursingCanula);

        /// <summary>
        /// 拔管/换管/取消拔管
        /// </summary>
        /// <param name="nursingCanulaDto"></param>
        /// <param name="inDeptTime"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateNursingCanulaInfoAsync(NursingCanulaDto nursingCanulaDto,
            DateTime? inDeptTime,
            TubeDrawStateEnum state = TubeDrawStateEnum.其他);

        /// <summary>
        /// 删除一条导管
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteNursingCanulaInfoAsync(Guid guid);

        /// <summary>
        /// 查询管道观察项
        /// </summary>
        /// <param name="canulaId"></param>
        /// <param name="nurseTime"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<JsonResult<CanulaDto>> SelectCanulaInfoAsync(Guid canulaId, DateTime nurseTime, string itemId = "");

        /// <summary>
        /// 新增或修改管道观察
        /// </summary>
        /// <param name="canula"></param>
        /// <returns></returns>
        Task<JsonResult<string>> CreateCanulaInfoAsync(CanulaDto canula);

        /// <summary>
        /// 删除一条导管记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteCanulaInfoAsync(Guid id);

        /// <summary>
        /// 复制一条数据
        /// </summary>
        /// <param name="copyCanulaDto"></param>
        /// <returns></returns>
        Task<JsonResult<string>> CopyCanulaInfoAsync(CopyCanulaDto copyCanulaDto);

        /// <summary>
        /// 全景视图-导管列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<NursingCanula>> GetAllViewNursingCanulaListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default);
    }
}