namespace YiJian.ECIS.ShareModel.Etos.EMRs
{
    /// <summary>
    /// 医疗相关信息
    /// </summary>
    public class MedicalInfoEto
    {
        /// <summary>
        /// 会诊申请时间
        /// </summary>
        public string ConsultationApplyTime { get; set; }

        /// <summary>
        /// 会诊科室
        /// </summary>
        public string ConsultationDept { get; set; }

        /// <summary>
        /// 会诊类别
        /// </summary>
        public string ConsultationCategory { get; set; }

        /// <summary>
        /// 会诊病情简要
        /// </summary>
        public string ConsultationResume { get; set; }

        /// <summary>
        /// 会诊时间
        /// </summary>
        public string ConsultationTime { get; set; }

        /// <summary>
        /// 会诊记录
        /// </summary>
        public string ConsultationRecord { get; set; }

        /// <summary>
        /// 获取医疗相关信息的字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, dynamic> GetMedicalInfoMap(string consultationDept = null)
        {
            var dic = new Dictionary<string, dynamic>();
            dic.Add("consultationApplyTime", ConsultationApplyTime);
            dic.Add("consultationDept", consultationDept ?? ConsultationDept);
            dic.Add("consultationCategory", ConsultationCategory);
            dic.Add("consultationResume", ConsultationResume);
            dic.Add("consultationTime", ConsultationTime);
            dic.Add("consultationRecord", ConsultationRecord);
            return dic;
        }

    }
}