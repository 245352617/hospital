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
    /// 【诊室固定】领域服务
    /// </summary>
    public class ConsultingRoomRegularManager : DomainService
    {
        private readonly IConsultingRoomRegularRepository _consultingRoomRegularRepository;

        public ConsultingRoomRegularManager(IConsultingRoomRegularRepository consultingRoomRegulars)
        {
            this._consultingRoomRegularRepository = consultingRoomRegulars;
        }

        /// <summary>
        /// 创建【诊室固定】规则
        /// </summary>
        /// <param name="consultingRoomId"></param>
        /// <param name="departmentId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        /// <exception cref="ConsultingRoomAlreadyExistsException"></exception>
        public async Task<ConsultingRoomRegular> CreateAsync([NotNull] Guid consultingRoomId, [NotNull] Guid departmentId, bool isActive)
        {
            Check.NotNull(consultingRoomId, nameof(ConsultingRoomRegular.ConsultingRoomId));

            var existingItem = await _consultingRoomRegularRepository.FirstOrDefaultAsync(p => p.ConsultingRoomId == consultingRoomId);
            if (existingItem != null)
            {// 诊室规则已存在
                throw new EcisBusinessException(CallErrorCodes.ConsultingRoomRegularExists);
            }

            return await _consultingRoomRegularRepository.InsertAsync(
                new ConsultingRoomRegular(
                    GuidGenerator.Create(),
                    departmentId,
                    consultingRoomId,
                    isActive
                )
            );
        }

        /// <summary>
        /// 修改【诊室固定】规则
        /// </summary>
        /// <param name="consultingRoomRegular">修改的实体</param>
        /// <returns></returns>
        /// <exception cref="ConsultingRoomAlreadyExistsException"></exception>
        public async Task<ConsultingRoomRegular> UpdateAsync([NotNull] ConsultingRoomRegular consultingRoomRegular)
        {
            Check.NotNull(consultingRoomRegular?.Id, nameof(ConsultingRoomRegular.Id));
            Check.NotNull(consultingRoomRegular?.ConsultingRoomId, nameof(ConsultingRoomRegular.ConsultingRoomId));

            var existingItem = await _consultingRoomRegularRepository
                .FirstOrDefaultAsync(p => p.Id != consultingRoomRegular.Id && p.ConsultingRoomId == consultingRoomRegular.ConsultingRoomId);
            if (existingItem != null)
            {// 诊室规则已存在
                throw new EcisBusinessException(CallErrorCodes.ConsultingRoomRegularExists);
            }

            return await _consultingRoomRegularRepository.UpdateAsync(consultingRoomRegular);
        }

        /// <summary>
        /// 删除【诊室固定】规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync([NotNull] Guid id)
        {
            var existingItem = await _consultingRoomRegularRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (existingItem == null)
            {// 诊室固定规则不存在
                throw new EcisBusinessException(CallErrorCodes.ConsultingRoomRegularNotExists);
            }

            await _consultingRoomRegularRepository.DeleteAsync(id);
        }
    }
}
