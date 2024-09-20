using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病人信息对接导入实体
    /// </summary>
    public class PatientInsertDto
    {
        /// <summary>
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        public string ArchiveId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>

        public string MarriageStatus { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }


        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdentityCard { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        public string NewAddress { get; set; }


        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string LinkPhoneNum { get; set; }

        /// <summary>
        /// 联系人地址
        /// </summary>
        public string LinkAddress { get; set; }



        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime InDeptTime { get; set; }

        /// <summary>
        /// 入科诊断
        /// </summary>
        public string Indiagnosis { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        public string WardCode { get; set; }

        /// <summary>
        /// 主管医生
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 主管医生名称
        /// </summary>
        public string DoctorName { get; set; }


        /// <summary>
        /// 专科医生ID
        /// </summary>
        public string HospitalDoctorId { get; set; }

        /// <summary>
        /// 专科医生名称
        /// </summary>
        public string HospitalDoctorName { get; set; }

        /// <summary>
        /// 责任护士
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 出科诊断
        /// </summary>
        public string Outdiagnosis { get; set; }

        /// <summary>
        /// 住院次数
        /// </summary>
        public int VisitTimes { get; set; }

        /// <summary>
        /// 入院時間
        /// </summary>
        public DateTime? InHosTime { get; set; }

        /// <summary>
        /// 出院時間
        /// </summary>
        public DateTime? OutHosTime { get; set; }

        /// <summary>
        /// 护理级别（1：一级护理，2：二级护理，3：三级护理，4.特级护理）
        /// </summary>
        public int? NurseGrade { get; set; }

        /// <summary>
        /// 危重程度（0：其他，1：病危，2：病重）
        /// </summary>
        public int? CriticaDegree { get; set; }

        /// <summary>
        /// 入科来源（0：其他， 1:产房，2：产科，3：门诊，4：急诊科，5：手术室，6：外院转入）
        /// </summary>
        public int? InSource { get; set; }
    }
}
