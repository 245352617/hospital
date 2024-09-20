namespace YiJian.ECIS.Call.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Domain.Services;

    /// <summary>
    /// 科室服务
    /// </summary>
    public class DepartmentManager : DomainService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IConsultingRoomRepository _consultingRoomRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository, IConsultingRoomRepository consultingRoomRepository)
        {
            _departmentRepository = departmentRepository;
            _consultingRoomRepository = consultingRoomRepository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await this._departmentRepository.GetListAsync();
        }
        /// <summary>
        /// 创建科室
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        /// <exception cref="ConsultingRoomAlreadyExistsException"></exception>
        public async Task<Department> CreateAsync(string name, string code, bool isActive)
        {
            var existingDepartment = await _departmentRepository.FirstOrDefaultAsync(p => p.Name == name);

            if (existingDepartment != null)
            {
                throw new DepartmentNameAlreadyExistsException(name);
            }

            return await _departmentRepository.InsertAsync(
                new Department(
                    GuidGenerator.Create(),
                    name,
                    code,
                    isActive
                )
            );
        }

        /// <summary>
        /// 查询科室，不存在时创建科室
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        /// <exception cref="ConsultingRoomAlreadyExistsException"></exception>
        public async Task<Department> GetOrCreateAsync(string name, string code, bool isActive)
        {
            var existingDepartment = await _departmentRepository.FirstOrDefaultAsync(p => p.Name == name);

            if (existingDepartment != null)
            {
                throw new DepartmentNameAlreadyExistsException(name);
            }

            return await _departmentRepository.InsertAsync(
                new Department(
                    GuidGenerator.Create(),
                    name,
                    code,
                    isActive
                )
            );
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DepartmentConsultingRoomExistsException"></exception>
        public async Task DeleteAsync(Guid id)
        {
            if (await _consultingRoomRepository.AnyAsync(c => c.Id == id))
            {
                throw new DepartmentConsultingRoomExistsException();
            }
            await _departmentRepository.DeleteAsync(id);
        }

    }
}
