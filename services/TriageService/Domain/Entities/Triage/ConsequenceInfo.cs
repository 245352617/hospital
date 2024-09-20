using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊结果表
    /// </summary>
    public class ConsequenceInfo : BaseEntity<Guid>
    {
        public ConsequenceInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        [Description("院前分诊患者建档表主键Id")]
        public Guid PatientInfoId { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        [Description("分诊科室编码")]
        [StringLength(60)]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [Description("分诊科室名称")]
        [StringLength(60)]
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 科室变更Code
        /// </summary>
        [Description("科室变更Code")]
        [StringLength(60)]
        public string ChangeDept { get; set; }

        /// <summary>
        /// 科室变更名称
        /// </summary>
        [Description("科室变更名称")]
        [StringLength(60)]
        public string ChangeDeptName { get; set; }

        /// <summary>
        /// 分诊去向编码
        /// </summary>
        [Description("分诊去向编码")]
        [StringLength(60)]
        public string TriageTargetCode { get; set; }

        /// <summary>
        /// 分诊去向名称
        /// </summary>
        [Description("分诊去向名称")]
        [StringLength(60)]
        public string TriageTargetName { get; set; }

        /// <summary>
        /// 实际分诊级别Code
        /// </summary>
        [Description("实际分诊级别Code")]
        [StringLength(60)]
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 实际分诊级别名称
        /// </summary>
        [Description("实际分诊级别名称")]
        [StringLength(60)]
        public string ActTriageLevelName { get; set; }

        /// <summary>
        /// 自动分诊级别Code
        /// </summary>
        [Description("自动分诊级别Code")]
        [StringLength(60)]
        public string AutoTriageLevel { get; set; }

        /// <summary>
        /// 自动分诊级别名称
        /// </summary>
        [Description("自动分诊级别名称")]
        [StringLength(60)]
        public string AutoTriageLevelName { get; set; }

        /// <summary>
        /// 分诊级别变更Code
        /// </summary>
        [Description("分诊级别变更Code")]
        [StringLength(500)]
        public string ChangeLevel { get; set; }

        /// <summary>
        /// 分诊级别变更名称
        /// </summary>
        [Description("分诊级别变更名称")]
        [StringLength(500)]
        public string ChangeLevelName { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        [Description("医生编码")]
        [StringLength(60)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        [Description("医生姓名")]
        [StringLength(60)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        [Description("班次")]
        [StringLength(60)]
        public string WorkType { get; set; }

        /// <summary>
        /// 分诊分区代码
        /// </summary>
        [Description("分诊分区代码")]
        [StringLength(60)]
        public string TriageAreaCode { get; set; }

        /// <summary>
        /// 分诊分区
        /// </summary>
        [Description("分诊分区")]
        [StringLength(60)]
        public string TriageAreaName { get; set; }

        /// <summary>
        /// 变更分诊原因代码
        /// </summary>
        /// <value></value>
        [Description("变更就诊原因代码")]
        [StringLength(256)]
        public string ChangeTriageReasonCode { get; set; }

        /// <summary>
        /// 变更分诊原因 
        /// </summary>
        /// <value></value>
        [Description("变更就诊原因")]
        [StringLength(256)]
        public string ChangeTriageReasonName { get; set; }

        /// <summary>
        /// 变更分诊，目前在已就诊状态下修改"分诊科室"或"就诊医生"后，其值会设置为：<see langword="true"/>
        /// </summary>
        /// <value></value>
        [Description("变更就诊")]
        [StringLength(256)]
        public bool ChangeTriage { get; set; } = false;
    }
}