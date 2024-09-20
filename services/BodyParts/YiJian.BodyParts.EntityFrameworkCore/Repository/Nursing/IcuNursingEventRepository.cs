using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:护理记录表
    /// </summary>
    public class IcuNursingEventRepository : BaseRepository<EntityFrameworkCore.DbContext, IcuNursingEvent, Guid>, IIcuNursingEventRepository
    {

        public IcuNursingEventRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 根据时间获取护理记录列表
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<IcuNursingEvent>> GetIcuNursingEvents(string PI_ID, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                var nursingEventTypeArr = new[] { EventTypeEnum.护理记录, EventTypeEnum.皮肤记录 };

                List<IcuNursingEvent> nursingEvents = await DbContext.IcuNursingEvent.AsNoTracking()
                    .Where(x => x.PI_ID == PI_ID  && x.ValidState == 1 && nursingEventTypeArr.Contains(x.EventType))
                    .WhereIf(startTime.HasValue,x=> x.NurseTime >= startTime)
                    .WhereIf(endTime.HasValue,x=> x.NurseTime <= endTime)
                    .OrderBy(x => x.NurseTime).ToListAsync();

                return nursingEvents;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
