namespace YiJian.ECIS.Call.Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Domain.Services;

    /// <summary>
    /// 诊室服务
    /// </summary>
    public class ConsultingRoomManager : DomainService
    {
        private readonly IConsultingRoomRepository _consultingRoomRepository;

        public ConsultingRoomManager(IConsultingRoomRepository consultingRoomRepository)
        {
            _consultingRoomRepository = consultingRoomRepository;
        }

        /// <summary>
        /// 创建诊室
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="ip"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        /// <exception cref="ConsultingRoomAlreadyExistsException"></exception>
        public async Task<ConsultingRoom> CreateAsync(string name, string code, string ip, bool isActive)
        {
            var existingConsultingRoom = await _consultingRoomRepository.FirstOrDefaultAsync(p => p.Code == code || p.Name == name);
            if (existingConsultingRoom != null)
            {
                throw new ConsultingRoomAlreadyExistsException(name);
            }

            return await _consultingRoomRepository.InsertAsync(
               ConsultingRoom.Create(
                    GuidGenerator.Create(),
                    name,
                    code,
                    ip,
                    isActive
                )
            );
        }
    }
}
