using System;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 交接班请求参数
    /// </summary>
    public class ShiftChangeRequestDto
    {
        /// <summary>
        /// 患者编号
        /// </summary> 
        public string PatientNo { get; set; }

        /// <summary>
        /// 开始查询
        /// </summary>
        public DateTime? BeginTime { get;set;}

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

    }

}
