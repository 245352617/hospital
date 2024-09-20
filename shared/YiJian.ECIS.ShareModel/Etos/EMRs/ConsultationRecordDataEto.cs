namespace YiJian.ECIS.ShareModel.Etos.EMRs
{

    /// <summary>
    /// 会诊记录数据
    /// </summary>
    public class ConsultationRecordDataEto
    {
        /// <summary>
        /// 患者相关信息
        /// </summary>
        public PatientInfoEto PatientInfo { get; set; }

        /// <summary>
        /// 医疗相关信息
        /// </summary>
        public MedicalInfoEto MedicalInfo { get; set; }

        /// <summary>
        /// 邀请科室
        /// </summary>
        public Dictionary<string, string> InviteDepartmentDic { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 获取绑定的数字字典
        /// </summary>
        public Dictionary<string, Dictionary<string, object>> GetMap()
        {
            var dic = new Dictionary<string, Dictionary<string, object>>();
            dic.Add("patientInfo", PatientInfo.GetPatientInfoMap());
            dic.Add("medicalInfo", MedicalInfo.GetMedicalInfoMap());
            return dic;
        }

    }
}