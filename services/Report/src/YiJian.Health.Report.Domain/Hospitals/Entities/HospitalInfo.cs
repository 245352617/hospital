using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Health.Report.Hospitals.Entities
{
    /// <summary>
    /// 医院的基础信息
    /// </summary>
    [Comment("医院的基础信息")]
    public class HospitalInfo : FullAuditedAggregateRoot<Guid>
    {
        private HospitalInfo()
        {

        }

        public HospitalInfo(Guid id,
            [NotNull] string name,
            [CanBeNull] string logo,
            [CanBeNull] string hospitalLevel,
            [CanBeNull] string address)
        {
            Id = id;
            Name = Check.NotNullOrEmpty(name, nameof(name), maxLength: 100);
            Logo = Check.Length(logo, nameof(logo), maxLength: 4000);
            HospitalLevel = Check.Length(hospitalLevel, nameof(hospitalLevel), maxLength: 50);
            Address = Check.Length(address, nameof(address), maxLength: 200);
        }

        /// <summary>
        /// 医院的名称
        /// </summary>
        [Comment("医院的名称")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 医院徽标
        /// </summary>
        [Comment("医院徽标")]
        [StringLength(4000)]
        public string Logo { get; set; }

        /// <summary>
        /// 医院评级(级别，如：三级甲等)
        /// </summary>
        [Comment("医院评级(级别，如：三级甲等)")]
        [StringLength(50)]
        public string HospitalLevel { get; set; }

        /// <summary>
        /// 医院的地址
        /// </summary>
        [Comment("医院的地址")]
        [StringLength(200)]
        public string Address { get; set; }

        public void Update(
           [NotNull] string name,
           [CanBeNull] string logo,
           [CanBeNull] string hospitalLevel,
           [CanBeNull] string address)
        {
            Name = Check.NotNullOrEmpty(name, nameof(name), maxLength: 100);
            Logo = Check.Length(logo, nameof(logo), maxLength: 4000);
            HospitalLevel = Check.Length(hospitalLevel, nameof(hospitalLevel), maxLength: 50);
            Address = Check.Length(address, nameof(address), maxLength: 200);
        }

    }
}
