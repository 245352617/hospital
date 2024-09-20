using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 更新在科记录Dto
    /// </summary>
    public class UpdateAdmissionRecordDto
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int AR_ID { get; set; }

        /// <summary>
        /// PVID 分诊库患者基本信息表主键ID
        /// </summary>
        [Required(ErrorMessage = "分诊患者基本信息ID不能为空")]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 就诊状态 -1时不做更新
        /// </summary>
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        [MaxLength(20, ErrorMessage = "就诊区域的最大长度为20")]
        public string Bed { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 首诊医生编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "首诊医生编码的最大长度为20")]
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 首诊医生名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "首诊医生编码的最大长度为50")]
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 就诊区域编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "就诊区域编码的最大长度为20")]
        public string AreaCode { get; set; }

        /// <summary>
        /// 就诊区域名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "就诊区域名称的最大长度为50")]
        public string AreaName { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "分诊科室编码的最大长度为20")]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "分诊科室名称的最大长度为50")]
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 当前用户是否对此患者关注(默认不关注)
        /// </summary>
        public bool IsAttention { get; set; }

        /// <summary>
        /// 责任医生编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "责任医生编码的最大长度为20")]
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 责任医生名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "责任医生名称的最大长度为50")]
        public string DutyDoctorName { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "挂号医生编码最大长度为20")]
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        [MaxLength(50, ErrorMessage = "挂号医生姓名最大长度为50")]
        public string RegisterDoctorName { get; set; }

        /// <summary>
        /// 责任护士编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "责任护士编码的最大长度为20")]
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "责任护士名称的最大长度为50")]
        public string DutyNurseName { get; set; }
        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        public string IdTypeName { get; set; }

        /// <summary>
        /// 是否为患者主页发起开始就诊
        /// </summary>
        public bool IsDetailsPage { get; set; }

        /// <summary>
        /// 护理等级
        /// </summary>  
        public string Pflegestufe { get; set; }

        /// <summary>
        /// 床头贴列表 
        /// </summary>
        public List<string> BedHeadStickerList { get; set; } = new List<string>();

        /// <summary>
        /// 接诊科室编码（部分医院挂号科室是门诊科室的二级科室，物理上是同一个科室）
        /// </summary>
        [MaxLength(50)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 接诊科室编码（部分医院挂号科室是门诊科室的二级科室，物理上是同一个科室）
        /// </summary>
        [MaxLength(50)]
        public string DeptName { get; set; }

        /// <summary>
        /// 来院方式Code
        /// </summary>
        public string ToHospitalWayCode { get; set; }

        /// <summary>
        /// 来院方式名称
        /// </summary>
        public string ToHospitalWayName { get; set; }

        /// <summary>
        /// 入区方式
        /// </summary>
        public string InDeptWay { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// 是否修改记录
        /// </summary>
        public bool IsUpdate { get; set; }
    }

}