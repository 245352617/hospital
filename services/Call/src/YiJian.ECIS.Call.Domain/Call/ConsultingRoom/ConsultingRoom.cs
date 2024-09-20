namespace YiJian.ECIS.Call.Domain
{
    using JetBrains.Annotations;
    using System;
    using Volo.Abp;
    using Volo.Abp.Domain.Entities.Auditing;

    /// <summary>
    /// 诊室
    /// </summary>
    public class ConsultingRoom : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; private set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; private set; }

        public ConsultingRoom([NotNull] Guid id, [NotNull] string name, [NotNull] string code, string ip, bool isActive)
        {
            Id = Check.NotNull(id, nameof(Id));
            Name = Check.NotNull(name, nameof(Name));
            Code = Check.NotNull(code, nameof(Code));
            IP = ip;
            IsActived = isActive;
        }

        public static ConsultingRoom Create([NotNull] Guid id, [NotNull] string name, [NotNull] string code, string ip, bool isActive)
        {
            return new ConsultingRoom(id, name, code, ip, isActive);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="ip"></param>
        /// <param name="isActive"></param>
        public void Edit([NotNull] string name, [NotNull] string code, string ip, bool isActive)
        {
            Name = Check.NotNull(name, nameof(Name));
            Code = Check.NotNull(code, nameof(Code));
            IP = ip;
            IsActived = isActive;
        }

        private ConsultingRoom()
        {
        }
    }
}
