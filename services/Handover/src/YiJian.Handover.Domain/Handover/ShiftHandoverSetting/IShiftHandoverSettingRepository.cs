using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Handover
{
    public interface IShiftHandoverSettingRepository:IRepository<ShiftHandoverSetting,Guid>
    {
        
    }
}