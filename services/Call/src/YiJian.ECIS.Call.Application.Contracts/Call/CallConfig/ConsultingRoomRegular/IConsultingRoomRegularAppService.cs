using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.Call.CallConfig.Dtos;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 【诊室固定】应用服务层接口
    /// </summary>
    public interface IConsultingRoomRegularAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns></returns>
        Task<PagedResultDto<ConsultingRoomRegularData>> GetPagedListAsync(GetConsultingRoomRegularInput input);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ConsultingRoomRegularData>> GetListAsync();

        /// <summary>
        /// 根据id获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ConsultingRoomRegularData> GetAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ConsultingRoomRegularData> CreateAsync(ConsultingRoomRegularCreation input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ConsultingRoomRegularData> UpdateAsync(Guid id, ConsultingRoomRegularUpdate input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
