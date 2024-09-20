using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【医生变动】创建 DTO
    /// direction：input
    /// </summary>
    [Serializable]
    public class DoctorRegularCreation
    {
        /// <summary>
        /// 医生id
        /// </summary>
        [Required(ErrorMessage = "医生id必填")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称（兼容微服务设计，对医生名称进行冗余存储，由前端传入该参数）
        /// </summary>
        [Required(ErrorMessage = "医生名称必填")]
        [StringLength(25, ErrorMessage = "目录名称最大长度25字符")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 医生所属科室id（兼容微服务设计，对医生名称进行冗余存储，由前端传入该参数）
        /// </summary>
        [Required(ErrorMessage = "医生所属科室id必填")]
        public Guid? DoctorDepartmentId { get; set; }

        /// <summary>
        /// 医生所属科室名称（兼容微服务设计，对医生名称进行冗余存储，由前端传入该参数）
        /// </summary>
        [Required(ErrorMessage = "医生所属科室名称必填")]
        [StringLength(100, ErrorMessage = "目录名称最大长度100字符")]
        public string DoctorDepartmentName { get; set; }

        /// <summary>
        /// 急诊科室id
        /// </summary>
        [Required(ErrorMessage = "急诊科室id必填")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; set; }
    }
}
