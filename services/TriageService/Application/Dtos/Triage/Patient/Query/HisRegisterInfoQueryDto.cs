using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// His挂号信息查询Dto
    /// </summary>
    public class HisRegisterInfoQueryDto
    {
        /// <summary>
        /// 病患ID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string visitNo { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string regSerialNo { get; set; }

        /// <summary>
        /// 患者证件号码
        /// </summary>
        public string patientIdNo { get; set; }

        /// <summary>
        /// 挂号开始时间
        /// </summary>
        public string beginTime { get; set; }
        /// <summary>
        /// 挂号结束时间  
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 患者名称
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilePhone { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string certificateType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string certificateNum { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string icCardId { get; set; }
    }
}