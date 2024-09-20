namespace YiJian.Handover
{
    public class HandoverHistoryPatientsDataList
    {
        /// <summary> 患者id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public int? VisitNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string Bed { get; set; }


        /// <summary>
        /// 交班内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 交班医生名称
        /// </summary>
        public string HandoverDoctorName { get; set; }
    }
}