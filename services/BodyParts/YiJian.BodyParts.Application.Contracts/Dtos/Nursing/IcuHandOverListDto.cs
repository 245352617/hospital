using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 交班患者信息列表
    /// </summary>
    public class IcuHandOverListDto
    {
        /// <summary>
        /// 患者流水号
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 是否交班，0：未交班，1：已交班
        /// </summary>
        public bool IsHandOver { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime InDeptTime { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }
    }
}
