using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊患者信息Dto
    /// </summary>
    public class TriagePatientInfoDto
    {
        /// <summary>
        /// 分诊患者基本信息表Id
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 是否挂号  1:已建档 0:未建档
        /// </summary>
        public int IsRegister { get; set; }
        
        /// <summary>
        /// 是否开通绿通  1:已开通 0:未开通
        /// </summary>
        public int IsGreenRode { get; set; }
        
        /// <summary>
        /// 是否群伤   1:是 0:不是
        /// </summary>
        public int IsGroupInjury { get; set; }

        /// <summary>
        /// 是否已分诊, 0:否  1:是
        /// </summary>
        public int IsTriaged { get; set; }
        
        /// <summary>
        /// 分诊等级
        /// </summary>
        public string TriageLevel { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        public string ToHospitalWay { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday  { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
        
        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactsPhone { get; set; }

    }
}