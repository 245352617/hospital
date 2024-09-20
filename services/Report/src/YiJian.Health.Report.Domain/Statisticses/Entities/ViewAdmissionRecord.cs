using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 入院病人记录视图
    /// </summary>
    [Comment("入院病人记录视图")]
    public class ViewAdmissionRecord
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

    }

}
