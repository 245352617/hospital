using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Entities
{
    /// <summary>
    /// 疫苗记录信息
    /// </summary>
    [Comment("疫苗记录信息")]
    public class ImmunizationRecord : Entity<Guid>
    {
        public ImmunizationRecord(Guid id,
            EAcupunctureManipulation acupunctureManipulation,
            int times,
            DateTime recordTime,
            bool confirmed,
            Guid doctorAdviceId,
            int medicineId,
            string patientId)
        {
            Id = id;
            AcupunctureManipulation = acupunctureManipulation;
            Times = times;
            RecordTime = recordTime;
            Confirmed = confirmed;
            DoctorAdviceId = doctorAdviceId;
            MedicineId = medicineId;
            PatientId = patientId;
        }

        /// <summary>
        /// 针法，0=四针法，1=五针法
        /// </summary>
        [Comment("针法，0=四针法，1=五针法")]
        public EAcupunctureManipulation AcupunctureManipulation { get; set; }

        /// <summary>
        /// 接种次数（第一次，第二次，第三次...）
        /// </summary>
        [Comment("接种次数（第一次，第二次，第三次...）")]
        public int Times { get; set; }

        /// <summary>
        /// 接种的记录时间
        /// </summary>
        [Comment("接种的记录时间")]
        public DateTime RecordTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否确认的，回传给HIS之后就是确认的true，否则就是false
        /// </summary>
        [Comment("是否确认的，回传给HIS之后就是确认的true，否则就是false")]
        public bool Confirmed { get; set; }

        /// <summary>
        /// 医嘱Id，有医嘱id就可以确认开出来的药的药品id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorAdviceId { get; set; }

        /// <summary>
        /// 药品ID,HIS给过来的
        /// </summary> 
        [Comment("药品ID,HIS给过来的")]
        public int MedicineId { get; set; }

        /// <summary>
        /// 接种患者Id
        /// </summary>
        [Comment("接种患者Id")]
        [StringLength(32)]
        public string PatientId { get; set; }

    }
}
