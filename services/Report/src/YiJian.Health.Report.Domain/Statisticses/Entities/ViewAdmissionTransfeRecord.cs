using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 入院患者流转记录视图
    /// </summary>
    public class ViewAdmissionTransfeRecord
    {
        /// <summary>
        /// 就诊号
        /// </summary>
        [Comment("就诊号")]
        [StringLength(50)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [Comment("就诊流水号")]
        [StringLength(50)]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者ID")]
        [StringLength(50)]
        public string PatientID { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Comment("性别")]
        [StringLength(4)]
        public string SexName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Comment("年龄")]
        [StringLength(20)]
        public string Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Comment("出生日期")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Comment("身份证")]
        [StringLength(20)]
        public string IDNo { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [Comment("联系方式")]
        [StringLength(100)]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 接诊医生
        /// </summary>
        [Comment("接诊医生")]
        [StringLength(50)]
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 接诊医生
        /// </summary>
        [Comment("接诊医生")]
        [StringLength(50)]
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary>
        [Comment("接诊护士")]
        [StringLength(50)]
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary>
        [Comment("接诊护士")]
        [StringLength(50)]
        public string DutyNurseName { get; set; }

        /// <summary>
        /// 接诊时间
        /// </summary> 
        [Comment("接诊时间")]
        public DateTime VisitDate { get; set; }

        /// <summary>
        /// 分诊等级
        /// </summary>
        [Comment("分诊等级")]
        [StringLength(50)]
        public string TriageLevel { get; set; }

        /// <summary>
        /// 分诊等级名称
        /// </summary>
        [Comment("分诊等级名称")]
        [StringLength(50)]
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        [Comment("出科时间")]
        [StringLength(50)]
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [NotMapped]
        public int Row { get; set; }


        /// <summary>
        /// 流转类型
        /// </summary>
        [Comment("流转类型")]
        [StringLength(50)]
        public string TransferType { get; set; }

        /// <summary>
        /// 流转时间
        /// </summary>
        [Comment("流转时间")]
        public DateTime? TransferTime { get; set; }

        /// <summary>
        /// 滞留时长-分钟
        /// </summary>
        [Comment("滞留时长-分钟")]
        public int ResidenceTime { get; set; }

        /// <summary>
        /// 流转源区域
        /// </summary>
        [Comment("流转源区域")]
        [StringLength(50)]
        public string FromAreaCode { get; set; }

        /// <summary>
        /// 流转目标区域
        /// </summary>
        [Comment("流转目标区域")]
        [StringLength(50)]
        public string ToAreaCode { get; set; }

        /// <summary>
        /// 流转目标区域名称
        /// </summary>
        [Comment("流转目标区域名称")]
        [StringLength(50)]
        public string ToArea { get; set; }

        /// <summary>
        /// 流转原因
        /// </summary>
        [Comment("流转原因")]
        [StringLength(250)]
        public string TransferReason { get; set; }

    }
}