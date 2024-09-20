using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Writes.Dto
{

    /// <summary>
    /// 患者的电子病历
    /// </summary>
    public class PatientEmrBaseDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 患者唯一Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>  
        [Required(ErrorMessage = "患者编号不能为空"), StringLength(32, ErrorMessage = "患者编号最大长度32字符")]
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>  
        [Required(ErrorMessage = "患者名称不能为空"), StringLength(50, ErrorMessage = "患者名称最大长度50字符")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "科室编号不能为空"), StringLength(32, ErrorMessage = "科室编号最大长度32字符")]
        public string DeptCode { get; set; }

        [Required(ErrorMessage = "科室名称不能为空"), StringLength(100, ErrorMessage = "科室名称最大长度100字符")]
        public string DeptName { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>  
        [Required(ErrorMessage = "医生编号不能为空"), StringLength(32, ErrorMessage = "医生编号最大长度32字符")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>  
        [Required(ErrorMessage = "医生名称不能为空"), StringLength(50, ErrorMessage = "医生名称最大长度50字符")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 病历名称
        /// </summary>  
        [Required(ErrorMessage = "病历名称不能为空"), StringLength(200, ErrorMessage = "病历名称最大长度200字符")]
        public string Title { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary> 
        [StringLength(32, ErrorMessage = "一级分类最大长度32字符")]
        public string CategoryLv1 { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary> 
        [StringLength(32, ErrorMessage = "二级分类最大长度32字符")]
        public string CategoryLv2 { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary> 
        public DateTime? AdmissionTime { get; set; }

        /// <summary>
        /// 出院时间
        /// </summary> 
        public DateTime? DischargeTime { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）
        /// </summary>
        public EClassify Classify { get; set; } = EClassify.EMR;

        /// <summary>
        /// 原电子病历模板Id(上一級)
        /// </summary>
        public Guid OriginalId { get; set; }

        /// <summary>
        /// 最初引入病历库的Id
        /// </summary> 
        public Guid? OriginId { get; set; }

        /// <summary>
        /// 电子病历模板目录名称编码
        /// </summary>  
        public string Code { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string SubTitle { get; set; }

    }

}
