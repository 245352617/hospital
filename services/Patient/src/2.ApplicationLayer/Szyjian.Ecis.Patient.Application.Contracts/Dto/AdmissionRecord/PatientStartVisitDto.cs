using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 患者接诊或入科Dto
    /// </summary>
    public class PatientStartVisitDto
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int AR_ID { get; set; }

        /// <summary>
        /// 分诊患者信息Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        public string Bed { get; set; }

        /// <summary>
        /// 责任医生编码
        /// </summary>
        [MaxLength(20)]
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 责任医生名称
        /// </summary>
        [MaxLength(50)]
        public string DutyDoctorName { get; set; }

        /// <summary>
        /// 责任护士编码
        /// </summary>
        [MaxLength(20)]
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        [MaxLength(50)]
        public string DutyNurseName { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

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
        /// 分诊科室编码（挂号科室）
        /// </summary>
        [MaxLength(50)]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [MaxLength(50)]
        public string TriageDeptName { get; set; }

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
        /// 就诊区域编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 就诊区域
        /// </summary>
        public string AreaName { get; set; }

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