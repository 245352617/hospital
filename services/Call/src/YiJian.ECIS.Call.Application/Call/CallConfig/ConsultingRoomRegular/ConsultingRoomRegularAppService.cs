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
    /// 【诊室固定】应用服务
    /// </summary>
    public class ConsultingRoomRegularAppService : CallAppService, IConsultingRoomRegularAppService
    {
        private readonly IConsultingRoomRegularRepository _consultingRoomRegularRepository;
        private readonly ConsultingRoomRegularManager _consultingRoomRegularManager;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IConsultingRoomRepository _consultingRoomRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="consultingRoomRegularRepository"></param>
        /// <param name="consultingRoomRegularManager"></param>
        /// <param name="departmentRepository"></param>
        /// <param name="consultingRoomRepository"></param>
        public ConsultingRoomRegularAppService(IConsultingRoomRegularRepository consultingRoomRegularRepository
            , ConsultingRoomRegularManager consultingRoomRegularManager
            , IDepartmentRepository departmentRepository
            , IConsultingRoomRepository consultingRoomRepository)
        {
            _consultingRoomRegularRepository = consultingRoomRegularRepository;
            _consultingRoomRegularManager = consultingRoomRegularManager;
            _departmentRepository = departmentRepository;
            _consultingRoomRepository = consultingRoomRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">新增的实体</param>
        /// <returns></returns>
        public async Task<ConsultingRoomRegularData> CreateAsync(ConsultingRoomRegularCreation input)
        {
            var consultingRoom = await this._consultingRoomRepository.FirstOrDefaultAsync(x => x.Id == input.ConsultingRoomId);
            var dept = await this._departmentRepository.FirstOrDefaultAsync(x => x.Id == input.DepartmentId);
            if (consultingRoom is null)
            {
                await _consultingRoomRepository.InsertAsync(new ConsultingRoom(input.ConsultingRoomId.Value, input.ConsultingRoomName, input.ConsultingRoomCode, input.IP, input.ConsultingRoomIsActived));
            }
            if (dept is null)
            {
                await _departmentRepository.InsertAsync(new Department(input.DepartmentId.Value, input.DeptName, input.DeptCode, input.DeptIsActived));
            }

            var record = await _consultingRoomRegularManager.CreateAsync(input.ConsultingRoomId.Value, input.DepartmentId.Value, input.IsActived);
            // 保存修改
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var output = await GetAsync(record.Id);

            return output;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">删除的记录id</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _consultingRoomRegularManager.DeleteAsync(id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">查询的记录id</param>
        /// <returns></returns>
        public async Task<ConsultingRoomRegularData> GetAsync(Guid id)
        {
            var query = from crr in (await _consultingRoomRegularRepository.GetQueryableAsync())
                        join cr in (await _consultingRoomRepository.GetQueryableAsync()) on crr.ConsultingRoomId equals cr.Id
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on crr.DepartmentId equals dpt.Id
                        where crr.Id == id
                        select new ConsultingRoomRegularData
                        {
                            Id = crr.Id,
                            IsActived = crr.IsActived,
                            ConsultingRoomId = crr.ConsultingRoomId,
                            ConsultingRoomCode = cr.Code,
                            ConsultingRoomName = cr.Name,
                            IP = cr.IP,
                            DepartmentId = dpt.Id,
                            DepartmentCode = dpt.Code,
                            DepartmentName = dpt.Name,
                        };

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ConsultingRoomRegularData>> GetListAsync()
        {
            var query = from crr in (await _consultingRoomRegularRepository.GetQueryableAsync())
                        join cr in (await _consultingRoomRepository.GetQueryableAsync()) on crr.ConsultingRoomId equals cr.Id
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on crr.DepartmentId equals dpt.Id
                        select new ConsultingRoomRegularData
                        {
                            Id = crr.Id,
                            IsActived = crr.IsActived,
                            ConsultingRoomId = crr.ConsultingRoomId,
                            ConsultingRoomCode = cr.Code,
                            ConsultingRoomName = cr.Name,
                            IP = cr.IP,
                            DepartmentId = dpt.Id,
                            DepartmentCode = dpt.Code,
                            DepartmentName = dpt.Name,
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ConsultingRoomRegularData>> GetPagedListAsync(GetConsultingRoomRegularInput input)
        {
            var query = from crr in (await _consultingRoomRegularRepository.GetQueryableAsync())
                        join cr in (await _consultingRoomRepository.GetQueryableAsync()) on crr.ConsultingRoomId equals cr.Id
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on crr.DepartmentId equals dpt.Id
                        select new ConsultingRoomRegularData
                        {
                            Id = crr.Id,
                            IsActived = crr.IsActived,
                            ConsultingRoomId = crr.ConsultingRoomId,
                            ConsultingRoomCode = cr.Code,
                            ConsultingRoomName = cr.Name,
                            IP = cr.IP,
                            DepartmentId = dpt.Id,
                            DepartmentCode = dpt.Code,
                            DepartmentName = dpt.Name,
                        };
            var list = await query
                .OrderBy(nameof(ConsultingRoomRegular.Id))
                .PageBy(input.SkipCount, input.Size)
                .ToListAsync();

            var totalCount = await query.LongCountAsync();
            var result = new PagedResultDto<ConsultingRoomRegularData>(totalCount, list);

            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">修改的记录id</param>
        /// <param name="input">修改的实体</param>
        /// <returns></returns>
        public async Task<ConsultingRoomRegularData> UpdateAsync(Guid id, ConsultingRoomRegularUpdate input)
        {
            var consultingRoom = await this._consultingRoomRepository.FirstOrDefaultAsync(x => x.Id == input.ConsultingRoomId);
            if (consultingRoom is null)
            {
                // 诊室不存在
                Oh.Error("诊室不存在");
            }
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == input.DepartmentId);
            if (department is null)
            {
                // 科室不存在
                Oh.Error("科室不存在");
            }

            var record = await _consultingRoomRegularRepository.GetAsync(id);
            record.Edit(input.DepartmentId.Value, input.ConsultingRoomId.Value, input.IsActived);
            await _consultingRoomRegularManager.UpdateAsync(record);
            // 保存修改
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var output = await GetAsync(record.Id);

            return output;
        }

    }
}
