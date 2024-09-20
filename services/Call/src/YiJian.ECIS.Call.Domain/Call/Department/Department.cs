namespace YiJian.ECIS.Call.Domain
{
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using Volo.Abp;
    using Volo.Abp.Domain.Entities.Auditing;

    public class Department : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; private set; }

        /// <summary>
        /// 诊室
        /// </summary>
        public ICollection<ConsultingRoom> ConsultingRooms { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">名称</param>
        /// <param name="code">代码</param>
        /// <param name="isActive">使用状态</param>
        public Department([NotNull] Guid id, [NotNull] string name, [NotNull] string code, bool isActive)
        {
            Id = Check.NotNull(id, nameof(Id));
            Name = Check.NotNull(name, nameof(Name));
            Code = Check.NotNull(code, nameof(Code));
            IsActived = isActive;

            AddLocalEvent(new DepartmentCreatedEvent
            {
                Id = id,
                Name = name,
                Code = code
            });
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code">代码</param>
        /// <param name="isActive"></param>
        public void Edit([NotNull] string name, [NotNull] string code, bool isActive)
        {
            Name = Check.NotNull(name, nameof(Name));
            Code = Check.NotNull(code, nameof(Code));
            IsActived = isActive;
        }

        private Department()
        {
        }
    }
}
