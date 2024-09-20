using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 创建或更新病历患者信息
    /// </summary>
    public class CreateOrUpdateEmrPatientDto
    {
        /// <summary>
        /// 病历患者基本信息主键Id
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// 证件类型号码
        /// </summary>
        public string DocumentNum { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string Faber { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 是否三无人员
        /// </summary>
        public bool IsNoThree { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 电话判断(主述)
        /// </summary>
        public string NarrationName { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        public string Consciousness { get; set; }
    }
}