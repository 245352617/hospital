using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Entities;

namespace YiJian.DoctorsAdvices.Entities
{
    /// <summary>
    /// 医嘱映射表，只映射一个自增的Id，目的是应付各种医院的主键问题
    /// </summary>
    [Comment("医嘱映射表,只映射一个自增的Id")]
    public class DoctorsAdviceMap : Entity<long>
    {
        public DoctorsAdviceMap(Guid doctorsAdviceId)
        {
            DoctorsAdviceId = doctorsAdviceId;
        }

        /// <summary>
        /// DoctorsAdvice 表的主键
        /// </summary>
        public Guid DoctorsAdviceId { get; set; }
    }
}
