using System;
using Newtonsoft.Json;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 叫号队列 DTO
    /// </summary>
    public class QueueEto
    {
        /// <summary>
        /// 队列ID
        /// </summary>
        public string QueueId { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 诊室名称
        /// </summary>
        public string ClinicName { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 队列号
        /// </summary>
        public string QueueNo { get; set; }

        /// <summary>
        /// 排队患者名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 排队状态
        /// 0:等候
        /// 1.受理中
        /// 2.过号
        /// 3.优先（插队）
        /// 5.暂挂
        /// </summary>
        public string PatientState { get; set; }

        /// <summary>
        /// 时段类型
        /// 1：上午（0-13）
        /// 2: 下午 （13-23）
        /// </summary>
        public string TimeType { get; set; }

        /// <summary>
        /// 原科室名称
        /// </summary>
        public string UsedDeptName { get; set; }

        /// <summary>
        /// 入列时间
        /// </summary>
        public DateTime? ListingTime { get; set; }
         
        /// <summary>
        /// 病历号
        /// <![CDATA[
        /// 根据院内情况转换为就诊号码或主索引id，该字段用于就诊卡号得显示，条形码得展示。
        /// ]]>
        /// </summary>
        public string MedicalNo { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegSerialNo { get; set; }
    }
}