namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories
{
    using System;
    using Volo.Abp.EntityFrameworkCore;
    using YiJian.ECIS.Call.Domain;

    public class ConsultingRoomRepository : CallRepositoryBase<ConsultingRoom, Guid>, IConsultingRoomRepository
    {
        public ConsultingRoomRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
