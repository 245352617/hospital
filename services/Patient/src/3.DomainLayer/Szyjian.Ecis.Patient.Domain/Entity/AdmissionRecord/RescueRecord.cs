using FreeSql.DataAnnotations;
using System;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 入抢记录表
    /// </summary>
    [Table(Name = "Pat_RescueRecord")]
    public class RescueRecord
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsPrimary = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// PI_Id
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 时间点名称
        /// </summary>
        public string TimePointName { get; set; }

        /// <summary>
        /// 时间点
        /// </summary>
        public DateTime TimePoint { get; set; }

        /// <summary>
        /// 滞留时间
        /// </summary>
        public double Retention { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperateCode { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperateName { get; set; }
    }
}
