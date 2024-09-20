namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 金湾生命体征信息
    /// </summary>
    public class VitalSignsInfoByJinWan
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 采集状态（1 已获取  2未获取）
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 收缩压（mmHg）
        /// </summary>
        public string ssy { get; set; }
        /// <summary>
        /// 舒张压（mmHg）
        /// </summary>
        public string szy { get; set; }
        /// <summary>
        /// 脉搏（次）
        /// </summary>
        public string mb { get; set; }
        /// <summary>
        /// 体温（℃）
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// 血氧（%）
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string SerialNumber { get; set; }

    }
}