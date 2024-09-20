using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Handover
{
    public class DoctorPatients :AuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// 医生交班id
        /// </summary>
        [Description("医生交班id")]
        public Guid DoctorHandoverId { get;private set; }

        /// <summary>
        /// tiage分诊患者id
        /// </summary>
        [Description("tiage分诊患者id")]
        public Guid PI_ID { get;private set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [Description("患者id")]
        [Column(TypeName = "nvarchar(50)")]
        public string PatientId { get;private set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Description("就诊号")]
        public int? VisitNo { get;private set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        [Description("患者姓名")]
        public string PatientName { get;private set; }

        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(20)]
        [Description("性别")]
        public string Sex { get;private set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        [StringLength(20)]
        public string  SexName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        [Description("年龄")]
        public string Age { get;private set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        [Description("分诊级别")]
        public string TriageLevelName { get;private set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("诊断")]
        public string DiagnoseName { get;private set; }

        /// <summary>
        /// 床号
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        [Description("床号")]
        public string Bed { get;private set; }

        /// <summary>
        /// 交班内容
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("交班内容")]
        public string Content { get;private set; }

        /// <summary>
        /// 检验
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("检验")]
        public string Test { get;private set; }

        /// <summary>
        /// 检查
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("检查")]
        public string Inspect { get;private set; }

        /// <summary>
        /// 电子病历
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("电子病历")]
        public string Emr { get;private set; }

        /// <summary>
        /// 出入量
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("出入量")]
        public string InOutVolume { get;private set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("生命体征")]
        public string VitalSigns { get;private set; }

        /// <summary>
        /// 药物
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        [Description("药物")]
        public string Medicine { get;private set; }
        /// <summary>
        /// 交班状态
        /// </summary>
        [Description("交班状态")]
        public bool Status { get; private set; }
        #region constructor

        public DoctorPatients(Guid id, Guid doctorHandoverId, Guid pIID, string patientId, int? visitNo,
            string patientName, string sex,string sexName,  string age, string triageLevel, string diagnose, string bed, string content,
            string test, string inspect, string emr, string inOutVolume, string vitalSigns, string medicine, bool status) :base(id)
        {

            DoctorHandoverId = doctorHandoverId;
            Modify(pIID, patientId, visitNo, patientName, sex, sexName,age, triageLevel, diagnose, bed, content, test, inspect, emr, inOutVolume, vitalSigns, medicine,status);
        }
        #endregion

        #region Modify

        public void Modify(Guid pIID, string patientId, int? visitNo, string patientName, string sex,string sexName,  string age, string triageLevelName, string diagnoseName, string bed, string content, string test, string inspect, string emr, string inOutVolume, string vitalSigns, string medicine, bool status)
        {   
            PI_ID = pIID;
            PatientId = patientId;
            
            VisitNo = visitNo;
            
            PatientName = patientName;
            
            Sex = sex;
            SexName = sexName;
            Age = age;
            
            TriageLevelName = triageLevelName;
            
            DiagnoseName = diagnoseName;
            
            Bed = bed;
            
            Content = content;
            
            Test = test;
            
            Inspect = inspect;
            
            Emr = emr;
            
            InOutVolume = inOutVolume;
            
            VitalSigns = vitalSigns;
            
            Medicine = medicine;
            Status = status;

        }
        #endregion

        #region constructor
        private DoctorPatients(bool status)
        {
            Status = status;
            // for EFCore
        }
        #endregion
    }
}
