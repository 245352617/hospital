using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:病人日志
    /// </summary>
    public class IcuPatientDiagDto
    {


        /// <summary>
        /// 
        /// </summary>
        public string Ids { get; set; }
        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        /// <example></example>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 护理医生
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 护理医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime NurseTime { get; set; }
    }
}
