using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Domain;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


public class ConsultingRoomRepository : MasterDataRepositoryBase<ConsultingRoom, Guid>, IConsultingRoomRepository
{
    public ConsultingRoomRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
