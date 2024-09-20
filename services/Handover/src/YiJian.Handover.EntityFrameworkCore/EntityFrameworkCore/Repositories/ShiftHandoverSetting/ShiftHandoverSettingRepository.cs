using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Handover.EntityFrameworkCore;

namespace YiJian.Handover
{
    public class ShiftHandoverSettingRepository:HandoverRepositoryBase<ShiftHandoverSetting,Guid>,IShiftHandoverSettingRepository
    {
        public ShiftHandoverSettingRepository(IDbContextProvider<HandoverDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }
      
    }
}