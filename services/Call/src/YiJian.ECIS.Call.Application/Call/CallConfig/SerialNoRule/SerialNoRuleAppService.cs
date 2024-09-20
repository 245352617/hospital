using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.Call.CallConfig.Dtos;
using YiJian.ECIS.Call.Domain;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 【排队号规则】应用服务
    /// </summary>
    public class SerialNoRuleAppService : CallAppService, ISerialNoRuleAppService
    {
        private readonly ISerialNoRuleRepository _serialNoRuleRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly SerialNoRuleManager _serialNoRuleManager;

        /// <summary>
        /// 构造方法
        /// </summary> 
        public SerialNoRuleAppService(ISerialNoRuleRepository serialNoRuleRepository
            , IDepartmentRepository departmentRepository
            , SerialNoRuleManager serialNoRuleManager)
        {
            _serialNoRuleRepository = serialNoRuleRepository;
            _departmentRepository = departmentRepository;
            _serialNoRuleManager = serialNoRuleManager;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SerialNoRuleData> CreateAsync(SerialNoRuleCreation input)
        {
            // 医生id、医生可用状态不在当前服务验证
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == input.DepartmentId);
            if (department is null)
            {
                // 科室不存在
                throw new DepartmentNotExistsException();
            }

            if (await (await _serialNoRuleRepository.GetQueryableAsync()).Where(x => x.DepartmentId == input.DepartmentId).CountAsync() > 0)
            {
                // 当前科室已存在排队号规则
                Oh.Error("当前科室已存在排队号规则");
            }

            var serialNoRule = new SerialNoRule(input.DepartmentId.Value, input.Prefix, input.SerialLength);
            var record = await _serialNoRuleRepository.InsertAsync(serialNoRule);
            // 保存修改
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var output = await GetAsync(record.Id);

            return output;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            await _serialNoRuleManager.DeleteAsync(id);
        }

        /// <summary>
        /// 根据id获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SerialNoRuleData> GetAsync(int id)
        {
            var query = from snr in (await _serialNoRuleRepository.GetQueryableAsync())
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on snr.DepartmentId equals dpt.Id
                        where snr.Id == id
                        orderby snr.Id
                        select new SerialNoRuleData
                        {
                            Id = snr.Id,
                            DepartmentId = dpt.Id,
                            DepartmentName = dpt.Name,
                            Prefix = snr.Prefix,
                            SerialLength = snr.SerialLength,
                        };

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SerialNoRuleData>> GetListAsync()
        {
            var query = from snr in (await _serialNoRuleRepository.GetQueryableAsync())
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on snr.DepartmentId equals dpt.Id
                        orderby snr.Id
                        select new SerialNoRuleData
                        {
                            Id = snr.Id,
                            DepartmentId = dpt.Id,
                            DepartmentName = dpt.Name,
                            Prefix = snr.Prefix,
                            SerialLength = snr.SerialLength,
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SerialNoRuleData>> GetPagedListAsync(GetSerialNoRuleInput input)
        {
            var query = from snr in (await _serialNoRuleRepository.GetQueryableAsync())
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on snr.DepartmentId equals dpt.Id
                        orderby snr.Id
                        select new SerialNoRuleData
                        {
                            Id = snr.Id,
                            DepartmentId = dpt.Id,
                            DepartmentName = dpt.Name,
                            Prefix = snr.Prefix,
                            SerialLength = snr.SerialLength,
                        };
            var list = await query
                .OrderBy(nameof(SerialNoRule.Id))
                .PageBy(input.SkipCount, input.Size).ToListAsync();

            var totalCount = await query.LongCountAsync();
            var result = new PagedResultDto<SerialNoRuleData>(totalCount, list);

            return result;
        }

        /// <summary>
        /// 获取科室排队号（叫号）
        /// </summary>
        /// <param name="departmentId">科室id</param>
        /// <returns></returns>
        public async Task<string> GetSerialNoAsync(Guid departmentId)
        {
            return await _serialNoRuleManager.GetSerialNoAsync(departmentId,string.Empty);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SerialNoRuleData> UpdateAsync(int id, SerialNoRuleUpdate input)
        {
            // 医生id、医生可用状态不在当前服务验证
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == input.DepartmentId);
            if (department is null)
            {
                // 科室不存在
                throw new DepartmentNotExistsException();
            }

            var record = await _serialNoRuleRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (record is null)
            {
                // 当前排队号规则不存在
                Oh.Error("排队号规则不存在");
            }

            if (await (await _serialNoRuleRepository.GetQueryableAsync()).Where(x => x.DepartmentId == input.DepartmentId && x.Id != id).CountAsync() > 0)
            {
                // 当前科室已存在排队号规则
                Oh.Error("当前科室已存在排队号规则");
            }
            _ = record.Update(input.DepartmentId.Value, input.Prefix, input.SerialLength);
            await _serialNoRuleRepository.UpdateAsync(record);
            // 保存修改
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var output = await GetAsync(record.Id);

            return output;
        }
    }
}
