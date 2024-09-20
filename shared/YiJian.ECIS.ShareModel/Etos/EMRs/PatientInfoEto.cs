namespace YiJian.ECIS.ShareModel.Etos.EMRs
{
    /// <summary>
    /// 患者相关信息
    /// </summary>
    public class PatientInfoEto
    {
        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 分诊科室
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string Narrationname { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        public string Presentmedicalhistory { get; set; }

        /// <summary>
        /// 获取患者相关信息的字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, dynamic> GetPatientInfoMap()
        {
            var dic = new Dictionary<string, dynamic>();
            dic.Add("patientName", PatientName);
            dic.Add("age", Age);
            dic.Add("sexName", SexName);
            dic.Add("visitNo", VisitNo);
            dic.Add("triageDeptName", TriageDeptName);
            dic.Add("ContactsPhone", ContactsPhone);
            dic.Add("Narrationname", Narrationname);
            dic.Add("Presentmedicalhistory", Presentmedicalhistory);
            return dic;
        }
    }
}