using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 就诊区医生选择的科室和诊室
    /// </summary>
    [Table(Name = "Pat_DoctorDept")]
    public class DoctorDept : Entity<Guid>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorCode"></param>
        /// <param name="dept"></param>
        /// <param name="room"></param>
        public DoctorDept(Guid id, string doctorCode, string dept, string room)
        {
            Id = id;
            DoctorCode = doctorCode;
            Dept = dept;
            Room = room;
        }

        /// <summary>
        /// 医生编码
        /// </summary>
        [Required, StringLength(20)]
        [Column(Position = 2)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        [Required, StringLength(1000)]
        [Column(Position = 3)]
        public string Dept { get; set; }

        /// <summary>
        /// 诊室
        /// </summary>
        [Required, StringLength(1000)]
        [Column(Position = 4)]
        public string Room { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

    }
}