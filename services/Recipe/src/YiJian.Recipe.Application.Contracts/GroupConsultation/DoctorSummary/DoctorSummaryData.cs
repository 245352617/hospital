
namespace YiJian.Recipe
{
    using System;

    /// <summary>
    /// 会诊纪要医生 读取输出
    /// </summary>
    [Serializable]
    public class DoctorSummaryData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 会诊id
        /// </summary>
        public Guid GroupConsultationId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 报道时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 医生职称
        /// </summary>
        public string DoctorTitle { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion { get; set; }

    }
}