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
    /// 【医生变动】应用服务层接口
    /// </summary>
    public interface IDoctorRegularAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns></returns>
        Task<PagedResultDto<DoctorRegularData>> GetPagedListAsync(GetDoctorRegularInput input);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DoctorRegularData>> GetListAsync();

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DoctorRegularData> CreateAsync(DoctorRegularCreation input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DoctorRegularData> UpdateAsync(Guid id, DoctorRegularUpdate input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
