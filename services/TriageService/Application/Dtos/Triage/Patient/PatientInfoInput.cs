using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 查询建档患者信息输入项
    /// </summary>
    public class PatientInfoInput
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string PatientId { get; set; }
        
        /// <summary>
        /// 患者姓名 
        /// </summary>
        public string PatientName { get; set; }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }
        /// <summary>
        /// 来院方式
        /// </summary>
        public string ToHospitalWayCode { get; set; }
        /// <summary>
        /// 分诊级别
        /// </summary>
        public string ActTriageLevel { get; set; }


        


    }
}