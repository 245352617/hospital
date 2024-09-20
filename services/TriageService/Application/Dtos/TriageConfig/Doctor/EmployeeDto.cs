using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 护士信息
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 护士编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 护士姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 医生姓名首字母拼音
        /// </summary>
        public string NamePy { get; set; }
    }
}
