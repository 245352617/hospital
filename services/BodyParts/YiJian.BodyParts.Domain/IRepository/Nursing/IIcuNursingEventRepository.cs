using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:护理记录表
    /// </summary>
    public interface IIcuNursingEventRepository : IRepository<IcuNursingEvent, Guid>, IBaseRepository<IcuNursingEvent, Guid>
    {
        /// <summary>
        /// 根据时间获取护理记录列表
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<IcuNursingEvent>> GetIcuNursingEvents(string PI_ID, DateTime? startTime=null, DateTime? endTime=null);
    }
}
