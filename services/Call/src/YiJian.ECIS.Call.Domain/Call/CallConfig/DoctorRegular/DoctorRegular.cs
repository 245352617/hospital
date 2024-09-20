using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【医生变动】领域实体
    /// </summary>
    public class DoctorRegular : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 医生id
        /// </summary>
        [Required]
        public virtual string DoctorId { get; private set; }

        /// <summary>
        /// 医生名称（冗余存储）
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 医生所属科室id（冗余存储）
        /// </summary>
        [Required]
        public Guid DoctorDepartmentId { get; set; }

        /// <summary>
        /// 医生所属科室名称（冗余存储）
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string DoctorDepartmentName { get; set; }

        /// <summary>
        /// 对应急诊科室id
        /// </summary>
        [Required]
        public virtual Guid DepartmentId { get; private set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [Required]
        public virtual bool IsActived { get; private set; }

        /// <summary>
        /// 隐藏无参构造方法
        /// </summary>
        private DoctorRegular()
        {
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="id"></param>
        public DoctorRegular([NotNull] Guid id)
        {
            Check.NotNull(id, nameof(Id));

            this.Id = id;
        }

        /// <summary>
        /// 设置科室id
        /// </summary>
        /// <param name="departmentId">对应急诊科室id</param>
        /// <returns></returns>
        public DoctorRegular SetDepartment([NotNull] Guid departmentId)
        {
            this.DepartmentId = Check.NotNull(departmentId, nameof(DepartmentId));

            return this;
        }

        /// <summary>
        /// 修改医生信息
        /// </summary>
        /// <param name="doctorId">医生id</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="doctorDepartmentId">医生所属科室id</param>
        /// <param name="doctorDepartmentName">医生所属科室名称</param>
        public DoctorRegular SetDoctor([NotNull] string doctorId, [NotNull] string doctorName, [NotNull] Guid doctorDepartmentId, [NotNull] string doctorDepartmentName)
        {
            this.DoctorId = Check.NotNull(doctorId, nameof(DoctorId));
            this.DoctorName = Check.NotNull(doctorName, nameof(DoctorName), maxLength: 50);
            this.DoctorDepartmentId = Check.NotNull(doctorDepartmentId, nameof(DoctorDepartmentId));
            this.DoctorDepartmentName = Check.NotNull(doctorDepartmentName, nameof(DoctorDepartmentName), maxLength: 50);

            return this;
        }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <returns></returns>
        public DoctorRegular SetActive(bool active)
        {
            this.IsActived = active;
            return this;
        }
    }
}
