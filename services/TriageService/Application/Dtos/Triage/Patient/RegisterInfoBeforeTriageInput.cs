using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊前挂号（适用于 挂号 --> 分诊 的模式）
    /// </summary>
    public class RegisterInfoBeforeTriageInput
    {
        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        /// <example>000162659</example>
        [Required(ErrorMessage = "患者唯一标识不能为空")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        /// <example>叶子骏</example>
        [Required(ErrorMessage = "患者姓名不能为空")]
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        /// <example>Sex_Man</example>
        /// <example>Sex_Woman</example>
        /// <example>Sex_Unknown</example>
        [Required(ErrorMessage = "患者性别不能为空")]
        public string Sex { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        /// <example>2021-11-18</example>
        //[SwaggerSchema(Format = "date")]
        public string Birthday { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        /// <example>广东省深圳市福田区</example>
        public string Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        /// <example>叶子骏</example>
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        /// <example>13430122222</example>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        /// <example>ToHospitalWay_001</example>
        public string ToHospitalWay { get; set; }

        /// <summary>
        /// 费别（对应患者性质）
        /// </summary>
        /// <example>Faber_003</example>
        public string ChargeType { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        /// <example>441900199009140038</example>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        /// <example>Nation_HZ0</example>
        public string Nation { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        /// <example>0000016059</example>
        public string CardNo { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        /// <example>0000853713</example>
        [Required(ErrorMessage = "挂号流水号不能为空")]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        /// <example>2021-11-18 17:19:00</example>
        //[SwaggerSchema(Format = "datetime")]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        /// <example>003351</example>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        /// <example>003354</example>
        public string Operator { get; set; }

        /// <summary>
        /// 再次就诊标识（就诊次数）
        /// </summary>
        /// <example>5</example>
        public string VisitNo { get; set; }
    }
}
