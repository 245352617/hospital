using System;
using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 推送患者分诊信息队列Dto
    /// </summary>
    public class PatientInfoMqEto
    {
        /// <summary>
        /// Id 不需要前端传值
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegisterNo { get; set; }
            
        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        public string RegisterDoctorName { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }


        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime? TriageTime { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 院前分诊结果信息
        /// </summary>
        public ConsequenceInfoMqDto ConsequenceInfo { get; set; }

    }
}