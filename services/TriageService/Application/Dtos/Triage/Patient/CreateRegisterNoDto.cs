using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 挂号Dto
    /// </summary>
    public class CreateRegisterNoDto
    {

        /// <summary>
        /// 患者分诊记录Id
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }
        
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptId { get; set; }
        
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }
        
        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 是否医保（1：是  0：否）
        /// </summary>
        public string Insurance { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 日程表ID
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 午别 （1：上午，2：下午，3：晚上,4：全天）
        /// </summary>
        public string NoonId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime? OrderTime { get; set; }

        /// <summary>
        /// 患者类别
        /// </summary>
        public string PatientClass { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 挂号级别代码
        /// </summary>
        public string ReglevlCode { get; set; }

        /// <summary>
        /// 挂号级别名称
        /// </summary>
        public string ReglevlName { get; set; }

        /// <summary>
        /// 看诊日期
        /// </summary>
        public DateTime? SeeDate { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisitNum { get; set; }
    }
}