using System;
using System.Collections.Generic;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class HospitalApplyRecordDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Triage_PatientInfo表ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 转住院申请状态
        /// </summary>
        public HospitalApplyRecordStatus Status { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        public string MedicalCard { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InpatientNum { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 联系人地址
        /// </summary>
        public string ContactsAddress { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 申请人编码
        /// </summary>
        public string ApplyCode { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplyName { get; set; }

        /// <summary>
        /// 住院时间
        /// </summary>
        public DateTime InpatientTime { get; set; }

        /// <summary>
        /// 婚姻类型
        /// </summary>
        public string MarriageType { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNo { get; set; }

        /// <summary>
        /// 预交金
        /// </summary>
        public decimal Advance { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayWay { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 入院嘱托
        /// </summary>
        public string InHospitalEntrust { get; set; }

        /// <summary>
        /// 护理嘱托
        /// </summary>
        public string NurseEntrust { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime DateOfAdmission { get; set; }

        /// <summary>
        /// 住院科室编码
        /// </summary>
        public string InpatientDepartmentCode { get; set; }

        /// <summary>
        /// 住院科室名称
        /// </summary>
        public string InpatientDepartmentName { get; set; }

        /// <summary>
        /// 入院情况编码
        /// </summary>
        public string AdmissionCode { get; set; }
        /// <summary>
        /// 入院情况名称
        /// </summary>
        public string AdmissionName { get; set; }
        /// <summary>
        /// 患者来源编码
        /// </summary>
        public string PatientSourceCode { get; set; }
        /// <summary>
        /// 患者来源名称
        /// </summary>
        public string PatientSourceName { get; set; }
        /// <summary>
        /// 入院方式
        /// </summary>
        public string AdmissionModeCode { get; set; }
        /// <summary>
        /// 入院方式名称
        /// </summary>
        public string AdmissionModeName { get; set; }
        /// <summary>
        /// 是否十日内再入院
        /// </summary>
        public bool TenDaysAdmission { get; set; }
        /// <summary>
        /// 是否十日内再入院 字符串
        /// </summary>
        public string TenDaysAdmissionStr { get; set; }
        /// <summary>
        /// 病区收治编码
        /// </summary>
        public string AdmissionTypeCode { get; set; }

        /// <summary>
        /// 病区收治名称
        /// </summary>
        public string AdmissionTypeName { get; set; }

        /// <summary>
        /// 预收治病区编码
        /// </summary>
        public string WardCode { get; set; }

        /// <summary>
        /// 预收治病区名称
        /// </summary>
        public string WardName { get; set; }
        /// <summary>
        /// 获取数据结构
        /// </summary>
        /// <returns></returns>
        public void GetColumns<T>(List<string> colums, T tt)
        {
            Type t = tt.GetType();
            var ps = t.GetProperties();
            Array.ForEach(ps, p => { colums.Add(p.Name); });
        }

        /// <summary>
        /// 云签
        /// </summary>
        public string StampBase { get; set; }

        /// <summary>
        /// 诊疗小组序号
        /// </summary>
        public string TreatmentNo { get; set; }

        /// <summary>
        /// 诊疗小组名称
        /// </summary>
        public string TreatmentName { get; set; }

        /// <summary>
        /// 日间化疗0否 1是
        /// </summary>
        public int? IsDaytimeChemotherapy { get; set; }

        /// <summary>
        /// 内外科楼1内科楼 2外科楼
        /// </summary>
        public int? InoutFloor { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 入院方式1
        /// </summary>
        public string AdmissionModeName1 { get; set; }

        /// <summary>
        /// 门诊急诊1门诊 2急诊
        /// </summary>
        public int? SignatureType { get; set; }

        /// <summary>
        /// 1自费 2医保 3待审核
        /// </summary>
        public int? FeeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 申请医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string HospitalApplyNo { get; set; }
    }
}