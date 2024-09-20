using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientInfoChangeDto : IComparable<PatientInfoChangeDto>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 变更字段
        /// </summary>
        public string ChangeField { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }

        /// <summary>
        /// 变更前值
        /// </summary>
        public string BeforeValue { get; set; }

        /// <summary>
        /// 变更后值
        /// </summary>
        public string AfterValue { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperatedCode { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatedName { get; set; }

        public int CompareTo(PatientInfoChangeDto obj)
        {
            return CreationTime.CompareTo(obj.CreationTime);
        }
    }
}
