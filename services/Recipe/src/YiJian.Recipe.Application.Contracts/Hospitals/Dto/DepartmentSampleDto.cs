using System;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 执行科室实体
    /// </summary>
    public class DepartmentSampleDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

    }
}
