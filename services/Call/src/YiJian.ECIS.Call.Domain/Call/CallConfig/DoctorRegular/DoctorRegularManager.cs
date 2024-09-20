using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【医生变动】领域服务
    /// </summary>
    public class DoctorRegularManager : DomainService
    {
        private readonly IDoctorRegularRepository _doctorRegularRepository;

        public DoctorRegularManager(IDoctorRegularRepository doctorRegularRepository)
        {
            this._doctorRegularRepository = doctorRegularRepository;
        }

        /// <summary>
        /// 创建【医生变动】规则
        /// </summary>
        /// <param name="doctorRegular">医生变动规则id</param>
        /// <returns></returns>
        /// <exception cref="ConsultingRoomAlreadyExistsException"></exception>
        public async Task<DoctorRegular> CreateAsync([NotNull] DoctorRegular doctorRegular)
        {
            var existingItem = await _doctorRegularRepository.FirstOrDefaultAsync(p => p.DoctorId == doctorRegular.DoctorId);
            if (existingItem != null)
            {// 医生变动规则已存在
                throw new EcisBusinessException(CallErrorCodes.DoctorRegularExists);
            }

            return await _doctorRegularRepository.InsertAsync(doctorRegular);
        }

        /// <summary>
        /// 修改【医生变动】规则
        /// </summary>
        /// <param name="doctorRegular">修改的实体</param>
        /// <returns></returns>
        /// <exception cref="ConsultingRoomAlreadyExistsException"></exception>
        public async Task<DoctorRegular> UpdateAsync([NotNull] DoctorRegular doctorRegular)
        {
            Check.NotNull(doctorRegular?.Id, nameof(doctorRegular.Id));
            Check.NotNull(doctorRegular?.DoctorId, nameof(doctorRegular.DoctorId));
            Check.NotNull(doctorRegular?.DepartmentId, nameof(doctorRegular.DepartmentId));

            var existingItem = await _doctorRegularRepository
                .FirstOrDefaultAsync(p => p.Id != doctorRegular.Id && p.DoctorId == doctorRegular.DoctorId);
            if (existingItem != null)
            {// 医生变动规则已存在
                throw new EcisBusinessException(CallErrorCodes.DoctorRegularExists);
            }

            return await _doctorRegularRepository.UpdateAsync(doctorRegular);
        }

        /// <summary>
        /// 删除【医生变动】规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync([NotNull] Guid id)
        {
            var existingItem = await _doctorRegularRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (existingItem == null)
            {// 【医生变动】规则不存在
                throw new EcisBusinessException(CallErrorCodes.DoctorRegularNotExists);
            }

            await _doctorRegularRepository.DeleteAsync(id);
        }
    }
}
