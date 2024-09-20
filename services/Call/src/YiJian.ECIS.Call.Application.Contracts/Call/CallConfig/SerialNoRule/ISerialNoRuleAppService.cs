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
    /// 【排队号规则】应用服务层接口
    /// </summary>
    public interface ISerialNoRuleAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns></returns>
        Task<PagedResultDto<SerialNoRuleData>> GetPagedListAsync(GetSerialNoRuleInput input);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SerialNoRuleData>> GetListAsync();

        /// <summary>
        /// 根据id获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SerialNoRuleData> GetAsync(int id);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SerialNoRuleData> CreateAsync(SerialNoRuleCreation input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SerialNoRuleData> UpdateAsync(int id, SerialNoRuleUpdate input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// 获取科室排队号（叫号）
        /// </summary>
        /// <param name="departmentId">科室id</param>
        /// <returns></returns>
        Task<string> GetSerialNoAsync(Guid departmentId);
    }
}
