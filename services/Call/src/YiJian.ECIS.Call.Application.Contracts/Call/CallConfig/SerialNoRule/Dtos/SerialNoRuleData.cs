using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【排队号规则】查询 DTO
    /// direction：output
    /// </summary>
    [Serializable]
    public class SerialNoRuleData
    {
        public int Id { get; set; }

        /// <summary>
        /// 科室id
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 开头字母
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 流水号位数
        /// </summary>
        public ushort SerialLength { get; set; }
    }
}
