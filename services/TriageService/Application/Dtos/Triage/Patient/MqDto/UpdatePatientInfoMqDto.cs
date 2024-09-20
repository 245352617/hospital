using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 更新患者信息推送队列Dto
    /// </summary>
    public class UpdatePatientInfoMqDto
    {
        /// <summary>
        /// 患者分诊基本信息Id
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
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// 是否流感
        /// </summary>
        public bool FluFlag { get; set; }
        
        /// <summary>
        /// 是否分诊错误
        /// </summary>
        public bool TriageErrorFlag { get; set; }
        
        /// <summary>
        /// 是否咳嗽
        /// </summary>
        public bool CoughFlag { get; set; }

        /// <summary>
        /// 是否咽痛
        /// </summary>
        public bool ChestFlag { get; set; }
        
        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(50)]
        public string TypeOfVisitCode { get; set; }
        
        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(100)]
        public string TypeOfVisitName { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string AllergyHistory { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
        
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [MaxLength(50)]
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [MaxLength(20)]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string Narration { get; set; }

        /// <summary>
        /// 绿色通道编码
        /// </summary>
        public string GreenRoadCode { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        public string GreenRoadName { get; set; }
    }
}