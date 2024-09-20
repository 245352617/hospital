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

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 【医生变动】应用服务
    /// </summary>
    public class DoctorRegularAppService : CallAppService, IDoctorRegularAppService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDoctorRegularRepository _doctorRegularRepository;
        private readonly DoctorRegularManager _doctorRegularManager;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// <param name="doctorRegularRepository"></param>
        /// <param name="doctorRegularManager"></param>
        public DoctorRegularAppService(IDepartmentRepository departmentRepository
            , IDoctorRegularRepository doctorRegularRepository
            , DoctorRegularManager doctorRegularManager)
        {
            _departmentRepository = departmentRepository;
            _doctorRegularRepository = doctorRegularRepository;
            _doctorRegularManager = doctorRegularManager;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">新增的实体</param>
        /// <returns></returns>
        public async Task<DoctorRegularData> CreateAsync(DoctorRegularCreation input)
        {
            // 医生id、医生可用状态不在当前服务验证
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == input.DepartmentId);
            if (department is null)
            {
                // 科室不存在
                throw new DepartmentNotExistsException();
            }

            var doctorRegular = (new DoctorRegular(GuidGenerator.Create()))
                .SetDoctor(input.DoctorId, input.DoctorName, input.DoctorDepartmentId.Value, input.DoctorDepartmentName)
                .SetDepartment(department.Id)
                .SetActive(input.IsActived);
            var record = await _doctorRegularManager.CreateAsync(doctorRegular);
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
            await _doctorRegularManager.DeleteAsync(id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">查询的记录id</param>
        /// <returns></returns>
        public async Task<DoctorRegularData> GetAsync(Guid id)
        {
            var query = from dr in (await _doctorRegularRepository.GetQueryableAsync())
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on dr.DepartmentId equals dpt.Id
                        where dr.Id == id
                        select new DoctorRegularData
                        {
                            Id = dr.Id,
                            DoctorId = dr.DoctorId,
                            DoctorName = dr.DoctorName,
                            DoctorDepartmentId = dr.DoctorDepartmentId,
                            DoctorDepartmentName = dr.DoctorDepartmentName,
                            IsActived = dr.IsActived,
                            DepartmentId = dpt.Id,
                            DepartmentName = dpt.Name,
                        };

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DoctorRegularData>> GetListAsync()
        {
            var query = from dr in (await _doctorRegularRepository.GetQueryableAsync())
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on dr.DepartmentId equals dpt.Id
                        select new DoctorRegularData
                        {
                            Id = dr.Id,
                            DoctorId = dr.DoctorId,
                            DoctorName = dr.DoctorName,
                            DoctorDepartmentId = dr.DoctorDepartmentId,
                            DoctorDepartmentName = dr.DoctorDepartmentName,
                            IsActived = dr.IsActived,
                            DepartmentId = dpt.Id,
                            DepartmentName = dpt.Name,
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DoctorRegularData>> GetPagedListAsync(GetDoctorRegularInput input)
        {
            var query = from dr in (await _doctorRegularRepository.GetQueryableAsync())
                        join dpt in (await _departmentRepository.GetQueryableAsync()) on dr.DepartmentId equals dpt.Id
                        select new DoctorRegularData
                        {
                            Id = dr.Id,
                            DoctorId = dr.DoctorId,
                            DoctorName = dr.DoctorName,
                            DoctorDepartmentId = dr.DoctorDepartmentId,
                            DoctorDepartmentName = dr.DoctorDepartmentName,
                            IsActived = dr.IsActived,
                            DepartmentId = dpt.Id,
                            DepartmentName = dpt.Name,
                        };
            var list = await query
                .OrderBy(nameof(DoctorRegular.Id))
                .PageBy(input.SkipCount, input.Size)
                .ToListAsync();

            var totalCount = query.LongCount();
            var result = new PagedResultDto<DoctorRegularData>(totalCount, list);

            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">修改的记录id</param>
        /// <param name="input">修改的实体</param>
        /// <returns></returns>
        public async Task<DoctorRegularData> UpdateAsync(Guid id, DoctorRegularUpdate input)
        {
            // 验证医生id、医生可用状态不在当前服务验证
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == input.DepartmentId);
            if (department is null)
            {
                // 科室不存在
                throw new DepartmentNotExistsException();
            }

            var record = (await _doctorRegularRepository.GetAsync(id))
                .SetDoctor(input.DoctorId, input.DoctorName, input.DoctorDepartmentId.Value, input.DoctorDepartmentName)
                .SetDepartment(department.Id)
                .SetActive(input.IsActived);

            await _doctorRegularManager.UpdateAsync(record);
            // 保存修改
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var output = await GetAsync(record.Id);

            return output;
        }

    }
}
