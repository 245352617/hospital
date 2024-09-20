using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【医生变动】查询 DTO
    /// direction：output
    /// </summary>
    [Serializable]
    public class DoctorRegularData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 医生id
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 院内科室（医生所属科室）id
        /// </summary>
        public Guid DoctorDepartmentId { get; set; }

        /// <summary>
        /// 院内科室（医生所属科室）名称
        /// </summary>
        public string DoctorDepartmentName { get; set; }

        /// <summary>
        /// 对应急诊科室id
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 对应急诊科室名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; set; }
    }
}
