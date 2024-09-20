using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【诊室固定】领域实体
    /// </summary>
    public class ConsultingRoomRegular : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 急诊科室id
        /// </summary>
        public virtual Guid DepartmentId { get; private set; }

        /// <summary>
        /// 急诊诊室id
        /// </summary>
        public virtual Guid ConsultingRoomId { get; private set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public virtual bool IsActived { get; private set; }

        /// <summary>
        /// 隐藏无参构造方法
        /// </summary>
        private ConsultingRoomRegular()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="departmentId">科室ID</param>
        /// <param name="consultingRoomId">诊室id</param>
        /// <param name="isActived">是否使用</param>
        public ConsultingRoomRegular([NotNull] Guid id, [NotNull] Guid departmentId, [NotNull] Guid consultingRoomId, bool isActived)
        {
            Id = Check.NotNull(id, nameof(Id));
            DepartmentId = Check.NotNull(departmentId, nameof(departmentId));
            ConsultingRoomId = Check.NotNull(consultingRoomId, nameof(ConsultingRoomId));
            IsActived = isActived;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="departmentId">科室ID</param>
        /// <param name="consultingRoomId">诊室id</param>
        /// <param name="isActived">是否使用</param>
        public void Edit([NotNull] Guid departmentId, [NotNull] Guid consultingRoomId, bool isActived)
        {
            DepartmentId = Check.NotNull(departmentId, nameof(departmentId));
            ConsultingRoomId = Check.NotNull(consultingRoomId, nameof(ConsultingRoomId));
            IsActived = isActived;
        }
    }
}
