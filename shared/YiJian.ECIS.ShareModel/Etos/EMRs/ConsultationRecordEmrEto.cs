namespace YiJian.ECIS.ShareModel.Etos.EMRs
{

    /// <summary>
    /// 会诊记录电子病历
    /// </summary>
    public class ConsultationRecordEmrEto
    {
        /// <summary>
        /// 患者唯一Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>   
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>   
        public string PatientName { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>   
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>   
        public string DoctorName { get; set; }

        /// <summary>
        /// 病历名称
        /// </summary>   
        public string Title { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>  
        public string CategoryLv1 { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>  
        public string CategoryLv2 { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary> 
        public DateTime? AdmissionTime { get; set; }

        /// <summary>
        /// 出院时间
        /// </summary> 
        public DateTime? DischargeTime { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）
        /// </summary>
        public int Classify { get; set; } = 0;

        /// <summary>
        /// 电子病历Xml文档
        /// </summary> 
        public string EmrXml { get; set; }

        /// <summary>
        /// 诊断信息
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 数据绑定
        /// </summary>
        public ConsultationRecordBindEto DataBind { get; set; }

        /// <summary>
        /// 更新邀请科室
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="doctorName"></param>
        public void ModifyDept(string deptCode, string doctorName)
        {
            DeptCode = deptCode;
            DeptName = doctorName;
        }

    }
}