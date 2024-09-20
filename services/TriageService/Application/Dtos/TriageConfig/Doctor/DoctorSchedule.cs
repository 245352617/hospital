using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 医生排班、号源信息
    /// </summary>
    public class DoctorSchedule
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
        /// 医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 医生姓名首字母拼音
        /// </summary>
        public string DoctorNamePy { get; set; }

        /// <summary>
        /// 医生职称
        /// </summary>
        public string DoctorTitle { get; set; }

        /// <summary>
        /// 挂号类别（主任、副主任...）
        /// </summary>
        public string RegType { get; set; }

        /// <summary>
        /// 费用
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// 班次(金湾：1-上午班 2-下午班 4-全天班)
        /// </summary>
        public string WorkType { get; set; }

        /// <summary>
        /// 可用号源
        /// </summary>
        public int AvailableNum { get; set; }
    }
}
