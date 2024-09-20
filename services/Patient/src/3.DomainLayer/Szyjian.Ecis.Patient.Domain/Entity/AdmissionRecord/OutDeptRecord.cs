using FreeSql.DataAnnotations;
using System;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 出科记录
    /// </summary>
    [Table(Name = "Pat_OutDeptRecord")]
    public class OutDeptRecord
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// piid
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime OutDeptTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
