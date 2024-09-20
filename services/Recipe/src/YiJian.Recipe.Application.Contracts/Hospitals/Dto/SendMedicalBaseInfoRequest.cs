namespace YiJian.Hospitals.Dto
{
    public class SendMedicalBaseInfoRequest
    {
        private SendMedicalBaseInfoRequest()
        {

        }

        public SendMedicalBaseInfoRequest(string visSerialNo, string patientId, string doctorCode, string doctorName, string deptCode)
        {
            VisSerialNo = visSerialNo;
            PatientId = patientId;
            DoctorCode = doctorCode;
            DeptCode = deptCode;
            DoctorName = doctorName;
        }

        /// <summary>
        /// 就诊流水号
        /// <![CDATA[
        /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
        /// ]]>
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称 
        /// <![CDATA[
        /// 一级科室名称:4.4.1 科室字典（his提供） deptName
        /// ]]>
        /// </summary> 
        public string DeptName { get; set; }

    }

}
