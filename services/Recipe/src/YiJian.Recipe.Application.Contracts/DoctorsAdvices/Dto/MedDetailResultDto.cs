using System;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱信息
    /// </summary>
    public class MedDetailResultDto
    {
        /// <summary>
        /// 病人ID
        /// <![CDATA[
        /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台） patientId
        /// ]]>
        /// </summary> 
        public string PatientId { get; set; }

        /// <summary>
        /// 医嘱项目分类编码
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 科室名称 
        /// <![CDATA[
        /// 一级科室名称:4.4.1 科室字典（his提供） deptName
        /// ]]>
        /// </summary> 
        public string DeptName { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary> 
        public string DoctorName { get; set; }

        /// <summary>
        /// His识别号 对应his处方识别（C）、医技序号（Y）  可用于二维码展示等
        /// </summary>
        public string HisNumber { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 缴费
        /// </summary>
        public string IsPay { get; set; }
    }
}
