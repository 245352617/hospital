using Newtonsoft.Json;
using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{

    /// <summary>
    /// 用于医嘱新增提交时回传给his进行存储
    /// </summary>
    public class SendMedicalInfoRequest
    {
        private SendMedicalInfoRequest()
        {

        }

        public SendMedicalInfoRequest(string visSerialNo, string patientId, string doctorCode, string doctorName, string deptCode, string deptName)
        {
            VisSerialNo = visSerialNo;
            PatientId = patientId;
            DoctorCode = doctorCode;
            DeptName = DeptName;
            DeptCode = deptCode;
            DoctorName = doctorName;
        }

        public SendMedicalInfoRequest(SendMedicalBaseInfoRequest baseInfo)
        {
            VisSerialNo = baseInfo.VisSerialNo;
            PatientId = baseInfo.PatientId;
            DeptCode = baseInfo.DeptCode;
            DeptName = baseInfo.DeptName;
            DoctorCode = baseInfo.DoctorCode;
            DoctorName = baseInfo.DoctorName;
        }

        /// <summary>
        /// 就诊流水号
        /// <![CDATA[
        /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
        /// ]]>
        /// </summary>
        [JsonProperty("visSerialNo")]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 病人ID
        /// <![CDATA[
        /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台） patientId
        /// ]]>
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        [JsonProperty("doctorCode")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 科室编码
        /// <![CDATA[
        /// 一级科室科室编码:4.4.1 科室字典（his提供） deptCode
        /// ]]>
        /// </summary>
        [JsonProperty("deptCode")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称 
        /// <![CDATA[
        /// 一级科室名称:4.4.1 科室字典（his提供） deptName
        /// ]]>
        /// </summary>
        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        [JsonProperty("doctorName")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 处方外层节点
        /// </summary>
        [JsonProperty("drugItem")]
        public List<DrugItemRequest> DrugItem { get; set; } = new List<DrugItemRequest>();

        /// <summary>
        /// 诊疗项目外层节点
        /// </summary>
        [JsonProperty("projectlist")]
        public List<ProjectListRequest> Projectlist { get; set; } = new List<ProjectListRequest>();

        /// <summary>
        /// 添加处方外层节点
        /// </summary>
        /// <param name="items"></param>
        public void AddDrugItems(IEnumerable<DrugItemRequest> items)
        {
            if (DrugItem == null) DrugItem = new List<DrugItemRequest>();
            DrugItem.AddRange(items);
        }

        public void AddProjectItems(List<ProjectListRequest> items)
        {
            if (Projectlist == null) Projectlist = new List<ProjectListRequest>();
            Projectlist.AddRange(items);
        }

    }

}
