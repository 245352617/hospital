using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 患者信息基类
    /// </summary>
    public class PatientBasicInformationDto
    {
        /// <summary> 
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 病人Id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime InDeptTime { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public string InDeptTime2
        {
            get
            {
                if (InDeptTime != DateTime.MinValue)
                {
                    return InDeptTime.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 入科诊断
        /// </summary>
        public string Indiagnosis { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public string OutDeptTime2
        {
            get
            {
                if (OutDeptTime != null)
                {
                    return OutDeptTime.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
