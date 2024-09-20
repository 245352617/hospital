using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱请求的参数
    /// </summary>
    public class DoctorsAdviceRequestDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary> 
        [Required(ErrorMessage = "系统标识必填，0=急诊，1=院前")]
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary> 
        [Required(ErrorMessage = "患者唯一标识必填")]
        public Guid PIID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        [StringLength(20, ErrorMessage = "患者ID长度在20个字符内")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        //[StringLength(30, ErrorMessage = "患者名称长度在30个字符内")]
        public string PatientName { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary> 
        [Required(ErrorMessage = "申请医生编码必填"), StringLength(20, ErrorMessage = "申请医生编码长度在20个字符内")]
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary> 
        [Required(ErrorMessage = "申请医生必填"), StringLength(50, ErrorMessage = "申请医生长度在50个字符内")]
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary> 
        [StringLength(50, ErrorMessage = "申请科室编码长度在20个字符内")]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary> 
        [StringLength(50, ErrorMessage = "申请科室长度在50个字符内")]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 管培生代码
        /// </summary> 
        [StringLength(20, ErrorMessage = "管培生代码长度在20个字符内")]
        public string TraineeCode { get; set; }

        /// <summary>
        /// 管培生名称
        /// </summary> 
        [StringLength(50, ErrorMessage = "管培生名称长度在50个字符内")]
        public string TraineeName { get; set; }

        /// <summary>
        /// 是否慢性病
        /// </summary> 
        public bool? IsChronicDisease { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>   
        public string Diagnosis { get; set; }

        /// <summary>
        /// 医嘱类型编码
        /// </summary> 
        [StringLength(20, ErrorMessage = "医嘱类型编码长度在20个字符内")]
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary> 
        [StringLength(20, ErrorMessage = "医嘱类型长度在20个字符内")]
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 医保机构编码
        /// </summary>
        [StringLength(50, ErrorMessage = "医保机构编码长度在50个字符内")]
        public string MeducalInsuranceCode { get; set; }

        /// <summary>
        /// 医保二级编码
        /// </summary>
        [StringLength(50, ErrorMessage = "医保二级编码长度在50个字符内")]
        public string YBInneCode { get; set; }

        /// <summary>
        /// 医保统一名称
        /// </summary>
        [StringLength(50, ErrorMessage = "医保二级编码长度在50个字符内")]
        public string MeducalInsuranceName { get; set; }

        /// <summary>
        /// 分方标记
        /// </summary>
        public string PrescriptionFlag { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public int SourceType { get; set; }
    }

}
