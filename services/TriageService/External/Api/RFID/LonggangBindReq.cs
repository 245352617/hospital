namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 龙岗绑定RFID请求参数类
    /// </summary>
    public class LonggangBindReq
    {
        /// <summary>
        /// 手环编号
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNumber { get; set; }

        /// <summary>
        /// 1：卒中
        /// 2：胸痛
        /// 4：创伤
        /// 8：急性呼吸衰竭
        /// 16：急性心衰
        /// 32：重型颅脑损伤
        /// </summary>
        public int Triage { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string RegisterName { get; set; }

        /// <summary>
        /// 登记号
        /// </summary>
        public string RegisterId { get; set; }
    }
}